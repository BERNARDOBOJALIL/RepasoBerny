namespace RepasoBerny.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Pages.Register());
            //MainPage = new AppShell();

        }
    }
}
