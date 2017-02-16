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
    public static class IntExtensions
    {
        /// <summary>
        /// Get a string in the form: "no [type]s" for value 0, "[an/a] [type]" for value 1, or "[n] [type]s" for more than 1 .
        /// </summary>
        /// <remarks>
        /// The parameter "userAn" is used to determine, for a single item, the type will be prefixed with "an"
        /// (see the example "an hat")
        /// </remarks>
        /// <seealso cref="ToPluralString(int,string,string)"/>
        /// <example>
        /// 0 leaf,false = no leafs
        /// 1,leaf,false = a leaf
        /// 10,leaf,false = 10 leafs
        /// 0,leaf,false,leaves = no leaves
        /// 1,leaf,false,leaves = a leaf
        /// 10,leaf,false,leaves = 10 leaves
        /// 1,hotel,true = an hotel
        /// 1,apple,true = an apple
        /// 1,hat,false = a hat
        /// </example>
        /// <param name="value">The numeric value</param>
        /// <param name="type">The type of thing you are counting</param>
        /// <param name="forceAn">Force the logic to use "an"</param>
        /// <param name="plural">The plural version of the thing you are counting</param>
        /// <returns>[no/an/a/N] [types](s)</returns>
        public static string ToPluralString(this int value, string type, bool forceAn, string plural = null)
        {
            switch (value)
            {
                case 0:
                    return "no " + (plural ?? type + "s");
                case 1:
                    return (forceAn ? "an " : "a ") + type;
            }
            return value + " " + (plural ?? type + "s");
        }

        /// <summary>
        /// Get a string in the form: "no [type]s" for value 0, "[an/a] [type]" for value 1, or "[n] [type]s" for more than 1 
        /// </summary>
        /// <remarks>
        /// The logic assumes that, for a single item, any type name that starting with any one of AEIOUHaeiouh will be prefixed with "an"
        /// (see the example "an hat")
        /// </remarks>
        /// <seealso cref="ToPluralString(int,string,bool,string)"/>
        /// <example>
        /// 0 leaf = no leafs
        /// 1,leaf = a leaf
        /// 10,leaf = 10 leafs
        /// 0,leaf,leaves = no leaves
        /// 1,leaf,leaves = a leaf
        /// 10,leaf,leaves = 10 leaves
        /// 1,hotel = an hotel
        /// 1,apple = an apple
        /// 1,hat = an hat
        /// </example>
        /// <param name="value">The numeric value</param>
        /// <param name="type">The type of thing you are counting</param>
        /// <param name="plural">The plural version of the thing you are counting</param>
        /// <param name="useOne">Use 1 instead of a or an</param>
        /// <returns>[no/an/a/N] [types](s)</returns>
        public static string ToPluralString(this int value, string type, string plural = null, bool useOne = false)
        {
            switch (value)
            {
                case 0:
                    return "no " + (plural ?? type + "s");
                case 1:
                    if (useOne)
                        return "1 " + type;
                    return ("AEIOUHaeiouh".IndexOf(type[0]) > -1 ? "an " : "a ") + type;
            }
            return value + " " + (plural ?? type + "s");
        }

        /// <summary>
        /// Get the English ordinal for a number
        /// </summary>
        /// <param name="index">The value to get the ordinal for</param>
        /// <returns>The English ordinal (including the original number)</returns>
        public static string ToOrdinalString(this int index)
        {
            if (index <= 3 || index >= 21)
            {
                if (index == 0)
                    return "Last";

                switch (index % 10)
                {
                    case 1:
                        return index + "st";
                    case 2:
                        return index + "nd";
                    case 3:
                        return index + "rd";
                }
            }
            return index + "th";
        }
    }
}