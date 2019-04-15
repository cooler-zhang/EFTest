using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Unity;

namespace EFTest
{
    public class StudentService
    {
        private readonly ClassRepository _classRepository;
        private readonly StudentRepository _studentRepository;

        public string ConnectionStringKey
        {
            get { return CallContext.LogicalGetData("ConnectionStringKey") as string; }
            set { if (VirtualCompanyIsValid(value)) CallContext.LogicalSetData("ConnectionStringKey", value); }
        }

        public StudentService(ClassRepository classRepository, StudentRepository studentRepository)
        {
            this._classRepository = classRepository;
            this._studentRepository = studentRepository;
        }

        public void CreateStudent()
        {
            var student = new StudentEntity();
            student.Name = "张三";
            student.Class = _classRepository.Find(1);
            Task.Run(() => { _studentRepository.Create(student); });
        }

        public void TaskSearch()
        {
            //var aa = ContainerManager.Current.Resolve<ClassRepository>().Find(1);
            //var bb = ContainerManager.Current.Resolve<StudentRepository>().Find(1);

            var context = System.Web.HttpContext.Current;

            Parallel.For(0, 10, (index) =>
            {
                CallContext.HostContext = context;
                var request = HttpContext.Current.Request;
            });

            var classTask = Task.Run(() =>
            {
                CallContext.HostContext = context;
                var request = HttpContext.Current.Request;
                var key = ConnectionStringKey;
                ContainerManager.Current.Resolve<ClassRepository>().Find(1);
            });
            var studentTask = Task.Run(() => { ContainerManager.Current.Resolve<StudentRepository>().Find(1); });

            Task.WaitAll(classTask, studentTask);
        }

        private void GetConnectionStringKey()
        {
            var current = HttpContext.Current;
            if (current == null && string.IsNullOrWhiteSpace(ConnectionStringKey))
            {
                throw new Exception(
                    "Unable to determine what the Connection String Key should be; please set it via SqlHelper.ConnectionStringKey (HttpContext.Current is null)");
            }

            if (current != null)
            {
                ConnectionStringKey = GetVirtualCompanyFromRequest(current.Request);
            }

            if (string.IsNullOrWhiteSpace(ConnectionStringKey))
            {
                throw new Exception(
                    "Unable to determine what the Connection String Key should be; please set it via SqlHelper.ConnectionStringKey");
            }
        }

        private Regex _virtualCompanyValidateRegex = new Regex("^\\d*$");
        private bool VirtualCompanyIsValid(string virtualCompany)
        {
            return String.IsNullOrEmpty(virtualCompany) ? false : !_virtualCompanyValidateRegex.IsMatch(virtualCompany);
        }

        public string GetVirtualCompanyFromRequest(HttpRequest request)
        {
            try
            {
                string virtualCompany = request.Url.Host.Split('.')[0];
                if (!VirtualCompanyIsValid(virtualCompany))
                {
                    virtualCompany = request.ServerVariables["HTTP_HOST"].Split('.')[0];
                }
                return virtualCompany;
            }
            catch
            {
                return ConnectionStringKey;
            }
        }
    }
}
