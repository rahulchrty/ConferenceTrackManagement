using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public interface IAllocateTrack
    {
        List<Track> Allocate(List<Track> scheduledTracks, int trackIndex, Talk talk);
    }
}
