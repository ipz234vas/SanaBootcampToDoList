﻿using ToDoList.Models.Entities;
using ToDoList.Models.Contextes;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace ToDoList.Repositories
{
	public class XMLRepository : IRepository
	{
		private readonly ToDoListXMLContext _context;

		public XMLRepository(ToDoListXMLContext context)
		{
			_context = context;
		}
		public List<TaskModel> GetTasks()
		{
			XDocument document = XDocument.Load(_context._XMLConnectionString);

			var categories = document.Root.Elements("categories").Elements("category").Select(task => new CategoryModel
			{
				Id = int.Parse(task.Element("id").Value),
				Name = task.Element("name").Value,
				Description = task.Element("description").Value,
			}).ToDictionary(category => category.Id);

			var tasks = document.Root.Elements("tasks").Elements("task").Select(task => new TaskModel
			{
				Id = int.Parse(task.Element("id").Value),
				TaskDescription = task.Element("taskDescription").Value,
				IsCompleted = bool.Parse(task.Element("isCompleted").Value),
				FinishDate = string.IsNullOrEmpty(task.Element("finishDate").Value) ? (DateTime?)null : DateTime.Parse(task.Element("finishDate").Value),
				CategoryId = string.IsNullOrEmpty(task.Element("categoryId").Value) ? (int?)null : int.Parse(task.Element("categoryId").Value),
				Category = string.IsNullOrEmpty(task.Element("categoryId").Value) ? null : categories[int.Parse(task.Element("categoryId").Value)]
			});

			tasks = tasks.OrderByDescending(task => task.Id);
			tasks = tasks.OrderBy(task => task.IsCompleted);

			return tasks.ToList();
		}
		public TaskModel GetTaskById(int taskId)
		{
			XDocument document = XDocument.Load(_context._XMLConnectionString);

			var taskElement = document.Root.Elements("tasks").Elements("task").FirstOrDefault(task => (int)task.Element("id") == taskId);

			if (taskElement != null)
			{
				var task = new TaskModel
				{
					Id = int.Parse(taskElement.Element("id").Value),
					TaskDescription = taskElement.Element("taskDescription").Value,
					IsCompleted = bool.Parse(taskElement.Element("isCompleted").Value),
					FinishDate = string.IsNullOrEmpty(taskElement.Element("finishDate").Value) ? (DateTime?)null : DateTime.Parse(taskElement.Element("finishDate").Value),
					CategoryId = string.IsNullOrEmpty(taskElement.Element("categoryId").Value) ? (int?)null : int.Parse(taskElement.Element("categoryId").Value)
				};

				if (task.CategoryId.HasValue)
				{
					task.Category = GetCategoryById(task.CategoryId.Value);
				}

				return task;
			}

			return null;
		}

		public List<CategoryModel> GetCategories()
		{
			XDocument document = XDocument.Load(_context._XMLConnectionString);
			var categories = document.Root.Elements("categories").Elements("category").Select(category => new CategoryModel
			{
				Id = int.Parse(category.Element("id").Value),
				Name = category.Element("name").Value,
				Description = category.Element("description").Value,
			});

			return categories.OrderBy(category => category.Name).ToList();
		}
		public CategoryModel GetCategoryById(int categoryId)
		{
			XDocument document = XDocument.Load(_context._XMLConnectionString);

			var category = document.Root
				.Elements("categories")
				.Elements("category")
				.FirstOrDefault(category => (int)category.Element("id") == categoryId);

			if (category != null)
			{
				return new CategoryModel
				{
					Id = int.Parse(category.Element("id").Value),
					Name = category.Element("name").Value,
					Description = category.Element("description").Value
				};
			}

			return null;
		}
		public TaskModel AddTask(TaskModel task)
		{
			XDocument document = XDocument.Load(_context._XMLConnectionString);

			XElement nextTaskIdElement = document.Root.Element("nextTaskId");
			int nextTaskId = int.Parse(nextTaskIdElement.Value);

			task.Id = nextTaskId;

			nextTaskIdElement.Value = (nextTaskId + 1).ToString();

			XElement tasksElement = document.Root.Element("tasks");

			XElement newTaskElement = new XElement("task",
				new XElement("id", task.Id),
				new XElement("taskDescription", task.TaskDescription),
				new XElement("isCompleted", task.IsCompleted),
				new XElement("finishDate", task.FinishDate.HasValue ? task.FinishDate.Value.ToString("yyyy-MM-dd") : ""),
				new XElement("categoryId", task.CategoryId.HasValue ? task.CategoryId.Value.ToString() : "")
			);

			tasksElement.Add(newTaskElement);

			document.Save(_context._XMLConnectionString);

			return task;
		}
		public void UpdateTaskStatus(int taskId, bool IsCompleted)
		{
			XDocument document = XDocument.Load(_context._XMLConnectionString);

			XElement taskElement = document.Root.Element("tasks").Elements("task").FirstOrDefault(task => (int)task.Element("id") == taskId);
			if (taskElement != null)
			{
				taskElement.Element("isCompleted").Value = IsCompleted.ToString().ToLower();

				document.Save(_context._XMLConnectionString);
			}
			else throw new Exception($"Task with ID {taskId} does not exist.");
		}
		public void DeleteTask(int taskId)
		{
			XDocument document = XDocument.Load(_context._XMLConnectionString);

			XElement taskElement = document.Root.Element("tasks").Elements("task").FirstOrDefault(task => (int)task.Element("id") == taskId);

			if (taskElement != null)
			{
				taskElement.Remove();

				document.Save(_context._XMLConnectionString);
			}
			else throw new Exception($"Task with ID {taskId} does not exist.");
		}
	}
}
