using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Parsing
{
    public interface IEntityParser<T> where T : EntityBase
    {
        /// <summary>
        /// Given some "contents" attempt to parse to an instance of T
        /// </summary>
        /// <param name="contents">The contents which will be parsed</param>
        /// <returns></returns>
        T Parse(string contents);
    }
}
