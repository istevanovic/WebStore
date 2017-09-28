using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Prodavnica.Models;
using Prodavnica.Models.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Prodavnica.Providers
{
    internal class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
         public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
                {
                    context.Validated();
                }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            korisnik user;
            using (var db = new prodavnicaEntities())
            {
                // Query for the Blog named ADO.NET Blog 
                user = db.korisnik.Where<korisnik>(k => k.username == context.UserName && k.password== context.Password)
                                .FirstOrDefault();
               

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
                else
                {
                    var old = db.korisnik.Find(user.idKorisnik);
                    korisnik korisnik = old;
                    korisnik.datumPoslednjegLogovanja = DateTime.Now;
                    db.Entry(old).CurrentValues.SetValues(korisnik);
                }
                
            }
           

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("username", user.username));
            identity.AddClaim(new Claim("role", user.role.Trim()));
            identity.AddClaim(new Claim("id", user.idKorisnik.ToString().Trim()));
            identity.AddClaim(new Claim(ClaimTypes.Role, user.role.Trim()));
            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "role", user.role.Trim()
                    },
                    {
                        "userName", context.UserName
                    },
                    {
                        "id", user.idKorisnik.ToString()
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
            //context.Validated(identity);

        }

      /*  public static AuthenticationProperties CreateProperties(string userName, string Roles,string id)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
        {
            { "username", userName },
            {"role",Roles},
            {"id",id}


        };
            return new AuthenticationProperties(data);
        }*/

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        // dodali novo zbog refresh
        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "role", context.Ticket.Identity.Claims.First(t => t.Type == "role").Value.ToString()
                    },
                    {
                        "userName", context.Ticket.Identity.Claims.First(t => t.Type == "username").Value.ToString()
                    },
                     {
                        "id", context.Ticket.Identity.Claims.First(t => t.Type == "id").Value.ToString()
                    }
                });
            var newTicket = new AuthenticationTicket(newIdentity, props);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

    }
}
