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
    /// Testing resolution of required dependencies.
    /// </summary>
    /// <example>
    /// 
    /// public class TypeWithRequiredDependency
    /// {
    ///     [Dependency]
    ///     public int Field = 44;
    /// 
    ///     [Dependency(name)]
    ///     public int Property { get; set; }
    /// 
    ///     [InjectionConstructor]
    ///     public TypeWithRequiredDependency([Dependency] int value = 55) { }
    /// 
    ///     [InjectionMethod]
    ///     public void Method([Dependency(name)] int value) { }
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
        [DynamicData(nameof(Annotated_Required_Data))]
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void Annotated_Required(string test, Type type, string name, Type dependency, object expected)
        {
            // Act
            _ = Container.Resolve(type, name) as PatternBase;
        }


        // Test Data
        public static IEnumerable<object[]> Annotated_Required_Data
        {
            get
            {
                var Required_Value  = Required.MakeGenericType(typeof(int));
                var Required_Ref    = Required.MakeGenericType(typeof(Unresolvable));
                var Required_Struct = Required.MakeGenericType(typeof(TestStruct));

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                          Test Name                   Type                    Name    Dependency         Expected

                yield return new object[] { "Required_Value",           Required_Value,         null, typeof(int),          RegisteredInt };
                yield return new object[] { "Required_Class",           Required_Ref,           null, typeof(Unresolvable), RegisteredUnresolvable };
                yield return new object[] { "Required_Struct",          Required_Struct,        null, typeof(TestStruct),   RegisteredStruct };

                yield return new object[] { "Required_Value_Named",     Required_Value,         Name, typeof(int),          RegisteredInt };
                yield return new object[] { "Required_Class_Named",     Required_Ref,           Name, typeof(Unresolvable), RegisteredUnresolvable };
                yield return new object[] { "Required_Class_Null",      Required_Ref,           Null, typeof(Unresolvable), RegisteredUnresolvable };
            }
        }
    }
}
