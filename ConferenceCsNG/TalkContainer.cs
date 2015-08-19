using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConferenceCsNG
{
    public class TalkContainer
    {
        public Dictionary<string, Int32> _talkDict = new Dictionary<string, Int32>();
        public int _durationMorning = 180;
        public int _minDurationEvening = 180;
        public int _maxDurationEvening = 240;

        public TalkContainer()
        {
            try
            {
                using (StreamReader sr = new StreamReader("input.txt"))
                {
                    String line = sr.ReadToEnd();
                    string[] talks = Regex.Split(line, "\r\n");
                    foreach (string talk in talks)
                    {
                        int inx = talk.LastIndexOf(" ");
                        Int32 duration = 0;
                        if (talk.Substring(inx + 1) == "lightning")
                        {
                            duration = 5;
                        }
                        else
                        {
                            duration = Convert.ToInt32(talk.Substring(inx + 1, 2));
                        }
                        _talkDict.Add(talk.Substring(0, inx), duration);
                        
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public void SortTalks(ref Dictionary<String, Dictionary <String, int>> sessions)
        {
            int maxDailyDuration = _durationMorning + _maxDurationEvening;
            int totalTime = _talkDict.Sum(x => x.Value);
            int days = totalTime / maxDailyDuration;
            if((totalTime % maxDailyDuration) > 0)
            {
                days++;
            }

            int numSessions = days * 2;
            
            for (int i = 0; i < numSessions; i++)
            {
                sessions.Add("session" + Convert.ToString(i + 1), new Dictionary<String, int>());
            }

            foreach (KeyValuePair<string, Int32> entry in _talkDict)
            {
                foreach (KeyValuePair<String, Dictionary <String, int>> session in sessions)
                {
                    if(Convert.ToInt32(session.Key[session.Key.Length-1]) % 2 == 1) // morning session
                    {
                        if (session.Value.Sum(x => x.Value) + entry.Value <= _durationMorning)
                        {
                            session.Value.Add(entry.Key, entry.Value);
                            break;
                        }
                    }
                    else //afternoon session
                    {
                        if (session.Value.Sum(x => x.Value) + entry.Value <= _maxDurationEvening)
                        {
                            session.Value.Add(entry.Key, entry.Value);
                            break;
                        }
                    }
                }
            }
        }
    }
}
