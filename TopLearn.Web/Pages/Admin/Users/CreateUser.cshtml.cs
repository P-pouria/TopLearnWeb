using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.DTOs;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Pages.Admin.Users
{
    public class CreateUserModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;
        public CreateUserModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        [BindProperty]
        public CreateUserViewModel CreateUserViewModel { get; set; }
        public void OnGet()
        {
            ViewData["Roles"] = _permissionService.GetRoles();
        }

        public IActionResult OnPost(List<int>SelectedRoles)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            int userId = _userService.AddUserFormAdmin(CreateUserViewModel);

            //Add Roles
            _permissionService.AddRolesToUser(SelectedRoles,userId);

            return Redirect("/Admin/Users");
           
        }
    }
}
