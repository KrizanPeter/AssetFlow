using AssetFlow.Application.Interfaces.IRepositories;
using AssetFlow.Domain.Entities.Auth;
using AssetFlow.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssetFlow.Persistence.Repositories
{
    public class UserRepository : Repository<AppUser>, IUserRepository
    {
        private readonly IdentityContext db;

        public UserRepository(IdentityContext db) : base(db)
        {
            this.db = db;
        }
        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void Update(AppUser user)
        {
            var objFromDb = db.Users.FirstOrDefault(a => a.Id == user.Id);
        }

        public AppUser? UpdateAccountId(Guid userId, Guid accountId)
        {
            var user = db.Users.First(a => a.Id == userId);

            if (user != null)
            {
                user.AccountId = accountId;
                db.SaveChanges();
                return user;
            }
            return user;
        }
    }
}
