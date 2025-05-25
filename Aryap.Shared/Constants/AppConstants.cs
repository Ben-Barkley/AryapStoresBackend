namespace Aryap.Data.Shared.Constants
{
    // Application-specific constants
    public static class ApplicationConstants
    {
        public const string ApplicationName = "Clothing Store API";
        public const string ApplicationCode = "CLOTHING_STORE_API";
    }

    // General-purpose constants
    public static class GeneralConstants
    {
        public const string APPLICATION_ID = "APP_001";
        public const string APPLICATION_CODE = "CLOTHING_APP_CODE";
        public const string CORSAllowedOrigins = "http://localhost:3000,http://localhost:4200"; // Example origins
        public const string OrganizationDomain = "clothingstore.com";

        
    }

    // HTTP-related constants
    public static class HTMLConstants
    {
        public const string APPLICATION_CONTENT_TYPE_JSON = "application/json";
        public const string APPLICATION_CONTENT_TYPE_TEXT_XML = "text/xml";
    }

    // AppSettings-related constants
    public static class AppSettingsConstants
    {
        public const string ConnectionStringsSectionKey = "ConnectionStrings";
        public const string DefaultConnectionStringKey = "ConnectionStrings:DefaultConnection";

        public const string AppSettingsKey = "AppSettings";
        public const string AllowedCORSOriginsKey = "AppSettings:AllowedCORSOrigins";
    }

    // Date format constants
    public static class DateFormatConstants
    {
        public const string FULL = "yyyy-MM-dd hh:mm:ss tt";
        public const string Compact = "yyyyMMddHHmmss";
    }

    // Active/Inactive status constants
    public static class ActiveStatuses
    {
        public static readonly string ACTIVE = "ACTIVE";
        public static readonly string INACTIVE = "INACTIVE";
        public static readonly string ALL = "ALL";
    }

    // Integration service constants
    public static class IntegrationServiceConstants
    {
        public const string CLIENT_ID = "ClientID";
        public const string CLIENT_KEY = "ClientKey";
        public const string CLIENT_SECRET = "ClientSecret";
    }

    // Authentication type constants
    public static class AuthenticationTypes
    {
        public const string ACTIVE_DIRECTORY = "ACTIVE_DIRECTORY";
        public const string ASPNET_IDENTITY = "ASPNET_IDENTITY";
    }

}