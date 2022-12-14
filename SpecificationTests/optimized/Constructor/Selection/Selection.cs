using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Specification.Constructor.Selection
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        [Ignore]
        public void CtorSelectionUnregistered()
        {
            // Act
            var result0 = iUnity0.Resolve<MultiLevelType>();
            var result1 = iUnity1.Resolve<MultiLevelType>();
            var result2 = iUnity2.Resolve<MultiLevelType>();
            var result3 = iUnity3.Resolve<MultiLevelType>();
            var result4 = iUnity4.Resolve<MultiLevelType>();
            var result5 = iUnity5.Resolve<MultiLevelType>();

            // Validate
            Assert.IsNotNull(result0);
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result3);
            Assert.IsNotNull(result4);
            Assert.IsNotNull(result5);

            Assert.AreEqual(0, result0.Level);
            Assert.AreEqual(0, result1.Level);
            Assert.AreEqual(0, result2.Level);
            Assert.AreEqual(0, result3.Level);
            Assert.AreEqual(0, result4.Level);
            Assert.AreEqual(0, result5.Level);

            //Assert.IsInstanceOfType(result1.Param1, typeof(Level1));
            //Assert.IsInstanceOfType(result2.Param2, typeof(Level2));
            //Assert.IsInstanceOfType(result3.Param3, typeof(Level3));
            //Assert.IsInstanceOfType(result4.Param4, typeof(Level4));
            //Assert.IsInstanceOfType(result5.Param5, typeof(Level5));
        }

        [TestMethod]
        [Ignore]
        public void CtorSelectionUnregisteredReverse()
        {
            // Act
            var result5 = iUnity5.Resolve<MultiLevelType>();
            var result4 = iUnity4.Resolve<MultiLevelType>();
            var result3 = iUnity3.Resolve<MultiLevelType>();
            var result2 = iUnity2.Resolve<MultiLevelType>();
            var result1 = iUnity1.Resolve<MultiLevelType>();
            var result0 = iUnity0.Resolve<MultiLevelType>();

            // Validate
            Assert.IsNotNull(result0);
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result3);
            Assert.IsNotNull(result4);
            Assert.IsNotNull(result5);

            Assert.AreEqual(0, result0.Level);
            Assert.AreEqual(0, result1.Level);
            Assert.AreEqual(0, result2.Level);
            Assert.AreEqual(0, result3.Level);
            Assert.AreEqual(0, result4.Level);
            Assert.AreEqual(0, result5.Level);
        }
    }
}
