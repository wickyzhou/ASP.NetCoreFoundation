using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }

        public int StudentID { get; set; }//外键格式1：导航属性名【Student】+导航属性类主键【ID】

        public int CourseID { get; set; }//外键格式2： 导航属性类的主键【Course 的主键是 CourceID】

        [DisplayFormat(NullDisplayText ="no grade")]
        public Grade? Grade { get; set; }


        //一个课程登记记录，只可能是一个学生和一门课程，所以此处的导航属性，只需要用到关联的类型Student和Cource；
        public Student Student { get; set; }

        public Course Course { get; set; }
    }

    public enum Grade
    {
        A,B,C,D,F
    }
}
