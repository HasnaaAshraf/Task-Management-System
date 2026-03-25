using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Data.Contracts.RepositoryInterfaces;
using TaskManagement.Data.Contracts.ServicesInterfaces;
using TaskManagement.Data.Models;

namespace TaskManagement.Business.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<List<User>> GetAllUsersAsync()
		{
			return await _userRepository.GetAllUsersAsync();
		}

		public Task<User> GetUserByIdAsync(int id)
		{
			return _userRepository.GetUserByIdAsync(id);
		}

		public async Task<User> GetUserByEmailAsync(string email)
		{
			return await _userRepository.GetUserByEmailAsync(email);
		}

		public async Task AddUserAsync(User user)
		{
			await _userRepository.AddUserAsync(user);
		}

		public async Task UpdateUserAsync(User user)
		{
			await _userRepository.UpdateUserAsync(user);
		}

		public async Task DeleteUserAsync(int id)
		{
			var user = await _userRepository.GetUserByIdAsync(id);

			if (user != null)
			{
				await _userRepository.DeleteUserAsync(id);
			}
		}



	}
}
