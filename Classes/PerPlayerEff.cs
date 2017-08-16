using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Q3LogAnalyzer.Classes
{
    public class PerPlayerEff
    {

#region Public fields
    
        public double EfficiencySum;
        public int EfficiencySumCnt;
        public Dictionary<string, int> WeaponsUsingStat;

#endregion

#region Ctor

        public PerPlayerEff()
        {
            WeaponsUsingStat = new Dictionary<string, int>();
        }

#endregion

#region Class methods

        public void AcceptEfficiency(double efficiency)
        {
            EfficiencySum += efficiency;
            EfficiencySumCnt++;
        }

        public void AcceptWeapon(string weapon)
        {
            if (!WeaponsUsingStat.ContainsKey(weapon))
            {
                WeaponsUsingStat.Add(weapon, 1);
            }
            else
            {
                WeaponsUsingStat[weapon] += 1;
            }
        }

        public double CalculateEfficiency()
        {
            return EfficiencySum / EfficiencySumCnt;
        }

        public string CalculateWeaponsUsing()
        {
            StringBuilder sb = new StringBuilder();

            var stat = WeaponsUsingStat.Select(r => r).OrderByDescending(r => r.Value);
            foreach (KeyValuePair<string, int> v in stat)
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }
                sb.AppendFormat("{0} [{1}]", v.Key, v.Value);
            }

            return sb.ToString();
        }

#endregion

    }
}
