using System.Collections.Generic;
using System.Linq;

namespace Q3LogAnalyzer.Classes
{
    public class Record : Dictionary<string, string>
    {
        private string _time;

        public string Time
        {
            get
            {
                if (_time == null)
                {
                    string time = GetSafe("Time");
                    if (time.IndexOf(':') == 1) time = "0" + time;
                    _time = time.Count(c => c == ':') == 1
                        ? string.Format("00:{0}", time)
                        : time;
                }
                return _time;
            }
        }

        public string GetSafe(string key)
        {
            string value;
            return string.IsNullOrEmpty(key) || !TryGetValue(key, out value)
                ? string.Empty
                : value;
        }

        public override string ToString()
        {
            return Time;
        }
    }
}
