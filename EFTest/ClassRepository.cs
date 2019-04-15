using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace EFTest
{
    public class ClassRepository
    {
        private readonly MyDbContext _ctx;

        public ClassRepository(MyDbContext ctx)
        {
            this._ctx = ctx;
        }

        public ClassEntity Find(int id)
        {
            return _ctx.Classes.Find(id);
        }

        public void Delete(int id)
        {
            var class1 = _ctx.Classes.Where(a => a.Id == id).FirstOrDefault();
            _ctx.Classes.Remove(class1);
            _ctx.SaveChanges();
        }
    }
}
