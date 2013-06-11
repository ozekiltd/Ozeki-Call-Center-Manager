using System.Collections.Generic;
using System.Linq;
using OPSSDKCommon.Model;

namespace CallCenterManager.Model
{
	public class UserStatisticsContainer
	{
		readonly object sync;
		public readonly List<UserStatistics> PhoneBook;

		public UserStatisticsContainer() : this(new PhoneBookItem[]{}) { }

		public UserStatisticsContainer(IEnumerable<PhoneBookItem> my_phone_book)
		{
			sync = new object();

			if (my_phone_book == null) PhoneBook = new List<UserStatistics>();
			else PhoneBook = my_phone_book.Select(phone_book_item => new UserStatistics(phone_book_item)).ToList();
		}

		public UserStatistics GetUserStatistics(string username)
		{
			lock (sync)
			{
				foreach (var user_statistics in PhoneBook.Where(user_statistics => user_statistics.PhoneBookItem.Username == username))
				{
					return user_statistics;
				}
			}
			return null;
		}

		public PhoneNumberStatistics GetPhoneNumberStatistics(string phone_number)
		{
			lock (sync)
			{
				foreach (var user_statistics in PhoneBook.Where(user_statistics => user_statistics.NumberStatistics.ContainsKey(phone_number)))
				{
					return user_statistics.NumberStatistics[phone_number];
				}
			}
			return null;
		}

		internal void CopyOldStatistics(UserStatisticsContainer old)
		{
			if (old == null) return;

			lock (sync)
			{
				foreach (var old_user_statistics in old.PhoneBook)
				{
					var user = GetUserStatistics(old_user_statistics.PhoneBookItem.Username);
					if (user != null) user = old_user_statistics;
				}
			}
		}
	}
}
