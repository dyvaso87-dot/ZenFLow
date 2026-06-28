using System.Windows;
using ZenFlow.Datos;
using ZenFlow.Logica;

namespace ZenFlow
{
    public partial class App : Application
    {
        public static GestorTareas GestorTareas { get; private set; } = null!;
        public static GestorHabitos GestorHabitos { get; private set; } = null!;
        public static GestorApps GestorApps { get; private set; } = null!;
        public static MotorEnfoque MotorEnfoque { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            // Atrapa cualquier excepción no manejada
            AppDomain.CurrentDomain.UnhandledException += (s, ex) =>
            {
                MessageBox.Show(ex.ExceptionObject.ToString(),
                    "Error al iniciar", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            };

            try
            {
                base.OnStartup(e);

                GestorTareas = new GestorTareas(TareaRepoJson.Instancia);
                GestorHabitos = new GestorHabitos(HabitoRepoJson.Instancia);
                GestorApps = new GestorApps(AppBloqueadaRepoJson.Instancia);
                MotorEnfoque = new MotorEnfoque();

                new MainWindow().Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al iniciar",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}