using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GoldVault.Classes;
using GoldVault.Pages;
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
    public partial class MainViewModel : ObservableObject
    {

        private readonly TransactionDatabase _transactionDatabase;

        public ObservableCollection<Transaction> Transactions { get; } = new ObservableCollection<Transaction>();

        [ObservableProperty]
        private string totalBalance;

        [ObservableProperty]
        private int transactionAmount;

        [ObservableProperty]
        private string transactionNote;

        public ICommand NavigateToTransactionsCommand { get; }
        public ICommand SendMoneyCommand { get; }
        public ICommand ReceiveMoneyCommand { get; }

        [RelayCommand]
        private async void Appearing()
        {
            await LoadTransactionsAsync();
            CalculateBalance();
        }
        //TOOD: Implement the NavigateToTransactions method
        //Magyarra vagy angolra? az egesz alkalmazast
        public MainViewModel(TransactionDatabase transactionDatabase)
        {
            _transactionDatabase = transactionDatabase;

            NavigateToTransactionsCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync($"//{nameof(HistoryPage)}");
            });

            SendMoneyCommand = new Command(async () =>
            {
                if (TransactionAmount > 0)
                {
                    await AddTransaction("Kimenő", TransactionAmount, TransactionNote);
                    await RefreshDataAsync();
                    ClearInputFields();
                    await Application.Current.MainPage.DisplayAlert("Alert", "Money sent successfully", "OK");
                }
            });

            ReceiveMoneyCommand = new Command(async () =>
            {
                if (TransactionAmount > 0)
                {
                    await AddTransaction("Bejövő", TransactionAmount, TransactionNote);
                    await RefreshDataAsync();
                    ClearInputFields();
                    await Application.Current.MainPage.DisplayAlert("Alert", "Money recived successfully", "OK");
                }
            });
        }

        private async Task AddTransaction(string type, int amount, string note)
        {
            var transaction = new Transaction
            {
                Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Type = type,
                Amount = amount,
                Note = note
            };

            await _transactionDatabase.SaveTransactionAsync(transaction);
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

        public void CalculateBalance()
        {
            var transactions = Transactions;
            var balance = transactions.Where(t => t.Type == "Bejövő").Sum(t => t.Amount) -
                          transactions.Where(t => t.Type == "Kimenő").Sum(t => t.Amount);
            TotalBalance = $"{balance} HUF";
        }

        private async Task RefreshDataAsync()
        {
            await LoadTransactionsAsync();
            CalculateBalance();
        }

        private void ClearInputFields()
        {
            TransactionAmount = 0;
            TransactionNote = string.Empty;
        }
    }
}
