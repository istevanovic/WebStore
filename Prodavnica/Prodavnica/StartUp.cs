using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Prodavnica.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(Prodavnica.Startup))] // ovo obavezno ovde
namespace Prodavnica
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // ovo mora ovim redosledom zbog CORS
            //1
            HttpConfiguration config = new HttpConfiguration();
            //2
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureOAuth(app);
           
            //3
            WebApiConfig.Register(config);
            //4
            app.UseWebApi(config);
        }
        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),//.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider(),

                // dodala novo zbog refresh
                RefreshTokenProvider = new RefreshTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}
