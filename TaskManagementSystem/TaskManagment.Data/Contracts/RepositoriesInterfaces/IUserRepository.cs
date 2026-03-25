using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Data.Models;

namespace TaskManagement.Data.Contracts.RepositoryInterfaces
{
	public interface IUserRepository
	{
		Task<List<User>> GetAllUsersAsync();

		Task<User> GetUserByIdAsync(int id);

		Task<User> GetUserByEmailAsync(string email);

		Task AddUserAsync(User user);

		Task UpdateUserAsync(User user);
		Task DeleteUserAsync(int id);
		Task SaveChangesAsync();
	}
}
