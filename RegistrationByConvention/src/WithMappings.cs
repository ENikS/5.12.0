

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Unity.RegistrationByConvention
{
    /// <summary>
    /// Provides helper methods to map types to the types interfaces to which register them.
    /// </summary>
    public static class WithMappings
    {
        private static readonly Type[] EmptyTypes = new Type[0];

        /// <summary>
        /// Returns no types.
        /// </summary>
        /// <param name="implementationType">The type to register.</param>
        /// <returns>An empty enumeration.</returns>
        public static IEnumerable<Type> None(Type implementationType)
        {
            return EmptyTypes;
        }

        /// <summary>
        /// Returns an enumeration with the interface that matches the name of <paramref name="implementationType"/>.
        /// </summary>
        /// <param name="implementationType">The type to register.</param>
        /// <returns>An enumeration with the first interface matching the name of <paramref name="implementationType"/> (for example, if type is MyType, a matching interface is IMyType),
        /// or an empty enumeration if no such interface is found.</returns>
        public static IEnumerable<Type> FromMatchingInterface(Type implementationType)
        {
            var matchingInterfaceName = "I" + (implementationType ?? throw new ArgumentNullException(nameof(implementationType))).Name;

            var @interface = GetImplementedInterfacesToMap(implementationType)
                .FirstOrDefault(i => string.Equals(i.Name, matchingInterfaceName, StringComparison.Ordinal));

            return @interface != null ? new[] { @interface } : EmptyTypes;
        }

        /// <summary>
        /// Returns an enumeration with all the interfaces implemented by <paramref name="implementationType"/>.
        /// </summary>
        /// <param name="implementationType">The type to register.</param>
        /// <returns>An enumeration with all the interfaces implemented by the implementation type except <see cref="IDisposable"/>.</returns>
        public static IEnumerable<Type> FromAllInterfaces(Type implementationType)
        {
            return GetImplementedInterfacesToMap(implementationType ?? throw new ArgumentNullException(nameof(implementationType))).Where(i => i != typeof(IDisposable));
        }

        /// <summary>
        /// Returns an enumeration with all the interfaces implemented by <paramref name="implementationType"/> that belong to the same assembly as implementationType.
        /// </summary>
        /// <param name="implementationType">The type to register.</param>
        /// <returns>An enumeration with all the interfaces implemented by the implementation type that belong to the same assembly.</returns>
        public static IEnumerable<Type> FromAllInterfacesInSameAssembly(Type implementationType)
        {
            var implementationTypeAssembly = (implementationType ?? throw new ArgumentNullException(nameof(implementationType))).GetTypeInfo().Assembly;
            return GetImplementedInterfacesToMap(implementationType).Where(i => i.GetTypeInfo().Assembly == implementationTypeAssembly);
        }

        private static IEnumerable<Type> GetImplementedInterfacesToMap(Type type)
        {
            var typeInfo = type.GetTypeInfo();

            if (!typeInfo.IsGenericType)
            {
                return typeInfo.ImplementedInterfaces;
            }

            return !typeInfo.IsGenericTypeDefinition 
                ? typeInfo.ImplementedInterfaces 
                : FilterMatchingGenericInterfaces(typeInfo);
        }

        private static IEnumerable<Type> FilterMatchingGenericInterfaces(TypeInfo typeInfo)
        {
            var parameters = typeInfo.GenericTypeParameters;

            foreach (var @interface in typeInfo.ImplementedInterfaces)
            {
                var interfaceTypeInfo = @interface.GetTypeInfo();

                if (!(interfaceTypeInfo.IsGenericType && interfaceTypeInfo.ContainsGenericParameters))
                {
                    // skip non generic interfaces, or interfaces without generic parameters
                    continue;
                }

                if (GenericParametersMatch(parameters, interfaceTypeInfo.GenericTypeArguments))
                {
                    yield return interfaceTypeInfo.GetGenericTypeDefinition();
                }
            }
        }

        private static bool GenericParametersMatch(Type[] parameters, Type[] interfaceArguments)
        {
            if (parameters.Length != interfaceArguments.Length)
            {
                return false;
            }

            return !parameters.Where((t, i) => t != interfaceArguments[i]).Any();
        }
    }
}