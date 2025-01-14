using GoldVault.ViewModels;

namespace GoldVault.Pages;

public partial class StockPage : ContentPage
{
	public StockPage(StockViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;

    }
}