using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfApp6
{
    public class IFunction : Sci_Calculator
    {
        public decimal X { get; set; }
        public decimal Res { get; set; }
    }

    public class Trig : IFunction
    {
        public string Funct { get; private set; }
        public bool specCase = false;
        public bool sinPos;
        public bool cosPos;

        public Trig(decimal x, string funct)
        {
            Funct = funct;
            X = reductionForm(x);
            List<decimal> specAngles = new List<decimal> { 0m, pi/3, pi/2, pi*2/3, pi, pi*4/3, pi*3/2, pi*5/3, 2*pi };
            if (specAngles.Contains(x)) { specCase = true; }

            try
            {
                switch (Funct)
                {
                    case "sin":
                        Res = sin(x); break;
                    case ("cos"):
                        Res = cos(x); break;
                    case ("tan"):
                        Res = sin(x) / cos(x); break;
                    case ("arcsin"):
                        Res = arcsin(x); break;
                    case ("arccos"):
                        Res = arccos(x); break;
                    case ("arctan"):
                        Res = arcsin(x) / arccos(x); break;
                    case ("cosec"):
                        Res = 1 / sin(x); break;
                    case ("sec"):
                        Res = 1 / cos(x); break;
                    case ("cot"):
                        Res = cos(x) / sin(x); break;
                    default:
                        throw new System.InvalidOperationException();
                }
            }
            catch (System.InvalidOperationException) { MessageBox.Show("No function recognised", "Maths Error"); }

            decimal sin(decimal theta)
            {
                if (specCase == true)
                {
                    decimal[] sinSpecCase = { 0m, 0m, 0.5m, rt2 / 2, rt3 / 2, 0m, 1 };
                    int i = Convert.ToInt32((theta / pi) * 6);
                    if (sinPos) { return sinSpecCase[i]; }
                    return -sinSpecCase[i];
                }
                int len = 50;
                decimal Res = theta;
                decimal prevR = 0;
                int[] exp = new int[len];
                Exponent sign;
                Exponent t;
                
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
                }
                if (sinPos) { return Res; }
                return -Res;
            }

            decimal cos(decimal theta)
            {
                if (specCase == true)
                {
                    decimal[] cosSpecCase = { 1m, 0m, rt3 / 2, rt2 / 2, 1/2, 0m, 0m };
                    int i = Convert.ToInt32((theta / pi) * 6);
                    if (cosPos) { return cosSpecCase[i]; }
                    return -cosSpecCase[i];
                }
                int len = 50;
                decimal Res = 1;
                decimal prevR = 0;
                int[] exp = new int[len];
                Exponent sign;
                Exponent t;
                
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
                }
                if (cosPos) { return Res; }
                return -Res;
            }

            decimal arcsin(decimal theta)
            {
                List<decimal> arcsinSpecCase = new List<decimal> { 0m, 0m, 0.5m, rt2 / 2, rt3 / 2, 0m, 1 };
                if (arcsinSpecCase.Contains(theta))
                {
                    foreach (decimal y in specAngles)
                    {
                        if (theta == sin(y))
                        {
                            if (sinPos) { return y; }
                            return -y;
                        }
                    }
                }
                int len = 50;
                decimal Res = theta;
                decimal prevR = 0;
                int[] exp = new int[len];
                Exponent t;
                Exponent bin;

                for (int n = 3; n < len; n++)
                {
                    if (n % 2 == 1)
                    {
                        t = new Exponent(theta, n);
                        bin = new Exponent(2, n - 1);
                        Res += t.Res / (decimal)(bin.Res * (n + 1));
                        if (Res - prevR < 0.00000000000000000000000001m) { break; }
                        prevR = Res;
                    }
                }
                if (sinPos) { return Res; }
                return -Res;
            }

            decimal arccos(decimal theta)
            {
                List<decimal> arccosSpecCase = new List<decimal> { 0m, 0m, 0.5m, rt2 / 2, rt3 / 2, 0m, 1 };
                Trig cosComp;
                Decimal res = 0m;
                try
                {
                    if (arccosSpecCase.Contains(theta))
                    {
                        foreach (decimal y in specAngles)
                        {
                            cosComp = new Trig(y, "cos");
                            if (theta == cosComp.Res)
                            {
                                if (sinPos) { res = y; }
                                res = -y;
                            }
                            else { throw new Exception(); }
                        }
                    }
                else { res = pi / 2 - arcsin(theta); }
                }
                catch { MessageBox.Show("The program experienced a computational error, please retry", "Computational Error"); }
                return res;
            }

            decimal reductionForm(decimal theta)
            {
                if (theta > 2 * pi) { theta = theta % 2 * pi; }
                sinPos = (theta < pi) ? true : false;
                cosPos = (theta < pi / 2 || theta > pi * 3 / 4) ? true : false;
                theta = theta % pi;
                return theta;
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
    
    public class NatLogarithm : IFunction
    {
        public NatLogarithm(decimal n)
        {
            X = n;
            decimal[] lns = { 0m,
                0.693147180559945309417232121458m,
                1.09861228866810969139524523692m,
                1.38629436111989061883446424292m,
                1.60943791243410037460075933323m,
                1.79175946922805500081247735838m,
                1.94591014905531330510535274344m,
                2.07944154167983592825169636437m,
                2.19722457733621938279049047384m,
                2.30258509299404568401799145468m };
            decimal ln2 = lns[1];

            ulong exp = 0;
            decimal temp = n;
            while (temp > 2) { temp = temp / 2; exp++; }
            n = temp;

            int intN = (int)n;
            if ( (decimal)intN == n) { Res = lns[intN - 1]; }
            else
            {
                Res = n - 1;
                Exponent sign;
                Exponent expN;
                decimal prevR = 0;

                for (int i = 2; i < 50; i++)
                {
                    sign = new Exponent(-1, i - 1);
                    expN = new Exponent(n - 1, i);
                    Res = Res + (sign.Res * expN.Res) / i;
                    if (Absolute(Res.CompareTo(prevR)) < 0.00000000000000000000000001m) { break; }
                    prevR = Res;
                }
            }
            Res = exp * ln2 + Res;
        }
    }

    public class Logarithm : NatLogarithm
    {
        public decimal B;

        public Logarithm(decimal b, decimal n) : base(n)
        {
            B = b;
            long count = 0;
            decimal tempN = n;
            decimal tempB = b;
            bool pos = (b > 1) ? true : false;

            try
            {
                if (b == 1 || b < 0) { throw new InvalidOperationException(); }
                if (n == 1) { Res = 0m; }
                else if (n % b == 0)
                {
                    if (!pos) { tempB = 1 / tempB; }
                    while (true)
                    {
                        tempN = tempN / tempB;
                        count++;
                        if (tempN == 1) break;
                    }
                    if (pos) { Res = count; }
                    else { Res = -count; }
                }
                else
                {
                    if (n == 0) { Res = 0; }
                    //logX = new NatLogarithm(n);
                    //else { Res = logX.Res / logB.Res; }
                    else
                    {
                        b = (new NatLogarithm(b)).Res;
                        if (pos) { Res = n / b; }
                        else { Res = -n / b; }
                    }
                }
            }
            catch (InvalidOperationException) { MessageBox.Show($"Logarithms domain error. \n x must be positive, and b cannot equal 1", "Domain Error"); Res = Decimal.MinValue; }
        }
    }

    public class Exponent : IFunction
    {
        int intX, intP;
        public decimal P;

        public Exponent(decimal x, decimal p)
        {
            X = x;
            P = p;
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
                    long exp = 0;
                    decimal temp = p;
                    while (temp > 1) { temp = temp / 2; exp++; }
                    p = temp;
                    for (int n = 0; n < exp; n++)
                    {
                        x = x * x;
                    }
                    if ( p != 0 ) { Res = dcExponent(x, p); }
                    else { Res = x; }
                }
                else if (p > 0)
                {
                    if ((1/p) % 2 == 0 && 1 / p > 2)
                    {
                        Res = x;
                        decimal compP = 1 / p / 2;
                        for(int i = 0; i < compP; i++) { Res = dcExponent(Res, 1/2m); }
                        return Res;
                    }
                    decimal n = 0,
                            prevN = (decimal)(Math.Sqrt((double)n)) + x / 2;
                    while (true)
                    {
                        xPrevN = new Exponent(prevN, (int)(1 / p - 1));
                        n = (prevN + x / xPrevN.Res) / 2;
                        if (Absolute(prevN - n) < 0.0000000000000000000000000001m) { break; }
                        prevN = n;
                    }
                    Res = n;
                }
                else { Res = 1m; }
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