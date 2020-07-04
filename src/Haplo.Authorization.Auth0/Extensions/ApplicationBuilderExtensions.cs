using Microsoft.AspNetCore.Builder;

namespace Haplo.Authorization.Auth0.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseAuth0(this IApplicationBuilder app)
        {
            app.UseAuthentication();//TODO check before publishing if this is actually needed
            app.UseAuthorization();
        }
    }
}