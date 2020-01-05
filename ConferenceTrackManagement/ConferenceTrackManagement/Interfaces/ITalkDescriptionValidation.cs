using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public interface ITalkDescriptionValidation
    {
        List<string> ValidateDescriptions(List<Talk> talkList);
    }
}
