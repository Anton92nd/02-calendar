using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("This program generates calendar page for a given date.\n" +
					"Input date in one of the following formats:\n" + "DD/MM/YYYY or DD.MM.YYYY");
			DateTime date;
			if (DateTime.TryParse(Console.ReadLine(), out date))
			{
				var image = ImageGenerator.GetCalendarImage(new Calendar(date));
				image.Save("calendar.png", ImageFormat.Png);
				Console.WriteLine("Successfully generated!");
			}
			else
			{
				Console.WriteLine("Wrong date format");
			}
		}
	}
}
