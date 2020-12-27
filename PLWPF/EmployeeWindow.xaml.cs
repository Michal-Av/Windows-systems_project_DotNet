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
using System.Windows.Shapes;
using System.ComponentModel;
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        BE.Employee employee;
        BL.IBL bl;
        private readonly BackgroundWorker worker = new BackgroundWorker();
        IEnumerable<IGrouping<string, IGrouping<string, ATM>>> banks;

        public EmployeeWindow()
        {
            InitializeComponent();
            EmployeeId.LostFocus += EmployeeId_LostFocus;
            employee = new BE.Employee();
            this.DataContext = employee;
            bl = BL.FactoryBl.GetBL();
            employee.Date = DateTime.Now;
            this.Education.ItemsSource = Enum.GetValues(typeof(BE.education));
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            this.Special.ItemsSource = bl.GetAllSpecialily();
        }

        private void EmployeeId_LostFocus(object sender, RoutedEventArgs e)
        {
            BE.Employee t = bl.GetEmployee(employee.Id);
            if (t != null)
                employee = t;
            start.Text = employee.Start;
            Special.Text = employee.NumSpecialization;

            DataContext = employee;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                employee.Start = (string)start.Text;
                employee.PhoneNumber = employee.Start + employee.End;
                bl.addEmployee(employee);
                employee = new BE.Employee();
                this.DataContext = employee;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.removeEmployee(employee);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.updateEmployee(employee);
                employee = new BE.Employee();
                this.DataContext = employee;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void changeImageButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog f = new Microsoft.Win32.OpenFileDialog();
            f.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if (f.ShowDialog() == true)
            {
                this.employeeImage.Source = new BitmapImage(new Uri(f.FileName));

            }

        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.bankComboBox.ItemsSource = banks;
            this.bankComboBox.DisplayMemberPath = "Key";
            this.refreshButton.IsEnabled = true;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            banks = Bl_imp.GetAllAtmGroupByBankAndCity().ToList();
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            this.refreshButton.IsEnabled = false;
            worker.RunWorkerAsync();
        }
    }
}
