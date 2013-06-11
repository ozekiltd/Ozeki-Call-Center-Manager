using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OPSSDK;
using OPSSDKCommon.Model;
using Ozeki.VoIP;

namespace CallCenterManager.Model
{
	class MockClient : IOPSClient
    {
		public string PhoneSystemVersion { get; private set; }
		public bool IsLoggedIn { get; private set; }
		public void Login(string server_address, string username, string password)
		{
			throw new NotImplementedException();
		}

		public void Logout()
		{
			throw new NotImplementedException();
		}

		public event EventHandler<VoIPEventArgs<LoginResult>> LoginCompleted;
		public event EventHandler<VoIPEventArgs<ISession>> SessionCompleted;
		public event EventHandler<VoIPEventArgs<ISession>> SessionCreated;
		public event EventHandler<VoIPEventArgs<ErrorInfo>> ErrorOccurred;
		public event EventHandler PhoneBookChanged;
		public event EventHandler<VoIPEventArgs<Statistics>> StatisticsChanged;

		public string IpAddress { get; private set; }

		public MockClient(string ip_address)
		{
			IpAddress = ip_address;
		}

		public List<ISession> SessionList
		{
			get { return new List<ISession>(); }
		}

		public void GetSessionListAsync(Action<List<ISession>> completed)
		{
			completed(SessionList);
		}

		public List<PhoneBookItem> PhoneBook
		{
			get
			{
				var result = new List<PhoneBookItem>
					{
						new PhoneBookItem("Adam", "Adam Cru", "123456", new List<string> {"1000"}, "adam.cruz@company.com", null),
						new PhoneBookItem("Sue", "Sue Goldie", "234567", new List<string> {"1001", "1002"}, "sue.goldie@company.com", null)
					};
				return result;
			}
		}

		public void GetPhoneBookAsync(Action<List<PhoneBookItem>> completed)
		{
			Task.Factory.StartNew(() =>
			{
				Thread.Sleep(1000);
				completed(PhoneBook);
			});
		}

		public bool Login(string username, string password)
		{
			return ((username == "user") && (password == "pass"));
		}

		public void LoginAsync(string username, string password, Action<bool> completed)
		{
			Task.Factory.StartNew(() =>
			{
				Thread.Sleep(1000);
				completed(Login(username, password));
			});
		}
    }
}
