using System;
using System.Linq;
using Haplo.Authorization.Auth0.Handlers;
using Haplo.Authorization.Auth0.Models;
using Haplo.Authorization.Auth0.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Haplo.Authorization.Auth0.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuth0(
            this IServiceCollection services, 
            Auth0Options auth0Options)
        {
            if (string.IsNullOrWhiteSpace(auth0Options.Audience))
                throw new ArgumentException("Audience is null or empty");
            
            if (string.IsNullOrWhiteSpace(auth0Options.Authority))
                throw new ArgumentException("Authority is null or empty");

            if (auth0Options.Permissions == null || auth0Options.Permissions.Any(string.IsNullOrWhiteSpace))
                throw new ArgumentException("Permissions is null or empty or contains an empty element");
            

            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
            
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = auth0Options.Authority;
                    options.Audience = auth0Options.Audience;
                });
            
            services
                .AddAuthorization(options =>
                {
                    foreach (var permission in auth0Options.Permissions)
                    {
                        options
                            .AddPolicy(
                                permission, 
                                policy => 
                                    policy.Requirements.Add(new HasScopeRequirement(permission, auth0Options.Authority)));
                    }
                });

            return services;
        }
    }
}