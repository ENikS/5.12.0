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
        /// <summary>
        /// Tries to resolve a generic class that at registration, is open and contains an array property of the generic type.
        /// </summary>
        /// <remarks>See Bug 3849</remarks>
        [TestMethod]
        public void ResolveConfiguredGenericType()
        {
            // Arrange
            Container.RegisterType(typeof(GenericArrayPropertyDependency<>), "testing", new InjectionProperty("Stuff"))
                     .RegisterInstance<string>("first", "first")
                     .RegisterInstance<string>("second", "second");

            // Act
            var result = Container.Resolve<GenericArrayPropertyDependency<string>>("testing");

            // Validate
            CollectionAssert.AreEquivalent(new[] { "first", "second" }, result.Stuff);
        }

        [TestMethod]
#if V4
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
#else
        [ExpectedException(typeof(ResolutionFailedException))]
#endif
        public void WhenResolvingAnOpenGenericType()
        {
            Container.Resolve(typeof(List<>));
        }

        /// <summary>
        /// Sample from Unit test cases.
        /// modified for WithLifetime.
        /// pass
        /// </summary>
        [TestMethod]
        public void CanRegisterGenericTypesAndResolveThem()
        {
            // Arrange
            IDictionary<string, string> myDict = new Dictionary<string, string>();

            myDict.Add("One", "two");
            myDict.Add("Two", "three");

            Container.RegisterInstance(myDict);
            Container.RegisterType(typeof(IDictionary<,>), typeof(Dictionary<,>), new InjectionConstructor());

            // Act
            IDictionary<string, string> result = Container.Resolve<IDictionary<string, string>>();

            // Validate
            Assert.AreSame(myDict, result);
        }

        /// <summary>
        /// Sample from Unit test cases.
        /// </summary>
        [TestMethod]
        public void CanSpecializeGenericsViaTypeMappings()
        {
            // Arrange
            Container.RegisterType(typeof(IRepository<>), typeof(MockRespository<>))
                     .RegisterType<IRepository<Service>, FooRepository>();

            // Act
            IRepository<string> generalResult = Container.Resolve<IRepository<string>>();
            IRepository<Service> specializedResult = Container.Resolve<IRepository<Service>>();

            // Validate
            Assert.IsInstanceOfType(generalResult, typeof(MockRespository<string>));
            Assert.IsInstanceOfType(specializedResult, typeof(FooRepository));
        }

        /// <summary>
        /// Using List of int type. Pass
        /// WithLifetime passed is null.
        /// </summary>
        [TestMethod]
        public void NoLifetimeSpecified()
        {
            // Arrange
            var myList = new List<int>();

            Container.RegisterInstance<IList<int>>(myList)
                     .RegisterType<List<int>>();

            // Act
            var result = Container.Resolve<IList<int>>();

            // Validate
            Assert.AreSame(myList, result);
        }

        /// <summary>
        /// check mapping with generics
        /// </summary>
        [TestMethod]
        public void TypeMappingWithExternallyControlled()
        {
            // Arrange
            Container.RegisterInstance("Test String")
                     .RegisterType(typeof(IFoo<>), typeof(Foo<>), new ContainerControlledLifetimeManager());

            // Act
            IFoo<string> result = Container.Resolve<IFoo<string>>();

            // Validate
            Assert.IsInstanceOfType(result, typeof(Foo<string>));
        }

        /// <summary>
        /// Using List of string type.
        /// Passes if WithLifetime passed is null. Pass
        /// </summary>
        [TestMethod]
        public void ListOfString()
        {
            // Arrange
            var myList = new List<string>();
            Container.RegisterInstance<IList<string>>(myList)
                     .RegisterType<List<string>>();

            // Act
            IList<string> result = Container.Resolve<IList<string>>();

            // Validate
            Assert.AreSame(myList, result);
        }

        /// <summary>
        /// Using List of object type.
        /// Passes if WithLifetime passed is null. Pass
        /// </summary>
        [TestMethod]
        public void ListOfObjectType()
        {
            // Arrange
            var myList = new List<Service>();

            Container.RegisterInstance(myList)
                     .RegisterType<IList<Service>, List<Service>>();

            // Act
            var result = Container.Resolve<IList<Service>>();

            // Validate
            Assert.IsInstanceOfType(result, typeof(List<Service>));
        }

        /// <summary>
        /// have implemented constructor injection of generic type. Pass
        /// </summary>
        [TestMethod]
        public void ImplementConstructorInjection()
        {
            // Arrange
            Refer<int> myRefer = new Refer<int>();
            myRefer.Str = "HiHello";
            Container.RegisterInstance<Refer<int>>(myRefer)
                     .RegisterType<Refer<int>>();

            // Act
            Refer<int> result = Container.Resolve<Refer<int>>();

            // Validate
            Assert.AreSame(myRefer, myRefer);
        }

        /// <summary>
        /// have implemented constructor injection of generic type. passes
        /// </summary>
        [TestMethod]
        public void ConstrucotorInjectionGenerics()
        {
            // Arrange
            Refer<int> myRefer = new Refer<int>();
            myRefer.Str = "HiHello";
            Container.RegisterInstance<Refer<int>>(myRefer)
                     .RegisterType<IRepository<int>, MockRespository<int>>();

            // Act
            IRepository<int> result = Container.Resolve<IRepository<int>>();

            // Validate
            Assert.IsInstanceOfType(result, typeof(IRepository<int>));
        }

        /// <summary>
        /// Passing a generic class as parameter to List which is generic
        /// </summary>
        [TestMethod]
        public void GenericStack()
        {
            // Arrange
            Stack<Service> obj = new Stack<Service>();
            Container.RegisterInstance<Stack<Service>>(obj);

            // Act
            Stack<Service> obj1 = Container.Resolve<Stack<Service>>();

            // Validate
            Assert.AreSame(obj1, obj);
        }

        [TestMethod]
        public void CheckPropInjection()
        {
            // Arrange
            Container.RegisterType<IRepository<int>, MockRespository<int>>();

            // Act
            IRepository<int> result = Container.Resolve<IRepository<int>>();

            // Validate
            Assert.IsNotNull(result);
        }

        [TestMethod]
#if V4
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
#else
        [ExpectedException(typeof(InvalidOperationException))]
#endif
        public void UserExceptionIsNotWrappadInResolutionFailed()
        {
            // Arrange
            Container.RegisterType<IService, Service>("1");
            Container.RegisterFactory<IService>("2", c => { throw new InvalidOperationException(); });

            // Act
            var array = Container.Resolve<IEnumerable<IService>>().ToArray();

            Assert.Fail("Should throw at the line above");
        }
    }
}
