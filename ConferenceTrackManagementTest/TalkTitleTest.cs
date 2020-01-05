using ConferenceTrackManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConferenceTrackManagementTest
{
    [TestClass]
    public class TalkTitleTest
    {
        private ITalkTitle _talkTitle;
        [TestInitialize]
        public void SetUp()
        {
            _talkTitle = new TalkTitle();
        }

        [TestMethod]
        public void Given_Ruby_Session_30min()
        {
            string talkDetailInput = "Ruby session 30min";
            var actualResult = _talkTitle.ExtractTitle(talkDetailInput);
            string expected = "Ruby session";
            Assert.AreEqual(expected, actualResult);
        }

        [TestMethod]
        public void Given_The_Description_With_Many_Spaces_Between_Name_And_Time()
        {
            string talkDetailInput = "Ruby session         30min";
            var actualResult = _talkTitle.ExtractTitle(talkDetailInput);
            string expected = "Ruby session";
            Assert.AreEqual(expected, actualResult);
        }

        [TestMethod]
        public void Given_En_Empty_String()
        {
            string talkDetailInput = string.Empty;
            var actualResult = _talkTitle.ExtractTitle(talkDetailInput);
            string expected = string.Empty;
             Assert.AreEqual(expected, actualResult);
        }
    }
}
