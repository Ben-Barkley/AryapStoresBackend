namespace Aryap.Shared.Constants

{
    // General error messages
    public static class GeneralErrors
    {
        public const string EntityNotFound = "The requested entity was not found.";
        public const string InvalidOperation = "An invalid operation was attempted.";
        public const string UnauthorizedAccess = "You do not have permission to perform this action.";
        public const string InternalServerError = "An unexpected error occurred. Please try again later.";
    }

    // Validation-related error messages
    public static class ValidationErrors
    {
        public const string InvalidInput = "The provided input is invalid.";
        public const string MissingRequiredField = "A required field is missing.";
        public const string InvalidDateFormat = "The provided date format is invalid.";
        public const string InvalidEmailFormat = "The provided email format is invalid.";
       
    }

    // Database-related error messages
    public static class DatabaseErrors
    {
        public const string DatabaseConnectionFailed = "Failed to connect to the database.";
        public const string RecordAlreadyExists = "A record with the same details already exists.";
        public const string RecordUpdateFailed = "Failed to update the record.";
        public const string RecordDeleteFailed = "Failed to delete the record.";
    }

    // Authentication-related error messages
    public static class AuthenticationErrors
    {
        public const string InvalidCredentials = "Invalid username or password.";
        public const string TokenExpired = "The provided token has expired.";
        public const string TokenInvalid = "The provided token is invalid.";
        public const string UserNotAuthenticated = "User is not authenticated.";
        public const string UserNotAuthorized = "User is not authorized to perform this action.";
    }

    // API-related error messages
    public static class ApiErrors
    {
        public const string ExternalApiError = "An error occurred while calling the external API.";
        public const string ApiRateLimitExceeded = "The API rate limit has been exceeded.";
        public const string InvalidApiResponse = "The API returned an invalid response.";
        public const string ApiTimeout = "The API request timed out.";
    }

}