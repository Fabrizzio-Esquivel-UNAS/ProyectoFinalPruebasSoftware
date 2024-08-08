using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Calendarios;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.SortProviders;

public sealed class CalendarioViewModelSortProvider : ISortingExpressionProvider<CalendarioViewModel, Calendario>
{
    private static readonly Dictionary<string, Expression<Func<Calendario, object>>> s_expressions = new()
    {
        { "id", calendario => calendario.Id },
        { "accessToken", calendario => calendario.AccessToken }
    };

    public Dictionary<string, Expression<Func<Calendario, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}