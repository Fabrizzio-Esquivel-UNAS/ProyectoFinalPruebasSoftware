using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Contratos;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Queries.Contratos.GetAll;

public sealed class GetAllContratosQueryHandler :
    IRequestHandler<GetAllContratosQuery, PagedResult<ContratoViewModel>>
{
    private readonly ISortingExpressionProvider<ContratoViewModel, Contrato> _sortingExpressionProvider;
    private readonly IContratoRepository _contratoRepository;

    public GetAllContratosQueryHandler(
        IContratoRepository contratoRepository,
        ISortingExpressionProvider<ContratoViewModel, Contrato> sortingExpressionProvider)
    {
        _contratoRepository = contratoRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<ContratoViewModel>> Handle(
        GetAllContratosQuery request,
        CancellationToken cancellationToken)
    {
        var contratosQuery = _contratoRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        //if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        //{
        //    contratosQuery = contratosQuery.Where(contrato =>
        //        contrato.Nombre.Contains(request.SearchTerm));
        //}

        var totalCount = await contratosQuery.CountAsync(cancellationToken);

        contratosQuery = contratosQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var contratos = await contratosQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(contrato => ContratoViewModel.FromContrato(contrato))
            .ToListAsync(cancellationToken);

        return new PagedResult<ContratoViewModel>(
            totalCount, contratos, request.Query.Page, request.Query.PageSize);
    }
}