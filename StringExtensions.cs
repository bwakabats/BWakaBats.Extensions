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
using System.Text;
using System.Text.RegularExpressions;

namespace BWakaBats.Extensions
{
    public static partial class StringExtensions
    {
        /// <summary>
        ///   Returns a new string in which all occurrences of a specified string (optionally ignoring the case) in the current
        ///   instance are replaced with another specified string.
        /// </summary>
        /// <param name="source">The source string</param>
        /// <param name="oldValue">The string to be replaced.</param>
        /// <param name="newValue">The string to replace all occurrences of oldValue.</param>
        /// <param name="ignoreCase">Ignoe the case?</param>
        /// <returns>
        ///   A string that is equivalent to the current string except that all instances of
        ///   oldValue are replaced with newValue. If oldValue is not found in the current
        ///   instance, the method returns the current instance unchanged.
        /// </returns>
        public static string Replace(this string source, string oldValue, string newValue, bool ignoreCase)
        {
            if (!ignoreCase)
                return source.Replace(oldValue, newValue);
            return Regex.Replace(source, oldValue, newValue ?? "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Returns a new string in which all the uppercase letter and number have been prefixed with a space.
        /// </summary>
        /// <param name="source">The source string</param>
        /// <param name="fromVariableName">If true, then some words are removed (e.g. Id)</param>
        /// <returns>The words</returns>
        public static string ToWords(this string source, bool fromVariableName = false)
        {
            if (string.IsNullOrWhiteSpace(source))
                return "";

            string words = Regex.Replace(source, "[^A-Za-z0-9]", " ");
            words = Regex.Replace(words, "(?<!(^|[A-Z]))(?=[A-Z])|(?<!^)(?=[A-Z][a-z])", " $1") + " ";

            if (fromVariableName)
            {
                if (words.EndsWith(" Id ", StringComparison.Ordinal))
                {
                    words = words.Substring(0, words.Length - 3);
                }
                words = words.Replace("Date Time", " ");
                words = words.Replace(" Html ", " ");
            }
            while (words.IndexOf("  ", StringComparison.Ordinal) > 0)
            {
                words = words.Replace("  ", " ");
            }
            return words.Trim();
        }

        /// <summary>
        /// Returns a null if the original string contains nothing but whitespace and/or HTML tags.
        /// Otherwise returns the original string
        /// </summary>
        /// <param name="source">The source string</param>
        /// <returns>The original string, or null</returns>
        public static string TrimHtml(this string source)
        {
            //Return null if just HTML tags and whitespace...
            if (string.IsNullOrWhiteSpace(source))
                return null;

            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            string result = source.Replace("&lt;", "<").Replace("&gt;", ">");
            for (int index = 0; index < result.Length; index++)
            {
                char chr = result[index];
                switch (chr)
                {
                    case '<':
                        inside = true;
                        break;
                    case '>':
                        inside = false;
                        break;
                    default:
                        if (!inside)
                        {
                            array[arrayIndex] = chr;
                            arrayIndex++;
                        }
                        break;
                }
            }
            result = new string(array, 0, arrayIndex);
            result = result.Replace("&nbsp;", " ");
            if (string.IsNullOrWhiteSpace(result))
                return null;

            return source; // othersize return the original source
        }

        /// <summary>
        /// Returns the Soundex of a string. Optionally, pad the result with 0s to 4 characters
        /// </summary>
        /// <param name="words">The source word</param>
        /// <param name="pad">Pad with 0s to 4 characters?</param>
        /// <returns>The soundex</returns>
        public static string Soundex(this string words, bool pad)
        {
            return words.Soundex(4, pad);
        }

        /// <summary>
        /// Returns the Soundex of a string. 
        /// </summary>
        /// <param name="words">The source work</param>
        /// <param name="length">The maximum length of the Soundex</param>
        /// <param name="pad">Pad the result with 0s to the maximum length?</param>
        /// <returns></returns>
        public static string Soundex(this string words, int length = 4, bool pad = true)
        {
            if (string.IsNullOrWhiteSpace(words))
                return "";

            string result = "";
            //               ABCDEFGHIJKLMNOPQRSTUVWXYZ
            string lookup = "01230120022455012623010202";
            char previousChar = ' ';
            char currentChar = ' ';
            int currentLength = 0;
            foreach (var chr in words.ToUpperInvariant())
            {
                if (chr >= 'A' && chr <= 'Z')
                {
                    if (currentLength == 0)
                    {
                        result += chr;
                        currentLength++;
                        if (currentLength == length)
                            return result;
                    }
                    else
                    {
                        currentChar = lookup[chr - 'A'];
                        if (currentChar != '0' && previousChar != currentChar)
                        {
                            result += currentChar;
                            currentLength++;
                            if (currentLength == length)
                                return result;
                        }
                    }
                }
                else
                {
                    currentChar = ' ';
                }
                previousChar = currentChar;
            }

            if (!pad)
                return result;

            return result + new string('0', length - currentLength);
        }

        /// <summary>
        /// Return the string converted to a Guid
        /// </summary>
        /// <param name="source">The source string</param>
        /// <returns>The guid equivalent</returns>
        public static Guid ToGuid(this string source)
        {
            byte[] stringbytes = Encoding.UTF8.GetBytes(source);
            using (var provider = new System.Security.Cryptography.SHA1CryptoServiceProvider())
            {
                byte[] hashedBytes = provider.ComputeHash(stringbytes);
                Array.Resize(ref hashedBytes, 16);
                return new Guid(hashedBytes);
            }
        }
    }
}