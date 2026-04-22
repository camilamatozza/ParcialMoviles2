using PaisesApp.ViewModels;

namespace PaisesApp
{
    public partial class DetallePage : ContentPage
    {
        public DetallePage()
        {
            InitializeComponent();
            BindingContext = new DetalleViewModel();
        }

        private async void OnVolverClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}