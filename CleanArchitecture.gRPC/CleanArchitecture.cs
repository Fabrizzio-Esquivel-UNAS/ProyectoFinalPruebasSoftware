using CleanArchitecture.Domain.Entities;
using CleanArchitecture.gRPC.Interfaces;

namespace CleanArchitecture.gRPC;

public sealed class CleanArchitecture : ICleanArchitecture
{
    public CleanArchitecture(
        IUsersContext users,
        ITenantsContext tenants,
        IContratosContext contratos,
        IFacultadesContext facultades,
        IEscuelasContext escuelas,
        IGruposInvestigacionContext gruposInvestigacion,
        IHistorialCoordinadoresContext historialCoordinadores,
        ISolicitudesContext solicitudes,
        ILineasInvestigacionContext lineasInvestigacion)
    {
        Users = users;
        Tenants = tenants;
        Contratos = contratos;
        Facultades = facultades;
        Escuelas = escuelas;
        GruposInvestigacion = gruposInvestigacion;
        HistorialCoordinadores = historialCoordinadores;
        Solicitudes = solicitudes;
        LineasInvestigacion = lineasInvestigacion;
    }

    public IUsersContext Users { get; }

    public ITenantsContext Tenants { get; }

    public IFacultadesContext Facultades { get; } 

    public IEscuelasContext Escuelas { get; }

    public IContratosContext Contratos { get; }

    public IFacultadesContext? Facultad { get; }

    public IEscuelasContext? Escuela { get; }

    public IGruposInvestigacionContext GruposInvestigacion { get; }

    public IHistorialCoordinadoresContext HistorialCoordinadores { get; }

    public ISolicitudesContext Solicitudes { get; }

    public ILineasInvestigacionContext LineasInvestigacion { get; }

}