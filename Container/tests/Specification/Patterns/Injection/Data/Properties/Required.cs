using System;
using Regression;
using Regression.Implicit.Properties;
#if UNITY_V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif


namespace Injection.Required.Properties
{
    public class Inherited_Import<TDependency> : BaselineTestType<TDependency> { }
    public class Inherited_Twice<TDependency> : Inherited_Import<TDependency> { }


    #region Validation

    public class PrivateTestType<TDependency>
        : PatternBaseType
    {
        [Dependency] private TDependency Property { get; set; }

        public override object Value { get => Property; protected set => throw new NotSupportedException(); }
        public override object Default => default(TDependency);
    }

    public class ProtectedTestType<TDependency>
        : PatternBaseType
    {
        [Dependency] protected TDependency Property { get; set; }

        public override object Value { get => Property; protected set => throw new NotSupportedException(); }
        public override object Default => default(TDependency);
    }

    public class InternalTestType<TDependency>
        : PatternBaseType
    {
        [Dependency] internal TDependency Property { get; set; }

        public override object Value { get => Property; protected set => throw new NotSupportedException(); }
        public override object Default => default(TDependency);
    }

    #endregion
}
