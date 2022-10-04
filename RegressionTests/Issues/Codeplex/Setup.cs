using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Issues
{
    [TestClass]
    public partial class CodePlex
    {
        protected IUnityContainer Container;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Container = new UnityContainer();
        }
    }

    public static class CollectionAssertExtensions
    {
        public static void AreEqual(ICollection expected, ICollection actual)
        {
            AreEqual(expected, actual, new DefaultComparer());
        }

        public static void AreEqual(ICollection expected, ICollection actual, IComparer comparer)
        {
            AreEqual(expected, actual, comparer, string.Empty);
        }

        public static void AreEqual(ICollection expected, ICollection actual, string message)
        {
            AreEqual(expected, actual, new DefaultComparer(), message);
        }

        public static void AreEqual(ICollection expected, ICollection actual, IComparer comparer, string message)
        {
            if (!AreCollectionsEqual(expected, actual, comparer, out var reason))
            {
                throw new AssertFailedException(string.Format(CultureInfo.CurrentCulture, "{0}({1})", message, reason));
            }
        }

        public static void AreEquivalent(ICollection expected, ICollection actual)
        {
            if (Equals(expected, actual))
            {
                return;
            }

            if (expected.Count != actual.Count)
            {
                throw new AssertFailedException("collections differ in size");
            }

            var expectedCounts = expected.Cast<object>().GroupBy(e => e).ToDictionary(g => g.Key, g => g.Count());
            var actualCounts = actual.Cast<object>().GroupBy(e => e).ToDictionary(g => g.Key, g => g.Count());

            foreach (var kvp in expectedCounts)
            {
                if (actualCounts.TryGetValue(kvp.Key, out var actualCount))
                {
                    if (actualCount != kvp.Value)
                    {
                        throw new AssertFailedException(string.Format(CultureInfo.InvariantCulture, "collections have different count for element {0}", kvp.Key));
                    }
                }
                else
                {
                    throw new AssertFailedException(string.Format(CultureInfo.InvariantCulture, "actual does not contain element {0}", kvp.Key));
                }
            }
        }

        private static bool AreCollectionsEqual(ICollection expected, ICollection actual, IComparer comparer, out string reason)
        {
            if (Equals(expected, actual))
            {
                reason = null;
                return true;
            }

            if (expected.Count != actual.Count)
            {
                reason = "collections differ in size";
                return false;
            }

            var expectedEnum = expected.GetEnumerator();
            var actualEnum = actual.GetEnumerator();
            for (int i = 0; expectedEnum.MoveNext() && actualEnum.MoveNext(); i++)
            {
                if (comparer.Compare(expectedEnum.Current, actualEnum.Current) != 0)
                {
                    reason = string.Format(CultureInfo.CurrentCulture, "collections differ at index {0}", i);
                    return false;
                }
            }

            reason = null;
            return true;
        }

        private class DefaultComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                return x.Equals(y) ? 0 : -1;
            }
        }
    }

    public static class EnumerableAssertionExtensions
    {
        public static void AssertContainsExactly<TItem>(this IEnumerable<TItem> items, params TItem[] expected)
        {
            CollectionAssertExtensions.AreEqual(expected, items.ToArray());
        }

        public static void AssertContainsInAnyOrder<TItem>(this IEnumerable<TItem> items, params TItem[] expected)
        {
            CollectionAssertExtensions.AreEquivalent(expected, items.ToArray());
        }

        public static void AssertTrueForAll<TItem>(this IEnumerable<TItem> items, Func<TItem, bool> predicate)
        {
            Assert.IsTrue(items.All(predicate));
        }

        public static void AssertTrueForAny<TItem>(this IEnumerable<TItem> items, Func<TItem, bool> predicate)
        {
            Assert.IsTrue(items.Any(predicate));
        }

        public static void AssertFalseForAll<TItem>(this IEnumerable<TItem> items, Func<TItem, bool> predicate)
        {
            Assert.IsFalse(items.All(predicate));
        }

        public static void AssertFalseForAny<TItem>(this IEnumerable<TItem> items, Func<TItem, bool> predicate)
        {
            Assert.IsFalse(items.Any(predicate));
        }

        public static void AssertHasItems<TItem>(this IEnumerable<TItem> items)
        {
            Assert.IsTrue(items.Any());
        }

        public static void AssertHasNoItems<TItem>(this IEnumerable<TItem> items)
        {
            Assert.IsFalse(items.Any());
        }
    }

}
