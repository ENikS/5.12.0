using Unity;

namespace Microsoft.Practices.Unity.Tests.TestObjects
{
    public class ObjectWithInjectionConstructor
    {
        private object constructorDependency;

        public ObjectWithInjectionConstructor(object constructorDependency)
        {
            this.constructorDependency = constructorDependency;
        }

        [InjectionConstructor]
        public ObjectWithInjectionConstructor(string s)
        {
            constructorDependency = s;
        }

        public object ConstructorDependency
        {
            get { return constructorDependency; }
        }
    }
}
