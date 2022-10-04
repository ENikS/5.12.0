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
        /// Tests injecting dependencies by type
        /// </summary>
        /// <example>
        /// Container.RegisterType(target, new InjectionConstructor(type), 
        ///                                new InjectionMethod("Method", type) , 
        ///                                new InjectionField("Field", type), 
        ///                                new InjectionProperty("Property", type));
        /// </example>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Registered_Data))]
        public virtual void Injected_Implicitly_ByType(string test, Type type, string name, Type dependency, object expected)
        {
            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;
            // Arrange
            Container.RegisterType(target, GetInjectionValue(dependency));
            
            RegisterTypes();

            // Act
            var instance = Container.Resolve(target, name) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }


        #region Required

        /// <summary>
        /// Tests injecting by type required dependencies from empty container
        /// </summary>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Required_Data))]
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void Injected_Implicitly_Required(string test, Type type, string name, Type dependency, object expected)
        {
            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;
            // Arrange
            Container.RegisterType(target, GetInjectionValue(dependency));

            // Act
            _ = Container.Resolve(target, name) as PatternBase;
        }

        #endregion


        #region Optional

        /// <summary>
        /// Tests injecting by type optional dependencies from empty container
        /// </summary>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Optional_Data))]
        public virtual void Injected_Implicitly_Optional(string test, Type type, string name, Type dependency, object expected)
        {
            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;
            // Arrange
            Container.RegisterType(target, GetInjectionOptional(dependency));

            // Act
            var instance = Container.Resolve(target, name) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        #endregion


        #region With Default

        /// <summary>
        /// Tests injecting by type dependencies with default from empty container
        /// </summary>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Default_Data))]
        public virtual void Injected_Implicitly_Default(string test, Type type, string name, Type dependency, object expected)
        {
            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;
            // Arrange
            Container.RegisterType(target, GetInjectionValue(dependency));

            // Act
            var instance = Container.Resolve(target, name) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        #endregion
    }
}
