using System;
using System.Collections.Generic;
using OPSSDK;
using OPSSDKCommon.Model;
using Ozeki.VoIP;

namespace CallCenterManager.Model
{
	public interface IOPSClient
	{
		bool IsLoggedIn { get; }
		void Login(string server_address, string username, string password);
		void Logout();

		event EventHandler<VoIPEventArgs<LoginResult>> LoginCompleted;
		event EventHandler<VoIPEventArgs<ISession>> SessionCompleted;
		event EventHandler<VoIPEventArgs<ISession>> SessionCreated;
		event EventHandler<VoIPEventArgs<ErrorInfo>> ErrorOccurred;
		event EventHandler PhoneBookChanged;
		event EventHandler<VoIPEventArgs<Statistics>> StatisticsChanged;

		List<ISession> SessionList { get; }

		void GetSessionListAsync(Action<List<ISession>> completed);
		List<PhoneBookItem> PhoneBook { get; }

		void GetPhoneBookAsync(Action<List<PhoneBookItem>> completed);
	}
}
