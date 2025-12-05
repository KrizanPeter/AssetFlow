using AssetFlow.Application.Interfaces.IRepositories;
using AssetFlow.Application.Interfaces.IServices;
using AssetFlow.Domain.Entities;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssetFlow.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDocumentRepositoryDb<Account> _repository;

        public AccountService(IDocumentRepositoryDb<Account> repository)
        {
            _repository = repository;
        }

        public async Task<Result> CreateAccount(Guid userId, Guid accountId)
        {
            try
            {
                await _repository.AddAsync(new Account
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