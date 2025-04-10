using System;
using Microsoft.Maui.Controls;

namespace RepasoBerny.Mobile.Pages
{
    public partial class Survey : ContentPage
    {
        int etapaActual = 1;

        public Survey()
        {
            InitializeComponent();
            ActualizarVista();
        }

        private void ActualizarVista()
        {
            Seccion1.IsVisible = false;
            Seccion2.IsVisible = false;
            Seccion3.IsVisible = false;
            Seccion4.IsVisible = false;

            switch (etapaActual)
            {
                case 1:
                    Seccion1.IsVisible = true;
                    ProgresoBarra.Progress = 0.25;
                    break;
                case 2:
                    Seccion2.IsVisible = true;
                    ProgresoBarra.Progress = 0.50;
                    break;
                case 3:
                    Seccion3.IsVisible = true;
                    ProgresoBarra.Progress = 0.75;
                    break;
                case 4:
                    Seccion4.IsVisible = true;
                    ProgresoBarra.Progress = 1.0;
                    break;
            }
        }

        // Botones de navegaci�n
        private void OnSiguienteSeccion1(object sender, EventArgs e)
        {
            etapaActual = 2;
            ActualizarVista();
        }

        private void OnAnteriorSeccion2(object sender, EventArgs e)
        {
            etapaActual = 1;
            ActualizarVista();
        }

        private void OnSiguienteSeccion2(object sender, EventArgs e)
        {
            etapaActual = 3;
            ActualizarVista();
        }

        private void OnAnteriorSeccion3(object sender, EventArgs e)
        {
            etapaActual = 2;
            ActualizarVista();
        }

        private void OnSiguienteSeccion3(object sender, EventArgs e)
        {
            etapaActual = 4;
            ActualizarVista();
        }

        private void OnAnteriorSeccion4(object sender, EventArgs e)
        {
            etapaActual = 3;
            ActualizarVista();
        }

        // Bot�n para regresar a la p�gina Seguimiento
        private async void OnRegresarClick(object sender, EventArgs e)
        {
            // Cambia esta l�nea si tu navegaci�n funciona diferente (por Shell, por ejemplo)
           await Navigation.PopAsync();

        }

        // L�gica para asegurar que solo se seleccione una opci�n en preguntas "S� / No"
        private void OnUncheckOther(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox changedCheckBox && changedCheckBox.Parent is Layout parentLayout)
            {
                if (e.Value) // Solo act�a si se marc�
                {
                    foreach (var child in parentLayout.Children)
                    {
                        if (child is CheckBox cb && cb != changedCheckBox)
                        {
                            cb.IsChecked = false;
                        }
                    }
                }
            }
        }
    }
}
