using System.Windows.Forms;

namespace CallCenterManager.Util
{
	static class ConfirmBox
	{
		public static DialogResult Show(string title, string message)
		{
			return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
		}
	}
}
