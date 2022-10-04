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
        #region Passing

        /// <summary>
        /// Tests injecting dependencies by optional generic parameter
        /// </summary>
        /// <example>
        /// Container.RegisterType(target, new InjectionConstructor(new OptionalGenericParameter(T)), 
        ///                                new InjectionMethod("Method", new OptionalGenericParameter(T)) , 
        ///                                new InjectionField("Field",   new OptionalGenericParameter(T)), 
        ///                                new InjectionProperty("Property", new OptionalGenericParameter(T)));
        /// </example>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Registered_Data))]
        public virtual void Injected_ByGenericOptional(string test, Type type, string name, Type dependency, object expected)
        {
            if (!type.IsGenericType) return;

            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;

            Type definition = type.IsGenericTypeDefinition
                            ? type
                            : type.GetGenericTypeDefinition();
            // Arrange
            RegisterTypes();

            Container.RegisterType(definition, GetGenericOptional(dependency, name));

            // Act
            var instance = Container.Resolve(target) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        #endregion


        #region Required

        /// <summary>
        /// Tests injecting by optional generic parameter required dependencies from empty container
        /// </summary>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Required_Data))]
        public virtual void Injected_ByGenericOptional_Required(string test, Type type, string name, Type dependency, object expected)
        {
            if (!type.IsGenericType) return;

            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;

            Type definition = type.IsGenericTypeDefinition
                            ? type
                            : type.GetGenericTypeDefinition();
            // Arrange
            Container.RegisterType(definition, GetGenericOptional(dependency, name));

            // Act
            var instance = Container.Resolve(target) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        #endregion


        #region Optional

        /// <summary>
        /// Tests injecting by optional generic parameter optional dependencies from empty container
        /// </summary>
        /// <remarks>
        /// The injection member overrides Optional attribute and throws if dependency 
        /// could not be resolved.
        /// </remarks>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Optional_Data))]
        public virtual void Injected_ByGenericOptional_Optional(string test, Type type, string name, Type dependency, object expected)
        {
            if (!type.IsGenericType) return;

            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;

            Type definition = type.IsGenericTypeDefinition
                            ? type
                            : type.GetGenericTypeDefinition();
            // Arrange
            Container.RegisterType(definition, GetGenericOptional(dependency, name));

            // Act
            var instance = Container.Resolve(target) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        #endregion


        #region With Default

        /// <summary>
        /// Tests injecting by optional generic parameter dependencies with default from empty container
        /// </summary>
        /// <remarks>
        /// Injected member overrides existing default and either is resolved or throws
        /// </remarks>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Default_Data))]
        public virtual void Injected_ByGenericOptional_Default(string test, Type type, string name, Type dependency, object expected)
        {
            if (!type.IsGenericType) return;

            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;

            Type definition = type.IsGenericTypeDefinition
                            ? type
                            : type.GetGenericTypeDefinition();
            // Arrange
            Container.RegisterType(definition, GetGenericOptional(dependency, name));

            // Act
            var instance = Container.Resolve(target) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        #endregion
    }
}
