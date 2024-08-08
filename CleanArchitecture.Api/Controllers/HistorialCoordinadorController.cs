using System;
using System.Threading.Tasks;
using CleanArchitecture.Api.Models;
using CleanArchitecture.Api.Swagger;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.HistorialCoordinadores;
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
public sealed class HistorialCoordinadorController : ApiController
{
    private readonly IHistorialCoordinadorService _historialcoordinadorService;

    public HistorialCoordinadorController(
        INotificationHandler<DomainNotification> notifications,
        IHistorialCoordinadorService historialcoordinadorService) : base(notifications)
    {
        _historialcoordinadorService = historialcoordinadorService;
    }

    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation("Get a list of all historialcoordinadores")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<HistorialCoordinadorViewModel>>))]
    public async Task<IActionResult> GetAllHistorialCoordinadoresAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<HistorialCoordinadorViewModelSortProvider, HistorialCoordinadorViewModel, HistorialCoordinador>]
        SortQuery? sortQuery = null)
    {
        var historialcoordinadores = await _historialcoordinadorService.GetAllHistorialCoordinadoresAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);
        return Response(historialcoordinadores);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [SwaggerOperation("Get a historialcoordinador by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<HistorialCoordinadorViewModel>))]
    public async Task<IActionResult> GetHistorialCoordinadorByIdAsync([FromRoute] Guid id)
    {
        var historialcoordinador = await _historialcoordinadorService.GetHistorialCoordinadorByIdAsync(id);
        return Response(historialcoordinador);
    }

    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation("Create a new historialcoordinador")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateHistorialCoordinadorAsync([FromBody] CreateHistorialCoordinadorViewModel historialcoordinador)
    {
        var historialcoordinadorId = await _historialcoordinadorService.CreateHistorialCoordinadorAsync(historialcoordinador);
        return Response(historialcoordinadorId);
    }

    [HttpPut]
    [AllowAnonymous]
    [SwaggerOperation("Update an existing historialcoordinador")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateHistorialCoordinadorViewModel>))]
    public async Task<IActionResult> UpdateHistorialCoordinadorAsync([FromBody] UpdateHistorialCoordinadorViewModel historialcoordinador)
    {
        await _historialcoordinadorService.UpdateHistorialCoordinadorAsync(historialcoordinador);
        return Response(historialcoordinador);
    }

    [HttpDelete("{id}")]
    [AllowAnonymous]
    [SwaggerOperation("Delete an existing historialcoordinador")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteHistorialCoordinadorAsync([FromRoute] Guid id)
    {
        await _historialcoordinadorService.DeleteHistorialCoordinadorAsync(id);
        return Response(id);
    }
}