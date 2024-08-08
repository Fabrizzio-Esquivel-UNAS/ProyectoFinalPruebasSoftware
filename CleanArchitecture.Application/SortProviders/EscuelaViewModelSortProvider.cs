using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Escuelas;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.SortProviders;

public sealed class EscuelaViewModelSortProvider : ISortingExpressionProvider<EscuelaViewModel, Escuela>
{
    private static readonly Dictionary<string, Expression<Func<Escuela, object>>> s_expressions = new()
    {
        { "id", escuela => escuela.Id },
        { "nombre", escuela => escuela.Nombre }
    };

    public Dictionary<string, Expression<Func<Escuela, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}