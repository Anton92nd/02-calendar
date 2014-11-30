using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Calendar
{
	public static class ImageGenerator
	{
		public static Bitmap GetCalendarImage(Calendar calendar)
		{
			var image = new Bitmap(8 * 70, (calendar.weeks.Count + 2) * 70);
			var g = Graphics.FromImage(image);
			InitImage(image, g, calendar.year, calendar.month);
			DrawWeeks(image, g, calendar.weeks);
			DrawDays(image, g, calendar);
			return image;
		}

		private static void DrawDays(Bitmap image, Graphics g, Calendar calendar)
		{
			bool wasFirst = false;
			bool wasLast = false;
			for (int i = 0; i < calendar.days.Count; i++)
			{
				if (calendar.days[i] == 1 && wasFirst)
					wasLast = true;
				if (calendar.days[i] == 1)
					wasFirst = true;
				if (calendar.day == calendar.days[i] && wasFirst && !wasLast)
					g.FillEllipse(Brushes.Goldenrod, 63 + i % 7 * 70, 140 + i / 7 * 70, 70, 70);
				var brush = (i % 7) == 6 ? Brushes.Red : ((wasFirst && !wasLast) ? Brushes.Black : Brushes.Gray);
				g.DrawString(calendar.days[i].ToString(), new Font("Courier", 16), brush, 80F + (i % 7) * 70F, 165F + (i / 7) * 70F);
			}
		}

		private static void DrawWeeks(Bitmap image, Graphics g, List<int> weeks)
		{
			for (var i = 0; i < weeks.Count; i++)
				g.DrawString(weeks[i].ToString(), new Font("Courier", 16, FontStyle.Bold), Brushes.DarkSlateGray, 10F, 165F + 70 * i);
		}

		private static readonly string[] WeekDays = {"MON", "TUE", "WED", "THU", "FRI", "SAT", "SUN"};

		private static void InitImage(Bitmap image, Graphics g, int year, Months month)
		{
			g.FillRectangle(Brushes.Gainsboro, 0, 0, image.Width, image.Height);
			g.DrawString(month.ToString() + ", " + year, new Font("Calibri", 28, FontStyle.Bold),
				Brushes.DarkViolet, image.Width / 2.0F - 120, 15.0F);
			g.DrawString("#", new Font("Courier", 16), Brushes.BlueViolet, 20F, 100F);
			for (var x = 0; x < 7; x++)
				g.DrawString(WeekDays[x], new Font("Courier", 16), Brushes.DarkViolet, 70F + x * 70F, 100F);
		}
	}
}
