namespace CleanArchitecture.Domain.Errors;

public static class ErrorCodes
{
    public const string CommitFailed = "COMMIT_FAILED";
    public const string ObjectNotFound = "OBJECT_NOT_FOUND";
    public const string InsufficientPermissions = "UNAUTHORIZED";
    public const string UnexpectedError = "UNEXPECTED_ERROR";
    public const string InvalidOperation = "INVALID_OPERATION";
    public const string HttpRequestFailed = "HTTP_REQUEST_FAILED";
}