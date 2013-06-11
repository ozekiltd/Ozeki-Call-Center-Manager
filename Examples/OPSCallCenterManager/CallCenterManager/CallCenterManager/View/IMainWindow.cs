
using System.Collections.Generic;
using CallCenterManager.Model;
using OPSSDK;
using OPSSDKCommon.Model;
using OPSSDKCommon.Model.Session;

namespace CallCenterManager.View
{
	public enum LoginState
	{
		LoggedOut,
		LoggingIn,
		LoggedIn
	}

	public enum JoinState
	{
		Joined,
		NotJoined
	}

	public enum ListenState
	{
		Listening,
		NotListening
	}

	interface IMainWindow : IBaseView
	{
		void ShowPhoneBook();
	    void ShowSessions();

	    void ShowConnectToServerWindow();
		void SetState(LoginState state);

		void SetSessionJoinState(JoinState state, ISession session);
		void SetSessionListenState(ListenState state, ISession session);

		void ShowStatistics(Statistics stats);

		void CreateSession(ISession session);
		void ChangeSessionState(ISession session, SessionState state);
		void CompleteSession(ISession session);

		void ShowPhoneBook(IEnumerable<PhoneBookItem> phone_book);
		void ShowSessions(IEnumerable<ISession> sessions);

		void AddSessionToUser(string user);
		void RemoveSessionFromUser(string user);

		void ShowUserStatistics(UserStatisticsContainer user_statistics_container);
	}
}
