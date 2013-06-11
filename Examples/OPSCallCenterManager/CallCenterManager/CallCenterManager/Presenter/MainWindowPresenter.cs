using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using CallCenterManager.Model;
using CallCenterManager.View;
using System.IO;
using OPSSDK;
using OPSSDKCommon.Model;
using OPSSDKCommon.Model.Call;
using OPSSDKCommon.Model.Session;
using Ozeki.Media.MediaHandlers;
using Ozeki.VoIP;

namespace CallCenterManager.Presenter
{
	enum MyCallParty
	{
		Callee,
		Caller,
		Both
	}

	class MainWindowPresenter
	{
	    readonly IMainWindow view;
        readonly IOPSClient client;
		readonly Speaker speaker;
		readonly Microphone microphone;
		UserStatisticsContainer user_statistics_container;

		public MainWindowPresenter(IMainWindow my_view, IOPSClient my_client)
		{
			view = my_view;
			client = my_client;

			client.ErrorOccurred += ClientOnErrorOccured;
			
			microphone = Microphone.GetDefaultDevice();
			if (microphone != null) microphone.Start();

			speaker = Speaker.GetDefaultDevice();
			if (speaker != null) speaker.Start();

			client.SessionCreated += ClientOnSessionCreated;
			client.SessionCompleted += ClientOnSessionCompleted;
			client.PhoneBookChanged += ClientOnPhoneBookChanged;
			client.StatisticsChanged += ClientOnStatisticsChanged;
		}

		~MainWindowPresenter()
		{
			client.SessionCreated -= ClientOnSessionCreated;
		}

		public void Connect()
        {
            if (!client.IsLoggedIn) view.ShowConnectToServerWindow();
            else view.ShowError("Connection failed", "You are already connected");
        }

		public void Disconnect()
		{
		    if (!client.IsLoggedIn)
		    {
                view.ShowError("Disconnect failed", "You are not connected");
		        return;
		    }

		    view.SetState(LoginState.LoggedOut);

		    client.Logout();
		}

		public IList<PhoneBookItem> GetPhoneBook()
		{
			if (client.IsLoggedIn)
			{
				var old = user_statistics_container;
				user_statistics_container = new UserStatisticsContainer(client.PhoneBook);
				user_statistics_container.CopyOldStatistics(old);
				return client.PhoneBook;
			}
			user_statistics_container = new UserStatisticsContainer();
			return new List<PhoneBookItem>();
		}

		public void GetPhoneBookAsync()
		{
			client.GetPhoneBookAsync(Completed);
		}

		public IList<ISession> GetSessionList()
		{
			return client.IsLoggedIn ? client.SessionList : new List<ISession>();
		}

		public void GetSessionListAsync()
		{
			client.GetSessionListAsync(Completed);
		}

		public void Listen(ISession session)
		{
			ConnectAudioReceiverToCallParty(session, CallParty.All);
			view.SetSessionListenState(ListenState.Listening, session);
		}

		public void StopListening(ISession session)
		{
			DisconnectAudioReceiverToCallParty(session, CallParty.All);
			view.SetSessionListenState(ListenState.NotListening, session);
		}

		public void Join(ISession session)
		{
			ConnectAudioSenderToCallParty(session, CallParty.All);
			ConnectAudioReceiverToCallParty(session, CallParty.All);
			view.SetSessionJoinState(JoinState.Joined, session);
		}

		public void Leave(ISession session)
		{
			DisconnectAudioSenderToCallParty(session, CallParty.All);
			DisconnectAudioReceiverToCallParty(session, CallParty.All);
			view.SetSessionJoinState(JoinState.NotJoined, session);
		}

		public void ExportPhoneBook(string path)
		{
			if (client.IsLoggedIn)
			{
				using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
				{
					using (var sw = new StreamWriter(fs))
					{
						foreach (var user_info in client.PhoneBook)
						{
							var sb = new StringBuilder();
							sb.AppendFormat("{0};{1}", user_info.Name, user_info.PhoneNumber);
							sw.WriteLine(sb.ToString());
						}
						sw.Close();
					}
					fs.Close();
				}
			}
			else view.ShowError("Export failed", "You have to login first");
		}

		private void ClientOnStatisticsChanged(object sender, VoIPEventArgs<Statistics> event_args)
		{
			view.ShowStatistics(event_args.Item);
		}

		void ClientOnPhoneBookChanged(object sender, EventArgs event_args)
		{
			GetPhoneBookAsync();
		}

		void ClientOnSessionCreated(object sender, VoIPEventArgs<ISession> event_args)
		{
			var session = event_args.Item;
			if (session == null) return;

			session.SessionStateChanged += ItemOnSessionStateChanged;
			view.CreateSession(session);

			view.AddSessionToUser(session.Caller);
			view.AddSessionToUser(session.Callee);

			var number_stats = user_statistics_container.GetPhoneNumberStatistics(session.Caller);
		    if (number_stats != null) number_stats.CreateCall(true);

		    number_stats = user_statistics_container.GetPhoneNumberStatistics(session.Callee);
            if (number_stats != null) number_stats.CreateCall(false);    

			view.ShowUserStatistics(user_statistics_container);
		}

		void ClientOnSessionCompleted(object sender, VoIPEventArgs<ISession> event_args)
		{
			var session = event_args.Item;
			if (session == null) return;

			session.SessionStateChanged -= ItemOnSessionStateChanged;
			view.CompleteSession(session);

			view.RemoveSessionFromUser(session.Caller);
			view.RemoveSessionFromUser(session.Callee);

			var number_stats = user_statistics_container.GetPhoneNumberStatistics(session.Caller);
			if (number_stats != null) number_stats.CompleteCall();

			number_stats = user_statistics_container.GetPhoneNumberStatistics(session.Callee);
			if (number_stats != null) number_stats.CompleteCall();

			view.ShowUserStatistics(user_statistics_container);
		}

		void ItemOnSessionStateChanged(object sender, VoIPEventArgs<SessionState> event_args)
		{
			view.ChangeSessionState(sender as ISession, event_args.Item);
		}

		void ConnectAudioReceiverToCallParty(ISession session, CallParty call_party)
		{
			ConnectOrDisconnectAudioReceiverToCallParty(session, call_party, speaker, true);
		}

		void ConnectAudioSenderToCallParty(ISession session, CallParty call_party)
		{
			ConnectOrDisconnectAudioSenderToCallParty(session, call_party, microphone, true);
		}

		void DisconnectAudioReceiverToCallParty(ISession session, CallParty call_party)
		{
			ConnectOrDisconnectAudioReceiverToCallParty(session, call_party, speaker, false);
		}

		void DisconnectAudioSenderToCallParty(ISession session, CallParty call_party)
		{
			ConnectOrDisconnectAudioSenderToCallParty(session, call_party, microphone, false);
		}

		static void ConnectOrDisconnectAudioReceiverToCallParty(ISession session, CallParty call_party, AudioHandler receiver, bool connect)
		{
			if (receiver == null) return;

			if (connect) session.ConnectAudioReceiver(call_party, receiver);
			else session.DisconnectAudioReceiver(call_party, receiver);
		}

		static void ConnectOrDisconnectAudioSenderToCallParty(ISession session, CallParty call_party, AudioHandler sender, bool connect)
		{
			if (sender == null) return;

			if (connect) session.ConnectAudioSender(call_party, sender);
			else session.DisconnectAudioSender(call_party, sender);
		}

		static void ClientOnErrorOccured(object sender, VoIPEventArgs<ErrorInfo> event_args)
		{
			Debug.Fail(event_args.Item.Message);
		}

		void Completed(List<PhoneBookItem> phone_book)
		{
			var old = user_statistics_container;
			user_statistics_container = new UserStatisticsContainer(phone_book);
			user_statistics_container.CopyOldStatistics(old);
			view.ShowPhoneBook(phone_book);
		}

		void Completed(List<ISession> sessions)
		{
			foreach (var session in sessions)
			{
				session.SessionStateChanged += ItemOnSessionStateChanged;

				var number_stats = user_statistics_container.GetPhoneNumberStatistics(session.Caller);
				if (number_stats != null) number_stats.CreateCall(true);

				number_stats = user_statistics_container.GetPhoneNumberStatistics(session.Callee);
				if (number_stats != null) number_stats.CreateCall(false);

				view.AddSessionToUser(session.Caller);
				view.AddSessionToUser(session.Callee);
			}

			view.ShowUserStatistics(user_statistics_container);
			view.ShowSessions(sessions);
		}
	}
}
