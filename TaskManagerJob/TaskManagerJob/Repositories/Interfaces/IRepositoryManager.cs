namespace TaskManagerJob.Repositories.Interfaces
{
    public interface IRepositoryManager
    {
        ITaskRepository TaskRepository { get; }
        IUserRepository UserRepository { get; }
        INotificationRepository NotificationRepository { get; }
        Task SaveAsync();
    }
}
