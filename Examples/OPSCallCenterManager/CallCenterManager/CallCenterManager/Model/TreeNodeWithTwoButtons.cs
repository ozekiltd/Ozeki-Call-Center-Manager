using System;
using System.Windows.Forms;

namespace CallCenterManager.Model
{
	class TreeNodeWithTwoButtons : TreeNode
	{
        public Button Join { get; private set; }
        public Button Listen { get; private set; }

		public event EventHandler JoinClicked;
		public event EventHandler ListenClicked;

		public TreeNodeWithTwoButtons(string text) : base(text)
		{
			Join = new Button();
            Join.Click += JoinOnClick;
			Listen = new Button();
			Listen.Click += ListenOnClick;
        }

		~TreeNodeWithTwoButtons()
		{
			Join.Click -= JoinOnClick;
			Listen.Click -= ListenOnClick;
		}

		void JoinOnClick(object sender, EventArgs event_args)
		{
			var handler = JoinClicked;
			if (handler != null) handler(this, event_args);
		}

		void ListenOnClick(object sender, EventArgs event_args)
		{
			var handler = ListenClicked;
			if (handler != null) handler(this, event_args);
		}
	}
}
