using System.Windows;
using System.Windows.Controls;

namespace ZenFlow.Views
{
    public partial class VistaApps : Page
    {
        public VistaApps()
        {
            InitializeComponent();
            Cargar();
        }

        private void Cargar()
        {
            ListaApps.ItemsSource = null;
            ListaApps.ItemsSource = App.GestorApps.ObtenerTodas();
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtNombre.Text) ||
                string.IsNullOrWhiteSpace(TxtProceso.Text))
            {
                MessageBox.Show("Completa el nombre y el proceso de la app.");
                return;
            }

            App.GestorApps.AgregarApp(TxtNombre.Text.Trim(), TxtProceso.Text.Trim());
            TxtNombre.Clear();
            TxtProceso.Clear();
            Cargar();
        }

        private void Toggle_Click(object sender, RoutedEventArgs e)
        {
            var id = (int)((CheckBox)sender).Tag;
            App.GestorApps.ToggleActivar(id);
            Cargar();
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            var id = (int)((Button)sender).Tag;
            var resultado = MessageBox.Show(
                "¿Eliminar esta app de la lista?",
                "Eliminar", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                App.GestorApps.Eliminar(id);
                Cargar();
            }
        }
    }
}