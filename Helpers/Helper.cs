using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Q3LogAnalyzer.Helpers
{
    public static class Helper
    {

#region Constants

        private const int WM_SETREDRAW = 0x000B;
        private const int LVM_FIRST = 0x1000;
        private const int LVM_SETGROUPINFO = LVM_FIRST + 147;
        private const int LVGF_STATE = 0x004;

#endregion

#region P/Invoke

        [StructLayout(LayoutKind.Sequential)]
        struct LVGROUP
        {
            public int cbSize;
            public int mask;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pszHeader;
            public int cchHeader;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pszFooter;
            public int cchFooter;
            public int iGroupId;
            public int stateMask;
            public int state;
            public int uAlign;
        }

        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr window, int message, int wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int cmd);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int BringWindowToTop(IntPtr hwndParent);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x,
                                               int y, int cx, int cy, uint uFlags);


#endregion

#region Fields

        private static Regex _regexRemoveNameColorizing = new Regex(@"\^\d", RegexOptions.Compiled);
        private static PropertyInfo _propGroupId;

#endregion

#region Ctor

        static Helper()
        {
            Type tGroup = typeof (ListViewGroup);
            _propGroupId = tGroup.GetProperty("ID", BindingFlags.NonPublic |
                BindingFlags.Instance);
        }

#endregion

#region Methods

        public static void SetGroupCollapse(ListView listView, GroupState state)
        {
            SetGroupCollapse(listView, state, listView.Groups.Count);
        }

        private static int? GetListViewGroupId(ListViewGroup group)
        {
            return _propGroupId.GetValue(group, null) as int?;
        }

        public static void SetGroupCollapse(ListView listView, GroupState state, 
            int groupsCount)
        {
            LVGROUP group = new LVGROUP();
            group.cbSize = Marshal.SizeOf(group);
            group.state = (int)(state | GroupState.Collapsible);
            group.mask = LVGF_STATE;
            IntPtr ptr = Marshal.AllocHGlobal(group.cbSize);

            for (int i = 0; i < groupsCount; i++)
            {
                if (i == listView.Groups.Count) break;

                ListViewGroup lvGroup = listView.Groups[i];
                int? id = GetListViewGroupId(lvGroup);
                group.iGroupId = id.HasValue ? id.Value : listView.Groups.IndexOf(lvGroup);

                Marshal.StructureToPtr(group, ptr, false);
                SendMessage(listView.Handle, LVM_SETGROUPINFO, group.iGroupId, ptr);
            }

            Marshal.FreeHGlobal(ptr);
        }
        
        public static string RemoveNameColorizing(string name)
        {
            return _regexRemoveNameColorizing.Replace(name, "");
        }

        public static void LockUpdate(Control parentCtrl)
        {
            SendMessage(parentCtrl.Handle, WM_SETREDRAW, 0, (IntPtr)0);
        }

        public static void UnlockUpdate(Control parentCtrl)
        {
            SendMessage(parentCtrl.Handle, WM_SETREDRAW, 1, (IntPtr)0);
            parentCtrl.Invalidate(true);
        }

        public static void BringWindowToFront(IntPtr windowHandle)
        {
            ShowWindow(windowHandle, 1);
            BringWindowToTop(windowHandle);
            SetForegroundWindow(windowHandle);
            SetWindowPos(windowHandle, 0, 0, 0, 0, 0, 0x0001 | 0x0002);
        }

#endregion

    }

    [Flags]
    public enum GroupState
    {
        Collapsible = 8,
        Collapsed = 1,
        Expanded = 0
    }
}
