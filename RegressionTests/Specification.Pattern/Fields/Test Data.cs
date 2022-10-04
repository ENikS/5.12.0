using System;
using System.ComponentModel;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Specification.Pattern
{
    public partial class Fields
    {
        #region Implicit

        public class Implicit_Dependency_Value : PatternBase
        {
            public int Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Implicit_Dependency_Class : PatternBase
        {
            public Unresolvable Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Implicit_Dependency_Dynamic : PatternBase
        {
            public dynamic Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Implicit_Dependency_Generic<T> : PatternBase
        {
            public T Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Implicit_WithDefault_Value : PatternBase
        {
            [DefaultValue(DefaultInt)]
            public int Field = DefaultInt;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Implicit_WithDefault_Class : PatternBase
        {
            [DefaultValue(DefaultString)]
            public string Field = DefaultString;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        #endregion


        #region Required

        public class Required_Dependency_Value : PatternBase
        {
            [Dependency] public int Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Required_Dependency_Class : PatternBase
        {
            [Dependency] public Unresolvable Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Required_Dependency_Dynamic : PatternBase
        {
            [Dependency] public dynamic Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Required_Dependency_Generic<T> : PatternBase
        {
            [Dependency] public T Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Required_Dependency_Named<T> : PatternBase
        {
            [Dependency(Name)] public T Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Required_WithDefault_Value : PatternBase
        {
            [Dependency]
            [DefaultValue(DefaultInt)]
            public int Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Required_WithDefault_Class : PatternBase
        {
            [Dependency]
            [DefaultValue(DefaultString)]
            public string Field = DefaultString;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }


        #endregion


        #region Optional

        public class Optional_Dependency_Value : PatternBase
        {
            [OptionalDependency] public int Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Optional_Dependency_Class : PatternBase
        {
            [OptionalDependency] public Unresolvable Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Optional_Dependency_Dynamic : PatternBase
        {
            [OptionalDependency] public dynamic Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Optional_Dependency_Generic<T> : PatternBase
        {
            [OptionalDependency] public T Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Optional_Dependency_Named<T> : PatternBase
        {
            [OptionalDependency(Name)] public T Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Optional_WithDefault_Value : PatternBase
        {
            [OptionalDependency] public int Field = DefaultInt;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Optional_WithDefault_Class : PatternBase
        {
            [OptionalDependency] public string Field = DefaultString;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        #endregion
    }
}
