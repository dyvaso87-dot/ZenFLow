using System.Windows;
using System.Windows.Controls;

namespace ZenFlow.Views
{
    public partial class VistaTareas : Page
    {
        public VistaTareas()
        {
            InitializeComponent();
            Cargar();
        }

        private void Cargar()
        {
            ListaTareas.ItemsSource = App.GestorTareas
                .ObtenerTareasPendientes()
                .Select(t => $"{t.Titulo} — {t.FechaLimite:dd/MM/yyyy}");
        }

        private void AgregarTarea_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtTitulo.Text)) return;
            var fecha = DpFecha.SelectedDate ?? DateTime.Today;
            App.GestorTareas.AgregarTarea(TxtTitulo.Text, fecha);
            TxtTitulo.Clear();
            Cargar();
        }
    }
}