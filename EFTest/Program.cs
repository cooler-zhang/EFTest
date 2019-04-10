using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace EFTest
{
    class Program
    {
        private static UnityContainer _container = null;

        static void Main(string[] args)
        {
            InitIoC();

            Parallel.For(0, 1000, (index) =>
            {
                var testService = _container.Resolve<StudentService>();
                testService.CreateStudent();
            });
        }

        public static void InitIoC()
        {
            _container = new UnityContainer();
            _container.RegisterType<MyDbContext>(new PerCallContextLifeTimeManager());
            _container.RegisterType<ClassRepository>();
            _container.RegisterType<StudentRepository>();
            _container.RegisterType<StudentService>();
        }
    }
}
