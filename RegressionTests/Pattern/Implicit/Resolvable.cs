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
        [DynamicData(nameof(Implicit_Resolvable_Data))]
        public virtual void Implicit_Resolvable(string test, Type type, string name, Type dependency, object expected)
        {
            // Arrange
            RegisterTypes();

            // Act
            var instance = Container.Resolve(type, name) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        // Test Data
        public static IEnumerable<object[]> Implicit_Resolvable_Data
        {
            get
            {
                var Poco_Value      = PocoType.MakeGenericType(typeof(int));
                var Poco_Ref        = PocoType.MakeGenericType(typeof(Unresolvable));
                var Poco_Struct     = PocoType.MakeGenericType(typeof(TestStruct));

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                          Test Name                   Type                    Name    Dependency              Expected

                // Simple poco type

                yield return new object[] { "Generic_Value",            Poco_Value,             null,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Generic_Class",            Poco_Ref,               null,   typeof(Unresolvable),   RegisteredUnresolvable           };
                yield return new object[] { "Generic_Struct",           Poco_Struct,            null,   typeof(TestStruct),     RegisteredStruct    };
                
                yield return new object[] { "Value_Named",              Poco_Value,             Name,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Class_Named",              Poco_Ref,               Name,   typeof(Unresolvable),   RegisteredUnresolvable           };
                yield return new object[] { "Class_Null",               Poco_Ref,               Null,   typeof(Unresolvable),   RegisteredUnresolvable           };

                yield return new object[] { "Default_Value",            PocoType_Default_Value, null,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Default_Class",            PocoType_Default_Class, null,   typeof(Unresolvable),   RegisteredString    };
            }
        }
    }
}
