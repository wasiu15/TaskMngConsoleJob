using TaskManagerJob.Entities;

namespace TaskManagerJob.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByUserId(string userId, bool trackChanges);
    }
}
