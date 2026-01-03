using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssetFlow.Application.MediatR.Commands.Handlers
{
    public class AddSnapshotCommandHandler : IRequestHandler<AddSnapshotCommand, Result<Guid>>
    {
        public Task<Result<Guid>> Handle(AddSnapshotCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
