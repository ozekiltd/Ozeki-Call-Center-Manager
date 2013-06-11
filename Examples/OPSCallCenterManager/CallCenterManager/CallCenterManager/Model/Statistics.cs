namespace CallCenterManager.Model
{
	public class Statistics
	{
		public ulong NumberOfDroppedSessions { get; set; }
		public ulong NumberOfSessionsTalkDurationLessThenAMinute { get; set; }
		public ulong NumberOfSessionsTalkDurationOverAMinute { get; set; }
	}
}
