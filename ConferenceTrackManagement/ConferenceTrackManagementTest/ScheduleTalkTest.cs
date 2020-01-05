using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConferenceTrackManagement;
using System.Collections.Generic;
using Moq;

namespace ConferenceTrackManagementTest
{
    [TestClass]
    public class ScheduleTalkTest
    {
        private Mock<ITalkDescription> _mockTalkDescription;
        private Mock<ITalkDescriptionValidation> _mockTalkDescriptionValidation;
        private Mock<IScheduleTrack> _mockScheduleTrack;
        private Mock<IArrangeSchedules> _mockArrangeSchedules;
        private IScheduleTalk _scheduleTalk;
        [TestInitialize]
        public void Setup()
        {
            _mockTalkDescription = new Mock<ITalkDescription>();
            _mockTalkDescriptionValidation = new Mock<ITalkDescriptionValidation>();
            _mockScheduleTrack = new Mock<IScheduleTrack>();
            _mockArrangeSchedules = new Mock<IArrangeSchedules>();
        }

        [TestMethod]
        public void Given_A_Single_Talk_To_Schedule()
        {
            List<string> talkInputList = new List<string>();
            talkInputList.Add("Writing Fast Tests Against Enterprise Rails 60min");
            List<Talk> talkList = new List<Talk>();
            talkList.Add(new Talk { DurationInMin = 60, Title = "Writing Fast Tests Against Enterprise Rails" });
            _mockTalkDescription.Setup(x => x.GetTalksDescription(It.IsAny<List<string>>())).Returns(talkList);
            _mockTalkDescriptionValidation.Setup(x => x.ValidateDescriptions(It.IsAny<List<Talk>>())).Returns(new List<string>());
            List<Track> tracks = new List<Track>();
            tracks.Add(
                    new Track
                    {
                        TrackNo = 1,
                        Talks = new List<Talk>() { new Talk { DurationInMin = 60, Title = "Writing Fast Tests Against Enterprise Rails" } }
                    }
                );
            _mockScheduleTrack.Setup(x => x.GetTracks(It.IsAny<List<Talk>>(), It.IsAny<int>(), It.IsAny<int>())).Returns(tracks);
            List<string> schedules = new List<string>();
            schedules.Add("Track: 1");
            schedules.Add("09:00 AM: Writing Fast Tests Against Enterprise Rails");
            schedules.Add("12:00 PM: Lunch");
            schedules.Add("05:00 PM: Network Session");
            _mockArrangeSchedules.Setup(x => x.GetSchedules(It.IsAny<List<Track>>())).Returns(schedules);

            _scheduleTalk = new ScheduleTalk(_mockTalkDescription.Object, _mockTalkDescriptionValidation.Object,
                            _mockScheduleTrack.Object, _mockArrangeSchedules.Object);
            _scheduleTalk.ProcessScheduling(talkInputList);

            _mockTalkDescription.Verify(x => x.GetTalksDescription(It.IsAny<List<string>>()), Times.Once());
            _mockTalkDescriptionValidation.Verify(x => x.ValidateDescriptions(It.IsAny<List<Talk>>()), Times.Once());
            _mockScheduleTrack.Verify(x => x.GetTracks(It.IsAny<List<Talk>>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once());
            _mockArrangeSchedules.Verify(x => x.GetSchedules(It.IsAny<List<Track>>()), Times.Once());
        }

        [TestMethod]
        public void Given_A_Single_Talk_To_Schedule_Which_Has_Invalid_Data()
        {
            List<string> talkInputList = new List<string>();
            talkInputList.Add("Writing Fast Tests Against Enterprise Rails");
            List<Talk> talkList = new List<Talk>();
            talkList.Add(new Talk { DurationInMin = 0, Title = "Writing Fast Tests Against Enterprise Rails" });
            _mockTalkDescription.Setup(x => x.GetTalksDescription(It.IsAny<List<string>>())).Returns(talkList);
            List<string> validationMessage = new List<string>();
            validationMessage.Add("Line no 1: Talk duration is required");
            _mockTalkDescriptionValidation.Setup(x => x.ValidateDescriptions(It.IsAny<List<Talk>>())).Returns(validationMessage);

            _scheduleTalk = new ScheduleTalk(_mockTalkDescription.Object, _mockTalkDescriptionValidation.Object,
                            _mockScheduleTrack.Object, _mockArrangeSchedules.Object);
            _scheduleTalk.ProcessScheduling(talkInputList);

            _mockTalkDescription.Verify(x => x.GetTalksDescription(It.IsAny<List<string>>()), Times.Once());
            _mockTalkDescriptionValidation.Verify(x => x.ValidateDescriptions(It.IsAny<List<Talk>>()), Times.Once());
            _mockScheduleTrack.Verify(x => x.GetTracks(It.IsAny<List<Talk>>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never());
            _mockArrangeSchedules.Verify(x => x.GetSchedules(It.IsAny<List<Track>>()), Times.Never());
        }
    }
}
