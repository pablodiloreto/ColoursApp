using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServerAspNetIdentity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("api1", "MyAPI")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // machine to machine client
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // scopes that client has access to
                AllowedScopes = { "api1" }
            },

            // interactive ASP.NET Core Web App
            new Client
            {
                ClientId = "web",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                // where to redirect to after login
                RedirectUris = { "https://localhost:44382/signin-oidc", "https://coloursweb.app.aprender.it/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:44382/signout-callback-oidc", "https://coloursweb.app.aprender.it/signout-callback-oidc" },


                AllowOfflineAccess = true,
                
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api1",
                }
            },
            new Client
            {
                ClientId = "apim",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                // where to redirect to after login
                RedirectUris = { "https://am101a-pdiloreto-apim.developer.azure-api.net/signin-oauth/code/callback/ids", "https://am101a-pdiloreto-apim.developer.azure-api.net/signin-oauth/implicit/callback" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:44382/signout-callback-oidc", "https://coloursweb.app.aprender.it/signout-callback-oidc" },


                AllowOfflineAccess = true,

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api1",
                }
            },

        };
}
