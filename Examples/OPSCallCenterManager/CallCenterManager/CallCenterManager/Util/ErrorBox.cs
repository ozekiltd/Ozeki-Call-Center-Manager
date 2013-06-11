using System;
using System.Windows.Forms;

namespace CallCenterManager.Util
{
	static class ErrorBox
	{
		public static void Show(Exception exception)
		{
			MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
		}

		public static void Show(string title, string message)
		{
			MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
		}
	}
}
