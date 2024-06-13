using GraphQL.Types;
using ToDoListAPI.Mutations;
using ToDoListAPI.Queries;

namespace ToDoListAPI.Schemas
{
	public class MainSchema : Schema
	{
		public MainSchema(IServiceProvider serviceProvider) : base(serviceProvider)
		{
			Description = "Use the 'Repository' header to specify the repository type for todoList operations. Valid values are 'DataBase' and 'XML'.";
			Query = serviceProvider.GetRequiredService<MainQuery>();
			Query.Description = "queries for todoList\n" + "\n" + Description;
			Mutation = serviceProvider.GetRequiredService<MainMutation>();
			Mutation.Description = "mutations for todoList\n" + "\n" + Description;
		}
	}
}
