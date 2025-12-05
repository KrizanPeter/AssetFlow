using AssetFlow.Domain.Entities;
using JasperFx;
using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AssetFlow.Persistence.ExtensionsDI
{
    public class MartenDI
    {
        public static void AddMartenServices(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddMarten(options =>
            {
                options.Connection(configuration.GetConnectionString("DomainConnection"));

                // Optional: schema name for documents
                options.DatabaseSchemaName = "DomainDb";

                // Auto-create schema objects on startup
                options.AutoCreateSchemaObjects = AutoCreate.All;

                // Example: add a document type explicitly
                options.Schema.For<Account>().Identity(x => x.Id);
            });
        }

    }
}
