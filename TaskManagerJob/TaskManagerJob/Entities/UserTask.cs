using System.ComponentModel.DataAnnotations;
using TaskManagerJob.Dtos;
using TaskManagerJob.Utilities;

namespace TaskManagerJob.Entities
{
    public class UserTask
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public string UserId { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
