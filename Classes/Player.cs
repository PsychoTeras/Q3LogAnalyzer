namespace Q3LogAnalyzer.Classes
{
    public class Player
    {

#region Public fields

        public string Name;
        public Team Team;

#endregion

#region Ctor

        public Player() { }

        public Player(string name, Team team)
        {
            Name = name;
            Team = team;
        }

#endregion

#region Class methods

        public override string ToString()
        {
            return string.Format("{0} [{1}]", Name, Team);
        }

#endregion

    }
}
