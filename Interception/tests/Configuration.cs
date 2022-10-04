using Microsoft.Practices.Unity.InterceptionExtension.Tests;
using Microsoft.Practices.Unity.TestSupport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Unity;
using Unity.Injection;
using Unity.Interception;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.TypeInterceptors.VirtualMethodInterception;
using Unity.Interception.PolicyInjection;
using Unity.Interception.PolicyInjection.MatchingRules;
using Unity.Interception.PolicyInjection.Pipeline;
using Unity.Interception.PolicyInjection.Policies;
using Unity.Lifetime;

namespace Configuration
{
    [TestClass]
    public class ConfigurationFixture
    {
        [TestMethod]
        public void CanSetUpPolicy()
        {
            IUnityContainer container = new UnityContainer()
                .AddNewExtension<Interception>();

            IMatchingRule rule1 = new AlwaysMatchingRule();
            ICallHandler handler1 = new CallCountHandler();

            container
                .Configure<Interception>()
                    .AddPolicy("policy1")
                        .AddMatchingRule(rule1)
                        .AddCallHandler(handler1);

            container
                .Configure<Interception>()
                    .SetInterceptorFor<Wrappable>("wrappable", new VirtualMethodInterceptor());

            container.RegisterType<Wrappable>("wrappable",
                //new InterceptionBehavior<FakeInterceptionBehavior>(),
                new Interceptor<VirtualMethodInterceptor>());

            Wrappable wrappable1 = container.Resolve<Wrappable>("wrappable");
            wrappable1.Method2();

            Assert.AreEqual(1, ((CallCountHandler)handler1).CallCount);
        }
    }
}
