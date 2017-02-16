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
namespace BWakaBats.Extensions
{
    public static class BoolExtensions
    {
        /// <summary>
        /// Create a Javascript-friendly version of the boolean expresstion
        /// </summary>
        /// <param name="source">A C# True or False value</param>
        /// <returns>true or false</returns>
        public static string ToJavaString(this bool source)
        {
            return source ? "true" : "false";
        }
    }
}