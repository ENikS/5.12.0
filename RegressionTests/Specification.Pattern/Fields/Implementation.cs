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
    public partial class Fields : VerificationPattern
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
        }

        protected override InjectionMember GetByNameMember(Type type, string name)
            => new InjectionField("Field");

        protected override InjectionMember GetByNameOptional(Type type, string name)
#if NET46 || NET461
            => new InjectionField("Field", true);
#else
            => new OptionalField("Field");
#endif

        protected override InjectionMember GetResolvedMember(Type type, string name) 
            => new InjectionField("Field", new ResolvedParameter(type, name));

        protected override InjectionMember GetOptionalMember(Type type, string name)
            => new InjectionField("Field", new OptionalParameter(type, name));

        protected override InjectionMember GetOptionalOptional(Type type, string name)
#if NET46 || NET461
            => new InjectionField("Field", new OptionalParameter(type, name));
#else
            => new OptionalField("Field", new OptionalParameter(type, name));
#endif

        protected override InjectionMember GetGenericMember(Type _, string name)
            => new InjectionField("Field", new GenericParameter("T", name));

        protected override InjectionMember GetGenericOptional(Type type, string name)
            => new InjectionField("Field", new OptionalGenericParameter("T", name));

        protected override InjectionMember GetInjectionValue(object argument) 
            => new InjectionField("Field", argument);

        protected override InjectionMember GetInjectionOptional(object argument)
#if NET46 || NET461
            => new InjectionField("Field", argument);
#else
            => new OptionalField("Field", argument);
#endif
    }
}
