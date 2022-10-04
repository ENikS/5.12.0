using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Resolution;
#endif

namespace Specification
{
    public partial class Constructors
    {
        public static IEnumerable<object[]> DefaultConstructorTestData
        {

            // Format:                     |TypeFrom,  |TypeTo,                                        |Name,      |TypeToResolve
            get
            {
                yield return new object[] { null, typeof(object), null, typeof(object) };
                yield return new object[] { null, typeof(TestClass), null, typeof(TestClass) };
                yield return new object[] { null, typeof(GenericTestClass<int, string, object>), null, typeof(GenericTestClass<int, string, object>) };
                yield return new object[] { null, typeof(object), "0", typeof(object) };
                yield return new object[] { null, typeof(TestClass), "1", typeof(TestClass) };
                yield return new object[] { null, typeof(GenericTestClass<,,>), "2", typeof(GenericTestClass<int, string, object>) };
                yield return new object[] { null, typeof(GenericTestClass<int, string, object>), "3", typeof(GenericTestClass<int, string, object>) };
                yield return new object[] { null, typeof(GenericTestClass<,,>), "4", typeof(GenericTestClass<object, string, object>) };
            }
        }

        public static IEnumerable<object[]> DefaultConstructorTestDataFailed
        {
            // Format:                      |TypeTo/TypeToResolve,        |Name
            get
            {
                yield return new object[] { typeof(GenericTestClass<,,>), null };
                yield return new object[] { typeof(GenericTestClass<,,>), null };
                yield return new object[] { typeof(GenericTestClass<,,>), "2" };
                yield return new object[] { typeof(GenericTestClass<,,>), "4" };
            }
        }

        public static IEnumerable<object[]> ConstructorSelectionTestData
        {

            // Format: Type typeFrom, Type typeTo, string name, Type typeToResolve, object[] parameters, Func<object, bool> validator
            get
            {
                // SelectByValues
                yield return new object[]
                {
                    "SelectByValues",                           //  string name, 
                    null,                                       //  Type typeFrom, 
                    typeof(TypeWithMultipleCtors),              //  Type typeTo, 
                    typeof(TypeWithMultipleCtors),              //  Type typeToResolve, 
                    new object[] { 0, string.Empty, 0.0f },     //  object[] parameters, 
                                                                //  Func<object, bool> validator
                    new Func<object, bool>(r => TypeWithMultipleCtors.Two == ((TypeWithMultipleCtors)r).Signature)           
                };
                
                // DefaultConstructorGeneric
                yield return new object[]
                {
                    "DefaultConstructorGeneric",                //  string name, 
                    null,                                       //  Type typeFrom, 
                    typeof(InjectionTestCollection<>),          //  Type typeTo, 
                    typeof(InjectionTestCollection<object>),    //  Type typeToResolve, 
                    new object[] { },                           //  object[] parameters, 
                    new Func<object, bool>(r =>                 //  Func<object, bool> validator
                        typeof(InjectionTestCollection<>).Name == ((InjectionTestCollection<object>)r).CollectionName)
                };

                // SelectAndResolveByValue
                yield return new object[]
                {
                    "SelectAndResolveByValue",                  //  Legacy name, 
                    null,                                       //  Type typeFrom, 
                    typeof(TypeWithMultipleCtors),              //  Type typeTo, 
                    typeof(TypeWithMultipleCtors),              //  Type typeToResolve, 
                    new object[] {
                        new ResolvedParameter(typeof(string)),  //  object[] parameters, 
                        string.Empty,
                        string.Empty },
                    new Func<object, bool>(r =>                 //  Func<object, bool> validator
                        TypeWithMultipleCtors.Four == ((TypeWithMultipleCtors)r).Signature)
                };

                // ResolveUnNamedTypeArgument
                yield return new object[]
                {
                    "ResolveUnNamedTypeArgument",               //  Legacy name,  
                    null,                                       //  Type typeFrom, 
                    typeof(TypeWithMultipleCtors),              //  Type typeTo, 
                    typeof(TypeWithMultipleCtors),              //  Type typeToResolve, 
                    new object[] {
                        typeof(string),                         //  object[] parameters, 
                        typeof(string),
                        typeof(Type)},
                    new Func<object, bool>(r =>                 //  Func<object, bool> validator
                        TypeWithMultipleCtors.Three == ((TypeWithMultipleCtors)r).Signature)
                };
#if !NET45
                // ResolveNamedTypeArgument
                yield return new object[]
                {
                    "ResolveNamedTypeArgument",                 //  string name, 
                    null,                                       //  Type typeFrom, 
                    typeof(TypeWithMultipleCtors),             //  Type typeTo, 
                    typeof(TypeWithMultipleCtors),             //  Type typeToResolve, 
                    new object[] {
                        typeof(string),                         //  object[] parameters, 
                        typeof(string),
                        typeof(IUnityContainer)},
                    new Func<object, bool>(r =>                 //  Func<object, bool> validator
                        TypeWithMultipleCtors.Three == ((TypeWithMultipleCtors)r).Signature)
                };
#endif
                //
                yield return new object[]
                {
                    "",                                         //  string name, 
                    null,                                       //  Type typeFrom, 
                    typeof(object),                             //  Type typeTo, 
                    typeof(object),                             //  Type typeToResolve, 
                    new object[] {},                            //  object[] parameters, 
                    new Func<object, bool>(r => true)           //  Func<object, bool> validator
                };
            }
        }

        public static IEnumerable<object[]> ConstructorRegistrationFailedTestData
        {

            // Format: Type typeFrom, Type typeTo, string name, Type typeToResolve, object[] parameters, Func<object, bool> validator
            get
            { 
                yield return new object[] { null, typeof(object),                                 null, typeof(object),                                   new object[] {}, new Func<object, bool>(r => true) };
                yield return new object[] { null, typeof(TestClass),                              null, typeof(TestClass),                                new object[] {}, new Func<object, bool>(r => true) };
                yield return new object[] { null, typeof(GenericTestClass<int, string, object>),  null, typeof(GenericTestClass<int, string, object>),    new object[] {}, new Func<object, bool>(r => true) };
                yield return new object[] { null, typeof(object),                                 "0",  typeof(object),                                   new object[] {}, new Func<object, bool>(r => true) };
                yield return new object[] { null, typeof(TestClass),                              "1",  typeof(TestClass),                                new object[] {}, new Func<object, bool>(r => true) };
                yield return new object[] { null, typeof(GenericTestClass<,,>),                   "2",  typeof(GenericTestClass<int, string, object>),    new object[] {}, new Func<object, bool>(r => true) };
                yield return new object[] { null, typeof(GenericTestClass<int, string, object>),  "3",  typeof(GenericTestClass<int, string, object>),    new object[] {}, new Func<object, bool>(r => true) };
                yield return new object[] { null, typeof(GenericTestClass<,,>),                   "4",  typeof(GenericTestClass<object, string, object>), new object[] {}, new Func<object, bool>(r => true) };
            }
        }


        [DataTestMethod]
        [DynamicData(nameof(ConstructorSelectionTestData))]
        public void Injection_Selection(string name, Type typeFrom, Type typeTo, Type typeToResolve, object[] parameters, Func<object, bool> validator)
        {
            // Setup
            Container.RegisterType(typeFrom, typeTo, name, new InjectionConstructor(parameters));

            Container.RegisterInstance(typeof(Type),   null, typeof(TypeWithMultipleCtors), new ContainerControlledLifetimeManager())
                     .RegisterInstance(typeof(string), null, TypeWithMultipleCtors.Four, new ContainerControlledLifetimeManager())
                     .RegisterInstance(typeof(string), TypeWithMultipleCtors.Five, TypeWithMultipleCtors.Five, new ContainerControlledLifetimeManager());

            // Act
            var result = Container.Resolve(typeToResolve, name);

            // Verify
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeToResolve);
            Assert.IsTrue(validator?.Invoke(result) ?? true);
        }

        [DataTestMethod]
        [DynamicData(nameof(DefaultConstructorTestData))]
        public void Injection_Default(Type typeFrom, Type typeTo, string name, Type typeToResolve)
        {
            // Setup
            Container.RegisterType(typeFrom, typeTo, name, new InjectionConstructor());

            // Act
            var result = Container.Resolve(typeToResolve, name);

            // Verify
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeToResolve);
        }


        [DataTestMethod]
        [DynamicData(nameof(DefaultConstructorTestDataFailed))]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Injection_DefaultCtorValidation(Type type, string name)
        {
            // Setup
            Container.RegisterType(type, name, new InjectionConstructor());

            // Act
            var result = Container.Resolve(type, name);
            Assert.IsNotNull(result);
        }

    }
}
