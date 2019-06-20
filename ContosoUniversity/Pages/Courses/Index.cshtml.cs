using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity;
using ContosoUniversity.Models;
using ContosoUniversity.Views;

namespace ContosoUniversity.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.SchoolContext _context;

        public IndexModel(ContosoUniversity.SchoolContext context)
        {
            _context = context;
        }

        //public IList<Course> Course { get;set; }

        //public async Task OnGetAsync()
        //{   
        //    //预先加载Department
        //    Course = await _context.Courses
        //        .Include(c => c.Department).ToListAsync();
        //}

        public IList<CourseViewModel> CourseVM { get; set; }

        public async Task OngetAsync()
        {
            CourseVM = await _context.Courses
                    .Select(p => new CourseViewModel
                    {
                        CourseID = p.CourseID,
                        Title = p.Title,
                        Credits = p.Credits,
                        DepartmentName = p.Department.Name,
                        MoneySecurity = p.MoneySecurity,
                        ModifyTime=p.ModifyTime,
                        ModifyDate=p.ModifyDate
                    }
                ).ToListAsync();
        }
    }
}
