using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PaisesApp.Models;
using PaisesApp.Services;

namespace PaisesApp.ViewModels
{
    public class PaisesViewModel : INotifyPropertyChanged
    {
        private readonly PaisService _service;

        public ObservableCollection<Pais> Paises { get; set; }
            = new ObservableCollection<Pais>();

        private string _mensaje = "Tocá el botón para cargar los países";
        public string Mensaje
        {
            get => _mensaje;
            set { _mensaje = value; OnPropertyChanged(); }
        }

        private bool _cargando = false;
        public bool Cargando
        {
            get => _cargando;
            set { _cargando = value; OnPropertyChanged(); }
        }

        public ICommand CargarCommand { get; }

        public PaisesViewModel()
        {
            _service = new PaisService();
            CargarCommand = new Command(async () => await CargarPaises());
        }

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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string nombre = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
    }
}