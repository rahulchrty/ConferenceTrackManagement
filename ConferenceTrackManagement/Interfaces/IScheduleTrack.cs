using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public interface IScheduleTrack
    {
        List<Track> GetTracks(List<Talk> talkList, int morningSessionLength, int afternoonSessionLength);
    }
}
