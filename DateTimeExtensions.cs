// *****************************************************
//                      EXTENSIONS     
//                  by  Shane Whitehead
//                  bwakabats@gmail.com
// *****************************************************
//      The software is released under the GNU GPL:
//          http://www.gnu.org/licenses/gpl.txt
//
// Feel free to use, modify and distribute this software
// I only ask you to keep this comment intact.
// Please contact me with bugs, ideas, modification etc.
// *****************************************************
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace BWakaBats.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Get the date in the format Nth Monthember
        /// </summary>
        /// <param name="value">The date</param>
        /// <returns>The format Nth Monthember</returns>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ToDay")]
        public static string ToDayMonthString(this DateTime value)
        {
            return value.ToString("ddd", CultureInfo.InvariantCulture) + " " + value.Day.ToOrdinalString() + " " + value.ToString("MMM", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Get the date in the format Nth Monthember 1999
        /// </summary>
        /// <param name="value">The date</param>
        /// <returns>The format Nth Monthember 1999</returns>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ToDay")]
        public static string ToDayMonthYearString(this DateTime value)
        {
            return value.ToString("ddd", CultureInfo.InvariantCulture) + " " + value.Day.ToOrdinalString() + " " + value.ToString("MMM yyyy", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Get the approximate date and time relative to another date and time
        /// </summary>
        /// <example>
        /// Today in 30 minutes;
        /// Today, 3 hours ago;
        /// Tomorrow at 9:30;
        /// Last Sunday;
        /// Wednesday at 9:30;
        /// 19th January;
        /// </example>
        /// <param name="value"></param>
        /// <param name="relativeTo"></param>
        /// <returns></returns>
        public static string ToRelativeString(this DateTime value, DateTime relativeTo = default(DateTime))
        {
            var hasTime = value.TimeOfDay.TotalMilliseconds != 0;
            if (relativeTo == default(DateTime))
            {
                relativeTo = DateTimeUK.Now;
            }
            string time = hasTime ? " at " + value.TimeOfDay.ToTimeString() : "";

            if (relativeTo.Year != value.Year)
            {
                return value.ToDayMonthYearString() + time;
            }

            var diff = relativeTo.Subtract(value);

            int days = diff.Days;
            if (days > 6 || days < -6)
            {
                return value.ToDayMonthString() + time;
            }
            if (days > 1)
            {
                return "Last " + value.ToString("dddd", CultureInfo.InvariantCulture) + time;
            }
            if (days < -1)
            {
                return "This " + value.ToString("dddd", CultureInfo.InvariantCulture) + time;
            }
            if (days == -1)
            {
                return "Tomorrow" + time;
            }
            if (days == 1)
            {
                return "Yesterday" + time;
            }

            if (!hasTime)
                return "Today";

            int hours = diff.Hours;
            if (hours < -1)
            {
                return "Today in " + (-hours).ToPluralString("hour");
            }
            if (hours > 1)
            {
                return "Today, " + hours.ToPluralString("hour") + " ago";
            }

            int minutes = diff.Minutes;
            if (minutes < 1 && minutes > -1)
            {
                return "Just now";
            }
            if (minutes < 0)
            {
                return "Today in " + (-minutes).ToPluralString("minute");
            }
            return "Today, " + minutes.ToPluralString("minute") + " ago";
        }
    }
}