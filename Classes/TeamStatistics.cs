using System.Collections.Generic;
using System.Linq;

namespace Q3LogAnalyzer.Classes
{
    public class TeamStatistics
    {

#region Properties

        public bool IsCompletedSession
        {
            get { return Scores.Keys.Count > 1 && WinnerTeam != Team.unknown; }
        }

        public string Map { get; private set; }
        public PlayerList Players { get; private set; }
        public Team WinnerTeam { get; private set; }
        public Dictionary<Team, int> Scores { get; private set; }

#endregion

#region Static methods

        public static TeamStatistics Calculate(string map, PlayerList players, IEnumerable<Team> teams, 
            IEnumerable<Statistics> playerStatistic)
        {
            TeamStatistics teamStatistics = new TeamStatistics(map, players);
            teamStatistics.GetStatistics(teams, playerStatistic);
            return teamStatistics;
        }

#endregion

#region Ctor

        private TeamStatistics(string map, PlayerList players)
        {
            Map = map;
            Players = players;
            Scores = new Dictionary<Team, int>();
        }

#endregion

#region Class methods

        private void GetStatistics(IEnumerable<Team> teams, IEnumerable<Statistics> playerStatistic)
        {
            Scores.Clear();
            int highScores = 0;
            WinnerTeam = Team.unknown;

            foreach (Team team in teams)
            {
                Team t = team;
                IEnumerable<Statistics> players = playerStatistic.Where(s => s.Player.Team == t);

                int scores = 0;
                foreach (Statistics statistics in players)
                {
                    scores += statistics.KillsCount - statistics.TeamKillsCount -
                              statistics.SuicidesCount - statistics.SwimmingsCount;
                }

                Scores.Add(team, scores);
                if (scores > highScores)
                {
                    WinnerTeam = team;
                    highScores = scores;
                }
            }
        }

        public override string ToString()
        {
            string info = string.Empty;
            if (IsCompletedSession)
            {
                info = string.Format("Winners: {0} [", WinnerTeam.ToString().ToUpper());
                foreach (KeyValuePair<Team, int> scores in Scores)
                {
                    info += string.Format("{0}: {1} ", scores.Key.ToString().ToUpper()[0], scores.Value);
                }
                info = info.TrimEnd() + "]";
            }
            return info;
        }

#endregion


    }
}
