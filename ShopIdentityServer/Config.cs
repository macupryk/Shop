using IdentityModel;
using IdentityServer4;
//using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
 
namespace ShopIdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResources.Phone(),
                new IdentityResource {
                Name = "role",
                UserClaims = new List<string> {"role"}
            }
        };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
    
                new ApiResource("api1", "My API",
                new List<string>() { JwtClaimTypes.Name, JwtClaimTypes.Email })
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
        {
            new Client
            {
                ClientId = "client",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = { "api1", IdentityServerConstants.StandardScopes.Profile, IdentityServerConstants.StandardScopes.Email  }
            },
            // resource owner password grant client
            new Client
            {
                ClientId = "ro.client",
                AllowedGrantTypes =  GrantTypes.ResourceOwnerPassword   ,

                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "api1", IdentityServerConstants.StandardScopes.Profile, IdentityServerConstants.StandardScopes.Email }
            },
            // OpenID Connect hybrid flow client (MVC)
            new Client
            {
                ClientId = "mvc",
                ClientName = "MVC Client",
                AllowedGrantTypes = GrantTypes.Hybrid,

                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                RedirectUris           = { "http://localhost:5002/signin-oidc" },
                PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "api1"
                },

                AllowOfflineAccess = true
            },
            // JavaScript Client
            new Client
            {
                ClientId = "js",
                ClientName = "JavaScript Client",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,

                RedirectUris =           { "http://localhost:5003/callback.html" },
                PostLogoutRedirectUris = { "http://localhost:5003/index.html" },
                AllowedCorsOrigins =     { "http://localhost:5003" },

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "api1"
                }
            }
        };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
    {
        new TestUser
        {
            SubjectId = "1",
            Username = "alice",
            Password = "password",
             Claims = new List<Claim>()   {
        new Claim( ClaimTypes.Name, "alice"),
        new Claim( ClaimTypes.Email, "alice@domain.com")
    }
        },
        new TestUser
        {
            SubjectId = "2",
            Username = "bob",
            Password = "password",
             Claims = new List<Claim>()
    {
        new Claim( ClaimTypes.Name, "bob"),
        new Claim(ClaimTypes.Email, "bob@domain.com")
    }
        }
    };
        }
    }
}
