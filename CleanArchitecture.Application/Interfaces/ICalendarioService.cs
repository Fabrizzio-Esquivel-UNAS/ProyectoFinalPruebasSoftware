using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Calendarios;
using CleanArchitecture.Domain.DTOs.Calendly;

namespace CleanArchitecture.Application.Interfaces;

public interface ICalendarioService
{
    public Task<Guid> CreateCalendarioAsync(CreateCalendarioViewModel calendario);
    public Task UpdateCalendarioAsync(UpdateCalendarioViewModel calendario);
    public Task DeleteCalendarioAsync(Guid calendarioId);
    public Task<CalendarioViewModel> VincularCalendarioAsync(Guid calendarioId, string calendarioName);
    public Task SincronizarCalendarioAsync(Guid calendarioId);
    public Task<CalendarioViewModel?> GetCalendarioByIdAsync(Guid calendarioId);
    public Task<CalendarioViewModel?> GetCalendarioByUserIdAsync(Guid userId);
    public Task<string?> GetAccessTokenAsync(Guid userId);
    public string AuthCalendarioAsync(string redirectUri);
    public Task<CalendlyToken> GetTokensAsync(string code);
    public Task<PagedResult<CalendarioViewModel>> GetAllCalendariosAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}