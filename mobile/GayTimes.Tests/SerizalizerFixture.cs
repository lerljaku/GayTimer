using System;
using System.IO;
using GayTimer.Entities;
using GayTimer.Services;
using NUnit.Framework;

namespace GayTimes.Tests
{
    [TestFixture]
    public class SerizalizerFixture
    {
        private SerializerProvider SUT;
        
        [SetUp]
        public void Init()
        {
            SUT = new SerializerProvider();
        }

        [Test]
        public void Test()
        {
            var data = new Player()
            {
                Id = 1,
                Created = new DateTime(2000,01,01),
                Nick = "OttoGej",
            };

            var serData = SUT.Serialize(data);

            var user = SUT.Deserialize<Player>(serData);

            Assert.AreEqual(data.Id, user.Id);
            Assert.AreEqual(data.Created, user.Created);
            Assert.AreEqual(data.Nick, user.Nick);
        }
    }
}