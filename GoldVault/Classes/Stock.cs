using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldVault.Classes
{
    public class Stock
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string CurrentPrice { get; set; }
        public string DailyChange { get; set; }
        public bool IsFavorite { get; set; } = false;

        [Ignore]
        public string ChangeColor => DailyChange.StartsWith("-") ? "#f44336" : "#4caf50";
        [Ignore]
        public string FavColor => IsFavorite ? "lightgreen" : "#ffffff";
    }
}
