using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly SchoolContext _context;

        public CreateModel(SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; }

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    _context.Student.Add(Student);
        //    await _context.SaveChangesAsync();

        //    return RedirectToPage("./Index");
        //}


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyStudent = new Student();

            if (await TryUpdateModelAsync<Student>(
                emptyStudent,
                "student",   // Prefix for form value.
                s => s.FirstName, s => s.LastName, s => s.EnrollmentDate) //只创建这几个字段，阻止过多发布
               )
            {
                //如果更新模型对象成功，则把新加的对象添加到模型里面
                _context.Student.Add(emptyStudent);
                await _context.SaveChangesAsync();//会调用Insert命令
                return RedirectToPage("./Index");
            }

            return null;
        }
    }
}