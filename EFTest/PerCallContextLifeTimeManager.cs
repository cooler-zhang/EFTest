using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity.Lifetime;

namespace EFTest
{
    public class PerCallContextLifeTimeManager : LifetimeManager, ITypeLifetimeManager
    {
        private string _key => string.Format($"PerCallContextLifeTimeManager_{Name}_{Thread.CurrentThread.ManagedThreadId}");

        public string Name { get; set; }

        public PerCallContextLifeTimeManager(string name)
        {
            this.Name = name;
        }

        public override object GetValue(ILifetimeContainer container = null)
        {
            return CallContext.GetData(_key) ?? NoValue;
        }

        public override void SetValue(object newValue, ILifetimeContainer container = null)
        {
            CallContext.SetData(_key, newValue);
        }

        public override void RemoveValue(ILifetimeContainer container = null)
        {
            CallContext.FreeNamedDataSlot(_key);
        }

        protected override LifetimeManager OnCreateLifetimeManager()
        {
            return this;
        }
    }
}
