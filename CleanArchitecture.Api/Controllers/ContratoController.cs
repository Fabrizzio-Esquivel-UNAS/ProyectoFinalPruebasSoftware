using System;
using System.Threading.Tasks;
using CleanArchitecture.Api.Models;
using CleanArchitecture.Api.Swagger;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Contratos;
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
public sealed class ContratoController : ApiController
{
    private readonly IContratoService _contratoService;

    public ContratoController(
        INotificationHandler<DomainNotification> notifications,
        IContratoService contratoService) : base(notifications)
    {
        _contratoService = contratoService;
    }

    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation("Get a list of all contratos")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<ContratoViewModel>>))]
    public async Task<IActionResult> GetAllContratosAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<ContratoViewModelSortProvider, ContratoViewModel, Contrato>]
        SortQuery? sortQuery = null)
    {
        var contratos = await _contratoService.GetAllContratosAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);
        return Response(contratos);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [SwaggerOperation("Get a contrato by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<ContratoViewModel>))]
    public async Task<IActionResult> GetContratoByIdAsync([FromRoute] Guid id)
    {
        var contrato = await _contratoService.GetContratoByIdAsync(id);
        return Response(contrato);
    }

    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation("Create a new contrato")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateContratoAsync([FromBody] CreateContratoViewModel contrato)
    {
        var contratoId = await _contratoService.CreateContratoAsync(contrato);
        return Response(contratoId);
    }

    [HttpPut]
    [AllowAnonymous]
    [SwaggerOperation("Update an existing contrato")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateContratoViewModel>))]
    public async Task<IActionResult> UpdateContratoAsync([FromBody] UpdateContratoViewModel contrato)
    {
        await _contratoService.UpdateContratoAsync(contrato);
        return Response(contrato);
    }

    [HttpDelete("{id}")]
    [AllowAnonymous]
    [SwaggerOperation("Delete an existing contrato")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteContratoAsync([FromRoute] Guid id)
    {
        await _contratoService.DeleteContratoAsync(id);
        return Response(id);
    }
}