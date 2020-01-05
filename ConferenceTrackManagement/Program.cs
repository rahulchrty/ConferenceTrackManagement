using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConferenceTrackManagement
{
    public class Program
    {
        static void Main(string[] args)
        {
            ITalkTitle talkTitle = new TalkTitle();
            ITimeDuration timeDuration = new TimeDuration();
            ITalkDescription talkDescription = new TalkDescription(talkTitle, timeDuration);
            ITalkDescriptionValidation talkDescriptionValidation = new TalkDescriptionValidation();
            IAllocateTrack allocateTrack = new AllocateTrack();
            IScheduleTrack scheduleTrack = new ScheduleTrack(allocateTrack);
            IArrangeSchedules arrangeSchedules = new ArrangeSchedules();
            IScheduleTalk scheduleTalk = new ScheduleTalk(talkDescription, talkDescriptionValidation, scheduleTrack, arrangeSchedules);
            try
            {
                Console.WriteLine("Please enter the file path of the listed talks: ");
                string filePath = Console.ReadLine();
                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath);
                    List<string> scheduleResults = scheduleTalk.ProcessScheduling(lines.ToList());
                    Console.WriteLine();
                    foreach (string eachSchedule in scheduleResults)
                    {
                        Console.WriteLine(eachSchedule);
                    }
                }
                else
                {
                    Console.WriteLine("File not found");
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Error in reading file");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
