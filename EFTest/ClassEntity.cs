using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest
{
    public class ClassEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<StudentEntity> Students { get; set; }

        public StudentEntity AddStudent(string name, int age, decimal studyPayment)
        {
            StudentEntity student = new StudentEntity();
            student.Name = name;
            student.Age = age;
            student.StudyPayment = studyPayment;
            Students.Add(student);
            return student;
        }
    }
}
