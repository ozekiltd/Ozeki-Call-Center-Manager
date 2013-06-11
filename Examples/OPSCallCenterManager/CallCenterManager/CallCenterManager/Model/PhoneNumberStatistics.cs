using System.Collections.Generic;

namespace CallCenterManager.Model
{
	public class PhoneNumberStatistics
	{
		readonly object sync = new object();
		public uint CallsInProgess { get; private set; } // Calls in progress at the moment

		public uint NumberOfCalls { get; private set; } // All calls
		public uint NumberOfDials { get; private set; } // All dials
		public uint NumberOfReceivedCalls { get; private set; } // All received calls

		PhoneNumberStatistics()
		{
			CallsInProgess = 0;
			NumberOfCalls = 0;
		}

		public void CreateCall(bool as_caller)
		{
			lock (sync)
			{
				CallsInProgess++;
				NumberOfCalls++;

				if (as_caller) NumberOfDials++;
				else NumberOfReceivedCalls++;
			}
		}

		public void CompleteCall()
		{
			lock (sync)
			{
				if (CallsInProgess > 0) CallsInProgess--;
			}
		}

		public static Dictionary<string, PhoneNumberStatistics> GetPhoneNumberStatisticsList(string phone_number)
		{
			var result = new Dictionary<string, PhoneNumberStatistics>();
			if (phone_number == null) return result;
			result.Add(phone_number, new PhoneNumberStatistics());
			return result;
		}
	}
}
