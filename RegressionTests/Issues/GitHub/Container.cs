using Microsoft.VisualStudio.TestTools.UnitTesting;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Lifetime;
#endif

namespace Issues
{
    public partial class GitHub
    {
#if !NET45
        [TestMethod]
        // https://github.com/unitycontainer/container/issues/140
        public void Issue_Container_140()
        {
            // Setup
            var noOverride = "default";
            var parOverride = "custom-via-parameteroverride";
            var depOverride = "custom-via-dependencyoverride";

            Container.RegisterType<Foo>(new InjectionConstructor(noOverride));
            // Act
            var defaultValue = Container.Resolve<Foo>().ToString();
            var depValue = Container.Resolve<Foo>(Override.Dependency<string>(depOverride)).ToString();
            var parValue = Container.Resolve<Foo>(Override.Parameter<string>("dependency", parOverride))
                                       .ToString();
            // Verify
            Assert.AreSame(noOverride, defaultValue);
            Assert.AreSame(parOverride, parValue);
            Assert.AreSame(depOverride, depValue);
        }
#endif

        [TestMethod]
        // https://github.com/unitycontainer/container/issues/136
        public void Issue_Container_136()
        {
            // Setup
            Container.RegisterType<IAnimal, Cat>();

            var child = Container.CreateChildContainer()
                                 .RegisterType<IAnimal, Dog>(); //this should overwrite previous registration

            // Act
            var zoo = child.Resolve<Zoo>();
            var animal = zoo.GetAnimal();

            // Verify
            Assert.IsNotNull(zoo);
            Assert.IsNotNull(animal);
            Assert.IsInstanceOfType(animal, typeof(Dog));
        }

        [TestMethod]
        // https://github.com/unitycontainer/container/issues/136
        public void Issue_Container_136_Ctor()
        {
            // Setup
            Container.RegisterType<IAnimal, Cat>();

            var child = Container.CreateChildContainer()
                                 .RegisterType<IAnimal, Dog>(); //this should overwrite previous registration
            // Act
            var zoo = child.Resolve<Zoo>();
            var animal = zoo.GetAnimal();

            // Verify
            Assert.IsNotNull(zoo);
            Assert.IsNotNull(animal);
            Assert.IsInstanceOfType(animal, typeof(Dog));
        }

#if !NET45
        [TestMethod]
        // https://github.com/unitycontainer/container/issues/129
        public void Issue_Container_129()
        {
            var config = "production.sqlite";

            // Setup
            Container.RegisterType<IProctRepository, ProctRepository>("DEBUG");
            Container.RegisterType<IProctRepository, ProctRepository>("PROD", new InjectionConstructor(config));

            // Act
            var ur = Container.Resolve<ProctRepository>();
            var qa = Container.Resolve<IProctRepository>("DEBUG");
            var prod = Container.Resolve<IProctRepository>("PROD");

            // Verify
            Assert.AreEqual(ur.Value, "default.sqlite");
            Assert.AreEqual(prod.Value, config);
            Assert.AreNotEqual(qa.Value, config);
        }

        [TestMethod]
        // https://github.com/unitycontainer/container/issues/67
        public void Issue_Container_67()
        {
            Container.RegisterType<ILogger, MockLogger>(new TransientLifetimeManager());

            var child = Container.CreateChildContainer();

            child.RegisterType<OtherService>(new TransientLifetimeManager());

            Assert.IsTrue(child.IsRegistered<ILogger>());
            Assert.IsFalse(child.IsRegistered<MockLogger>());
            Assert.IsTrue(child.IsRegistered<OtherService>());

            Container.RegisterType<IOtherService, OtherService>();

            child = child.CreateChildContainer();

            Assert.IsTrue(child.IsRegistered<ILogger>());
            Assert.IsFalse(child.IsRegistered<MockLogger>());
            Assert.IsTrue(child.IsRegistered<IOtherService>());
            Assert.IsTrue(child.IsRegistered<OtherService>());
        }
#endif
    }
}
