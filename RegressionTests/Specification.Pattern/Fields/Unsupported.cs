#if V4
using Microsoft.Practices.Unity;
#else
using System;
using Unity;
using Unity.Injection;
#endif

namespace Specification.Pattern
{
    // Tests in this file are not applicable to Fields
    public partial class Fields
    {
        // Fields do not support implicit injection

        public override void Implicit_Resolvable(string test, Type type, string name, Type dependency, object expected) { }
#if !V4
        public override void Implicit_WithDefault(string test, Type type, string name, Type dependency, object expected) { }
#endif
        public override void Implicit_Unregistered(string test, Type type, string name, Type dependency, object expected) { }


        // Fields do not support parameters
        public override void Implicit_Parameters(string name) { }
        public override void Annotated_Parameters(string target) { }
        public override void Injected_Parameters(string target, string method) { }
    }
}
