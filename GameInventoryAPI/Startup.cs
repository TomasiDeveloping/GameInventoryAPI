using GameInventoryAPI.OAuth;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(GameInventoryAPI.Startup))]

namespace GameInventoryAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Formatters.Add(new XmlMediaTypeFormatter());
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            ConfigureAuth(app);
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/Login"),
                Provider = new OAuthWebProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1)
            });
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}