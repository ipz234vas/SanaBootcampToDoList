using GraphQL;
using GraphQL.Types;
using ToDoList.Models.Entities;
using ToDoList.Repositories;
using ToDoListAPI.Types;

namespace ToDoListAPI.Mutations
{
	public class MainMutation : ObjectGraphType
	{
		public MainMutation(IRepository repository)
		{
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
			});
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
				return taskToUpdate;
			});
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
			});
		}
	}
}