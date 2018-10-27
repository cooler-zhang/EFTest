using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest
{
    public class ProductService
    {
        public ProductEntity CreateProduct(ProductEntity product)
        {
            using (var ctx = new MyDbContext())
            {
                product = ctx.Products.Add(product);
                ctx.SaveChanges();
                return product;
            }
        }
    }
}
