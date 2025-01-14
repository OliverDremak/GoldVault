using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldVault.Classes;
using Microsoft.Win32;
using SQLite;

namespace GoldVault.Services
{
    public class TransactionDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public TransactionDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Transaction>().Wait();
        }

        public Task<List<Transaction>> GetTransactionsAsync()
        {
            return _database.Table<Transaction>().ToListAsync();
        }

        public Task<int> SaveTransactionAsync(Transaction transaction)
        {
            if (transaction.Id != 0)
            {
                return _database.UpdateAsync(transaction);
            }
            else
            {
                return _database.InsertAsync(transaction);
            }
        }

        public Task<int> DeleteTransactionAsync(Transaction transaction)
        {
            return _database.DeleteAsync(transaction);
        }
    }
}