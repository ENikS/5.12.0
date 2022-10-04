using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity.Injection;
using Unity.Lifetime;
using Unity;
#endif

namespace Breaking.Changes
{
    [TestClass]
    public partial class BreakingChangesV4
    {
        protected const string Name = "name";
        protected IUnityContainer Container;
        protected readonly ContainerRegistrationComparer EqualityComparer = new ContainerRegistrationComparer();

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Container = new UnityContainer();
            Container.RegisterInstance(Name);
        }
    }

    #region Test Data

    public interface IService
    { }

    public class Service : IService
    { }

    public interface IService<T>
    {
        string Id { get; }
    }

    public class Service<T> : IService<T>
    {
        public string Id { get; } = Guid.NewGuid().ToString();

        public Service()
        {
        }

        public Service(object inject)
        {
            Id = $"Ctor injected with: { inject.GetHashCode() }";
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

#if NET46 || NET461
    public class ContainerRegistrationComparer : IEqualityComparer<IContainerRegistration>
    {
        public bool Equals(IContainerRegistration x, IContainerRegistration y)
        {
            return x.RegisteredType == y.RegisteredType && x.Name == y.Name;
        }

        public int GetHashCode(IContainerRegistration obj)
        {
            return obj.RegisteredType.GetHashCode() * 17 +
                    obj.Name?.GetHashCode() ?? 0;
        }
    }
#else
    public class ContainerRegistrationComparer : IEqualityComparer<ContainerRegistration>
    {
        public bool Equals(ContainerRegistration x, ContainerRegistration y)
        {
            return x.RegisteredType == y.RegisteredType && x.Name == y.Name;
        }

        public int GetHashCode(ContainerRegistration obj)
        {
            return obj.RegisteredType.GetHashCode() * 17 +
                   obj.Name?.GetHashCode() ?? 0;
        }
    }
#endif

    public class CtorWithAttributedParams
    {
        public const string DependencyName = "dependency_name";

        public string Signature { get; }

        /// <summary>
        /// Constructor with NAMED dependency
        /// </summary>
        /// <param name="first">Parameter marked for injection with named (Name == DependencyName) dependency </param>
        /// <param name="second">not impertant</param>
        public CtorWithAttributedParams([Dependency(DependencyName)] string first)
        {
            Signature = first;
        }
    }

    public class ObjectWithThreeProperties
    {
        [Dependency]
        public string Name { get; set; }

        public object Property { get; set; }

        [Dependency]
        public IUnityContainer Container { get; set; }
    }


    public interface IProctRepository
    {
        string Value { get; }
    }

    public class ProctRepository : IProctRepository
    {
        public string Value { get; }

        public ProctRepository(string base_name = "default.sqlite")
        {
            Value = base_name;
        }
    }

    public interface IFoo { }

    public interface IBar { }

    public class Bar : IBar { }

    public class Foo : IFoo
    {
        private readonly string _dependency;

        public Foo([Dependency] string dependency)
        {
            _dependency = dependency;
        }

        public override string ToString() => _dependency;
    }


    public interface ILogger
    {
    }

    public class MockLogger : ILogger
    {
    }

    public class Test_Class
    {
        public Test_Class(string field)
        {
            this.Field = field;
        }
        public string Field { get; }
    }
    public interface IInterface
    {
    }

    public class Class1 : IInterface
    {
    }

    public class Class2 : IInterface
    {
    }

    public class ATestClass
    {
        public ATestClass(IEnumerable<IInterface> interfaces)
        {
            Value = interfaces;
        }

        public IEnumerable<IInterface> Value { get; }
    }

    #endregion
}
