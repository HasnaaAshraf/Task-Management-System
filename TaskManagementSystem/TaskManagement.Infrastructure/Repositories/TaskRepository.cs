using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Data.Contracts.RepositoryInterfaces;
using TaskManagement.Data.Models;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories
{
	public class TaskRepository : ITaskRepository
	{

		private readonly TaskDBContext _taskDBContext;

		public TaskRepository(TaskDBContext taskDBContext)
		{
			_taskDBContext = taskDBContext;
		}

		public async Task<List<TaskItem>> GetAllTasksAsync()
		{
			return await _taskDBContext.Tasks.AsNoTracking().ToListAsync();
		}

		public async Task<TaskItem?> GetTaskByIdAsync(int id)
		{
			return await _taskDBContext.Tasks.FindAsync(id);
		}

		public async Task AddTaskAsync(TaskItem task)
		{
		    await _taskDBContext.Tasks.AddAsync(task);
			await SaveChangesAsync();
		}

		public async Task UpdateTaskAsync(TaskItem task)
		{
		   _taskDBContext.Tasks.Update(task);
			await SaveChangesAsync();
		}

		public async Task DeleteTaskAsync(int id)
		{
			var task = await _taskDBContext.Tasks.FindAsync(id);

			if (task != null)
			{
				_taskDBContext.Tasks.Remove(task);
				await SaveChangesAsync();
			}
		}

		public async Task SaveChangesAsync()
		{
		   await _taskDBContext.SaveChangesAsync();
		}

	}
}
