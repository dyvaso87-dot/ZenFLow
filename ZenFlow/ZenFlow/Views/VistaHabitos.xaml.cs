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
            ListaHabitos.ItemsSource = null;
            ListaHabitos.ItemsSource = App.GestorHabitos.ObtenerTodos();
        }

        private void AgregarHabito_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtHabito.Text)) return;
            App.GestorHabitos.AgregarHabito(TxtHabito.Text);
            TxtHabito.Clear();
            Cargar();
        }

        private void Cumplir_Click(object sender, RoutedEventArgs e)
        {
            var id = (int)((Button)sender).Tag;
            App.GestorHabitos.RegistrarCumplimiento(id);
            Cargar();
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            var id = (int)((Button)sender).Tag;
            var resultado = MessageBox.Show(
                "¿Seguro que quieres eliminar este hábito?",
                "Eliminar hábito",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                App.GestorHabitos.EliminarHabito(id);
                Cargar();
            }
        }
    }
}