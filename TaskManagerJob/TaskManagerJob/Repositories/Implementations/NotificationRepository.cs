using Microsoft.EntityFrameworkCore;
using TaskManagerJob.Data;
using TaskManagerJob.Entities;
using TaskManagerJob.Repositories.Interfaces;

namespace TaskManagerJob.Repositories.Implementations
{
    public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        public NotificationRepository(RepositoryContext context) : base(context)
        {

        }

        public void CreateNotification(Notification notification) => Create(notification);
        public async Task<Notification> GetByNotificationIdAndUserId(string taskId, string userId, bool trackChanges) => await FindByCondition(x => x.TaskId.Equals(taskId) && x.RecievedUserId.Equals(userId), trackChanges).FirstOrDefaultAsync();
    }
}
