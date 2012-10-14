using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Codemash.Client.Data.Entities;
using Newtonsoft.Json.Linq;
using SQLite;

namespace Codemash.Client.Data.Repository.Impl
{
    public abstract class RepositoryBase<T> where T : EntityBase, new()
    {
        private const string DATABASE_NAME = "Codemash2013.db";

        private IList<T> _repository;
        private readonly string _databasePath;

        protected IList<T> Repository
        {
            get { return _repository ?? (_repository = new List<T>()); }
        }

        /// <summary>
        /// Determine if a Table exists for this entity type
        /// </summary>
        private bool TableExists
        {
            get
            {
                try
                {
                    using (var db = new SQLiteConnection(_databasePath))
                    {
                        return db.Table<T>().Any();
                    }
                }
                catch (SQLiteException)
                {
                    return false;
                }
            }
        }

        protected RepositoryBase()
        {
            string applicationPath = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            _databasePath = Path.Combine(applicationPath, DATABASE_NAME);
        }

        /// <summary>
        /// Load the repository with data
        /// </summary>
        public async Task<int> LoadAsync()
        {
            if (!TableExists)
            {
                var entities = await LoadFromWebServiceAsync();
                foreach (var entity in entities)
                {
                    Repository.Add(entity);
                }

                Save();
            }
            else
            {
                LoadRepositoryFromDatabase();
            }

            return 0;
        }

        public event EventHandler LoadCompleted;

        private async Task<IList<T>> LoadFromWebServiceAsync()
        {
            using (var client = new HttpClient())
            {
                var responseString = await client.GetStringAsync(DownloadUrl);
                var jsonArray = JArray.Parse(responseString);

                return (from ji in jsonArray.AsJEnumerable()
                        select CreateEntity(ji)).ToList();
            }
        }

        private void LoadRepositoryFromDatabase()
        {
            using (var db = new SQLiteConnection(_databasePath))
            {
                var entities = db.Table<T>().ToList();
                foreach (var entity in entities)
                {
                    entity.MarkUnmodified();
                    Repository.Add(entity);
                }
            }
        }

        /// <summary>
        /// Save all dirty entities in the Repository
        /// </summary>
        protected void Save()
        {
            using (var db = new SQLiteConnection(_databasePath))
            {
                var dirtyEntities = Repository.Where(e => e.IsDirty).ToList();
                db.CreateTable<T>();

                foreach (var entity in dirtyEntities)
                {
                    switch (entity.EntityState)
                    {
                        case EntityState.Modified:
                            db.Update(entity);
                            break;
                        case EntityState.Deleted:
                            db.Delete(entity);
                            break;
                        case EntityState.New:
                            db.Insert(entity);
                            break;
                    }
                }
            }

            CleanRepository();
        }

        /// <summary>
        /// Remove Deleted entities and reset EntityState for all to Unmodified
        /// </summary>
        private void CleanRepository()
        {
            // remove objects which have been deleted
            var removedEntities = Repository.Where(e => e.EntityState == EntityState.Deleted).ToList();
            foreach (var entity in removedEntities)
            {
                Repository.Remove(entity);
            }

            // reflect that all entities went through save
            foreach (var entity in Repository)
            {
                entity.MarkUnmodified();
            }
        }

        // abstract properties
        protected abstract string DownloadUrl { get; }

        // abstract methods
        protected abstract T CreateEntity(JToken token);
    }
}
