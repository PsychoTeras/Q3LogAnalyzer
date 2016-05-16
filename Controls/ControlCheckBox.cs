using System.Windows.Forms;

namespace Q3LogAnalyzer.Controls
{
    public class ControlCheckBox : CheckBox
    {
        protected override bool ShowFocusCues
        {
            get { return false; }
        }

        public ControlCheckBox()
        {
            SetStyle(ControlStyles.Selectable, false);
        }
    }
}
