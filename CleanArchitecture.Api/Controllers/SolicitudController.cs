using System;
using System.Threading.Tasks;
using CleanArchitecture.Api.Models;
using CleanArchitecture.Api.Swagger;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Solicitudes;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using CleanArchitecture.Domain.Settings;
using Google.Apis.Calendar.v3.Data;
using System.Threading;
using Microsoft.AspNetCore.Cors;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
[Authorize]
[Route("/api/v1/[controller]")]
public sealed class SolicitudController : ApiController
{
    private readonly ISolicitudService _solicitudService;

    public SolicitudController(
        INotificationHandler<DomainNotification> notifications,
        ISolicitudService solicitudService) : base(notifications)
    {
        _solicitudService = solicitudService;

    }

    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation("Get a list of all solicitudes")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<SolicitudViewModel>>))]
    public async Task<IActionResult> GetAllSolicitudesAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<SolicitudViewModelSortProvider, SolicitudViewModel, Solicitud>]
        SortQuery? sortQuery = null)
    {
        var solicitudes = await _solicitudService.GetAllSolicitudesAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);
        return Response(solicitudes);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [SwaggerOperation("Get a solicitud by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<SolicitudViewModel>))]
    public async Task<IActionResult> GetSolicitudByIdAsync([FromRoute] Guid id)
    {
        var solicitud = await _solicitudService.GetSolicitudByIdAsync(id);
        return Response(solicitud);
    }

    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation("Create a new solicitud")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateSolicitudAsync([FromBody] CreateSolicitudViewModel solicitud)
    {
        var solicitudId = await _solicitudService.CreateSolicitudAsync(solicitud);
        return Response(solicitudId);
    }

    [HttpPut]
    [AllowAnonymous]
    [SwaggerOperation("Update an existing solicitud")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateSolicitudViewModel>))]
    public async Task<IActionResult> UpdateSolicitudAsync([FromBody] UpdateSolicitudViewModel solicitud)
    {
        await _solicitudService.UpdateSolicitudAsync(solicitud);
        return Response(solicitud);
    }

    [HttpDelete("{id}")]
    [AllowAnonymous]
    [SwaggerOperation("Delete an existing solicitud")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteSolicitudAsync([FromRoute] Guid id)
    {
        await _solicitudService.DeleteSolicitudAsync(id);
        return Response(id);
    }
}