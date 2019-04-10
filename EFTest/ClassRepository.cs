using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void SeedData()
        {
            ClassEntity class1 = new ClassEntity();
            class1.Name = "小班";

            ClassEntity class2 = new ClassEntity();
            class2.Name = "中班";

            ClassEntity class3 = new ClassEntity();
            class3.Name = "大班";

            _ctx.Classes.AddRange(new List<ClassEntity>() { class1, class2, class3 });

            _ctx.SaveChanges();
        }
    }
}
