

using System.Reflection;
using System.Reflection.Emit;
using Unity.Interception.InterceptionBehaviors;

namespace Unity.Interception.Interceptors.TypeInterceptors.VirtualMethodInterception.InterceptingClassGeneration
{
    /// <summary>
    /// This class provides the code needed to implement the <see cref="IInterceptingProxy"/>
    /// interface on a class.
    /// </summary>
    internal static class InterceptingProxyImplementor
    {
        internal static FieldBuilder ImplementIInterceptingProxy(TypeBuilder typeBuilder)
        {
            typeBuilder.AddInterfaceImplementation(typeof(IInterceptingProxy));
            FieldBuilder proxyInterceptorPipelineField =
                typeBuilder.DefineField(
                    "pipeline",
                    typeof(InterceptionBehaviorPipeline),
                    FieldAttributes.Private | FieldAttributes.InitOnly);

            ImplementAddInterceptionBehavior(typeBuilder, proxyInterceptorPipelineField);

            return proxyInterceptorPipelineField;
        }

        private static void ImplementAddInterceptionBehavior(TypeBuilder typeBuilder, FieldInfo proxyInterceptorPipelineField)
        {
            // Declaring method builder
            // Method attributes
            const MethodAttributes MethodAttributes = MethodAttributes.Private | MethodAttributes.Virtual
                | MethodAttributes.Final | MethodAttributes.HideBySig
                    | MethodAttributes.NewSlot;

            MethodBuilder methodBuilder =
                typeBuilder.DefineMethod(nameof(IInterceptingProxy.AddInterceptionBehavior),
                    MethodAttributes);

            // Setting return type
            methodBuilder.SetReturnType(typeof(void));
            // Adding parameters
            methodBuilder.SetParameters(typeof(IInterceptionBehavior));
            // Parameter method
            methodBuilder.DefineParameter(1, ParameterAttributes.None, "interceptor");

            ILGenerator il = methodBuilder.GetILGenerator();
            // Writing body
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, proxyInterceptorPipelineField);
            il.Emit(OpCodes.Ldarg_1);
            il.EmitCall(OpCodes.Callvirt, InterceptionBehaviorPipelineMethods.Add, null);
            il.Emit(OpCodes.Ret);
            typeBuilder.DefineMethodOverride(methodBuilder, IInterceptingProxyMethods.AddInterceptionBehavior);
        }
    }
}
