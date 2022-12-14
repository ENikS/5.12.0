

using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.Interception.Utilities;

namespace Unity.Interception.PolicyInjection.MatchingRules
{
    /// <summary>
    /// An <see cref="IMatchingRule"/> that matches members in a given namespace. You can
    /// specify either a single namespace (e.g. <c>System.Data</c>) or a namespace root
    /// (e.g. <c>System.Data.*</c> to match types in that namespace or below.
    /// </summary>
    public class NamespaceMatchingRule : IMatchingRule
    {
        private readonly List<NamespaceMatchingInfo> _matches;

        /// <summary>
        /// Create a new <see cref="NamespaceMatchingRule"/> that matches the given
        /// namespace.
        /// </summary>
        /// <param name="namespaceName">namespace name to match. Comparison is case sensitive.</param>
        public NamespaceMatchingRule(string namespaceName)
            : this(namespaceName, false)
        {
        }

        /// <summary>
        /// Create a new <see cref="NamespaceMatchingRule"/> that matches the given
        /// namespace.
        /// </summary>
        /// <param name="namespaceName">namespace name to match.</param>
        /// <param name="ignoreCase">If false, comparison is case sensitive. If true, comparison is case insensitive.</param>
        public NamespaceMatchingRule(string namespaceName, bool ignoreCase)
            : this(new[] { new MatchingInfo(namespaceName, ignoreCase) })
        {
        }

        /// <summary>
        /// Create a new <see cref="NamespaceMatchingRule"/> that matches any of
        /// the given namespace names.
        /// </summary>
        /// <param name="matches">Collection of namespace names to match.</param>
        public NamespaceMatchingRule(IEnumerable<MatchingInfo> matches)
        {
            Guard.ArgumentNotNull(matches, "matches");

            _matches = new List<NamespaceMatchingInfo>();
            foreach (MatchingInfo match in matches)
            {
                _matches.Add(new NamespaceMatchingInfo(match.Match, match.IgnoreCase));
            }
        }

        /// <summary>
        /// Check to see if the given <paramref name="member"/> is in a namespace
        /// matched by any of our given namespace names.
        /// </summary>
        /// <param name="member">member to check.</param>
        /// <returns>True if member is contained in a matching namespace, false if not.</returns>
        public bool Matches(MethodBase member)
        {
            return
                _matches.Exists(
                    delegate(NamespaceMatchingInfo match) { return match.Matches(member.DeclaringType); });
        }

        /// <summary>
        /// A helper class that encapsulates the name to match, case sensitivity flag,
        /// and the wildcard rules for matching namespaces.
        /// </summary>
        private class NamespaceMatchingInfo : MatchingInfo
        {
            private bool wildCard;
            private const string WildCardString = ".*";

            /// <summary>
            /// Construct a new <see cref="NamespaceMatchingInfo"/> that matches the
            /// given namespace name.
            /// </summary>
            /// <param name="match">Namespace name to match.</param>
            /// <param name="ignoreCase">If false, comparison is case sensitive. If true, comparison is case insensitive.</param>
            public NamespaceMatchingInfo(string match, bool ignoreCase)
                : base(match, ignoreCase)
            {
                if (NamespaceName.EndsWith(WildCardString, StringComparison.Ordinal))
                {
                    NamespaceName = NamespaceName.Substring(0, NamespaceName.Length - WildCardString.Length);
                    wildCard = true;
                }
            }

            private string NamespaceName
            {
                get { return Match; }
                set { Match = value; }
            }

            /// <summary>
            /// Check if the given type <paramref name="t"/> is in a matching namespace.
            /// </summary>
            /// <param name="t">Type to check.</param>
            /// <returns>True if type is in a matching namespace, false if not.</returns>
            public bool Matches(Type t)
            {
                if (t == null)
                {
                    return string.IsNullOrEmpty(NamespaceName);
                }

                StringComparison comparison = IgnoreCase
                    ? StringComparison.OrdinalIgnoreCase
                    : StringComparison.Ordinal;

                bool exactMatch =
                    string.Compare(
                        t.Namespace,
                        NamespaceName,
                        comparison)
                    == 0;

                if (wildCard)
                {
                    return exactMatch ||
                        t.Namespace.StartsWith(NamespaceName + ".", comparison);
                }
                return exactMatch;
            }
        }
    }
}
