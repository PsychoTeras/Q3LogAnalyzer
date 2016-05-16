using System.Collections.Generic;
using System.Linq;

namespace Q3LogAnalyzer.Classes
{
    public class PlayerList : Dictionary<string, Player>
    {

#region Properties

        public IEnumerable<string> Names
        {
            get
            {
                return this.
                    Select(p => p.Value).
                    GroupBy(p => p.Team, p => p.Name, (t, p) => new {P = p}).
                    SelectMany(p => p.P);
            }
        }

#endregion

#region Class methods

        public void AddRange(PlayerList players)
        {
            foreach (KeyValuePair<string, Player> pair in players)
            {
                Add(pair.Key, pair.Value);
            }
        }

        public Player CreateOrUpdatePlayer(string name, Team team)
        {
            Player player;
            if (TryGetValue(name, out player))
            {
                player.Team = team;
            }
            else
            {
                player = new Player(name, team);
                Add(name, player);
            }
            return player;
        }

        public Team GetTeam(string name)
        {
            Player player;
            return TryGetValue(name, out player)
                ? player.Team
                : Team.unknown;
        }

        public IEnumerable<Player> GetTeamPlayers(Team team)
        {
            return Values.Where(d => d.Team == team);
        }

#endregion

    }
}
