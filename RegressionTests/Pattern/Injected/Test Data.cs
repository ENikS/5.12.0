using System.Collections.Generic;
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
        // Passing Registered Data
        public static IEnumerable<object[]> Injected_Data
        {
            get
            {
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                          Test Name                   Type                        Name    Dependency              Expected

                // Implicit
                                                                        
                yield return new object[] { "Implicit_Value",           PocoType,                   null,   typeof(int),            InjectedInt         };
                yield return new object[] { "Implicit_Class",           PocoType,                   null,   typeof(Unresolvable),   InjectedSingleton   };

                yield return new object[] { "Implicit_Value_Type_Name", PocoType,                   null,   typeof(int),            InjectedInt         };
                yield return new object[] { "Implicit_Class_Type_Name", PocoType,                   null,   typeof(Unresolvable),   InjectedSingleton   };

                yield return new object[] { "Implicit_Default_Value",   PocoType_Default_Value,     null,   typeof(int),            InjectedInt         };
                yield return new object[] { "Implicit_Default_Class",   PocoType_Default_Class,     null,   typeof(string),         InjectedString      };

                // Required

                yield return new object[] { "Required_Value",           Required,                   null,   typeof(int),            InjectedInt         };
                yield return new object[] { "Required_Class",           Required,                   null,   typeof(Unresolvable),   InjectedSingleton   };

                yield return new object[] { "Required_Value_Named",     Required_Named,             Name,   typeof(int),            InjectedInt         };
                yield return new object[] { "Required_Class_Named",     Required_Named,             Name,   typeof(Unresolvable),   InjectedSingleton   };

                yield return new object[] { "Required_Default_Value",   Required_Default_Value,     null,   typeof(int),            InjectedInt         };
                yield return new object[] { "Required_Default_Class",   Required_Default_String,    null,   typeof(string),         InjectedString      };

                //// Optional

                yield return new object[] { "Optional_Value",           Optional,                   null,   typeof(int),            InjectedInt         };
                yield return new object[] { "Optional_Class",           Optional,                   null,   typeof(Unresolvable),   InjectedSingleton   };

                yield return new object[] { "Optional_Value_Named",     Optional_Named,             Name,   typeof(int),            InjectedInt         };
                yield return new object[] { "Optional_Class_Named",     Optional_Named,             Name,   typeof(Unresolvable),   InjectedSingleton   };

                yield return new object[] { "Optional_Default_Value",   Optional_Default_Value,     null,   typeof(int),            InjectedInt         };
                yield return new object[] { "Optional_Default_Class",   Optional_Default_Class,     null,   typeof(string),         InjectedString      };
            }
        }



        // Passing Registered Data
        public static IEnumerable<object[]> Registered_Data
        {
            get
            {
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                          Test Name                   Type                        Name    Dependency              Expected

                // Implicit
                                                                        
                yield return new object[] { "Implicit_Value",           PocoType,                   null,   typeof(int),            RegisteredInt           };
                yield return new object[] { "Implicit_Class",           PocoType,                   null,   typeof(Unresolvable),   RegisteredUnresolvable  };
                yield return new object[] { "Implicit_Struct",          PocoType,                   null,   typeof(TestStruct),     RegisteredStruct        };

                yield return new object[] { "Implicit_Value_Type_Name", PocoType,                   null,   typeof(int),            RegisteredInt           };
                yield return new object[] { "Implicit_Class_Type_Name", PocoType,                   null,   typeof(Unresolvable),   RegisteredUnresolvable  };

                yield return new object[] { "Implicit_Default_Value",   PocoType_Default_Value,     null,   typeof(int),            RegisteredInt           };
                yield return new object[] { "Implicit_Default_Class",   PocoType_Default_Class,     null,   typeof(string),         RegisteredString        };

                // Required

                yield return new object[] { "Required_Value",           Required,                   null,   typeof(int),            RegisteredInt           };
                yield return new object[] { "Required_Class",           Required,                   null,   typeof(Unresolvable),   RegisteredUnresolvable  };
                yield return new object[] { "Required_Struct",          Required,                   null,   typeof(TestStruct),     RegisteredStruct        };

                yield return new object[] { "Required_Value_Named",     Required_Named,             Name,   typeof(int),            NamedInt                };
                yield return new object[] { "Required_Class_Named",     Required_Named,             Name,   typeof(Unresolvable),   NamedSingleton          };

                yield return new object[] { "Required_Default_Value",   Required_Default_Value,     null,   typeof(int),            RegisteredInt           };
                yield return new object[] { "Required_Default_Class",   Required_Default_String,    null,   typeof(string),         RegisteredString        };

                //// Optional

                yield return new object[] { "Optional_Value",           Optional,                   null,   typeof(int),            RegisteredInt           };
                yield return new object[] { "Optional_Class",           Optional,                   null,   typeof(Unresolvable),   RegisteredUnresolvable  };
                yield return new object[] { "Optional_Struct",          Optional,                   null,   typeof(TestStruct),     RegisteredStruct        };

                yield return new object[] { "Optional_Value_Named",     Optional_Named,             Name,   typeof(int),            NamedInt                };
                yield return new object[] { "Optional_Class_Named",     Optional_Named,             Name,   typeof(Unresolvable),   NamedSingleton          };

                yield return new object[] { "Optional_Default_Value",   Optional_Default_Value,     null,   typeof(int),            RegisteredInt           };
                yield return new object[] { "Optional_Default_Class",   Optional_Default_Class,     null,   typeof(string),         RegisteredString        };
            }
        }

        // With Default Data
        public static IEnumerable<object[]> Default_Data
        {
            get
            {
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                          Test Name                   Type                        Name    Dependency              Expected      
                                                                                                                                                  
                yield return new object[] { "Implicit_Default_Value",   PocoType_Default_Value,     null,   typeof(int),            DefaultInt    };
                yield return new object[] { "Implicit_Default_Class",   PocoType_Default_Class,     null,   typeof(string),         DefaultString };
                                                                                                                                                  
                yield return new object[] { "Required_Default_Value",   Required_Default_Value,     null,   typeof(int),            DefaultInt    };
                yield return new object[] { "Required_Default_Class",   Required_Default_String,    null,   typeof(string),         DefaultString };
                                                                                                                                                  
                yield return new object[] { "Optional_Default_Value",   Optional_Default_Value,     null,   typeof(int),            DefaultInt    };
                yield return new object[] { "Optional_Default_Class",   Optional_Default_Class,     null,   typeof(string),         DefaultString };
            }
        }


        // Optional Data
        public static IEnumerable<object[]> Optional_Data
        {
            get
            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                          Test Name                   Type                        Name    Dependency              Expected

                yield return new object[] { "Optional_Value",           Optional,                   null,   typeof(int),            0      };
                yield return new object[] { "Optional_Class",           Optional,                   null,   typeof(Unresolvable),   null   };

                yield return new object[] { "Optional_Value_Named",     Optional_Named,             null,   typeof(int),            0      };
                yield return new object[] { "Optional_Class_Named",     Optional_Named,             null,   typeof(Unresolvable),   null   };
            }
        }

        // Required Data

        public static IEnumerable<object[]> Required_Data
        {
            get
            {
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                          Test Name                   Type                        Name    Dependency           Expected

                yield return new object[] { "Required_Value",           PocoType,                   null,   typeof(int),          0    };
                yield return new object[] { "Required_Class",           PocoType,                   null,   typeof(Unresolvable), null };
              //yield return new object[] { "Required_Struct",          PocoType,                   null,   typeof(TestStruct),      };

                yield return new object[] { "Required_Value_Type_Name", PocoType,                   Name,   typeof(int),          0    };
                yield return new object[] { "Required_Class_Type_Name", PocoType,                   Name,   typeof(Unresolvable), null };

                yield return new object[] { "Required_Value",           Required,                   null,   typeof(int),          0    };
                yield return new object[] { "Required_Class",           Required,                   null,   typeof(Unresolvable), null };
              //yield return new object[] { "Required_Struct",          Required,                   null,   typeof(TestStruct),      };

                yield return new object[] { "Required_Value_Named",     Required_Named,             null,   typeof(int),          0    };
                yield return new object[] { "Required_Class_Named",     Required_Named,             null,   typeof(Unresolvable), null };
            }
        }

    }
}
