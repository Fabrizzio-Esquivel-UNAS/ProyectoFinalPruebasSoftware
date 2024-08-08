using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.GruposInvestigacion;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.SortProviders;

public sealed class GrupoInvestigacionViewModelSortProvider : ISortingExpressionProvider<GrupoInvestigacionViewModel, GrupoInvestigacion>
{
    private static readonly Dictionary<string, Expression<Func<GrupoInvestigacion, object>>> s_expressions = new()
    {
        { "id", grupoinvestigacion => grupoinvestigacion.Id },
        { "nombre", grupoinvestigacion => grupoinvestigacion.Nombre }
    };

    public Dictionary<string, Expression<Func<GrupoInvestigacion, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}