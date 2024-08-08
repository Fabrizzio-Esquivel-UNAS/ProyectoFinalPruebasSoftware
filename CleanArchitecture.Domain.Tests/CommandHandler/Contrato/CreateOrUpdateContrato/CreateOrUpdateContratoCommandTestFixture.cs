using System;
using CleanArchitecture.Domain.Commands.Contratos.CreateContrato;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Contrato.CreateContrato;

public sealed class CreateOrUpdateContratoCommandTestFixture : CommandHandlerFixtureBase
{
    public CreateContratoCommandHandler CommandHandler { get; }
    public IContratoRepository ContratoRepository { get; }
    public ISolicitudRepository SolicitudRepository { get; }

    public CreateOrUpdateContratoCommandTestFixture()
    {
        ContratoRepository = Substitute.For<IContratoRepository>();
        SolicitudRepository = Substitute.For<ISolicitudRepository>();

        CommandHandler = new CreateContratoCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            ContratoRepository);
    }

    public Entities.Contrato SetupContrato()
    {
        var contrato = new Entities.Contrato(
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateOnly.FromDateTime(DateTime.Now),
            null);

        ContratoRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == contrato.Id))
            .Returns(contrato);

        return contrato;
    }

    public void SetupExistingContrato(Guid id)
    {
        ContratoRepository
            .ExistsAsync(Arg.Is<Guid>(x => x == id))
            .Returns(true);
    }
}
