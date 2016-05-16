using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Q3LogAnalyzer.Helpers
{
    public class ColumnSorter : IComparer
    {
        enum ColumnType : byte
        {
            String,
            Integer,
            Float
        }

        private static CaseInsensitiveComparer _stringComparer = 
            new CaseInsensitiveComparer();
        private static Dictionary<ListView, List<ColumnType>> _cachedColumnTypes 
            = new Dictionary<ListView, List<ColumnType>>();

        public int SortColumn { get; set; }
        public SortOrder Order { get; set; }

        public ColumnSorter()
        {
            SortColumn = 0;
            Order = SortOrder.None;
        }

        private ColumnType GetColumnType(ListView listView, int columnIdx)
        {
            if (!_cachedColumnTypes.ContainsKey(listView))
            {
                List<ColumnType> columnTypes = new List<ColumnType>();
                foreach (ColumnHeader column in listView.Columns)
                {
                    byte bColumnType;
                    byte.TryParse((string)column.Tag, out bColumnType);
                    columnTypes.Add((ColumnType)bColumnType);
                }
                _cachedColumnTypes.Add(listView, columnTypes);
            }
            return _cachedColumnTypes[listView][columnIdx];
        }

        public int Compare(object x, object y)
        {
            ListViewItem listviewX = (ListViewItem)x;
            ListViewItem listviewY = (ListViewItem)y;
            ListView listView = listviewX.ListView;

            int groupX = listView.Groups.IndexOf(listviewX.Group);
            int groupY = listView.Groups.IndexOf(listviewY.Group);

            int compareResult = 0;
            if (groupX != groupY)
            {
                compareResult = groupX.CompareTo(groupY);
            }
            else
            {
                if (listviewX.SubItems.Count == 1) return -1;
                if (listviewY.SubItems.Count == 1) return 1;

                string textX = listviewX.SubItems[SortColumn].Text;
                string textY = listviewY.SubItems[SortColumn].Text;
                ColumnType columnType = GetColumnType(listView, SortColumn);
                switch (columnType)
                {
                    case ColumnType.String:
                        compareResult = _stringComparer.Compare(textX, textY);
                        break;
                    case ColumnType.Integer:
                        int iX, iY;
                        int.TryParse(textX, out iX);
                        int.TryParse(textY, out iY);
                        compareResult = iX.CompareTo(iY);
                        break;
                    case ColumnType.Float:
                        float fX, fY;
                        float.TryParse(textX, out fX);
                        float.TryParse(textY, out fY);
                        compareResult = fX.CompareTo(fY);
                        break;
                }
            }

            switch (Order)
            {
                case SortOrder.Ascending:
                    return compareResult;
                case SortOrder.Descending:
                    return -compareResult;
                default:
                    return 0;
            }
        }
    }
}
