using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Instructors
{
    public class EditModel : InstructorCoursesPageModel
    {
        private readonly ContosoUniversity.SchoolContext _context;

        public EditModel(ContosoUniversity.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Instructor Instructor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Instructor = await _context.Instructors.FirstOrDefaultAsync(m => m.ID == id);

            //Instructor = await _context.Instructors.Include(m => m.OfficeAssignment).FirstOrDefaultAsync(s => s.ID == id); 自己写的忘记加AsNoTracking

            //Instructor = await _context.Instructors
            //.Include(i => i.OfficeAssignment)
            //.AsNoTracking()
            //.FirstOrDefaultAsync(m => m.ID == id);

            //Include 相当于Left Join 
            Instructor = await _context.Instructors
            .Include(i => i.OfficeAssignment)
            .Include(i => i.CourseAssignments).ThenInclude(i => i.Course)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            if (Instructor == null)
            {
                return NotFound();
            }
            PopulateAssignedCourseData(_context, Instructor);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCourses)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            /*

               //先查找一个连接数据的临时表集合
               var instructorToUpdate= await _context.Instructors
               .Include(i => i.OfficeAssignment)
               .AsNoTracking()
               .FirstOrDefaultAsync(i => i.ID == id);


               //如果办公室位置为空，则将 Instructor.OfficeAssignment 设置为 null。 当 Instructor.OfficeAssignment 为 null 时，OfficeAssignment 表中的相关行将会删除。 因为OfficeAssignment 设置了外键引用参照Instructor
               //看不出哪里设置的？？？？
               if (await TryUpdateModelAsync<Instructor>(
                      instructorToUpdate,
                      "Instructor",
                      i => i.FirstName, i => i.LastName, i => i.HireDate, i => i.OfficeAssignment))//,i=>i.OfficeAssignment.Location 加上这个也不行， 因为这个是更新Instructors数据集？
               {
                   if (string.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment?.Location))//用Location因为string.IsNullOrWhiteSpace 接受的是字符串类型？？？更方便
                   {
                       instructorToUpdate.OfficeAssignment = null;
                   }
                   await _context.SaveChangesAsync();
               }

               return RedirectToPage("./Index");
           }*/

            var instructorToUpdate = await _context.Instructors
             .Include(i => i.OfficeAssignment)
             .Include(i => i.CourseAssignments)
                 .ThenInclude(i => i.Course)
             .FirstOrDefaultAsync(s => s.ID == id);

            if (await TryUpdateModelAsync<Instructor>(
                instructorToUpdate,
                "Instructor",
                i => i.FirstName, i => i.LastName,
                i => i.HireDate, i => i.OfficeAssignment))
            {
                if (String.IsNullOrWhiteSpace(
                    instructorToUpdate.OfficeAssignment?.Location))
                {
                    instructorToUpdate.OfficeAssignment = null;
                }
                UpdateInstructorCourses(_context, selectedCourses, instructorToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            UpdateInstructorCourses(_context, selectedCourses, instructorToUpdate);
            PopulateAssignedCourseData(_context, instructorToUpdate);
            return Page();
        }
    }

}
