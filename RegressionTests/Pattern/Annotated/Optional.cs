using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    /// <summary>
    /// Testing optional dependencies.
    /// </summary>
    /// <example>
    /// 
    /// public class TypeWithOptionalDependencies
    /// {
    ///     [InjectionConstructor]
    ///     public TypeWithOptionalDependencies([OptionalDependency] int value)
    /// 
    ///     [OptionalDependency]
    ///     public int Field = 22;
    /// 
    ///     [OptionalDependency(name)]
    ///     public int Property { get; set; }
    /// 
    ///     [InjectionMethod]
    ///     public void Method([OptionalDependency(name)] int value = 55)
    /// }
    /// 
    /// </example>
    public abstract partial class VerificationPattern
    {
        /// <summary>
        /// Tests dependency resolution from empty container.
        /// </summary>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Annotated_Optional_Data))]
        public virtual void Annotated_Optional(string test, Type type, string name, Type dependency, object expected)
        {
            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        /// <summary>
        /// Tests dependency resolution from empty container.
        /// </summary>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Annotated_Optional_Data))]
        public virtual void Annotated_Optional_Registered(string test, Type type, string name, Type dependency, object expected)
        {
            // Arrange
            Container.RegisterType(type, name);

            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }


        // Test Data
        public static IEnumerable<object[]> Annotated_Optional_Data
        {
            get
            {
                var Optional_Value  = Optional.MakeGenericType(typeof(int));
                var Optional_Ref    = Optional.MakeGenericType(typeof(Unresolvable));
                var Optional_Struct = Optional.MakeGenericType(typeof(TestStruct));

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                          Test Name                   Type                    Name    Dependency           Expected

                yield return new object[] { "Optional_Value",           Optional_Value,         null,   typeof(int),          0 };
                yield return new object[] { "Optional_Class",           Optional_Ref,           null,   typeof(Unresolvable), null };
       // TODO: yield return new object[] { "Optional_Struct",          Optional_Struct,        null,   typeof(TestStruct),   RegisteredStruct };

                yield return new object[] { "Optional_Value_Named",     Optional_Value,         Name,   typeof(int),          0 };
                yield return new object[] { "Optional_Class_Named",     Optional_Ref,           Name,   typeof(Unresolvable), null };
                yield return new object[] { "Optional_Class_Null",      Optional_Ref,           Null,   typeof(Unresolvable), null };
            }
        }
    }
}
