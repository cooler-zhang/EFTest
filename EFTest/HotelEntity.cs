using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest
{
    public class HotelEntity : ProductEntity
    {
        public HotelLevel Level { get; set; }
    }

    public enum HotelLevel
    {
        One = 0,
        Two = 5,
        Three = 10,
        Four = 15,
        Five = 20,
    }
}
