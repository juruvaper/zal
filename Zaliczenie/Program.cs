using MailKit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Reflection.Emit;
using System.Text;
using Zaliczenie.Models;

var builder = WebApplication.CreateBuilder(args);

// JWT settings
var jwtSettings = new JwtSettings
{
    SecretKey = "SuperSecretKey123456!", // Minimum 16 znaków!
    Issuer = "example-api",
    Audience = "example-client",
    ExpiryMinutes = 60
};

builder.Services.AddSingleton(jwtSettings);

// JWT Authentication
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Wpisz token JWT w formacie: Bearer {token}"
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        { securityScheme, new string[] { } }
    };

    c.AddSecurityRequirement(securityRequirement);
});

// Usuwamy rejestrację MailService i ExternalApiService

builder.Services.AddSingleton<BooksService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 🔐 Kolejność ma znaczenie:
app.UseAuthentication();    // ⬅️ najpierw Authentication
app.UseAuthorization();     // ⬅️ potem Authorization

app.MapControllers();

app.Run();

