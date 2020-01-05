using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public class Track
    {
        public int TrackNo { get; set; }
        public List<Talk> Talks { get; set; }
    }
}
