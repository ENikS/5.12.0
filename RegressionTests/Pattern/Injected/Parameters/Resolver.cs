using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Regression.Tests;
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
        /// Tests injecting dependencies by resolver wrapped in parameter
        /// </summary>
        /// <remarks>
        /// A resolver is an object that implements "IResolve" interface
        /// </remarks>
        /// <example>
        /// Container.RegisterType(target, new InjectionConstructor(new InjectionParameter(resolver)), 
        ///                                new InjectionMethod("Method", new InjectionParameter(resolver)) , 
        ///                                new InjectionField("Field",   new InjectionParameter(resolver)), 
        ///                                new InjectionProperty("Property", new InjectionParameter(resolver)));
        /// </example>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Injected_Data))]
        public virtual void Injected_ByResolver(string test, Type type, string name, Type dependency, object expected)
        {
            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;
            // Arrange
            var resolver = new ValidatingResolver(expected);
            var parameter = new InjectionParameter(dependency, resolver);
            Container.RegisterType(target, name, GetInjectionValue(parameter));

            RegisterTypes();

            // Act
            var instance = Container.Resolve(target, name) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }


        /// <summary>
        /// Tests injecting dependencies by resolver wrapped in parameter from empty container
        /// </summary>
        [DataTestMethod]
        [DynamicData(nameof(Injected_Data))]
        public virtual void Injected_ByResolver_FromEmpty(string test, Type type, string name, Type dependency, object expected)
        {
            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;
            // Arrange
            var resolver = new ValidatingResolver(expected);
            var parameter = new InjectionParameter(dependency, resolver);
            Container.RegisterType(target, name, GetInjectionValue(parameter));

            // Act
            var instance = Container.Resolve(target, name) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }
    }
}
