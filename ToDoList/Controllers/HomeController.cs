using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ToDoList.Factories;
using ToDoList.Models.ViewModels;
using ToDoList.Models.Entities;
using ToDoList.Repositories;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RepositoryFactory _repositoryFactory;

        public HomeController(ILogger<HomeController> logger, RepositoryFactory repositoryFactory)
        {
            _logger = logger;
            _repositoryFactory = repositoryFactory;
        }
        public IActionResult Index()
        {
            var _repository = _repositoryFactory.GetRepository();

            var categories = _repository.GetCategories();
            var tasks = _repository.GetTasks();

            var viewModel = new ToDoListViewModel
            {
                Categories = categories,
                Tasks = tasks
            };

			ViewBag.RepositoryType = _repositoryFactory.GetRepositoryType();
			return View(viewModel);
        }
        [HttpPost]
        public IActionResult AddTask(TaskModel model)
        {
            var _repository = _repositoryFactory.GetRepository();

            var newTask = new TaskModel
            {
                TaskDescription = model.TaskDescription,
                CategoryId = model.CategoryId,
                FinishDate = model.FinishDate
            };
            _repository.AddTask(newTask);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UpdateTaskStatus(int taskId, bool IsCompleted)
        {
            var _repository = _repositoryFactory.GetRepository();

            _repository.UpdateTaskStatus(taskId, IsCompleted);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteTask(int taskId)
        {
            var _repository = _repositoryFactory.GetRepository();

            _repository.DeleteTask(taskId);
            return RedirectToAction("Index");
        }
		[HttpPost]
		public IActionResult ChangeRepositoryType(RepositoryType repositoryType)
		{
			HttpContext.Session.SetString("RepositoryName", repositoryType.ToString());
			return RedirectToAction("Index");
		}
		public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult React()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
