using System.Windows.Forms;

namespace Q3LogAnalyzer.Controls
{
    public class ListViewEx : ListView
    {
        public ListViewEx()
        {
            DoubleBuffered = true;
        }

        public event ScrollEventHandler Scroll;

        protected virtual void OnScroll(ScrollEventArgs e)
        {
            Scroll?.Invoke(this, e);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x115)
            {
                OnScroll(new ScrollEventArgs((ScrollEventType) (m.WParam.ToInt32() & 0xffff), 0));
            }
        }
    }
}
