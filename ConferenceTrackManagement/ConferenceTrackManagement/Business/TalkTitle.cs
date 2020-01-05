namespace ConferenceTrackManagement
{
    public class TalkTitle : ITalkTitle
    {
        public string ExtractTitle(string talkDetailInput)
        {
            string name = string.Empty;
            if (!string.IsNullOrWhiteSpace(talkDetailInput))
            {
                name = talkDetailInput.Substring(0, talkDetailInput.LastIndexOf(" ")).Trim();
            }
            return name;
        }
    }
}
