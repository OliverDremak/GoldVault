using CommunityToolkit.Mvvm.ComponentModel;
using GoldVault.Classes;
using GoldVault.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GoldVault.ViewModels
{
    public partial class StockViewModel : ObservableObject
    {
        private readonly StockDatabase _stockDatabase;

        public ObservableCollection<Stock> Stocks { get; private set; } = new ObservableCollection<Stock>();

        public ICommand RemoveStockCommand { get; }
        public ICommand AddFavoriteCommand { get; }
        public ICommand AddStockCommand { get; }
        public ICommand AddExamplesCommand { get; }
        public StockViewModel(StockDatabase stockDatabase)
        {
            _stockDatabase = stockDatabase;
            LoadStocks();
            //SeedExampleStocks();

            RemoveStockCommand = new Command<Stock>(async stock => await RemoveStock(stock));
            AddFavoriteCommand = new Command<Stock>(async stock => await AddFavorite(stock));
            AddStockCommand = new Command<Stock>(async stock => await AddStock());
            AddExamplesCommand = new Command(async () => await SeedExampleStocks());
        }

        private async void LoadStocks()
        {
            var stocks = await _stockDatabase.GetStocksAsync();
            Stocks.Clear();
            foreach (var stock in stocks)
            {
                Stocks.Add(stock);
            }
        }

        public async Task AddStock()
        {
            var stock = new Stock { Name = "New Stock", Symbol = "NEW", CurrentPrice = "0.00", DailyChange = "+0.00" };
            await _stockDatabase.SaveStockAsync(stock);
            Stocks.Add(stock);
        }

        public async Task DeleteStock(Stock stock)
        {
            await _stockDatabase.DeleteStockAsync(stock);
            Stocks.Remove(stock);
        }

        public async Task SeedExampleStocks()
        {
            var exampleStocks = new List<Stock>
            {
                new Stock { Name = "Apple Inc.", Symbol = "AAPL", CurrentPrice = "175.64", DailyChange = "+1.12" },
                new Stock { Name = "Microsoft Corp.", Symbol = "MSFT", CurrentPrice = "313.45", DailyChange = "-0.67" },
                new Stock { Name = "Tesla Inc.", Symbol = "TSLA", CurrentPrice = "732.12", DailyChange = "+5.89" },
                new Stock { Name = "Amazon.com Inc.", Symbol = "AMZN", CurrentPrice = "139.48", DailyChange = "-1.25" },
                new Stock { Name = "Alphabet Inc.", Symbol = "GOOGL", CurrentPrice = "125.76", DailyChange = "+0.42" }
            };

            foreach (var stock in exampleStocks)
            {
                await _stockDatabase.SaveStockAsync(stock);
                Stocks.Add(stock);
            }
        }

        public async Task RemoveStock(Stock stock)
        {
            if (stock == null) return;
            await _stockDatabase.DeleteStockAsync(stock);
            Stocks.Remove(stock);
        }

        public async Task AddFavorite(Stock stock)
        {
            if (stock == null) return;
            stock.IsFavorite = !stock.IsFavorite;
            await _stockDatabase.SaveStockAsync(stock);

            // Refresh the collection to reflect changes
            //Stocks.Clear();
            //LoadStocks();
            var index = Stocks.IndexOf(stock);
            if (index >= 0)
            {
                Stocks[index] = stock;
            }
        }
    }
}
