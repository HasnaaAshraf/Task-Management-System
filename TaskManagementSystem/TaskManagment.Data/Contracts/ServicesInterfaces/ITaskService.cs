using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Data.Models;

namespace TaskManagement.Data.Contracts.ServicesInterfaces
{
	public interface ITaskService
	{
		Task<List<TaskItem>> GetAllTasksAsync();
		Task<TaskItem> GetTaskByIdAsync(int id);
		Task AddTaskAsync(TaskItem task);
		Task UpdateTaskAsync(TaskItem task);
		Task DeleteTaskAsync(int id);
	}
}
