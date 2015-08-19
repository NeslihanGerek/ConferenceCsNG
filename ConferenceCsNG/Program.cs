using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceCsNG
{
    class Program
    {
        static TalkContainer talkContainer = new TalkContainer();

        static void Main(string[] args)
        {
            Dictionary<String, Dictionary<String, int>> sessions = new Dictionary<String, Dictionary<String, int>>();
            
            talkContainer.SortTalks(ref sessions);
            WriteSessionsToFile(sessions);           
        }

        static void WriteSessionsToFile(Dictionary<String, Dictionary<String, int>> sessions)
        {
            StreamWriter file = new StreamWriter("output.txt");
            bool haveLunch = false;
            int talkTime = 9;
            int talkMins = 0;
            string amPm = "AM";
            int LunchTime = 12;
            int trackCount = 1;
            
            foreach (KeyValuePair<String, Dictionary<String, int>> session in sessions)
            {
                if (!haveLunch)
                {
                    talkTime = 9;
                    talkMins = 0;
                    amPm = "AM";
                    file.WriteLine("Track " + trackCount + ":");
                    trackCount++;
                }
                else
                {
                    haveLunch = false;
                }
                foreach (KeyValuePair<String, int> sessionInfo in session.Value)
                {
                    string timeStr = getTimeString(talkTime, talkMins);
                    file.WriteLine(timeStr + amPm + " " + sessionInfo.Key + " " + sessionInfo.Value + "min");
                    if(sessionInfo.Value == 60)
                    {
                        talkTime++;
                    }
                    else if(talkMins + sessionInfo.Value >= 60)
                    {
                        talkTime++;
                        talkMins = talkMins + sessionInfo.Value - 60;
                    }
                    else
                    {
                        talkMins += sessionInfo.Value;
                    }
                }

                if (!haveLunch && amPm == "AM") // LunchTime
                {
                    talkTime = 13;
                    talkMins = 0;
                    amPm = "PM";
                    haveLunch = true;
                    file.WriteLine(LunchTime + ":00PM Lunch");
                }

                if(!haveLunch && amPm == "PM") // time for networking
                {
                    string timeStr = getTimeString(talkTime, talkMins);
                    file.WriteLine(timeStr + amPm + " Networking Event");
                }
            }

            file.Close();
        }

        static string getTimeString(int talkTime, int talkMins)
        {
            string timeStr = "";
            if(talkTime > 12)
            {
                talkTime -= 12;
            }
            if(talkTime < 10)
            {
                timeStr = "0";
            }
            timeStr += talkTime + ":";

            if (talkMins < 10)
            {
                timeStr += "0";
            }
            timeStr += talkMins;

            return timeStr;
        }
    }
}
