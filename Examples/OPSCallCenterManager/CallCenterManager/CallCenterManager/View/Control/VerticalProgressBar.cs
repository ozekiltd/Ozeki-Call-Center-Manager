using System.Windows.Forms;

namespace CallCenterManager.View.Control
{
	public class VerticalProgressBar : ProgressBar
	{
		protected override CreateParams CreateParams
		{
			get
			{
				var cp = base.CreateParams;
				cp.Style |= 0x04;
				return cp;
			}
		}
	}
}
