using System.Windows;
using System.Windows.Controls;

namespace ZenFlow.Views
{
    public partial class VistaHabitos : Page
    {
        public VistaHabitos()
        {
            InitializeComponent();
            Cargar();
        }

        private void Cargar()
        {
            ListaHabitos.ItemsSource = App.GestorHabitos
                .ObtenerTodos()
                .Select(h => $"{h.Nombre} — racha: {h.RachaDias} días");
        }

        private void AgregarHabito_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtHabito.Text)) return;
            App.GestorHabitos.AgregarHabito(TxtHabito.Text);
            TxtHabito.Clear();
            Cargar();
        }

        private void RegistrarCumplimiento(object sender, SelectionChangedEventArgs e)
        {
            var idx = ListaHabitos.SelectedIndex;
            if (idx < 0) return;
            var habito = App.GestorHabitos.ObtenerTodos()[idx];
            App.GestorHabitos.RegistrarCumplimiento(habito.Id);
            Cargar();
        }
    }
}