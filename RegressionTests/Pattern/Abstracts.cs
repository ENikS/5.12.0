using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Specification
{
    public abstract partial class VerificationPattern
    {
        protected abstract InjectionMember GetByNameMember(Type type, string name);

        protected abstract InjectionMember GetByNameOptional(Type type, string name);

        protected abstract InjectionMember GetResolvedMember(Type type, string name);

        protected abstract InjectionMember GetOptionalMember(Type type, string name);

        protected abstract InjectionMember GetOptionalOptional(Type type, string name);

        protected abstract InjectionMember GetGenericMember(Type type, string name);

        protected abstract InjectionMember GetGenericOptional(Type type, string name);
        
        protected abstract InjectionMember GetInjectionValue(object argument);

        protected abstract InjectionMember GetInjectionOptional(object argument);
    }
}
