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

            // Singleton aplicado — no usamos new, usamos .Instancia
            GestorTareas = new GestorTareas(TareaRepoJson.Instancia);
            GestorHabitos = new GestorHabitos(HabitoRepoJson.Instancia);
            MotorEnfoque = new MotorEnfoque();

            new MainWindow().Show();
        }
    }
}