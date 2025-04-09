using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;

namespace RepasoBerny.Mobile.Pages
{
    public partial class Offline : ContentPage
    {
        private Dictionary<string, bool> _originalValues;

        public Offline()
        {
            InitializeComponent();

            // Carga los valores guardados por el usuario
            LoadSavedSettings();

            // Guarda los valores originales
            StoreOriginalValues();

            // Aplica los colores personalizados
            ApplySwitchColors();

            // Suscribirse a eventos
            WifiSwitch.Toggled += OnSwitchToggled;
            AutoSyncSwitch.Toggled += OnSwitchToggled;
            NotifySwitch.Toggled += OnSwitchToggled;
            RequestIdSwitch.Toggled += OnSwitchToggled;
            FingerprintSwitch.Toggled += OnSwitchToggled;
        }

        private void ApplySwitchColors()
        {
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

        private void LoadSavedSettings()
        {
            WifiSwitch.IsToggled = Preferences.Get(nameof(WifiSwitch), false);
            AutoSyncSwitch.IsToggled = Preferences.Get(nameof(AutoSyncSwitch), false);
            NotifySwitch.IsToggled = Preferences.Get(nameof(NotifySwitch), false);
            RequestIdSwitch.IsToggled = Preferences.Get(nameof(RequestIdSwitch), false);
            FingerprintSwitch.IsToggled = Preferences.Get(nameof(FingerprintSwitch), false);
        }

        private void OnSwitchToggled(object sender, ToggledEventArgs e)
        {
            if (sender is Switch switchControl)
            {
                // Reaplica los colores personalizados
                switchControl.OnColor = Color.FromArgb("#00AEEF");
                switchControl.ThumbColor = Colors.White;

                // Guarda la preferencia del usuario
                Preferences.Set(switchControl.StyleId ?? switchControl.AutomationId ?? switchControl.ClassId ?? GetSwitchKey(switchControl), e.Value);
            }

            CheckForChanges();
        }

        private string GetSwitchKey(Switch switchControl)
        {
            if (switchControl == WifiSwitch) return nameof(WifiSwitch);
            if (switchControl == AutoSyncSwitch) return nameof(AutoSyncSwitch);
            if (switchControl == NotifySwitch) return nameof(NotifySwitch);
            if (switchControl == RequestIdSwitch) return nameof(RequestIdSwitch);
            if (switchControl == FingerprintSwitch) return nameof(FingerprintSwitch);
            return "UnknownSwitch";
        }

        private void CheckForChanges()
        {
            bool hasChanges =
                WifiSwitch.IsToggled != _originalValues[nameof(WifiSwitch)] ||
                AutoSyncSwitch.IsToggled != _originalValues[nameof(AutoSyncSwitch)] ||
                NotifySwitch.IsToggled != _originalValues[nameof(NotifySwitch)] ||
                RequestIdSwitch.IsToggled != _originalValues[nameof(RequestIdSwitch)] ||
                FingerprintSwitch.IsToggled != _originalValues[nameof(FingerprintSwitch)];

            RevertButton.IsEnabled = hasChanges;
            RevertButton.Opacity = hasChanges ? 1.0 : 0.5;
        }

        private async void OnRevertButtonClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Revertir cambios",
                "¿Estás seguro de que deseas revertir todos los cambios?",
                "Sí", "No");

            if (answer)
            {
                WifiSwitch.IsToggled = _originalValues[nameof(WifiSwitch)];
                AutoSyncSwitch.IsToggled = _originalValues[nameof(AutoSyncSwitch)];
                NotifySwitch.IsToggled = _originalValues[nameof(NotifySwitch)];
                RequestIdSwitch.IsToggled = _originalValues[nameof(RequestIdSwitch)];
                FingerprintSwitch.IsToggled = _originalValues[nameof(FingerprintSwitch)];

                // Elimina las preferencias guardadas
                Preferences.Remove(nameof(WifiSwitch));
                Preferences.Remove(nameof(AutoSyncSwitch));
                Preferences.Remove(nameof(NotifySwitch));
                Preferences.Remove(nameof(RequestIdSwitch));
                Preferences.Remove(nameof(FingerprintSwitch));

                ApplySwitchColors();

                await DisplayAlert("Confirmación", "Los cambios han sido revertidos", "OK");

                CheckForChanges();
            }
        }
    }
}
