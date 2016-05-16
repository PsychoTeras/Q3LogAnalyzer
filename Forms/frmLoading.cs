using System;
using System.Threading;
using System.Windows.Forms;

namespace Q3LogAnalyzer.Forms
{
    public partial class frmLoading : Form
    {
        private const int WS_EX_NOACTIVATE = 0x08000000;
        private const int WS_EX_TOPMOST = 0x00000008;

        private Thread _formThread;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams baseParams = base.CreateParams;
                baseParams.ExStyle |= WS_EX_NOACTIVATE | WS_EX_TOPMOST;
                return baseParams;
            }
        }

        public frmLoading()
        {
            InitializeComponent();
            SetApplicationInfo();
            ShowInSeparateThread();
        }

        private void SetApplicationInfo()
        {
            lblVersion.Text = string.Format("v {0}", Application.ProductVersion);
            lblVersion.Left = Width - lblVersion.Width - 9;
            lblHeader.Text = Application.ProductName;
        }

        private void ShowInSeparateThread()
        {
            if (IsHandleCreated)
            {
                throw new InvalidOperationException("Form is already running.");
            }
            _formThread = new Thread(form => Application.Run(form as Form));
            _formThread.SetApartmentState(ApartmentState.STA);
            _formThread.IsBackground = true;
            _formThread.Start(this);
        }

        protected new void Show() {}

        protected new void Hide() {}

        public new void Close() {}

        private void FrmLoadingFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                BeginInvoke(new Action(() =>
                    {
                        if (disposing && (components != null))
                        {
                            components.Dispose();
                        }
                        base.Dispose(disposing);
                    }));
            }
            catch {}
        }
    }
}
