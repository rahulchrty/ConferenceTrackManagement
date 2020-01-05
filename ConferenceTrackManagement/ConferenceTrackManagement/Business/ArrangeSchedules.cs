using System;
using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public class ArrangeSchedules : IArrangeSchedules
    {
        public List<string> GetSchedules(List<Track> tracks)
        {
            List<string> schedules = new List<string>();
            TimeSpan morningSessionStartTime = new TimeSpan(9, 00, 00);
            TimeSpan afternoonSessionStartTime = new TimeSpan(13, 00, 00);
            foreach (Track eachTrack in tracks)
            {
                int morningSessionTimeLaps = TrackConfig.MorningSessionLength;
                int afternoonSessionTimeLaps = TrackConfig.AfternoonSessionLength;
                schedules.Add("Track: " + eachTrack.TrackNo);
                foreach(Talk eachTalk in eachTrack.Talks)
                {
                    if (morningSessionTimeLaps != 0 && morningSessionTimeLaps >= eachTalk.DurationInMin)
                    {
                        morningSessionTimeLaps -= eachTalk.DurationInMin;
                        int startMin = TrackConfig.MorningSessionLength - (morningSessionTimeLaps + eachTalk.DurationInMin);
                        TimeSpan sessionBeginsAt = new TimeSpan(morningSessionStartTime.Hours, startMin, 00);
                        DateTime morningSessionTime = new DateTime(1, 1, 1, sessionBeginsAt.Hours, sessionBeginsAt.Minutes, 00);
                        string scheduledTime = morningSessionTime.ToString("hh:mm tt");
                        schedules.Add(scheduledTime + ": " + eachTalk.Title);
                        if (morningSessionTimeLaps == 0)
                        {
                            schedules.Add(TrackConfig.LunchTime);
                        }
                    }
                    else
                    {
                        if (morningSessionTimeLaps != 0)
                        {
                            schedules.Add(TrackConfig.LunchTime);
                            morningSessionTimeLaps = 0;
                        }
                        afternoonSessionTimeLaps -= eachTalk.DurationInMin;
                        int startMin = TrackConfig.AfternoonSessionLength - (afternoonSessionTimeLaps + eachTalk.DurationInMin);
                        TimeSpan sessionBeginsAt = new TimeSpan(afternoonSessionStartTime.Hours, startMin, 00);
                        DateTime afternoonSessionTime = new DateTime(1, 1, 1, sessionBeginsAt.Hours, sessionBeginsAt.Minutes, 00);
                        string scheduledTime = afternoonSessionTime.ToString("hh:mm tt");
                        schedules.Add(scheduledTime + ": " + eachTalk.Title);
                    }
                }
                if (morningSessionTimeLaps > 0)
                {
                    schedules.Add(TrackConfig.LunchTime);
                }
                int networkSessionStarts = TrackConfig.AfternoonSessionLength - afternoonSessionTimeLaps;
                if (networkSessionStarts != 0 && networkSessionStarts > 60)
                {
                    TimeSpan networksessionBeginsAt = new TimeSpan(afternoonSessionStartTime.Hours, networkSessionStarts, 00);
                    DateTime networkSessionTime = new DateTime(1, 1, 1, networksessionBeginsAt.Hours, networksessionBeginsAt.Minutes, 00);
                    string networkScheduledTime = networkSessionTime.ToString("hh:mm tt");
                    schedules.Add(networkScheduledTime + ": " + TrackConfig.NetworkSessionTitle);
                }
                else
                {
                    schedules.Add(TrackConfig.NetworkSessionDefaultTime + TrackConfig.NetworkSessionTitle);
                }
            }
            return schedules;
        }
    }
}
