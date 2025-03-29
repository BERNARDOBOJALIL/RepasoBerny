namespace RepasoBerny.Mobile.Pages;

public partial class Seguimiento : ContentPage
{
	public Seguimiento()
	{
		InitializeComponent();
	}

    private async void OnScreenTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new ReportesAnalisis());
    }
}