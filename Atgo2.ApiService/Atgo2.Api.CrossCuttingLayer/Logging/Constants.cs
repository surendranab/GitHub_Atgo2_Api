namespace Atgo2.Api.CrossCuttingLayer.Logging
{
    public static class Constants
    {
        public static string MethodInvokedMessage = "Method invoked.";
        public static string MethodExecutedMessage = "Execution completed in {0} ms.";
        public static string ExceptionMessage = "Exception occurred";

        public static string UserLoginModule = "UserLogin";
        public static string UserModule = "User";
        public static string RoleModule = "Role";
        public static string UserRoleModule = "UserRole";
        public static string GroupModule = "Group";
        public static string CommonModule = "Common";
        public static string LookUpModule = "LookUp";
        public static string ReportModule = "Report";
        public static string UserAccessModule = "UserAccess";
        public static string MembershipModule = "Membership";

        public static string AuthModule = "Auth";
        public static string AccessModule = "Access";        
        public static string TokenModule = "Token";
        public static string ThemesModule = "Themes";
        public static string AppMiddleWareNonce = "AppMiddleWireNonce";
        public static string AppMiddleWareTokenDecryption = "AppMiddleWireTokenDecryption";
        public static string EmailModule = "EmailModule";
        public static string TimeZoneInfo = "TimeZoneInfo";
        public static string TokenValidation = "TokenValidation";
        public static string CommonExceptionFilterModule = "CommonExceptionFilter";
        public static string AutomationException = "AutomationException";        
    }
}