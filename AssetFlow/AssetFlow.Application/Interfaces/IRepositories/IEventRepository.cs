using System;
using System.Collections.Generic;
using System.Text;
using Marten;

namespace AssetFlow.Application.Interfaces.IRepositories
{
    public interface IEventRepository
    {
        Task StartStreamAsync<TEvent>(Guid streamId, TEvent @event);
        Task AppendEventAsync<TEvent>(Guid streamId, TEvent @event);
        Task<TAggregate?> AggregateStreamAsync<TAggregate>(Guid streamId) where TAggregate : class, new();

        //Transactions
        void StartStreamTransactional<TEvent>(Guid streamId, TEvent @event);
        void AppendEventTransactional<TEvent>(Guid streamId, TEvent @event);

        void WithSession(IDocumentSession session);

    }
}
