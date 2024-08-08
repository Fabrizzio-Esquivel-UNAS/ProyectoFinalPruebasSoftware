namespace CleanArchitecture.Domain.Constants;

public static class MaxLengths
{
    public static class User
    {
        public const int Email = 320;
        public const int FirstName = 100;
        public const int LastName = 100;
        public const int Password = 128;
        public const int Telefono = 9;
        public const int Codigo = 10;
    }

    public static class Tenant
    {
        public const int Name = 255;
    }

    public static class Contrato
    {
        public const int Nombre = 255;
    }

    public static class Facultad
    {
        public const int Nombre = 255;
    }

    public static class Escuela
    {
        public const int Nombre = 255;
    }

    public static class GrupoInvestigacion
    {
        public const int Nombre = 100;
    }

    public static class Solicitud
    {
        public const int NumeroTesis = 10;
        public const int Afinidad = 400;
    }

    public static class LineaInvestigacion
    {
        public const int Nombre = 100;
    }
}