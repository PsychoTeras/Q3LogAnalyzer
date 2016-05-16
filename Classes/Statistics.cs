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

        public float EfficiencyPenalty;
        public float IntermediateEfficiency;
        public float Efficiency;

#endregion

#region Static ctor

        public static Statistics Calculate(RecordList records, Player player)
        {
            Statistics statistics = new Statistics();
            statistics.GetStatistics(records, player);
            return statistics;
        }

#endregion

#region Ctor

        private Statistics() { }

#endregion

#region Class methods

        private void GetStatistics(RecordList records, Player player)
        {
            Player = player;

            KillsCount = records.GetKillsCount(player.Name);
            TeamKillsCount = records.GetTeamKillsCount(player.Name);
            TotalDeathsCount = records.GetTotalDeathsCount(player.Name);
            BackstabsCount = records.GetBackstabsCount(player.Name);
            SuicidesCount = records.GetSuicidesCount(player.Name);
            SwimmingsCount = records.GetSwimingsCount(player.Name);

            CalculateTotalScores();
            CalculateIntermediateEfficiency();
        }

        private void CalculateTotalScores()
        {
            TotalScores = KillsCount - TeamKillsCount - SuicidesCount - SwimmingsCount;
        }

        private void CalculateIntermediateEfficiency()
        {
            float totalVictims = TotalDeathsCount - BackstabsCount - SuicidesCount - SwimmingsCount;
            float victimsCoeff = totalVictims*0.3f;
            IntermediateEfficiency = Math.Max(TotalScores*victimsCoeff, 0);
            EfficiencyPenalty = (float) (IntermediateEfficiency <= 0 ? 0 : Math.Log10(IntermediateEfficiency));
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

            float penalty = penalties.Keys.Sum
                (
                    p => playersStatistics[p].EfficiencyPenalty * penalties[p]
                );

            IntermediateEfficiency = Math.Max(IntermediateEfficiency - penalty, 0);
        }

        public void CalculateFinal(float teamEfficiencySum)
        {
            Efficiency = teamEfficiencySum > 0
                ? 1/teamEfficiencySum*IntermediateEfficiency
                : 0;
        }

#endregion

    }
}
