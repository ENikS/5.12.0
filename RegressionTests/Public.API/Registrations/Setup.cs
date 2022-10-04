using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Public.API
{
    [TestClass]
    public partial class IUnityContainer_Registrations
    {
        protected const string Name = "name";
        private string other = "other";
        protected IUnityContainer Container;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Container = new UnityContainer();
            Container.RegisterType<ILogger, MockLogger>();
            Container.RegisterType<ILogger, MockLogger>(Name);

            var service = new Service();
            Container.RegisterInstance<IService>(service);
            Container.RegisterInstance<IService>(Name, service);

            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>));
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>), Name);
        }

        #region Test Data

        public interface ILogger
        {
        }

        public class MockLogger : ILogger
        {
        }

        public class SpecialLogger : ILogger
        {
        }

        public interface IService
        {
        }

        public interface IGenericService<T>
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

        public interface ISpecialLogger
        {
        }

        public class MockLoggerWithCtor : ILogger
        {
            public MockLoggerWithCtor(string _)
            {

            }
        }

        public class SpecialLoggerWithCtor : ILogger, ISpecialLogger
        {
            public SpecialLoggerWithCtor(string _)
            {

            }
        }

        #endregion
    }
}
