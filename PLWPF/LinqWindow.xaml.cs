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
    /// Interaction logic for LinqWindow.xaml
    /// </summary>
    public partial class LinqWindow : Window
    {
        BL.IBL bl;
        public LinqWindow()
        {
            InitializeComponent();
            bl = BL.FactoryBl.GetBL();
        }

        private void GetAllEmployer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AllEmployersUserControl uc = new AllEmployersUserControl();
                uc.Source = bl.GetAllEmployerLastName();
                this.page.Content = uc;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void GroupContractByCity_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GroupContractByCityUserControl uc = new GroupContractByCityUserControl();
                uc.Source = bl.GroupContractByCity();
                this.page.Content = uc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void GetAllFarEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AllFarEmployeesUserControl uc = new AllFarEmployeesUserControl();
                uc.Source = bl.GetAllFarEmployee();
                this.page.Content = uc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
