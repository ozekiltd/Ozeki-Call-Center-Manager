using System;
using OPSSDK;
using OPSSDKCommon.Model.Call;
using OPSSDKCommon.Model.Session;
using Ozeki.Media.MediaHandlers;
using Ozeki.VoIP;

namespace CallCenterManager.Model
{
	class MockSession : ISession
    {
		public void AttendedTransfer(CallParty transferor, ISession target)
		{
			throw new NotImplementedException();
		}

		public void BlindTransfer(CallParty transferor, string target)
		{
			throw new NotImplementedException();
		}

		public void Close()
		{
			throw new NotImplementedException();
		}

		public void Hold()
		{
			throw new NotImplementedException();
		}

		public void Unhold()
		{
			throw new NotImplementedException();
		}

		public void SendDTMF(int key, CallParty party)
		{
			throw new NotImplementedException();
		}

		public void Forward(string target)
		{
			throw new NotImplementedException();
		}

		public bool ConnectAudioReceiver(CallParty side, AudioHandler receiver)
		{
			throw new NotImplementedException();
		}

		public bool DisconnectAudioReceiver(CallParty side, AudioHandler receiver)
		{
			throw new NotImplementedException();
		}

		public bool ConnectAudioSender(CallParty side, AudioHandler sender)
		{
			throw new NotImplementedException();
		}

		public bool DisconnectAudioSender(CallParty side, AudioHandler sender)
		{
			throw new NotImplementedException();
		}

		public string SessionID { get; private set; }
		public SessionState State { get; private set; }
		public string Callee { get; private set; }
		public string Caller { get; private set; }
		public CallDirection CallDirection { get; private set; }
		public SessionState SessionState { get; private set; }
		public DateTime StartTime { get; private set; }
		public TimeSpan RingDuration { get; private set; }
		public TimeSpan TalkDuration { get; private set; }
		public TimeSpan StateDuration { get; private set; }
		public event EventHandler<VoIPEventArgs<SessionState>> SessionStateChanged;
    }
}
