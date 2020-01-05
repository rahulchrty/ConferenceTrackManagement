using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public class TalkDescriptionValidation : ITalkDescriptionValidation
    {
        public List<string> ValidateDescriptions(List<Talk> talkList)
        {
            List<string> validationsMessages = new List<string>();
            int lineCounter = 0;
            foreach (Talk eachTalk in talkList)
            {
                lineCounter += 1;
                if (string.IsNullOrWhiteSpace(eachTalk.Title) && eachTalk.DurationInMin == 0)
                {
                    string message = string.Format("Line no {0}: Has invalid details", lineCounter);
                    validationsMessages.Add(message);
                }
                if (eachTalk.DurationInMin == 0)
                {
                    string message = string.Format("Line no {0}: Talk duration is required", lineCounter);
                    validationsMessages.Add(message);
                }
                if (string.IsNullOrWhiteSpace(eachTalk.Title))
                {
                    string message = string.Format("Line no {0}: Talk title is required", lineCounter);
                    validationsMessages.Add(message);
                }
            }
            return validationsMessages;
        }
    }
}
