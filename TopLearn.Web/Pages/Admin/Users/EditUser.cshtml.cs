using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.DTOs;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Web.Pages.Admin.Users
{
    public class EditUserModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;

        public EditUserModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        [BindProperty]
        public EditUserViewModel EditUserViewModel { get; set; }

        public void OnGet(int id)
        {
            EditUserViewModel = _userService.GetUserForShowInEditMode(id);
            var roles = _permissionService.GetRoles();
            ViewData["Roles"] = roles ?? new List<Role>();
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            // Check if ModelState is valid
            if (!ModelState.IsValid)
            {
                ViewData["Roles"] = _permissionService.GetRoles() ?? new List<Role>();
                return Page();
            }

            // Update user details
            _userService.EditUserFromAdmin(EditUserViewModel);
            _permissionService.EditRolesUser(EditUserViewModel.UserId, SelectedRoles);

            return RedirectToPage("Index");
        }
    }
}
