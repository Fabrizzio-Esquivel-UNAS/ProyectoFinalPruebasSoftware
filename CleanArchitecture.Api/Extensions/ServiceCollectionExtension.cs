using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Azure.Core;
using CleanArchitecture.Api.Swagger;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.ViewModels.Calendarios;
using CleanArchitecture.Domain.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Polly;
using System.Net.NetworkInformation;

namespace CleanArchitecture.Api.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "CleanArchitecture",
                Version = "v1",
                Description = "A clean architecture API"
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. " +
                              "Use the /api/v1/user/login endpoint to generate a token",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });

            c.ParameterFilter<SortableFieldsAttributeFilter>();

            c.SupportNonNullableReferenceTypes();

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });
        return services;
    }

    //public static void AddCalendly(OAuthOptions options, IConfiguration configuration)
    //{
    //    options.ClientId = configuration["Calendly:ClientId"]!;
    //    options.ClientSecret = configuration["Calendly:ClientSecret"]!;
    //    options.CallbackPath = new PathString(configuration["Calendly:CallbackEndpoint"]!);

    //    options.AuthorizationEndpoint = configuration["Calendly:AuthorizationEndpoint"]!;
    //    options.TokenEndpoint = configuration["Calendly:TokenEndpoint"]!;
    //    options.SaveTokens = true;

    //    options.Events = new OAuthEvents
    //    {
    //        OnCreatingTicket = async context =>
    //        {
    //            if (context.AccessToken is null || context.RefreshToken is null)
    //                throw new ArgumentException("OAuth tokens cannot be null.");

    //            // Save tokens to the database or perform other actions
    //            var calendarioService = context.HttpContext.RequestServices.GetRequiredService<ICalendarioService>();
    //            await calendarioService.CreateCalendarioAsync(new CreateCalendarioViewModel(
    //                context.AccessToken,
    //                context.RefreshToken,
    //                DateTime.UtcNow.AddSeconds(int.Parse(context.TokenResponse.ExpiresIn ?? "0")),
    //                DateTime.UtcNow));
    //        }
    //    };
    //}

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        // Configure JWT bearer authentication
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(jwtOptions =>
        {
            jwtOptions.TokenValidationParameters = CreateTokenValidationParameters(configuration);
        });

        // Configure options for TokenSettings and CalendlySettings
        services.AddOptions<TokenSettings>()
            .Bind(configuration.GetSection("Auth"))
            .ValidateOnStart();

        services.AddOptions<CalendlySettings>()
            .Bind(configuration.GetSection("Calendly"))
            .ValidateOnStart();

        // Configure an HttpClient with Polly for transient fault handling
        services.AddHttpClient("CalendlyClient")
            .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2)));

        // Configure authentication with OAuth and cookies for Calendly
        //services.AddAuthentication()
        //    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme) // Add cookie authentication
        //    .AddOAuth("Calendly", options =>
        //    {
        //        AddCalendly(options, configuration);
        //        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Ensure OAuth uses cookies to sign in
        //    });

        return services;
    }

    public static TokenValidationParameters CreateTokenValidationParameters(IConfiguration configuration)
    {
        var result = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Auth:Issuer"],
            ValidAudience = configuration["Auth:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    configuration["Auth:Secret"]!)),
            RequireSignedTokens = false
        };

        return result;
    }
}