using AssetFlow.Domain.Entities.DocumentEntities;
using AssetFlow.Domain.Entities.EventAggregates;
using AssetFlow.Domain.Events;
using JasperFx;
using JasperFx.Events;
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

                // Schema for documents
                options.DatabaseSchemaName = "DomainDb";

                // Auto-create schema objects on startup
                options.AutoCreateSchemaObjects = AutoCreate.All;

                // Document entity configuration
                options.Schema.For<Account>().Identity(x => x.Id);

                // --- Event store configuration ---
                options.Events.DatabaseSchemaName = "EventDb"; // separate schema for events
                options.Events.StreamIdentity = StreamIdentity.AsGuid; // or AsString if you prefer

                // Register your event types (Asset events)
                options.Events.AddEventType<SnapshotAssetCreated>();
                options.Events.AddEventType<LedgerAssetCreated>();

            });
        }
    }
}
