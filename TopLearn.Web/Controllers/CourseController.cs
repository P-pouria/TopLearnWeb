using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Controllers
{
    public class CourseController : Controller
    {
        private ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public IActionResult Index(int pageId = 1, string filter = "", string getType = "all", string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null)
        {
            ViewBag.selectedGroups = selectedGroups;
            ViewBag.Groups = _courseService.GetAllGroup();
            ViewBag.pageId = pageId;

            var coursesData = _courseService.GetCourse(pageId, filter, getType, orderByType, startPrice, endPrice, selectedGroups, 9);

            return View(coursesData);
        }

        [Route("ShowCourse/{id}")]
        public IActionResult ShowCoruse(int id)
        {
            var course =_courseService.GetCourseForShow(id);
            if(course==null)
            {
                return NotFound();
            }
            return View(course);
        }
    }
}
