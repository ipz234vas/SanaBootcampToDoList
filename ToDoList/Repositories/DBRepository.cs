using Dapper;
using ToDoList.Models.Entities;
using ToDoList.Models.Contextes;

namespace ToDoList.Repositories
{
	public class DBRepository : IRepository
	{
		private readonly ToDoListDBContext _context;

		public DBRepository(ToDoListDBContext context)
		{
			_context = context;
		}

		public List<TaskModel> GetTasks()
		{
			var connection = _context.CreateConnection();

			var sql = @"
						SELECT task.*, category.*
						FROM Tasks task
						LEFT JOIN Categories category ON task.CategoryId = category.Id
						";

			var taskDictionary = new Dictionary<int, TaskModel>();

			var tasks = connection.Query<TaskModel, CategoryModel, TaskModel>(
				sql,
				(task, category) =>
				{
					TaskModel taskEntry;

					if (!taskDictionary.TryGetValue(task.Id, out taskEntry))
					{
						taskEntry = task;
						taskEntry.Category = category;
						taskDictionary.Add(taskEntry.Id, taskEntry);
					}
					else
					{
						taskEntry.Category = category;
					}

					return taskEntry;
				},
				splitOn: "Id"
			);

			tasks = tasks.OrderByDescending(task => task.Id);
			tasks = tasks.OrderBy(task => task.IsCompleted);

			return tasks.ToList();
		}

		public TaskModel GetTaskById(int taskId)
		{
			var connection = _context.CreateConnection();

			var sql = "SELECT * FROM Tasks WHERE Id = @TaskId";
			var task = connection.QuerySingleOrDefault<TaskModel>(sql, new { TaskId = taskId });

			if (task != null && task.CategoryId.HasValue)
			{
				task.Category = GetCategoryById(task.CategoryId.Value);
			}

			return task;
		}

		public List<CategoryModel> GetCategories()
		{
			var connection = _context.CreateConnection();

			var sql = "SELECT * FROM Categories ORDER BY Name";

			var categories = connection.Query<CategoryModel>(sql);

			return categories.ToList();
		}
		public CategoryModel GetCategoryById(int categoryId)
		{
			var connection = _context.CreateConnection();

			var sql = "SELECT * FROM Categories WHERE Id = @CategoryId";

			var category = connection.QuerySingleOrDefault<CategoryModel>(sql, new { CategoryId = categoryId });

			return category;
		}

		public TaskModel AddTask(TaskModel task)
		{
			var connection = _context.CreateConnection();

			var sql = @"
						INSERT INTO Tasks (TaskDescription, IsCompleted, FinishDate, CategoryId)
						OUTPUT INSERTED.Id
						VALUES (@TaskDescription, @IsCompleted, @FinishDate, @CategoryId);
					   ";

			var taskId = connection.QuerySingle<int>(sql, task);
			task.Id = taskId;

			return task;
		}

		public void UpdateTaskStatus(int taskId, bool isCompleted)
		{
			var connection = _context.CreateConnection();

			var sql = "UPDATE Tasks SET IsCompleted = @isCompleted WHERE Id = @taskId";
			int updatedTasksCount = connection.Execute(sql, new { TaskId = taskId, IsCompleted = isCompleted });
			if(updatedTasksCount == 0) throw new Exception($"Task with ID {taskId} does not exist.");
		}

		public void DeleteTask(int taskId)
		{
			var connection = _context.CreateConnection();

			var sql = "DELETE FROM Tasks WHERE Id = @taskId";
			int deletedTasksCount = connection.Execute(sql, new { TaskId = taskId });
			if (deletedTasksCount == 0) throw new Exception($"Task with ID {taskId} does not exist.");
		}
	}
}
