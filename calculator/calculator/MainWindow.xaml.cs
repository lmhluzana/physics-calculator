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

namespace calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double pi = 3.1415926;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Exponent Function
        /// </summary>
        /// <param name="baseNum"> Value of base </param>
        /// <param name="exp"> Exponent </param>
        /// <returns> Base raised to exponent </returns>
        public double exponent(double baseNum, double exp)
        {
            bool pos = (exp >= 0) ? true : false;
            exp = (exp >= 0) ? exp : -exp;
            double res = 1;
            for (int b = 0; b < exp; b++) { res = res*baseNum; }
            if (pos) return res;
            return 1 / res;
        }

        /// <summary>
        /// Factorial Function
        /// </summary>
        /// <param name="num"> Base of factorial </param>
        /// <returns> Value of factorial </returns>
        public double factorial(double num)
        {
            double factRes = 1;
            for (int x = 1; x <= num; x++)
            {
                factRes = factRes * x;
            }
            return factRes;
        }

        /*
        public int decP(double num)
        {
            int dec = 1;
            if (num > 1)
            {
                while (num > 10)
                {
                    num = num / 10;
                    dec++;
                }
            }
            return dec;
        }*/

        /// <summary>
        /// Trigonometry base method
        /// </summary>
        /// <param name="funct"> Trig function </param>
        /// <param name="theta"> Angle </param>
        /// <returns> Value of function at given angle </returns>
        public double trigonometry(string funct, double theta)
        {
            theta = theta * (pi / 180);
            double trigRes = 2;
            switch (funct)
            {
                case ("sin"):
                    return Sin(theta);
                case ("cos"):
                    return Cos(theta);
                case ("tan"):
                    return Tan(theta);
                case ("arcsin"):
                    return Arcsin(theta);
                case ("arccos"):
                    return Arccos(theta);
                case ("arctan"):
                    return Arctan(theta);
                case ("cosec"):
                    return Cosec(theta);
                case ("sec"):
                    return Sec(theta);
                case ("cot"):
                    return Cot(theta);
                
            }
            return trigRes;
        }

        public double Sin(double theta)
        {
            int len = 50;
            int n;
            double sinRes = theta;
            int[] exp = new int[len];
            for (int num = 0; num < len; num++) { exp[num] = 2 * num + 1; };
            foreach (int x in exp)
            {
                n = x / 2;
                sinRes += exponent(-1, n) * exponent(theta, x) / factorial(x);
            }
            return sinRes;
        }
        public double Arcsin(double theta) { return (Sin(theta)); }
        public double Cosec(double theta)
        {
            try
            {
                return 1 / Sin(theta);
            }
            catch(DivideByZeroException e) { MessageBox.Show(e.ToString(), "Maths Error"); }
            return -1;
        }

        public double Cos(double theta)
        {
            int len = 50;
            int n;
            double cosRes = theta;
            int[] exp = new int[len];
            for (int num = 0; num < len; num++) { exp[num] = 2 * num; };
            foreach (int x in exp)
            {
                n = x / 2;
                cosRes += exponent(-1, n) * exponent(theta, x) / factorial(x);
            }
            return cosRes;
        }
        public double Arccos(double theta) { return (Cos(theta)); }
        public double Sec(double theta)
        {
            try
            {
                return 1 / Cos(theta);
            }
            catch (DivideByZeroException e) { MessageBox.Show(e.ToString(), "Maths Error"); }
            return -1;
        }

        public double Tan(double theta)
        {
            double tanRes = 2;
            return tanRes;
        }
        public double Arctan(double theta) { return (Tan(theta)); }
        public double Cot(double theta) { return 1 / Tan(theta); }

        private void sinBtn_Click(object sender, RoutedEventArgs e)
        {
            // Sin tests
            Tester.TestEq(Sin(0), 0);
            Tester.TestEq(Sin(3.1415926/2), 1);
            // Cos tests
            Tester.TestEq(Cos(0), 1);
            Tester.TestEq(Cos(3.1415926/2), 0);
        }
    }

}
