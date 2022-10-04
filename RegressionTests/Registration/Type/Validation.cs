using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Resolution;
#endif

namespace Registrations
{
    public partial class RegisterType
    {
        public static IEnumerable<object[]> ArgumetTestData
        {
            get
            {
                yield return new object[] { typeof(IService), typeof(Service),  null, null,                                     typeof(TransientLifetimeManager) };
       // TODO: yield return new object[] { typeof(Service),  typeof(IService), null, null,                                     typeof(TransientLifetimeManager) };
                yield return new object[] { typeof(object),   typeof(object),   null, null,                                     typeof(TransientLifetimeManager) };
                yield return new object[] { null,             typeof(object),   null, null,                                     typeof(TransientLifetimeManager) };
                yield return new object[] { typeof(object),   null,             null, null,                                     typeof(TransientLifetimeManager) };
                yield return new object[] { typeof(object),   typeof(object),   Name, null,                                     typeof(TransientLifetimeManager) };
                yield return new object[] { null,             typeof(object),   Name, null,                                     typeof(TransientLifetimeManager) };
                yield return new object[] { typeof(object),   null,             Name, null,                                     typeof(TransientLifetimeManager) };
                yield return new object[] { typeof(object),   typeof(object),   null, new ContainerControlledLifetimeManager(), typeof(ContainerControlledLifetimeManager) };
                yield return new object[] { null,             typeof(object),   null, new ContainerControlledLifetimeManager(), typeof(ContainerControlledLifetimeManager) };
                yield return new object[] { typeof(object),   null,             null, new ContainerControlledLifetimeManager(), typeof(ContainerControlledLifetimeManager) };
                yield return new object[] { typeof(object),   typeof(object),   Name, new ContainerControlledLifetimeManager(), typeof(ContainerControlledLifetimeManager) };
                yield return new object[] { null,             typeof(object),   Name, new ContainerControlledLifetimeManager(), typeof(ContainerControlledLifetimeManager) };
                yield return new object[] { typeof(object),   null,             Name, new ContainerControlledLifetimeManager(), typeof(ContainerControlledLifetimeManager) };
            }
        }

        public static IEnumerable<object[]> ArgumetTestDataFailing
        {
            get
            {
                yield return new object[] { typeof(ArgumentException), null, null, null, null,                                     null };
                yield return new object[] { typeof(ArgumentException), null, null, Name, null,                                     null };
                yield return new object[] { typeof(ArgumentException), null, null, null, new ContainerControlledLifetimeManager(), null };
                yield return new object[] { typeof(ArgumentException), null, null, Name, new ContainerControlledLifetimeManager(), null };
            }
        }


        [DataTestMethod]
        [DynamicData(nameof(ArgumetTestData))]
#if V4
        public void ArgumentValidation(Type typeFrom, Type typeTo, string name, LifetimeManager lifetimeManager, Type manager)
#else
        public void ArgumentValidation(Type typeFrom, Type typeTo, string name, ITypeLifetimeManager lifetimeManager, Type manager)
#endif
        {
            // Act
            Container.RegisterType(typeFrom, typeTo, name, lifetimeManager);

            var registeredType = typeFrom ?? typeTo;
            var registration = Container.Registrations.FirstOrDefault(r => r.RegisteredType == registeredType && r.Name == name);

            Assert.IsNotNull(registration);
            Assert.IsInstanceOfType(registration.LifetimeManager, manager);
        }


        [DataTestMethod]
        [DynamicData(nameof(ArgumetTestDataFailing))]
#if V4
        public void ArgumentValidationFailing(Type exception, Type typeFrom, Type typeTo, string name, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
#else
        public void ArgumentValidationFailing(Type exception, Type typeFrom, Type typeTo, string name, ITypeLifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
#endif
        {
            try
            {
                // Act
                Container.RegisterType(typeFrom, typeTo, name, lifetimeManager, injectionMembers);
                Assert.Fail("Did not throw and exception of type {exception?.Name}");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, exception);
            }
        }
    }
}
