using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime EnrollmentDate { get; set; }

        //Enrollments是导航属性，例如与ID=1的Student相关的所有Enrollment实体集合。简单的理解就是Enrollment实体中的StundentID,是本类的外键。
        //本实体主键被什么对象作为外键引用。有多条记录的时候，此属性类型必须是集合类型：List<T>,HashSet<T>,ICollection<T>[会自动创建哈希集合],
        public ICollection<Enrollment> Enrollments { get; set; }

        public string Secret { get; set; }
    }
}
