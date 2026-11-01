using System.Windows;
using CrazyUniverse.WPF.ViewModels;

namespace CrazyUniverse.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void Simulations_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel vm)
            {
                SimulationWindow window = new SimulationWindow(vm.GetAllRobots());
                window.Owner = this;
                window.ShowDialog();
            }
        }
    }
}
