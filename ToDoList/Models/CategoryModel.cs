using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
	[Table("Categories")]
	public class CategoryModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public virtual ICollection<TaskModel> Tasks { get; set; }
	}
}
