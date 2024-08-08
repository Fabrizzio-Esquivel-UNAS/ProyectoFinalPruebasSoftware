using System;
using System.Threading.Tasks;
using CleanArchitecture.Api.Models;
using CleanArchitecture.Api.Swagger;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Citas;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using CleanArchitecture.Domain.Settings;
using Google.Apis.Calendar.v3.Data;
using System.Threading;
using CleanArchitecture.Application.Services;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
[Authorize]
[Route("/api/v1/[controller]")]
public sealed class CitaController : ApiController
{
    private readonly ICitaService _citaService;

    public CitaController(
        INotificationHandler<DomainNotification> notifications,
        ICitaService citaService) : base(notifications)
    {
        _citaService = citaService;
    }

    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation("Get a list of all citas")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<CitaViewModel>>))]
    public async Task<IActionResult> GetAllCitasAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<CitaViewModelSortProvider, CitaViewModel, Cita>]
        SortQuery? sortQuery = null)
    {
        var citas = await _citaService.GetAllCitasAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);
        return Response(citas);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [SwaggerOperation("Get a cita by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<CitaViewModel>))]
    public async Task<IActionResult> GetCitaByIdAsync([FromRoute] Guid id)
    {
        var cita = await _citaService.GetCitaByIdAsync(id);
        return Response(cita);
    }

    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation("Create a new cita")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateOrUpdateCitaAsync([FromBody] CreateCitaViewModel cita)
    {
        var citaId = await _citaService.CreateOrUpdateCitaAsync(cita);
        return Response(citaId);
    }

    [HttpPut]
    [AllowAnonymous]
    [SwaggerOperation("Update an existing cita")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateCitaViewModel>))]
    public async Task<IActionResult> UpdateCitaAsync([FromBody] UpdateCitaViewModel cita)
    {
        await _citaService.UpdateCitaAsync(cita);
        return Response(cita);
    }

    [HttpDelete("{id}")]
    [AllowAnonymous]
    [SwaggerOperation("Delete an existing cita")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteCitaAsync([FromRoute] Guid id)
    {
        await _citaService.DeleteCitaAsync(id);
        return Response(id);
    }
}