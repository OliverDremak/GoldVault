using GoldVault.Services;
using GoldVault.ViewModels;

namespace GoldVault
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            Application.Current.UserAppTheme = AppTheme.Light;
        }
    }
}
