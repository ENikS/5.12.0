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
    public abstract partial class VerificationPattern
    {
        [DataTestMethod]
        [DynamicData(nameof(Annotated_Resolvable_Data))]
        public virtual void Annotated_Resolvable(string test, Type type, string name, Type dependency, object expected)
        {
            // Arrange
            RegisterTypes();

            // Act
            var instance = Container.Resolve(type, name) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        [DataTestMethod]
        [DynamicData(nameof(Annotated_Resolvable_Data))]
        public virtual void Annotated_Resolvable_Registered(string test, Type type, string name, Type dependency, object expected)
        {
            // Arrange
            RegisterTypes();
            Container.RegisterType(type, name);

            // Act
            var instance = Container.Resolve(type, name) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }


        // Test Data
        public static IEnumerable<object[]> Annotated_Resolvable_Data
        {
            get
            {
                var Required_Value  = Required.MakeGenericType(typeof(int));
                var Required_Ref    = Required.MakeGenericType(typeof(Unresolvable));
                var Required_Struct = Required.MakeGenericType(typeof(TestStruct));

                var Optional_Value  = Optional.MakeGenericType(typeof(int));
                var Optional_Ref    = Optional.MakeGenericType(typeof(Unresolvable));
                var Optional_Struct = Optional.MakeGenericType(typeof(TestStruct));

                var Required_Named_Value  = Required_Named.MakeGenericType(typeof(int));
                var Required_Named_Ref    = Required_Named.MakeGenericType(typeof(Unresolvable));
                var Optional_Named_Value  = Optional_Named.MakeGenericType(typeof(int));
                var Optional_Named_Ref    = Optional_Named.MakeGenericType(typeof(Unresolvable));

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                          Test Name                   Type                    Name    Dependency              Expected

                // Required

                yield return new object[] { "Required_Value",           Required_Value,         null,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Required_Class",           Required_Ref,           null,   typeof(Unresolvable),   RegisteredUnresolvable           };
                yield return new object[] { "Required_Struct",          Required_Struct,        null,   typeof(TestStruct),     RegisteredStruct    };

                yield return new object[] { "Required_Value_Named",     Required_Named_Value,   null,   typeof(int),            NamedInt            };
                yield return new object[] { "Required_Class_Named",     Required_Named_Ref,     null,   typeof(Unresolvable),   NamedSingleton      };

                yield return new object[] { "Required_Default_Value",   Required_Default_Value, null,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Required_Default_Class",   Required_Default_String, null,   typeof(Unresolvable),   RegisteredString    };

                // Optional

                yield return new object[] { "Optional_Value",           Optional_Value,         null,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Optional_Class",           Optional_Ref,           null,   typeof(Unresolvable),   RegisteredUnresolvable           };
                yield return new object[] { "Optional_Struct",          Optional_Struct,        null,   typeof(TestStruct),     RegisteredStruct    };

                yield return new object[] { "Optional_Value_Named",     Optional_Named_Value,   null,   typeof(int),            NamedInt            };
                yield return new object[] { "Optional_Class_Named",     Optional_Named_Ref,     null,   typeof(Unresolvable),   NamedSingleton      };

                yield return new object[] { "Optional_Default_Value",   Optional_Default_Value, null,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Optional_Default_Class",   Optional_Default_Class, null,   typeof(Unresolvable),   RegisteredString    };
            }
        }
    }
}
