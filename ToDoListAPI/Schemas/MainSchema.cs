using GraphQL.Types;
using ToDoListAPI.Mutations;
using ToDoListAPI.Queries;

namespace ToDoListAPI.Schemas
{
	public class MainSchema : Schema
	{
		public MainSchema(IServiceProvider serviceProvider) : base(serviceProvider)
		{
			Query = serviceProvider.GetRequiredService<MainQuery>();
			Mutation = serviceProvider.GetRequiredService<MainMutation>();
		}
	}
}
