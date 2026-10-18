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
    }
}