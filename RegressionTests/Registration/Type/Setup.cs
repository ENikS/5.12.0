using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Registrations
{
    [TestClass]
    public partial class RegisterType
    {
        protected const string Name = "name";
        protected IUnityContainer Container;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Container = new UnityContainer();
            Container.RegisterInstance(Name);
        }

        #region Test Data

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

            public TypeWithAmbiguousCtors(object first)
            {
                Signature = Two;
            }

            public TypeWithAmbiguousCtors(Type first, Type second, Type third)
            {
                Signature = Three;
            }

            public TypeWithAmbiguousCtors(string first, string second, string third)
            {
                Signature = Four;
            }

            public TypeWithAmbiguousCtors(string first, [Dependency(Five)] string second, IUnityContainer third)
            {
                Signature = Five;
            }

            [Dependency]
            public IUnityContainer Container { get; set; }
        }

        public interface IFoo<TEntity>
        {
            TEntity Value { get; }
        }

        public class Foo<TEntity> : IFoo<TEntity>
        {
            public Foo()
            {
            }

            public Foo(TEntity value)
            {
                Value = value;
            }

            public TEntity Value { get; }
        }

        public interface ILogger
        {
        }

        public class MockLogger : ILogger
        {
        }
        
        public class OtherLogger : ILogger
        {
        }

        public interface IService
        {
        }

        public class Service : IService, IDisposable
        {
            public string ID { get; } = Guid.NewGuid().ToString();

            public static int Instances = 0;

            public Service()
            {
                Interlocked.Increment(ref Instances);
            }

            public bool Disposed = false;
            public void Dispose()
            {
                Disposed = true;
            }
        }

        public interface IOtherService
        {
        }

        public class OtherService : IService, IOtherService, IDisposable
        {
            [InjectionConstructor]
            public OtherService()
            {

            }

            public OtherService(IUnityContainer container)
            {

            }


            public bool Disposed = false;
            public void Dispose()
            {
                Disposed = true;
            }
        }
    
        #endregion
    }

    [TestClass]
    public partial class RegisterType_Diagnostic : RegisterType
    {
        [TestInitialize]
#if V4
        public override void TestInitialize() => base.TestInitialize();
#else
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new Unity.Diagnostic());
        }
#endif
    }
}
