using OPSSDK;
using System.Collections.Generic;
using OPSSDKCommon.Model;

namespace CallCenterManager.Model
{
	public class UserStatistics
	{
		public PhoneBookItem PhoneBookItem { get; private set; }
		public Dictionary<string, PhoneNumberStatistics> NumberStatistics { get; private set; }

		public UserStatistics(PhoneBookItem phone_book_item)
		{
			PhoneBookItem = phone_book_item;
			NumberStatistics = PhoneNumberStatistics.GetPhoneNumberStatisticsList(phone_book_item.PhoneNumber);
		}
	}
}
