
using System.Windows;

namespace Butthesda_Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel vm = new ViewModel();

        public MainWindow()
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}
