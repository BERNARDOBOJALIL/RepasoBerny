using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepasoBerny.Mobile.ViewModel
{
    partial class ViewModelTest:ObservableObject
    {
        [ObservableProperty]
        public string text;
        [ObservableProperty]
        int count;
        [RelayCommand]
        public void CambiarTexto()
        {
            Count++;
            Text = "Hola Mundo";
        }
    }
}
