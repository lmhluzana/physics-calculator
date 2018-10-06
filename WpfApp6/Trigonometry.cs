using System;
using System.Windows;

namespace WpfApp6
{
    public class Function : Sci_Calculator
    {
        public decimal X { get; set; }
        public decimal res { get; set; }

        public Function(decimal x)
        {
            X = x;
        }
    }

    public class Trig : Function
    {
        public string funct { get; private set; }

        public Trig(decimal x, string funct) : base(x)
        {
            this.funct = funct;
            if (x > 2*pi) { x = reductionForm(x); }

            try
            {
                switch (funct)
                {
                    case "sin":
                        res = sin(x); break;
                    case ("cos"):
                        res = cos(x); break;
                    case ("tan"):
                        res = sin(x) / cos(x); break;
                    case ("arcsin"):
                        res = 1 / sin(x); break;
                    case ("arccos"):
                        res = 1 / cos(x); break;
                    case ("arctan"):
                        res = cos(x) / sin(x); break;
                    case ("cosec"):
                        break;
                    case ("sec"):
                        break;
                    case ("cot"):
                        break;
                    default:
                        throw new System.InvalidOperationException();
                }
            }
            catch (System.InvalidOperationException ex) { MessageBox.Show("No function recognised", "Maths Error"); }

            decimal sin(decimal theta)
            {
                if (specCase == true)
                {
                    decimal[] sinSpecCase = { 0m, 0.5m, rt2 / 2, rt3 / 2, 1 };
                    int i = Convert.ToInt32((theta / pi) * 4);
                    return sinSpecCase[i];
                }
                int len = 50;
                int n;
                decimal[] lsSinRes = new decimal[len];
                decimal dbSinRes = 0;
                decimal prevR = 0;
                int[] exp = new int[len];
                for (int num = 0; num < len; num++) { exp[num] = 2 * num + 1; };
                foreach (int i in exp)
                {
                    n = i / 2;
                    dbSinRes += (decimal)(exponent(-1, n) * exponent(theta, i) / factorial(i));
                    if (dbSinRes == prevR) { break; }
                    prevR = dbSinRes;
                }
                return dbSinRes;
            }

            decimal cos(decimal theta)
            {
                int len = 50;
                int n;
                decimal cosRes = 0;
                int[] exp = new int[len];
                for (int num = 0; num < len; num++) { exp[num] = 2 * num; };
                foreach (int i in exp)
                {
                    n = i / 2;
                    cosRes += (decimal)(exponent(-1, n) * exponent(theta, i) / factorial(i));
                }
                return cosRes;
            }
        }
    }

    //public class Sin : Trigonometry
    //{
    //    public Sin(decimal x, string funct)
    //    {
    //        if (specCase == true)
    //        {
    //            decimal[] sinSpecCase = { 0m, 0.5m, rt2 / 2, rt3 / 2, 1 };
    //            int i = Convert.ToInt32((theta / pi) * 4);
    //            return Convert.ToDouble(sinSpecCase[i]);
    //        }
    //        int len = 50;
    //        int n;
    //        decimal[] lsSinRes = new decimal[len];
    //        double dbSinRes = 0;
    //        double prevR = 0;
    //        int[] exp = new int[len];
    //        for (int num = 0; num < len; num++) { exp[num] = 2 * num + 1; };
    //        foreach (int x in exp)
    //        {
    //            n = x / 2;
    //            dbSinRes += exponent(-1, n) * exponent(theta, x) / factorial(x);
    //            if (dbSinRes == prevR) { break; }
    //            prevR = dbSinRes;
    //        }
    //        //dbSinRes += Convert.ToDouble(lsSinRes.Sum());
    //        res = dbSinRes;
    //    }
    //}
    //public class Cos : Trigonometry
    //{
    //    public Cos(decimal x) : base(x)
    //    {
    //        int len = 50;
    //        int n;
    //        double cosRes = 0;
    //        int[] exp = new int[len];
    //        for (int num = 0; num < len; num++) { exp[num] = 2 * num; };
    //        foreach (int x in exp)
    //        {
    //            n = x / 2;
    //            cosRes += exponent(-1, n) * exponent(theta, x) / factorial(x);
    //        }
    //        res = cosRes;
    //    }
    //}
    //public class Tan : Trigonometry
    //{
    //    public Tan(decimal theta) : base(x)
    //    {
    //    }
    //}

    public class Logarithm : Function
    {
        public decimal b;

        public Logarithm(decimal b, decimal n) : base(n)
        {
            if (!(b == euler))
            {
                if (b == 10) { TenLogarithm tnLog = new TenLogarithm(n); }
                else
                {
                    NatLogarithm logB = new NatLogarithm(b);
                    NatLogarithm logX = new NatLogarithm(n);
                    res = logB.res / logX.res;
                }
            }
            else { NatLogarithm log = new NatLogarithm(b); }
        }

    }

    public class TenLogarithm : Logarithm
    {
        public TenLogarithm(decimal n) : base(n)
        {
            b = 10m;
            NatLogarithm log10 = new NatLogarithm(10);
            NatLogarithm logX = new NatLogarithm(n);
            res = logX.res / log10.res;
        }
    }

    public class NatLogarithm : Logarithm
    {
        public NatLogarithm(decimal n) : base(n)
        {
            b = euler;
            this.x = n;
            double res = (double)n - 1;
            try
            {
                if (b == 1 || b == 0) throw new InvalidOperationException();

                for (int i = 2; i < 40; i++)
                {
                    res = res + (exponent(-1, i - 1) / i) * exponent((decimal)(n - 1), i);
                }
            }
            catch (InvalidOperationException ex) { MessageBox.Show("The base of a log cannot equal 1 or 0 \n" + ex.ToString(), "Maths Error"); }
        }
    }
}