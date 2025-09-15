using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminUsersController(IUserRepository userRepository, UserManager<IdentityUser> userManager)
        {
            this._userRepository = userRepository;
            this._userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await _userRepository.GetAll();  // We could send the domain object

            var usersViewModel = new UserViewModel();
            usersViewModel.Users = new List<User>();
            foreach (var user in users) 
            {
                usersViewModel.Users.Add(new Models.ViewModels.User
                {
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName,
                    EmailAddress = user.Email
                });
            }
            return View(usersViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(UserViewModel userViewModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = userViewModel.Username,
                Email = userViewModel.Email,
            };

            var identitiResult = await _userManager.CreateAsync(identityUser, userViewModel.Password);

            if (identitiResult is not null)
            {
                if (identitiResult.Succeeded)
                {
                    // Asign Roles to this User
                    var roles = new List<string> { "User" };
                    if (userViewModel.AdminRoleChckBox)
                    {
                        roles.Add("Admin");
                    }

                    identitiResult =  await _userManager.AddToRolesAsync(identityUser, roles);
                    if (identitiResult is not null && identitiResult.Succeeded)
                    {
                        return RedirectToAction("List", "AdminUsers");

                    }
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userToBeDeleted = await _userManager.FindByIdAsync(id.ToString());
            if (userToBeDeleted is not null)
            {
                var result = await _userManager.DeleteAsync(userToBeDeleted);
                if (result is not null && result.Succeeded)
                {
                    return RedirectToAction("List", "AdminUsers");
                }
            }
            return View("List");
        }
    }
}
