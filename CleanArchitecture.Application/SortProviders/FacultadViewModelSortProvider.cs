using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Facultades;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.SortProviders;

public sealed class FacultadViewModelSortProvider : ISortingExpressionProvider<FacultadViewModel, Facultad>
{
    private static readonly Dictionary<string, Expression<Func<Facultad, object>>> s_expressions = new()
    {
        { "id", facultad => facultad.Id },
        { "nombre", facultad => facultad.Nombre }
    };

    public Dictionary<string, Expression<Func<Facultad, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}