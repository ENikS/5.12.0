using Microsoft.VisualStudio.TestTools.UnitTesting;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Resolution
{
    [TestClass]
    public partial class BuildUp
    {
        protected const string Name = "name";
        protected IUnityContainer Container;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Container = new UnityContainer();
        }

        #region Test Data

        public interface Interface1
        {
            [Dependency]
            object InterfaceProp
            {
                get;
                set;
            }
        }

        public class BaseStub1 : Interface1
        {
            private object baseProp;
            private object interfaceProp;

            [Dependency]
            public object BaseProp
            {
                get { return this.baseProp; }
                set { this.baseProp = value; }
            }

            public object InterfaceProp
            {
                get { return this.interfaceProp; }
                set { this.interfaceProp = value; }
            }
        }

        public class ChildStub1 : BaseStub1
        {
            private object childProp;

            [Dependency]
            public object ChildProp
            {
                get { return this.childProp; }
                set { this.childProp = value; }
            }
        }

        public class BuildUnmatchedObject2_PropertyDependencyClassStub1
        {
            private object myFirstObj;

            [Dependency]
            public object MyFirstObj
            {
                get { return myFirstObj; }
                set { myFirstObj = value; }
            }
        }

        public class BuildUnmatchedObject2__PropertyDependencyClassStub2
        {
            private object myFirstObj;
            private object mySecondObj;

            [Dependency]
            public object MyFirstObj
            {
                get { return myFirstObj; }
                set { myFirstObj = value; }
            }

            [Dependency]
            public object MySecondObj
            {
                get { return mySecondObj; }
                set { mySecondObj = value; }
            }
        }

        public class TypeWithAmbuguousAnnotations
        {
            public TypeWithAmbuguousAnnotations() => Ctor = 1;

            [InjectionConstructor]
            public TypeWithAmbuguousAnnotations(object arg) => Ctor = 2;

            public TypeWithAmbuguousAnnotations(IUnityContainer container) => Ctor = 3;

            [InjectionConstructor]
            public TypeWithAmbuguousAnnotations(object[] data) => Ctor = 4;

            public int Ctor { get; }    // Constructor called 

            [Dependency]
            public IUnityContainer Container { get; set; }
        }

        public class TypeWithAmbiguousCtors
        {
            public const string One = "1";
            public const string Two = "2";
            public const string Three = "3";
            public const string Four = "4";
            public const string Five = "5";

            public string Signature { get; }

            public TypeWithAmbiguousCtors()
            {
                Signature = One;
            }

            public TypeWithAmbiguousCtors(object first, object second)
            {
                Signature = Two;
            }

            public TypeWithAmbiguousCtors(IUnityContainer first, object second)
            {
                Signature = Three;
            }

            public TypeWithAmbiguousCtors(object first, IUnityContainer second)
            {
                Signature = Four;
            }

            [Dependency]
            public IUnityContainer Container { get; set; }
        }

        public interface IFooInterface
        {
            [Dependency]
            object InterfaceProp
            {
                get;
                set;
            }
        }

        public interface IFooInterface2
        {
            object InterfaceProp
            {
                get;
                set;
            }
        }

        public class BarClass : IFooInterface
        {
            public object InterfaceProp { get; set; }
        }

        public class BarClass2 : IFooInterface2
        {
            [Dependency]
            public object InterfaceProp { get; set; }
        }

        #endregion
    }
}
