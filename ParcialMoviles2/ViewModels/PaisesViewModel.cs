using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PaisesApp.Models;
using PaisesApp.Services;

namespace PaisesApp.ViewModels;

public partial class PaisesViewModel : ObservableObject
{
    private readonly IPaisService _service;

    public ObservableCollection<Pais> Paises { get; set; }
        = new ObservableCollection<Pais>();

    [ObservableProperty]
    private string _mensaje = "Tocá el botón para cargar los países";

    [ObservableProperty]
    private bool _cargando;

    public PaisesViewModel(IPaisService service)
    {
        _service = service;
    }

    [RelayCommand]
    private async Task CargarPaises()
    {
        if (Cargando) return;

        try
        {
            Cargando = true;
            Mensaje = "⏳ Cargando países...";
            Paises.Clear();

            var lista = await _service.GetPaisesAsync();

            foreach (var p in lista)
                Paises.Add(p);

            Mensaje = $"✅ {lista.Count} países cargados";
        }
        catch (HttpRequestException)
        {
            Mensaje = "❌ Sin conexión o error del servidor. Intentá de nuevo.";
        }
        catch (Exception)
        {
            Mensaje = "❌ Ocurrió un error inesperado. Intentá de nuevo.";
        }
        finally
        {
            Cargando = false;
        }
    }
}