using System;
using AssetFlow.Domain.Entities.DocumentEntities;
using AssetFlow.Domain.Events;
using JasperFx.Events.Projections;
using Marten;
using Marten.Events;
using Marten.Events.Aggregation;
using Marten.Events.Projections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AssetFlow.Persistence.ExtensionsDI
{
    public class MartenDI
    {
        public static void AddMartenServices(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddMarten((StoreOptions options) =>
            {
                // Ensure a non-null connection string
                var connectionString = configuration.GetConnectionString("DomainConnection")
                    ?? throw new InvalidOperationException("Missing required connection string: DomainConnection");

                options.Connection(connectionString);

                // Schema for documents
                options.DatabaseSchemaName = "domain";

                // Document mapping
                options.Schema.For<Account>().Identity(x => x.Id);

                // Event store configuration
                options.Events.DatabaseSchemaName = "events";

                // Register event types
                options.Events.AddEventType<SnapshotAssetCreated>();
                options.Events.AddEventType<LedgerAssetCreated>();

                options.Projections.Add<SnapshotAssetProjection>(ProjectionLifecycle.Inline);
            });
        }
    }
}
