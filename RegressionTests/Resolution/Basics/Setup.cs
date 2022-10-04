using Microsoft.VisualStudio.TestTools.UnitTesting;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Resolution
{
    [TestClass]
    public partial class Basics
    {
        protected const string Name = "name";
        protected IUnityContainer Container;

        [TestInitialize]
        public virtual void TestInitialize() => Container = new UnityContainer();

        #region Test Data

        public interface IFoo { }

        public class Foo : IFoo { }

        public class Foo1 : IFoo { }

        public class ObjectWithOneDependency
        {
            private readonly object inner;

            public ObjectWithOneDependency(object inner)
            {
                this.inner = inner;
            }

            public object InnerObject => inner;

            public void Validate()
            {
                Assert.IsNotNull(inner);
            }
        }

        public class ObjectWithTwoConstructorDependencies
        {
            private readonly ObjectWithOneDependency oneDep;

            public ObjectWithTwoConstructorDependencies(ObjectWithOneDependency oneDep)
            {
                this.oneDep = oneDep;
            }

            public ObjectWithOneDependency OneDep => oneDep;

            public void Validate()
            {
                Assert.IsNotNull(oneDep);
                oneDep.Validate();
            }
        }

        public class ClassWithMultipleConstructorParameters
        {
            public ClassWithMultipleConstructorParameters(object parameterA, object parameterB, IUnityContainer container)
            {
            }
        }

        #endregion
    }
}
