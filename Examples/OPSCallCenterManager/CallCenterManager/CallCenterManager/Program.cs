using System;
using System.Windows.Forms;
using CallCenterManager.Model;
using CallCenterManager.Util;
using CallCenterManager.View;

namespace CallCenterManager
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			SimpleIOCContainer.Instance.AddDependency<IOPSClient>(() => new RealClient());
			//SimpleIOCContainer.Instance.AddDependency<IOPSClient>(() => new MockClient());
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainWindow());
		}
	}
}
