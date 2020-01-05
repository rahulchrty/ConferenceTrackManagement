using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public class ScheduleTrack : IScheduleTrack
    {
        private IAllocateTrack _allocateTrack;
        public ScheduleTrack(IAllocateTrack allocateTrack)
        {
            _allocateTrack = allocateTrack;
        }
        public List<Track> GetTracks(List<Talk> talkList, int morningSessionLength, int afternoonSessionLength)
        {
            int totalTracks = 0;
            int totalTalks = talkList.Count;
            int[,] tracks = new int[totalTalks / 2 + 1, 2];
            List<Track> scheduledTracks = new List<Track>();
            foreach (Talk eachTalk in talkList)
            {
                int trackIndex = 0;
                int sessionIndex = 0;
                int maxMorningTimeToBeAllocated = morningSessionLength + 1;
                int maxAfternoonTimeToBeAllocated = afternoonSessionLength + 1;
                for (int i = 0; i < totalTracks; i++)
                {
                    if (tracks[i, 0] >= eachTalk.DurationInMin && tracks[i, 0] - eachTalk.DurationInMin < maxMorningTimeToBeAllocated)
                    {
                        trackIndex = i;
                        sessionIndex = 0;
                        maxMorningTimeToBeAllocated = tracks[i, 0] - eachTalk.DurationInMin;
                    }
                    else if (tracks[i, 1] >= eachTalk.DurationInMin && tracks[i, 1] - eachTalk.DurationInMin < maxAfternoonTimeToBeAllocated)
                    {
                        trackIndex = i;
                        sessionIndex = 1;
                        maxAfternoonTimeToBeAllocated = tracks[i, 1] - eachTalk.DurationInMin;
                    }
                }
                if (maxMorningTimeToBeAllocated == morningSessionLength + 1 && maxAfternoonTimeToBeAllocated == afternoonSessionLength + 1)
                {
                    tracks[totalTracks, 0] = morningSessionLength - eachTalk.DurationInMin;
                    tracks[totalTracks, 1] = afternoonSessionLength;
                    scheduledTracks = _allocateTrack.Allocate(scheduledTracks, totalTracks, eachTalk);
                    totalTracks += 1;
                }
                else
                {
                    tracks[trackIndex, sessionIndex] -= eachTalk.DurationInMin;
                    scheduledTracks = _allocateTrack.Allocate(scheduledTracks, trackIndex, eachTalk);
                }
            }
            return scheduledTracks;
        }
    }
}
