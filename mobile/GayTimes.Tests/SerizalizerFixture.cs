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
            var data = new Gay()
            {
                Id = 1,
                Created = new DateTime(2000,01,01),
                Nick = "OttoGej",
                PasswordHash = "456sdfwer",
                PasswordSalt = "45sdafdasfa86erw32sadf6asdf",
            };

            var serData = SUT.Serialize(data);

            var user = SUT.Deserialize<Gay>(serData);

            Assert.AreEqual(data.Id, user.Id);
            Assert.AreEqual(data.Created, user.Created);
            Assert.AreEqual(data.PasswordHash, user.PasswordHash);
            Assert.AreEqual(data.PasswordSalt, user.PasswordSalt);
            Assert.AreEqual(data.Nick, user.Nick);
        }
    }
}