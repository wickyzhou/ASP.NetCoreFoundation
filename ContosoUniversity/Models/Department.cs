using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }

        [StringLength(50,MinimumLength =3)]
        public String Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName ="money")]
        public decimal Budget { get; set; }

        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        [Display(Name="Start Date")]
        public DateTime StartDate { get; set; }

        public int? InstructorID { get; set; }  //一个系可以没有老师（临时招聘）

        public Instructor Administrator { get; set; } //管理员 只能是一个老师

        public ICollection<Course> Courses { get; set; }    //一个系可以有多门课程
    }
}
