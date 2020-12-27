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
    /// Interaction logic for SpecializationWindow.xaml
    /// </summary>
    public partial class SpecializationWindow : Window
    {
        BE.Specialization sp;
        BL.IBL bl;
        public SpecializationWindow()
        {
            InitializeComponent();
            SExpertNum.LostFocus += SExpertNum_LostFocus;
            sp = new BE.Specialization();
            this.DataContext = sp;
            bl = BL.FactoryBl.GetBL();
            this.SDomain.ItemsSource = Enum.GetValues(typeof(BE.Domain));
        }
        private void SExpertNum_LostFocus(object sender, RoutedEventArgs e)
        {
            BE.Specialization t = bl.GetSpecialization(sp.Expert);
            if (t != null)
                sp = t;
            DataContext = sp;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.addSeciality(sp);
                sp= new BE.Specialization();
                this.DataContext = sp;
                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.removeSeciality(sp);
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
                bl.updateSeciality(sp);
                sp = new BE.Specialization();
                this.DataContext = sp;
                GetBindingExpression(TextBox.TextProperty).UpdateSource();
                Window SpecializationWindow = new SpecializationWindow();
                SpecializationWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
