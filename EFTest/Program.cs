using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                //var tour = new TourEntity();
                //tour.Name = "Tour";
                //tour.Price = 100;
                //tour.BeginDate = DateTime.Now;
                //tour.EndDate = DateTime.Now.AddDays(10);
                //ctx.Products.Add(tour);

                //ctx.SaveChanges();

                //var tours = ctx.Tours.ToList();

                var products = ctx.Products.ToList();

                //ctx.UpdateStudent();
            }

            //new Program().CreateClass();

            //new Program().Class1AddStudent();

            //new Program().DeleteStudent1();

            using (MyDbContext ctx = new MyDbContext())
            {
                var students = ctx.Students.ToList();

                ctx.Students.Include("Class").ToArray();

                var studentsLookup = students.ToLookup(a => a.Class_Id);
                foreach (var item in studentsLookup)
                {
                    var key = item.Key;
                    var value = item.ToList();
                }
            }
        }

        public void CreateClass()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                ClassEntity class1 = new ClassEntity();
                class1.Name = "小班";

                ClassEntity class2 = new ClassEntity();
                class2.Name = "中班";

                ClassEntity class3 = new ClassEntity();
                class3.Name = "大班";

                ctx.Classes.AddRange(new List<ClassEntity>() { class1, class2, class3 });

                ctx.SaveChanges();
            }
        }

        public void Class1AddStudent()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                ClassEntity class1 = ctx.Classes.Where(a => a.Id == 1).FirstOrDefault();
                StudentEntity student1 = class1.AddStudent("小明", 3, 300.1234m);
                StudentEntity student2 = class1.AddStudent("小花", 3, 500.1234m);
                StudentEntity student3 = class1.AddStudent("小光", 3, 600.1234m);
                ctx.Students.AddRange(new List<StudentEntity>() { student1, student2, student3 });
                ctx.SaveChanges();
            }
        }

        public void DeleteStudent1()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                StudentEntity student = ctx.Students.Where(a => a.Id == 3).FirstOrDefault();
                ctx.Students.Remove(student);
                ctx.SaveChanges();
            }
        }

        public void DeleteClass1()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                ClassEntity class1 = ctx.Classes.Where(a => a.Id == 1).FirstOrDefault();
                ctx.Classes.Remove(class1);

                ctx.SaveChanges();
            }
        }
    }
}
