using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace RepasoBerny.Mobile.Pages
{
    public partial class Offline : ContentPage
    {
        // Valores originales de los switches
        private Dictionary<string, bool> _originalValues;

        public Offline()
        {
            InitializeComponent();

            // Guarda los valores originales cuando se carga la página
            StoreOriginalValues();

            // Aplica los colores personalizados inicialmente
            ApplySwitchColors();

            // Suscribirse a eventos de cambio para cada switch
            WifiSwitch.Toggled += OnSwitchToggled;
            AutoSyncSwitch.Toggled += OnSwitchToggled;
            NotifySwitch.Toggled += OnSwitchToggled;
            RequestIdSwitch.Toggled += OnSwitchToggled;
            FingerprintSwitch.Toggled += OnSwitchToggled;
        }

        private void ApplySwitchColors()
        {
            // Aplica colores a todos los switches
            Color switchOnColor = Color.FromArgb("#00AEEF");
            Color thumbColor = Colors.White;

            WifiSwitch.OnColor = switchOnColor;
            WifiSwitch.ThumbColor = thumbColor;

            AutoSyncSwitch.OnColor = switchOnColor;
            AutoSyncSwitch.ThumbColor = thumbColor;

            NotifySwitch.OnColor = switchOnColor;
            NotifySwitch.ThumbColor = thumbColor;

            RequestIdSwitch.OnColor = switchOnColor;
            RequestIdSwitch.ThumbColor = thumbColor;

            FingerprintSwitch.OnColor = switchOnColor;
            FingerprintSwitch.ThumbColor = thumbColor;
        }

        private void StoreOriginalValues()
        {
            _originalValues = new Dictionary<string, bool>
            {
                { nameof(WifiSwitch), WifiSwitch.IsToggled },
                { nameof(AutoSyncSwitch), AutoSyncSwitch.IsToggled },
                { nameof(NotifySwitch), NotifySwitch.IsToggled },
                { nameof(RequestIdSwitch), RequestIdSwitch.IsToggled },
                { nameof(FingerprintSwitch), FingerprintSwitch.IsToggled }
            };
        }

        private void OnSwitchToggled(object sender, ToggledEventArgs e)
        {
            // Cuando cualquier switch cambia, nos aseguramos de mantener los colores personalizados
            if (sender is Switch switchControl)
            {
                // Reaplica los colores personalizados
                switchControl.OnColor = Color.FromArgb("#00AEEF");
                switchControl.ThumbColor = Colors.White;
            }

            // Verifica si hay cambios para actualizar el estado del botón
            CheckForChanges();
        }

        private void CheckForChanges()
        {
            bool hasChanges =
                WifiSwitch.IsToggled != _originalValues[nameof(WifiSwitch)] ||
                AutoSyncSwitch.IsToggled != _originalValues[nameof(AutoSyncSwitch)] ||
                NotifySwitch.IsToggled != _originalValues[nameof(NotifySwitch)] ||
                RequestIdSwitch.IsToggled != _originalValues[nameof(RequestIdSwitch)] ||
                FingerprintSwitch.IsToggled != _originalValues[nameof(FingerprintSwitch)];

            // Opcional: Puedes cambiar la apariencia del botón según si hay cambios o no
            RevertButton.IsEnabled = hasChanges;
            RevertButton.Opacity = hasChanges ? 1.0 : 0.5;
        }

        private async void OnRevertButtonClicked(object sender, EventArgs e)
        {
            // Pregunta al usuario si realmente quiere revertir los cambios
            bool answer = await DisplayAlert("Revertir cambios",
                "¿Estás seguro de que deseas revertir todos los cambios?",
                "Sí", "No");

            if (answer)
            {
                // Revierte a los valores originales
                WifiSwitch.IsToggled = _originalValues[nameof(WifiSwitch)];
                AutoSyncSwitch.IsToggled = _originalValues[nameof(AutoSyncSwitch)];
                NotifySwitch.IsToggled = _originalValues[nameof(NotifySwitch)];
                RequestIdSwitch.IsToggled = _originalValues[nameof(RequestIdSwitch)];
                FingerprintSwitch.IsToggled = _originalValues[nameof(FingerprintSwitch)];

                // Reasegura que los colores se mantengan
                ApplySwitchColors();

                // Opcional: Muestra un mensaje de confirmación
                await DisplayAlert("Confirmación", "Los cambios han sido revertidos", "OK");

                // Actualiza el estado del botón
                CheckForChanges();
            }
        }
    }
}