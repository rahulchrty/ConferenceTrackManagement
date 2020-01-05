using ConferenceTrackManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace ConferenceTrackManagementTest
{
    [TestClass]
    public class TalkDescriptionTest
    {
        private Mock<ITalkTitle> _mockTalkTitle;
        private Mock<ITimeDuration> _mockTimeDuration;
        private ITalkDescription _talkDescription;
        [TestInitialize]
        public void Setup()
        {
            _mockTalkTitle = new Mock<ITalkTitle>();
            _mockTimeDuration = new Mock<ITimeDuration>();
        }

        [TestMethod]
        public void Given_A_Single_Talk()
        {
            List<string> talkInputList = new List<string>();
            talkInputList.Add("Ruby session 50min");
            _mockTalkTitle.Setup(x => x.ExtractTitle(It.IsAny<string>())).Returns("Ruby session");
            _mockTimeDuration.Setup(x => x.ExtractTime(It.IsAny<string>())).Returns(50);
            _talkDescription = new TalkDescription(_mockTalkTitle.Object, _mockTimeDuration.Object);
            var result = _talkDescription.GetTalksDescription(talkInputList);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void Given_A_Talk_With_50min_DurationThen_Get_DurationInMinutes_As_Int_50()
        {
            List<string> talkInputList = new List<string>();
            talkInputList.Add("Ruby session 50min");
            _mockTalkTitle.Setup(x => x.ExtractTitle(It.IsAny<string>())).Returns("Ruby session");
            _mockTimeDuration.Setup(x => x.ExtractTime(It.IsAny<string>())).Returns(50);
            _talkDescription = new TalkDescription(_mockTalkTitle.Object, _mockTimeDuration.Object);
            var result = _talkDescription.GetTalksDescription(talkInputList);
            Assert.AreEqual(50, result[0].DurationInMin);
        }

        [TestMethod]
        public void Given_A_Talk_Jedi_Session_40min_Get_Title_As_Jedi_Session()
        {
            List<string> talkInputList = new List<string>();
            talkInputList.Add("Jedi session 40min");
            _mockTalkTitle.Setup(x => x.ExtractTitle(It.IsAny<string>())).Returns("Jedi session");
            _mockTimeDuration.Setup(x => x.ExtractTime(It.IsAny<string>())).Returns(40);
            _talkDescription = new TalkDescription(_mockTalkTitle.Object, _mockTimeDuration.Object);
            var result = _talkDescription.GetTalksDescription(talkInputList);
            Assert.AreEqual("Jedi session", result[0].Title);
        }

        [TestMethod]
        public void Given_A_Two_Talks()
        {
            List<string> talkInputList = new List<string>();
            talkInputList.Add("Ruby session 50min");
            talkInputList.Add("Go session 40min");
            _mockTalkTitle.SetupSequence(x => x.ExtractTitle(It.IsAny<string>())).Returns("Ruby session").Returns("Go session");
            _mockTimeDuration.SetupSequence(x => x.ExtractTime(It.IsAny<string>())).Returns(50).Returns(40);
            _talkDescription = new TalkDescription(_mockTalkTitle.Object, _mockTimeDuration.Object);
            var result = _talkDescription.GetTalksDescription(talkInputList);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void Given_A_Talk_With_Invalid_Data()
        {
            List<string> talkInputList = new List<string>();
            talkInputList.Add(" FiveMin");
            _mockTalkTitle.Setup(x => x.ExtractTitle(It.IsAny<string>())).Returns(string.Empty);
            _mockTimeDuration.Setup(x => x.ExtractTime(It.IsAny<string>())).Returns(0);
            _talkDescription = new TalkDescription(_mockTalkTitle.Object, _mockTimeDuration.Object);
            var result = _talkDescription.GetTalksDescription(talkInputList);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void Given_A_Two_Talks_With_One_Invalid_Data()
        {
            List<string> talkInputList = new List<string>();
            talkInputList.Add("Ruby session 50min");
            talkInputList.Add(string.Empty);
            _mockTalkTitle.SetupSequence(x => x.ExtractTitle(It.IsAny<string>())).Returns("Ruby session").Returns(string.Empty);
            _mockTimeDuration.SetupSequence(x => x.ExtractTime(It.IsAny<string>())).Returns(50).Returns(0);
            _talkDescription = new TalkDescription(_mockTalkTitle.Object, _mockTimeDuration.Object);
            var result = _talkDescription.GetTalksDescription(talkInputList);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void Given_Input_As_Null()
        {
            List<string> talkInputList = null;
            _talkDescription = new TalkDescription(_mockTalkTitle.Object, _mockTimeDuration.Object);
            var result = _talkDescription.GetTalksDescription(talkInputList);
            Assert.AreEqual(0, result.Count);
        }
    }
}
