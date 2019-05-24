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
    public class IndexModel : PageModel
    {
        private readonly SchoolContext _context;

        public IndexModel(SchoolContext context)
        {
            _context = context;
        }

        //public IList<Student> Student { get;set; }
        public PaginatedList<Student> Student { get; set; }
        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            //Razor 页面使用 NameSort 和 DateSort 为列标题超链接配置相应的查询字符串值
            NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            CurrentSort = sortOrder;
            CurrentFilter = searchString;

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {   
                //翻页的时候，必须把筛选条件清除，不然又会做过滤查询，影响总条数
                searchString = currentFilter;
            }



            IQueryable<Student> StudentIQ = from s in _context.Student
                                            select s;
            if (!string.IsNullOrEmpty(searchString))
            {   
                //contains如果是IEnumerable集合调用，则会使用.NetCore实现，并且任何.Net Core 和.Net Framework都会区分大小写，需要都改成大写比较：LastName.ToUpper().Contains(searchString.ToUpper())。
                //如果在IQueryable对象调用，则会使用数据库实现
                StudentIQ = StudentIQ.Where(m => m.LastName.Contains(searchString)
                                        || m.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":   StudentIQ = StudentIQ.OrderByDescending(s => s.LastName); break;
                case "Date":        StudentIQ = StudentIQ.OrderBy(s => s.EnrollmentDate);break;
                case "date_desc":   StudentIQ = StudentIQ.OrderByDescending(s=>s.EnrollmentDate);break;
                default:            StudentIQ = StudentIQ.OrderBy(s=>s.LastName);break;
            }
            //创建或修改 IQueryable 时，不会向数据库发送任何查询。 将 IQueryable 对象转换成集合后才能执行查询。
            //Student = await StudentIQ.AsNoTracking().ToListAsync();

            int pageSize = 3;
            Student = await PaginatedList<Student>.CreateAsync(StudentIQ.AsNoTracking(), pageIndex ?? 1,pageSize);
        }

        public string NameSort { get; set; }

        public string DateSort { get; set; }

        public string CurrentFilter { get; set; }

        public string CurrentSort { get; set; }


    }
}
