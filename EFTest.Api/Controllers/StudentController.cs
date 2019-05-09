using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Unity;

namespace EFTest.Api.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentService _testService;

        public StudentController(StudentService testService)
        {
            this._testService = testService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return Content("Success");
        }

        [HttpGet]
        public ActionResult Create()
        {
            _testService.CreateStudent();
            return Content("Success");
        }

        [HttpGet]
        public ActionResult TaskSearch()
        {
            _testService.TaskSearch();
            return Content("Success");
        }

        [HttpGet]
        public ActionResult TestResolve()
        {
            Parallel.For(0, 100, (index) =>
            {
                var ctx = ContainerManager.Current.Resolve<MyDbContext>();
                var student = ctx.Students.Find(index);
                if (student != null)
                {
                    student.Age += index;
                    ctx.SaveChanges();
                }
                var classItem = ctx.Classes.Find(index);
                System.Diagnostics.Debug.WriteLine($"Index:{index} Thread:{Thread.CurrentThread.ManagedThreadId}");
            });

            return Content("Success");
        }

        [HttpGet]
        public ActionResult TestResolve1()
        {
            Parallel.For(0, 100, (index) =>
            {
                var repository = ContainerManager.Current.Resolve<StudentRepository>();
                var repository2 = ContainerManager.Current.Resolve<ClassRepository>();
                var student = repository.Find(index);
                if (student != null)
                {
                    student.Age += index;
                    repository.Modified(student);
                }
                var @class = repository2.Find(index);
                System.Diagnostics.Debug.WriteLine($"Index:{index} Thread:{Thread.CurrentThread.ManagedThreadId}");
            });

            return Content("Success");
        }

        [HttpGet]
        public ActionResult TestResolve2()
        {
            var repository = ContainerManager.Current.Resolve<StudentRepository>();
            var repository2 = ContainerManager.Current.Resolve<ClassRepository>();
            SqlHelper.ConnectionStringKey = "Key1";
            var repository3 = ContainerManager.Current.Resolve<StudentRepository>();
            var isEqualsRepository = repository == repository3;
            var isEqualsDbContext = repository._ctx == repository2._ctx;
            var isEqualsDbContext2 = repository._ctx == repository3._ctx;
            return Content("Success");
        }
    }
}