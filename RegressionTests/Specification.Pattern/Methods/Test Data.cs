using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Specification.Pattern
{
    public partial class Methods
    {
        #region No Default

        public class Implicit_Dependency_Value : PatternBase
        {
            [InjectionMethod]
            public void Method(int value) => Value = value;
        }

        public class Implicit_Dependency_Class : PatternBase
        {
            [InjectionMethod]
            public void Method(Unresolvable value) => Value = value;
        }

        public class Implicit_Dependency_Dynamic : PatternBase
        {
            [InjectionMethod]
            public void Method(dynamic value) => Value = value;
        }

        public class Implicit_Dependency_Generic<T> : PatternBase
        {
            [InjectionMethod]
            public void Method(T value) => Value = value;
        }

        public class Implicit_WithDefault_Value : PatternBase
        {
            [InjectionMethod]
            public void Method(int value = DefaultInt) => Value = value;
        }

        public class Implicit_WithDefault_Class : PatternBase
        {
            [InjectionMethod]
            public void Method(string value = DefaultString) => Value = value;
        }

        #endregion


        #region Required

        public class Required_Dependency_Value : PatternBase
        {
            [InjectionMethod]
            public void Method([Dependency] int value) => Value = value;
        }

        public class Required_Dependency_Class : PatternBase
        {
            [InjectionMethod]
            public void Method([Dependency] Unresolvable value) => Value = value;
        }

        public class Required_Dependency_Dynamic : PatternBase
        {
            [InjectionMethod]
            public void Method([Dependency] dynamic value) => Value = value;
        }

        public class Required_Dependency_Generic<T> : PatternBase
        {
            [InjectionMethod]
            public void Method([Dependency] T value) => Value = value;
        }

        public class Required_Dependency_Named<T> : PatternBase
        {
            [InjectionMethod]
            public void Method([Dependency(Name)] T value) => Value = value;
        }

        public class Required_WithDefault_Value : PatternBase
        {
            [InjectionMethod]
            public void Method([Dependency] int value = DefaultInt) => Value = value;
        }

        public class Required_WithDefault_Class : PatternBase
        {
            [InjectionMethod]
            public void Method([Dependency] string value = DefaultString) => Value = value;
        }

        #endregion


        #region Optional

        public class Optional_Dependency_Value : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency] int value) => Value = value;
        }

        public class Optional_Dependency_Class : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency] Unresolvable value) => Value = value;
        }

        public class Optional_Dependency_Dynamic : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency] dynamic value) => Value = value;
        }

        public class Optional_Dependency_Generic<T> : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency] T value) => Value = value;
        }

        public class Optional_Dependency_Named<T> : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency(Name)] T value) => Value = value;
        }

        public class Optional_WithDefault_Value : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency] int value = DefaultInt) => Value = value;
        }

        public class Optional_WithDefault_Class : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency] string value = DefaultString) => Value = value;
        }

        public class Optional_Dependency_Value_Named : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency(Name)] int value) => Value = value;
        }

        public class Optional_Dependency_Class_Named : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency(Name)] Unresolvable value) => Value = value;
        }

        #endregion




        #region Unsupported


        public class Implicit_Dependency_Ref : PatternBase
        {
            [InjectionMethod]
            public void Method(ref Unresolvable value) => Value = value;
        }

        public class Implicit_Dependency_Out : PatternBase
        {
            [InjectionMethod]
            public void Method(out Unresolvable value) => value = null;
        }

        public class Implicit_Generic_Ref<T> : PatternBase where T : class
        {
            [InjectionMethod]
            public void Method(ref T value) => Value = value;
        }

        public class Implicit_Generic_Out<T> : PatternBase where T : class
        {
            [InjectionMethod]
            public void Method(out T value) => value = null;
        }



        public class Required_Dependency_Ref : PatternBase
        {
            [InjectionMethod]
            public void Method([Dependency] ref Unresolvable value) => Value = value;
        }

        public class Required_Dependency_Out : PatternBase
        {
            [InjectionMethod]
            public void Method([Dependency] out Unresolvable value) => value = null;
        }

        public class Required_Generic_Ref<T> : PatternBase where T : class
        {
            [InjectionMethod]
            public void Method([Dependency] ref T value) => Value = value;
        }

        public class Required_Generic_Out<T> : PatternBase where T : class
        {
            [InjectionMethod]
            public void Method([Dependency] out T value) => value = null;
        }



        public class Optional_Dependency_Ref : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency] ref Unresolvable value) => Value = value;
        }

        public class Optional_Dependency_Out : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency] out Unresolvable value) => value = null;
        }

        public class Optional_Generic_Ref<T> : PatternBase where T : class
        {
            [InjectionMethod]
            public void Method([OptionalDependency] ref T value) => Value = value;
        }

        public class Optional_Generic_Out<T> : PatternBase where T : class
        {
            [InjectionMethod]
            public void Method([OptionalDependency] out T value) => value = null;
        }

        #endregion
    }
}
