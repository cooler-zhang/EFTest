using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace EFTest
{
    public class StudentRepository
    {
        public readonly MyDbContext _ctx;

        public StudentRepository(MyDbContext ctx)
        {
            this._ctx = ContainerManager.Current.Resolve<MyDbContext>();
        }

        public StudentEntity Find(int id)
        {
            return _ctx.Students.Find(id);
        }

        public void Create(StudentEntity student)
        {
            _ctx.Students.Add(student);
            _ctx.SaveChanges();
        }

        public void Modified(StudentEntity student)
        {
            _ctx.SaveChanges();
        }

        public void Delete(int id)
        {
            var student = _ctx.Students.Where(a => a.Id == id).FirstOrDefault();
            _ctx.Students.Remove(student);
            _ctx.SaveChanges();
        }

        public void SeedData()
        {
            ClassEntity class1 = _ctx.Classes.Where(a => a.Id == 1).FirstOrDefault();
            StudentEntity student1 = class1.AddStudent("小明", 3, 300.1234m);
            StudentEntity student2 = class1.AddStudent("小花", 3, 500.1234m);
            StudentEntity student3 = class1.AddStudent("小光", 3, 600.1234m);
            _ctx.Students.AddRange(new List<StudentEntity>() { student1, student2, student3 });
            _ctx.SaveChanges();
        }
    }
}
