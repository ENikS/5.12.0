using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Specification
{
    public partial class Constructors
    {

        public class ClassWithTreeConstructors
        {
            protected ClassWithTreeConstructors()
            {

            }

            public ClassWithTreeConstructors(IUnityContainer container)
            {
                Value = container;
            }

            public ClassWithTreeConstructors(string name)
            {
                Value = name;
            }

            public ClassWithTreeConstructors(object value)
            {
                Value = value;
            }

            public object Value { get; }
        }

        public class SampleType
        {
            public SampleType(object arg) => Ctor = 1;

            public SampleType(IService service, object arg) => Ctor = 2;

            public SampleType(IService service, object arg, Type type) => Ctor = 3;

            public int Ctor { get; } // Constructor called 
        }

        public class SampleType<T>
        {
            public SampleType(T arg)
            {
                Ctor = 1;
                Value = arg;
            }

            public SampleType([Dependency("one")] IService service, T arg)
            {
                Ctor = 2;
                Service = service;
                Value = arg;
            }

            public SampleType(IService service, T arg, object obj)
            {
                Ctor = 3;
                Service = service;
                Value = arg;
            }

            public int Ctor { get; }        // Called Constructor
            public IService Service { get; }// Service passed in
            public T Value { get; }         // Generic argument value
        }

        public class Foo
        {
            public Foo(string data)
            {
                Data = data;
            }

            public object Data { get; }
        }

        public class MultipleTypeService
        {
            public MultipleTypeService(int data)
            {
                Data = data;
            }
            public MultipleTypeService(string data)
            {
                Data = data;
            }
            public MultipleTypeService(double data)
            {
                Data = data;
            }

            public object Data { get; }
        }

        public class Service<T>
        {
            public Service() => Ctor = 1;

            public Service(T arg) => Ctor = 2;

            public int Ctor { get; }    // Constructor called 
        }

        public class ServiceOne : IService
        {
        }

        public class ServiceTwo : IService
        {
        }

        public class TestClass
        {
            public TestClass() { }                                                    // 0
            public TestClass(object first) { }                                        // 1
            public TestClass(int first, string second, string third) { }              // 2
            public TestClass(Type first, Type second, Type third) { }                 // 3
            public TestClass(int first, string second, double third) { }              // 4
            public TestClass(string first, string second, string third) { }           // 5
            public TestClass(string first, string second, IUnityContainer third) { }  // 6
        }

        public class GenericTestClass<TA, TB, TC>
        {
            public TB CollectionName { get; }

            public GenericDependencyClass<TA, TC> GenDependency { get; }

            public GenericTestClass(TB name)
            {
                CollectionName = name;
            }

            public GenericTestClass(GenericDependencyClass<TA, TC> genericDependency)
            {
                GenDependency = genericDependency;
            }

            public GenericTestClass()
            {

            }
        }

        public interface IGenericService<T>
        {
        }

        public class OtherService : IService, IDisposable
        {
            public string ID { get; } = Guid.NewGuid().ToString();

            public static int Instances = 0;

            public OtherService()
            {
                Interlocked.Increment(ref Instances);
            }

            public bool Disposed = false;
            public void Dispose()
            {
                Disposed = true;
            }
        }

        public class TypeWithMultipleCtors
        {
            public const string One = "1";
            public const string Two = "2";
            public const string Three = "3";
            public const string Four = "4";
            public const string Five = "5";

            public string Signature { get; }

            public TypeWithMultipleCtors()
            {
                Signature = One;
            }

            public TypeWithMultipleCtors(int first, string second, float third)
            {
                Signature = Two;
            }

            public TypeWithMultipleCtors(Type first, Type second, Type third)
            {
                Signature = Three;
            }

            public TypeWithMultipleCtors(string first, string second, string third)
            {
                Signature = first;
            }

            public TypeWithMultipleCtors(string first, [Dependency]string second, Type third)
            {
                Signature = second;
            }

            public TypeWithMultipleCtors(string first, [Dependency(Five)] string second, IUnityContainer third)
            {
                Signature = second;
            }
        }

        public class InjectionTestCollection<T>
        {
            public IGenericService<T> Printer { get; }

            public string CollectionName { get; }

            public InjectionTestCollection()
            {
                CollectionName = typeof(InjectionTestCollection<>).Name;
            }

            public InjectionTestCollection(string name)
            {

            }

            public InjectionTestCollection(string name, IGenericService<T> printService)
            {
                CollectionName = name;
                Printer = printService;
            }

            public InjectionTestCollection(string name, IGenericService<T> printService, T[] items)
                : this(name, printService)
            {
                Items = items;
            }

            public InjectionTestCollection(string name, IGenericService<T> printService, T[][] itemsArray)
                : this(name, printService, itemsArray.Length > 0 ? itemsArray[0] : null)
            { }



            public T[] Items { get; set; }
        }

        public class GenericInjectionTestClass<A, B, C>
        {
            public IGenericService<A> Printer { get; }

            public string CollectionName { get; }

            public GenericDependencyClass<A, C> GenDependency { get; }

            public GenericInjectionTestClass(string name, IGenericService<A> printService, B[] itemsArray, IEnumerable<C> enumerable)
            {
                CollectionName = name;
                Printer = printService;
                ItemsAll = itemsArray;
                Items = enumerable;
            }

            public GenericInjectionTestClass(GenericDependencyClass<A, C> genericDependency)
            {
                GenDependency = genericDependency;
            }

            public GenericInjectionTestClass(string name, IGenericService<A> printService)
            {
                CollectionName = name;
                Printer = printService;
            }

            public GenericInjectionTestClass(string name)
            {
                CollectionName = name;
            }

            public GenericInjectionTestClass()
            {
                CollectionName = typeof(InjectionTestCollection<>).Name;
            }
            public B[] ItemsAll { get; set; }

            public IEnumerable<C> Items { get; set; }
        }

        public class GenericDependencyClass<T, V>
        {
            public IGenericService<T> Printer { get; }

            public IEnumerable<V> Items { get; set; }
        }
    }
}
