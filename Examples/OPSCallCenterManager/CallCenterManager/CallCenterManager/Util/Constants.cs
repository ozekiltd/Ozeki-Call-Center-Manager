
using System.Drawing;

namespace CallCenterManager.Util
{
	static class Constants
	{
		public const int USER_ICON_INDEX = 1;
		public const int PHONE_NUMBER_ICON_INDEX = 2;

		public const int BUSY_INDEX = 1;
		public const int COMPLETED_INDEX = 2;
		public const int ERROR_INDEX = 3;
		public const int HOLD_INDEX = 4;
		public const int IN_CALL_INDEX = 5;
		public const int RINGING_INDEX = 6;
		public const int TRANSFER_INDEX = 7;

		public const int INCOMING_INDEX = 8;
		public const int OUTGOING_INDEX = 9;
		public const int INBOUND_INDEX = 10;
		public const int OUTBOUND_INDEX = 11;

		public const int INCOMING_CALL_ICON_INDEX = 0;
		public const int OUTGOING_CALL_ICON_INDEX = 1;

		public const string LISTEN = "Listen";
		public const string STOP_LISTENING = "Stop listening";

		public const string JOIN = "Join";
		public const string LEAVE = "Leave";

		public const string USER_FORMAT_STRING = "{0} - {1} {2}";
		public const string SESSION_FORMAT_STRING = "{0} -> {1} ({2})";
		public const string SERIES_FORMAT_STRING = "{0} ({1}) number of calls in progress";

		public const string CALL_IN_PROGRESS = "call in progress";
		public const string CALLS_IN_PROGRESS = "calls in progress";

		public static readonly Color LISTENING_COLOR = Color.Tomato;
		public static readonly Color JOINED_COLOR = Color.LightSkyBlue;
	}
}
