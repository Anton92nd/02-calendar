using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
	public enum Months
	{
		January = 1, February, March, April, May, June, July, August, September, October, November, December
	}

	public class Calendar
	{
		public readonly List<int> weeks, days; 
		public readonly int day, year;
		public readonly Months month;

		public Calendar(DateTime date)
		{
			weeks = GetWeeks(date);
			day = date.Day;
			year = date.Year;
			month = (Months)(date.Month);
			days = GetDays(date, weeks.Count);
		}

		private List<int> GetDays(DateTime date, int weeksNumber)
		{
			var result = new List<int>();
			var dayOfWeek = ConvertDayOfWeek(new DateTime(date.Year, date.Month, 1).DayOfWeek);
			var prevMonth = date.AddMonths(-1);
			var daysInPrevMonth = DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
			for (var i = daysInPrevMonth - dayOfWeek + 1; i <= daysInPrevMonth; i++)
				result.Add(i);
			for (var i = 1; i <= DateTime.DaysInMonth(date.Year, date.Month); i++)
				result.Add(i);
			var counter = 1;
			while (result.Count < 7*weeksNumber)
				result.Add(counter++);
			return result;
		}

		private List<int> GetWeeks(DateTime date)
		{
			var firstWeek = GetWeekNumber(new DateTime(date.Year, date.Month, 1));
			var lastDay = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
			var lastWeek = GetWeekNumber(lastDay);
			var result = new List<int>();
			for (var i = firstWeek; i <= lastWeek; i++)
				result.Add(i);
			if (date.Month == 12 && lastDay.DayOfWeek != DayOfWeek.Sunday)
				result[result.Count - 1] = 1;
			return result;
		}

		static private int ConvertDayOfWeek(DayOfWeek day)
		{
			return ((int)day + 6) % 7;
		}

		static private int GetWeekNumber(DateTime date)
		{
			var jan1 = ConvertDayOfWeek(new DateTime(date.Year, 1, 1).DayOfWeek);
			return (jan1 + date.DayOfYear - 1) / 7 + 1;
		}
	}
}
