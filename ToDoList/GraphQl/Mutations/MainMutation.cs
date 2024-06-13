using GraphQL;
using GraphQL.Types;
using ToDoList.Factories;
using ToDoList.GraphQl.Types;
using ToDoList.Models.Entities;

namespace ToDoList.GraphQl.Mutations
{
	public class MainMutation : ObjectGraphType
	{
		public MainMutation(RepositoryFactory factory)
		{
			var repository = factory.GetRepository(factory._httpContextAccessor.HttpContext);
			Field<TaskType>("addTask").Arguments(new QueryArgument<InputTaskType> { Name = "task" }).Resolve(context =>
			{
				var task = context.GetArgument<TaskModel>("task");
				try
				{
					return repository.AddTask(task);
				}
				catch (Exception ex)
				{
					context.Errors.Add(new ExecutionError(ex.Message));
					return ex.Message;
				}
			}).Description("Add new task");
			Field<TaskType>("updateTaskStatus").Arguments(new QueryArgument<InputTaskType> { Name = "task" }).Resolve(context =>
			{
				var taskToUpdate = context.GetArgument<TaskModel>("task");
				if (taskToUpdate != null)
				{
					try
					{
						repository.UpdateTaskStatus(taskToUpdate.Id, taskToUpdate.IsCompleted);
					}
					catch (Exception ex)
					{
						context.Errors.Add(new ExecutionError(ex.Message));
						return null;
					}
				}
				return repository.GetTaskById(taskToUpdate.Id);
			}).Description("Update task status");
			Field<String>("deleteTask").Arguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }).Resolve(context =>
			{
				var taskId = context.GetArgument<int>("id");

				try
				{
					repository.DeleteTask(taskId);
				}
				catch (Exception ex)
				{
					context.Errors.Add(new ExecutionError(ex.Message));
					return null;
				}

				return "Task deleted successful";
			}).Description("Delete task");
		}
	}
}
