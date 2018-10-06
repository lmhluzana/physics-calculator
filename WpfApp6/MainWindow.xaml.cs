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
using ThinkLib;

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        graphic myW;
        Simple_Calculator myW2;
        Sci_Calculator myW3;

        public MainWindow()
        {
            InitializeComponent();
            myW = new graphic();
            myW2 = new Simple_Calculator();
            myW3 = new Sci_Calculator();
        }

        private void Graph_Calculator_Click(object sender, RoutedEventArgs e)
        {
            myW.Show();
            main1.Hide();
        }

        private void Sci_Calculator_Click(object sender, RoutedEventArgs e)
        {
            myW3.Show();
            main1.Hide();
        }

        private void Simp_Calculator_Click(object sender, RoutedEventArgs e)
        {
            myW2.Show();
            main1.Hide();
        }
    }

    public partial class SciCalcWindow : Window
    {

    }
}
