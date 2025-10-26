using ImageWizardUI.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageWizardUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel m_vm;

        public MainWindow()
        {
            InitializeComponent();

            m_vm = new MainWindowViewModel();

            this.DataContext = m_vm;

            m_vm.Dispatcher = this.Dispatcher;
        }
    }
}