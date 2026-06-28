using System.Windows;
using System.Windows.Controls;

namespace ZenFlow.Views
{
    public partial class VistaEnfoque : Page
    {
        public VistaEnfoque()
        {
            InitializeComponent();
            CargarApps();

            App.MotorEnfoque.TiempoActualizado += seg =>
            {
                TxtTimer.Text = $"{seg / 60:D2}:{seg % 60:D2}";
            };

            App.MotorEnfoque.SesionTerminada += () =>
            {
                TxtEstado.Text = "Listo";
                BtnIniciar.IsEnabled = true;
                var min = int.TryParse(TxtMinutos.Text, out var m) ? m : 25;
                TxtTimer.Text = $"{min:D2}:00";
                MessageBox.Show("¡Sesión completada! Buen trabajo Dylan 🎉",
                    "ZenFlow", MessageBoxButton.OK, MessageBoxImage.Information);
            };
        }

        private void CargarApps()
        {
            ListaApps.ItemsSource = null;
            ListaApps.ItemsSource = App.GestorApps.ObtenerTodas();
        }

        private void Toggle_Click(object sender, RoutedEventArgs e)
        {
            var id = (int)((CheckBox)sender).Tag;
            App.GestorApps.ToggleActivar(id);
            CargarApps();
        }

        private void EliminarApp_Click(object sender, RoutedEventArgs e)
        {
            var id = (int)((Button)sender).Tag;
            var r = MessageBox.Show("¿Eliminar esta app de la lista?",
                "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (r == MessageBoxResult.Yes)
            {
                App.GestorApps.Eliminar(id);
                CargarApps();
            }
        }

        private void AgregarApp_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtNombreApp.Text) ||
                string.IsNullOrWhiteSpace(TxtProcesoApp.Text))
            {
                MessageBox.Show("Completa el nombre y el proceso.");
                return;
            }
            App.GestorApps.AgregarApp(TxtNombreApp.Text.Trim(),
                                       TxtProcesoApp.Text.Trim());
            TxtNombreApp.Clear();
            TxtProcesoApp.Clear();
            CargarApps();
        }

        private void Iniciar_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TxtMinutos.Text, out int minutos) || minutos <= 0)
            {
                MessageBox.Show("Escribe un número válido de minutos.");
                return;
            }

            var apps = App.GestorApps.ObtenerProcesosActivos();
            if (apps.Count == 0)
            {
                MessageBox.Show("Activa al menos una app para bloquear.");
                return;
            }

            TxtEstado.Text = "En sesión";
            BtnIniciar.IsEnabled = false;
            App.MotorEnfoque.IniciarSesion(minutos, apps);
        }

        private void Pausar_Click(object sender, RoutedEventArgs e)
        {
            App.MotorEnfoque.Pausar();
            TxtEstado.Text = App.MotorEnfoque.EnSesion ? "En sesión" : "Pausado";
        }

        private void Terminar_Click(object sender, RoutedEventArgs e)
        {
            App.MotorEnfoque.Terminar();
            TxtEstado.Text = "Listo";
            BtnIniciar.IsEnabled = true;
            var min = int.TryParse(TxtMinutos.Text, out var m) ? m : 25;
            TxtTimer.Text = $"{min:D2}:00";
        }

        private void Menos_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(TxtMinutos.Text, out int min) && min > 1)
                TxtMinutos.Text = (min - 1).ToString();
        }

        private void Mas_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(TxtMinutos.Text, out int min) && min < 120)
                TxtMinutos.Text = (min + 1).ToString();
        }

        private void Preset_Click(object sender, RoutedEventArgs e)
        {
            var min = ((Button)sender).Tag.ToString();
            TxtMinutos.Text = min;
            TxtTimer.Text = $"{int.Parse(min):D2}:00";
        }

        private void AgregarAppCustom_Click(object sender, RoutedEventArgs e) { }
        private void QuitarAppCustom_Click(object sender, RoutedEventArgs e) { }
    }
}