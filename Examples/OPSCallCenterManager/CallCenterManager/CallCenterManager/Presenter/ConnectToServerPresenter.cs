using CallCenterManager.View;
using System;
using OPSSDK;
using Ozeki.VoIP;
using CallCenterManager.Model;

namespace CallCenterManager.Presenter
{
	public class ConnectToServerPresenter
	{
        readonly IConnectToServer view;
        readonly IOPSClient client;
	    WaitWindow window;
        readonly object sync;

        public ConnectToServerPresenter(IConnectToServer my_view, IOPSClient my_client)
		{
			sync = new object();
            view = my_view;
            client = my_client;
			client.ErrorOccurred += ClientOnErrorOccurred;
			client.LoginCompleted += ClientOnLoginCompleted;
		}

        public void Connect()
        {
			if (CanConnect) Login();
			else view.ShowError("Connection failed", "Please fill out all fields");
		}

		public void CloseWindow()
		{
			if (window != null) view.CloseWaitWindow(window);
		}

        void Login()
        {
			lock (sync)
			{
				view.SetState(LoginState.LoggingIn);
				window = view.ShowWaitWindow();
				client.Login(view.Server, view.Username, view.Password);
			}
		}

		void ClientOnErrorOccurred(object sender, VoIPEventArgs<ErrorInfo> vo_ip_event_args)
		{
			view.ShowError(vo_ip_event_args.Item.Type.ToString(), vo_ip_event_args.Item.Message);
		}

		void ClientOnLoginCompleted(object sender, VoIPEventArgs<LoginResult> e)
	    {
            view.CloseWaitWindow(window);
            view.SetState((e.Item == LoginResult.Success) ? LoginState.LoggingIn : LoginState.LoggedOut);

            if (e.Item != LoginResult.Success)
                view.ShowError("Connection failed", String.Format("Failed to connect to server: {0}", e.Item));
            else
            {
				client.LoginCompleted -= ClientOnLoginCompleted;
				client.ErrorOccurred -= ClientOnErrorOccurred;
                view.Connected = true;
                view.Close();
            }
	    }
        
	    bool CanConnect
        {
            get
            {
                return !String.IsNullOrEmpty(view.Server) && !String.IsNullOrEmpty(view.Username) && !String.IsNullOrEmpty(view.Password) && !client.IsLoggedIn;
            }
        }
	}
}
