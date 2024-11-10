using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.Courses
{
    public class CreateEpisodeModel : PageModel
    {
        private ICourseService _courseService;
        public CreateEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseEpisode CourseEpisode { get; set; }

        public void OnGet(int id)
        {
            CourseEpisode = new CourseEpisode();
            CourseEpisode.CourseId = id;
        }

        public IActionResult OnPost(IFormFile fileEpisode)
        {
            if (!ModelState.IsValid || fileEpisode == null)
            {
                return Page();
            }

            if (_courseService.CheckExistFile(fileEpisode.FileName))
            {
                ViewData["IsExistFile"] = true;
                return Page();
            }

            _courseService.AddEpisode(CourseEpisode, fileEpisode);

            return RedirectToAction("/Admin/Courses/IndexEpisode/" + CourseEpisode.CourseId);
        }
    }
}
