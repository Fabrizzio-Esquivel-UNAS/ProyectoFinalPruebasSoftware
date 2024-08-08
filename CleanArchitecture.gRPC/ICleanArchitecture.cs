using CleanArchitecture.gRPC.Interfaces;

namespace CleanArchitecture.gRPC;

public interface ICleanArchitecture
{
    IUsersContext Users { get; }
    ITenantsContext Tenants { get; }
    IContratosContext Contratos { get; }
    IFacultadesContext Facultades { get; }
    IEscuelasContext Escuelas { get; }
    IGruposInvestigacionContext GruposInvestigacion { get; }
    IHistorialCoordinadoresContext HistorialCoordinadores { get; }
    ISolicitudesContext Solicitudes { get; }
    ILineasInvestigacionContext LineasInvestigacion { get; }
}