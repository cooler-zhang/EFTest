using System.Web.Mvc;

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
    }
}