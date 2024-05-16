using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ToDoListService _service;

		public HomeController(ILogger<HomeController> logger, ToDoListDBContext context)
		{
			_logger = logger;
			_service = new ToDoListService(context);
		}
		public IActionResult Index()
		{
			var categories = _service.GetCategories();
			var tasks = _service.GetTasks();

			var viewModel = new ToDoListViewModel
			{
				Categories = categories,
				Tasks = tasks
			};

			return View(viewModel);
		}
		[HttpPost]
		public IActionResult AddTask(TaskModel model)
		{
			var newTask = new TaskModel
			{
				TaskDescription = model.TaskDescription,
				CategoryId = model.CategoryId,
				FinishDate = model.FinishDate
			};
			_service.AddTask(newTask);

			return RedirectToAction("Index");
		}
		[HttpPost]
		public IActionResult UpdateTaskStatus(int taskId, bool IsCompleted)
		{
			_service.UpdateTaskStatus(taskId, IsCompleted);
			return RedirectToAction("Index");
		}
		[HttpPost]
		public IActionResult DeleteTask(int taskId)
		{
			_service.DeleteTask(taskId);
			return RedirectToAction("Index");
		}
		public IActionResult Privacy()
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
