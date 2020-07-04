using System.Collections.Generic;

namespace Haplo.Authorization.Auth0.Settings
{
    public class Auth0Options
    {
        public string Authority { get; set; }
        public string Audience { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}