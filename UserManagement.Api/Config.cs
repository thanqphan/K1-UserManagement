using Duende.IdentityServer.Models;

namespace UserManagement.Api
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("roles", new[] { "role" })
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
    {
        // invoice API specific scopes
        new ApiScope(name: "invoice.read",   displayName: "Reads your invoices."),
        new ApiScope(name: "invoice.pay",    displayName: "Pays your invoices."),

        // customer API specific scopes
        new ApiScope(name: "customer.read",    displayName: "Reads you customers information."),
        new ApiScope(name: "customer.contact", displayName: "Allows contacting one of your customers."),

        // shared scopes
        new ApiScope(name: "manage",    displayName: "Provides administrative access."),
        new ApiScope(name: "enumerate", displayName: "Allows enumerating data.")
    };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
    {
        new ApiResource("invoice", "Invoice API")
        {
            Scopes = { "invoice.read", "invoice.pay", "manage", "enumerate" }
        },

        new ApiResource("customer", "Customer API")
        {
            Scopes = { "customer.read", "customer.contact", "manage", "enumerate" }
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
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, // Loại GrantType sử dụng
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "invoice.read", "invoice.pay" } // Gán Scopes cho Client
                }
            };
        }
    }
}
