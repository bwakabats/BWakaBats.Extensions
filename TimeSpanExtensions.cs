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
using System.Globalization;

namespace BWakaBats.Extensions
{
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Returns the timespan as a time
        /// </summary>
        /// <example>
        /// Midnight;
        /// Midday;
        /// 12:30am;
        /// 1:30pm;
        /// </example>
        /// <param name="value">The source time</param>
        /// <returns>The time</returns>
        public static string ToTimeString(this TimeSpan value)
        {
            int hours = value.Hours;
            int minutes = value.Minutes;
            if (hours == 0)
            {
                if (minutes == 0)
                {
                    return "midnight";
                }
                else
                {
                    return "12" + minutes.ToString(":00", CultureInfo.InvariantCulture) + "am";
                }
            }
            else if (hours == 12)
            {
                if (minutes == 0)
                {
                    return "midday";
                }
                else
                {
                    return "12" + minutes.ToString(":00", CultureInfo.InvariantCulture) + "pm";
                }
            }
            else if (hours < 12)
            {
                return hours + minutes.ToString(":00", CultureInfo.InvariantCulture) + "am";
            }
            else
            {
                return (hours - 12) + minutes.ToString(":00", CultureInfo.InvariantCulture) + "pm";
            }
        }

        /// <summary>
        /// Returns the timespan in the format [H hour(s)] [M minutes(s)]
        /// </summary>
        /// <example>
        /// 1 hour 3 minutes;
        /// 1 minute;
        /// 12 hours;
        /// </example>
        /// <param name="value">The time</param>
        /// <returns>The long time</returns>
        public static string ToLongString(this TimeSpan value)
        {
            if (value.Hours == 0)
            {
                return value.Minutes.ToPluralString("minute");
            }
            if (value.Minutes == 0)
            {
                return value.Hours.ToPluralString("hour");
            }
            return value.Hours.ToPluralString("hour") + " " + value.Minutes.ToPluralString("minute");
        }
    }
}