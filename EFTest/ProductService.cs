using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest
{
    public class ProductService
    {
        private readonly MyDbContext _ctx;

        public ProductService(MyDbContext ctx)
        {
            this._ctx = ctx;
        }

        public ProductEntity CreateProduct(ProductEntity product)
        {
            product = _ctx.Products.Add(product);
            _ctx.SaveChanges();
            return product;
        }
    }
}
