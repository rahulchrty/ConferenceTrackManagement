using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public interface IScheduleTalk
    {
        List<string> ProcessScheduling(List<string> talkInputList);
    }
}
