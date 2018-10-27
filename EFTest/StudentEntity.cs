using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest
{
    public class StudentEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public virtual ICollection<TeacherEntity> Teachers { get; set; }

        public virtual ClassEntity Class { get; set; }

        public int Class_Id { get; set; }

        public decimal StudyPayment { get; set; }
    }
}
