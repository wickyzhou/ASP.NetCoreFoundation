using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly SchoolContext _context;

        public DetailsModel(SchoolContext context)
        {
            _context = context;
        }

        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Student = await _context.Student.FirstOrDefaultAsync(m => m.ID == id);

            Student = await _context.Student
                            .Include(s => s.Enrollments) //加载Stundent类定义的导航属性
                                .ThenInclude(e => e.Course) //加载Enrollment类中的Cource导航属性
                            .AsNoTracking() //对于返回的实体未在当前上下文中更新的情况,可以提升性能。
                            .FirstOrDefaultAsync(m => m.ID == id);


            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
