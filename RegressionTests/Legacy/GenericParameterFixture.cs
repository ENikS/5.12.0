using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Resolution;
#endif


namespace Unity.V4
{
    [TestClass]
    public class GenericParameterFixture
    {
        [TestMethod]
        public void CanCallNonGenericConstructorOnOpenGenericType()
        {
            IUnityContainer container = new UnityContainer()
                .RegisterType(typeof(ClassWithOneGenericParameter<>),
                        new InjectionConstructor("Fiddle", new InjectionParameter<object>("someValue")));

            ClassWithOneGenericParameter<User> result = container.Resolve<ClassWithOneGenericParameter<User>>();

            Assert.IsNull(result.InjectedValue);
        }

        [TestMethod]
        public void CanCallConstructorTakingGenericParameter()
        {
            IUnityContainer container = new UnityContainer()
                .RegisterType(typeof(ClassWithOneGenericParameter<>),
                    new InjectionConstructor(new GenericParameter("T")));

            Account a = new Account();
            container.RegisterInstance<Account>(a);

            ClassWithOneGenericParameter<Account> result = container.Resolve<ClassWithOneGenericParameter<Account>>();
            Assert.AreSame(a, result.InjectedValue);
        }

        [TestMethod]
        public void CanConfiguredNamedResolutionOfGenericParameter()
        {
            IUnityContainer container = new UnityContainer()
                .RegisterType(typeof(ClassWithOneGenericParameter<>),
                    new InjectionConstructor(new GenericParameter("T", "named")));

            Account a = new Account();
            container.RegisterInstance<Account>(a);
            Account named = new Account();
            container.RegisterInstance<Account>("named", named);

            ClassWithOneGenericParameter<Account> result = container.Resolve<ClassWithOneGenericParameter<Account>>();
            Assert.AreSame(named, result.InjectedValue);
        }

        [Ignore("Breaking Change, v6 ")]
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void ResolvingOpenGenericWithConstructorParameterAmbiguityThrows()
        {
            var container = new UnityContainer();
            container.RegisterType(
                typeof(GenericTypeWithMultipleGenericTypeParameters<,>),
                new InjectionConstructor(new GenericParameter("T", "instance")));
            container.RegisterInstance("instance", "the string");
            container.Resolve<GenericTypeWithMultipleGenericTypeParameters<string, string>>();
        }

        [Ignore("Breaking Change, v6 ")]
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void ResolvingOpenGenericWithMethodAmbiguityThrows()
        {
            var container = new UnityContainer();
            container.RegisterType(
                typeof(GenericTypeWithMultipleGenericTypeParameters<,>),
                new InjectionMethod("Set", new GenericParameter("T", "instance")));
            container.RegisterInstance("instance", "the string");

            //// equivalent to doing the following, which would be rejected by the compiler
            //new GenericTypeWithMultipleGenericTypeParameters<string, string>().Set(container.Resolve<string>("instance"));

            container.Resolve<GenericTypeWithMultipleGenericTypeParameters<string, string>>();
        }

        // Our various test objects
        public class ClassWithOneGenericParameter<T>
        {
            public T InjectedValue;

            public ClassWithOneGenericParameter(string s, object o)
            {
            }

            public ClassWithOneGenericParameter(T injectedValue)
            {
                InjectedValue = injectedValue;
            }
        }

        public class GenericTypeWithMultipleGenericTypeParameters<T, U>
        {
            private T theT;
            private U theU;
            public string Value;

            [InjectionConstructor]
            public GenericTypeWithMultipleGenericTypeParameters()
            {
            }

            public GenericTypeWithMultipleGenericTypeParameters(T theT)
            {
                this.theT = theT;
            }

            public GenericTypeWithMultipleGenericTypeParameters(U theU)
            {
                this.theU = theU;
            }

            public void Set(T theT)
            {
                this.theT = theT;
            }

            public void Set(U theU)
            {
                this.theU = theU;
            }

            public void SetAlt(T theT)
            {
                this.theT = theT;
            }

            public void SetAlt(string value)
            {
                this.Value = value;
            }
        }
        // A couple of sample objects we're stuffing into our commands
        public class User
        {
            public void DoSomething(string message)
            {
            }
        }

        public class Account
        {
        }

        // Value type used for testing nesting
        public struct Customer
        {
        }
    }
}
