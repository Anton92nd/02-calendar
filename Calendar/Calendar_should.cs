using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Calendar
{
	[TestFixture]
	class Calendar_should
	{
		[Test]
		public void save_the_same_date()
		{
			var result = new Calendar(new DateTime(1994, 11, 22));
			Assert.AreEqual(22, result.day);
			Assert.AreEqual((Months)11, result.month);
			Assert.AreEqual(1994, result.year);
		}

		[Test]
		public void generate_weeks_numbers()
		{
			var result = new Calendar(new DateTime(2014, 2, 22)).weeks;
			Assert.AreEqual(new int[] {5, 6, 7, 8, 9}, result);
		}

		[Test]
		public void replace_last_week_if_it_is_the_first_week_of_next_year()
		{
			var result = new Calendar(new DateTime(2014, 12, 30)).weeks;
			Assert.AreEqual(new int[] {49, 50, 51, 52, 1}, result);
		}

		[Test]
		public void generate_days_including_prev_and_next_month()
		{
			var expected = new int[] {30, 31}.Concat(Enumerable.Range(1, 31)).Concat(new int[]{1, 2}).ToArray();
			var result = new Calendar(new DateTime(2014, 1, 2)).days;
			Assert.AreEqual(expected, result);
		}
	}
}
