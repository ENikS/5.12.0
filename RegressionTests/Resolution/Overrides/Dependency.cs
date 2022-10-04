using Microsoft.VisualStudio.TestTools.UnitTesting;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Resolution;
#endif

namespace Resolution
{
    public partial class Overrides
    {
        [TestMethod]
        public void NamedInstance()
        {
            // Arrange
            Container.RegisterInstance<IFoo>(new Foo2())
                     .RegisterInstance<IFoo>(Name, new Foo1());

            // Act / Validate
            Assert.IsInstanceOfType(Container.Resolve<IFoo>(), typeof(Foo2));
            Assert.IsInstanceOfType(Container.Resolve<IFoo>(Name), typeof(Foo1));
        }

        [TestMethod]
        public void NamedFactory()
        {
            // Arrange
            Container.RegisterFactory<IFoo>((c, t, n) => new Foo2())
                     .RegisterFactory<IFoo>(Name, (c, t, n) => new Foo1());

            // Act / Validate
            Assert.IsInstanceOfType(Container.Resolve<IFoo>(), typeof(Foo2));
            Assert.IsInstanceOfType(Container.Resolve<IFoo>(Name), typeof(Foo1));
        }

        [TestMethod]
        public void DependencyOverrideOccursEverywhereTypeMatches()
        {
            // Setup
            Container
                .RegisterType<ObjectThatDependsOnSimpleObject>(new InjectionProperty("OtherTestObject"))
                .RegisterType<SimpleTestObject>(new InjectionConstructor());

            // Act
            var overrideValue = new SimpleTestObject(15); // arbitrary value

            var result = Container.Resolve<ObjectThatDependsOnSimpleObject>(
                new DependencyOverride<SimpleTestObject>(overrideValue));

            // Verify
            Assert.AreSame(overrideValue, result.TestObject);
            Assert.AreSame(overrideValue, result.OtherTestObject);
        }

        [TestMethod]
        public void CanOverridePropertyValueWithNullWithExplicitInjectionParameter()
        {
            // Setup
            Container
                .RegisterType<ObjectTakingASomething>(new InjectionProperty("MySomething"))
                .RegisterType<IService, Service1>()
                .RegisterType<IService, Service2>("other");

            // Act
            var result = Container.Resolve<ObjectTakingASomething>(
                Override.Property(nameof(ObjectTakingASomething.MySomething), Inject.Parameter<IService>(null))
                        .OnType<ObjectTakingASomething>());

            // Verify
            Assert.IsNull(result.MySomething);
        }

        [TestMethod]
        public void InjectedPropertyWithDependencyOverride()
        {
            // Setup
            var noOverride = "default";
            var depOverride = "custom-via-override";
            Container.RegisterType<TestType>(new InjectionConstructor(), 
                                             new InjectionProperty(nameof(TestType.DependencyProperty), noOverride));

            // Act
            var theType = Container.Resolve<TestType>();
            var defaultValue = theType.DependencyProperty;

            var depValue  = Container.Resolve<TestType>(Override.Dependency<string>(depOverride)).DependencyProperty;
            var propValue = Container.Resolve<TestType>(Override.Property(nameof(TestType.DependencyProperty), depOverride)).DependencyProperty;
            
            // Verify
            Assert.AreSame(noOverride, defaultValue);
            Assert.AreSame(depOverride, depValue);
            Assert.AreSame(depOverride, propValue);
        }
    }
}
