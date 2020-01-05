using ConferenceTrackManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ConferenceTrackManagementTest
{
    [TestClass]
    public class TalkDescriptionValidationTest
    {
        private ITalkDescriptionValidation _talkDescriptionValidation;
        [TestInitialize]
        public void Setup()
        {
            _talkDescriptionValidation = new TalkDescriptionValidation();
        }
        [TestMethod]
        public void Given_A_ValidDetails()
        {
            List<Talk> talkList = new List<Talk>();
            talkList.Add(new Talk { DurationInMin = 20, Title = "Python training" });
            var result = _talkDescriptionValidation.ValidateDescriptions(talkList);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Given_Two_ValidDetails()
        {
            List<Talk> talkList = new List<Talk>();
            talkList.Add(new Talk { DurationInMin = 20, Title = "Python training" });
            talkList.Add(new Talk { DurationInMin = 60, Title = "Regular expression session" });
            var result = _talkDescriptionValidation.ValidateDescriptions(talkList);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Given_A_Talk_Details_With_0_Min()
        {
            List<Talk> talkList = new List<Talk>();
            talkList.Add(new Talk { DurationInMin = 0, Title = "Python training" });
            var result = _talkDescriptionValidation.ValidateDescriptions(talkList);
            string expectedResult = "Line no 1: Talk duration is required";
            Assert.AreEqual(expectedResult, result[0]);
        }

        [TestMethod]
        public void Given_A_Talk_Details_With_No_Title()
        {
            List<Talk> talkList = new List<Talk>();
            talkList.Add(new Talk { DurationInMin = 10, Title = string.Empty });
            var result = _talkDescriptionValidation.ValidateDescriptions(talkList);
            string expectedResult = "Line no 1: Talk title is required";
            Assert.AreEqual(expectedResult, result[0]);
        }

        [TestMethod]
        public void Given_Talk_Details_With_No_Title_And_0_Min_Duration()
        {
            List<Talk> talkList = new List<Talk>();
            talkList.Add(new Talk { DurationInMin = 0, Title = string.Empty });
            var result = _talkDescriptionValidation.ValidateDescriptions(talkList);
            string expectedResult = "Line no 1: Has invalid details";
            Assert.AreEqual(expectedResult, result[0]);
        }

        [TestMethod]
        public void Given_2_Details_1st_Without_Titile_And_2nd_With_0min_Duration()
        {
            List<Talk> talkList = new List<Talk>();
            talkList.Add(new Talk { DurationInMin = 30, Title = string.Empty });
            talkList.Add(new Talk { DurationInMin = 0, Title = "Blender session" });
            var result = _talkDescriptionValidation.ValidateDescriptions(talkList);
            Assert.AreEqual(2, result.Count);
        }
    }
}