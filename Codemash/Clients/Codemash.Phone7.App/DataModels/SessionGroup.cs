using System.Collections;
using System.Collections.Generic;

namespace Codemash.Phone7.App.DataModels
{
    public class SessionGroup : IEnumerable<SessionListView>
    {
        public string Title { get; set; }
        public IList<SessionListView> Items { get; set; }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<SessionListView> GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            var that = obj as SessionGroup;
            return (that != null) && (Title.Equals(that.Title));
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }
    }
}
