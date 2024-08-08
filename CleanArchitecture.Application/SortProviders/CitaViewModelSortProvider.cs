using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Citas;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.SortProviders;

public sealed class CitaViewModelSortProvider : ISortingExpressionProvider<CitaViewModel, Cita>
{
    private static readonly Dictionary<string, Expression<Func<Cita, object>>> s_expressions = new()
    {
        { "id", cita => cita.Id },
        { "eventoId", cita => cita.EventoId },
        { "asesorUserId", cita => cita.AsesorUserId },
        { "asesoradoUserId", cita => cita.AsesoradoUserId },
    };

    public Dictionary<string, Expression<Func<Cita, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}