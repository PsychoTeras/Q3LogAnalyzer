using System;
using System.Collections.Generic;
using System.Linq;

namespace Q3LogAnalyzer.Classes
{
    public class Statistics
    {

#region Public fields

        public Player Player;

        public int KillsCount;
        public int TeamKillsCount;
        public int TotalDeathsCount;
        public int BackstabsCount;
        public int SuicidesCount;
        public int SwimmingsCount;
        
        public int TotalScores;

        public double EfficiencyPenalty;
        public double IntermediateEfficiency;
        public double Efficiency;

#endregion

#region Static ctor

        public static Statistics Calculate(RecordList records, Player player, GameType gameType)
        {
            Statistics statistics = new Statistics();
            statistics.GetStatistics(records, player, gameType);
            return statistics;
        }

#endregion

#region Ctor

        private Statistics() { }

#endregion

#region Class methods

        private void GetStatistics(RecordList records, Player player, GameType gameType)
        {
            Player = player;

            KillsCount = records.GetKillsCount(player.Name);
            TeamKillsCount = records.GetTeamKillsCount(player.Name);
            TotalDeathsCount = records.GetTotalDeathsCount(player.Name);
            BackstabsCount = records.GetBackstabsCount(player.Name);
            SuicidesCount = records.GetSuicidesCount(player.Name);
            SwimmingsCount = records.GetSwimingsCount(player.Name);

            CalculateTotalScores(gameType);

            switch (gameType)
            {
                case GameType.TeamDeathmatch:
                    CalculateIntermediateEfficiency();
                    break;
            }
        }

        private void CalculateTotalScores(GameType gameType)
        {
            switch (gameType)
            {
                case GameType.Deathmatch:
                    TotalScores = KillsCount + TeamKillsCount - SuicidesCount - SwimmingsCount;
                    break;
                case GameType.TeamDeathmatch:
                    TotalScores = KillsCount - TeamKillsCount - SuicidesCount - SwimmingsCount;
                    break;
            }
        }

        private void CalculateIntermediateEfficiency()
        {
            double victimsCoeff = Math.Max(TotalDeathsCount - BackstabsCount, 1);
            IntermediateEfficiency = Math.Max(TotalScores/victimsCoeff, 0);
            EfficiencyPenalty = IntermediateEfficiency/victimsCoeff;
        }

        public void CalculateEfficiencyPenalty(RecordList records, string player,
            Dictionary<string, Statistics> playersStatistics)
        {
            IEnumerable<Record> ffRecords = records.Where(d =>
            {
                string killer = d["Killer"];
                string victim = d["Victim"];
                if (killer == player && victim != player)
                {
                    Team killerTeam = records.GetTeam(killer);
                    Team victimTeam = records.GetTeam(victim);
                    return killerTeam == victimTeam;
                }
                return false;
            });

            Dictionary<string, short> penalties = new Dictionary<string, short>();
            foreach (Record record in ffRecords)
            {
                string victim = record["Victim"];
                if (!penalties.ContainsKey(victim))
                {
                    penalties.Add(victim, 1);
                }
                else
                {
                    penalties[victim]++;
                }
            }

            double penalty = penalties.Keys.Sum
                (
                    p => playersStatistics[p].EfficiencyPenalty * penalties[p]
                );

            IntermediateEfficiency = Math.Max(IntermediateEfficiency - penalty, 0);
        }

        public void CalculateFinal(double teamEfficiencySum)
        {
            Efficiency = teamEfficiencySum > 0
                ? 1/teamEfficiencySum*IntermediateEfficiency
                : 0;
        }

        public override string ToString()
        {
            return Player.ToString();
        }

#endregion

    }
}
