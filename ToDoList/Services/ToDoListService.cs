using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Services
{
	public class ToDoListService
	{
		private readonly ToDoListDBContext _context;

		public ToDoListService(ToDoListDBContext context)
		{
			_context = context;
		}

		public List<TaskModel> GetTasks()
		{
			var tasks = _context.Tasks.Include(t => t.Category).OrderByDescending(t => t.Id).ToList();
			tasks = tasks.OrderBy(t => t.IsCompleted).ToList();
			return tasks;
		}
        public List<CategoryModel> GetCategories()
        {
            return _context.Categories.ToList();
        }
        public void AddTask(TaskModel task)
		{
			_context.Tasks.Add(task);
			_context.SaveChanges();
		}

		public void UpdateTaskStatus(int taskId, bool IsCompleted)
		{
			var task = _context.Tasks.Find(taskId);
			if (task != null)
			{
				task.IsCompleted = IsCompleted;
				_context.SaveChanges();
			}
		}

		public void DeleteTask(int taskId)
		{
			var task = _context.Tasks.Find(taskId);
			if (task != null)
			{
				_context.Remove(task);
				_context.SaveChanges();
			}
		}
	}

}
