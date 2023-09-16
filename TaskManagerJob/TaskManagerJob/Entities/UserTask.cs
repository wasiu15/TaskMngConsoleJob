using System.ComponentModel.DataAnnotations;
using TaskManagerJob.Utilities;

namespace TaskManagerJob.Entities
{
    public class UserTask
    {
        [Key]
        public string TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public string? ProjectId { get; set; }
        public string? UserId { get; set; }
    }
}
