using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odoo.Net.Core
{
    class ModelInterceptor : IInterceptor
    {
        [ThreadStatic]
        static bool callingParent;
        internal static bool CallingParent { get => callingParent; set => callingParent = value; }

        /// <summary>
        /// 拦截器实体
        /// </summary>
        public readonly static ModelInterceptor Interceptor = new ModelInterceptor();

        void IInterceptor.Intercept(IInvocation invocation)
        {
            if (CallingParent)
            {
                CallingParent = false;
                invocation.Proceed();
                return;
            }
            var model = invocation.InvocationTarget as Model;
            var method = model.Meta.FindOverridMethod(invocation.Method);
            if (method != null)
            {
                invocation.ReturnValue = method.Invoke(invocation.Arguments);
                return;
            }
            invocation.Proceed();
        }
    }
}
