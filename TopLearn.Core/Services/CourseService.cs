using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services
{
    public class CourseService : ICourseService
    {
        private TopLearnContext _context;
        public CourseService(TopLearnContext context)
        {
            _context = context;
        }

        public async Task<List<CourseGroup>> GetAllGroup()
        {
            return await _context.CourseGroups
                         .Include(g => g.CourseGroups) // برای بارگذاری زیرگروه‌ها
                         .ToListAsync();
        }

    }
}
