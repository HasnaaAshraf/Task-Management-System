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
	public class UserRepository : IUserRepository
	{

		private readonly TaskDBContext _taskDBContext;

		public UserRepository(TaskDBContext taskDBContext)
		{
			_taskDBContext = taskDBContext;
		}

		public async Task<List<User>> GetAllUsersAsync()
		{
			return await _taskDBContext.Users.AsNoTracking().ToListAsync();
		}

		public async Task<User?> GetUserByIdAsync(int id)
		{
			return await _taskDBContext.Users.FindAsync(id);
		}

		public async Task<User?> GetUserByEmailAsync(string email)
		{
			return await _taskDBContext.Users.FirstOrDefaultAsync(u => u.Email == email);
		}

		public async Task AddUserAsync(User user)
		{
			await _taskDBContext.Users.AddAsync(user);
			await SaveChangesAsync();
		}		

		public async Task UpdateUserAsync(User user)
		{
			_taskDBContext.Users.Update(user);
			await SaveChangesAsync();
		}

		public async Task DeleteUserAsync(int id)
		{
			var user = await _taskDBContext.Users.FindAsync(id);

			if (user != null)
			{
				_taskDBContext.Users.Remove(user);
				await SaveChangesAsync();
			}
		}

		public async Task SaveChangesAsync()
		{
			await _taskDBContext.SaveChangesAsync();
		}

	}
}
