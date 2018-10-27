using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest
{
    public class TourEntity : ProductEntity
    {
        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
