using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace Compiled
{

    [TestClass]
    public class Property : Unity.Specification.Diagnostic.Property.Validation.SpecificationTests
    {
        public override IUnityContainer GetContainer()
        {
            return new UnityContainer().AddExtension(new ForceCompillation())
                                       .AddExtension(new Diagnostic());
        }
    }
}

namespace Resolved
{

    [TestClass]
    public class Property : Unity.Specification.Diagnostic.Property.Validation.SpecificationTests
    {
        public override IUnityContainer GetContainer()
        {
            return new UnityContainer().AddExtension(new ForceActivation())
                                       .AddExtension(new Diagnostic());
        }
    }
}
