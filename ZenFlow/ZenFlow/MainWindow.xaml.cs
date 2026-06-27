using System.Windows;
using System.Windows.Controls;

namespace ZenFlow
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Views.VistaTareas());
        }

        private void NavClick(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Tag.ToString();
            MainFrame.Navigate(tag switch
            {
                "Tareas" => (object)new Views.VistaTareas(),
                "Enfoque" => new Views.VistaEnfoque(),
                "Habitos" => new Views.VistaHabitos(),
                _ => new Views.VistaTareas()
            });
        }
    }
}