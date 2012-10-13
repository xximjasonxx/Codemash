﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codemash.Client.Data.Entities;

namespace Codemash.Client.Data.Repository
{
    public interface IRepository<T> where T : EntityBase
    {
        /// <summary>
        /// Load the repository with data
        /// </summary>
        void Load();

        /// <summary>
        /// Indicates Load has completed
        /// </summary>
        event EventHandler LoadCompleted;

        /// <summary>
        /// Return an item from the repository by an ID value
        /// </summary>
        /// <param name="id">The ID value, should be unique</param>
        /// <returns></returns>
        T Get(int id);

        /// <summary>
        /// return a list of all items in the repository
        /// </summary>
        /// <returns></returns>
        IList<T> GetAll();
    }
}