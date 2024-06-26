using ancanet.server;
using ancanet.server.Data;
using ancanet.server.Email.FluentEmail;
using ancanet.server.Email.IdentityEmail;
using ancanet.server.Identity.Ucpf;
using ancanet.server.Middlewares;
using ancanet.server.Models;
using ancanet.server.SignalR.Hubs;
using ancanet.server.Swagger;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddConfiguredSwaggerGen();

// Auth Setup
builder.Services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddAuthorizationBuilder();

// Database Setup
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("TestingDb")));

// Identity Setup
builder.Services.AddIdentityCore<AppUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddApiEndpoints();

//Email Setup
builder.Services.AddConfiguredFluentEmail(builder.Configuration, isTest: true);
builder.Services.AddTransientFluentEmailService();
builder.Services.AddIdentityEmailSender();

//Identity Additional Claim Setup
builder.Services.AddAdditionalUserClaimsPrincipalFactory();

//Configure Identity
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    //Signin settings.
    options.SignIn.RequireConfirmedEmail = AncanetConsts.RequireConfirmedEmail;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
});

// Add SignalR
builder.Services.AddSignalR();

//Custom Factory-based Middleware registration
builder.Services.AddProfileSetupRedirection();

//CORS Enable
builder.Services.AddCors(
    options =>
    {
        options.AddDefaultPolicy(policy  =>
        {
            policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
    });

var app = builder.Build();

app
    .MapGroup("Account")
    .MapIdentityApi<AppUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseProfileSetupRedirection();

app.UseAuthorization();

app.MapHub<ChatHub>("/chat");

app.MapControllers();

app.Run();
