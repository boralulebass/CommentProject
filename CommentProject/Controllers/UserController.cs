using CommentProject.EntityLayer.Concrete;
using CommentProject.Models.RoleViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using X.PagedList;

namespace CommentProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<AppRole> _roleManager;

		public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public IActionResult Index(int page=1)
		{
			var values = _userManager.Users.ToList().ToPagedList(page,6);
			return View(values);
		}
		[HttpGet]
		public async Task<IActionResult> AssignRole(int id)
		{
			var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
			TempData["userid"] = user.Id;
			var roles = _roleManager.Roles.ToList();
			var userroles = await _userManager.GetRolesAsync(user);
			List<RoleAssignViewModel> roleAssignViewModels = new List<RoleAssignViewModel>();
			foreach (var item in roles)
			{
				RoleAssignViewModel model = new RoleAssignViewModel();
				model.RoleID = item.Id;
				model.RoleName = item.Name;
				model.RoleExist = userroles.Contains(item.Name);
				roleAssignViewModels.Add(model);
			}
			return View(roleAssignViewModels);
		}
		[HttpPost]
		public async Task<IActionResult> AssignRole(List<RoleAssignViewModel> roleAssignViewModel)
		{
			var userid = (int)TempData["userid"];
			var user = _userManager.Users.FirstOrDefault(x => x.Id == userid);
			foreach (var item in roleAssignViewModel)
			{
				if (item.RoleExist)
				{
					await _userManager.AddToRoleAsync(user, item.RoleName);
				}
				else
				{
					await _userManager.RemoveFromRoleAsync(user, item.RoleName);
				}
			}
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> DeleteUser(int id)
		{
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
			await _userManager.DeleteAsync(user);
			return RedirectToAction("Index");
		}
		public IActionResult OneUser(int id)
		{
			var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
			ViewBag.name = user.Name;
			ViewBag.surname = user.Surname;
			ViewBag.mail = user.Email;
			ViewBag.id = user.Id;
			ViewBag.username = user.UserName;
			return View();
		}
		public async Task<IActionResult> EmailConfirming(int id) 
		{
			var user = _userManager.Users.FirstOrDefault(x=>x.Id == id);
			user.EmailConfirmed = true;
			await _userManager.UpdateAsync(user);
			return RedirectToAction("Index");
		}

	}
}
