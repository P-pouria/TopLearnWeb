using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.ViewComponents
{
    public class CourseGroupComponent : ViewComponent
    {
        private ICourseService _courseService;

        public CourseGroupComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var groups = await _courseService.GetAllGroup();
            if (groups == null || !groups.Any())
            {
                return Content("هیچ گروهی برای نمایش وجود ندارد.");
            }
            return View("CourseGroup", groups);
        }

    }
}
