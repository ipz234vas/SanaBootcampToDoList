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

		public List<CategoryModel> GetCategories()
		{
			var connection = _context.CreateConnection();

			var sql = "SELECT * FROM Categories ORDER BY Name";

			var categories = connection.Query<CategoryModel>(sql);

			return categories.ToList();
		}

		public void AddTask(TaskModel task)
		{
			var connection = _context.CreateConnection();

			var sql = "INSERT INTO Tasks (TaskDescription, isCompleted, FinishDate, CategoryId) " +
			   "VALUES (@TaskDescription, @isCompleted, @FinishDate, @CategoryId)";

			connection.Execute(sql, task);
		}

		public void UpdateTaskStatus(int taskId, bool isCompleted)
		{
			var connection = _context.CreateConnection();

			var sql = "UPDATE Tasks SET IsCompleted = @isCompleted WHERE Id = @taskId";
			connection.Execute(sql, new { TaskId = taskId, IsCompleted = isCompleted });
		}

		public void DeleteTask(int taskId)
		{
			var connection = _context.CreateConnection();

			var sql = "DELETE FROM Tasks WHERE Id = @taskId";
			connection.Execute(sql, new { TaskId = taskId });
		}
	}
}
