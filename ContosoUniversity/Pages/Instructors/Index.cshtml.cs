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

namespace ContosoUniversity.Pages.Instructors
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.SchoolContext _context;

        public IndexModel(ContosoUniversity.SchoolContext context)
        {
            _context = context;
        }

        //public IList<Instructor> Instructor { get;set; }

        //public async Task OnGetAsync()
        //{
        //    Instructor = await _context.Instructors.ToListAsync();
        //}

        public InstructorIndexData Instructor { get; set; }

        public int InstructorID { get; set; }

        public int CourseID { get; set; }

        public async Task OnGetAsync(int? id, int? courseID)
        {
            Instructor = new InstructorIndexData();
            Instructor.Instructors = await _context.Instructors
                                    .Include(i => i.OfficeAssignment)
                                    .Include(i => i.CourseAssignments)
                                        .ThenInclude(i => i.Course)
                                            .ThenInclude(i => i.Department)
                                    // .Include(i => i.CourseAssignments)
                                    //    .ThenInclude(i => i.Course)
                                    //        .ThenInclude(i => i.Enrollments)
                                    //            .ThenInclude(i => i.Student)
                                    //.AsNoTracking()       取消显示全部加载，
                                    .OrderBy(i => i.LastName)
                                    .ToListAsync();

            /*   SELECT[i].[ID], [i].[FirstName], [i].[HireDate], [i].[LastName], [i.OfficeAssignment].[InstructorID], [i.OfficeAssignment].[Location]
                 FROM[Instructor] AS[i]
                   LEFT JOIN[OfficeAssignment] AS[i.OfficeAssignment] ON[i].[ID] = [i.OfficeAssignment].[InstructorID]
                 ORDER BY[i].[LastName], [i].[ID]

                 生成一下两个查询，效率感觉很低呀

                 SELECT[i.CourseAssignments].[CourseID], [i.CourseAssignments].[InstructorID], [c.Course].[CourseID], [c.Course].[Credits], [c.Course].[DepartmentID], [c.Course].[Title]
                 FROM[CourseAssignment] AS[i.CourseAssignments]
         I            NNER JOIN[Course] AS[c.Course] ON[i.CourseAssignments].[CourseID] = [c.Course].[CourseID]
                      INNER JOIN(
                                 SELECT DISTINCT [i0].[ID], [i0].[LastName]
                                  FROM [Instructor] AS [i0]
                                  LEFT JOIN [OfficeAssignment] AS[i.OfficeAssignment0] ON [i0].[ID] = [i.OfficeAssignment0].[InstructorID]
                                 ) AS[t] ON[i.CourseAssignments].[InstructorID] = [t].[ID]
                 ORDER BY[t].[LastName], [t].[ID]
         */
            //if (id != null)
            //{
            //    InstructorID = id.Value;
            //    Instructor instructor = Instructor.Instructors.Where(i => i.ID == id.Value).Single(); //集合仅包含一个选项的时候才能用，空或者多个都会报异常
            //    Instructor.Courses = instructor.CourseAssignments.Select(s => s.Course);
            //}
            //if (courseID != null)
            //{
            //    CourseID = courseID.Value;
            //    Instructor.Enrollments = Instructor.Courses.Where(x => x.CourseID == courseID).Single().Enrollments;
            //}

            if (id != null)
            {
                InstructorID = id.Value;
                Instructor instructor = Instructor.Instructors.Single(//直接在Single里面调用where条件
                    i => i.ID == id.Value);
                Instructor.Courses = instructor.CourseAssignments.Select(
                    s => s.Course);
            }

            //if (courseID != null)
            //{
            //    CourseID = courseID.Value;
            //    Instructor.Enrollments = Instructor.Courses.Single(
            //        x => x.CourseID == courseID).Enrollments;
            //} 用下列进行显示load加载


            if (courseID != null)
            {
                CourseID = courseID.Value;
                var selectedCourse = Instructor.Courses.Where(x => x.CourseID == courseID).Single();
                await _context.Entry(selectedCourse).Collection(x => x.Enrollments).LoadAsync();
                foreach (Enrollment enrollment in selectedCourse.Enrollments)
                {
                    await _context.Entry(enrollment).Reference(x => x.Student).LoadAsync();
                }
                Instructor.Enrollments = selectedCourse.Enrollments;
            }
        }
    }
}
