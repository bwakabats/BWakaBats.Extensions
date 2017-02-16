// *****************************************************
//                      EXTENSIONS     
//                  by  Shane Whitehead
//                  bwakabats@gmail.com
// *****************************************************
//      The software is released under the GNU GPL:
//          http://www.gnu.org/licenses/gpl.txt
//
// Feel free to use, modify and distribute this software
// Please contact me with bugs, ideas, modification etc.
// *****************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace BWakaBats.Extensions
{
    public static class ICollectionExtensions
    {
        /// <summary>
        /// Adds a list of items to the ICollection
        /// </summary>
        /// <typeparam name="TSource">The type of items</typeparam>
        /// <param name="source">The ICollection</param>
        /// <param name="items">The list of object to add to the ICollection</param>
        public static void Add<TSource>(this ICollection<TSource> source, IEnumerable<TSource> items)
        {
            foreach (var item in items.ToList())
            {
                source.Add(item);
            }
        }

        /// <summary>
        /// Removes a list of items from the ICollection
        /// </summary>
        /// <typeparam name="TSource">The type of items</typeparam>
        /// <param name="source">The ICollection</param>
        /// <param name="items">The list of object to remove from the ICollection</param>
        public static void Remove<TSource>(this ICollection<TSource> source, IEnumerable<TSource> items)
        {
            foreach (var item in items.ToList())
            {
                source.Remove(item);
            }
        }

        /// <summary>
        /// Removes the items that match the predicate from the ICollection
        /// </summary>
        /// <typeparam name="TSource">The type of items</typeparam>
        /// <param name="source">The ICollection</param>
        /// <param name="items">The predicate</param>
        public static void Remove<TSource>(this ICollection<TSource> source, Func<TSource, bool> predicate)
            where TSource : class
        {
            source.Remove(source.Where(predicate));
        }

        /// <summary>
        /// Removes all the items from the ICollection (not always the same as clearing the collection)
        /// </summary>
        /// <typeparam name="TSource">The type of items</typeparam>
        /// <param name="source">The ICollection</param>
        public static void RemoveAll<TSource>(this ICollection<TSource> source)
            where TSource : class
        {
            source.Remove(source);
        }
    }
}