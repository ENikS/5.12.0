using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Public.API
{
    public partial class IUnityContainer_Registrations
    {

        // http://unity.codeplex.com/WorkItem/View.aspx?WorkItemId=6053
        [TestMethod]
        public void ResolveAllWithChildDoesNotRepeatOverriddenRegistrations()
        {
            var expected = new HashSet<string>(new[] { "string1", "string20", "string30" });

            Container
                .RegisterInstance("str1", "string1")
                .RegisterInstance("str2", "string2");

            var child = Container.CreateChildContainer()
                .RegisterInstance("str2", "string20")
                .RegisterInstance("str3", "string30");

            var array = child.ResolveAll<string>();
            var actual = new HashSet<string>(array);

            Assert.IsTrue(actual.SetEquals(expected));
        }
    }
}
