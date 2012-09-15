﻿
using System;

namespace Codemash.Api.Data.Entities
{
    public class SessionChange : EntityBase, IChange
    {
        private int _sessionId;
        private ChangeAction _action;
        private string _key;
        private string _value;

        public int SessionChangeId { get; set; }

        /// <summary>
        /// Allows changes to be grouped in blocks by when they were created
        /// </summary>
        internal DateTime ChangeTime { get; set; }

        public int SessionId
        {
            get { return _sessionId; }
            set
            {
                ValueChanged();
                _sessionId = value;
            }
        }

        public int ID
        {
            set { SessionId = value; }
        }

        public ChangeAction Action
        {
            get { return _action; }
            set
            {
                ValueChanged();
                _action = value;
            }
        }

        public string Key
        {
            get { return _key; }
            set
            {
                ValueChanged();
                _key = value;
            }
        }

        public string Value
        {
            get { return _value; }
            set
            {
                ValueChanged();
                _value = value;
            }
        }
    }
}
