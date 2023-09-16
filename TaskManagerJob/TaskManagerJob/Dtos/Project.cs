using System.ComponentModel.DataAnnotations;
using TaskManagerJob.Entities;

namespace TaskManagerJob.Dtos
{
    public class Project
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<UserTask> UserTasks { get; set; }
    }
}
