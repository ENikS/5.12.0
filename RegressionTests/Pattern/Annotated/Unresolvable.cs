using Microsoft.VisualStudio.TestTools.UnitTesting;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    public abstract partial class VerificationPattern
    {
        /// <summary>
        /// Tests invalid parameter types
        /// </summary>
        /// <example>
        /// 
        /// public class PocoType 
        /// {
        ///     [InjectionConstructor]
        ///     public PocoType(ref int value)
        ///     
        ///     [InjectionMethod]
        ///     public void Method(out int value)
        /// }
        /// 
        /// </example>
        /// <param name="target">Contract name</param>
        [DataTestMethod]
        [DataRow("Required_Dependency_Ref")]
        [DataRow("Required_Dependency_Out")]
#if V6 // TODO: Create an issue
        [DataRow("Optional_Dependency_Ref")]
        [DataRow("Optional_Dependency_Out")]
#endif
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void Annotated_Parameters(string target)
        {
            var type = TargetType(target);

            // Make dependencies available
            RegisterTypes();

            // Act
            _ = Container.Resolve(type);
        }
    }
}
