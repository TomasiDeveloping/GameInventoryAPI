using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GameInventoryAPI.OAuth
{
    public class OAuthWebProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // Abfrage an die Datenbank nach Benutzer und Password  

            //var user = Database.DatabaseHandler.GetUserByLoginNameAndPassword(context.UserName, context.Password);  
            //if (user == null)  
            //{  
            //    context.SetError("invalid_grant", "The user name or password is incorrect.");  
            //    return;  
            //}  

            ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            AuthenticationProperties props = new AuthenticationProperties(new Dictionary<string, string>
{
{  
                    //"id", user.UserId.ToString()  
                    "id", "1" // Nur für Testzwecke ohne Originale UserId  
                }
});
            AuthenticationTicket ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}