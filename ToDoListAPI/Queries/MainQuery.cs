using GraphQL;
using GraphQL.Types;
using ToDoList.Factories;
using ToDoList.Models.Entities;
using ToDoList.Repositories;
using ToDoListAPI.Types;

namespace ToDoListAPI.Queries
{
	public class MainQuery : ObjectGraphType
	{
		public MainQuery(IRepository repository)
		{
			Field<TaskType>("task").Arguments(new QueryArgument<IdGraphType> { Name = "id" }).Resolve(context =>
			{
				var taskId = context.GetArgument<int>("id");
				try
				{
					return repository.GetTaskById(taskId);
				}
				catch (Exception ex)
				{
					context.Errors.Add(new ExecutionError(ex.Message));
					return null;
				}
			});
			
			Field<ListGraphType<TaskType>>("tasks").Resolve(context =>
			repository.GetTasks());

			Field<CategoryType>("category").Arguments(new QueryArgument<IdGraphType> { Name = "id" }).Resolve(context =>
			{
				var categoryId = context.GetArgument<int>("id");
				try
				{
					return repository.GetCategoryById(categoryId);
				}
				catch (Exception ex)
				{
					context.Errors.Add(new ExecutionError(ex.Message));
					return null;
				}
			});

			Field<ListGraphType<CategoryType>>("categories").Resolve(context =>
			repository.GetCategories());
		}
	}
}
