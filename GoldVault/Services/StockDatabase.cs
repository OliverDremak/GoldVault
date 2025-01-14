using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldVault.Classes;
using SQLite;

namespace GoldVault.Services
{
    public class StockDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public StockDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Stock>().Wait();
        }

        public Task<List<Stock>> GetStocksAsync()
        {
            return _database.Table<Stock>().ToListAsync();
        }

        public Task<Stock> GetStockAsync(int id)
        {
            return _database.Table<Stock>().FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> SaveStockAsync(Stock stock)
        {
            if (stock.Id != 0)
            {
                return _database.UpdateAsync(stock);
            }
            else
            {
                return _database.InsertAsync(stock);
            }
        }

        public Task<int> DeleteStockAsync(Stock stock)
        {
            return _database.DeleteAsync(stock);
        }
    }
}
