using GraphQL;
using GraphQL.Types;
using ToDoList.Factories;
using ToDoList.GraphQl.Types;

namespace ToDoList.GraphQl.Queries
{
	public class MainQuery : ObjectGraphType
	{
		public MainQuery(RepositoryFactory factory)
		{
			var repository = factory.GetRepository(factory._httpContextAccessor.HttpContext);

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
			}).Description("Get task by Id");

			Field<ListGraphType<TaskType>>("tasks").Resolve(context =>
			repository.GetTasks()).Description("Get all tasks");

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
			}).Description("Get category by Id");

			Field<ListGraphType<CategoryType>>("categories").Resolve(context =>
			repository.GetCategories()).Description("Get all categories");
		}
	}
}
