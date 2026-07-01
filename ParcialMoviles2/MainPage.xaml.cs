using PaisesApp.Models;
using PaisesApp.ViewModels;

namespace PaisesApp;

public partial class MainPage : ContentPage
{
    public MainPage(PaisesViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
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

    private async void OnSensoresClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("sensores");
    }
}