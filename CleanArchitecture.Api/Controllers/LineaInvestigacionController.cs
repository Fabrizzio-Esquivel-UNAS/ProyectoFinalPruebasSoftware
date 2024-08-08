using System;
using System.Threading.Tasks;
using CleanArchitecture.Api.Models;
using CleanArchitecture.Api.Swagger;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.LineasInvestigacion;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using CleanArchitecture.Application.Services;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
[AllowAnonymous]
//[Authorize]
[Route("/api/v1/[controller]")]
public sealed class LineaInvestigacionController : ApiController
{
    private readonly ILineaInvestigacionService _lineainvestigacionService;

    public LineaInvestigacionController(
        INotificationHandler<DomainNotification> notifications,
        ILineaInvestigacionService lineainvestigacionService) : base(notifications)
    {
        _lineainvestigacionService = lineainvestigacionService;
    }

    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation("Get a list of all lineasInvestigacion")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<LineaInvestigacionViewModel>>))]
    public async Task<IActionResult> GetAllLineasInvestigacionAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<LineaInvestigacionViewModelSortProvider, LineaInvestigacionViewModel, LineaInvestigacion>]
        SortQuery? sortQuery = null)
    {
        var lineasInvestigacion = await _lineainvestigacionService.GetAllLineasInvestigacionAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);
        return Response(lineasInvestigacion);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [SwaggerOperation("Get a lineainvestigacion by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<LineaInvestigacionViewModel>))]
    public async Task<IActionResult> GetLineaInvestigacionByIdAsync([FromRoute] Guid id)
    {
        var lineainvestigacion = await _lineainvestigacionService.GetLineaInvestigacionByIdAsync(id);
        return Response(lineainvestigacion);
    }

    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation("Create a new lineainvestigacion")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateLineaInvestigacionAsync([FromBody] CreateLineaInvestigacionViewModel lineainvestigacion)
    {
        var lineainvestigacionId = await _lineainvestigacionService.CreateLineaInvestigacionAsync(lineainvestigacion);
        return Response(lineainvestigacionId);
    }

    [HttpPut]
    [AllowAnonymous]
    [SwaggerOperation("Update an existing lineainvestigacion")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateLineaInvestigacionViewModel>))]
    public async Task<IActionResult> UpdateLineaInvestigacionAsync([FromBody] UpdateLineaInvestigacionViewModel lineainvestigacion)
    {
        await _lineainvestigacionService.UpdateLineaInvestigacionAsync(lineainvestigacion);
        return Response(lineainvestigacion);
    }

    [HttpDelete("{id}")]
    [AllowAnonymous]
    [SwaggerOperation("Delete an existing lineainvestigacion")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteLineaInvestigacionAsync([FromRoute] Guid id)
    {
        await _lineainvestigacionService.DeleteLineaInvestigacionAsync(id);
        return Response(id);
    }
}