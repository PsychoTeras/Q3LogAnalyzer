using System.Windows.Forms;

namespace Q3LogAnalyzer.Controls
{
    public class ControlButton : Button
    {
        protected override bool ShowFocusCues
        {
            get { return false; }
        }

        public ControlButton()
        {
            SetStyle(ControlStyles.Selectable, false);
        }
    }
}
