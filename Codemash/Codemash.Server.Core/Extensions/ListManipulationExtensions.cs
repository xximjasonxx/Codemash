using System;
using System.Collections.Generic;

namespace Codemash.Server.Core.Extensions
{
    public static class ListManipulationExtensions
    {
        /// <summary>
        /// Given an action, apply that action to each item in the list
        /// </summary>
        /// <typeparam name="T">The type of items contained within the list</typeparam>
        /// <param name="list">The list instance being extended</param>
        /// <param name="manipulator">The manipulation to occur for each list item</param>
        /// <remarks>The incoming type cannot be a primitive (int, string) even those these are classes</remarks>
        public static void Apply<T>(this IEnumerable<T> list, Action<T> manipulator) where T : class
        {
            foreach (var item in list)
            {
                manipulator.Invoke(item);
            }
        }
    }
}
