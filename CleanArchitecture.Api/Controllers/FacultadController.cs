using System;
using System.Threading.Tasks;
using CleanArchitecture.Api.Models;
using CleanArchitecture.Api.Swagger;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Facultades;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
[Authorize]
[Route("/api/v1/[controller]")]
public sealed class FacultadController : ApiController
{
    private readonly IFacultadesService _facultadService;

    public FacultadController(
        INotificationHandler<DomainNotification> notifications,
        IFacultadesService FacultadService) : base(notifications)
    {
        _facultadService = FacultadService;
    }

    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation("Get a list of all facultades")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<FacultadViewModel>>))]
    public async Task<IActionResult> GetAllFacultadesAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<FacultadViewModelSortProvider, FacultadViewModel, Facultad>]
        SortQuery? sortQuery = null)
    {
        var facultades = await _facultadService.GetAllFacultadesAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);
        return Response(facultades);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [SwaggerOperation("Get a facultad by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<FacultadViewModel>))]
    public async Task<IActionResult> GetFacultadesByIdAsync([FromRoute] Guid id)
    {
        var facultad = await _facultadService.GetFacultadesByIdAsync(id);
        return Response(facultad);
    }

    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation("Create a new facultad")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateFacultadesAsync([FromBody] CreateFacultadViewModel facultad)
    {
        var facultadId = await _facultadService.CreateFacultadesAsync(facultad);
        return Response(facultadId);
    }

    [HttpPut]
    [AllowAnonymous]
    [SwaggerOperation("Update an existing facultad")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateFacultadViewModel>))]
    public async Task<IActionResult> UpdateFacultadesAsync([FromBody] UpdateFacultadViewModel facultad)
    {
        await _facultadService.UpdateFacultadesAsync(facultad);
        return Response(facultad);
    }

    [HttpDelete("{id}")]
    [AllowAnonymous]
    [SwaggerOperation("Delete an existing facultad")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteFacultadesAsync([FromRoute] Guid id)
    {
        await _facultadService.DeleteFacultadesAsync(id);
        return Response(id);
    }
}