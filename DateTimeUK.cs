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

namespace BWakaBats.Extensions
{
    /// <summary>
    /// Get the current UK time, regardless of locale
    /// </summary>
    public static class DateTimeUK
    {
        private static TimeZoneInfo _gmt;

        public static DateTime ToUKDateTime(this DateTime dateTime)
        {
            return DateTimeUK.ConvertFrom(dateTime);
        }

        /// <summary>
        /// Gets a System.DateTime object that is set to the current date and time in the UK
        /// </summary>
        public static DateTime Now
        {
            get { return ConvertFrom(DateTime.Now); }
        }

        /// <summary>
        /// Converts a System.DateTime object to a date and time in the UK
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ConvertFrom(DateTime dateTime)
        {
            if (_gmt == null)
            {
                _gmt = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            }
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime.ToUniversalTime(), _gmt);
        }

        /// <summary>
        /// Gets the current date in the UK
        /// </summary>
        public static DateTime Today
        {
            get { return ConvertFrom(DateTime.Now).Date; }
        }
    }
}
