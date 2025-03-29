namespace RepasoBerny.Mobile.Pages;

public partial class Register : ContentPage
{
	public Register()
	{
		InitializeComponent();
	}

    private async void OnScreenTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new Seguimiento()); 
    }

}