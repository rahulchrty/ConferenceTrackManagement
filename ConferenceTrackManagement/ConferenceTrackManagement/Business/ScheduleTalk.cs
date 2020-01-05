using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public class ScheduleTalk : IScheduleTalk
    {
        private ITalkDescription _talkDescription;
        private ITalkDescriptionValidation _talkDescriptionValidation;
        private IScheduleTrack _scheduleTrack;
        private IArrangeSchedules _arrangeSchedules;
        public ScheduleTalk(ITalkDescription talkDescription, ITalkDescriptionValidation talkDescriptionValidation,
                            IScheduleTrack scheduleTrack, IArrangeSchedules arrangeSchedules)
        {
            _talkDescription = talkDescription;
            _talkDescriptionValidation = talkDescriptionValidation;
            _scheduleTrack = scheduleTrack;
            _arrangeSchedules = arrangeSchedules;
        }
        public List<string> ProcessScheduling(List<string> talkInputList)
        {
            List<string> result = new List<string>();
            List<Talk> talkList = _talkDescription.GetTalksDescription(talkInputList);
            List<string> validationMessage = _talkDescriptionValidation.ValidateDescriptions(talkList);
            if (validationMessage.Count == 0)
            {
                List<Track> tracks = _scheduleTrack.GetTracks(talkList, TrackConfig.MorningSessionLength, TrackConfig.AfternoonSessionLength);
                result = _arrangeSchedules.GetSchedules(tracks);
            }
            else
            {
                result = validationMessage;
            }
            return result;
        }
    }
}
