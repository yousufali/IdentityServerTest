﻿using System.Collections.Generic;
using IdentityServer4.Models;

namespace IdentityServer4Core
{
    public class Config
    {
        public static IEnumerable<Scope> GetScopes()
        {
            return new List<Scope>
            {
                new Scope
                {
                    Name = "api1",
                    Description = "My API"
                }
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
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    // secret for authentication
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = new List<string>
                    {
                        "api1"
                    }
                }
            };
        }
    }
}
