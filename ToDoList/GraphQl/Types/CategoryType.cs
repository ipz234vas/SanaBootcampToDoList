using GraphQL.Types;
using ToDoList.Factories;
using ToDoList.Models.Entities;

namespace ToDoList.GraphQl.Types
{
	public class CategoryType : ObjectGraphType<CategoryModel>
	{
		public CategoryType(RepositoryFactory factory)
		{
			var repository = factory.GetRepository(factory._httpContextAccessor.HttpContext);
			Field(x => x.Id);
			Field(x => x.Name);
			Field(x => x.Description, true);
			Field<ListGraphType<TaskType>>("Tasks").Resolve(context =>
				 repository.GetTasks());
		}
	}
}
