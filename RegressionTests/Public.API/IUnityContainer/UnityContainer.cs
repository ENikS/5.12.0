using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity.Injection;
using Unity.Lifetime;
using Unity;
#endif

namespace Public.API
{
    public partial class IUnityContainer_Extensions
    {
        #region IUnityContainer

        [Ignore]
        [TestMethod]
        public void ResolveTest()
        {
            //object Resolve(Type t, string name, params ResolverOverride[] resolverOverrides);
        }

        [Ignore]
        [TestMethod]
        public void ResolveAllTest()
        {
            //IEnumerable<object> ResolveAll(Type t, params ResolverOverride[] resolverOverrides);
        }

        [Ignore]
        [TestMethod]
        public void BuildUpTest()
        {
            //object BuildUp(Type t, object existing, string name, params ResolverOverride[] resolverOverrides);
        }

        [Ignore]
        [TestMethod]
        public void TeardownTest()
        {
            //void Teardown(object o);
        }

        [Ignore]
        [TestMethod]
        public void AddExtensionTest()
        {
            //IUnityContainer AddExtension(UnityContainerExtension extension);
        }

        [Ignore]
        [TestMethod]
        public void ConfigureTest()
        {
            //object Configure(Type configurationInterface);
        }

        [Ignore]
        [TestMethod]
        public void RemoveAllExtensionsTest()
        {
            //IUnityContainer RemoveAllExtensions();
        }

        [Ignore]
        [TestMethod]
        public void ParentTest()
        {
            //IUnityContainer Parent { get; }
        }

        [Ignore]
        [TestMethod]
        public void CreateChildContainerTest()
        {
            //IUnityContainer CreateChildContainer();
        }

        #endregion
    }
}
