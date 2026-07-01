using Microsoft.Maui.Devices.Sensors;

namespace PaisesApp;

public partial class SensoresPage : ContentPage
{
    public SensoresPage()
    {
        InitializeComponent();
    }

    private async void OnTomarFotoClicked(object sender, EventArgs e)
    {
        try
        {
            var status = await Permissions.RequestAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
            {
                await DisplayAlert("Permiso denegado", "La app necesita acceso a la cámara.", "OK");
                return;
            }

            var foto = await MediaPicker.CapturePhotoAsync();
            if (foto != null)
            {
                var stream = await foto.OpenReadAsync();
                FotoPreview.Source = ImageSource.FromStream(() => stream);
                FotoPreview.IsVisible = true;
                Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(200));
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void OnObtenerUbicacionClicked(object sender, EventArgs e)
    {
        try
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                await DisplayAlert("Permiso denegado", "La app necesita acceso a la ubicación.", "OK");
                return;
            }

            LblUbicacion.Text = "⏳ Obteniendo ubicación...";

            var request = new GeolocationRequest(GeolocationAccuracy.Medium,
                TimeSpan.FromSeconds(10));
            var ubicacion = await Geolocation.GetLocationAsync(request);

            if (ubicacion != null)
            {
                LblUbicacion.Text = $"📍 Lat: {ubicacion.Latitude:F4}, Lon: {ubicacion.Longitude:F4}";
                Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(300));
            }
        }
        catch (FeatureNotSupportedException)
        {
            LblUbicacion.Text = "❌ GPS no disponible en este dispositivo.";
        }
        catch (PermissionException)
        {
            LblUbicacion.Text = "❌ Permiso de ubicación denegado.";
        }
        catch (Exception ex)
        {
            LblUbicacion.Text = $"❌ Error: {ex.Message}";
        }
    }

    private void OnVibrarClicked(object sender, EventArgs e)
    {
        Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(500));
    }
}