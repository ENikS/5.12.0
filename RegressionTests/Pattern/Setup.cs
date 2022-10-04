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
    [TestClass]
    public abstract partial class VerificationPattern
    {
        #region Fields

        // Test Constants
        protected const int NamedInt = 123;
        protected const int DefaultInt = 345;
        protected const int InjectedInt = 678;
        protected const int RegisteredInt = 890;
        protected const string Name = "name";
        protected const string Null = "null";
        protected const string NamedString = "named";
        protected const string DefaultString = "default";
        protected const string InjectedString = "injected";
        protected const string RegisteredString = "registered";
        public readonly static Unresolvable RegisteredUnresolvable = Unresolvable.Create("singleton");
        public readonly static Unresolvable NamedSingleton = Unresolvable.Create("named");
        public readonly static Unresolvable InjectedSingleton = SubUnresolvable.Create("injected");
        public readonly static object RegisteredStruct = new TestStruct(55, "struct");

        // Generic type names
        protected static string Type_Implicit_Dependency_Ref;
        protected static string Type_Implicit_Dependency_Out;
        protected static string Type_Implicit_Generic_Ref;
        protected static string Type_Implicit_Generic_Out;
        protected static string Type_Required_Dependency_Ref;
        protected static string Type_Required_Dependency_Out;
        protected static string Type_Required_Generic_Ref;
        protected static string Type_Required_Generic_Out;
        protected static string Type_Optional_Dependency_Ref;
        protected static string Type_Optional_Dependency_Out;
        protected static string Type_Optional_Generic_Ref;
        protected static string Type_Optional_Generic_Out;

        // Test types
        protected static Type PocoType;
        protected static Type Required;
        protected static Type Optional;
        protected static Type Required_Named;
        protected static Type Optional_Named;
        protected static Type PocoType_Default_Value;
        protected static Type Required_Default_Value;
        protected static Type Optional_Default_Value;
        protected static Type PocoType_Default_Class;
        protected static Type Required_Default_String;
        protected static Type Optional_Default_Class;

        protected IUnityContainer Container;

        #endregion


        [TestInitialize]
        public virtual void TestInitialize() => Container = new UnityContainer();

        
        #region Implementation

        protected Type TargetType(string name) => Type.GetType($"{GetType().FullName}+{name}");

        protected virtual void RegisterTypes()
        {
            Container.RegisterInstance(RegisteredInt)
                     .RegisterInstance(RegisteredString)
                     .RegisterInstance(RegisteredUnresolvable)
                     .RegisterInstance(typeof(TestStruct), RegisteredStruct)
#if !V4 // Only Unity v5 and up allow `null` as a value
                     .RegisterInstance(typeof(string),       Null, (object)null)
                     .RegisterInstance(typeof(Unresolvable), Null, (object)null)
#endif
                     .RegisterInstance(Name, NamedInt)
                     .RegisterInstance(Name, NamedSingleton);
        }

        #endregion
    }
}
