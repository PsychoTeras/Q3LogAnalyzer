using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Q3LogAnalyzer.Classes;
using Q3LogAnalyzer.Controls.GraphLib;
using Q3LogAnalyzer.Helpers;

namespace Q3LogAnalyzer.Forms
{
    public partial class frmMain : Form
    {
        private const string TextNoValue = "-";
        private const string TextTotal = "TOTAL";

        private string _currentPath;
        private string _currentLogFile;
        private bool _copyLogLocally;

        private SessionList _sessions;
        private Dictionary<ListView, ColumnSorter> _lvColumnSorters;

        InteractiveGraphViewer.Tracker _locationTracker;
        InteractiveGraphViewer.FloatingHint _floatingHint;

        private Font _fontBold;
        private Brush _brushBlack;
        private Brush _brushForeground;
        private Brush _brushHighlight;
        private Brush _brushSelection;
        private Brush _brushRowTotal;
        private Pen _penGrayDash;

        public frmMain()
        {
            InitializeComponent();
            InitializeApplication();
        }

        private void ParseCommandLineParams()
        {
            string[] args = Environment.GetCommandLineArgs();
            for (int i = 1; i < args.Length; i++)
            {
                string arg = args[i].ToLower();
                switch (arg)
                {
                    case "-c":
                        _copyLogLocally = true;
                        break;
                    case "-f":
                        if (++i < args.Length) _currentLogFile = args[i];
                        break;
                }
            }
        }

        private void ProcessStartParams()
        {
            if (!string.IsNullOrEmpty(_currentLogFile))
            {
                using (new frmLoading())
                {
                    if (LoadLogFile(_currentLogFile) && _copyLogLocally)
                    {
                        string destFileName = Path.GetFileName(_currentLogFile);
                        string destFilePath = Path.Combine(Application.StartupPath, destFileName);
                        File.Copy(_currentLogFile, destFilePath, true);
                    }
                }
                Helper.BringWindowToFront(Handle);
            }
        }

        private void InitializeApplication()
        {
            _lvColumnSorters = new Dictionary<ListView, ColumnSorter>();
            _currentPath = (Directory.GetCurrentDirectory() + @"\");

            _locationTracker = graphViewer.CreateTracker(Color.Gray);
            _floatingHint = graphViewer.CreateFloatingHint();
            _floatingHint.FillColor = Color.FromArgb(200, Color.LightGreen);

            _fontBold = new Font(lvStatistic.Font, FontStyle.Italic);
            _brushBlack = new SolidBrush(lvStatistic.ForeColor);
            _brushForeground = new SolidBrush(Color.FromKnownColor(KnownColor.Window));
            _brushHighlight = new SolidBrush(Color.FromKnownColor(KnownColor.HighlightText));
            _brushSelection = new SolidBrush(Color.FromKnownColor(KnownColor.Highlight));
            _brushRowTotal = new SolidBrush(Color.FromArgb(25, Color.DodgerBlue));
            _penGrayDash = new Pen(Color.Black) { DashStyle = DashStyle.Dot };

            ListViewColumnClick(lvStatistic, new ColumnClickEventArgs(1));

            ParseCommandLineParams();

            ProcessStartParams();
        }

        private void LockUpdate()
        {
            lvStatistic.BeginUpdate();
            lvDetailed.BeginUpdate();
            lvSummary.BeginUpdate();
        }

        private void UnlockUpdate()
        {
            lvStatistic.EndUpdate();
            lvDetailed.EndUpdate();
            lvSummary.EndUpdate();
        }

        private void FillDetailedListView(TimeSpan startTime, IEnumerable<string> groupNames, 
            ListViewGroup group, RecordList records)
        {
            //Create records
            IEnumerable<string> subColumnNames = groupNames.Skip(1);

            List<ListViewItem> items = new List<ListViewItem>(records.Count);
            foreach (Record record in records)
            {
                TimeSpan time = TimeSpan.Parse(record.Time) - startTime;
                string text = SecondsToString(time.TotalSeconds);
                ListViewItem item = new ListViewItem(text) {Group = group};
                foreach (string subColumnName in subColumnNames)
                {
                    text = record.GetSafe(subColumnName);
                    item.SubItems.Add(text);
                }
                items.Add(item);
            }

            lvDetailed.Items.AddRange(items.ToArray());
        }

        private void FillStatisticListView(ListViewGroup group, Statistics[] statistics, Session session)
        {
            List<ListViewItem> items = new List<ListViewItem>(statistics.Length + 1);

            ListViewItem item;
            
            if (session.GameType == GameType.TeamDeathmatch)
            {
                item = new ListViewItem(TextTotal) {Group = group};
                items.Add(item);
            }

            //Create records
            foreach (Statistics stat in statistics)
            {
                item = new ListViewItem(stat.Player.Name) { Group = group, Tag = stat.Player.Name };
                switch (session.GameType)
                {
                    case GameType.Deathmatch:
                        item.SubItems.Add(TextNoValue);
                        item.SubItems.Add(stat.TotalScores.ToString());
                        item.SubItems.Add(TextNoValue);
                        item.SubItems.Add(TextNoValue);
                        item.SubItems.Add(stat.TotalDeathsCount.ToString());
                        item.SubItems.Add(TextNoValue);
                        item.SubItems.Add(stat.SuicidesCount.ToString());
                        item.SubItems.Add(stat.SwimmingsCount.ToString());
                        item.SubItems.Add(TextNoValue);
                        break;
                    case GameType.TeamDeathmatch:
                        item.SubItems.Add(stat.Player.Team.ToString());
                        item.SubItems.Add(stat.TotalScores.ToString());
                        item.SubItems.Add(stat.KillsCount.ToString());
                        item.SubItems.Add(stat.TeamKillsCount.ToString());
                        item.SubItems.Add(stat.TotalDeathsCount.ToString());
                        item.SubItems.Add(stat.BackstabsCount.ToString());
                        item.SubItems.Add(stat.SuicidesCount.ToString());
                        item.SubItems.Add(stat.SwimmingsCount.ToString());
                        item.SubItems.Add(stat.Efficiency.ToString("0.##"));
                        break;

                }
                items.Add(item);
            }

            lvStatistic.Items.AddRange(items.ToArray());
        }

        private void ReloadSessions()
        {
            if (_sessions == null) return;

            //Begin update content. Prevent blinking while updating
            Cursor = Cursors.WaitCursor;
            LockUpdate();

            //Clear all data. lvLogs.Items.Clear() will clear content only without columns
            lvStatistic.Items.Clear();
            lvStatistic.Groups.Clear();

            lvDetailed.Clear();
            lvDetailed.Groups.Clear();

            lvSummary.Items.Clear();
            lvSummary.Groups.Clear();

            //Recreate colums
            IEnumerable<string> groupNames = RecordList.Groups;
            List<ColumnHeader> headers = new List<ColumnHeader>(groupNames.Count());
            foreach (string groupName in groupNames)
            {
                ColumnHeader header = new ColumnHeader();
                header.Text = groupName;
                headers.Add(header);
            }
            lvDetailed.Columns.AddRange(headers.ToArray());

            int processedSessionsCount = 0,
                sessionsCount = btnFilterLast50Games.Checked
                    ? Math.Min(_sessions.Count, 50)
                    : _sessions.Count,
                sessionIdx = sessionsCount;

            List<ListViewGroup> groupsStatistic = new List<ListViewGroup>(sessionIdx);
            List<ListViewGroup> groupsDetailed = new List<ListViewGroup>(sessionIdx);
            List<TeamStatistics> sessionsTeamStatistics = new List<TeamStatistics>(sessionIdx);

            foreach (Session session in _sessions)
            {
                //Calculate players statistics
                Statistics[] statistics = session.CalculatePlayersStatistics();
                if (btnFilterEmptyRecords.Checked)
                {
                    bool ignoreSession = false;
                    switch (session.GameType)
                    {
                        case GameType.Deathmatch:
                            ignoreSession = statistics.Length <= 1 || statistics.Sum(s => Math.Abs(s.TotalScores)) <= 1;
                            break;
                        case GameType.TeamDeathmatch:
                            ignoreSession = statistics.Sum(s => s.Efficiency) <= 1;
                            break;
                    }
                    if (ignoreSession) continue;
                }


                //Fill sessions statistics
                ListViewGroup groupStatistic = new ListViewGroup { Tag = session };
                FillStatisticListView(groupStatistic, statistics, session);
                if (groupStatistic.Items.Count > 0)
                {
                    groupsStatistic.Add(groupStatistic);
                }

                //Fill detailed sessions statistics
                TimeSpan startTime = TimeSpan.Parse(session.StartTime);
                ListViewGroup groupDetailed = new ListViewGroup { Tag = session };
                FillDetailedListView(startTime, groupNames, groupDetailed, session.Records);
                if (groupDetailed.Items.Count > 0)
                {
                    groupsDetailed.Add(groupDetailed);
                }

                //Calculate sessions summary statistics
                string sessionHeader = string.Format("Session {0}. {1}", sessionIdx--, session);
                switch (session.GameType)
                {
                    case GameType.Deathmatch:
                    {
                        Statistics winner = statistics.First(s => s.TotalScores == statistics.Max(sm => sm.TotalScores));
                        sessionHeader = string.Format("{0} Winner: {1}", sessionHeader, winner.Player.Name);
                        break;
                    }
                    case GameType.TeamDeathmatch:
                    {
                        TeamStatistics teamStatistics = session.CalculateTeamStatistics(statistics);
                        if (teamStatistics.IsCompletedSession)
                        {
                            sessionHeader = string.Format("{0} {1}", sessionHeader, teamStatistics);
                            sessionsTeamStatistics.Add(teamStatistics);
                        }
                        break;
                    }
                }
                groupStatistic.Header = groupDetailed.Header = sessionHeader;

                if (btnFilterLast50Games.Checked && ++processedSessionsCount == 50) break;
            }

            List<ListViewItem> itemsSummary = new List<ListViewItem>(sessionIdx);
            IEnumerable<string> maps = sessionsTeamStatistics.Select(s => s.Session).Select(s => s.Map).Distinct();
            foreach (string m in maps)
            {
                string map = m;
                IEnumerable<TeamStatistics> mapTeamStatistics = sessionsTeamStatistics.Where
                    (
                        s => s.Session.Map == map && s.Session.Players.Count == 4 && s.Session.Duration.Minutes >= 13
                    );
                IEnumerable<TeamStatistics> redWinners = mapTeamStatistics.Where(s => s.WinnerTeam == Team.red);
                int redWinsNum = redWinners.Count();
                IEnumerable<TeamStatistics> blueWinners = mapTeamStatistics.Where(s => s.WinnerTeam == Team.blue);
                int blueWinsNum = blueWinners.Count();
                ListViewItem item = new ListViewItem(map);
                item.SubItems.Add(redWinsNum.ToString());
                item.SubItems.Add(blueWinsNum.ToString());
                item.SubItems.Add(redWinsNum > blueWinsNum ? "RED" : redWinsNum < blueWinsNum ? "BLUE" : "DRAW");
                float rate = redWinsNum > blueWinsNum
                    ? (blueWinsNum == 0 ? 1 : (float) blueWinsNum/redWinsNum)
                    : redWinsNum < blueWinsNum
                        ? (redWinsNum == 0 ? 1 : (float) redWinsNum/blueWinsNum)
                        : 0f;
                item.SubItems.Add(redWinsNum == blueWinsNum ? TextNoValue : rate.ToString("0.##"));
                itemsSummary.Add(item);
            }

            lvStatistic.Groups.AddRange(groupsStatistic.ToArray());
            lvDetailed.Groups.AddRange(groupsDetailed.ToArray());
            lvSummary.Items.AddRange(itemsSummary.ToArray());

            //Prepare columns
            lvStatistic.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvStatistic.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            lvStatistic.Columns[lvStatistic.Columns.Count - 1].Width = 65;
            lvStatistic.Sort();

            lvDetailed.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvDetailed.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            lvDetailed.Columns[lvDetailed.Columns.Count - 1].Width = 160;
            lvDetailed.Sort();

            lvSummary.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvSummary.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            lvSummary.Columns[lvSummary.Columns.Count - 1].Width = 160;
            lvSummary.Sort();

            CollapseAll(true);

            RefreshPerfGraph();

            //End update
            UnlockUpdate();
            Cursor = Cursors.Default;
        }

        private bool LoadLogFile(string logFileName)
        {
            if (string.IsNullOrEmpty(logFileName) || !File.Exists(logFileName))
            {
                return false;
            }

            Cursor = Cursors.WaitCursor;

            HRTimer timer = HRTimer.CreateAndStart();

            _sessions = SessionList.FromFile(logFileName);
            ReloadSessions();

            double loadedAt = timer.StopWatch();
            slFileName.Text = string.Format("File: {0}", logFileName);
            slLoadingTime.Text = string.Format("Loaded at {0:0.000} msec", loadedAt);
            
            _currentLogFile = logFileName;

            Cursor = Cursors.Default;

            return true;
        }

        private void BtnLogOpenClick(object sender, EventArgs e)
        {
            if (ofdQ3Log.ShowDialog() == DialogResult.OK)
            {
                LoadLogFile(ofdQ3Log.FileName);
            }
        }

        public static void ExportToExcel(ListView lv, string prmBookName, string prmPath)
        {
            try
            {
                if (lv.Items.Count == 0)
                {
                    MessageBox.Show("No items to export", "System Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                DirectoryInfo di = new DirectoryInfo(prmPath);
                if (!di.Exists)
                {
                    di.Create();
                }

                string fullFileName = prmPath + prmBookName + ".xls";
                using (StreamWriter sw = new StreamWriter(fullFileName, false))
                {
                    sw.AutoFlush = true;
                    for (int col = 0; col < lv.Columns.Count; col++)
                    {
                        sw.Write("\t" + lv.Columns[col].Text);
                    }

                    for (int row = 0; row < lv.Items.Count; row++)
                    {
                        if (lv.Items[row].SubItems.Count != lv.Columns.Count) continue;

                        string st1 = row == 0 ? "\n" : "";
                        for (int col = 0; col < lv.Columns.Count; col++)
                        {
                            st1 += "\t" + lv.Items[row].SubItems[col].Text;
                        }
                        sw.WriteLine(st1);
                    }
                }

                MessageBox.Show("Finished:\n" + fullFileName, "Export to Excel", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmMainKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F3:
                    BtnLogOpenClick(null, null);
                    break;
            }
        }

        private void BtnExportClick(object sender, EventArgs e)
        {
            string fileName = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
            ExportToExcel(lvStatistic, fileName, _currentPath);
        }

        private void ExpandAll()
        {
            Helper.SetGroupCollapse(lvStatistic, GroupState.EXPANDED);
            Helper.SetGroupCollapse(lvDetailed, GroupState.EXPANDED);
        }

        private void CollapseAll(bool expandFirst)
        {
            Helper.SetGroupCollapse(lvStatistic, GroupState.COLLAPSED);
            Helper.SetGroupCollapse(lvDetailed, GroupState.COLLAPSED);
            if (expandFirst)
            {
                Helper.SetGroupCollapse(lvStatistic, GroupState.EXPANDED, 1);
                if (lvStatistic.Groups.Count > 0 && lvDetailed.Groups.Count > 0 &&
                    lvStatistic.Groups[0].Tag == lvDetailed.Groups[0].Tag)
                {
                    Helper.SetGroupCollapse(lvDetailed, GroupState.EXPANDED, 1);
                }
            }
        }

        private void BtnExpandAllClick(object sender, EventArgs e)
        {
            ExpandAll();
        }

        private void BtnCollapsAllClick(object sender, EventArgs e)
        {
            CollapseAll(false);
        }

        private void ListViewColumnClick(object sender, ColumnClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            ColumnSorter sorter;
            ListView listView = (ListView) sender;
            if (!_lvColumnSorters.ContainsKey(listView))
            {
                sorter = new ColumnSorter();
                _lvColumnSorters.Add(listView, sorter);
                listView.ListViewItemSorter = sorter;
            }
            else
            {
                sorter = _lvColumnSorters[listView];
            }

            if (e.Column == sorter.SortColumn)
            {
                sorter.Order = sorter.Order == SortOrder.Ascending
                    ? SortOrder.Descending
                    : SortOrder.Ascending;
            }
            else
            {
                sorter.SortColumn = e.Column;
                sorter.Order = SortOrder.Ascending;
            }

            listView.Sort();
            listView.SetSortIcon(sorter.SortColumn, sorter.Order);

            Cursor = Cursors.Default;
        }

        private void BtnFilterLast50GamesClick(object sender, EventArgs e)
        {
            ReloadSessions();
        }

        private void BtnFilterEmptyRecordsClick(object sender, EventArgs e)
        {
            ReloadSessions();
        }

        private void PopulateGraph(TimeSpan startTime, IEnumerable<Record> records, string type, Color color)
        {
            if (!records.Any()) return;

            Graph graph = new Graph();
            int i = 0;
            foreach (Record record in records)
            {
                TimeSpan time = TimeSpan.Parse(record.Time) - startTime;
                graph.AddPoint(time.TotalSeconds, ++i);
            }
            GraphViewer.DisplayedGraph displayedGraph = graphViewer.AddGraph(graph, color, 2, type);
            displayedGraph.DefaultPointMarkingStyle = GraphViewer.DisplayedGraph.PointMarkingStyle.Circle;
            displayedGraph.Tag = records.ToArray();
        }

        private void PopulateGraphTotal(TimeSpan startTime, IEnumerable<KeyValuePair<EventType, Record>> records, 
            Color color)
        {
            if (!records.Any()) return;

            int scores = 0;
            Graph graph = new Graph();
            foreach (KeyValuePair<EventType, Record> pair in records)
            {
                EventType eventType = pair.Key;
                scores += eventType == EventType.Kill ? 1 : -1;
                Record record = pair.Value;
                TimeSpan time = TimeSpan.Parse(record.Time) - startTime;
                graph.AddPoint(time.TotalSeconds, scores);
            }
        
            string name = string.Format("{0} team", color.Name);
            GraphViewer.DisplayedGraph displayedGraph = graphViewer.AddGraph(graph, color, 2, name);
            displayedGraph.DefaultPointMarkingStyle = GraphViewer.DisplayedGraph.PointMarkingStyle.Circle;
            displayedGraph.Tag = records.ToArray();
        }

        private void RefreshPerfGraph()
        {
            graphViewer.ResetGraphs();
            legendControl.Hide();

            if (lvStatistic.SelectedItems.Count == 0) return;

            ListViewItem item = lvStatistic.SelectedItems[0];
            Session session = (Session) item.Group.Tag;
            string player = (string) item.Tag;

            RecordList records = session.Records;
            TimeSpan startTime = TimeSpan.Parse(session.StartTime);

            //Populate selected player info
            if (player != null)
            {
                //Kills
                HashSet<Record> teamKills = new HashSet<Record>(records.GetTeamKills(player));
                IEnumerable<Record> kills = records.GetKills(player).Where(r => !teamKills.Contains(r));
                PopulateGraph(startTime, kills, "Kills", Color.DodgerBlue);

                //Deaths
                IEnumerable<Record> deaths = records.GetTotalDeaths(player);
                PopulateGraph(startTime, deaths, "Deaths", Color.IndianRed);
            }

            //otherwise populate total session info
            else
            {
                TeamList teams = session.Teams;
                foreach (Team team in teams)
                {
                    Color teamColor = Color.FromName(team.ToString());
                    HashSet<string> playerNames = new HashSet<string>
                        (
                            session.Players.GetTeamPlayers(team).Select(d => d.Name)
                        );
                    var teamRecords = records.GetTeamRecords(team, playerNames);
                    PopulateGraphTotal(startTime, teamRecords, teamColor);
                }
            }

            legendControl.Visible = !graphViewer.IsEmpty;
        }

        private void LvStatisticSelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPerfGraph();
        }

        private string SecondsToString(int seconds)
        {
            return string.Format("{0:00}:{1:00}", (seconds / 60) % 60, seconds % 60);
        }

        private string SecondsToString(double seconds)
        {
            return SecondsToString((int)seconds);
        }

        private void GraphViewerFormatXValue(object sender, double seconds, ref string formattedValue)
        {
            formattedValue = SecondsToString(seconds);
        }

        private void GraphViewerFormatYValue(object sender, double value, ref string formattedValue)
        {
            formattedValue = Math.Round(value).ToString();
        }

        private void GraphViewerMouseLeave(object sender, EventArgs e)
        {
            _floatingHint.Hidden = true;
            _locationTracker.Hidden = true;
        }

        private void GraphViewerMouseMove(InteractiveGraphViewer sender, GraphMouseEventArgs e)
        {
            if (!(_locationTracker.Hidden = graphViewer.IsEmpty))
            {
                _locationTracker.X = e.DataX;
                _locationTracker.Y = e.DataY;
            }

            int distanceSquare;
            var graphPoint = graphViewer.FindNearestGraphPoint(e.DataX, e.DataY, true, out distanceSquare);
            if (graphPoint != null && distanceSquare <= 16)
            {
                GraphViewer.DisplayedGraph.DisplayedPoint point = graphPoint.NearestReferencePoint;
                GraphViewer.DisplayedGraph graph = graphPoint.Graph;

                string @event = string.Empty, metric;
                bool isPrivateStatistic = graph.Tag is IEnumerable<Record>;
                if (isPrivateStatistic)
                {
                    metric = "#";
                    Record[] records = (Record[]) graph.Tag;
                    switch (graph.Hint)
                    {
                        case "Kills":
                            @event = string.Format("Victim [{0}]", records[point.Index]["Victim"]);
                            break;
                        case "Deaths":
                            @event = string.Format("Killer [{0}]", records[point.Index]["Killer"]);
                            break;
                    }
                }
                else
                {
                    metric = "Scores: ";
                    var records = (KeyValuePair<EventType, Record>[]) graph.Tag;
                    KeyValuePair<EventType, Record> record = records[point.Index];
                    @event = string.Format("{2} [{0}] killed by [{1}]", records[point.Index].Value["Victim"],
                        records[point.Index].Value["Killer"], record.Key == EventType.Kill ? "+" : "-");
                }

                string hint = string.Format("{3}{1}\n{2} at {0}", SecondsToString(point.X),
                    point.Y, @event, metric);
                _floatingHint.Show(e, hint);
            }
            else
            {
                _floatingHint.Hidden = true;
            }
        }

        private void LvStatisticDrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private Dictionary<string, Color> _colsColor = new Dictionary<string, Color>
        {
            {"Scores", Color.LightYellow},
            {"Frags", Color.LightCyan},
            {"Team kills", Color.LightCyan},
            {"Total deaths", Color.SeaShell},
            {"Backstabs", Color.SeaShell},
            {"Suicides", Color.SeaShell},
            {"Swimmings", Color.SeaShell},
            {"Efficiency", Color.AliceBlue},
        };

        private void LvStatisticDrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            Rectangle rect;
            bool isSelected = e.Item.Selected;
            Rectangle r1 = e.Item.GetBounds(ItemBoundsPortion.Label);
            
            e.DrawBackground();

            if (e.Item.SubItems.Count == 1)
            {
                rect = new Rectangle(r1.X, r1.Y, e.Bounds.Width - r1.X, e.Bounds.Height);
                Brush brush = isSelected
                    ? _brushSelection
                    : btnColorizeStatistics.Checked ? _brushRowTotal : _brushForeground;
                e.Graphics.FillRectangle(brush, rect);
                e.Graphics.DrawString(TextTotal, _fontBold, isSelected ? _brushHighlight : _brushBlack, rect);
                if (!btnColorizeStatistics.Checked)
                {
                    e.Graphics.DrawLine(_penGrayDash, new Point(rect.Left, rect.Bottom - 1),
                        new Point(rect.Right - 1, rect.Bottom - 1));
                }
            }
            else
            {
                int xShift = e.ColumnIndex == 0 ? r1.Left : 0;
                rect = new Rectangle(e.Bounds.X + xShift, e.Bounds.Y, e.Bounds.Width - xShift, e.Bounds.Height);

                if (btnColorizeStatistics.Checked)
                {
                    Color colColor;
                    if (!_colsColor.TryGetValue(lvStatistic.Columns[e.ColumnIndex].Text, out colColor))
                    {
                        colColor = lvDetailed.BackColor;
                    }
                    using (Brush brush = new SolidBrush(colColor))
                    {
                        e.Graphics.FillRectangle(isSelected ? _brushSelection : brush, rect);
                    }
                }
                else
                {
                    e.Graphics.FillRectangle(isSelected ? _brushSelection : _brushForeground, rect);
                }

                e.Graphics.DrawString(e.SubItem.Text, Font, isSelected ? _brushHighlight : _brushBlack, rect);
            }
        }

        private void BtnColorizeStatisticCheckedChanged(object sender, EventArgs e)
        {
            lvStatistic.Invalidate(false);
        }
    }
}