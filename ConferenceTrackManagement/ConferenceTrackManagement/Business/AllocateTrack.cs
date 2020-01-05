using System.Collections.Generic;
using System.Linq;

namespace ConferenceTrackManagement
{
    public class AllocateTrack : IAllocateTrack
    {
        public List<Track> Allocate(List<Track> scheduledTracks, int trackIndex, Talk talk)
        {
            List<Track> allocateTalk = scheduledTracks;
            int trackOn = trackIndex + 1;
            if (allocateTalk.Any(x => x.TrackNo == trackOn))
            {
                int scheduledTrackIndex = allocateTalk.FindIndex(x => x.TrackNo == trackOn);
                Track track = allocateTalk[scheduledTrackIndex];
                List<Talk> talkList = track.Talks;
                talkList.Add(talk);
                track.Talks = talkList;
                allocateTalk[scheduledTrackIndex] = track;
            }
            else
            {
                List<Talk> talkList = new List<Talk>();
                talkList.Add(talk);
                allocateTalk.Add(new Track { TrackNo = trackIndex + 1, Talks = talkList });
            }
            return allocateTalk;
        }
    }
}
