﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity;
using Unity.Injection;

namespace Microsoft.Practices.Unity.Configuration.Tests.TestObjects
{
    public class TestInjectionMemberElement : InjectionMemberElement
    {
        private static int numElements = 0;
        private readonly int index;

        public TestInjectionMemberElement()
        {
            this.index = Interlocked.Increment(ref numElements);
        }

        /// <summary>
        /// Each element must have a unique key, which is generated by the subclasses.
        /// </summary>
        public override string Key
        {
            get { return "TestInjectionMemberElement: " + this.index.ToString(); }
        }

        /// <summary>
        /// Return the set of <see cref="InjectionMember"/>s that are needed
        /// to configure the container according to this configuration element.
        /// </summary>
        /// <param name="container">Container that is being configured.</param>
        /// <param name="fromType">Type that is being registered.</param>
        /// <param name="toType">Type that <paramref name="fromType"/> is being mapped to.</param>
        /// <param name="name">Name this registration is under.</param>
        /// <returns>One or more <see cref="InjectionMember"/> objects that should be
        /// applied to the container registration.</returns>
        public override IEnumerable<InjectionMember> GetInjectionMembers(IUnityContainer container, Type fromType, Type toType, string name)
        {
            return Enumerable.Empty<InjectionMember>();
        }
    }
}
