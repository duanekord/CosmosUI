using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Claims;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using cosmosui.Utils;
using Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Owin.Security.ActiveDirectory;

namespace cosmosui
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string resource = ConfigurationManager.AppSettings["ida:resource"];
        private static string appKey = ConfigurationManager.AppSettings["ida:ClientSecret"];
        private static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static string tenantId = ConfigurationManager.AppSettings["ida:TenantId"];
        private static string redirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];
        private static string authority = aadInstance + tenantId;
        string graphResourceId = "https://graph.windows.net";
        public static string token;

        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseWindowsAzureActiveDirectoryBearerAuthentication(
                new WindowsAzureActiveDirectoryBearerAuthenticationOptions
                {
                    Tenant = tenantId,
                    AuthenticationType = "OAuth2Bearer",
                    TokenValidationParameters = new TokenValidationParameters() { ValidAudience = "https://graph.microsoft.com" }
                });


            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = clientId,
                    Authority = authority,

                    Notifications = new OpenIdConnectAuthenticationNotifications
                    {
                        // If there is a code in the OpenID Connect response, redeem it for an access token and refresh token, and store those away.
                        AuthorizationCodeReceived = (context) =>
                        {
                            var code = context.Code;
                            var credential = new ClientCredential(clientId, appKey);
                            var signedInUserId =
                                context.AuthenticationTicket.Identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                            var authContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(authority, new NaiveSessionCache(signedInUserId));
                            var result = authContext.AcquireTokenByAuthorizationCode(
                                code, new Uri(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path)), credential,
                                resource);
                            token = result.AccessToken;

                            return Task.FromResult(0);
                        },
                        RedirectToIdentityProvider = (context) =>
                        {
                            // This ensures that the address used for sign in and sign out is picked up dynamically from the request 
                            // this allows you to deploy your app (to Azure Web Sites, for example)without having to change settings 
                            // Remember that the base URL of the address used here must be provisioned in Azure AD beforehand. 
                            var appBaseUrl = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "/";
                            context.ProtocolMessage.RedirectUri = appBaseUrl;
                            context.ProtocolMessage.PostLogoutRedirectUri = appBaseUrl;
                            return Task.FromResult(0);
                        },
                        AuthenticationFailed = (context) =>
                        {
                            // Suppress the exception if you don't want to see the error 
                            context.HandleResponse();
                            return Task.FromResult(0);
                        }
                    }

                });

        }
    }
}
