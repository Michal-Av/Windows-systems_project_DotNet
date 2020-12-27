//Talia Tsameret 207045956 Michal Avraham 313206419
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Employee_Click(object sender, RoutedEventArgs e)
        {
            Window EmployeeWindow = new EmployeeWindow();
            EmployeeWindow.Show();
        }

        private void Employer_Click(object sender, RoutedEventArgs e)
        {
            Window EmployerWindow = new EmployerWindow();
            EmployerWindow.Show();
        }

        private void Specialization_Click(object sender, RoutedEventArgs e)
        {
            Window SpecializationWindow = new SpecializationWindow();
            SpecializationWindow.Show();
        }

        private void Contract_Click(object sender, RoutedEventArgs e)
        {
            Window ContractWindow = new ContractWindow();
            ContractWindow.Show();
        }

        private void Linq_Click(object sender, RoutedEventArgs e)
        {
            Window LinqWindow = new LinqWindow();
            LinqWindow.Show();
        }
    }
}
