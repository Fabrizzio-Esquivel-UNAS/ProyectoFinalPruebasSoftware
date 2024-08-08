using System;
using System.Net.Http;
using System.Threading.Tasks;
using CleanArchitecture.Domain.DTOs.Calendly;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain.Interfaces;

public interface ICalendly
{
    HttpClient GetHttpClient(string accessToken);
    Task<T> GetDataAsync<T>(string apiUri, HttpClient httpClient, FormUrlEncodedContent? encodedContent = null);
    Task<T> GetDataAsync<T>(string apiUri, string accessToken, FormUrlEncodedContent? encodedContent = null);
    Task<CalendlyScheduledEventResponse> GetScheduledEvents(string userUri, HttpClient httpClient, string? pageToken);
    Task<CalendlyEventTypeResponse> GetEventTypes(string userUri, HttpClient httpClient, string? pageToken);
    Task<CalendlyInvitee> GetInvitee(string eventUri, HttpClient httpClient);
    Task<CalendlyToken> GetTokensWithAuthCode(string code);
    Task<CalendlyToken> GetTokensWithRefreshToken(string refreshToken);
    string GetAuthCode(string redirectUri);
}