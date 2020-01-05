using System;
using System.Text.RegularExpressions;

namespace ConferenceTrackManagement
{
    public class TimeDuration : ITimeDuration
    {
        public int ExtractTime (string talkDetialInput)
        {
            int duration = 0;
            if (!string.IsNullOrWhiteSpace(talkDetialInput))
            {
                string timeString = talkDetialInput.Substring(talkDetialInput.LastIndexOf(" ") + 1);
                if (timeString.ToLower().Trim().Equals("lightning"))
                {
                    duration = TrackConfig.LightningTalkDuration;
                }
                else
                {
                    Regex reg = new Regex("^\\d+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    MatchCollection matches = reg.Matches(timeString);
                    if (matches.Count > 0)
                    {
                        Int32.TryParse(matches[matches.Count - 1].Value, out duration);
                    }
                }
            }
            return duration;
        }
    }
}
