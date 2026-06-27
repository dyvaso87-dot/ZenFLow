using System.Windows;
using ZenFlow.Datos;
using ZenFlow.Logica;

namespace ZenFlow
{
    public partial class App : Application
    {
        public static GestorTareas GestorTareas { get; private set; }
        public static GestorHabitos GestorHabitos { get; private set; }
        public static MotorEnfoque MotorEnfoque { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            GestorTareas = new GestorTareas(new TareaRepoJson());
            GestorHabitos = new GestorHabitos(new HabitoRepoJson());
            MotorEnfoque = new MotorEnfoque();

            // Abrir la ventana principal manualmente
            new MainWindow().Show();
        }
    }
}