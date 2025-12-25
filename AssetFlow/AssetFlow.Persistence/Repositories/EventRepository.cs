using AssetFlow.Application.Interfaces.IRepositories;
using Marten;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;

namespace AssetFlow.Persistence.Repositories
{
    public class EventRepository : IEventRepository
    {
        private IDocumentSession _session;
        private readonly ILogger<EventRepository> _logger;

        public EventRepository(ILogger<EventRepository> logger)
        {
            _logger = logger;
        }

        public void WithSession(IDocumentSession session)
        {
            _session = session;
        }

        public async Task StartStreamAsync<TEvent>(Guid streamId, TEvent @event)
        {
            _session.Events.StartStream(streamId, @event);
            await _session.SaveChangesAsync();
        }

        public async Task AppendEventAsync<TEvent>(Guid streamId, TEvent @event)
        {
            _session.Events.Append(streamId, @event);
            await _session.SaveChangesAsync();
        }

        public async Task<TAggregate?> AggregateStreamAsync<TAggregate>(Guid streamId)
            where TAggregate : class, new()
        {
            return await _session.Events.AggregateStreamAsync<TAggregate>(streamId);
        }

        public void StartStreamTransactional<TEvent>(Guid streamId, TEvent @event)
        {
            _session.Events.StartStream(streamId, @event);
        }

        public void AppendEventTransactional<TEvent>(Guid streamId, TEvent @event)
        {
            _session.Events.Append(streamId, @event);
        }
    }
}
