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
        
        public decimal pi = 3.14159265358979323846264338327950288419716939937510582097494459230781640628620899m;
        public decimal euler = 2.718281828459045235360287471352662497757247093699959574966967627724076630353m;
        public decimal rt2 = 1.4142135623730950488016887242096980785696718753769480731766797379907324784621070m;
        public decimal rt3 = 1.7320508075688772935274463415058723669428052538103806280558069794519330169088000m;
        public bool specCase = false;
        public bool rad = true;


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

        public decimal reductionForm(decimal theta)
        {
            while (theta > 2*pi) { theta = theta / 2 * 2*pi; }
            return theta;
        }

        /// <summary>
        /// Exponent Function
        /// </summary>
        /// <param name="baseNum"> Value of base </param>
        /// <param name="exp"> Exponent </param>
        /// <returns> Base raised to exponent </returns>
        //public double exponent(decimal baseNum, decimal exp)
        //{
        //    bool pos = (exp >= 0) ? true : false;
        //    exp = Absolute(exp);
        //    decimal dcRes = 1;
        //    try
        //    {
        //        if (exp >= 1)
        //        {
        //            for (int b = 0; b < exp; b++)
        //            {
        //                dcRes = dcRes * baseNum;
        //            }
        //        }
        //        else if (exp > 0)
        //        {
        //            decimal X = 0,
        //                    prevX = (decimal)(Math.Sqrt((double)baseNum)) + baseNum / 2;
        //            while (true)
        //            {
        //                X = (prevX + baseNum/((decimal)exponent(prevX, (int)(1/exp - 1))))/2;
        //                if ((prevX - X) < 0.00000000000001m) { break; }
        //                prevX = X;
        //            }
        //            return Convert.ToDouble(X);
        //        }
        //        else { return 0; }
        //    }
        //    catch (Exception ex) { MessageBox.Show("Maths Error" + ex.ToString(), "Error"); }
        //    if (pos) return Convert.ToDouble(dcRes);
        //    return 1 / Convert.ToDouble(dcRes);
        //}
        //public double exponent(int baseNum, int exp)
        //{
        //    bool pos = (exp >= 0) ? true : false;
        //    exp = Absolute(exp);
        //    double dbRes = 1;
        //    if (exp >= 1)
        //    {
        //        for (int b = 0; b < exp; b++)
        //        {
        //            dbRes = dbRes * baseNum;
        //        }
        //    }
        //    else { return 1; }
        //    if (pos) return Convert.ToDouble(dbRes);
        //    return 1 / Convert.ToDouble(dbRes);
        //}

        /// <summary>
        /// Log Function
        /// </summary>
        /// <param name="b"> Base </param>
        /// <param name="n"> Number </param>
        /// <returns></returns>
        //public double logarithms(double b, double n)
        //{
        //    double res = n - 1;
        //    try
        //    {
        //        if (b == 1 || b == 0) throw new InvalidOperationException();
        //        if (!(Absolute((decimal)b - euler) < 0.0000000000001m)) { return logarithms((double)euler, n) / logarithms((double)euler, b); };
                
        //        for (int i = 2; i < 40; i++)
        //        {
        //            res = res + (exponent(-1, i - 1) / i) * exponent((decimal)(n - 1), i);
        //        }
        //    }
        //    catch (InvalidOperationException ex) { MessageBox.Show("The base of a log cannot equal 1 or 0 \n" + ex.ToString(), "Maths Error"); }
        //    return res;
        //}
        //public decimal logarithms(decimal b, decimal n)
        //{
        //    decimal min = 0, mid, max;
        //    int len;

        //    for (int i = 0; i < 40; i++)
        //    {

        //    }

        //    //decimal res = 0, 
        //    //        res1 = 0,
        //    //        prevN = (decimal)(Math.Log((double)n)) - 1;
        //    //while (true)
        //    //{
        //    //    prevN = ((decimal)exponent(b, prevN) + n)/ 2;
        //    //    //if ((n - res) < 0) {  prevR }
        //    //    prevR = res;
        //    //}
        //    //decimal res1 = n - 1;
        //    //decimal res = 0;
        //    //try
        //    //{
        //    //    if (!(b == euler)) { return logarithms(euler, n) / logarithms(euler, b); };

        //    //    for (int i = 2; i < 40; i++)
        //    //    {
        //    //        res1 = (decimal)(exponent(-1, i - 1) * exponent((n - 1), i) / i);
        //    //        res += res1;
        //    //    }
        //    //}
        //    //catch (InvalidOperationException ex) { MessageBox.Show("The base of a log cannot equal 1 or 0 \n" + ex.ToString(), "Maths Error"); }
        //    return 0;
        //}


        /// <summary>
        /// Trigonometry base method
        /// </summary>
        /// <param name="funct"> Trig function </param>
        /// <param name="theta"> Angle </param>
        /// <returns> Value of function at given angle </returns>
        //public double trigonometry(string funct, decimal theta)
        //{
        //    decimal[] trigSpecCase = { 0, pi / 2, pi, (3 / 4) * pi, 2 * pi };
        //    if (rad == false) { theta = theta * (pi / 180); }
        //    double trigRes = 0;
        //    if (trigSpecCase.Contains(theta)) { specCase = true; }

        //    switch (funct)
        //    {
        //        case ("sin"):
        //            return Sin(theta);
        //        case ("cos"):
        //            return Cos(theta);
        //        case ("tan"):
        //            return Tan(theta);
        //        case ("arcsin"):
        //            return Arcsin(theta);
        //        case ("arccos"):
        //            return Arccos(theta);
        //        case ("arctan"):
        //            return Arctan(theta);
        //        case ("cosec"):
        //            return Cosec(theta);
        //        case ("sec"):
        //            return Sec(theta);
        //        case ("cot"):
        //            return Cot(theta);
        //    }
        //    return trigRes;
        //}

        //public double Sin(decimal theta)
        //{
        //    if (specCase == true)
        //    {
        //        decimal[] sinSpecCase = { 0m, 0.5m, rt2 / 2, rt3 / 2, 1 };
        //        int i = Convert.ToInt32((theta / pi) * 4);
        //        return Convert.ToDouble(sinSpecCase[i]);
        //    }
        //    int len = 50;
        //    int n;
        //    decimal[] lsSinRes = new decimal[len];
        //    double dbSinRes = 0;
        //    double prevR = 0;
        //    int[] exp = new int[len];
        //    for (int num = 0; num < len; num++) { exp[num] = 2 * num + 1; };
        //    foreach (int x in exp)
        //    {
        //        n = x / 2;
        //        dbSinRes += exponent(-1, n) * exponent(theta, x) / factorial(x);
        //        if (dbSinRes == prevR) { break; }
        //        prevR = dbSinRes;
        //    }
        //    //dbSinRes += Convert.ToDouble(lsSinRes.Sum());
        //    return dbSinRes;
        //}
        //public double Arcsin(decimal theta)
        //{
        //    return (Sin(theta));
        //}
        //public double Cosec(decimal theta)
        //{
        //    try
        //    {
        //        return 1 / Sin(theta);
        //    }
        //    catch (DivideByZeroException e) { MessageBox.Show(e.ToString(), "Maths Error"); }
        //    return -1;
        //}

        //public double Cos(decimal theta)
        //{
        //    int len = 50;
        //    int n;
        //    double cosRes = 0;
        //    int[] exp = new int[len];
        //    for (int num = 0; num < len; num++) { exp[num] = 2 * num; };
        //    foreach (int x in exp)
        //    {
        //        n = x / 2;
        //        cosRes += exponent(-1, n) * exponent(theta, x) / factorial(x);
        //    }
        //    return cosRes;
        //}
        //public double Arccos(decimal theta) { return (Cos(theta)); }
        //public double Sec(decimal theta)
        //{
        //    try
        //    {
        //        return 1 / Cos(theta);
        //    }
        //    catch (DivideByZeroException e) { MessageBox.Show(e.ToString(), "Maths Error"); }
        //    return -1;
        //}

        //public double Tan(decimal theta)
        //{
        //    try
        //    {
        //        return Sin(theta) / Cos(theta);
        //    }
        //    catch (DivideByZeroException e) { MessageBox.Show(e.ToString(), "Maths Error"); }
        //    return -1;
        //}
        //public double Arctan(decimal theta) { return (Tan(theta)); }
        //public double Cot(decimal theta) { return 1 / Tan(theta); }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            //Log test
            NatLogarithm ln = new NatLogarithm(1);
            Tester.TestEq(ln.Res, 0m);
            Logarithm log = new Logarithm(2, 1);
            Tester.TestEq(log.Res, 0m);
            log = new Logarithm(2m, 4m);
            Tester.TestEq(log.Res, 2m);
            log = new Logarithm(2m, 64m);
            Tester.TestEq(log.Res, 6);
            log = new Logarithm(3m, 81m);
            Tester.TestEq(log.Res, 4);
            log = new Logarithm(2m, 2m);
            Tester.TestEq(log.Res, 1);
            log = new Logarithm(1m, 1m);
            Tester.TestEq(log, Decimal.MinValue);
            log = new Logarithm(0.1m, 1000m);
            Tester.TestEq(log.Res, -3);
            /////// Exponent Tests
            //Exponent xObj = new Exponent(1, 1);
            //Tester.TestEq(xObj.Res, 1m);
            //xObj = new Exponent(1, 0);
            //Tester.TestEq(xObj.Res, 1m);
            //xObj = new Exponent(0, 1);
            //Tester.TestEq(xObj.Res, 0m);
            //xObj = new Exponent(0.5m, 1m);
            //Tester.TestEq(xObj.Res, 0.5m);
            //xObj = new Exponent(0.9m, 0m);
            //Tester.TestEq(xObj.Res, 1m);
            //xObj = new Exponent(0.5m, 2m);
            //Tester.TestEq(xObj.Res, 0.25m);
            //// Roots
            //xObj = new Exponent(16m, 1 / 2m);
            //Tester.TestEq(xObj.Res, 4m);
            //xObj = new Exponent(9m, 1 / 2m);
            //Tester.TestEq(xObj.Res, 3m);
            //xObj = new Exponent(27m, 1 / 3m);
            //Tester.TestEq(xObj.Res, 3m);
            //xObj = new Exponent(64m, 1 / 3m);
            //Tester.TestEq(xObj.Res, 4m);
            //xObj = new Exponent(1000m, 1 / 3m);
            //Tester.TestEq(xObj.Res, 10m);
            //xObj = new Exponent(16m, 1 / 4m);
            //Tester.TestEq(xObj.Res, 2m);

            // Sin tests
            //Trig trigObj = new Trig(0, "sin");
            //Tester.TestEq(trigObj.Res, 0m);
            //trigObj = new Trig(pi / 2, "sin");
            //Tester.TestEq(trigObj.Res, 1m);
            //trigObj = new Trig(pi, "sin");
            //Tester.TestEq(trigObj.Res, 0m);
            ////Cos tests
            //Trig cosObj = new Trig(0, "cos");
            //Tester.TestEq(cosObj.Res, 1m);
            //cosObj = new Trig(pi/2, "cos");
            //Tester.TestEq(cosObj.Res, 0m);
            //cosObj = new Trig(pi, "cos");
            //Tester.TestEq(cosObj.Res, -1m);
            ////// Tan Tests
            //Trig tanObj = new Trig(0, "tan");
            //Tester.TestEq(tanObj.Res, 0m);
            //tanObj = new Trig(pi/2, "tan");
            //Tester.TestEq(tanObj.Res, -1m);
            ////Factorial Tests
            //Tester.TestEq(factorial(0), 1);
            //Tester.TestEq(factorial(1), 1);
            //Tester.TestEq(factorial(4), 24);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtInp.Clear();
        }

        private void btnDegRad_Click(object sender, RoutedEventArgs e)
        {
            rad = !(rad);
            if (btnDegRad.Content.ToString() == "RAD") { btnDegRad.Content = "DEG"; }
            else { btnDegRad.Content = "RAD"; }
        }
    }
}
