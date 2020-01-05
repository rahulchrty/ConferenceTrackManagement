using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public class TalkDescription : ITalkDescription
    {
        private ITalkTitle _talkTitle;
        private ITimeDuration _timeDuration;
        public TalkDescription(ITalkTitle talkTitle, ITimeDuration timeDuration)
        {
            _talkTitle = talkTitle;
            _timeDuration = timeDuration;
        }
        public List<Talk> GetTalksDescription(List<string> talkInputList)
        {
            List<Talk> talkList = new List<Talk>();
            if (talkInputList != null)
            {
                foreach (string talkDetailInput in talkInputList)
                {
                    int duration = _timeDuration.ExtractTime(talkDetailInput);
                    string title = _talkTitle.ExtractTitle(talkDetailInput);
                    talkList.Add(new Talk { DurationInMin = duration, Title = title });
                }
            }
            return talkList;
        }
    }
}
