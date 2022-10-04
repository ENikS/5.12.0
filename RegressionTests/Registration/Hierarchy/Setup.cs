using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Registrations
{
    [TestClass]
    public partial class Hierarchy
    {
        protected const string Name = "name";
        protected IUnityContainer Container;
        protected IUnityContainer iUnity0;
        protected IUnityContainer iUnity1;
        protected IUnityContainer iUnity2;
        protected IUnityContainer iUnity3;
        protected IUnityContainer iUnity4;
        protected IUnityContainer iUnity5;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Container = new UnityContainer();
            iUnity0 = Container;
            iUnity1 = iUnity0.CreateChildContainer().RegisterInstance(typeof(ILevel1), new Level1());
            iUnity2 = iUnity1.CreateChildContainer().RegisterInstance(typeof(ILevel2), new Level2());
            iUnity3 = iUnity2.CreateChildContainer().RegisterInstance(typeof(ILevel3), new Level3());
            iUnity4 = iUnity3.CreateChildContainer().RegisterInstance(typeof(ILevel4), new Level4());
            iUnity5 = iUnity4.CreateChildContainer().RegisterInstance(typeof(ILevel5), new Level5());
        }

        #region Test Data

        public class IUnityContainerInjectionClass
        {
            [Dependency]
            public IUnityContainer Container { get; set; }
        }

        public interface ITemporary
        {
        }

        public class Temp : ITemporary
        {
        }

        public class Temporary : ITemporary
        {
        }

        public class SpecialTemp : ITemporary //Second level
        {
        }

#pragma warning disable CA1063 // Implement IDisposable Correctly
        public class MyDisposableObject : IDisposable
        {
            private bool wasDisposed = false;

            public bool WasDisposed
            {
                get { return wasDisposed; }
                set { wasDisposed = value; }
            }

            public void Dispose()
            {
                wasDisposed = true;
            }
        }
#pragma warning restore CA1063 // Implement IDisposable Correctly

        public interface ILevel1 { }
        public interface ILevel2 { }
        public interface ILevel3 { }
        public interface ILevel4 { }
        public interface ILevel5 { }

        public class Level1 : ILevel1 { }
        public class Level2 : ILevel2 { }
        public class Level3 : ILevel3 { }
        public class Level4 : ILevel4 { }
        public class Level5 : ILevel5 { }

        public class Other1 : ILevel1 { }
        public class Other2 : ILevel2 { }
        public class Other3 : ILevel3 { }
        public class Other4 : ILevel4 { }
        public class Other5 : ILevel5 { }

        public class MultiLevelType
        {
            public MultiLevelType(IUnityContainer container)
            {
                Level = 0;
                Container = container;
            }

            public MultiLevelType(IUnityContainer container, ILevel1 one)
            {
                Level = 1;
                Container = container;
                Param1 = one;
            }

            public MultiLevelType(IUnityContainer container, ILevel1 one, ILevel2 two)
            {
                Level = 2;
                Container = container;
                Param1 = one;
                Param2 = two;
            }

            public MultiLevelType(IUnityContainer container, ILevel1 one, ILevel2 two, ILevel3 three)
            {
                Level = 3;
                Container = container;
                Param1 = one;
                Param2 = two;
                Param3 = three;
            }

            public MultiLevelType(IUnityContainer container, ILevel1 one, ILevel2 two, ILevel3 three, ILevel4 four)
            {
                Level = 4;
                Container = container;
                Param1 = one;
                Param2 = two;
                Param3 = three;
                Param4 = four;
            }

            public MultiLevelType(IUnityContainer container, ILevel1 one, ILevel2 two, ILevel3 three, ILevel4 four, ILevel5 five)
            {
                Level = 5;
                Container = container;
                Param1 = one;
                Param2 = two;
                Param3 = three;
                Param4 = four;
                Param5 = five;
            }

            public int Level { get; }
            public IUnityContainer Container { get; }
            public ILevel1 Param1 { get; }
            public ILevel2 Param2 { get; }
            public ILevel3 Param3 { get; }
            public ILevel4 Param4 { get; }
            public ILevel5 Param5 { get; }
        }

        public class Unresolvable
        {
            private Unresolvable() { }

            public static Unresolvable Create() => new Unresolvable();
        }
        #endregion
    }
}
