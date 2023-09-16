using TaskManagerJob.Entities;

namespace TaskManagerJob.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        void CreateNotification(Notification notification);
        Task<Notification> GetByNotificationIdAndUserId(string taskId, string userId, bool trackChanges);
    }
}
