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
        /// Tests injecting dependencies by resolution parameter
        /// </summary>
        /// <example>
        /// Container.RegisterType(target, new InjectionConstructor(new ResolvedParameter(type)), 
        ///                                new InjectionMethod("Method", new ResolvedParameter(type)) , 
        ///                                new InjectionField("Field",   new ResolvedParameter(type)), 
        ///                                new InjectionProperty("Property", new ResolvedParameter(type)));
        /// </example>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Registered_Data))]
        public virtual void Injected_ByResolving(string test, Type type, string name, Type dependency, object expected)
        {
            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;
            // Arrange
            Container.RegisterType(target, GetResolvedMember(dependency, name));

            RegisterTypes();

            // Act
            var instance = Container.Resolve(target) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        #endregion


        #region Required

        /// <summary>
        /// Tests injecting by resolution required dependencies from empty container
        /// </summary>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Required_Data))]
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void Injected_ByResolving_Required(string test, Type type, string name, Type dependency, object expected)
        {
            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;
            // Arrange
            Container.RegisterType(target, GetResolvedMember(dependency, name));

            // Act
            _ = Container.Resolve(target, name) as PatternBase;
        }

        #endregion


        #region Optional

        /// <summary>
        /// Tests injecting by resolution optional dependencies from empty container
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
        public virtual void Injected_ByResolving_Optional(string test, Type type, string name, Type dependency, object expected)
        {
            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;
            // Arrange
            Container.RegisterType(target, GetOptionalMember(dependency, name));

            // Act
            var instance = Container.Resolve(target) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        #endregion


        #region With Default

        /// <summary>
        /// Tests injecting by resolution dependencies with default from empty container
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
        public virtual void Injected_ByResolving_Default(string test, Type type, string name, Type dependency, object expected)
        {
            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;
            // Arrange
            Container.RegisterType(target, GetResolvedMember(dependency, name));

            // Act
            var instance = Container.Resolve(target) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        #endregion
    }
}
