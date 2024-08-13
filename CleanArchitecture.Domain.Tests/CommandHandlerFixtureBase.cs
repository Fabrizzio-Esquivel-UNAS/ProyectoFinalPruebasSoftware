using System;
using System.Linq.Expressions;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Domain.Settings;
using CleanArchitecture.Shared.Events;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests;

public class CommandHandlerFixtureBase
{
    protected IMediatorHandler Bus { get; }
    protected IUnitOfWork UnitOfWork { get; }
    protected DomainNotificationHandler NotificationHandler { get; }
    protected IUser User { get; }
    protected ISolicitudRepository SolicitudRepository { get; }
    protected IUserRepository UserRepository { get; }
    protected IContratoRepository ContratoRepository { get; }
    protected ICitaRepository CitaRepository { get; }
    protected IOptionsSnapshot<AsesoriaSettings> AsesoriaSettings { get; }
    protected IUserNotifications UserNotifications { get; }

    protected CommandHandlerFixtureBase()
    {
        Bus = Substitute.For<IMediatorHandler>();
        UnitOfWork = Substitute.For<IUnitOfWork>();
        NotificationHandler = Substitute.For<DomainNotificationHandler>();
        User = Substitute.For<IUser>();
        SolicitudRepository = Substitute.For<ISolicitudRepository>();
        UserRepository = Substitute.For<IUserRepository>();
        ContratoRepository = Substitute.For<IContratoRepository>();
        CitaRepository = Substitute.For<ICitaRepository>();
        AsesoriaSettings = Substitute.For<IOptionsSnapshot<AsesoriaSettings>>();
        UserNotifications = Substitute.For<IUserNotifications>();

        User.GetUserId().Returns(Guid.NewGuid());
        User.GetUserRole().Returns(UserRole.Admin);

        UnitOfWork.CommitAsync().Returns(true);
    }

    public CommandHandlerFixtureBase VerifyExistingNotification(string errorCode, string message)
    {
        Bus.Received(1).RaiseEventAsync(
            Arg.Is<DomainNotification>(not => not.Code == errorCode && not.Value == message));

        return this;
    }

    public CommandHandlerFixtureBase VerifyAnyDomainNotification()
    {
        Bus.Received(1).RaiseEventAsync(Arg.Any<DomainNotification>());

        return this;
    }

    public CommandHandlerFixtureBase VerifyNoDomainNotification()
    {
        Bus.DidNotReceive().RaiseEventAsync(Arg.Any<DomainNotification>());

        return this;
    }

    public CommandHandlerFixtureBase VerifyNoRaisedEvent<TEvent>()
        where TEvent : DomainEvent
    {
        Bus.DidNotReceive().RaiseEventAsync(Arg.Any<TEvent>());

        return this;
    }

    public CommandHandlerFixtureBase VerifyNoRaisedEvent<TEvent>(Expression<Predicate<TEvent>> checkFunction)
        where TEvent : DomainEvent
    {
        Bus.DidNotReceive().RaiseEventAsync(Arg.Is(checkFunction));

        return this;
    }

    public CommandHandlerFixtureBase VerifyNoCommit()
    {
        UnitOfWork.DidNotReceive().CommitAsync();

        return this;
    }

    public CommandHandlerFixtureBase VerifyCommit()
    {
        UnitOfWork.Received(1).CommitAsync();

        return this;
    }

    public CommandHandlerFixtureBase VerifyRaisedEvent<TEvent>(Expression<Predicate<TEvent>> checkFunction)
        where TEvent : DomainEvent
    {
        Bus.Received(1).RaiseEventAsync(Arg.Is(checkFunction));

        return this;
    }

    public void SetupCurrentUser(UserRole userRole = UserRole.User)
    {
        User.GetUserRole().Returns(userRole);
    }

    public Entities.User SetupUserWithRole(UserRole role)
    {
        var user = new Entities.User(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            "max@mustermann.com",
            "Max",
            "Mustermann",
            "Password",
            "123456789",
            "0123456789",
            role);

        UserRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == user.Id))
            .Returns(user);

        UserRepository
            .GetByEmailAsync(Arg.Is<string>(y => y == user.Email))
            .Returns(user);

        return user;
    }

    public Entities.Solicitud SetupSolicitud(Guid? asesoradoUserId = null, Guid? asesorUserId = null)
    {
        var solicitud = new Entities.Solicitud(
            Guid.NewGuid(),
            asesoradoUserId ?? Guid.NewGuid(),
            asesorUserId ?? Guid.NewGuid(),
            1,
            "Test solicitud");

        SolicitudRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == solicitud.Id))
            .Returns(solicitud);

        SolicitudRepository
            .GetLatestAceptadoSolicitudByAsesoradoUserIdAsync(Arg.Is<Guid>(y => y == solicitud.AsesoradoUserId))
            .Returns(solicitud);

        return solicitud;
    }

    public Entities.Cita SetupCita(Guid? asesorUserId = null, Guid? asesoradoUserId = null, DateTime? fechaCreacion = null)
    {
        var cita = new Entities.Cita(
            Guid.NewGuid(),
            Guid.NewGuid().ToString(),
            asesorUserId ?? Guid.NewGuid(),
            asesoradoUserId ?? Guid.NewGuid(),
            fechaCreacion ?? DateTime.UtcNow,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(1));

        CitaRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == cita.Id))
            .Returns(cita);

        return cita;
    }

    public Entities.Contrato SetupContrato(Guid? solicitudId = null, DateTime? fechaInicio = null, DateTime? fechaFinal = null)
    {
        var contrato = new Entities.Contrato(
            Guid.NewGuid(),
            solicitudId ?? Guid.NewGuid(),
            DateOnly.FromDateTime(fechaInicio ?? DateTime.UtcNow),
            DateOnly.FromDateTime(fechaFinal ?? DateTime.UtcNow.AddDays(7))
            );

        ContratoRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == contrato.Id))
            .Returns(contrato);

        ContratoRepository
            .GetBySolicitudIdAsync(Arg.Is<Guid>(y => y == contrato.SolicitudId))
            .Returns(contrato);

        return contrato;
    }
}