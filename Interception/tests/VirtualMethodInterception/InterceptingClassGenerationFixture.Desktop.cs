

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.Unity.InterceptionExtension.Tests.VirtualMethodInterception
{
    public partial class InterceptingClassGenerationFixture
    {
        [TestMethod]
        public void GeneratedTypeForAdditionalInterfaceWithMethodsHavingSignaturesMatchingMethodsInTheBaseClassIsVerifiable()
        {
            PermissionSet grantSet = new PermissionSet(PermissionState.None);
            grantSet.AddPermission(
                new SecurityPermission(
                    SecurityPermissionFlag.Execution
                    | SecurityPermissionFlag.ControlEvidence
                    | SecurityPermissionFlag.ControlPolicy));
            grantSet.AddPermission(
                new ReflectionPermission(ReflectionPermissionFlag.RestrictedMemberAccess
                    | ReflectionPermissionFlag.MemberAccess));
            grantSet.AddPermission(new FileIOPermission(PermissionState.Unrestricted));

            AppDomain sandbox =
                AppDomain.CreateDomain(
                    "sandbox",
                    AppDomain.CurrentDomain.Evidence,
                    new AppDomainSetup { ApplicationBase = AppDomain.CurrentDomain.BaseDirectory },
                    grantSet);

            sandbox.DoCallBack(() =>
            {
                global::Unity.Interception.Interceptors.TypeInterceptors.VirtualMethodInterception.InterceptingClassGeneration.InterceptingClassGenerator generator =
                    new global::Unity.Interception.Interceptors.TypeInterceptors.VirtualMethodInterception.InterceptingClassGeneration.InterceptingClassGenerator(typeof(MainType), typeof(IDoSomething), typeof(IDoSomethingToo));
                Type generatedType = generator.GenerateType();
            });
        }
    }
}
