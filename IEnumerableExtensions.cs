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
using System.Collections.Generic;
using System.Linq;

namespace BWakaBats.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<TSource> OrderByRandom<TSource>(this IEnumerable<TSource> source)
        {
            return source.OrderBy<TSource, Guid>(e => Guid.NewGuid());
        }

        #region IndexOf

        public static int IndexOf<TSource>(this IEnumerable<TSource> source, TSource item)
        {
            return source.IndexOf(item, 0);
        }

        public static int IndexOf<TSource>(this IEnumerable<TSource> source, TSource item, int index)
        {
            var list = source as List<TSource>;
            if (list != null)
                return list.IndexOf(item, index);

            return source.IndexOf(item, index, int.MaxValue);
        }

        public static int IndexOf<TSource>(this IEnumerable<TSource> source, TSource item, int index, int count)
        {
            var list = source as List<TSource>;
            if (list != null)
                return list.IndexOf(item, index, count);

            int position = 0;
            int end = count == int.MaxValue ? int.MaxValue : index + count;
            var comparer = EqualityComparer<TSource>.Default;
            foreach (var current in source)
            {
                if (position >= index && comparer.Equals(current, item))
                    return position;
                position++;
                if (position >= end)
                    break;
            }
            return -1;
        }

        #endregion

        #region MaxOrDefault

        public static TSource MaxOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            return source.DefaultIfEmpty().Max();
        }

        public static TSource MaxOrDefault<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
        {
            return source.DefaultIfEmpty(defaultValue).Max();
        }

        public static TResult MaxOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return source.Select(selector).DefaultIfEmpty().Max();
        }

        public static TResult MaxOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector, TResult defaultValue)
        {
            return source.Select(selector).DefaultIfEmpty(defaultValue).Max();
        }

        #endregion

        #region MinOrDefault

        public static TSource MinOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            return source.DefaultIfEmpty().Min();
        }

        public static TSource MinOrDefault<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
        {
            return source.DefaultIfEmpty(defaultValue).Min();
        }

        public static TResult MinOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return source.Select(selector).DefaultIfEmpty().Min();
        }

        public static TResult MinOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector, TResult defaultValue)
        {
            return source.Select(selector).DefaultIfEmpty(defaultValue).Min();
        }

        #endregion

        #region AverageOrDefault

        public static double AverageOrDefault(this IEnumerable<int> source, double defaultValue = 0)
        {
            return source.Any() ? source.Average() : defaultValue;
        }

        public static double AverageOrDefault(this IEnumerable<long> source, double defaultValue = 0)
        {
            return source.Any() ? source.Average() : defaultValue;
        }

        public static double AverageOrDefault(this IEnumerable<double> source, double defaultValue = 0)
        {
            return source.Any() ? source.Average() : defaultValue;
        }

        public static float AverageOrDefault(this IEnumerable<float> source, float defaultValue = 0)
        {
            return source.Any() ? source.Average() : defaultValue;
        }

        public static decimal AverageOrDefault(this IEnumerable<decimal> source, decimal defaultValue = 0)
        {
            return source.Any() ? source.Average() : defaultValue;
        }

        public static double AverageOrDefault(this IEnumerable<int?> source, double defaultValue = 0)
        {
            var results = source.Average();
            return results.HasValue ? results.Value : defaultValue;
        }

        public static double AverageOrDefault(this IEnumerable<long?> source, double defaultValue = 0)
        {
            var results = source.Average();
            return results.HasValue ? results.Value : defaultValue;
        }

        public static double AverageOrDefault(this IEnumerable<double?> source, double defaultValue = 0)
        {
            var results = source.Average();
            return results.HasValue ? results.Value : defaultValue;
        }

        public static float AverageOrDefault(this IEnumerable<float?> source, float defaultValue = 0)
        {
            var results = source.Average();
            return results.HasValue ? results.Value : defaultValue;
        }

        public static decimal AverageOrDefault(this IEnumerable<decimal?> source, decimal defaultValue = 0)
        {
            var results = source.Average();
            return results.HasValue ? results.Value : defaultValue;
        }

        public static double AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector, double defaultValue = 0)
        {
            return source.Any() ? source.Average(selector) : defaultValue;
        }

        public static double AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector, double defaultValue = 0)
        {
            return source.Any() ? source.Average(selector) : defaultValue;
        }

        public static double AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector, double defaultValue = 0)
        {
            return source.Any() ? source.Average(selector) : defaultValue;
        }

        public static float AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector, float defaultValue = 0)
        {
            return source.Any() ? source.Average(selector) : defaultValue;
        }

        public static decimal AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector, decimal defaultValue = 0)
        {
            return source.Any() ? source.Average(selector) : defaultValue;
        }

        public static double AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector, double defaultValue = 0)
        {
            var results = source.Average(selector);
            return results.HasValue ? results.Value : defaultValue;
        }

        public static double AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector, double defaultValue = 0)
        {
            var results = source.Average(selector);
            return results.HasValue ? results.Value : defaultValue;
        }

        public static double AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector, double defaultValue = 0)
        {
            var results = source.Average(selector);
            return results.HasValue ? results.Value : defaultValue;
        }

        public static float AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector, float defaultValue = 0)
        {
            var results = source.Average(selector);
            return results.HasValue ? results.Value : defaultValue;
        }

        public static decimal AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector, decimal defaultValue = 0)
        {
            var results = source.Average(selector);
            return results.HasValue ? results.Value : defaultValue;
        }

        #endregion

        #region SumOrDefault

        public static int SumOrDefault(this IEnumerable<int> source, int defaultValue = 0)
        {
            return source.Any() ? source.Sum() : defaultValue;
        }

        public static long SumOrDefault(this IEnumerable<long> source, long defaultValue = 0)
        {
            return source.Any() ? source.Sum() : defaultValue;
        }

        public static double SumOrDefault(this IEnumerable<double> source, double defaultValue = 0)
        {
            return source.Any() ? source.Sum() : defaultValue;
        }

        public static float SumOrDefault(this IEnumerable<float> source, float defaultValue = 0)
        {
            return source.Any() ? source.Sum() : defaultValue;
        }

        public static decimal SumOrDefault(this IEnumerable<decimal> source, decimal defaultValue = 0)
        {
            return source.Any() ? source.Sum() : defaultValue;
        }

        public static int SumOrDefault(this IEnumerable<int?> source, int defaultValue = 0)
        {
            var results = source.Sum();
            return results.HasValue ? results.Value : defaultValue;
        }

        public static long SumOrDefault(this IEnumerable<long?> source, long defaultValue = 0)
        {
            var results = source.Sum();
            return results.HasValue ? results.Value : defaultValue;
        }

        public static double SumOrDefault(this IEnumerable<double?> source, double defaultValue = 0)
        {
            var results = source.Sum();
            return results.HasValue ? results.Value : defaultValue;
        }

        public static float SumOrDefault(this IEnumerable<float?> source, float defaultValue = 0)
        {
            var results = source.Sum();
            return results.HasValue ? results.Value : defaultValue;
        }

        public static decimal SumOrDefault(this IEnumerable<decimal?> source, decimal defaultValue = 0)
        {
            var results = source.Sum();
            return results.HasValue ? results.Value : defaultValue;
        }

        public static int SumOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector, int defaultValue = 0)
        {
            return source.Any() ? source.Sum(selector) : defaultValue;
        }

        public static long SumOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector, long defaultValue = 0)
        {
            return source.Any() ? source.Sum(selector) : defaultValue;
        }

        public static double SumOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector, double defaultValue = 0)
        {
            return source.Any() ? source.Sum(selector) : defaultValue;
        }

        public static float SumOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector, float defaultValue = 0)
        {
            return source.Any() ? source.Sum(selector) : defaultValue;
        }

        public static decimal SumOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector, decimal defaultValue = 0)
        {
            return source.Any() ? source.Sum(selector) : defaultValue;
        }

        public static int SumOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector, int defaultValue = 0)
        {
            var results = source.Sum(selector);
            return results.HasValue ? results.Value : defaultValue;
        }

        public static long SumOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector, long defaultValue = 0)
        {
            var results = source.Sum(selector);
            return results.HasValue ? results.Value : defaultValue;
        }

        public static double SumOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector, double defaultValue = 0)
        {
            var results = source.Sum(selector);
            return results.HasValue ? results.Value : defaultValue;
        }

        public static float SumOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector, float defaultValue = 0)
        {
            var results = source.Sum(selector);
            return results.HasValue ? results.Value : defaultValue;
        }

        public static decimal SumOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector, decimal defaultValue = 0)
        {
            var results = source.Sum(selector);
            return results.HasValue ? results.Value : defaultValue;
        }

        #endregion
    }
}
