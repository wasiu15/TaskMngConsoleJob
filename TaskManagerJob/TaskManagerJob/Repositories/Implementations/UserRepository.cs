using Microsoft.EntityFrameworkCore;
using TaskManagerJob.Data;
using TaskManagerJob.Entities;
using TaskManagerJob.Repositories.Interfaces;

namespace TaskManagerJob.Repositories.Implementations
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext context) : base(context)
        {

        }
        public async Task<User> GetByUserId(string userId, bool trackChanges) => await FindByCondition(x => x.UserId.Equals(userId), trackChanges).FirstOrDefaultAsync();
    }
}
