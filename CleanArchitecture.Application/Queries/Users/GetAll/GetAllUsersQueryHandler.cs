using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Users;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Queries.Users.GetAll;

public sealed class GetAllUsersQueryHandler :
    IRequestHandler<GetAllUsersQuery, PagedResult<UserViewModel>>
{
    private readonly ISortingExpressionProvider<UserViewModel, User> _sortingExpressionProvider;
    private readonly IUserRepository _userRepository;
    private readonly ISolicitudRepository _solicitudRepository;
    private readonly IContratoRepository _contratoRepository;
    private readonly IHistorialCoordinadorRepository _historialCoordinadorRepository;

    public GetAllUsersQueryHandler(
        IUserRepository userRepository,
        ISolicitudRepository solicitudRepository,
        IContratoRepository contratoRepository,
        IHistorialCoordinadorRepository historialCoordinadorRepository,
        ISortingExpressionProvider<UserViewModel, User> sortingExpressionProvider)
    {
        _userRepository = userRepository;
        _solicitudRepository = solicitudRepository;
        _contratoRepository = contratoRepository;
        _historialCoordinadorRepository = historialCoordinadorRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<UserViewModel>> Handle(
        GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var usersQuery = _userRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            usersQuery = usersQuery.Where(user =>
                user.Email.Contains(request.SearchTerm) ||
                user.FirstName.Contains(request.SearchTerm) ||
                user.LastName.Contains(request.SearchTerm));
        }

        if (request.searchRoles != null)
        {
            usersQuery = usersQuery.Where(user =>
                request.searchRoles.Any(role => 
                    (int)user.Role == role));
        }

        var totalCount = await usersQuery.CountAsync(cancellationToken);

        usersQuery = usersQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var users = await usersQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .ToListAsync(cancellationToken);

        var usersViewModelList = new List<UserViewModel>();

        foreach (var user in users)
        {
            var userViewModel = UserViewModel.FromUser(user);
            if (user.Role == UserRole.Coordinador)
            {
                var historialCoordinador = await _historialCoordinadorRepository.GetLatestActiveCoordinadorByUserIdAsync(user.Id);

                if (historialCoordinador != null)
                {
                    userViewModel.GrupoInvestigacionId = historialCoordinador.GrupoInvestigacionId;
                }
            } 
            else if (user.Role == UserRole.Asesorado)
            {
                var solicitud = await _solicitudRepository.GetLatestAceptadoSolicitudByAsesoradoUserIdAsync(user.Id);
                if (solicitud != null)
                {
                    var contrato = await _contratoRepository.GetBySolicitudIdAsync(solicitud.Id);
                    if (contrato != null && contrato.FechaFinal == null)
                    {
                        userViewModel.AsesorId = solicitud.AsesorUserId;
                    }
                }
            }
            usersViewModelList.Add(userViewModel);
        }

        return new PagedResult<UserViewModel>(
            totalCount, usersViewModelList, request.Query.Page, request.Query.PageSize);
    }
}