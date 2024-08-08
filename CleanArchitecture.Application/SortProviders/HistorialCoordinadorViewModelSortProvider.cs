using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.HistorialCoordinadores;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.SortProviders;

public sealed class HistorialCoordinadorViewModelSortProvider : ISortingExpressionProvider<HistorialCoordinadorViewModel, HistorialCoordinador>
{
    private static readonly Dictionary<string, Expression<Func<HistorialCoordinador, object>>> s_expressions = new()
    {
        { "user", historialcoordinador => historialcoordinador.UserId },
        { "grupoInvestigacion", historialcoordinador => historialcoordinador.GrupoInvestigacionId },
        { "fecha inicio", historialcoordinador => historialcoordinador.FechaInicio },
    };






    public Dictionary<string, Expression<Func<HistorialCoordinador, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}
