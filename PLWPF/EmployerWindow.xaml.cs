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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for EmployerWindow.xaml
    /// </summary>
    public partial class EmployerWindow : Window
    {
        BE.Employer employer;
        BL.IBL bl;

        public EmployerWindow()
        {
            InitializeComponent();
            EmployerCompanyNum.LostFocus += EmployerCompanyNum_LostFocus;
            employer = new BE.Employer();
            this.DataContext = employer;
            bl = BL.FactoryBl.GetBL();
            this.Domain.ItemsSource = Enum.GetValues(typeof(BE.Domain));
        }

        private void EmployerCompanyNum_LostFocus(object sender, RoutedEventArgs e)
        {
            BE.Employer t = bl.GetEmployer(employer.CompanyNum);
            if (t != null)
                employer = t;
            start.Text = employer.Start;
            DataContext = employer;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                employer.Start = (string)start.Text;
                employer.PhoneNumber = employer.Start + employer.End;
                bl.addEmployer(employer);
                employer = new BE.Employer();
                this.DataContext = employer;
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
                bl.removeEmployer(employer);
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
                employer.Start = (string)start.Text;
                employer.PhoneNumber = employer.Start + employer.End;
                bl.updateEmployer(employer);
                employer = new BE.Employer();
                this.DataContext = employer;
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
                this.employerImage.Source = new BitmapImage(new Uri(f.FileName));

            }

        }
    }
    
}
