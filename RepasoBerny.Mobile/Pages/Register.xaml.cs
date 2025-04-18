using RepasoBerny.Mobile.ViewModel;
namespace RepasoBerny.Mobile.Pages;

public partial class Register : ContentPage
{
	public Register()
	{
		InitializeComponent();
        BindingContext = new RegistroViewModel();
    }
    private void PhoneEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;
        string text = entry.Text.Replace("+", "");

        if (text.Length > 4)
            text = text.Substring(0, 4);
        entry.Text = "+" + text;
    }
}