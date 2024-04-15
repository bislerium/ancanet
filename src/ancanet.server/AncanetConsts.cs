using System.Security.Claims;

namespace ancanet.server;

public static class AncanetConsts
{
    public static class  Claims
    {
        public static class IsProfileConfigured
        {
            public const string Type = "ipc";
            public const string ValueType = ClaimValueTypes.Boolean;
            
            public const string True = "true";
            public const string False = "false";
        }
    }
}