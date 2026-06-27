using System.Windows;
using System.Windows.Controls;

namespace ZenFlow.Views
{
    public partial class VistaEnfoque : Page
    {
        public VistaEnfoque()
        {
            InitializeComponent();
            App.MotorEnfoque.TiempoActualizado += seg =>
            {
                TxtTimer.Text = $"{seg / 60:D2}:{seg % 60:D2}";
            };
            App.MotorEnfoque.SesionTerminada += () =>
            {
                MessageBox.Show("¡Sesión completada! Buen trabajo, Dylan.");
                TxtTimer.Text = "25:00";
            };
        }

        private void Iniciar_Click(object sender, RoutedEventArgs e)
        {
            var apps = TxtApps.Text.Split(',')
                .Select(a => a.Trim())
                .Where(a => !string.IsNullOrEmpty(a))
                .ToList();
            App.MotorEnfoque.IniciarSesion(25, apps);
        }

        private void Pausar_Click(object sender, RoutedEventArgs e)
            => App.MotorEnfoque.Pausar();

        private void Terminar_Click(object sender, RoutedEventArgs e)
            => App.MotorEnfoque.Terminar();
    }
}