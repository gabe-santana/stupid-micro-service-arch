using Microsoft.AspNetCore.Authentication.Cookies;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("ocelot.json")
                            .Build();

builder.Services.AddOcelot(configuration);

builder.Services.AddAuthentication(options =>
{   
    // Store the session to cookies
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    // OpenId authentication
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "http://localhost:8080/auth/realms/Production/protocol/openid-connect/auth";
        options.Audience = "api1";
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            NameClaimType = "name",
            RoleClaimType = "role"
        };
    })
    .AddCookie("Cookies")
    .AddOpenIdConnect(options =>
    {
        // URL of the Keycloak server
        options.Authority = "http://localhost:8080/auth/realms/Production/protocol/openid-connect/auth";
        // Client configured in the Keycloak
        options.ClientId = "myClient";

        // For testing we disable https (should be true for production)
        options.RequireHttpsMetadata = false;
        options.SaveTokens = true;
        options.ClientId = "myClient";
        // Client secret shared with Keycloak
        options.ClientSecret = "8efae46d-e1f2-43bc-a810-d0ef6d961008";
        options.GetClaimsFromUserInfoEndpoint = true;

        // OpenID flow to use
        options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
    });

var app = builder.Build();

app.UseOcelot().Wait();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
