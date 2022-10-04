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
    /// Testing resolution of dependencies with default value.
    /// </summary>
    /// <example>
    /// 
    /// public class TypeWithRequiredDependency
    /// {
    ///     [Dependency]
    ///     public int Field = 44;
    /// 
    ///     [OptionalDependency(name)]
    ///     public int Property { get; set; } = 33
    /// 
    ///     [InjectionConstructor]
    ///     public TypeWithRequiredDependency([OptionalDependency] int value = 55) { }
    /// 
    ///     [InjectionMethod]
    ///     public void Method([Dependency(name)] int value = 77) { }
    /// }
    ///      
    /// </example>
    public abstract partial class VerificationPattern
    {
        [DataTestMethod]
        [DynamicData(nameof(Annotated_WithDefault_Data))]
        public virtual void Annotated_Default(string test, Type type, string name, Type dependency, object expected)
        {
            // Act
            var instance = Container.Resolve(type, name) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        [DataTestMethod]
        [DynamicData(nameof(Annotated_WithDefault_Data))]
        public virtual void Annotated_Default_Registered(string test, Type type, string name, Type dependency, object expected)
        {
            // Act
            Container.RegisterType(type, name);

            var instance = Container.Resolve(type, name) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        // Test Data
        public static IEnumerable<object[]> Annotated_WithDefault_Data
        {
            get
            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                          Test Name           Type                     Name    Dependency      Expected

                yield return new object[] { "Required_Value",   Required_Default_Value,  null,   typeof(int),    DefaultInt       };
                yield return new object[] { "Required_Class",   Required_Default_String, null,   typeof(string), DefaultString    };

                yield return new object[] { "Optional_Value",   Optional_Default_Value,  null,   typeof(int),    DefaultInt       };
                yield return new object[] { "Optional_Class",   Optional_Default_Class,  null,   typeof(string), DefaultString    };
            }
        }
    }
}
