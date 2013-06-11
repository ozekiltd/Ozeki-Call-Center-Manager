using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using CallCenterManager.Model;
using CallCenterManager.Presenter;
using CallCenterManager.Util;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using OPSSDK;
using OPSSDKCommon.Model;
using OPSSDKCommon.Model.Session;

namespace CallCenterManager.View
{
	public partial class MainWindow : BaseWindow, IMainWindow
	{
		readonly MainWindowPresenter presenter;

		const string NUMBER_OF_SESSIONS = "Number of calls";
		const string NUMBER_OF_DROPPED_SESSIONS = "Number of dropped calls";
		const string USER_STATES = "User states";

		UserStatisticsContainer user_statistics_container = new UserStatisticsContainer();
		Statistics stats = new Statistics();
		ulong number_of_sessions;
		ulong unit_of_time;

		readonly TreeNode phone_book_root_node;
		readonly TreeNode sessions_root_node;
		readonly TreeNode internal_root_node;
		readonly TreeNode external_root_node;
		readonly TreeNode inbound_root_node;
		readonly TreeNode outbound_root_node;

		readonly DataPoint talk_duration_less_than_a_minute;
		readonly DataPoint talk_duration_over_a_minute;

		readonly object user_stat_sync = new object();
		readonly object user_chart_sync = new object();
		readonly object join_sync = new object();
		readonly object listen_sync = new object();
		readonly object session_sync = new object();
		readonly object phone_book_sync = new object();

		readonly List<ISession> joined = new List<ISession>();
		readonly List<ISession> listened = new List<ISession>();

		public MainWindow()
		{
			InitializeComponent();

			phone_book_root_node = tv_PhoneBook.Nodes["phone_book_root_node"];
			sessions_root_node = tv_Sessions.Nodes["sessions_root_node"];
			internal_root_node = sessions_root_node.Nodes["internal_root_node"];
			external_root_node = sessions_root_node.Nodes["external_root_node"];
			inbound_root_node = sessions_root_node.Nodes["inbound_root_node"];
			outbound_root_node = sessions_root_node.Nodes["outbound_root_node"];
			sessions_root_node.ExpandAll();

			talk_duration_less_than_a_minute = new DataPoint { Name = "LessThenAMinute", LegendText = "Talk < 1 min", YValues = new[] { 0.0 }, Color = Color.FromArgb(100, 180, 100) };
			c_UserChart.Series[USER_STATES].Points.Add(talk_duration_less_than_a_minute);
			talk_duration_over_a_minute = new DataPoint { Name = "OverAMinute", LegendText = "Talk >= 1 min", YValues = new[] { 0.0 }, Color = Color.FromArgb(180, 80, 80) };
			c_UserChart.Series[USER_STATES].Points.Add(talk_duration_over_a_minute);

			SetState(LoginState.LoggedOut);

			presenter = new MainWindowPresenter(this, SimpleIOCContainer.Instance.Resolve<IOPSClient>());

			c_Statistics.Series[NUMBER_OF_SESSIONS].Points.AddXY(0, 0);
			c_Statistics.Series[NUMBER_OF_DROPPED_SESSIONS].Points.AddXY(0, 0);

			presenter.Connect();

			t_Timer.Start();
		}

		public void ShowPhoneBook()
		{
			presenter.GetPhoneBookAsync();
		}

		public void ShowSessions()
		{
			presenter.GetSessionListAsync();
		}

		public void ShowConnectToServerWindow()
        {
            var connect = new ConnectToServerWindow();
            connect.ShowDialog(this);
            if (!connect.Connected) return;
            SetState(LoginState.LoggedIn);
			ShowPhoneBook();
			ShowSessions();
        }

        public void SetState(LoginState state)
        {
            switch (state)
            {
				case LoginState.LoggedOut:
					tsmi_ConnectToServer.Enabled = true;
					tsmi_Disconnect.Enabled = false;
		            lock (phone_book_sync)
		            {
			            phone_book_root_node.Nodes.Clear();
		            }
		            internal_root_node.Nodes.Clear();
					external_root_node.Nodes.Clear();
		            inbound_root_node.Nodes.Clear();
					outbound_root_node.Nodes.Clear();
					break;
				case LoginState.LoggingIn:
				case LoginState.LoggedIn:
					tsmi_ConnectToServer.Enabled = false;
					tsmi_Disconnect.Enabled = true;
					break;
            }
        }

		public void SetSessionJoinState(JoinState state, ISession session)
		{
			switch (state)
			{
				case JoinState.Joined:
					lock (join_sync)
					{
						joined.Add(session);
					}
					break;
				case JoinState.NotJoined:
					lock (join_sync)
					{
						if (joined.Contains(session))
							joined.Remove(session);
					}
					break;
			}
		}

		public void SetSessionListenState(ListenState state, ISession session)
		{
			switch (state)
			{
				case ListenState.Listening:
					lock (listen_sync)
					{
						listened.Add(session);
					}
					break;
				case ListenState.NotListening:
					lock (listen_sync)
					{
						if (listened.Contains(session))
							listened.Remove(session);
					}
					break;
			}
		}

		public void ShowStatistics(Statistics statistics)
		{
			stats = statistics;
			AddPointToChartSeries(c_Statistics, NUMBER_OF_DROPPED_SESSIONS, unit_of_time, stats.NumberOfDroppedSessions);
		}

		public void CreateSession(ISession session)
		{
			number_of_sessions++;
			AddPointToChartSeries(c_Statistics, NUMBER_OF_SESSIONS, unit_of_time, number_of_sessions);

			lock (session_sync)
			{
				var session_node = CreateSessionNode(session);
				AddNodeToTreeNode(GetParentNodeForSession(session), session_node);
			}
		}

		public void ChangeSessionState(ISession session, SessionState state)
		{
			if (session == null) return;

			lock (session_sync)
			{
				var node = GetTreeNode(GetParentNodeForSession(session), session);
				if (node != null)
				{
					SetTreeNodeTextAndIconIndex(node, String.Format(Constants.SESSION_FORMAT_STRING, session.Caller, session.Callee, session.State), GetIconIndex(session.State));
				}
			}
		}

		public void CompleteSession(ISession session)
		{
			number_of_sessions--;
			AddPointToChartSeries(c_Statistics, NUMBER_OF_SESSIONS, unit_of_time, number_of_sessions);

			lock (session_sync)
			{
				var node = GetTreeNode(GetParentNodeForSession(session), session);
				RemoveTreeNode(node.Parent, node);
			}
		}

		public void ShowPhoneBook(IEnumerable<PhoneBookItem> phone_book)
		{
			GetPhoneBookNodes(phone_book);
		}

		public void ShowSessions(IEnumerable<ISession> sessions)
		{
			GetSessionNodes(sessions);
		}

		public void AddSessionToUser(string user)
		{
			lock (phone_book_sync)
			{
				var node = GetUserTreeNode(user);
				if (node == null) return;

				node.ToolTipText = String.IsNullOrEmpty(node.ToolTipText) ? "1" : (Convert.ToInt32(node.ToolTipText) + 1).ToString();
				SetTreeNodeText(node, node.ToolTipText == "1"
									? String.Format(Constants.USER_FORMAT_STRING, node.Tag, node.ToolTipText, Constants.CALL_IN_PROGRESS)
									: String.Format(Constants.USER_FORMAT_STRING, node.Tag, node.ToolTipText, Constants.CALLS_IN_PROGRESS));
			}
		}

		public void RemoveSessionFromUser(string user)
		{
			lock (phone_book_sync)
			{
				var node = GetUserTreeNode(user);
				if (node == null) return;

				if (!String.IsNullOrEmpty(node.ToolTipText))
				{
					node.ToolTipText = (Convert.ToInt32(node.ToolTipText) - 1).ToString();
					if (node.ToolTipText == "0") node.ToolTipText = String.Empty;
				}

				if (String.IsNullOrEmpty(node.ToolTipText)) SetTreeNodeText(node, node.Tag.ToString());
				else
				{
					SetTreeNodeText(node, node.ToolTipText == "1"
										? String.Format(Constants.USER_FORMAT_STRING, node.Tag, node.ToolTipText, Constants.CALL_IN_PROGRESS)
										: String.Format(Constants.USER_FORMAT_STRING, node.Tag, node.ToolTipText, Constants.CALLS_IN_PROGRESS));
				}
			}
		}

		public void ShowUserStatistics(UserStatisticsContainer my_user_statistics_container)
		{
			if (my_user_statistics_container == null) return;

			lock (user_stat_sync)
			{
				user_statistics_container = my_user_statistics_container;

				foreach (var user_statistics in my_user_statistics_container.PhoneBook)
				{
					foreach (var number_statistics in user_statistics.NumberStatistics)
					{
						var phone_number_stats = number_statistics.Value;

						var series_name = String.Format(Constants.SERIES_FORMAT_STRING, user_statistics.PhoneBookItem.Username, number_statistics.Key);

						lock (user_chart_sync)
						{
							var series = GetSeriesByName(c_CallsInProgressStatistics, series_name) ?? new Series(series_name)
								{
									BorderWidth = 3,
									ChartType = SeriesChartType.StepLine
								};
							AddSeriesToChart(c_CallsInProgressStatistics, series);
						}

						AddPointToChartSeries(c_CallsInProgressStatistics, series_name, unit_of_time, phone_number_stats.CallsInProgess);
					}
				}
			}
		}

		Series GetSeriesByName(Chart chart, string name)
		{
			if (!chart.InvokeRequired) return chart.Series.FindByName(name);
			return (Series)chart.EndInvoke(chart.BeginInvoke(new Delegates.SeriesResultChartStringParams(GetSeriesByName), chart, name));
		}

		void AddSeriesToChart(Chart chart, Series series)
		{
			if (!chart.InvokeRequired)
			{
				if (!chart.Series.Contains(series)) chart.Series.Add(series);
			}
			else chart.BeginInvoke(new Delegates.VoidResultChartSeriesParams(AddSeriesToChart), chart, series);
		}

		TreeNode GetParentNodeForSession(ISession session)
		{
			switch (session.CallDirection)
			{
				case CallDirection.Internal:
					return internal_root_node;
				case CallDirection.External:
					return external_root_node;
				case CallDirection.Inbound:
					return inbound_root_node;
				default:
					return outbound_root_node;
			}
		}

		void AddPhoneBookItem(PhoneBookItem phone_book_item)
		{
			lock (phone_book_sync)
			{
				var node = new TreeNode(phone_book_item.Name, Constants.USER_ICON_INDEX, Constants.USER_ICON_INDEX) { Tag = phone_book_item };
				var phone_number_node = new TreeNode(phone_book_item.PhoneNumber, Constants.PHONE_NUMBER_ICON_INDEX, Constants.PHONE_NUMBER_ICON_INDEX)
					{
						Tag = phone_book_item.PhoneNumber
					};
				node.Nodes.Add(phone_number_node);
				node.Expand();
				AddNodeToTreeNode(phone_book_root_node, node);
			}
		}

		static int GetIconIndex(SessionState state)
		{
			switch (state)
			{
				case SessionState.CalleeOnHold:
				case SessionState.OnHold:
				case SessionState.OnHoldInactive:
				case SessionState.CallerOnHold:
					return Constants.HOLD_INDEX;
				case SessionState.CalleeHungUp:
				case SessionState.CallerHungUp:
				case SessionState.Cancelled:
					return Constants.COMPLETED_INDEX;
				case SessionState.InCall:
					return Constants.IN_CALL_INDEX;
				case SessionState.Aborted:
				case SessionState.Busy:
				case SessionState.NotAnswered:
					return Constants.BUSY_INDEX;
				case SessionState.Setup:
				case SessionState.Ringing:
					return Constants.RINGING_INDEX;
				case SessionState.NotFound:
				case SessionState.Error:
				case SessionState.TransferFailed:
					return Constants.ERROR_INDEX;
				case SessionState.TransferRequested:
				case SessionState.TransferSetup:
				case SessionState.Transferring:
				case SessionState.TransferCompleted:
				case SessionState.Redirected:
					return Constants.TRANSFER_INDEX;
				default:
					return 0;
			}
		}

		static TreeNode CreateSessionNode(ISession session)
		{
			var icon_index = GetIconIndex(session.State);
			var node = new TreeNode(String.Format(Constants.SESSION_FORMAT_STRING, session.Caller, session.Callee, session.State), icon_index, icon_index)
			{
				//StateImageIndex = session.Incoming ? INCOMING_CALL_ICON_INDEX : OUTGOING_CALL_ICON_INDEX,
				Tag = session
			};
			return node;
		}

		TreeNode GetUserTreeNode(string username)
		{
			if (!InvokeRequired)
			{
				foreach (TreeNode node in phone_book_root_node.Nodes)
				{
					foreach (var number in node.Nodes.Cast<TreeNode>().Where(number => (string)number.Tag == username))
					{
						return number;
					}
				}
				return null;
			}

			return (TreeNode)EndInvoke(BeginInvoke(new Delegates.TreeNodeResultStringParams(GetUserTreeNode), username));
		}

		TreeNode GetTreeNode(TreeNode parent_node, object tag)
		{
			if (!InvokeRequired)
				return parent_node.Nodes.Cast<TreeNode>().FirstOrDefault(node => node.Tag.Equals(tag));

			return (TreeNode) EndInvoke(BeginInvoke(new Delegates.TreeNodeResultTreeNodeObjectParams(GetTreeNode), parent_node, tag));
		}

		void SetTreeNodeText(TreeNode node, string text)
		{
			if (!InvokeRequired) node.Text = text;
			else BeginInvoke(new Delegates.VoidResultTreeNodeStringParams(SetTreeNodeText), node, text);
		}

		void SetTreeNodeTextAndIconIndex(TreeNode node, string text, int icon_index)
		{
			if (!InvokeRequired)
			{
				node.ImageIndex = icon_index;
				node.SelectedImageIndex = icon_index;
				node.Text = text;
			}
			else BeginInvoke(new Delegates.VoidResultTreeNodeStringIntParams(SetTreeNodeTextAndIconIndex), node, text, icon_index);
		}

		void tsmi_About_Clicked(object sender, EventArgs e)
		{
			var about_box = new AboutBox();
			about_box.ShowDialog(this);
		}

		void tsmi_OpenOnlineDocumentation_Clicked(object sender, EventArgs e)
		{
			try
			{
				#if DEBUG
					Process.Start("http://inside.ozekiphone.com/call-center-manager-call-center-management-632.html");
				#else
					Process.Start("http://www.ozekiphone.com/call-center-manager-call-center-management-632.html");
				#endif
			}
			catch (Exception exception)
			{
				ErrorBox.Show(exception);
			}
		}

		void tsmi_ConnectToServerServer_Clicked(object sender, EventArgs e)
		{
			presenter.Connect();
		}

		void tsmi_Exit_Clicked(object sender, EventArgs e)
		{
			Application.Exit();
		}

		void tsmi_Disconnect_Clicked(object sender, EventArgs e)
		{
			user_statistics_container = new UserStatisticsContainer();
			stats = new Statistics();
			number_of_sessions = 0;
			talk_duration_less_than_a_minute.YValues = new[] { 0.0 };
			talk_duration_over_a_minute.YValues = new[] { 0.0 };
			presenter.Disconnect();
			c_CallsInProgressStatistics.Series.Clear();
		}

		void ListenOrStopListen()
		{
			if ((tv_Sessions.SelectedNode != null) && (tv_Sessions.SelectedNode.Tag != null))
			{
				var session = (ISession)tv_Sessions.SelectedNode.Tag;
				if (session == null) return;

				if (listened.Contains(session))
				{
					presenter.StopListening(session);
					btn_Listen.Text = Constants.LISTEN;
					tv_Sessions.SelectedNode.BackColor = joined.Contains(session) ? Constants.JOINED_COLOR : tv_Sessions.BackColor;
				}
				else
				{
					presenter.Listen(session);
					btn_Listen.Text = Constants.STOP_LISTENING;
					tv_Sessions.SelectedNode.BackColor = Constants.LISTENING_COLOR;
				}
			}
		}

		void btn_Listen_Click(object sender, EventArgs e)
		{
			ListenOrStopListen();
		}

		void tsmi_Listen_Click(object sender, EventArgs e)
		{
			ListenOrStopListen();
		}

		void tsmi_StopListening_Click(object sender, EventArgs e)
		{
			ListenOrStopListen();
		}

		void JoinOrLeave()
		{
			if ((tv_Sessions.SelectedNode != null) && (tv_Sessions.SelectedNode.Tag != null))
			{
				var session = (ISession)tv_Sessions.SelectedNode.Tag;
				if (session == null) return;

				if (joined.Contains(session))
				{
					presenter.Leave(session);
					btn_Join.Text = Constants.JOIN;
					tv_Sessions.SelectedNode.BackColor = listened.Contains(session) ? Constants.LISTENING_COLOR : tv_Sessions.BackColor;
				}
				else
				{
					presenter.Join(session);
					btn_Join.Text = Constants.LEAVE;
					tv_Sessions.SelectedNode.BackColor = Constants.JOINED_COLOR;
				}
			}
		}

		void btn_Join_Click(object sender, EventArgs e)
		{
			JoinOrLeave();
		}

		void tsmi_Join_Click(object sender, EventArgs e)
		{
			JoinOrLeave();
		}

		void tsmi_Leave_Click(object sender, EventArgs e)
		{
			JoinOrLeave();
		}

		void tv_Sessions_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			tv_Sessions.SelectedNode = e.Node;

			var session_selected = (e.Node != null) && (e.Node != sessions_root_node) && (e.Node != internal_root_node) && (e.Node != external_root_node) && (e.Node != inbound_root_node) && (e.Node != outbound_root_node);
			tsmi_Listen.Enabled = session_selected;
			tsmi_StopListening.Enabled = session_selected;
			tsmi_Join.Enabled = session_selected;
			tsmi_Leave.Enabled = session_selected;

			if (!session_selected)
			{
				btn_Listen.Enabled = false;
				btn_Join.Enabled = false;
				return;
			}
			var session = e.Node.Tag as ISession;
			if (session == null) return;

			tsmi_Listen.Enabled &= !listened.Contains(session);
			tsmi_StopListening.Enabled &= !tsmi_Listen.Enabled;

			tsmi_Join.Enabled &= !joined.Contains(session);
			tsmi_Leave.Enabled &= !tsmi_Join.Enabled;

			btn_Listen.Enabled = true;
			btn_Join.Enabled = true;

			btn_Listen.Text = tsmi_Listen.Enabled ? Constants.LISTEN : Constants.STOP_LISTENING;
			btn_Join.Text = tsmi_Join.Enabled ? Constants.JOIN : Constants.LEAVE;
		}

		void tv_PhoneBook_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			tv_PhoneBook.SelectedNode = e.Node;
		}

		void t_Timer_Tick(object sender, EventArgs e)
		{
			unit_of_time++;
			c_Statistics.Series[NUMBER_OF_SESSIONS].Points.AddXY(unit_of_time, number_of_sessions);
			c_Statistics.Series[NUMBER_OF_DROPPED_SESSIONS].Points.AddXY(unit_of_time, stats.NumberOfDroppedSessions);

			talk_duration_less_than_a_minute.YValues = new[] { (double)stats.NumberOfSessionsTalkDurationLessThenAMinute };
			talk_duration_over_a_minute.YValues = new[] { (double)stats.NumberOfSessionsTalkDurationOverAMinute };

			lock (user_stat_sync)
			{
				foreach (var user_statistics in user_statistics_container.PhoneBook)
				{
					foreach (var number_statistics in user_statistics.NumberStatistics)
					{
						var phone_number_stats = number_statistics.Value;
						var series_name = String.Format(Constants.SERIES_FORMAT_STRING, user_statistics.PhoneBookItem.Username, number_statistics.Key);
						AddPointToChartSeries(c_CallsInProgressStatistics, series_name, unit_of_time, phone_number_stats.CallsInProgess);
					}
				}
			}
		}

		void tsmi_tsmi_ExportPhonebook_Click(object sender, EventArgs e)
		{
			if (sfd_SaveFileDialog.ShowDialog() != DialogResult.OK) return;

			var save_file = true;

			if (File.Exists(sfd_SaveFileDialog.FileName))
				save_file = ConfirmBox.Show("File already exists", "Are you sure, you want to overwrite the selected file?") == DialogResult.OK;

			if (save_file) presenter.ExportPhoneBook(sfd_SaveFileDialog.FileName);
		}

		void GetPhoneBookNodes(IEnumerable<PhoneBookItem> phone_book_items)
		{
			lock (phone_book_sync)
			{
				ClearNodes(phone_book_root_node);

				foreach (var item in phone_book_items)
				{
					AddPhoneBookItem(item);
				}
				ExpandAllInTreeView(tv_PhoneBook);
			}
		}

		void GetSessionNodes(IEnumerable<ISession> sessions)
		{
			lock (phone_book_sync)
			{
				number_of_sessions = 0;
				ClearNodes(internal_root_node);
				ClearNodes(external_root_node);
				ClearNodes(inbound_root_node);
				ClearNodes(outbound_root_node);

				foreach (var session in sessions)
				{
					var session_node = CreateSessionNode(session);
					AddNodeToTreeNode(GetParentNodeForSession(session), session_node);
					number_of_sessions++;
				}
				
				ExpandAllInTreeView(tv_Sessions);
			}
		}

		void ExpandAllInTreeView(TreeView tree_view)
		{
			if (!InvokeRequired) tree_view.ExpandAll();
			else BeginInvoke(new Delegates.VoidResultTreeViewParams(ExpandAllInTreeView), tree_view);
		}

		void AddNodeToTreeNode(TreeNode parent_node, TreeNode node)
		{
			if (!InvokeRequired)
			{
				parent_node.Nodes.Add(node);
				if (parent_node.Nodes.Count == 1) parent_node.ExpandAll();
			}
			else BeginInvoke(new Delegates.VoidResultTreeNodeTreeNodeParams(AddNodeToTreeNode), parent_node, node);
		}

		void ClearNodes(TreeNode parent_node)
		{
			if (!InvokeRequired) parent_node.Nodes.Clear();
			else BeginInvoke(new Delegates.VoidResultTreeNodeParams(ClearNodes), parent_node);
		}

		void RemoveTreeNode(TreeNode parent_node, TreeNode node)
		{
			if (!InvokeRequired) parent_node.Nodes.Remove(node);
			else BeginInvoke(new Delegates.VoidResultTreeNodeTreeNodeParams(RemoveTreeNode), parent_node, node);
		}

		void AddPointToChartSeries(Chart chart, string series_name, ulong x, ulong y)
		{
			if (!chart.InvokeRequired) chart.Series[series_name].Points.AddXY(x, y);
			else chart.BeginInvoke(new Delegates.VoidResultChartStringUlongUlongParams(AddPointToChartSeries), chart, series_name, x, y);
		}
	}
}
