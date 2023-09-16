using TaskManagerJob.Data;
using TaskManagerJob.Repositories.Interfaces;

namespace TaskManagerJob.Repositories.Implementations
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<ITaskRepository> _taskRepository;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<INotificationRepository> _notificationRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _taskRepository = new Lazy<ITaskRepository>(() => new TaskRepository(repositoryContext));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryContext));
            _notificationRepository = new Lazy<INotificationRepository>(() => new NotificationRepository(repositoryContext));
        }

        public ITaskRepository TaskRepository => _taskRepository.Value;
        public IUserRepository UserRepository => _userRepository.Value;
        public INotificationRepository NotificationRepository => _notificationRepository.Value;

        public async Task SaveAsync() => _repositoryContext.SaveChanges();
    }
}
