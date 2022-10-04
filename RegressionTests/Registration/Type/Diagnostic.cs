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
    public partial class RegisterType_Diagnostic
    {
        public static IEnumerable<object[]> ArgumetTestDataDiagnostic
        {
            get
            {
                yield return new object[] { typeof(IService), typeof(Service),  null, null,                                     typeof(TransientLifetimeManager) };
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

        public static IEnumerable<object[]> ArgumetTestDataFailingDiagnostic
        {
            get
            {
                yield return new object[] { typeof(Service),  typeof(IService), null, null,                                     typeof(ArgumentException) };
                yield return new object[] { null,             null,             null, null,                                     typeof(ArgumentException) };
                yield return new object[] { null,             null,             Name, null,                                     typeof(ArgumentException) };
                yield return new object[] { null,             null,             null, new ContainerControlledLifetimeManager(), typeof(ArgumentException) };
                yield return new object[] { null,             null,             Name, new ContainerControlledLifetimeManager(), typeof(ArgumentException) };
            }
        }


        [DataTestMethod]
        [DynamicData(nameof(ArgumetTestDataDiagnostic))]
#if V4
        public void ArgumentValidationDiagnostic(Type typeFrom, Type typeTo, string name, LifetimeManager lifetimeManager, Type manager)
#else
        public void ArgumentValidationDiagnostic(Type typeFrom, Type typeTo, string name, ITypeLifetimeManager lifetimeManager, Type manager)
#endif
        {
            // Act
            Container.RegisterType(typeFrom, typeTo, name, lifetimeManager);

            var registeredType = typeFrom ?? typeTo;
            var registration = Container.Registrations.FirstOrDefault(r => r.RegisteredType == registeredType && r.Name == name);

            Assert.IsNotNull(registration);
            Assert.IsInstanceOfType(registration.LifetimeManager, manager);
        }

        [Ignore("v6 no upfront diagnostics")]
        [DataTestMethod]
        [DynamicData(nameof(ArgumetTestDataFailingDiagnostic))]
#if V4
        public void ArgumentValidationDiagnosticFailing(Type typeFrom, Type typeTo, string name, LifetimeManager lifetimeManager, Type exception)
#else
        public void ArgumentValidationDiagnosticFailing(Type typeFrom, Type typeTo, string name, ITypeLifetimeManager lifetimeManager, Type exception)
#endif
        {
            try
            {
                // Act
                Container.RegisterType(typeFrom, typeTo, name, lifetimeManager);
                Assert.Fail("Did not throw and exception of type {exception?.Name}");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, exception);
            }
        }
    }
}
