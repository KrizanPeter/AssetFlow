using AssetFlow.Application.Interfaces.IServices;
using AssetFlow.Domain.Entities.DocumentEntities;
using FluentResults;

namespace AssetFlow.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _uow;

        public AccountService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result> CreateAccount(Guid userId, Guid accountId)
        {
            try
            {
                await _uow.Accounts.AddAsync(new Account
                {
                    Id = accountId,
                    AppUserId = userId,
                    DateOfCreation = DateTime.UtcNow,
                    DateOfLastModification = DateTime.UtcNow,
                });

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error("Failed to create account").CausedBy(ex));
            }
        }
    }
}