using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CovidApp
{
    public class UserDB
    {
            
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [NotNull, Unique]
        public string Username { get; set; }
        [NotNull]
        public string Password { get; set; }
        public string Email { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<RequestDB> Requests { get; set; }

    }
}

