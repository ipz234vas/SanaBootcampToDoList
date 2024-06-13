using Microsoft.EntityFrameworkCore;
using ToDoList.Models.Entities;

namespace ToDoList.Repositories
{
    public interface IRepository
    {
        public List<TaskModel> GetTasks();
		public TaskModel GetTaskById(int taskId);
		public List<CategoryModel> GetCategories();
        public CategoryModel GetCategoryById(int categoryId);
		public TaskModel AddTask(TaskModel task);
        public void UpdateTaskStatus(int taskId, bool IsCompleted);
        public void DeleteTask(int taskId);
    }
}
