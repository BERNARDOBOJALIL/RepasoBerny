using RepasoBerny.Mobile.ViewModels;

namespace RepasoBerny.Mobile.Pages
{
    public partial class ReportesAnalisis : ContentPage
    {
        public ReportesAnalisis()
        {
            InitializeComponent();
            BindingContext = new ReportesAnalisisViewModel();
        }
    }
}
