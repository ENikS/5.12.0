using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Specification.Pattern
{
    public partial class Constructors
    {
#if !V4
        // Constructors cann't be injected by name
        public override void Injected_ByName(string test, Type type, string name, Type dependency, object expected) { }
        public override void Injected_ByName_Required(string test, Type type, string name, Type dependency, object expected) { }
        public override void Injected_ByName_Optional(string test, Type type, string name, Type dependency, object expected) { }
        public override void Injected_ByName_Default(string test, Type type, string name, Type dependency, object expected) { }
#endif
    }
}
