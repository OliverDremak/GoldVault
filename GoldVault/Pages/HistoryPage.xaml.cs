using GoldVault.ViewModels;

namespace GoldVault.Pages;

public partial class HistoryPage : ContentPage
{
    public HistoryPage(HistoryViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = vm;
    }

}

