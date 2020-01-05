using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public interface IArrangeSchedules
    {
        List<string> GetSchedules(List<Track> tracks);
    }
}
