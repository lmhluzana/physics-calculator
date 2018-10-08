using System;
using System.Windows;

namespace WpfApp6
{
    public class Function : Sci_Calculator
    {
        public decimal X { get; set; }
        public decimal Res { get; set; }

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
                        Res = sin(x); break;
                    case ("cos"):
                        Res = cos(x); break;
                    case ("tan"):
                        Res = sin(x) / cos(x); break;
                    case ("arcsin"):
                        Res = 1 / sin(x); break;
                    case ("arccos"):
                        Res = 1 / cos(x); break;
                    case ("arctan"):
                        Res = cos(x) / sin(x); break;
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
            catch (System.InvalidOperationException) { MessageBox.Show("No function recognised", "Maths Error"); }

            decimal sin(decimal theta)
            {
                if (specCase == true)
                {
                    decimal[] sinSpecCase = { 0m, 0.5m, rt2 / 2, rt3 / 2, 1 };
                    int i = Convert.ToInt32((theta / pi) * 4);
                    return sinSpecCase[i];
                }
                int len = 50;
                decimal[] lsSinRes = new decimal[len];
                decimal Res = theta;
                decimal prevR = 0;
                int[] exp = new int[len];
                Exponent sign;
                Exponent t;

                //for (int num = 0; num < len; num++) { exp[num] = 2 * num + 1; };
                //foreach (int i in exp)
                for (int n = 0; n < len; n++)
                {
                    if (n % 2 == 1)
                    {
                        sign = new Exponent(-1, n);
                        t = new Exponent(theta, n);
                        Res += sign.Res * t.Res / (decimal)factorial(n);
                        if (Res - prevR < 0.00000000000000000000000001m) { break; }
                        prevR = Res;
                    }
                    //sign = new Exponent(-1, n);
                    //t = new Exponent(theta, i);
                    //Res += sign.Res * t.Res / (decimal)factorial(i);
                    //if (Res - prevR < 0.00000000000000000000001m) { break; }
                    //prevR = Res;

                }
                return Res;
            }

            decimal cos(decimal theta)
            {
                int len = 50;
                decimal Res = 1;
                decimal prevR = 0;
                int[] exp = new int[len];
                Exponent sign;
                Exponent t;

                //for (int num = 0; num < len; num++) { exp[num] = 2 * num; };
                //foreach (int i in exp)
                for (int n = 0; n < len; n++)
                {
                    if (n % 2 == 0)
                    {
                        sign = new Exponent(-1, n);
                        t = new Exponent(theta, n);

                        Res += sign.Res * t.Res / (decimal)factorial(n);
                        if (Res - prevR < 0.00000000000000000000000001m) { break; }
                        prevR = Res;
                    }

                    //sign = new Exponent(-1, n);
                    //t = new Exponent(theta, i);

                    //Res += sign.Res * t.Res / (decimal)factorial(i);
                    //if (Res - prevR < 0.00000000000000000000000001m) { break; }
                }
                return Res;
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

    public class Logarithm : NatLogarithm
    {
        //public decimal b;

        //public Logarithm(decimal b, decimal n) : base(n)
        //{
        //    if (!(b == euler))
        //    {
        //        if (b == 10) { TenLogarithm tnLog = new TenLogarithm(n); }
        //        else
        //        {
        //            NatLogarithm logB = new NatLogarithm(b);
        //            NatLogarithm logX = new NatLogarithm(n);
        //            res = logB.res / logX.res;
        //        }
        //    }
        //    else { NatLogarithm log = new NatLogarithm(b); }
        //}
        public NatLogarithm logB;
        public NatLogarithm logX;
        public Logarithm(decimal b, decimal n) : base(n)
        {
            logB = new NatLogarithm(b);
            logX = new NatLogarithm(n);
            if (logX.Res == 0) { Res = 0; }
            else { Res = logX.Res / logB.Res; }
        }
    }

    public class TenLogarithm : Logarithm
    {

        public TenLogarithm(decimal b, decimal n) : base(b, n)
        {
            logB = new NatLogarithm(10);
            logX = new NatLogarithm(n);
            Res = logX.Res / logB.Res;
        }
    }

    public class NatLogarithm : Function
    {
        public NatLogarithm(decimal n) : base(n)
        {
            decimal Res = (decimal)n - 1;
            Exponent sign;
            Exponent expN;
            try
            {
                if (n == 0) throw new InvalidOperationException();

                for (int i = 2; i < 40; i++)
                {
                    sign = new Exponent(-1, i - 1 / i);
                    expN = new Exponent(n - 1, i);
                    Res = Res + (sign.Res * expN.Res);
                }
            }
            catch (InvalidOperationException ex) { MessageBox.Show("The base of a log cannot equal 1 or 0 \n" + ex.ToString(), "Maths Error"); }
        }
    }

    public class Exponent : Function
    {
        int intX, intP;

        public Exponent(decimal x, decimal p) : base(x)
        {
            intX = (int)x;
            intP = (int)p;
            if (intX == x && intP == p) { Res = intExponent(intX, intP); }
            else { Res = dcExponent(x, p); }
        }

        public decimal dcExponent(decimal x, decimal p)
        {
            bool pos = (p >= 0) ? true : false;
            p = Absolute(p);
            Res = 1;
            Exponent xPrevN;
            try
            {
                if (p >= 1)
                {
                    for (int b = 0; b < p; b++)
                    {
                        Res = Res * x;
                    }
                }
                else if (p > 0)
                {
                    decimal n = 0,
                            prevN = (decimal)(Math.Sqrt((double)n)) + x / 2;
                    while (true)
                    {
                        xPrevN = new Exponent(prevN, (int)(1 / p - 1));
                        n = (prevN + x / xPrevN.Res) / 2;
                        if ((prevN - n) < 0.00000000000001m) { break; }
                        prevN = n;
                    }
                    Res = n;
                }
                else { Res = 0m; }
            }
            catch (Exception ex) { MessageBox.Show("Maths Error" + ex.ToString(), "Error"); }
            if (pos) { return Res; }
            else { return 1 / Res; }
        }

        public decimal intExponent(int x, int p)
        {
            if (x == 0) { return  0; }
            bool pos = (p >= 0) ? true : false;
            p = Absolute(p);
            Res = 1;
            if (p >= 1)
            {
                for (int b = 0; b < p; b++)
                {
                    Res = Res * x;
                }
            }
            else { Res = 1; }

            if (pos) return Res;
            else { return 1 / Res; }
        }
    }

}