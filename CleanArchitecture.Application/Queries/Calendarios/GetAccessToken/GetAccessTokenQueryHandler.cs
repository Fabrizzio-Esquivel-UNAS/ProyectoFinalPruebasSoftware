using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels.Calendarios;
using CleanArchitecture.Domain.Commands.Calendarios.UpdateCalendario;
using CleanArchitecture.Domain.DTOs.Calendly;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Domain.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CleanArchitecture.Application.Queries.Calendarios.GetAccessToken;

public sealed class GetAccessTokenQueryHandler :
    IRequestHandler<GetAccessTokenQuery, CalendarioViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly ICalendarioRepository _calendarioRepository;
    private readonly CalendlySettings _calendlySettings;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IUserRepository _userRepository;
    private readonly IUser _user;

    public GetAccessTokenQueryHandler(ICalendarioRepository calendarioRepository, IMediatorHandler bus, IOptions<CalendlySettings> calendlySettings, IHttpClientFactory httpClientFactory, IUserRepository userRepository, IUser user)
    {
        _calendarioRepository = calendarioRepository;
        _bus = bus;
        _calendlySettings = calendlySettings.Value;
        _httpClientFactory = httpClientFactory;
        _userRepository = userRepository;
        _user = user;
    }

    public async Task<CalendarioViewModel?> Handle(GetAccessTokenQuery request, CancellationToken cancellationToken)
    {

        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetAccessTokenQuery),
                    $"User with id {request.UserId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        // Retrieve user tokens from the database
        if (user.Calendario == null || string.IsNullOrEmpty(user.Calendario.RefreshToken))
            throw new Exception("No refresh token found.");

        // Check if the access token is still valid
        if (user.Calendario.AccessTokenExpiration > DateTime.UtcNow)
            return CalendarioViewModel.FromCalendario(user.Calendario);

        // Token is expired, refresh it
        var newTokens = await RefreshAccessTokenAsync(user.Calendario.RefreshToken);

        // Update the database with the new tokens
        user.Calendario.AccessToken = newTokens.AccessToken;
        user.Calendario.AccessTokenExpiration = DateTime.UtcNow.AddSeconds(newTokens.ExpiresIn);

        await _bus.SendCommandAsync(new UpdateCalendarioCommand(
            user.Calendario.Id,
            user.Calendario.UserUri,
            user.Calendario.AccessToken,
            user.Calendario.RefreshToken,
            user.Calendario.AccessTokenExpiration,
            user.Calendario.RefreshTokenExpiration,
            user.Calendario.EventType,
            user.Calendario.EventsPageToken,
            user.Calendario.SchedulingUrl));

        return CalendarioViewModel.FromCalendario(user.Calendario);
    }

    // Method to refresh the access token using the refresh token
    private async Task<CalendlyToken> RefreshAccessTokenAsync(string refreshToken)
    {
        //var client = _httpClientFactory.CreateClient("CalendlyClient");
        var client = _httpClientFactory.CreateClient();

        // Create the request to refresh the token
        var response = await client.PostAsync("https://auth.calendly.com/oauth/token",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "refresh_token"},
                {"refresh_token", refreshToken},
                {"client_id", _calendlySettings.ClientId},
                {"client_secret", _calendlySettings.ClientSecret}
            }));

        // Ensure the response indicates success
        response.EnsureSuccessStatusCode();

        // Read and deserialize the response content
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CalendlyToken>(responseContent) ?? throw new InvalidOperationException();
    }
}