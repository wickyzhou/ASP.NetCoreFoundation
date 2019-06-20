using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class Course
    {   
        [Display(Name ="Number")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]//指定不需要数据库生成主键
        public int CourseID { get; set; }

        [StringLength(50,MinimumLength =3)]
        public string Title { get; set; }

        [Range(0,5)]
        public int Credits { get; set; }

        [ForeignKey("dd")]
        public int DepartmentID { get; set; }       //一对外键配置，不需要显示设定FK
        public Department Department { get; set; }

        [Column(TypeName ="money")]
        public double MoneySecurity { get; set; }

        public DateTime ModifyTime { get; set; }


        public DateTime ModifyDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<CourseAssignment> CourseAssignments { get; set; }
    }
}
