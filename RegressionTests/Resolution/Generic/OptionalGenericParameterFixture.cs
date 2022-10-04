using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Threading;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Resolution
{
    public partial class Generics
    {
        [TestMethod]
        public void CanCallConstructorTakingGenericParameterWithResolvableOptional()
        {
            Container.RegisterType(typeof(ClassWithOneGenericParameter<>),
                    new InjectionConstructor(new OptionalGenericParameter("T")));

            Account a = new Account();
            Container.RegisterInstance<Account>(a);

            ClassWithOneGenericParameter<Account> result = Container.Resolve<ClassWithOneGenericParameter<Account>>();

            Assert.AreSame(a, result.InjectedValue);
        }

        [TestMethod]
        public void CanCallConstructorTakingGenericParameterWithNonResolvableOptional()
        {
            Container.RegisterType(typeof(ClassWithOneGenericParameter<>),
                    new InjectionConstructor(new OptionalGenericParameter("T")));

            var result = Container.Resolve<ClassWithOneGenericParameter<IComparable>>();

            Assert.IsNull(result.InjectedValue);
        }

        [TestMethod]
        public void CanConfiguredNamedResolutionOfOptionalGenericParameter()
        {
            Container.RegisterType(typeof(ClassWithOneGenericParameter<>),
                    new InjectionConstructor(new OptionalGenericParameter("T", "named")));

            Account a = new Account();
            Container.RegisterInstance<Account>(a);
            Account named = new Account();
            Container.RegisterInstance<Account>("named", named);

            ClassWithOneGenericParameter<Account> result = Container.Resolve<ClassWithOneGenericParameter<Account>>();
            Assert.AreSame(named, result.InjectedValue);
        }
    }
}
