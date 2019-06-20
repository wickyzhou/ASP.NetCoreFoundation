using ContosoUniversity.Models;
using ContosoUniversity.Views;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Instructors
{
    public class InstructorCoursesPageModel : PageModel
    {
        public List<AssignedCourseData> AssignedCourseDataList;

        //初始化下面课程选中视图数据
        public void PopulateAssignedCourseData(SchoolContext context,Instructor instructor)
        {
            var allCourse = context.Courses;
            var instructorCourses = new HashSet<int>(instructor.CourseAssignments.Select(m => m.CourseID));//将课程分配表中的CourseID列添加到HashSet集合里面，值不重复的,用来判定某个课程是否存已经分配给某个老师了！！！
            AssignedCourseDataList = new List<AssignedCourseData>(); //实例化属性
            foreach (var course in allCourse)
            {
                AssignedCourseDataList.Add(new AssignedCourseData()
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseID)
                });
            }
        }

        public void UpdateInstructorCourses(SchoolContext context, string[] selectCourse, Instructor instructorToUpdate)
        {
            if (selectCourse==null)
            {
                instructorToUpdate.CourseAssignments = new List<CourseAssignment>();
                return;
            }
            var selectedCourseHS = new HashSet<string>(selectCourse);//界面上选中的课程ID???
            var instructorCourse = new HashSet<int>(instructorToUpdate.CourseAssignments.Select(m => m.CourseID));

            foreach (var course in context.Courses) //对课程表所有的课程做循环，因为这是最全的ID表
            {
                if (selectedCourseHS.Contains(course.CourseID.ToString())) // 当前CourseID已经被选中的情况（如果老师课程表不存在则增加记录，已存在就不做操作）
                {
                    if (!instructorCourse.Contains(course.CourseID))//现有的老师课程表里面不存在当前的课程ID,则需要添加一条记录到此表
                    {
                        instructorToUpdate.CourseAssignments.Add(new CourseAssignment() //添加语句
                        {
                            InstructorID = instructorToUpdate.ID,
                            CourseID = course.CourseID
                        });
                    }
                }
                else //如果没有此课程不在已选列表里面的情况（如果老师课程表已存在，则需要删除，不存在则不需要做操作）
                {
                    if (instructorCourse.Contains(course.CourseID))
                    {
                        CourseAssignment courseToRemove = instructorToUpdate.CourseAssignments.SingleOrDefault(i => i.CourseID==course.CourseID);
                        context.Remove(courseToRemove); //移除任何实体记录
                    }
                }
            }
        }
    }
}
