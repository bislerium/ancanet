using System.Reflection;
using System.Security.Claims;

namespace ancanet.server;

public static class AncanetConsts
{
    public static readonly Assembly Assembly = typeof(Program).Assembly;

    public const string ProfileSetupURL = "/account/setup";
    public static readonly string[] ExclusionsFromProfileSetup = [ProfileSetupURL];
    
    public const string ResendConfirmationEmail = "/Account/resendConfirmationEmail";
    //public static readonly string[] ExclusionsFromEmailConfirmation = [ResendConfirmationEmail];

    public static class  Claims
    {
        public static class IsProfileSetup
        {
            public const string Type = "ipc";
            public const string ValueType = ClaimValueTypes.Boolean;
            
            public const string True = nameof(True);
            public const string False = nameof(False);
        }

        public static class IsEmailConfirmed
        {
            public const string Type = "iec";
            public const string ValueType = ClaimValueTypes.Boolean;

            public const string True = nameof(True);
            public const string False = nameof(False);
        }
    }

    public const bool RequireConfirmedEmail = true;

    public static class Headers
    {
        public const string StopRedirection = "X-Stop-Redirection";
    }
}