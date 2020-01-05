using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConferenceTrackManagement;
using System.Collections.Generic;

namespace ConferenceTrackManagementTest
{
    [TestClass]
    public class ArrangeSchedulesTest
    {
        IArrangeSchedules _arrangeSchedules;
        [TestInitialize]
        public void SetUp()
        {
            _arrangeSchedules = new ArrangeSchedules();
        }

        [TestMethod]
        public void Given_A_Track_With_30min_Talk()
        {
            List<Track> tracks = new List<Track>();
            tracks.Add(
                  new Track { 
                    TrackNo = 1,
                    Talks = new List<Talk>() { new Talk { DurationInMin = 30, Title = "Python Session"} }
                  }   
                );
            var result = _arrangeSchedules.GetSchedules(tracks);
            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public void Given_A_Track_With_30min_Talk_Then_Get_Track_Given_Session_Lunch_And_Network_Session_Times()
        {
            List<Track> tracks = new List<Track>();
            tracks.Add(
                  new Track
                  {
                      TrackNo = 1,
                      Talks = new List<Talk>() { new Talk { DurationInMin = 30, Title = "Python Session" } }
                  }
                );
            var result = _arrangeSchedules.GetSchedules(tracks);
            Assert.AreEqual("Track: 1", result[0]);
            Assert.AreEqual("09:00 AM: Python Session", result[1]);
            Assert.AreEqual("12:00 PM: Lunch", result[2]);
            Assert.AreEqual("04:00 PM: Network Session", result[3]);
        }

        [TestMethod]
        public void Given_A_Track_With_3_Sessions_60Min_Each()
        {
            List<Talk> talks = new List<Talk>();
            talks.Add(new Talk { DurationInMin = 60, Title = "Python Session" });
            talks.Add(new Talk { DurationInMin = 60, Title = "Go Session" });
            talks.Add(new Talk { DurationInMin = 60, Title = "Blender Session" });
            List<Track> tracks = new List<Track>();
            tracks.Add(
                  new Track
                  {
                      TrackNo = 1,
                      Talks = talks
                  }
                );
            var result = _arrangeSchedules.GetSchedules(tracks);
            Assert.AreEqual("Track: 1", result[0]);
            Assert.AreEqual("09:00 AM: Python Session", result[1]);
            Assert.AreEqual("10:00 AM: Go Session", result[2]);
            Assert.AreEqual("11:00 AM: Blender Session", result[3]);
            Assert.AreEqual("12:00 PM: Lunch", result[4]);
            Assert.AreEqual("04:00 PM: Network Session", result[5]);
        }

        [TestMethod]
        public void Given_A_Track_Where_Morning_Session_Ends_5_Mins_Before_Lunch()
        {
            List<Talk> talks = new List<Talk>();
            talks.Add(new Talk { DurationInMin = 60, Title = "Python Session" });
            talks.Add(new Talk { DurationInMin = 60, Title = "Go Session" });
            talks.Add(new Talk { DurationInMin = 55, Title = "Blender Session" });
            List<Track> tracks = new List<Track>();
            tracks.Add(
                  new Track
                  {
                      TrackNo = 1,
                      Talks = talks
                  }
                );
            var result = _arrangeSchedules.GetSchedules(tracks);
            Assert.AreEqual("Track: 1", result[0]);
            Assert.AreEqual("09:00 AM: Python Session", result[1]);
            Assert.AreEqual("10:00 AM: Go Session", result[2]);
            Assert.AreEqual("11:00 AM: Blender Session", result[3]);
            Assert.AreEqual("12:00 PM: Lunch", result[4]);
            Assert.AreEqual("04:00 PM: Network Session", result[5]);
        }

        [TestMethod]
        public void Given_For_A_Track_A_Talk_Duration_Is_Longer_Than_Available_Time_In_Morning_Session()
        {
            List<Talk> talks = new List<Talk>();
            talks.Add(new Talk { DurationInMin = 60, Title = "Python Session" });
            talks.Add(new Talk { DurationInMin = 60, Title = "Go Session" });
            talks.Add(new Talk { DurationInMin = 45, Title = "Blender Session" });
            talks.Add(new Talk { DurationInMin = 45, Title = "Django Session" });
            List<Track> tracks = new List<Track>();
            tracks.Add(
                  new Track
                  {
                      TrackNo = 1,
                      Talks = talks
                  }
                );
            var result = _arrangeSchedules.GetSchedules(tracks);

            Assert.AreEqual("Track: 1", result[0]);
            Assert.AreEqual("09:00 AM: Python Session", result[1]);
            Assert.AreEqual("10:00 AM: Go Session", result[2]);
            Assert.AreEqual("11:00 AM: Blender Session", result[3]);
            Assert.AreEqual("12:00 PM: Lunch", result[4]);
            Assert.AreEqual("01:00 PM: Django Session", result[5]);
            Assert.AreEqual("04:00 PM: Network Session", result[6]);
        }

        [TestMethod]
        public void Given_A_Track_With_7_Sessions_60Min_Each()
        {
            List<Talk> talks = new List<Talk>();
            talks.Add(new Talk { DurationInMin = 60, Title = "Python Session" });
            talks.Add(new Talk { DurationInMin = 60, Title = "Go Session" });
            talks.Add(new Talk { DurationInMin = 60, Title = "Blender Session" });
            talks.Add(new Talk { DurationInMin = 60, Title = "Docker Session" });
            talks.Add(new Talk { DurationInMin = 60, Title = "Azure Session" });
            talks.Add(new Talk { DurationInMin = 60, Title = "SQL Session" });
            talks.Add(new Talk { DurationInMin = 60, Title = "SSH Session" });
            List<Track> tracks = new List<Track>();
            tracks.Add(
                  new Track
                  {
                      TrackNo = 1,
                      Talks = talks
                  }
                );
            var result = _arrangeSchedules.GetSchedules(tracks);
            Assert.AreEqual("Track: 1", result[0]);
            Assert.AreEqual("09:00 AM: Python Session", result[1]);
            Assert.AreEqual("10:00 AM: Go Session", result[2]);
            Assert.AreEqual("11:00 AM: Blender Session", result[3]);
            Assert.AreEqual("12:00 PM: Lunch", result[4]);
            Assert.AreEqual("01:00 PM: Docker Session", result[5]);
            Assert.AreEqual("02:00 PM: Azure Session", result[6]);
            Assert.AreEqual("03:00 PM: SQL Session", result[7]);
            Assert.AreEqual("04:00 PM: SSH Session", result[8]);
            Assert.AreEqual("05:00 PM: Network Session", result[9]);
        }

        [TestMethod]
        public void Given_2_Tracks_With_30min_Talk_For_Each()
        {
            List<Track> tracks = new List<Track>();
            tracks.Add(
                  new Track
                  {
                      TrackNo = 1,
                      Talks = new List<Talk>() { new Talk { DurationInMin = 30, Title = "Python Session" } }
                  }
                );
            tracks.Add(
                  new Track
                  {
                      TrackNo = 2,
                      Talks = new List<Talk>() { new Talk { DurationInMin = 30, Title = "Go Session" } }
                  }
                );
            var result = _arrangeSchedules.GetSchedules(tracks);
            Assert.AreEqual("Track: 1", result[0]);
            Assert.AreEqual("09:00 AM: Python Session", result[1]);
            Assert.AreEqual("12:00 PM: Lunch", result[2]);
            Assert.AreEqual("04:00 PM: Network Session", result[3]);
            Assert.AreEqual("Track: 2", result[4]);
            Assert.AreEqual("09:00 AM: Go Session", result[5]);
            Assert.AreEqual("12:00 PM: Lunch", result[6]);
            Assert.AreEqual("04:00 PM: Network Session", result[7]);
        }
    }
}
