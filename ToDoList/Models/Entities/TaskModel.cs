using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models.Entities
{
    [Table("Tasks")]
    public class TaskModel
    {
        public int Id { get; set; }
        public string TaskDescription { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? FinishDate { get; set; }
        public int? CategoryId { get; set; }
        public virtual CategoryModel Category { get; set; }
    }
}
