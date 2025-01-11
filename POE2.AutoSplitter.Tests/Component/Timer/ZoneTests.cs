using Microsoft.VisualStudio.TestTools.UnitTesting;
using POE2.AutoSplitter.Component.Timer;
using System.Linq;

namespace POE2.AutoSplitter.Tests.Component.Timer
{
    [TestClass]
    public class ZoneTests
    {
        [TestMethod]
        public void TestNormalZones()
        {
            var riverbank = Zone.ZONES.First(z => z.ZoneCode == "G1_1");
            var ardura = Zone.ZONES.First(z => z.ZoneCode == "G2_1");
            var marsh = Zone.ZONES.First(z => z.ZoneCode == "G3_1");

            Assert.AreEqual("The Riverbank", riverbank.Name);
            Assert.AreEqual("Vastiri Outskirts", ardura.Name);
            Assert.AreEqual("Sandswept Marsh", marsh.Name);
        }

        [TestMethod]
        public void TestCruelZones()
        {
            var riverbank = Zone.ZONES.First(z => z.ZoneCode == "C_G1_1");
            var ardura = Zone.ZONES.First(z => z.ZoneCode == "C_G2_1");
            var marsh = Zone.ZONES.First(z => z.ZoneCode == "C_G3_1");

            Assert.AreEqual("The Riverbank", riverbank.Name);
            Assert.AreEqual("Vastiri Outskirts", ardura.Name);
            Assert.AreEqual("Sandswept Marsh", marsh.Name);
        }

        [TestMethod]
        public void TestZoneTypes()
        {
            var town = Zone.ZONES.First(z => z.ZoneCode == "G1_town");
            var zone = Zone.ZONES.First(z => z.ZoneCode == "G1_1");

            Assert.AreEqual(Zone.ZoneType.Town, Zone.ZONETYPES[town]);
            Assert.AreEqual(Zone.ZoneType.Zone, Zone.ZONETYPES[zone]);
        }

        [TestMethod]
        public void TestActNumbers()
        {
            var act1 = Zone.ZONES.First(z => z.ZoneCode == "G1_1");
            var act2 = Zone.ZONES.First(z => z.ZoneCode == "G2_1");
            var act3 = Zone.ZONES.First(z => z.ZoneCode == "G3_1");
            var cruelAct1 = Zone.ZONES.First(z => z.ZoneCode == "C_G1_1");

            Assert.AreEqual(1, act1.Act);
            Assert.AreEqual(2, act2.Act);
            Assert.AreEqual(3, act3.Act);
            Assert.AreEqual(4, cruelAct1.Act);
        }
    }
}
