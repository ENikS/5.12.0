using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Resolution;

namespace Unity.Tests.v5.Override
{
    [TestClass]
    public class MultiThreadedPropertyOverrideTests
    {
        private static IUnityContainer container = new UnityContainer();
        private static List<SubjectType1ToInjectForPropertyOverride> defaultInjectedObjectList = new List<SubjectType1ToInjectForPropertyOverride>();
        private static List<SubjectType1ToInjectForPropertyOverride> override1InjectedObjectList = new List<SubjectType1ToInjectForPropertyOverride>();
        private static List<SubjectType1ToInjectForPropertyOverride> override2InjectedObjectList = new List<SubjectType1ToInjectForPropertyOverride>();
        private static int iterationCount = 200;

        [TestMethod]
        public void CanOverrideWithPerThreadLifetimeManagerWithDifferentOverridesInDifferenThreads()
        {
            var defaultObject = new TypeToInjectForPropertyOverride1(123);

            container.RegisterType<SubjectType1ToInjectForPropertyOverride>(
                new PerThreadLifetimeManager(), 
                new InjectionProperty(nameof(SubjectType1ToInjectForPropertyOverride.InjectedObject), defaultObject));

            var threadStartDefault = new ThreadStart(ResolveWithDefault);
            var threadStartOverride1 = new ThreadStart(ResolveWithOverride1);
            var threadStartOverride2 = new ThreadStart(ResolveWithOverride2);
            var threadList = new Thread[3];

            threadList[0] = new Thread(threadStartDefault);
            threadList[1] = new Thread(threadStartOverride1);
            threadList[2] = new Thread(threadStartOverride2);

            for (int i = 0; i < 3; i++)
            {
                threadList[i].Start();
            }

            for (int i = 0; i < 3; i++)
            {
                threadList[i].Join();
            }

            var result1 = defaultInjectedObjectList[0];

            for (int i = 1; i < iterationCount; i++)
            {
                Assert.IsInstanceOfType(defaultInjectedObjectList[i].InjectedObject, typeof(TypeToInjectForPropertyOverride1));
                Assert.AreEqual(result1, defaultInjectedObjectList[i]);
            }

            var result2 = override1InjectedObjectList[0];

            for (int i = 1; i < iterationCount; i++)
            {
                Assert.IsInstanceOfType(override1InjectedObjectList[i].InjectedObject, typeof(TypeToInjectForPropertyOverride2));
                Assert.AreEqual(result2, override1InjectedObjectList[i]);
            }

            var result3 = override2InjectedObjectList[0];

            for (int i = 1; i < iterationCount; i++)
            {
                Assert.IsInstanceOfType(override2InjectedObjectList[i].InjectedObject, typeof(TypeToInjectForPropertyOverride3));
                Assert.AreEqual(result3, override2InjectedObjectList[i]);
            }
        }

        private static void ResolveWithDefault()
        {
            var result = container.Resolve<SubjectType1ToInjectForPropertyOverride>();

            for (int i = 0; i < iterationCount; i++)
            {
                defaultInjectedObjectList.Add(result);
            }
        }

        private static void ResolveWithOverride1()
        {
            for (int i = 0; i < iterationCount; i++)
            {
                TypeToInjectForPropertyOverride2 overrideObject = new TypeToInjectForPropertyOverride2(222);
                var result = container.Resolve<SubjectType1ToInjectForPropertyOverride>(
                    new PropertyOverride(nameof(SubjectType1ToInjectForPropertyOverride.InjectedObject), overrideObject));
                override1InjectedObjectList.Add(result);
            }
        }

        private static void ResolveWithOverride2()
        {
            for (int i = 0; i < iterationCount; i++)
            {
                TypeToInjectForPropertyOverride3 overrideObject = new TypeToInjectForPropertyOverride3(333);
                var result = container.Resolve<SubjectType1ToInjectForPropertyOverride>(
                    new PropertyOverride(nameof(SubjectType1ToInjectForPropertyOverride.InjectedObject), overrideObject));
                override2InjectedObjectList.Add(result);
            }
        }
    }
}
