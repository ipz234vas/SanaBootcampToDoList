using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
	public class ToDoListDBContext : DbContext
	{
		public ToDoListDBContext(DbContextOptions<ToDoListDBContext> options)
		: base(options)
		{
		}
		public DbSet<CategoryModel> Categories { get; set; }
		public DbSet<TaskModel> Tasks { get; set; }
	}
}
