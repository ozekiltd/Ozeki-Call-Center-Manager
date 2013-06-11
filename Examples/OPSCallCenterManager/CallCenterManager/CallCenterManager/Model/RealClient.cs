using System;
using System.Collections.Generic;
using System.Diagnostics;
using OPSSDK;
using OPSSDKCommon.Model;
using OPSSDKCommon.Model.Session;
using Ozeki.VoIP;

namespace CallCenterManager.Model
{
	class RealClient : IOPSClient
	{
		public event EventHandler<VoIPEventArgs<LoginResult>> LoginCompleted;
		public event EventHandler<VoIPEventArgs<ISession>> SessionCompleted;
		public event EventHandler<VoIPEventArgs<ISession>> SessionCreated;
		public event EventHandler<VoIPEventArgs<ErrorInfo>> ErrorOccurred;
		public event EventHandler PhoneBookChanged;
		public event EventHandler<VoIPEventArgs<Statistics>> StatisticsChanged;

		Statistics stats;
		public bool IsLoggedIn { get; private set; }

		OpsClient ops_client;
		readonly object sync = new object();

		public void Login(string server_address, string username, string password)
		{
			stats = new Statistics();

			ops_client = new OpsClient();
			ops_client.ErrorOccurred += OPSClientOnErrorOccurred;

			ops_client.LoginAsync(server_address, username, password, Completed);
		}

		private void OPSClientOnPhoneBookItemsChanged(object sender, EventArgs event_args)
		{
			var handler = PhoneBookChanged;
			if (handler != null) handler(this, new EventArgs());
		}

		public void Logout()
		{
			IsLoggedIn = false;
			ops_client.ErrorOccurred -= OPSClientOnErrorOccurred;
			ops_client.SessionCreated -= OPSClientOnSessionCreated;
			ops_client.SessionCompleted -= OPSClientOnSessionCompleted;
			ops_client.PhoneBookItemsChanged -= OPSClientOnPhoneBookItemsChanged;
		}

		public List<ISession> SessionList
		{
			get { return ops_client.GetActiveSessions(); }
		}

		public void GetSessionListAsync(Action<List<ISession>> completed)
		{
			ops_client.GetActiveSessionsAsync(completed);
		}

		public List<PhoneBookItem> PhoneBook
		{
			get { return ops_client.GetPhoneBook(); }
		}

		public void GetPhoneBookAsync(Action<List<PhoneBookItem>> completed)
		{
			ops_client.GetPhoneBookAsync(completed);
		}

		void Completed(bool success)
		{
			IsLoggedIn = success;

			if (success)
			{
				ops_client.SessionCreated += OPSClientOnSessionCreated;
				ops_client.SessionCompleted += OPSClientOnSessionCompleted;
				ops_client.PhoneBookItemsChanged += OPSClientOnPhoneBookItemsChanged;
			}

			var handler = LoginCompleted;
			if (handler != null) 
				handler(this, IsLoggedIn ? new VoIPEventArgs<LoginResult>(LoginResult.Success) : new VoIPEventArgs<LoginResult>(LoginResult.WrongUsernameOrPassword));
		}

		void OPSClientOnSessionCompleted(object sender, VoIPEventArgs<ISession> vo_ip_event_args)
		{
			Debug.WriteLine("{0} - Call completed", vo_ip_event_args.Item.SessionID);

			lock (sync)
			{
				if (vo_ip_event_args.Item.TalkDuration.TotalSeconds < 60) stats.NumberOfSessionsTalkDurationLessThenAMinute++;
				else stats.NumberOfSessionsTalkDurationOverAMinute++;
			}

			var stats_handler = StatisticsChanged;
			if (stats_handler != null) stats_handler(this, new VoIPEventArgs<Statistics>(stats));

			var handler = SessionCompleted;
			if (handler != null) handler(this, vo_ip_event_args);
		}

		void OPSClientOnSessionCreated(object sender, VoIPEventArgs<ISession> e)
		{
			Debug.WriteLine("{0} - Call created", e.Item.SessionID);

			e.Item.SessionStateChanged += ((o, args) =>
				{
					if (args.Item == SessionState.NotFound)
					{
						lock (sync)
						{
							stats.NumberOfDroppedSessions++;
						}
						var stats_handler = StatisticsChanged;
						if (stats_handler != null) stats_handler(this, new VoIPEventArgs<Statistics>(stats));
					}
					Debug.WriteLine("{0} - Call state changed: {1}", e.Item.SessionID, e.Item.State);
				});
			
			var handler = SessionCreated;
			if (handler != null) handler(this, e);
		}

		static LoginResult ErrorTypeToLoginResult(ErrorType error_type)
		{
			switch (error_type)
			{
				case ErrorType.ConnectionFailure:
					return LoginResult.ConnectionFailure;
				case ErrorType.VersionMismatch:
					return LoginResult.VersionMismatch;
				default:
					return LoginResult.UnkownError;
			}
		}

		void OPSClientOnErrorOccurred(object sender, ErrorInfo error_info)
		{
			Debug.WriteLine("{0}", error_info.Message);

			if ((error_info.Type == ErrorType.ConnectionFailure) || (error_info.Type == ErrorType.VersionMismatch))
			{
				var handler = LoginCompleted;
				if (handler != null) handler(this, new VoIPEventArgs<LoginResult>(ErrorTypeToLoginResult(error_info.Type)));
			}

			var error_handler = ErrorOccurred;
			if (error_handler != null) error_handler(this, new VoIPEventArgs<ErrorInfo>(error_info));
		}
	}
}
