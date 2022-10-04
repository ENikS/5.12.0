using Microsoft.VisualStudio.TestTools.UnitTesting;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Resolution
{
    public partial class Overrides
    {
        [TestMethod]
        public void InjectedPropertyWithPropertyOverride()
        {
            // Setup
            var noOverride = "default";
            var propOverride = "custom-via-propertyoverride";

            Container.RegisterType<TestType>(new InjectionConstructor(), 
                                             new InjectionProperty(nameof(TestType.DependencyProperty), noOverride));
            // Act
            var defaultValue = Container.Resolve<TestType>().DependencyProperty;
            var propValue = Container.Resolve<TestType>(Override.Property(nameof(TestType.DependencyProperty), propOverride))
                                     .DependencyProperty;
            // Verify
            Assert.AreSame(noOverride, defaultValue);
            Assert.AreSame(propOverride, propValue);
        }
    }
}
