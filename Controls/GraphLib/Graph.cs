using System.Collections.Generic;
using System.Drawing;

namespace Q3LogAnalyzer.Controls.GraphLib
{
    public interface ILegendItem
    {
        Color Color { get; }
        bool Hidden { get; }
        string Hint { get; }
    }

    public class Graph
    {
        private List<KeyValuePair<double, double>> _data = new List<KeyValuePair<double, double>>();
        private bool _dataSortPending = true;

        public int PointCount
        {
            get { return _data.Count; }
        }

        public List<KeyValuePair<double, double>> Points
        {
            get
            {
                if (_dataSortPending)
                {
                    _data.Sort((left, right) => left.Key.CompareTo(right.Key));
                    _dataSortPending = false;
                }
                return _data;
            }
        }

        public void AddPoint(double x, double y)
        {
            _data.Add(new KeyValuePair<double, double>(x, y));
            _dataSortPending = true;
        }

        public KeyValuePair<double, double> GetPointByIndex(int sortedPointIndex)
        {
            return _data[sortedPointIndex];
        }
    }
}
