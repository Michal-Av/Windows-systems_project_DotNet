﻿using System;
using System.Collections;
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
    /// Interaction logic for AllFarEmployeesUserControl.xaml
    /// </summary>
    public partial class AllFarEmployeesUserControl : UserControl
    {
        private IEnumerable source;
        public IEnumerable Source
        {
            get { return source; }
            set
            {
                source = value;
                listView.ItemsSource = source;
            }
        }
        public AllFarEmployeesUserControl()
        {
            InitializeComponent();
        }
    }
}
