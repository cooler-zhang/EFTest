using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Unity.Lifetime;

namespace EFTest
{
    public class PerCallContextLifeTimeManager : LifetimeManager, ITypeLifetimeManager
    {
        private readonly string _key = string.Empty;

        public PerCallContextLifeTimeManager()
        {
            _key = string.Format("PerCallContextLifeTimeManager_{0}", Guid.NewGuid());
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
