using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConferenceTrackManagement;
using System.Collections.Generic;

namespace ConferenceTrackManagementTest
{
    [TestClass]
    public class AllocateTrackTest
    {
        private IAllocateTrack _allocateTrack;
        [TestInitialize]
        public void SetUp()
        {
            _allocateTrack = new AllocateTrack();
        }

        [TestMethod]
        public void Given_A_TrackNumber_And_A_TrackToSchedule_Then_Get_One_Track_Scheduled()
        {
            List<Track> scheduledTracks = new List<Track>();
            int trackIndex = 0;
            Talk talk = new Talk {Title = "Ruby Session", DurationInMin = 45 };
            var result = _allocateTrack.Allocate(scheduledTracks, trackIndex, talk);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void Given_A_TrackNumber_And_A_TrackToSchedule_Then_Get_One_Track_No_As_1()
        {
            List<Track> scheduledTracks = new List<Track>();
            int trackIndex = 0;
            Talk talk = new Talk { Title = "Ruby Session", DurationInMin = 45 };
            var result = _allocateTrack.Allocate(scheduledTracks, trackIndex, talk);
            Assert.AreEqual(1, result[0].TrackNo);
        }

        [TestMethod]
        public void Given_A_TrackNumber_And_A_TrackToSchedule_Then_Get_One_Track_With_One_Scheduled_Slot()
        {
            List<Track> scheduledTracks = new List<Track>();
            int trackIndex = 0;
            Talk talk = new Talk { Title = "Ruby Session", DurationInMin = 45 };
            var result = _allocateTrack.Allocate(scheduledTracks, trackIndex, talk);
            Assert.AreEqual(1, result[0].Talks.Count);
        }

        [TestMethod]
        public void Given_Track1_Has_1_Talk_Scheduled_And_A_New_Talk_To_Allocate_In_Track1()
        {
            List<Track> scheduledTracks = new List<Track>();
            scheduledTracks.Add(
                    new Track
                    {
                        TrackNo = 1,
                        Talks = new List<Talk> { new Talk { DurationInMin = 45, Title ="Ruby Session"} }
                    }
                );
            int trackIndex = 0;
            Talk talk = new Talk { Title = "Python Session", DurationInMin = 60 };
            var result = _allocateTrack.Allocate(scheduledTracks, trackIndex, talk);
            Assert.AreEqual(2, result[0].Talks.Count);
        }

        [TestMethod]
        public void Given_Has_Track1_And_A_New_Track_Is_Added()
        {
            List<Track> scheduledTracks = new List<Track>();
            scheduledTracks.Add(
                    new Track
                    {
                        TrackNo = 1,
                        Talks = new List<Talk> { new Talk { DurationInMin = 45, Title = "Ruby Session" } }
                    }
                );
            int trackIndex = 1;
            Talk talk = new Talk { Title = "Python Session", DurationInMin = 60 };
            var result = _allocateTrack.Allocate(scheduledTracks, trackIndex, talk);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void Given_2Tracks_And_Add_A_New_Talk_To_Second_Track()
        {
            List<Track> scheduledTracks = new List<Track>();
            scheduledTracks.Add(
                    new Track
                    {
                        TrackNo = 1,
                        Talks = new List<Talk> { new Talk { DurationInMin = 60, Title = "Ruby Session" } }
                    }
                );
            scheduledTracks.Add(
                    new Track
                    {
                        TrackNo = 2,
                        Talks = new List<Talk> { new Talk { DurationInMin = 30, Title = "Python Session" } }
                    }
                );
            int trackIndex = 1;
            Talk talk = new Talk { Title = "Golang Session", DurationInMin = 30 };
            var result = _allocateTrack.Allocate(scheduledTracks, trackIndex, talk);
            Assert.AreEqual(2, result[1].Talks.Count);
        }
    }
}
