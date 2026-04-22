using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PaisesApp.ViewModels
{
    [QueryProperty(nameof(Nombre), "nombre")]
    [QueryProperty(nameof(Capital), "capital")]
    [QueryProperty(nameof(Region), "region")]
    [QueryProperty(nameof(Poblacion), "poblacion")]
    public class DetalleViewModel : INotifyPropertyChanged
    {
        private string? _nombre;
        public string? Nombre
        {
            get => _nombre;
            set { _nombre = value; OnPropertyChanged(); }
        }

        private string? _capital;
        public string? Capital
        {
            get => _capital;
            set { _capital = value; OnPropertyChanged(); }
        }

        private string? _region;
        public string? Region
        {
            get => _region;
            set { _region = value; OnPropertyChanged(); }
        }

        private string? _poblacion;
        public string? Poblacion
        {
            get => _poblacion;
            set { _poblacion = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}