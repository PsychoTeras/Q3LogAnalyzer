using System;
using System.Linq;
using System.Collections.Generic;

namespace Q3LogAnalyzer.Classes
{
    public class Session
    {

#region Properties

        public string Map { get; private set; }
        public GameType GameType { get; private set; }
        public string StartTime { get; private set; }
        public string EndTime { get; private set; }

        public TeamList Teams { get; private set; }
        public PlayerList Players { get; private set; }
        public RecordList Records { get; private set; }

        public string Duration
        {
            get
            {
                if (!String.IsNullOrEmpty(StartTime) &&
                    !String.IsNullOrEmpty(EndTime))
                {
                    TimeSpan tsStart, tsEnd;
                    if (TimeSpan.TryParse(StartTime, out tsStart) &&
                        TimeSpan.TryParse(EndTime, out tsEnd))
                    {
                        return (tsEnd - tsStart).ToString();
                    }
                }
                return null;
            }
        } 

#endregion

#region Ctor

        public Session()
        {
            Teams = new TeamList();
            Players = new PlayerList();
            Records = new RecordList(Players);
        }

        public Session(string startTime, string map, GameType gameType)
            : this()
        {
            StartTime = startTime.Count(c => c == ':') == 1
                ? String.Format("00:{0}", startTime)
                : startTime;
            GameType = gameType;
            Map = map;
        }

#endregion

#region Class methods

        public void AcceptPlayer(string name, string team)
        {
            Team playerTeam;
            try
            {
                playerTeam = (Team)Enum.Parse(typeof(Team), team);
            }
            catch
            {
                playerTeam = Team.single_player;
            }

            if (!Teams.Contains(playerTeam))
            {
                Teams.Add(playerTeam);
            }

            Players.CreateOrUpdatePlayer(name, playerTeam);
        }

        public void AcceptRecord(string time, string killer, string victim, string weapon)
        {
            Records.AcceptRecord(time, killer, victim, weapon);
        }

        public void SetEndTime(string endTime)
        {
            EndTime = endTime.Count(c => c == ':') == 1
                ? string.Format("00:{0}", endTime)
                : endTime;
        }

        public void SortTeams()
        {
            Teams.Sort();
        }

        public Statistics[] CalculatePlayersStatistics()
        {
            //Get unique players list
            IEnumerable<string> players = Players.Names;

            //Calculate intermediate statistics
            Dictionary<string, Statistics> statistics = new Dictionary<string, Statistics>(Players.Count);
            foreach (KeyValuePair<string, Player> player in Players)
            {
                statistics.Add(player.Key, Statistics.Calculate(Records, player.Value));
            }

            switch (GameType)
            {
                case GameType.TeamDeathmatch:
                {
                    //Calculate efficiency penalty
                    foreach (string player in players)
                    {
                        statistics[player].CalculateEfficiencyPenalty(Records, player, statistics);
                    }

                    //Final calculation
                    IEnumerable<Team> teams = Players.Select(p => p.Value.Team);
                    foreach (Team t in teams)
                    {
                        Team team = t;
                        IEnumerable<Statistics> teamStats = statistics.Values.Where
                            (
                                s => s.Player.Team == team
                            );
                        float teamEfficiencySum = teamStats.Sum(s => s.IntermediateEfficiency);
                        foreach (Statistics stat in teamStats)
                        {
                            stat.CalculateFinal(teamEfficiencySum);
                        }
                    }
                    break;
                }
            }

            return statistics.Select(s => s.Value).ToArray();
        }

        public TeamStatistics CalculateTeamStatistics(IEnumerable<Statistics> playersStatistics)
        {
            return TeamStatistics.Calculate(Teams, playersStatistics);
        }

        public override string ToString()
        {
            string sGameType;
            switch (GameType)
            {
                case GameType.Deathmatch:
                    sGameType = "DM";
                    break;
                case GameType.TeamDeathmatch:
                    sGameType = "TDM";
                    break;
                default:
                    sGameType = "UNKNOWN";
                    break;
            }
            return String.Format("Map: {0} {1}. Duration: {2}.", Map, sGameType, Duration);
        }

#endregion
    }
}
