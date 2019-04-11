using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest
{
    public class StudentService
    {
        private readonly ClassRepository _classRepository;
        private readonly StudentRepository _studentRepository;

        public StudentService(ClassRepository classRepository, StudentRepository studentRepository)
        {
            this._classRepository = classRepository;
            this._studentRepository = studentRepository;
        }

        public void CreateStudent()
        {
            var class1 = _classRepository.Find(1);
            var student = new StudentEntity();
            student.Name = "张三";
            student.Class = class1;
            _studentRepository.Create(student);
        }
    }
}
