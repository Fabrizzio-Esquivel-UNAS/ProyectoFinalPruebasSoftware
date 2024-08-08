using System;

namespace CleanArchitecture.Domain.Constants;

public static class Ids
{
    public static class Seed
    {
        public static readonly Guid AdminUserId = new("00000000-0000-0000-0000-000000000010");
        public static readonly Guid CoordinadorUserId = new("00000000-0000-0000-0000-000000000001");
        public static readonly Guid AsesorUserId = new("00000000-0000-0000-0000-000000000002");
        public static readonly Guid AsesoradoUserId = new("00000000-0000-0000-0000-000000000003");
        public static readonly Guid UserId = new("00000000-0000-0000-0000-000000000004");

        public static readonly Guid GrupoInvestigacionId = new("00000000-0000-0000-0000-000000000001");
        public static readonly Guid LineaInvestigacionId = new("00000000-0000-0000-0000-000000000001");
        public static readonly Guid HistorialCoordinadorId = new("00000000-0000-0000-0000-000000000001");

        public static readonly Guid ContratoId = new("00000000-0000-0000-0000-000000000001");
        public static readonly Guid SolicitudId = new("00000000-0000-0000-0000-000000000001");

        public static readonly Guid TenantId = new("00000000-0000-0000-0000-000000000001");
        public static readonly Guid EscuelaId = new("00000000-0000-0000-0000-000000000001");
        public static readonly Guid FacultadId = new("00000000-0000-0000-0000-000000000001");
    }
}