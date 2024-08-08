using System;
using System.Threading.Tasks;
using CleanArchitecture.Api.Models;
using CleanArchitecture.Api.Swagger;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Escuelas;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
[AllowAnonymous]
//[Authorize]
[Route("/api/v1/[controller]")]
public sealed class EscuelaController : ApiController
{
    private readonly IEscuelasService _escuelaService;

    public EscuelaController(
        INotificationHandler<DomainNotification> notifications,
        IEscuelasService EscuelaService) : base(notifications)
    {
        _escuelaService = EscuelaService;
    }

    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation("Get a list of all escuelas")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<EscuelaViewModel>>))]
    public async Task<IActionResult> GetAllEscuelasAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<EscuelaViewModelSortProvider, EscuelaViewModel, Escuela>]
        SortQuery? sortQuery = null)
    {
        var escuelas = await _escuelaService.GetAllEscuelasAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);
        return Response(escuelas);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [SwaggerOperation("Get a escuela by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<EscuelaViewModel>))]
    public async Task<IActionResult> GetEscuelasByIdAsync([FromRoute] Guid id)
    {
        var escuela = await _escuelaService.GetEscuelasByIdAsync(id);
        return Response(escuela);
    }

    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation("Create a new escuela")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateEscuelasAsync([FromBody] CreateEscuelaViewModel escuela)
    {
        var escuelaId = await _escuelaService.CreateEscuelasAsync(escuela);
        return Response(escuelaId);
    }

    [HttpPut]
    [AllowAnonymous]
    [SwaggerOperation("Update an existing escuela")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateEscuelaViewModel>))]
    public async Task<IActionResult> UpdateEscuelasAsync([FromBody] UpdateEscuelaViewModel escuela)
    {
        await _escuelaService.UpdateEscuelasAsync(escuela);
        return Response(escuela);
    }

    [HttpDelete("{id}")]
    [AllowAnonymous]
    [SwaggerOperation("Delete an existing escuela")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteEscuelasAsync([FromRoute] Guid id)
    {
        await _escuelaService.DeleteEscuelasAsync(id);
        return Response(id);
    }
}