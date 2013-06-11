using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CallCenterManager.View.Control
{
	public class FlickerFreeTreeView : TreeView
	{
		const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
		//const int TVM_GETEXTENDEDSTYLE = 0x1100 + 45;
		const int TVS_EX_DOUBLEBUFFER = 0x0004;

		[DllImport("User32.dll")]
		static extern IntPtr SendMessage(IntPtr h_wnd, int msg, IntPtr wp, IntPtr lp);

		protected override void OnHandleCreated(EventArgs e)
		{
			SendMessage(Handle, TVM_SETEXTENDEDSTYLE, (IntPtr)TVS_EX_DOUBLEBUFFER, (IntPtr)TVS_EX_DOUBLEBUFFER);
			base.OnHandleCreated(e);
		}
	}
}
