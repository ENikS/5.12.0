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
        /// <summary>
        /// Tests injecting dependencies by value wrapped in parameter
        /// </summary>
        /// <example>
        /// Container.RegisterType(target, new InjectionConstructor(new InjectionParameter(value)), 
        ///                                new InjectionMethod("Method", new InjectionParameter(value)) , 
        ///                                new InjectionField("Field",   new InjectionParameter(value)), 
        ///                                new InjectionProperty("Property", new InjectionParameter(value)));
        /// </example>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Injected_Data))]
        public virtual void Injected_ByInjection(string test, Type type, string name, Type dependency, object expected)
        {
            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;
            // Arrange
            Container.RegisterType(target, name, GetInjectionValue(new InjectionParameter(dependency, expected)));

            RegisterTypes();

            // Act
            var instance = Container.Resolve(target, name) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }


        /// <summary>
        /// Tests injecting dependencies by value wrapped in parameter from empty container
        /// </summary>
        [DataTestMethod]
        [DynamicData(nameof(Injected_Data))]
        public virtual void Injected_ByInjection_FromEmpty(string test, Type type, string name, Type dependency, object expected)
        {
            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;
            // Arrange
            Container.RegisterType(target, name, GetInjectionValue(new InjectionParameter(dependency, expected)));

            // Act
            var instance = Container.Resolve(target, name) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }
    }
}
