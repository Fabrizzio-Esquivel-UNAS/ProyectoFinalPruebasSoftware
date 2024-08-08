using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels.Facultades;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using MediatR;

namespace CleanArchitecture.Application.Queries.Facultades.GetFacultadesById;

public sealed class GetFacultadesByIdQueryHandler :
    IRequestHandler<GetFacultadesByIdQuery, FacultadViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IFacultadRepository _facultadRepository;

    public GetFacultadesByIdQueryHandler(IFacultadRepository facultadRepository, IMediatorHandler bus)
    {
        _facultadRepository = facultadRepository;
        _bus = bus;
    }

    public async Task<FacultadViewModel?> Handle(GetFacultadesByIdQuery request, CancellationToken cancellationToken)
    {
        var facultad = await _facultadRepository.GetByIdAsync(request.FacultadesId);

        if (facultad is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetFacultadesByIdQuery),
                    $"Facultad with id {request.FacultadesId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return FacultadViewModel.FromFacultades(facultad);
    }
}