using GraphQL.Types;
using ToDoList.Factories;
using ToDoList.Models.Entities;
using ToDoList.Repositories;

namespace ToDoListAPI.Types
{
	public class TaskType : ObjectGraphType<TaskModel>
	{
		public TaskType(RepositoryFactory factory)
		{
			var repository = factory.GetRepository(factory._httpContextAccessor.HttpContext);
			Field(task => task.Id, true);
			Field(task => task.TaskDescription, true);
			Field(task => task.IsCompleted, true);
			Field(task => task.CategoryId, true);
			Field(task => task.FinishDate, true);
			Field<CategoryType>("category").Resolve(
			context =>
			{
				if (context.Source.CategoryId.HasValue)
					return repository.GetCategoryById(context.Source.CategoryId.Value);
				return null;
			});
		}
	}
}
