using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagement.Data.Contracts.ServicesInterfaces;
using TaskManagement.Data.Models;

namespace TaskManagement.Presentation.Controllers
{
	public class UserController : Controller
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(User user)
		{
			if (!ModelState.IsValid)
				return View(user);
			await _userService.AddUserAsync(user);
			return RedirectToAction("Login");
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(User user)
		{
			var userLogin = await _userService.GetUserByEmailAsync(user.Email);

			if (userLogin == null || userLogin.Password != user.Password)
			{
				ModelState.AddModelError("", "Invalid Email or Password");
				return View(user);
			}

			var claims = new List<Claim>
			{
			new Claim(ClaimTypes.Name, userLogin.Name),
			new Claim(ClaimTypes.Email, userLogin.Email),
			new Claim(ClaimTypes.NameIdentifier, userLogin.Id.ToString())
			};

			var identity = new ClaimsIdentity(claims, "CookieAuth");
			var principal = new ClaimsPrincipal(identity);

			await HttpContext.SignInAsync("CookieAuth", principal);

			return RedirectToAction("Index","Task");

		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Login");
		}

	}
}
