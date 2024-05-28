using Microsoft.EntityFrameworkCore;
using ToDoList.Models.Entities;

namespace ToDoList.Repositories
{
    public interface IRepository
    {
        public List<TaskModel> GetTasks();
        public List<CategoryModel> GetCategories();
        public void AddTask(TaskModel task);
        public void UpdateTaskStatus(int taskId, bool IsCompleted);
        public void DeleteTask(int taskId);
    }
}
