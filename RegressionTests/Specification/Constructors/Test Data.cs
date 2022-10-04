using Microsoft.VisualStudio.TestTools.UnitTesting;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    public partial class Constructors
    {

        #region Test Data

        public interface IRecInterface
        { }

        public class RecClass : IRecInterface
        {
            public RecClass()
            { }

            [InjectionConstructor]
            public RecClass(RecClass _) //1
            {
            }
        }

        public interface ILogger
        {
        }
        public class MockLogger : ILogger
        {
        }

        public interface ISomeCommonProperties
        {
            [Dependency]
            ILogger Logger { get; set; }

            [Dependency]
            object SyncObject { get; set; }
        }


        public class ObjectWithExplicitInterface : ISomeCommonProperties
        {
            private ILogger logger;
            private object syncObject;

            private object somethingElse;

            [Dependency]
            public object SomethingElse
            {
                get { return somethingElse; }
                set { somethingElse = value; }
            }

            [Dependency]
            ILogger ISomeCommonProperties.Logger
            {
                get { return logger; }
                set { logger = value; }
            }

            [Dependency]
            object ISomeCommonProperties.SyncObject
            {
                get { return syncObject; }
                set { syncObject = value; }
            }

            public void ValidateInterface()
            {
                Assert.IsNotNull(logger);
                Assert.IsNotNull(syncObject);
            }
        }

        public abstract class AbstractBase
        {
            private object baseProp;

            [Dependency]
            public object AbsBaseProp
            {
                get { return baseProp; }
                set { baseProp = value; }
            }

            public abstract void AbstractMethod();
        }

        public class ConcreteChild : AbstractBase
        {
            public override void AbstractMethod()
            {
            }

            [Dependency]
            public object ChildProp { get; set; }
        }

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

        public interface IService
        {
        }

        public class Service : IService
        {
            public Service() => Ctor = 1;

            [InjectionConstructor]
            public Service(object data)
            {
                Ctor = 2;
                Data = data?.ToString();
            }

            public Service(IUnityContainer container) => Ctor = 3;

            public Service(object[] data) => Ctor = 4;

            public int Ctor { get; }    // Constructor called 

            public Service(string data)
            {
                Data = data;
            }

            public string Data { get; }
        }

        public class CtorWithDependency
        {
            public CtorWithDependency([Dependency] string data)
            {
                Data = data;
            }

            public string Data { get; }
        }

        public class CtorWithNamedDependency
        {
            public CtorWithNamedDependency()
            {
                Data = null;
            }

            public CtorWithNamedDependency([Dependency("name")] string data)
            {
                Data = data;
            }

            public string Data { get; }
        }

        public class CtorWithOptionalDependency
        {
            public CtorWithOptionalDependency([OptionalDependency] string data = null)
            {
                Data = data;
            }

            public string Data { get; }
        }
        public class CtorWithOptionalNamedDependency
        {
            public CtorWithOptionalNamedDependency([OptionalDependency("name")] string data = null)
            {
                Data = data;
            }

            public string Data { get; }
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

        #endregion
    }
}
