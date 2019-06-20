using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Views
{
    public class CourseViewModel
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public string DepartmentName { get; set; }
        public double MoneySecurity { get; set; }
        public DateTime ModifyTime { get; set; }
        public DateTime ModifyDate { get; set; }

    }
}
