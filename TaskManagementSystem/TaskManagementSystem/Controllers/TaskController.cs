using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagement.Business.Services;
using TaskManagement.Data.Contracts.ServicesInterfaces;
using TaskManagement.Data.Models;
using TaskManagement.Presentation.Models;

namespace TaskManagement.Presentation.Controllers
{
	[Authorize]
	public class TaskController : Controller
	{
		private readonly ITaskService _taskService;
		private readonly IUserService _userService;

		public TaskController(ITaskService taskService,IUserService userService)
		{
			_taskService = taskService;
			_userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> Index(string search, bool showCompleted)
		{
			var tasks = await _taskService.GetAllTasksAsync();

			if (!string.IsNullOrEmpty(search))
			{
				tasks = tasks
					   .Where(t => t.Title.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
			};

			if (showCompleted)
			{
				tasks = tasks.Where(t => t.IsCompleted).ToList();
			}

			var model = new TasksHomeViewModel
			{
				Tasks = tasks ?? new List<TaskItem>()
			};

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var task = await _taskService.GetTaskByIdAsync(id);
			if (task == null) return NotFound();
			return View(task);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(TaskItem taskItem)
		{
			if (!ModelState.IsValid)
				return View(taskItem);

			var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

			if (userId == null)
				return Unauthorized();

			taskItem.UserId = int.Parse(userId);
			await _taskService.AddTaskAsync(taskItem);
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var task = await _taskService.GetTaskByIdAsync(id);
			if (task == null) return NotFound();
			return View(task);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id ,TaskItem taskItem)
		{
			var task = await _taskService.GetTaskByIdAsync(id);
			if (task == null) return NotFound();

			if (!ModelState.IsValid)
				return View(taskItem);
			
			task.Title = taskItem.Title;
			task.Description = taskItem.Description;
			task.DueTo = taskItem.DueTo;
			task.IsCompleted = taskItem.IsCompleted;
			
			await _taskService.UpdateTaskAsync(task);
			return RedirectToAction("Index");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			var task = await _taskService.GetTaskByIdAsync(id);
			if (task == null) return NotFound();
			await _taskService.DeleteTaskAsync(id);
			return RedirectToAction("Index","Task");
		}

		[HttpPost]
		public async Task<IActionResult> ToggleStatus(int id)
		{
			var tasks = await _taskService.GetTaskByIdAsync(id);

			if (tasks == null) return NotFound();

			tasks.IsCompleted = !tasks.IsCompleted;

			await _taskService.UpdateTaskAsync(tasks);

			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> Extend(int id)
		{
			var task = await _taskService.GetTaskByIdAsync(id);

			if (task == null)
				return NotFound();

			task.DueTo = task.DueTo.AddMinutes(10);

			await _taskService.UpdateTaskAsync(task);

			return Ok();
		}


	}
}
