using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Issues
{
    [TestClass]
    public partial class GitHub
    {
        protected const string Name = "name";
        protected IUnityContainer Container;

        [TestInitialize]
        public virtual void TestInitialize() => Container = new UnityContainer();

        #region Test Data

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

        public class ObjectWithLotsOfDependencies
        {
            private ILogger ctorLogger;
            private ObjectWithOneDependency dep1;
            private ObjectWithTwoConstructorDependencies dep2;
            private ObjectWithTwoProperties dep3;

            public ObjectWithLotsOfDependencies(ILogger logger, ObjectWithOneDependency dep1)
            {
                this.ctorLogger = logger;
                this.dep1 = dep1;
            }

            [Dependency]
            public ObjectWithTwoConstructorDependencies Dep2
            {
                get { return dep2; }
                set { dep2 = value; }
            }

            [InjectionMethod]
            public void InjectMe(ObjectWithTwoProperties dep3)
            {
                this.dep3 = dep3;
            }

            public void Validate()
            {
                Assert.IsNotNull(ctorLogger);
                Assert.IsNotNull(dep1);
                Assert.IsNotNull(dep2);
                Assert.IsNotNull(dep3);

                dep1.Validate();
                dep2.Validate();
                dep3.Validate();
            }

            public ILogger CtorLogger
            {
                get { return ctorLogger; }
            }

            public ObjectWithOneDependency Dep1
            {
                get { return dep1; }
            }

            public ObjectWithTwoProperties Dep3
            {
                get { return dep3; }
            }
        }

        public class ObjectWithOneDependency
        {
            private object inner;

            public ObjectWithOneDependency(object inner)
            {
                this.inner = inner;
            }

            public object InnerObject
            {
                get { return inner; }
            }

            public void Validate()
            {
                Assert.IsNotNull(inner);
            }
        }

        public class ObjectWithTwoProperties
        {
            private object obj1;
            private object obj2;

            [Dependency]
            public object Obj1
            {
                get { return obj1; }
                set { obj1 = value; }
            }

            [Dependency]
            public object Obj2
            {
                get { return obj2; }
                set { obj2 = value; }
            }

            public void Validate()
            {
                Assert.IsNotNull(obj1);
                Assert.IsNotNull(obj2);
                Assert.AreNotSame(obj1, obj2);
            }
        }

        public class ObjectWithTwoConstructorDependencies
        {
            private ObjectWithOneDependency oneDep;

            public ObjectWithTwoConstructorDependencies(ObjectWithOneDependency oneDep)
            {
                this.oneDep = oneDep;
            }

            public ObjectWithOneDependency OneDep
            {
                get { return oneDep; }
            }

            public void Validate()
            {
                Assert.IsNotNull(oneDep);
                oneDep.Validate();
            }
        }

        public class Test_Class
        {
            public Test_Class(string field)
            {
                this.Field = field;
            }
            public string Field { get; }
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

        public interface IZoo
        {

            IAnimal GetAnimal();
        }

        public class Zoo : IZoo
        {
            private readonly IAnimal _animal;

            public Zoo(IAnimal animal)
            {
                _animal = animal;
            }


            public IAnimal GetAnimal()
            {
                return _animal;
            }
        }

        public interface IAnimal
        {
            string Name { get; set; }
        }

        public class Cat : IAnimal
        {
            public string Name { get; set; }
        }

        public class Dog : IAnimal
        {

            public string Name { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }

        public interface IGeneric<T>
        {
        }

        public interface IThing
        {
        }

        public class Thing : IThing
        {
            [InjectionConstructor]
            public Thing()
            {
            }

            public Thing(int i)
            {
            }
        }

        public class Gen1 : IGeneric<IThing>
        {
        }

        public class Gen2 : IGeneric<IThing>
        {
        }

        public interface ILogger
        {
        }

        public class MockLogger : ILogger
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

        public class ObjectWithThreeProperties
        {
            [Dependency]
            public string Name { get; set; }

            public object Property { get; set; }

            [Dependency]
            public IUnityContainer Container { get; set; }
        }
        public interface I0 { }

        public interface I1 : I0 { }
        public interface I2 : I0 { }

        public class B1 : I1 { public B1(I1 i1) { } }

        public class C1 : I1 { public C1(I2 i2) { } }

        public class C2 : I2 { public C2(I1 i1) { } }

        public class D1 : I1
        {
#if !NET45
            [Dependency]
#endif
            public I1 Field;
        }

        public class E1 : I1
        {
            [Dependency]
            public I1 Property { get; set; }
        }

        public class F1 : I1
        {
            [InjectionMethod]
            public void Method(I1 i1) { }
        }

        public class G0 : I0 { }
        public class G1 : I1 { public G1(I0 i0) { } }

        #endregion
    }


    // TODO: [TestClass]
    public partial class GitHub_Diagnostic : GitHub
    {
        [TestInitialize]
#if V4
        public override void TestInitialize() => Container = new UnityContainer();
#else
        public override void TestInitialize() => Container = new UnityContainer()
            .AddExtension(new Unity.Diagnostic());
#endif
    }
}