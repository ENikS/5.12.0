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
        [DynamicData(nameof(Implicit_WithDefault_Data))]
        public virtual void Implicit_WithDefault(string test, Type type, string name, Type dependency, object expected)
        {
            // Act
            var instance = Container.Resolve(type, name) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        // Test Data
        public static IEnumerable<object[]> Implicit_WithDefault_Data
        {
            get
            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                          Test Name           Type                    Name    Dependency      Expected

                yield return new object[] { "Default_Value",    PocoType_Default_Value, null,   typeof(int),    DefaultInt       };
                yield return new object[] { "Default_Class",    PocoType_Default_Class, null,   typeof(string), DefaultString    };
            }
        }
    }
}
