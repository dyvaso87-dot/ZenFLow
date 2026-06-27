using System;
using System.Windows;
using System.Windows.Controls;

namespace ZenFlow.Views
{
    public partial class VistaTareas : Page
    {
        public VistaTareas()
        {
            InitializeComponent();
            TxtFecha.Text = DateTime.Today.ToString("dddd, dd 'de' MMMM 'de' yyyy",
                new System.Globalization.CultureInfo("es-MX"));
            Cargar();
        }

        private void Cargar()
        {
            ListaTareas.ItemsSource = null;
            ListaTareas.ItemsSource = App.GestorTareas.ObtenerTareasPendientes();
        }

        private void AgregarTarea_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtTitulo.Text)) return;
            var fecha = DpFecha.SelectedDate ?? DateTime.Today;
            App.GestorTareas.AgregarTarea(TxtTitulo.Text, fecha);
            TxtTitulo.Clear();
            DpFecha.SelectedDate = null;
            Cargar();
        }

        private void Completar_Click(object sender, RoutedEventArgs e)
        {
            var id = (int)((Button)sender).Tag;
            App.GestorTareas.CompletarTarea(id);
            Cargar();
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            var id = (int)((Button)sender).Tag;
            var resultado = MessageBox.Show(
                "¿Seguro que quieres eliminar esta tarea?",
                "Eliminar tarea",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                App.GestorTareas.EliminarTarea(id);
                Cargar();
            }
        }
    }
}