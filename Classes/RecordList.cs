using System.Linq;
using System.Collections.Generic;

namespace Q3LogAnalyzer.Classes
{
    public class RecordList : List<Record>
    {

#region Private fields

        private PlayerList _players;

        private static string[] _groups =
        {
            "Time", "Killer", "Victim", "Weapon"
        };

#endregion

#region Properties

        public static string[] Groups
        {
            get { return _groups; }
        }

        public PlayerList Players
        {
            get { return _players; }
        }

#endregion

#region Ctor

        public RecordList(PlayerList players)
        {
            _players = players;
        }

#endregion

#region Class methods

        public void AcceptRecord(string time, string killer, string victim, string weapon)
        {
            Record record = new Record();
            record.Add("Time", time);
            record.Add("Killer", killer);
            record.Add("Victim", victim);
            record.Add("Weapon", weapon);
            Add(record);
        }

        public Team GetTeam(string name)
        {
            Player player;
            return Players.TryGetValue(name, out player)
                ? player.Team
                : Team.unknown;
        }

#endregion

#region Statistic methods

        public IEnumerable<KeyValuePair<EventType, Record>> GetTeamRecords(Team team, HashSet<string> teamPlayers)
        {
            var records = new List<KeyValuePair<EventType, Record>>();
            records.AddRange
                (
                    this.Where(d =>
                    {
                        string killer = d["Killer"];
                        string victim = d["Victim"];
                        return teamPlayers.Contains(killer) && !teamPlayers.Contains(victim);
                    }).Select(d => new KeyValuePair<EventType, Record>(EventType.Kill, d))
                );
            records.AddRange
                (
                    this.Where(d =>
                    {
                        string killer = d["Killer"];
                        string victim = d["Victim"];
                        return (teamPlayers.Contains(killer) || killer == "<world>") && teamPlayers.Contains(victim);
                    }).Select(d => new KeyValuePair<EventType, Record>(EventType.TeamKill, d))
                );
            records.Sort((d1, d2) => d1.Value.Time.CompareTo(d2.Value.Time));
            return records;
        }

        public IEnumerable<Record> GetKills(string player)
        {
            return this.Where(d =>
            {
                string killer = d["Killer"];
                string victim = d["Victim"];
                if (killer == player && victim != player)
                {
                    Team killerTeam = _players.GetTeam(killer);
                    Team victimTeam = _players.GetTeam(victim);
                    return killerTeam != victimTeam;
                }
                return false;
            });
        }

        public int GetKillsCount(string player)
        {
            return GetKills(player).Count();
        }

        public IEnumerable<Record> GetTeamKills(string player)
        {
            return this.Where(d =>
            {
                string killer = d["Killer"];
                string victim = d["Victim"];
                if (killer == player && victim != player)
                {
                    Team killerTeam = _players.GetTeam(killer);
                    Team victimTeam = _players.GetTeam(victim);
                    return killerTeam == victimTeam;
                }
                return false;
            });
        }

        public int GetTeamKillsCount(string player)
        {
            return GetTeamKills(player).Count();
        }

        public IEnumerable<Record> GetTotalDeaths(string player)
        {
            return this.Where(d => d["Victim"] == player);
        }

        public int GetTotalDeathsCount(string player)
        {
            return GetTotalDeaths(player).Count();
        }

        public IEnumerable<Record> GetBackstabs(string player)
        {
            return this.Where(d =>
            {
                string killer = d["Killer"];
                string victim = d["Victim"];
                if (killer != player && victim == player)
                {
                    Team killerTeam = _players.GetTeam(killer);
                    Team victimTeam = _players.GetTeam(victim);
                    return killerTeam == victimTeam;
                }
                return false;
            });
        }

        public int GetBackstabsCount(string player)
        {
            return GetBackstabs(player).Count();
        }

        public IEnumerable<Record> GetSuicides(string player)
        {
            return this.Where(d => d["Killer"] == player && d["Victim"] == player);
        }

        public int GetSuicidesCount(string player)
        {
            return GetSuicides(player).Count();
        }

        public IEnumerable<Record> GetSwimings(string player)
        {
            return this.Where(d => (d["Killer"] == "<world>" && d["Victim"] == player));
        }

        public int GetSwimingsCount(string player)
        {
            return GetSwimings(player).Count();
        }

#endregion
    }
}
