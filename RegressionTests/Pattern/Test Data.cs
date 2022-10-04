using System;

namespace Specification
{

    public abstract partial class VerificationPattern
    {
        public struct TestStruct
        {
            public int Integer;
            public object Instance;

            public TestStruct(int value, object instance)
            {
                Integer = value;
                Instance = instance;
            }
        }

        public ref struct TestRefStruct
        {
            public int Integer;
            public object Object;
        }

        public abstract class PatternBase
        {
            public virtual object Value { get; protected set; }

        }



        #region Unresolvable

        public class Unresolvable
        {
            public readonly string ID;

            protected Unresolvable(string id) { ID = id; }

            public static Unresolvable Create(string name) => new Unresolvable(name);

            public override string ToString()
            {
                return $"Unresolvable.{ID}";
            }
        }

        public class SubUnresolvable : Unresolvable
        {
            private SubUnresolvable(string id)
                : base(id)
            {
            }
            public override string ToString()
            {
                return $"SubUnresolvable.{ID}";
            }
            public new static SubUnresolvable Create(string name) => new SubUnresolvable(name);
        }

        #endregion
    }
}


