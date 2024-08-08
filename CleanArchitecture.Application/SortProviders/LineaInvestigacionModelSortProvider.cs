using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.LineasInvestigacion;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.SortProviders;

public sealed class LineaInvestigacionViewModelSortProvider : ISortingExpressionProvider<LineaInvestigacionViewModel, LineaInvestigacion>
{
    private static readonly Dictionary<string, Expression<Func<LineaInvestigacion, object>>> s_expressions = new()
    {
        { "id", lineainvestigacion => lineainvestigacion.Id },
        { "nombre", lineainvestigacion => lineainvestigacion.Nombre }
    };

    public Dictionary<string, Expression<Func<LineaInvestigacion, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}