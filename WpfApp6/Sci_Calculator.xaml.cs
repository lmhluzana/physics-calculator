﻿using System;
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
using ThinkLib;

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for Sci_Calculator.xaml
    /// </summary>
    public partial class Sci_Calculator : Window
    {
        public Sci_Calculator()
        {
            InitializeComponent();
        }
        
        decimal pi = 3.14159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214808651328230664709384460955058223172535940812848111745028410270193852110555964462294895493038196442881097566593344612847564823378678316527120190914m;
        decimal rt2 = 1.41421356237309504880168872420969807856967187537694807317667973799073247846210703885038753432764157273501384623091229702492483605585073721264412149709993583141322266592750559275579995050115278206057147010955997160597027453459686201472851m;
        decimal rt3 = 1.73205080756887729352744634150587236694280525381038062805580697945193301690880003708114618675724857567562614141540670302996994509499895247881165551209437364852809323190230558206797482010108467492326501531234326690332288665067225466892183m;
        decimal euler = 2.718281828459045235360287471352662497757247093699959574966967627724076630353m;
        bool specCase = false;
        bool rad = true;
        
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

        public double Absolute(double num)
        {
            num = (num < 0) ? -1 * num : num;
            return num;
        }
        public decimal Absolute(decimal num)
        {
            num = (num < 0) ? -1 * num : num;
            return num;
        }
        public int Absolute(int num)
        {
            num = (num < 0) ? -1 * num : num;
            return num;
        }

        /// <summary>
        /// Exponent Function
        /// </summary>
        /// <param name="baseNum"> Value of base </param>
        /// <param name="exp"> Exponent </param>
        /// <returns> Base raised to exponent </returns>
        public double exponent(decimal baseNum, decimal exp)
        {
            bool pos = (exp >= 0) ? true : false;
            exp = Absolute(exp);
            decimal dcRes = 1;
            if (exp >= 1)
            {
                for (int b = 0; b < exp; b++)
                {
                    dcRes = dcRes * baseNum;
                }
            }
            else if (exp > 0)
            {
                decimal X = 0,
                        prevX = (decimal)(Math.Sqrt((double)baseNum)) + baseNum / 2;
                while (true)
                {
                    X = (prevX + baseNum/((decimal)exponent(prevX, (int)(1/exp - 1))))/2;
                    if ((prevX - X) < 0.00000000000001m) { break; }
                    prevX = X;
                }
                return Convert.ToDouble(X);
            }
            else { return 0; }
            if (pos) return Convert.ToDouble(dcRes);
            return 1 / Convert.ToDouble(dcRes);
        }

        public double exponent(int baseNum, int exp)
        {
            bool pos = (exp >= 0) ? true : false;
            exp = Absolute(exp);
            double dbRes = 1;
            if (exp >= 1)
            {
                for (int b = 0; b < exp; b++)
                {
                    dbRes = dbRes * baseNum;
                }
            }
            else { return 1; }
            if (pos) return Convert.ToDouble(dbRes);
            return 1 / Convert.ToDouble(dbRes);
        }
        
        //public double approx(double baseNum)
        //{
        //    List<Tuple<double>> roots = new List<Tuple<double>>(,);
        //}

        /// <summary>
        /// Log Function
        /// </summary>
        /// <param name="b"> Base </param>
        /// <param name="n"> Number </param>
        /// <returns></returns>
        public double logarithms(double b, double n)
        {
            double res = n - 1;
            try
            {
                if (b == 1 || b == 0) throw new InvalidOperationException();
                if (!(Absolute((decimal)b - euler) < 0.0000000000001m)) { return logarithms((double)euler, n) / logarithms((double)euler, b); };
                
                for (int i = 2; i < 40; i++)
                {
                    res = res + (exponent(-1, i - 1) / i) * exponent((decimal)(n - 1), i);
                }
            }
            catch (InvalidOperationException ex) { MessageBox.Show("The base of a log cannot equal 1 or 0 \n" + ex.ToString(), "Maths Error"); }
            return res;
        }

        public decimal logarithms(decimal b, decimal n)
        {
            decimal min, mid, max;
            int len;

            for (int i = 0; i < 40; i++)
            {

            }

            //decimal res = 0, 
            //        res1 = 0,
            //        prevN = (decimal)(Math.Log((double)n)) - 1;
            //while (true)
            //{
            //    prevN = ((decimal)exponent(b, prevN) + n)/ 2;
            //    //if ((n - res) < 0) {  prevR }
            //    prevR = res;
            //}
            //decimal res1 = n - 1;
            //decimal res = 0;
            //try
            //{
            //    if (!(b == euler)) { return logarithms(euler, n) / logarithms(euler, b); };

            //    for (int i = 2; i < 40; i++)
            //    {
            //        res1 = (decimal)(exponent(-1, i - 1) * exponent((n - 1), i) / i);
            //        res += res1;
            //    }
            //}
            //catch (InvalidOperationException ex) { MessageBox.Show("The base of a log cannot equal 1 or 0 \n" + ex.ToString(), "Maths Error"); }
            return null;
        }

        /// <summary>
        /// Trigonometry base method
        /// </summary>
        /// <param name="funct"> Trig function </param>
        /// <param name="theta"> Angle </param>
        /// <returns> Value of function at given angle </returns>
        public double trigonometry(string funct, decimal theta)
        {
            decimal[] trigSpecCase = { 0, pi / 2, pi, (3 / 4) * pi, 2 * pi };
            if (rad == false) { theta = theta * (pi / 180); }
            double trigRes = 0;
            if (trigSpecCase.Contains(theta)) { specCase = true; }

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

        public double Sin(decimal theta)
        {
            if (specCase == true)
            {
                decimal[] sinSpecCase = { 0m, 0.5m, rt2 / 2, rt3 / 2, 1 };
                int i = Convert.ToInt32((theta / pi) * 4);
                return Convert.ToDouble(sinSpecCase[i]);
            }
            int len = 50;
            int n;
            decimal[] lsSinRes = new decimal[len];
            double dbSinRes = 0;
            int[] exp = new int[len];
            for (int num = 0; num < len; num++) { exp[num] = 2 * num + 1; };
            foreach (int x in exp)
            {
                n = x / 2;
                lsSinRes[n] = Convert.ToDecimal(exponent(-1, n) * exponent(theta, x) / factorial(x));
            }
            dbSinRes += Convert.ToDouble(lsSinRes.Sum());
            return dbSinRes;
        }
        public double Arcsin(decimal theta)
        {

            return (Sin(theta));
        }
        public double Cosec(decimal theta)
        {
            try
            {
                return 1 / Sin(theta);
            }
            catch (DivideByZeroException e) { MessageBox.Show(e.ToString(), "Maths Error"); }
            return -1;
        }

        public double Cos(decimal theta)
        {
            int len = 50;
            int n;
            double cosRes = 0;
            int[] exp = new int[len];
            for (int num = 0; num < len; num++) { exp[num] = 2 * num; };
            foreach (int x in exp)
            {
                n = x / 2;
                cosRes += exponent(-1, n) * exponent(theta, x) / factorial(x);
            }
            return cosRes;
        }
        public double Arccos(decimal theta) { return (Cos(theta)); }
        public double Sec(decimal theta)
        {
            try
            {
                return 1 / Cos(theta);
            }
            catch (DivideByZeroException e) { MessageBox.Show(e.ToString(), "Maths Error"); }
            return -1;
        }

        public double Tan(decimal theta)
        {
            try
            {
                return Sin(theta) / Cos(theta);
            }
            catch (DivideByZeroException e) { MessageBox.Show(e.ToString(), "Maths Error"); }
            return -1;
        }
        public double Arctan(decimal theta) { return (Tan(theta)); }
        public double Cot(decimal theta) { return 1 / Tan(theta); }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            
            // Sin tests
            Tester.TestEq(Sin(0), 0);
            Tester.TestEq(Sin(pi / 2), 1.0);
            // Cos tests
            Tester.TestEq(Cos(0), 1);
            Tester.TestEq(Cos(pi / 2), 0);
            // Tan Tests
            Tester.TestEq(Tan(0), 0);
            Tester.TestEq(Cos(pi / 2), -1);
            //Factorial Tests
            Tester.TestEq(factorial(0), 1);
            Tester.TestEq(factorial(1), 1);
            Tester.TestEq(factorial(4), 24); 
            // Log tests
            Tester.TestEq(logarithms(2m, 4m), 2);
            Tester.TestEq(logarithms(2m, 64m), 6);
            Tester.TestEq(logarithms(3m, 81m), 4);
            Tester.TestEq(logarithms(2m, 2m), 1);
            Tester.TestEq(logarithms(1m, 1m), -69);
            Tester.TestEq(logarithms(0.01m, 10000m), -4); 
            // Exponent Tests
            Tester.TestEq(exponent(1, 1), 1);
            Tester.TestEq(exponent(1, 0), 1);
            Tester.TestEq(exponent(0, 1), 0);
            // Roots
            Tester.TestEq(exponent(16m, 1/2m), 4);
            Tester.TestEq(exponent(9m, 1/2m), 3);
            Tester.TestEq(exponent(27m, 1/3m), 3);
            Tester.TestEq(exponent(64m, 1 / 3m), 4);
            Tester.TestEq(exponent(1000m, 1 / 3m), 10);
            Tester.TestEq(exponent(16m, 1 / 4m), 2);


        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtInp.Clear();
        }
    }
}
