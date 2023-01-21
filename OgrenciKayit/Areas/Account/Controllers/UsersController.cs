using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace MvcWebUI.Areas.Account.Controllers
{
	[Area("Account")] // route üzerinden area adı
	public class UsersController : Controller
	{
		private readonly IAccountService _accountService;
		private readonly ICountryService _countryService;
		private readonly ICityService _cityservice;
		public UsersController(IAccountService accountService, ICountryService countryService, ICityService cityservice)
		{
			_accountService = accountService;
			_countryService = countryService;
			_cityservice = cityservice;
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(AccountLoginModel model)
		{
			if (ModelState.IsValid)
			{
				var userResult = new UserModel();
				var result = _accountService.Login(model, userResult);
				if (result.IsSuccessful)
				{
					List<Claim> claims = new List<Claim>()
					{
						new Claim(ClaimTypes.Name, userResult.UserName),
						new Claim(ClaimTypes.Role, userResult.RoleNameDisplay)
					};
					var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
					var principal = new ClaimsPrincipal(identity);
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
					return RedirectToAction("Index", "Home", new { area = "" });
				}
				ModelState.AddModelError("", result.Message);
			}
			return View();
		}

		public IActionResult Register()
		{
			ViewBag.Countries = new SelectList(_countryService.Query().ToList(), "Id", "Name");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Register(AccountRegisterModel model)
		{
			if (ModelState.IsValid)
			{
				var result = _accountService.Register(model);
				if (result.IsSuccessful)
					return RedirectToAction(nameof(Login));
				ModelState.AddModelError("", result.Message);
			}
			ViewBag.Countries = new SelectList(_countryService.Query().ToList(), "Id", "Name", model.UserDetail.CountryId);
			ViewBag.Cities = new SelectList(_cityservice.GetList(model.UserDetail.CountryId ?? 0), "Id", "Name", model.UserDetail.CityId);
			return View(model);
		}

		public IActionResult AccessDenied()
		{
			return View("_Error", "You don't have access to this operation!");
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home", new { area = "" });
		}
	}
}
