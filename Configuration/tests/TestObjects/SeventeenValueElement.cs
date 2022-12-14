

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity;
using Unity.Injection;

namespace Microsoft.Practices.Unity.Configuration.Tests.TestObjects
{
    internal class SeventeenValueElement : ParameterValueElement
    {
        /// <summary>
        /// Generate an <see cref="ParameterValue"/> object
        /// that will be used to configure the container for a type registration.
        /// </summary>
        /// <param name="container">Container that is being configured. Supplied in order
        /// to let custom implementations retrieve services; do not configure the container
        /// directly in this method.</param>
        /// <param name="parameterType">Type of the </param>
        /// <returns></returns>
        public override ParameterValue GetInjectionParameterValue(IUnityContainer container, Type parameterType)
        {
            return new InjectionParameter(17);
        }
    }
}
