using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Lifetime;
#endif

namespace Public.API
{
    public partial class IUnityContainer_Registrations
    {
        [TestMethod]
        public void IsNotNull()
        {
            Assert.IsNotNull(Container.Registrations);
        }

        [TestMethod]
        public void ContainerIncludesItselfUnderRegistrations()
        {
            Assert.IsNotNull(Container.Registrations.Where(r => r.RegisteredType == typeof(IUnityContainer)).FirstOrDefault());
        }

        [TestMethod]
        public void NewRegistrationsShowUpInRegistrationsSequence()
        {
            Container.RegisterType<ILogger, MockLogger>("second");

            var registrations = (from r in Container.Registrations
                                 where r.RegisteredType == typeof(ILogger)
                                 select r).ToList();

            Assert.IsTrue(registrations.Any(r => r.Name == null));
            Assert.IsTrue(registrations.Any(r => r.Name == "second"));
        }

        [TestMethod]
        public void TypeMappingShowsUpInRegistrationsCorrectly()
        {
            Container.RegisterType<ILogger, MockLogger>();

            var registration =
                (from r in Container.Registrations where r.RegisteredType == typeof(ILogger) select r).First();
            Assert.AreSame(typeof(MockLogger), registration.MappedToType);
        }

        [TestMethod]
        public void NonMappingRegistrationShowsUpInRegistrationsSequence()
        {
            Container.RegisterType<MockLogger>();
            var registration = (from r in Container.Registrations
                                where r.RegisteredType == typeof(MockLogger)
                                select r).First();

            Assert.AreSame(registration.RegisteredType, registration.MappedToType);
            Assert.IsNull(registration.Name);
        }

        [TestMethod]
        public void RegistrationsInParentContainerAppearInChild()
        {
            Container.RegisterType<ILogger, MockLogger>();
            var child = Container.CreateChildContainer();

            var registration =
                (from r in child.Registrations where r.RegisteredType == typeof(ILogger) select r).First();

            Assert.AreSame(typeof(MockLogger), registration.MappedToType);
        }

        [TestMethod]
        public void RegistrationsInChildContainerDoNotAppearInParent()
        {
            var child = Container.CreateChildContainer()
                .RegisterType<object>("named");

            var childRegistration = child
                .Registrations
#if !V4
                .Cast<IContainerRegistration>()
#endif
                .Where(r => r.RegisteredType == typeof(object)).First();
            var parentRegistration = Container
                .Registrations
#if !V4
                .Cast<IContainerRegistration>()
#endif
                .Where(r => r.RegisteredType == typeof(object)).FirstOrDefault();

            Assert.IsNull(parentRegistration);
            Assert.IsNotNull(childRegistration);
        }

        [TestMethod]
        public void DuplicateRegistrationsInParentAndChildOnlyShowUpOnceInChild()
        {
            Container.RegisterType<ILogger, MockLogger>("one");

            var child = Container.CreateChildContainer()
                .RegisterType<ILogger, SpecialLogger>("one");

            var registrations = from r in child.Registrations
                                where r.RegisteredType == typeof(ILogger) && "one" == r.Name
                                select r;

            Assert.AreEqual(1, registrations.Count());

            var childRegistration = registrations.First();
            Assert.AreSame(typeof(SpecialLogger), childRegistration.MappedToType);
            Assert.AreEqual("one", childRegistration.Name);
        }

        [TestMethod]
        public void WhenRegistrationsAreRetrievedFromAContainer()
        {
            Container.RegisterType<ILogger, MockLoggerWithCtor>(new InjectionConstructor("default"));
            Container.RegisterType<ILogger, MockLoggerWithCtor>("foo", new InjectionConstructor("foo"));

            var registrations = Container.Registrations;


            var @default = registrations
#if !V4
                .Cast<IContainerRegistration>()
#endif
                .FirstOrDefault(c => c.Name != null && c.RegisteredType == typeof(ILogger));

            Assert.IsNotNull(@default);
            Assert.AreEqual(typeof(MockLoggerWithCtor), @default.MappedToType);

            var foo = registrations
#if !V4
                .Cast<IContainerRegistration>()
#endif
                .SingleOrDefault(c => c.Name == "foo");

            Assert.IsNotNull(foo);
            Assert.AreEqual(typeof(MockLoggerWithCtor), @default.MappedToType);
        }

        [TestMethod]
        public void WhenRegistrationsAreRetrievedFromANestedContainer()
        {
            Container.RegisterType<ILogger, MockLoggerWithCtor>(new InjectionConstructor("default"));
            Container.RegisterType<ILogger, MockLoggerWithCtor>("foo", new InjectionConstructor("foo"));

            var child = Container.CreateChildContainer();

            child.RegisterType<ISpecialLogger, SpecialLoggerWithCtor>(new InjectionConstructor("default"));
            child.RegisterType<ISpecialLogger, SpecialLoggerWithCtor>("another", new InjectionConstructor("another"));

            var registrations = Container.Registrations;

            var mappedCount = child.Registrations.Where(c => c.MappedToType == typeof(SpecialLoggerWithCtor)).Count();

            Assert.AreEqual(2, mappedCount);
        }

        [TestMethod]
        public void WhenRegistrationsAreRetrievedFromAContainerByLifeTimeManager()
        {
            Container.RegisterType<ILogger, MockLoggerWithCtor>(new PerResolveLifetimeManager(), new InjectionConstructor("default"));
            Container.RegisterType<ILogger, MockLoggerWithCtor>("foo", new PerResolveLifetimeManager(), new InjectionConstructor("foo"));

            var registrations = Container.Registrations;

            var count = registrations.Where(c => c.LifetimeManager?.GetType() == typeof(PerResolveLifetimeManager)).Count();

            Assert.AreEqual(2, count);
        }

    }
}
