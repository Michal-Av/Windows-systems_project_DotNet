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
    /// Interaction logic for ContactWindow.xaml
    /// </summary>
    public partial class ContractWindow : Window
    {
        BE.contract c;
        BL.IBL bl;
        public ContractWindow()
        {
            InitializeComponent();
            contractNum.LostFocus += contractNum_LostFocus;
            c = new BE.contract();
            this.DataContext = c;
            bl = BL.FactoryBl.GetBL();
        }

        private void contractNum_LostFocus(object sender, RoutedEventArgs e)
        {
            BE.contract t = bl.GetContract_Num(c.NumContract);
            if (t != null)
                c= t;
            DataContext = c;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.addContract(c);
                c = new BE.contract();
                this.DataContext = c;
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
                bl.removeContract(c);
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
                bl.updateContract(c);
                c = new BE.contract();
                this.DataContext = c;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
