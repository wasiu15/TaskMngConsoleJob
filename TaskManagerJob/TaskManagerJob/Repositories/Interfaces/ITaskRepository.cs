using TaskManagerJob.Entities;

namespace TaskManagerJob.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<UserTask>> GetAnyUnCompletedTaskToDueInTwoDays(bool trackChanges);
    }
}
