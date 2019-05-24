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
    public class CreateModel1 : PageModel
    {
        private readonly SchoolContext _context;

        public CreateModel1(SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]//绑定此视图模型的属性，作为新增条件，模拟提交增加参数是没有用的。
        public StudentVM StudentVM { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var entry = _context.Add(new Student());
            entry.CurrentValues.SetValues(StudentVM);//从另一个 PropertyValues 对象读取值来设置此对象的值。 SetValues 使用属性名称匹配。 视图模型类型不需要与模型类型相关，它只需要具有匹配的属性。
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }

    public class StudentVM
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}