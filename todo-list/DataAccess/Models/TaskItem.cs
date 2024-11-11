using System.ComponentModel.DataAnnotations;

namespace todo_list.DataAccess.Models
{
    public class TaskItem
    {
        [Key]
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
