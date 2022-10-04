using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;
using Unity.Injection;

namespace Resolution
{
    public partial class Lazy
    {
        [TestMethod]
        public void Enumerable()
        {
            // Setup
            Container.RegisterType<IService, Service>("1");
            Container.RegisterType<IService, Service>("2");
            Container.RegisterType<IService, Service>("3");
            Container.RegisterType<IService, OtherService>();
            Service.Instances = 0;

            // Act
            var lazy = Container.Resolve<Lazy<IEnumerable<IService>>>();

            // Verify
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(lazy);
            Assert.IsNotNull(lazy.Value);

            var array = lazy.Value.ToArray();
            Assert.IsNotNull(array);
            Assert.AreEqual(3, Service.Instances);
            Assert.AreEqual(4, array.Length);
        }
    }
}
