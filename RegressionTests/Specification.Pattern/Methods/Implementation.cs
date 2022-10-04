using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Specification.Pattern
{
    [TestClass]
    public partial class Methods : VerificationPattern
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            PocoType = typeof(Implicit_Dependency_Generic<>);
            Required = typeof(Required_Dependency_Generic<>);
            Optional = typeof(Optional_Dependency_Generic<>);

            Required_Named = typeof(Required_Dependency_Named<>);
            Optional_Named = typeof(Optional_Dependency_Named<>);

            PocoType_Default_Value = typeof(Implicit_WithDefault_Value);
            Required_Default_Value = typeof(Required_WithDefault_Value);
            Optional_Default_Value = typeof(Optional_WithDefault_Value);

            PocoType_Default_Class = typeof(Implicit_WithDefault_Class);
            Required_Default_String = typeof(Required_WithDefault_Class);
            Optional_Default_Class = typeof(Optional_WithDefault_Class);


            Type_Implicit_Dependency_Ref = typeof(Implicit_Dependency_Ref).FullName;
            Type_Implicit_Dependency_Out = typeof(Implicit_Dependency_Out).FullName;
            Type_Implicit_Generic_Ref = typeof(Implicit_Generic_Ref<>).FullName;
            Type_Implicit_Generic_Out = typeof(Implicit_Generic_Out<>).FullName;
            Type_Required_Dependency_Ref = typeof(Required_Dependency_Ref).FullName;
            Type_Required_Dependency_Out = typeof(Required_Dependency_Out).FullName;
            Type_Required_Generic_Ref = typeof(Required_Generic_Ref<>).FullName;
            Type_Required_Generic_Out = typeof(Required_Generic_Out<>).FullName;
            Type_Optional_Dependency_Ref = typeof(Optional_Dependency_Ref).FullName;
            Type_Optional_Dependency_Out = typeof(Optional_Dependency_Out).FullName;
            Type_Optional_Generic_Ref = typeof(Optional_Generic_Ref<>).FullName;
            Type_Optional_Generic_Out = typeof(Optional_Generic_Out<>).FullName;
        }

        protected override InjectionMember GetByNameMember(Type type, string name)
            => new InjectionMethod("Method");

        protected override InjectionMember GetByNameOptional(Type type, string name)
            => new InjectionMethod("Method");

        protected override InjectionMember GetResolvedMember(Type type, string name) 
            => new InjectionMethod("Method", new ResolvedParameter(type, name));

        protected override InjectionMember GetOptionalMember(Type type, string name)
            => new InjectionMethod("Method", new ResolvedParameter(type, name));

        protected override InjectionMember GetOptionalOptional(Type type, string name)
            => new InjectionMethod("Method", new OptionalParameter(type, name));

        protected override InjectionMember GetGenericMember(Type _, string name)
            => new InjectionMethod("Method", new GenericParameter("T", name));

        protected override InjectionMember GetGenericOptional(Type type, string name)
            => new InjectionMethod("Method", new OptionalGenericParameter("T", name));

        protected override InjectionMember GetInjectionValue(object argument) 
            => new InjectionMethod("Method", argument);

        protected override InjectionMember GetInjectionOptional(object argument)
            => new InjectionMethod("Method", argument);
    }
}
