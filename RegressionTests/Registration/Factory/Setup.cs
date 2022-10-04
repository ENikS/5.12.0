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
    public partial class RegisterFactory
    {
        protected const string Name = "name";
        protected IUnityContainer Container;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Container = new UnityContainer();
        }

        #region Test Data

        public interface IFoo<T> { }
        public class Foo<T> : IFoo<T> { }

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

        #endregion
    }
}
