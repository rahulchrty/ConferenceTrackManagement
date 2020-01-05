using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public interface ITalkDescription
    {
        List<Talk> GetTalksDescription(List<string> talkInputList);
    }
}
