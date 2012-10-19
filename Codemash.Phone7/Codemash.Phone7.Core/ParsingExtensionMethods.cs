using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Codemash.Phone7.Core
{
    public static class ParsingExtensionMethods
    {
        /// <summary>
        /// Takes a string and parses it a unique key value
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int AsKey(this string str)
        {
            return str.GetHashCode();
        }
    }
}
