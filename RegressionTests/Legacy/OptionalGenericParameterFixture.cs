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
    public class OptionalGenericParameterFixture
    {
        [TestMethod]
        public void CanCallConstructorTakingGenericParameterWithResolvableOptional()
        {
            IUnityContainer container = new UnityContainer()
                .RegisterType(typeof(ClassWithOneGenericParameter<>),
                    new InjectionConstructor(new OptionalGenericParameter("T")));

            Account a = new Account();
            container.RegisterInstance<Account>(a);

            ClassWithOneGenericParameter<Account> result = container.Resolve<ClassWithOneGenericParameter<Account>>();

            Assert.AreSame(a, result.InjectedValue);
        }

        [TestMethod]
        public void CanCallConstructorTakingGenericParameterWithNonResolvableOptional()
        {
            IUnityContainer container = new UnityContainer()
                .RegisterType(typeof(ClassWithOneGenericParameter<>),
                    new InjectionConstructor(new OptionalGenericParameter("T")));

            var result = container.Resolve<ClassWithOneGenericParameter<IComparable>>();

            Assert.IsNull(result.InjectedValue);
        }

        [TestMethod]
        public void CanConfiguredNamedResolutionOfOptionalGenericParameter()
        {
            IUnityContainer container = new UnityContainer()
                .RegisterType(typeof(ClassWithOneGenericParameter<>),
                    new InjectionConstructor(new OptionalGenericParameter("T", "named")));

            Account a = new Account();
            container.RegisterInstance<Account>(a);
            Account named = new Account();
            container.RegisterInstance<Account>("named", named);

            ClassWithOneGenericParameter<Account> result = container.Resolve<ClassWithOneGenericParameter<Account>>();
            Assert.AreSame(named, result.InjectedValue);
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
