using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace EFTest
{
    public class ContainerManager
    {
        public static UnityContainer Current { get; private set; }

        static ContainerManager()
        {
            Current = new UnityContainer();
        }
    }
}