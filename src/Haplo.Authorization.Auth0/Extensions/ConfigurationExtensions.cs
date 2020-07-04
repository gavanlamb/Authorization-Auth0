using Haplo.Authorization.Auth0.Settings;
using Microsoft.Extensions.Configuration;

namespace Haplo.Authorization.Auth0.Extensions
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Get the configuration for auth0
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns>Configuration for auth0</returns>
        public static Auth0Options GetAuth0Configuration(
            this IConfiguration configuration)
            => configuration
                .GetSection("Haplo.Authorization.Auth0")
                .Get<Auth0Options>();
    }
}