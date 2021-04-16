using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CovidApp
{
    public class RequestDB
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [NotNull]
        public string Title { get; set; }
        [NotNull]
        public string Description { get; set; }
        [NotNull]
        public string BuyList { get; set; }
        [NotNull]
        public double Longitude { get; set; }
        [NotNull]
        public double Latitude { get; set; }
        [ForeignKey(typeof(UserDB))]
        public int UserId { get; set; }
        [ManyToOne]
        public UserDB User { get; set; }
         
    }

}
