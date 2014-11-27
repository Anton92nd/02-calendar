using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Calendar
{
	class Program
	{
		private static readonly string[] MonthToString = new[]
		{
			"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
		};

		static private int ConvertDayOfWeek(DayOfWeek day)
		{
			return ((int) day + 6) % 7;
		}

		static private int GetWeekNumber(DateTime date)
		{
			var jan1 = ConvertDayOfWeek(new DateTime(date.Year, 1, 1).DayOfWeek);
			return (jan1 + date.DayOfYear - 1)/7;
		}

		static void Main(string[] args)
		{
			var date = DateTime.Parse(Console.ReadLine());
			var image = DrawImage(date);
			image.Save("calendar.png", ImageFormat.Png);
		}

		private static Bitmap DrawImage(DateTime date)
		{
			var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
			var beginWeek = GetWeekNumber(new DateTime(date.Year, date.Month, 1));
			var endWeek = GetWeekNumber(new DateTime(date.Year, date.Month, daysInMonth));
			var endWeekDay = ConvertDayOfWeek(new DateTime(date.Year, date.Month, daysInMonth).DayOfWeek);
			var image = new Bitmap(8 * 70, (endWeek - beginWeek + 3) * 70);
			var g = Graphics.FromImage(image);
			InitImage(image, g, date);
			var weeks = Enumerable.Range(beginWeek + 1, endWeek - beginWeek + 1).ToArray();
			if (date.Month == 12 && endWeekDay < 6)
				weeks[weeks.Length - 1] = 1;
			DrawWeeks(image, g, weeks);
			DrawDays(image, g, date);
			return image;
		}

		private static void DrawDays(Bitmap image, Graphics g, DateTime date)
		{
			var firstDay = new DateTime(date.Year, date.Month, 1);
			var dayOfWeek = ConvertDayOfWeek(firstDay.DayOfWeek);
			var day = 1;
			var week = 0;
			var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
			for (int i = 0; i < daysInMonth; i++)
			{
				if (day == date.Day)
				{
					g.FillEllipse(Brushes.Goldenrod, 63 + dayOfWeek * 70, 140 + week * 70, 70, 70);
				}
				var brush = dayOfWeek == 6 ? Brushes.Red : Brushes.Black;
				g.DrawString(day.ToString(), new Font("Courier", 16), brush, 80F + dayOfWeek * 70F, 165F + week * 70F);
				week += (dayOfWeek + 1)/7;
				dayOfWeek = (dayOfWeek + 1)%7;
				day++;
			}
		}

		private static void DrawWeeks(Bitmap image, Graphics g, int[] weeks)
		{
			for (var i = 0; i < weeks.Length; i++)
			{
				g.DrawString(weeks[i].ToString(), new Font("Courier", 16, FontStyle.Bold), Brushes.DarkSlateGray, 10F, 165F + 70 * i);
			}
		}

		private static readonly string[] WeekDays = {"MON", "TUE", "WED", "THU", "FRI", "SAT", "SUN"};

		private static void InitImage(Bitmap image, Graphics g, DateTime date)
		{
			g.FillRectangle(Brushes.Gainsboro, 0, 0, image.Width, image.Height);
			g.DrawString(MonthToString[date.Month - 1] + ", " + date.Year, new Font("Calibri", 28, FontStyle.Bold),
				Brushes.DarkViolet, image.Width / 2.0F - 120, 15.0F);
			g.DrawString("#", new Font("Courier", 16), Brushes.BlueViolet, 20F, 100F);
			for (var x = 0; x < 7; x++)
				g.DrawString(WeekDays[x], new Font("Courier", 16), Brushes.DarkViolet, 70F + x * 70F, 100F);
		}
	}
}
