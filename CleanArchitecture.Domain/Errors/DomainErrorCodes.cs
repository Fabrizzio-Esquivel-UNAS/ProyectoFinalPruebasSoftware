namespace CleanArchitecture.Domain.Errors;

public static class DomainErrorCodes
{
    public static class User
    {
        // User Validation
        public const string EmptyId = "USER_EMPTY_ID";
        public const string EmptyFirstName = "USER_EMPTY_FIRST_NAME";
        public const string EmptyLastName = "USER_EMPTY_LAST_NAME";
        public const string EmailExceedsMaxLength = "USER_EMAIL_EXCEEDS_MAX_LENGTH";
        public const string FirstNameExceedsMaxLength = "USER_FIRST_NAME_EXCEEDS_MAX_LENGTH";
        public const string LastNameExceedsMaxLength = "USER_LAST_NAME_EXCEEDS_MAX_LENGTH";
        public const string InvalidEmail = "USER_INVALID_EMAIL";
        public const string InvalidTelefono = "USER_INVALID_TELEFONO";
        public const string InvalidCodigo = "USER_INVALID_CODIGO";
        public const string InvalidRole = "USER_INVALID_ROLE";

        // User Password Validation
        public const string EmptyPassword = "USER_PASSWORD_MAY_NOT_BE_EMPTY";
        public const string ShortPassword = "USER_PASSWORD_MAY_NOT_BE_SHORTER_THAN_6_CHARACTERS";
        public const string LongPassword = "USER_PASSWORD_MAY_NOT_BE_LONGER_THAN_50_CHARACTERS";
        public const string UppercaseLetterPassword = "USER_PASSWORD_MUST_CONTAIN_A_UPPERCASE_LETTER";
        public const string LowercaseLetterPassword = "USER_PASSWORD_MUST_CONTAIN_A_LOWERCASE_LETTER";
        public const string NumberPassword = "USER_PASSWORD_MUST_CONTAIN_A_NUMBER";
        public const string SpecialCharPassword = "USER_PASSWORD_MUST_CONTAIN_A_SPECIAL_CHARACTER";

        // General
        public const string AlreadyExists = "USER_ALREADY_EXISTS";
        public const string PasswordIncorrect = "USER_PASSWORD_INCORRECT";
    }

    public static class Tenant
    {
        // Tenant Validation
        public const string EmptyId = "TENANT_EMPTY_ID";
        public const string EmptyName = "TENANT_EMPTY_NAME";
        public const string NameExceedsMaxLength = "TENANT_NAME_EXCEEDS_MAX_LENGTH";

        // General
        public const string AlreadyExists = "TENANT_ALREADY_EXISTS";
    }

    public static class Calendario
    {
        // Calendario Validation
        public const string EmptyId = "CALENDARIO_EMPTY_ID";
        public const string EmptyName = "CALENDARIO_EMPTY_NAME";
        public const string NameExceedsMaxLength = "CALENDARIO_NAME_EXCEEDS_MAX_LENGTH";

        // General
        public const string AlreadyExists = "CALENDARIO_ALREADY_EXISTS";
    }

    public static class Contrato
    {
        // Contrato Validation
        public const string EmptyId = "PERIODO_EMPTY_ID";
        public const string EmptyFechaInicio = "PERIODO_EMPTY_FECHA_INICIO";
        public const string EmptyFechaFinal = "PERIODO_EMPTY_FECHA_FINAL";
        public const string EmptyNombre = "PERIODO_EMPTY_NOMBRE";
        public const string InvalidFechaInicio = "PERIODO_INVALID_FECHA_INICIO";
        public const string InvalidFechaFinal = "PERIODO_INVALID_FECHA_FINAL";
        public const string NombreExceedsMaxLength = "PERIODO_NOMBRE_EXCEEDS_MAX_LENGTH";

        // General
        public const string AlreadyExists = "PERIODO_ALREADY_EXISTS";
    }

    public static class Facultad
    {
        // Facultad Validation
        public const string EmptyId = "FACULTAD_EMPTY_ID";
        public const string EmptyNombre = "FACULTAD_EMPTY_NOMBRE";
        public const string NombreExceedsMaxLength = "FACULTAD_NOMBRE_EXCEEDS_MAX_LENGTH";

        // General
        public const string AlreadyExists = "FACULTAD_ALREADY_EXISTS";
    }

    public static class Escuela
    {
        // Escuela Validation
        public const string EmptyId = "ESCUELA_EMPTY_ID";
        public const string EmptyNombre = "ESCUELA_EMPTY_NOMBRE";
        public const string NombreExceedsMaxLength = "ESCUELA_NOMBRE_EXCEEDS_MAX_LENGTH";

        // General
        public const string AlreadyExists = "ESCUELA_ALREADY_EXISTS";
    }

    public static class GrupoInvestigacion
    {
        // Grupoinvestigacion Validation
        public const string EmptyId = "GRUPOINVESTIGACION_EMPTY_ID";
        public const string EmptyNombre = "GRUPOINVESTIGACION_EMPTY_NAME";
        public const string NombreExceedsMaxLength = "GRUPOINVESTIGACION_NAME_EXCEEDS_MAX_LENGTH";

        // General
        public const string AlreadyExists = "GRUPOINVESTIGACION_ALREADY_EXISTS";
    }

    public static class HistorialCoordinador
    {
        // HistorialCoordinador Validation
        public const string EmptyId = "HISTORIALCOORDINADOR_EMPTY_ID";
        public const string EmptyUserId = "HISTORIALCOORDINADOR_EMPTY_USERID";
        public const string InvalidUserId = "HISTORIALCOORDINADOR_INVALID_USERID";
        public const string EmptyGrupoInvestigacionId = "HISTORIALCOORDINADOR_EMPTY_GRUPOINVESTIGACIONID";
        public const string InvalidGrupoInvestigacionId = "HISTORIALCOORDINADOR_INVALID_GRUPOINVESTIGACIONID";
        public const string EmptyFechaInicio = "HISTORIALCOORDINADOR_EMPTY_FECHAINICIO";
        public const string InvalidFechaInicio = "HISTORIALCOORDINADOR_INVALID_FECHAINICIO";
        public const string EmptyFechaFin = "HISTORIALCOORDINADOR_EMPTY_FECHAFIN";
        public const string InvalidFechaFin = "HISTORIALCOORDINADOR_INVALID_FECHAFIN";

        // General
        public const string AlreadyExists = "TENANT_ALREADY_EXISTS";
    }

    public static class Solicitud
    {
        // Solicitud Validation
        public const string EmptyId = "SOLICITUD_EMPTY_ID";
        public const string EmptyNumeroTesis = "SOLICITUD_EMPTY_NUMEROTESIS";
        public const string InvalidNumeroTesis = "SOLICITUD_INVALID_NUMEROTESIS";
        public const string NumeroTesisExceedsMaxLength = "SOLICITUD_NUMEROTESIS_EXCEEDS_MAX_LENGTH";
        public const string EmptyAfinidad = "SOLICITUD_EMPTY_AFINIDAD";
        public const string AfinidadExceedsMaxLength = "SOLICITUD_AFINIDAD_EXCEEDS_MAX_LENGTH";

        // General
        public const string AlreadyExists = "SOLICITUD_ALREADY_EXISTS";

        // Additional error codes from the validation tests
        public const string EmptyAsesoradoUserId = "SOLICITUD_EMPTY_ASESORADO_USER_ID";
        public const string EmptyAsesorUserId = "SOLICITUD_EMPTY_ASESOR_USER_ID";
        public const string EmptyMensaje = "SOLICITUD_EMPTY_MENSAJE";
        public const string NullMensaje = "SOLICITUD_NULL_MENSAJE";
    }


    public static class LineaInvestigacion
    {
        // LineaInvestigacion Validation
        public const string EmptyId = "LINEAINVESTIGACION_EMPTY_ID";
        public const string EmptyNombre = "LINEAINVESTIGACION_EMPTY_NOMBRE";
        public const string NombreExceedsMaxLength = "LINEAINVESTIGACION_NOMBRE_EXCEEDS_MAX_LENGTH";

        // General
        public const string AlreadyExists = "LINEAINVESTIGACION_ALREADY_EXISTS";
    }

    public static class Cita
    {
        // Cita Validation
        public const string EmptyId = "CITA_EMPTY_ID";
        public const string EmptyName = "CITA_EMPTY_NAME";
        public const string NameExceedsMaxLength = "CITA_NAME_EXCEEDS_MAX_LENGTH";
        public const string UserMissingAsesorRole = "CITA_USER_MISSING_ASESOR_ROLE";
        public const string UserMissingAsesoradoRole = "CITA_USER_MISSING_ASESORADO_ROLE";

        // General
        public const string AlreadyExists = "CITA_ALREADY_EXISTS";
    }
}