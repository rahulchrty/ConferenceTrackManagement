using ConferenceTrackManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace ConferenceTrackManagementTest
{
    [TestClass]
    public class ScheduleTrackTest
    {
        private Mock<IAllocateTrack> _mockAllocateTrack;
        private IScheduleTrack _scheduleTrack;

        [TestInitialize]
        public void SetUp()
        {
            _mockAllocateTrack = new Mock<IAllocateTrack>();
        }

        [TestMethod]
        public void Given_A_Talk_To_Schedule()
        {
            List<Talk> talkList = new List<Talk>();
            talkList.Add(new Talk { DurationInMin = 20, Title = "Java Session"});
            int morningSessionLength = 60;
            int afternoonSessionLength = 60;
            List<Track> trackList = new List<Track>();
            trackList.Add(
                new Track { 
                    TrackNo = 1,
                    Talks = new List<Talk>() { new Talk {DurationInMin = 20, Title = "Java Session" } }
                }
                );
            _mockAllocateTrack.Setup(x => x.Allocate(It.IsAny<List<Track>>(), It.IsAny<int>(), It.IsAny<Talk>()))
                            .Returns(trackList);
            _scheduleTrack = new ScheduleTrack(_mockAllocateTrack.Object);
            _scheduleTrack.GetTracks(talkList, morningSessionLength, afternoonSessionLength);
            _mockAllocateTrack.Verify(x => x.Allocate(It.IsAny<List<Track>>(), It.IsAny<int>(), It.IsAny<Talk>()), Times.Once());
        }

        [TestMethod]
        public void Given_2_Talks_To_Schedule()
        {
            List<Talk> talkList = new List<Talk>();
            talkList.Add(new Talk { DurationInMin = 20, Title = "Java Session" });
            talkList.Add(new Talk { DurationInMin = 40, Title = "Python Session" });
            int morningSessionLength = 60;
            int afternoonSessionLength = 60;
            List<Track> trackList = new List<Track>();
            trackList.Add(
                new Track
                {
                    TrackNo = 1,
                    Talks = new List<Talk>() { new Talk { DurationInMin = 20, Title = "Java Session" } }
                }
                );
            trackList.Add(
                new Track
                {
                    TrackNo = 1,
                    Talks = new List<Talk>() { new Talk { DurationInMin = 40, Title = "Python Session" } }
                }
                );
            _mockAllocateTrack.Setup(x => x.Allocate(It.IsAny<List<Track>>(), It.IsAny<int>(), It.IsAny<Talk>()))
                            .Returns(trackList);
            _scheduleTrack = new ScheduleTrack(_mockAllocateTrack.Object);
            _scheduleTrack.GetTracks(talkList, morningSessionLength, afternoonSessionLength);
            _mockAllocateTrack.Verify(x => x.Allocate(It.IsAny<List<Track>>(), It.IsAny<int>(), It.IsAny<Talk>()), Times.Exactly(2));
        }

        [TestMethod]
        public void Given_2_Talks_To_Schedule_Each_With_60min_Durations()
        {
            List<Talk> talkList = new List<Talk>();
            talkList.Add(new Talk { DurationInMin = 60, Title = "Java Session" });
            talkList.Add(new Talk { DurationInMin = 60, Title = "Python Session" });
            int morningSessionLength = 60;
            int afternoonSessionLength = 60;
            List<Track> trackList = new List<Track>();
            trackList.Add(
                new Track
                {
                    TrackNo = 1,
                    Talks = new List<Talk>() { new Talk { DurationInMin = 60, Title = "Java Session" } }
                }
                );
            trackList.Add(
                new Track
                {
                    TrackNo = 1,
                    Talks = new List<Talk>() { new Talk { DurationInMin = 60, Title = "Python Session" } }
                }
                );
            _mockAllocateTrack.Setup(x => x.Allocate(It.IsAny<List<Track>>(), It.IsAny<int>(), It.IsAny<Talk>()))
                            .Returns(trackList);
            _scheduleTrack = new ScheduleTrack(_mockAllocateTrack.Object);
            _scheduleTrack.GetTracks(talkList, morningSessionLength, afternoonSessionLength);
            _mockAllocateTrack.Verify(x => x.Allocate(It.IsAny<List<Track>>(), It.IsAny<int>(), It.IsAny<Talk>()), Times.Exactly(2));
        }

        [TestMethod]
        public void Given_3_Talks_To_Schedule_Each_With_60min_Durations()
        {
            List<Talk> talkList = new List<Talk>();
            talkList.Add(new Talk { DurationInMin = 60, Title = "Java Session" });
            talkList.Add(new Talk { DurationInMin = 60, Title = "Python Session" });
            talkList.Add(new Talk { DurationInMin = 60, Title = "Go Session" });
            int morningSessionLength = 60;
            int afternoonSessionLength = 60;
            List<Track> trackList = new List<Track>();
            trackList.Add(
                new Track
                {
                    TrackNo = 1,
                    Talks = new List<Talk>() { new Talk { DurationInMin = 60, Title = "Java Session" } }
                }
                );
            trackList.Add(
                new Track
                {
                    TrackNo = 1,
                    Talks = new List<Talk>() { new Talk { DurationInMin = 60, Title = "Python Session" } }
                }
                );
            trackList.Add(
                new Track
                {
                    TrackNo = 2,
                    Talks = new List<Talk>() { new Talk { DurationInMin = 60, Title = "Go Session" } }
                }
                );
            _mockAllocateTrack.Setup(x => x.Allocate(It.IsAny<List<Track>>(), It.IsAny<int>(), It.IsAny<Talk>()))
                            .Returns(trackList);
            _scheduleTrack = new ScheduleTrack(_mockAllocateTrack.Object);
            _scheduleTrack.GetTracks(talkList, morningSessionLength, afternoonSessionLength);
            _mockAllocateTrack.Verify(x => x.Allocate(It.IsAny<List<Track>>(), It.IsAny<int>(), It.IsAny<Talk>()), Times.Exactly(3));
        }
    }
}
