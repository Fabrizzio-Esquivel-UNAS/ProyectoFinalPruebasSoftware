using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Contratos;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.SortProviders;

public sealed class ContratoViewModelSortProvider : ISortingExpressionProvider<ContratoViewModel, Contrato>
{
    private static readonly Dictionary<string, Expression<Func<Contrato, object>>> s_expressions = new()
    {
        { "fechaInicio", contrato => contrato.FechaInicio },
    };

    public Dictionary<string, Expression<Func<Contrato, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}