using CleanArchitecture.Domain.Commands.Tenants.CreateTenant;
using CleanArchitecture.Domain.Commands.Tenants.DeleteTenant;
using CleanArchitecture.Domain.Commands.Tenants.UpdateTenant;
using CleanArchitecture.Domain.Commands.Facultades.CreateFacultad;
using CleanArchitecture.Domain.Commands.Facultades.DeleteFacultad;
using CleanArchitecture.Domain.Commands.Facultades.UpdateFacultad;
using CleanArchitecture.Domain.Commands.Users.ChangePreferencias;
using CleanArchitecture.Domain.Commands.Users.ChangePassword;
using CleanArchitecture.Domain.Commands.Users.CreateUser;
using CleanArchitecture.Domain.Commands.Users.DeleteUser;
using CleanArchitecture.Domain.Commands.Users.LoginUser;
using CleanArchitecture.Domain.Commands.Users.UpdateUser;
using CleanArchitecture.Domain.EventHandler;
using CleanArchitecture.Domain.EventHandler.Fanout;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Shared.Events.Tenant;
using CleanArchitecture.Shared.Events.Facultad;
using CleanArchitecture.Shared.Events.User;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Domain.Commands.Escuelas.CreateEscuela;
using CleanArchitecture.Domain.Commands.Escuelas.UpdateEscuela;
using CleanArchitecture.Domain.Commands.Escuelas.DeleteEscuela;
using CleanArchitecture.Shared.Events.Escuela;
using CleanArchitecture.Domain.Commands.Citas.CreateCita;
using CleanArchitecture.Domain.Commands.Citas.DeleteCita;
using CleanArchitecture.Domain.Commands.Citas.UpdateCita;
using CleanArchitecture.Shared.Events.Cita;
using CleanArchitecture.Domain.Commands.Calendarios.CreateCalendario;
using CleanArchitecture.Domain.Commands.Calendarios.UpdateCalendario;
using CleanArchitecture.Domain.Commands.Calendarios.DeleteCalendario;
using CleanArchitecture.Shared.Events.Calendario;
using CleanArchitecture.Domain.Commands.GruposInvestigacion.CreateGrupoInvestigacion;
using CleanArchitecture.Domain.Commands.GruposInvestigacion.DeleteGrupoInvestigacion;
using CleanArchitecture.Domain.Commands.GruposInvestigacion.UpdateGrupoInvestigacion;
using CleanArchitecture.Domain.Commands.HistorialCoordinadores.CreateHistorialCoordinador;
using CleanArchitecture.Domain.Commands.HistorialCoordinadores.DeleteHistorialCoordinador;
using CleanArchitecture.Domain.Commands.HistorialCoordinadores.UpdateHistorialCoordinador;
using CleanArchitecture.Domain.Commands.LineasInvestigacion.CreateLineaInvestigacion;
using CleanArchitecture.Domain.Commands.LineasInvestigacion.DeleteLineaInvestigacion;
using CleanArchitecture.Domain.Commands.LineasInvestigacion.UpdateLineaInvestigacion;
using CleanArchitecture.Domain.Commands.Contratos.CreateContrato;
using CleanArchitecture.Domain.Commands.Contratos.DeleteContrato;
using CleanArchitecture.Domain.Commands.Contratos.UpdateContrato;
using CleanArchitecture.Domain.Commands.Solicitudes.CreateSolicitud;
using CleanArchitecture.Domain.Commands.Solicitudes.DeleteSolicitud;
using CleanArchitecture.Domain.Commands.Solicitudes.UpdateSolicitud;
using CleanArchitecture.Shared.Events.GrupoInvestigacion;
using CleanArchitecture.Shared.Events.HistorialCoordinador;
using CleanArchitecture.Shared.Events.LineaInvestigacion;
using CleanArchitecture.Shared.Events.Contrato;
using CleanArchitecture.Shared.Events.Solicitud;
using CleanArchitecture.Domain.DTOs;

namespace CleanArchitecture.Domain.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        // User
        services.AddScoped<IRequestHandler<CreateUserCommand>, CreateUserCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateUserCommand>, UpdateUserCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteUserCommand>, DeleteUserCommandHandler>();
        services.AddScoped<IRequestHandler<ChangePasswordCommand>, ChangePasswordCommandHandler>();
        services.AddScoped<IRequestHandler<ChangePreferenciasCommand>, ChangePreferenciasCommandHandler>();
        services.AddScoped<IRequestHandler<LoginUserCommand, TokenResponse>, LoginUserCommandHandler>();

        // Tenant
        services.AddScoped<IRequestHandler<CreateTenantCommand>, CreateTenantCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateTenantCommand>, UpdateTenantCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteTenantCommand>, DeleteTenantCommandHandler>();

        // Calendario
        services.AddScoped<IRequestHandler<CreateCalendarioCommand>, CreateCalendarioCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateCalendarioCommand>, UpdateCalendarioCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteCalendarioCommand>, DeleteCalendarioCommandHandler>();

        // Contrato
        services.AddScoped<IRequestHandler<CreateContratosCommand>, CreateContratosCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateContratosCommand>, UpdateContratosCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteContratosCommand>, DeleteContratosCommandHandler>();

        // Facultad
        services.AddScoped<IRequestHandler<CreateFacultadesCommand>, CreateFacultadesCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateFacultadesCommand>, UpdateFacultadesCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteFacultadesCommand>, DeleteFacultadesCommandHandler>();

        // Escuela
        services.AddScoped<IRequestHandler<CreateEscuelasCommand>, CreateEscuelasCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEscuelasCommand>, UpdateEscuelasCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteEscuelasCommand>, DeleteEscuelasCommandHandler>();

        // Grupoinvestigacion
        services.AddScoped<IRequestHandler<CreateGrupoInvestigacionCommand>, CreateGrupoInvestigacionCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateGrupoInvestigacionCommand>, UpdateGrupoInvestigacionCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteGrupoInvestigacionCommand>, DeleteGrupoInvestigacionCommandHandler>();

        // HistorialCoordinador
        services.AddScoped<IRequestHandler<CreateHistorialCoordinadorCommand>, CreateHistorialCoordinadorCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateHistorialCoordinadorCommand>, UpdateHistorialCoordinadorCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteHistorialCoordinadorCommand>, DeleteHistorialCoordinadorCommandHandler>();

        // Solicitud
        services.AddScoped<IRequestHandler<CreateSolicitudCommand>, CreateSolicitudCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateSolicitudCommand>, UpdateSolicitudCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteSolicitudCommand>, DeleteSolicitudCommandHandler>();

        // LineaInvestigacion
        services.AddScoped<IRequestHandler<CreateLineaInvestigacionCommand>, CreateLineaInvestigacionCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateLineaInvestigacionCommand>, UpdateLineaInvestigacionCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteLineaInvestigacionCommand>, DeleteLineaInvestigacionCommandHandler>();

        // Cita
        services.AddScoped<IRequestHandler<CreateOrUpdateCitaCommand>, CreateOrUpdateCitaCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateCitaCommand>, UpdateCitaCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteCitaCommand>, DeleteCitaCommandHandler>();

        return services;
    }

    public static IServiceCollection AddNotificationHandlers(this IServiceCollection services)
    {
        // Fanout
        services.AddScoped<IFanoutEventHandler, FanoutEventHandler>();

        // User
        services.AddScoped<INotificationHandler<UserCreatedEvent>, UserEventHandler>();
        services.AddScoped<INotificationHandler<UserUpdatedEvent>, UserEventHandler>();
        services.AddScoped<INotificationHandler<UserDeletedEvent>, UserEventHandler>();
        services.AddScoped<INotificationHandler<PasswordChangedEvent>, UserEventHandler>();

        // Tenant
        services.AddScoped<INotificationHandler<TenantCreatedEvent>, TenantEventHandler>();
        services.AddScoped<INotificationHandler<TenantUpdatedEvent>, TenantEventHandler>();
        services.AddScoped<INotificationHandler<TenantDeletedEvent>, TenantEventHandler>();

        // Calendario
        services.AddScoped<INotificationHandler<CalendarioCreatedEvent>, CalendarioEventHandler>();
        services.AddScoped<INotificationHandler<CalendarioUpdatedEvent>, CalendarioEventHandler>();
        services.AddScoped<INotificationHandler<CalendarioDeletedEvent>, CalendarioEventHandler>();

        // Contrato
        services.AddScoped<INotificationHandler<ContratoCreatedEvent>, ContratoEventHandler>();
        services.AddScoped<INotificationHandler<ContratoUpdatedEvent>, ContratoEventHandler>();
        services.AddScoped<INotificationHandler<ContratoDeletedEvent>, ContratoEventHandler>();

        // Facultad
        services.AddScoped<INotificationHandler<FacultadCreatedEvent>, FacultadesEventHandler>();
        services.AddScoped<INotificationHandler<FacultadUpdatedEvent>, FacultadesEventHandler>();
        services.AddScoped<INotificationHandler<FacultadDeletedEvent>, FacultadesEventHandler>();

        // Escuela
        services.AddScoped<INotificationHandler<EscuelaCreatedEvent>, EscuelasEventHandler>();
        services.AddScoped<INotificationHandler<EscuelaUpdatedEvent>, EscuelasEventHandler>();
        services.AddScoped<INotificationHandler<EscuelaDeletedEvent>, EscuelasEventHandler>();

        // GrupoInvestigacion
        services.AddScoped<INotificationHandler<GrupoInvestigacionCreatedEvent>, GrupoInvestigacionEventHandler>();
        services.AddScoped<INotificationHandler<GrupoInvestigacionUpdatedEvent>, GrupoInvestigacionEventHandler>();
        services.AddScoped<INotificationHandler<GrupoInvestigacionDeletedEvent>, GrupoInvestigacionEventHandler>();

        // HistorialCoordinador
        services.AddScoped<INotificationHandler<HistorialCoordinadorCreatedEvent>, HistorialCoordinadorEventHandler>();
        services.AddScoped<INotificationHandler<HistorialCoordinadorUpdatedEvent>, HistorialCoordinadorEventHandler>();
        services.AddScoped<INotificationHandler<HistorialCoordinadorDeletedEvent>, HistorialCoordinadorEventHandler>();

        // Solicitud
        services.AddScoped<INotificationHandler<SolicitudCreatedEvent>, SolicitudEventHandler>();
        services.AddScoped<INotificationHandler<SolicitudUpdatedEvent>, SolicitudEventHandler>();
        services.AddScoped<INotificationHandler<SolicitudDeletedEvent>, SolicitudEventHandler>();

        // LineaInvestigacion
        services.AddScoped<INotificationHandler<LineaInvestigacionCreatedEvent>, LineasInvestigacionEventHandler>();
        services.AddScoped<INotificationHandler<LineaInvestigacionUpdatedEvent>, LineasInvestigacionEventHandler>();
        services.AddScoped<INotificationHandler<LineaInvestigacionDeletedEvent>, LineasInvestigacionEventHandler>();

        // Cita
        services.AddScoped<INotificationHandler<CitaCreatedEvent>, CitaEventHandler>();
        services.AddScoped<INotificationHandler<CitaUpdatedEvent>, CitaEventHandler>();
        services.AddScoped<INotificationHandler<CitaDeletedEvent>, CitaEventHandler>();

        return services;
    }

    public static IServiceCollection AddApiUser(this IServiceCollection services)
    {
        // User
        services.AddScoped<IUser, ApiUser>();

        return services;
    }

    public static IServiceCollection AddApiCalendly(this IServiceCollection services)
    {
        // Calendly
        services.AddScoped<ICalendly, ApiCalendly>();

        return services;
    }

    public static IServiceCollection AddApiNotifications(this IServiceCollection services)
    {
        // Calendly
        services.AddScoped<IUserNotifications, ApiNotifications>();

        return services;
    }
}