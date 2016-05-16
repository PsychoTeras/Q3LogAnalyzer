using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Q3LogAnalyzer.Helpers;

namespace Q3LogAnalyzer.Classes
{
    public class SessionList : List<Session>
    {

#region Private fields

        private static Regex _regexSessionRecord = new Regex(
            @"^ +(?<Time>\d+:\d+) (InitGame:.+?\\mapname\\(?<Map>.+?)\\.+?\\g_gametype\\(?<GameType>\d)|ClientUserinfoChanged:.{3}n\\(?<Name>.+?)\\.+/(?<Team>.+?)\\|Kill:( \d+){3}: (?<Killer>.+?) killed (?<Victim>.+?) by MOD_(?<Weapon>.+)|ShutdownGame:)",
            RegexOptions.ExplicitCapture | RegexOptions.Compiled);

#endregion

#region Static methods

        public static SessionList FromFile(string file)
        {
            SessionList list = new SessionList();
            list.LoadFromFile(file);
            return list;
        }

#endregion
         
#region Class methods

        public void LoadFromFile(string file)
        {
            if (string.IsNullOrEmpty(file) || !File.Exists(file)) return;

            Dictionary<string, string> clearNames = new Dictionary<string, string>
            {
                {"<world>", "<world>"}
            };

            using (StreamReader sr = new StreamReader(file))
            {
                Session currentSession = null;
                while (!sr.EndOfStream)
                {
                    Match mSessionRecord = _regexSessionRecord.Match(sr.ReadLine());
                    if (!mSessionRecord.Success) continue;

                    //Record time
                    string time = mSessionRecord.Groups["Time"].Value;

                    //Check for game started
                    if (mSessionRecord.Groups["Map"].Success)
                    {
                        string map = mSessionRecord.Groups["Map"].Value;
                        string sGameType = mSessionRecord.Groups["GameType"].Value;
                        GameType gameType = (GameType) Enum.Parse(typeof (GameType), sGameType);
                        currentSession = new Session(time, map, gameType);
                        Insert(0, currentSession);
                        continue;
                    }
                    
                    //Skip if session has not been started
                    if (currentSession == null) continue;
                    
                    //Check for player entered
                    if (mSessionRecord.Groups["Name"].Success)
                    {
                        string name = mSessionRecord.Groups["Name"].Value;
                        string clearName;
                        if (!clearNames.TryGetValue(name, out clearName))
                        {
                            clearNames[name] = clearName = Helper.RemoveNameColorizing(name);
                        }

                        string team = mSessionRecord.Groups["Team"].Value;
                        currentSession.AcceptPlayer(clearName, team);
                    }

                    //or check for kill event
                    else if (mSessionRecord.Groups["Killer"].Success)
                    {
                        string killer = clearNames[mSessionRecord.Groups["Killer"].Value];
                        string victim = clearNames[mSessionRecord.Groups["Victim"].Value];
                        string weapon = mSessionRecord.Groups["Weapon"].Value;
                        currentSession.AcceptRecord(time, killer, victim, weapon);
                    }

                    //or check for session was finised
                    else
                    {
                        currentSession.SetEndTime(time);
                        currentSession.SortTeams();
                        currentSession = null;
                    }
                }

                //Process unfinished (interrupted) session
                if (currentSession != null && currentSession.Records.Any())
                {
                    currentSession.SetEndTime(currentSession.Records.Last().Time);
                    currentSession.SortTeams();
                }
            }
        }

#endregion

    }
}
