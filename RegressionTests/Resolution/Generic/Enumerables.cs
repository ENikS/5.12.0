using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Lifetime;
#endif

namespace Resolution
{
    public partial class Generics
    {
        [TestMethod]
        public void CanResolveOpenGenericCollections()
        {
            // Arrange
            Container.RegisterType(typeof(IService<>), typeof(ServiceA<>), "A")
                     .RegisterType(typeof(IService<>), typeof(ServiceB<>), "B");

            // Act
            List<IService<int>> result = Container.Resolve<IEnumerable<IService<int>>>().ToList();

            // Validate
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(svc => svc is ServiceA<int>));
            Assert.IsTrue(result.Any(svc => svc is ServiceB<int>));
        }

        [TestMethod]
        public void CanResolveStructConstraintsCollections()
        {
            // Arrange
            Container.RegisterType(typeof(IService<>), typeof(ServiceA<>), "A")
                     .RegisterType(typeof(IService<>), typeof(ServiceB<>), "B")
                     .RegisterType(typeof(IService<>), typeof(ServiceStruct<>), "Struct");

            // Act
            var result = Container.Resolve<IEnumerable<IService<int>>>().ToList();
            List<IService<string>> constrainedResult = Container.Resolve<IEnumerable<IService<string>>>().ToList();

            // Validate
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result.Any(svc => svc is ServiceA<int>));
            Assert.IsTrue(result.Any(svc => svc is ServiceB<int>));
            Assert.IsTrue(result.Any(svc => svc is ServiceStruct<int>));

            Assert.AreEqual(2, constrainedResult.Count);
            Assert.IsTrue(constrainedResult.Any(svc => svc is ServiceA<string>));
            Assert.IsTrue(constrainedResult.Any(svc => svc is ServiceB<string>));
        }

        [TestMethod]
        public void CanResolveClassConstraintsCollections()
        {
            // Arrange
            Container.RegisterType(typeof(IService<>), typeof(ServiceA<>), "A")
                     .RegisterType(typeof(IService<>), typeof(ServiceB<>), "B")
                     .RegisterType(typeof(IService<>), typeof(ServiceClass<>), "Class");

            // Act
            List<IService<string>> result = Container.Resolve<IEnumerable<IService<string>>>().ToList();
            List<IService<int>> constrainedResult = Container.Resolve<IEnumerable<IService<int>>>().ToList();

            // Validate
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result.Any(svc => svc is ServiceA<string>));
            Assert.IsTrue(result.Any(svc => svc is ServiceB<string>));
            Assert.IsTrue(result.Any(svc => svc is ServiceClass<string>));

            Assert.AreEqual(2, constrainedResult.Count);
            Assert.IsTrue(constrainedResult.Any(svc => svc is ServiceA<int>));
            Assert.IsTrue(constrainedResult.Any(svc => svc is ServiceB<int>));
        }

        [TestMethod]
        public void CanResolveDefaultCtorConstraintsCollections()
        {
            // Arrange
            Container.RegisterType(typeof(IService<>), typeof(ServiceA<>), "A")
                     .RegisterType(typeof(IService<>), typeof(ServiceB<>), "B")
                     .RegisterType(typeof(IService<>), typeof(ServiceNewConstraint<>), "NewConstraint");

            // Act
            List<IService<int>> result = Container.Resolve<IEnumerable<IService<int>>>().ToList();
            List<IService<TypeWithNoPublicNoArgCtors>> constrainedResult = Container.Resolve<IEnumerable<IService<TypeWithNoPublicNoArgCtors>>>().ToList();

            // Validate
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result.Any(svc => svc is ServiceA<int>));
            Assert.IsTrue(result.Any(svc => svc is ServiceB<int>));
            Assert.IsTrue(result.Any(svc => svc is ServiceNewConstraint<int>));

            Assert.AreEqual(2, constrainedResult.Count);
            Assert.IsTrue(constrainedResult.Any(svc => svc is ServiceA<TypeWithNoPublicNoArgCtors>));
            Assert.IsTrue(constrainedResult.Any(svc => svc is ServiceB<TypeWithNoPublicNoArgCtors>));
        }

        [TestMethod]
        public void CanResolveInterfaceConstraintsCollections()
        {
            // Arrange
            Container.RegisterType(typeof(IService<>), typeof(ServiceA<>), "A")
                     .RegisterType(typeof(IService<>), typeof(ServiceB<>), "B")
                     .RegisterType(typeof(IService<>), typeof(ServiceInterfaceConstraint<>), "InterfaceConstraint");

            // Act
            List<IService<string>> result = Container.Resolve<IEnumerable<IService<string>>>().ToList();
            List<IService<int>> constrainedResult = Container.Resolve<IEnumerable<IService<int>>>().ToList();

            // Validate
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result.Any(svc => svc is ServiceA<string>));
            Assert.IsTrue(result.Any(svc => svc is ServiceB<string>));
            Assert.IsTrue(result.Any(svc => svc is ServiceInterfaceConstraint<string>));

            Assert.AreEqual(2, constrainedResult.Count);
            Assert.IsTrue(constrainedResult.Any(svc => svc is ServiceA<int>));
            Assert.IsTrue(constrainedResult.Any(svc => svc is ServiceB<int>));
        }
    }
}
