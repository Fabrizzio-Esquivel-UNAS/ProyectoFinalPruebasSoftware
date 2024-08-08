using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using CleanArchitecture.Domain.DTOs.Calendly;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Settings;
using Google.Apis.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CleanArchitecture.Domain;

public sealed class ApiCalendly : ICalendly
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly CalendlySettings _calendlySettings;

    public ApiCalendly(IHttpClientFactory httpClientFactory, IOptions<CalendlySettings> calendlySettings)
    {
        _httpClientFactory = httpClientFactory;
        _calendlySettings = calendlySettings.Value;
    }

    public HttpClient GetHttpClient(string? accessToken = null)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient("CalendlyClient");
        if (accessToken != null) {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
        return httpClient;
    }

    public async Task<T> GetDataAsync<T>(string apiUri, HttpClient httpClient, FormUrlEncodedContent? encodedContent = null)
    {
        HttpResponseMessage? response = null;
        try
        {
            response = encodedContent == null ? await httpClient.GetAsync(apiUri) : await httpClient.PostAsync(apiUri, encodedContent);
            response.EnsureSuccessStatusCode(); // Check for errors
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content) ?? throw new InvalidOperationException("Failed to deserialize response.");
        }
        catch (Exception e)
        {
            Console.WriteLine("ENCODED CONTENT: " + (encodedContent == null ? null : await encodedContent.ReadAsStringAsync()));
            throw new InvalidOperationException("Failed to get calendly data: "+ e.Message);
        }
    }

    public async Task<T> GetDataAsync<T>(string apiUri, string accessToken, FormUrlEncodedContent? encodedContent = null)
    {
        T? dataAsync;
        using (HttpClient httpClient = GetHttpClient(accessToken))
        {
            dataAsync = await GetDataAsync<T>(apiUri, httpClient, encodedContent);
        }
        return dataAsync;
    }

    public async Task<CalendlyScheduledEventResponse> GetScheduledEvents(string userUri, HttpClient httpClient, string? pageToken)
    {
        var apiUri = String.Format(_calendlySettings.ScheduledEventsEndpoint, userUri);
        if (pageToken != null)
            apiUri += "&page_token=" + pageToken;

        return await GetDataAsync<CalendlyScheduledEventResponse>(apiUri, httpClient);
    }

    public async Task<CalendlyEventTypeResponse> GetEventTypes(string userUri, HttpClient httpClient, string? pageToken)
    {
        var apiUri = String.Format(_calendlySettings.EventTypesEndpoint, userUri);
        if (pageToken != null)
            apiUri += "&page_token=" + pageToken;

        return await GetDataAsync<CalendlyEventTypeResponse>(apiUri, httpClient);
    }

    public async Task<CalendlyInvitee> GetInvitee(string eventUri, HttpClient httpClient)
    {
        var inviteesResponse = await GetDataAsync<CalendlyInviteeResponse>(String.Format(_calendlySettings.InviteesEndpoint, eventUri), httpClient);
        return inviteesResponse.Collection.First();
    }

    public string GetAuthCode(string redirectUri) {
        string authUrl = $"{_calendlySettings.AuthorizationEndpoint}?client_id={_calendlySettings.ClientId}&redirect_uri={Uri.EscapeDataString(redirectUri)}&response_type=code";
        return authUrl;
    }

    public async Task<CalendlyToken> GetTokensWithAuthCode(string code) {
        var content = new FormUrlEncodedContent(new Dictionary<string, string> {
            { "grant_type", "authorization_code" },
            { "client_id", _calendlySettings.ClientId },
            { "client_secret", _calendlySettings.ClientSecret },
            { "code", code },
            { "redirect_uri", _calendlySettings.RedirectUri }
        });

        return await GetDataAsync<CalendlyToken>(_calendlySettings.TokenEndpoint, GetHttpClient(), content);
    }

    public async Task<CalendlyToken> GetTokensWithRefreshToken(string refreshToken)
    {
        var content = new FormUrlEncodedContent(new Dictionary<string, string> {
            {"grant_type", "refresh_token"},
            {"refresh_token", refreshToken},
            {"client_id", _calendlySettings.ClientId},
            {"client_secret", _calendlySettings.ClientSecret}
        });

        return await GetDataAsync<CalendlyToken>(_calendlySettings.TokenEndpoint, GetHttpClient(), content);
    }
}