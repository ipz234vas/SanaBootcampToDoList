using System.ComponentModel.DataAnnotations;
using ToDoList.Models.Entities;

namespace ToDoList.Models.ViewModels
{
    public class ToDoListViewModel
    {
        #region TaskModel
        [Required(ErrorMessage = "Опис завдання є обов'язковим")]
        public string TaskDescription { get; set; }

        public int? CategoryId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FinishDate { get; set; }
        #endregion
        public List<CategoryModel> Categories { get; set; }
        public List<TaskModel> Tasks { get; set; }
    }
}
