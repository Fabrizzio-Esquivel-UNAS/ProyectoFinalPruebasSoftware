using System;
using System.Threading.Tasks;
using CleanArchitecture.Api.Models;
using CleanArchitecture.Api.Swagger;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.GruposInvestigacion;
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
public sealed class GrupoInvestigacionController : ApiController
{
    private readonly IGrupoInvestigacionService _grupoinvestigacionService;

    public GrupoInvestigacionController(
        INotificationHandler<DomainNotification> notifications,
        IGrupoInvestigacionService grupoinvestigacionService) : base(notifications)
    {
        _grupoinvestigacionService = grupoinvestigacionService;
    }

    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation("Get a list of all gruposInvestigacion")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<GrupoInvestigacionViewModel>>))]
    public async Task<IActionResult> GetAllGruposInvestigacionAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<GrupoInvestigacionViewModelSortProvider, GrupoInvestigacionViewModel, GrupoInvestigacion>]
        SortQuery? sortQuery = null)
    {
        var gruposInvestigacion = await _grupoinvestigacionService.GetAllGruposInvestigacionAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);
        return Response(gruposInvestigacion);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [SwaggerOperation("Get a grupoinvestigacion by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<GrupoInvestigacionViewModel>))]
    public async Task<IActionResult> GetGrupoInvestigacionByIdAsync([FromRoute] Guid id)
    {
        var grupoinvestigacion = await _grupoinvestigacionService.GetGrupoInvestigacionByIdAsync(id);
        return Response(grupoinvestigacion);
    }

    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation("Create a new grupoinvestigacion")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateGrupoInvestigacionAsync([FromBody] CreateGrupoInvestigacionViewModel grupoinvestigacion)
    {
        var grupoinvestigacionId = await _grupoinvestigacionService.CreateGrupoInvestigacionAsync(grupoinvestigacion);
        return Response(grupoinvestigacionId);
    }

    [HttpPut]
    [AllowAnonymous]
    [SwaggerOperation("Update an existing grupoinvestigacion")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateGrupoInvestigacionViewModel>))]
    public async Task<IActionResult> UpdateGrupoInvestigacionAsync([FromBody] UpdateGrupoInvestigacionViewModel grupoinvestigacion)
    {
        await _grupoinvestigacionService.UpdateGrupoInvestigacionAsync(grupoinvestigacion);
        return Response(grupoinvestigacion);
    }

    [HttpDelete("{id}")]
    [AllowAnonymous]
    [SwaggerOperation("Delete an existing grupoinvestigacion")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteGrupoInvestigacionAsync([FromRoute] Guid id)
    {
        await _grupoinvestigacionService.DeleteGrupoInvestigacionAsync(id);
        return Response(id);
    }
}