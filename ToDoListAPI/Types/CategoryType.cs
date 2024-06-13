using GraphQL.Types;
using ToDoList.Models.Entities;
using ToDoList.Repositories;

namespace ToDoListAPI.Types
{
	public class CategoryType : ObjectGraphType<CategoryModel>
	{
		public CategoryType(IRepository repository) 
		{
			Field(x => x.Id);
			Field(x => x.Name);
			Field(x => x.Description, true);
			Field<ListGraphType<TaskType>>("Tasks").Resolve(context =>
				 repository.GetTasks());
		}
	}
}
