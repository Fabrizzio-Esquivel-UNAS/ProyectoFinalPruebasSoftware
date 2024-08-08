using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.LineasInvestigacion.GetLineaInvestigacionById;
using CleanArchitecture.Application.ViewModels.LineasInvestigacion;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using MediatR;

namespace CleanArchitecture.Application.Queries.LineasInvestigacion.GetLineaInvestigacionById;

public sealed class GetLineaInvestigacionByIdQueryHandler :
    IRequestHandler<GetLineaInvestigacionByIdQuery, LineaInvestigacionViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly ILineaInvestigacionRepository _lineainvestigacionRepository;

    public GetLineaInvestigacionByIdQueryHandler(ILineaInvestigacionRepository lineainvestigacionRepository, IMediatorHandler bus)
    {
        _lineainvestigacionRepository = lineainvestigacionRepository;
        _bus = bus;
    }

    public async Task<LineaInvestigacionViewModel?> Handle(GetLineaInvestigacionByIdQuery request, CancellationToken cancellationToken)
    {
        var lineainvestigacion = await _lineainvestigacionRepository.GetByIdAsync(request.LineaInvestigacionId);

        if (lineainvestigacion is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetLineaInvestigacionByIdQuery),
                    $"LineaInvestigacion with id {request.LineaInvestigacionId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return LineaInvestigacionViewModel.FromLineaInvestigacion(lineainvestigacion);
    }
}