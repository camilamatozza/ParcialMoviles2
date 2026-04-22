using PaisesApp.Models;

namespace PaisesApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnPaisTapped(object sender, TappedEventArgs e)
        {
            var border = sender as Border;
            var pais = border?.BindingContext as Pais;

            if (pais != null)
            {
                await Shell.Current.GoToAsync(
                    $"detalle" +
                    $"?nombre={Uri.EscapeDataString(pais.NombreComun)}" +
                    $"&capital={Uri.EscapeDataString(pais.CapitalTexto)}" +
                    $"&region={Uri.EscapeDataString(pais.Region ?? "Sin región")}" +
                    $"&poblacion={Uri.EscapeDataString(pais.PoblacionTexto)}");
            }
        }
    }
}