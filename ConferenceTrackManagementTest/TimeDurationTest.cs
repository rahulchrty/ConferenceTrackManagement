using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConferenceTrackManagement;

namespace ConferenceTrackManagementTest
{
    [TestClass]
    public class TimeDurationTest
    {
        private ITimeDuration _timeDuration;
        [TestInitialize]
        public void Setup()
        {
            _timeDuration = new TimeDuration();
        }
        [TestMethod]
        public void Given_Duration_As_Lightning()
        {
            string talkDetialInput = "Ruby Session Lightning";
            var result = _timeDuration.ExtractTime(talkDetialInput);
            Assert.AreEqual(5, result);
        }
        [TestMethod]
        public void Given_Duration_As_Flash()
        {
            string talkDetialInput = "Ruby Session Flash";
            var result = _timeDuration.ExtractTime(talkDetialInput);
            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void Given_Duration_As_1min()
        {
            string talkDetialInput = "Ruby Session 1min";
            var result = _timeDuration.ExtractTime(talkDetialInput);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Given_Duration_As_20min()
        {
            string talkDetialInput = "Ruby Session 20min";
            var result = _timeDuration.ExtractTime(talkDetialInput);
            Assert.AreEqual(20, result);
        }

        [TestMethod]
        public void Given_Duration_As_120min()
        {
            string talkDetialInput = "Ruby Session 120min";
            var result = _timeDuration.ExtractTime(talkDetialInput);
            Assert.AreEqual(120, result);
        }

        [TestMethod]
        public void Given_Duration_As_0min()
        {
            string talkDetialInput = "Ruby Session 0min";
            var result = _timeDuration.ExtractTime(talkDetialInput);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Given_Duration_As_10min_20min()
        {
            string talkDetialInput = "Ruby Session 10min 20min";
            var result = _timeDuration.ExtractTime(talkDetialInput);
            Assert.AreEqual(20, result);
        }

        [TestMethod]
        public void Given_The_Description_With_Many_Spaces_Between_Name_And_Time()
        {
            string talkDetialInput = "Jedi Session      20min";
            var result = _timeDuration.ExtractTime(talkDetialInput);
            Assert.AreEqual(20, result);
        }

        [TestMethod]
        public void Given_Duration_As_Desh10min()
        {
            string talkDetialInput = "Ruby Session -10min";
            var result = _timeDuration.ExtractTime(talkDetialInput);
            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void Given_Duration_As_min10()
        {
            string talkDetialInput = "Ruby Session min10";
            var result = _timeDuration.ExtractTime(talkDetialInput);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Given_Duration_As_EmptyString()
        {
            string talkDetialInput = string.Empty;
            var result = _timeDuration.ExtractTime(talkDetialInput);
            Assert.AreEqual(0, result);
        }
    }
}
