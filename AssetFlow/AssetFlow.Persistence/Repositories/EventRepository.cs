using AssetFlow.Application.Interfaces.IRepositories;
using Marten;

namespace AssetFlow.Persistence.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IDocumentSession _session;

        public EventRepository(IDocumentSession session)
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
