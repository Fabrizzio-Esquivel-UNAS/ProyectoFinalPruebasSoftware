using System;
using CleanArchitecture.Application.ViewModels.Facultades;
using MediatR;

namespace CleanArchitecture.Application.Queries.Facultades.GetFacultadesById;

public sealed record GetFacultadesByIdQuery(Guid FacultadesId) : IRequest<FacultadViewModel?>;