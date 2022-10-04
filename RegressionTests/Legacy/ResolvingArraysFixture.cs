using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Resolution;
#endif


namespace Unity.V4
{
    [TestClass]
    public class ResolvingArraysFixture
    {
        [TestMethod]
        public void ContainerCanResolveListOfT()
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType(typeof(List<>), new InjectionConstructor());

            var result = container.Resolve<List<EmptyClass>>();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ContainerReturnsEmptyArrayIfNoObjectsRegistered()
        {
            IUnityContainer container = new UnityContainer();
            List<object> results = new List<object>(container.ResolveAll<object>());

            Assert.IsNotNull(results);
            CollectionAssert.AreEqual(new object[0], results);
        }

        [TestMethod]
        public void ResolveAllof100()
        {
            IUnityContainer container = new UnityContainer();

            for (var i = 0; i < 100; i++)
            {
                container.RegisterInstance(i.ToString(), i);
            }

            var results  = container.Resolve<int[]>();

            Assert.IsNotNull(results);
            Assert.IsInstanceOfType(results, typeof(int[]));
        }

        [TestMethod]
        public void ResolveAll()
        {
            IUnityContainer root = new UnityContainer();
            object o1 = new object();
            object o2 = new object();

            root
                .RegisterInstance<object>("o1", o1)
                .RegisterInstance<object>(o1)
                .RegisterInstance<object>(root)
                .RegisterInstance<object>("o2", o2);

            var obj = "child";
            var child = root.CreateChildContainer()
                .RegisterInstance<object>(obj)
                .RegisterInstance<object>(o1)
                .RegisterInstance<object>("o2", obj);

            var results = root.Resolve<object[]>();
            var results1 = child.Resolve<object[]>();

            Assert.IsNotNull(results);
            Assert.IsNotNull(results1);
            Assert.IsInstanceOfType(results, typeof(object[]));
        }

        [TestMethod]
        public void ResolveAllReturnsRegisteredObjects()
        {
            IUnityContainer container = new UnityContainer();
            object o1 = "one";
            object o2 = "two";

            container
                .RegisterInstance<object>("o1", o1)
                .RegisterInstance<object>("o2", o2);

            List<object> results = new List<object>(container.ResolveAll<object>());

            CollectionAssert.AreEqual(new object[] { o1, o2 }, results);
        }

        [TestMethod]
        public void ResolveAllReturnsRegisteredObjectsForBaseClass()
        {
            IUnityContainer container = new UnityContainer();
            ILogger o1 = new MockLogger();
            ILogger o2 = new SpecialLogger();

            container
                .RegisterInstance<ILogger>("o1", o1)
                .RegisterInstance<ILogger>("o2", o2);

            List<ILogger> results = new List<ILogger>(container.ResolveAll<ILogger>());
            CollectionAssert.AreEqual(new ILogger[] { o1, o2 }, results);
        }

        public class InjectedObject
        {
            public readonly object InjectedValue;

            public InjectedObject(object injectedValue)
            {
                this.InjectedValue = injectedValue;
            }
        }

        public class EmptyClass
        {
        }


        #region Test Data

        public interface ILogger
        {
        }

        public class MockLogger : ILogger
        {
        }

        public class SpecialLogger : ILogger
        {
        }

        #endregion
    }
}
