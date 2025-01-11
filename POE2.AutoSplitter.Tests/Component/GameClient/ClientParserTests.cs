using Microsoft.VisualStudio.TestTools.UnitTesting;
using POE2.AutoSplitter.Component.GameClient;
using System;

namespace POE2.AutoSplitter.Tests.Component.GameClient
{
    [TestClass]
    public class ClientParserTests
    {
        class MockClientEventHandler : IClientEventHandler
        {
            protected bool handledEvent = false;
            protected long expectedTimestamp;

            public MockClientEventHandler(long expectedTimestamp)
            {
                this.expectedTimestamp = expectedTimestamp;
            }

            public void AssertEventProcessed()
            {
                Assert.IsTrue(handledEvent);
            }

            public virtual void HandleZoneChange(long timestamp, string zoneName)
            {
                throw new NotImplementedException();
            }

            public virtual void HandleLoadStart(long timestamp)
            {
                throw new NotImplementedException();
            }

            public virtual void HandleLoadEnd(long timestamp, string zoneName)
            {
                throw new NotImplementedException();
            }

            public virtual void HandleLevelUp(long timestamp, int level)
            {
                throw new NotImplementedException();
            }
        }

        class ExpectedZoneChange : MockClientEventHandler
        {
            private string expectedZoneName;

            public ExpectedZoneChange(long expectedTimestamp, string expectedZoneName) : base(expectedTimestamp)
            {
                this.expectedZoneName = expectedZoneName;
            }

            public override void HandleZoneChange(long timestamp, string zoneName)
            {
                Assert.AreEqual(expectedTimestamp, timestamp);
                Assert.AreEqual(expectedZoneName, zoneName);
                handledEvent = true;
            }
        }

        [TestMethod]
        public void ProcessEnterZone()
        {
            ExpectedZoneChange expected = new ExpectedZoneChange(177482140, "The Riverbank");
            ClientParser parser = new ClientParser(expected);
            parser.ProcessLine("2025/01/10 19:47:27 177482140 a50 [INFO Client 784] : You have entered The Riverbank.");
            expected.AssertEventProcessed();
        }

        [TestMethod]
        public void ProcessEnterZoneWithDifficulty()
        {
            ExpectedZoneChange expected = new ExpectedZoneChange(177482140, "C_The Riverbank");
            ClientParser parser = new ClientParser(expected);
            parser.ProcessLine("2025/01/10 19:47:27 177482140 a50 [INFO Client 784] : You have entered The Riverbank. [CRUEL]");
            expected.AssertEventProcessed();
        }
    }
}
