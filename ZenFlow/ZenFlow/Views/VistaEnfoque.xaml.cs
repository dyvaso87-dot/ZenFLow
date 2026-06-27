using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ZenFlow.Views
{
    public partial class VistaEnfoque : Page
    {
        private ObservableCollection<string> _appsCustom = new();

        public VistaEnfoque()
        {
            InitializeComponent();
            ListaAppsCustom.ItemsSource = _appsCustom;

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
                MessageBox.Show("¡Sesión completada! Buen trabajo, Dylan. 🎉",
                    "ZenFlow", MessageBoxButton.OK, MessageBoxImage.Information);
            };
        }

        private List<string> ObtenerAppsSeleccionadas()
        {
            var apps = new List<string>();
            if (ChkChrome.IsChecked == true) apps.Add("chrome");
            if (ChkSteam.IsChecked == true) apps.Add("steam");
            if (ChkSpotify.IsChecked == true) apps.Add("spotify");
            if (ChkDiscord.IsChecked == true) apps.Add("discord");
            if (ChkYoutube.IsChecked == true) apps.Add("msedge");
            if (ChkWhatsapp.IsChecked == true) apps.Add("whatsapp");
            apps.AddRange(_appsCustom);
            return apps;
        }

        private void Iniciar_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TxtMinutos.Text, out int minutos) || minutos <= 0)
            {
                MessageBox.Show("Escribe un número válido de minutos.");
                return;
            }

            var apps = ObtenerAppsSeleccionadas();
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

        private void AgregarAppCustom_Click(object sender, RoutedEventArgs e)
        {
            var app = TxtAppCustom.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(app) || _appsCustom.Contains(app)) return;
            _appsCustom.Add(app);
            TxtAppCustom.Clear();
        }

        private void QuitarAppCustom_Click(object sender, RoutedEventArgs e)
        {
            var app = ((Button)sender).Tag.ToString();
            _appsCustom.Remove(app);
        }
    }
}