using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Regression.Tests;
#endif

namespace Specification
{
    public abstract partial class VerificationPattern
    {
        /// <summary>
        /// Tests injecting dependencies by factory
        /// </summary>
        /// <remarks>
        /// A resolver is an object that implements "IResolverFactory" interface
        /// </remarks>
        /// <example>
        /// Container.RegisterType(target, new InjectionConstructor(resolver), 
        ///                                new InjectionMethod("Method", resolver) , 
        ///                                new InjectionField("Field", resolver), 
        ///                                new InjectionProperty("Property", resolver));
        /// </example>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Injected_Data))]
        public virtual void Injected_Implicitly_ByFactory(string test, Type type, string name, Type dependency, object expected)
        {
            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;
            // Arrange
            Container.RegisterType(target, name, GetInjectionValue(new ValidatingResolverFactory(expected)));
            
            RegisterTypes();

            // Act
            var instance = Container.Resolve(target, name) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }


        #region From Empty

        /// <summary>
        /// Tests injecting by factory from empty container
        /// </summary>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Injected_Data))]
        public virtual void Injected_Implicitly_ByFactory_FromEmpty(string test, Type type, string name, Type dependency, object expected)
        {
            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;
            // Arrange
            Container.RegisterType(target, name, GetInjectionValue(new ValidatingResolverFactory(expected)));

            // Act
            var instance = Container.Resolve(target, name) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        #endregion
    }
}
