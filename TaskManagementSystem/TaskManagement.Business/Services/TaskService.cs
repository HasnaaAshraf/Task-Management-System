using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Data.Contracts.RepositoryInterfaces;
using TaskManagement.Data.Contracts.ServicesInterfaces;
using TaskManagement.Data.Models;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Business.Services
{
	public class TaskService : ITaskService
	{

		private readonly ITaskRepository _taskRepository;

		public TaskService(ITaskRepository taskRepository)
		{
			_taskRepository = taskRepository;
		}

		public async Task<List<TaskItem>> GetAllTasksAsync()
		{
			return await _taskRepository.GetAllTasksAsync();
		}

		public async Task<TaskItem> GetTaskByIdAsync(int id)
		{
			return await _taskRepository.GetTaskByIdAsync(id);
		}

		public async Task AddTaskAsync(TaskItem task)
		{
		    await _taskRepository.AddTaskAsync(task);
		}

		public async Task UpdateTaskAsync(TaskItem task)
		{
			await _taskRepository.UpdateTaskAsync(task);
		}

		public async Task DeleteTaskAsync(int id)
		{
			var task = await _taskRepository.GetTaskByIdAsync(id);

			if (task != null)
			{
			    await _taskRepository.DeleteTaskAsync(id);
			}
		}

	}
}
