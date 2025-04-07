namespace RepasoBerny.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Pages.ReportesAnalisis());
            //MainPage = new AppShell();

        }
    }
}
