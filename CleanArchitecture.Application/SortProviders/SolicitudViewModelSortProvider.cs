using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Solicitudes;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.SortProviders;

public sealed class SolicitudViewModelSortProvider : ISortingExpressionProvider<SolicitudViewModel, Solicitud>
{
    private static readonly Dictionary<string, Expression<Func<Solicitud, object>>> s_expressions = new()
    {
        { "id", solicitud => solicitud.Id },
        { "asesorUserId", solicitud => solicitud.AsesorUserId },
        { "asesoradoUserId", solicitud => solicitud.AsesoradoUserId },
        { "numeroTesis", solicitud => solicitud.NumeroTesis },
        { "fechacreacion", solicitud => solicitud.FechaCreacion ?? DateTimeOffset.MinValue },
        { "fecharespuesta", solicitud => solicitud.FechaRespuesta ?? DateTimeOffset.MinValue },
        { "estado", solicitud => solicitud.Estado }
    };

    public Dictionary<string, Expression<Func<Solicitud, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}