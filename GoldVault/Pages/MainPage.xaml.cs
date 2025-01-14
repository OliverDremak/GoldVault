using GoldVault.ViewModels;

namespace GoldVault.Pages
{
    public partial class MainPage : ContentPage
    {

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            this.BindingContext = vm;
        }
        //Nem tudom hogyan lehetne viewmodelbe megcsinalni
        private void HideKeyboard()
        {
            MoneyEntry.Unfocus();
            NoteEntry.Unfocus();
        }

        private void OnSendMoneyClicked(object sender, EventArgs e)
        {
            HideKeyboard();
        }

        private void OnReceiveMoneyClicked(object sender, EventArgs e)
        {
            HideKeyboard();
        }

    }

}
