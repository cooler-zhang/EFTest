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
        private string Key => string.Format("CallContextLifeTimeManager_{0}_{1}", SqlHelper.ConnectionStringKey, _serviceName);

        //private string Key = string.Format("CallContextLifeTimeManager_{0}_{1}", SqlHelper.ConnectionStringKey, Guid.NewGuid());


        private readonly string _serviceName;

        public PerCallContextLifeTimeManager(string serviceName = null)
        {
            this._serviceName = string.IsNullOrWhiteSpace(serviceName) ? Guid.NewGuid().ToString() : serviceName;
        }

        public override object GetValue(ILifetimeContainer container = null)
        {
            return CallContext.LogicalGetData(Key) ?? NoValue;
        }

        public override void SetValue(object newValue, ILifetimeContainer container = null)
        {
            CallContext.LogicalSetData(Key, newValue);
        }

        public override void RemoveValue(ILifetimeContainer container = null)
        {
            CallContext.FreeNamedDataSlot(Key);
        }

        protected override LifetimeManager OnCreateLifetimeManager()
        {
            return this;
        }
    }

    public class SqlHelper
    {
        public static string ConnectionStringKey { get; set; } = "Key";
    }
}
