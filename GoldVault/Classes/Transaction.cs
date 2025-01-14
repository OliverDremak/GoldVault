using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace GoldVault.Classes
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Date { get; set; }
        public string Type { get; set; } 
        public int Amount { get; set; }
        public string Note { get; set; }

        [Ignore]
        public string Color => Type == "Bejövő" ? "#4caf50" : "#f44336";

    }

}
