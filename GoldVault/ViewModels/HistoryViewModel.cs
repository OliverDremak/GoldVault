using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldVault.Classes;
using GoldVault.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace GoldVault.ViewModels
{
    public partial class HistoryViewModel : ObservableObject
    {
        private readonly TransactionDatabase _transactionDatabase;

        [ObservableProperty]
        public ObservableCollection<Transaction> transactions = new ObservableCollection<Transaction>();

        [ObservableProperty]
        public string sumIncome;
        [ObservableProperty]
        public string sumOutcome;

        [RelayCommand]
        private async void Appearing()
        {
            //AddExampleTransactions(); //Proba transzakciók hozzáadása
            await LoadTransactionsAsync();
            SumOutcome = Transactions.Where(t => t.Type == "Kimenő").Sum(t => t.Amount).ToString();
            SumIncome = Transactions.Where(t => t.Type == "Bejövő").Sum(t => t.Amount).ToString();
        }

        public HistoryViewModel(TransactionDatabase transactionDatabase)
        {
            _transactionDatabase = transactionDatabase;
            //AddExampleTransactions();
            //Task.Run(async () => await LoadTransactionsAsync());
            //SumOutcome = Transactions.Where(t => t.Type == "Kimenő").Sum(t => t.Amount).ToString();
            //SumIncome = Transactions.Where(t => t.Type == "Bejövő").Sum(t => t.Amount).ToString();

        }

        private async Task LoadTransactionsAsync()
        {
            var transactions = await _transactionDatabase.GetTransactionsAsync();
            Transactions.Clear();
            foreach (var transaction in transactions)
            {
                Transactions.Add(transaction);
            }
        }

        private void AddExampleTransactions()
        {
            var exampleTransactions = new List<Transaction>
        {
            new Transaction { Date = "2025-01-01", Type = "Bejövő", Amount = 5000, Note = "Fizetés"},
            new Transaction { Date = "2025-01-02", Type = "Kimenő", Amount = 2000, Note = "Bevásárlás"},
            new Transaction { Date = "2025-01-03", Type = "Bejövő", Amount = 3000, Note = "Ajándék"}
        };

            foreach (var transaction in exampleTransactions)
            {
                Transactions.Add(transaction);
            }
        }
    }
}
