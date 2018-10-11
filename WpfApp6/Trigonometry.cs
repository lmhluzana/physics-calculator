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
                        Res = sin(X); break;
                    case ("cos"):
                        Res = cos(X); break;
                    case ("tan"):
                        if (X == pi/2 || X == pi*3/2) throw new InvalidOperationException("tan limit");
                        Res = sin(X) / cos(X); break;
                    case ("arcsin"):
                        if (X > 1m) throw new InvalidOperationException("arcsin limit");
                        Res = arcsin(x); break;
                    case ("arccos"):
                        if (X > 1m) throw new InvalidOperationException("arccos limit");
                        Res = arccos(X); break;
                    case ("arctan"):
                        Res = arcsin(X) / arccos(X); break;
                    case ("cosec"):
                        Res = 1 / sin(X); break;
                    case ("sec"):
                        Res = 1 / cos(X); break;
                    case ("cot"):
                        Res = cos(X) / sin(X); break;
                    default:
                        throw new InvalidOperationException("no funct");
                }
            }
            catch (InvalidOperationException ex )
            {
                if (ex.Message == "no funct") { MessageBox.Show("No function recognised", "Maths Error"); }
                else if (ex.Message == "arcsin limit") { MessageBox.Show($"arcsinx is not defined for x = {x}", "Trigonometry Error"); }
                else if (ex.Message == "arccos limit") { MessageBox.Show($"arccosx is not defined for x = {x}", "Trigonometry Error"); }
                else { MessageBox.Show($"tanx is not defined for x = {x}", "Trigonometry Error"); }
            }

            decimal sin(decimal theta)
            {
                if (specCase == true)
                {
                    decimal[] sinSpecCase = { 0m, 0m, 0.5m, rt2 / 2, rt3 / 2, 0m, 1, 0m, rt3/2, rt2/2, 1/2, 0m, 0m };
                    int i = Convert.ToInt32((theta / pi) * 11);
                    if (sinPos) { return sinSpecCase[i]; }
                    return -sinSpecCase[i];
                }
                int len = 28;
                decimal Res = theta;
                decimal prevR = 0;
                int[] exp = new int[len];
                int I = 1;
                Exponent sign;
                Exponent t;
                
                for (int n = 2; n < len; n++)
                {
                    if (n % 2 == 1)
                    {
                        sign = new Exponent(-1, I++);
                        t = new Exponent(theta, n);
                        Res += sign.Res * t.Res / (decimal)factorial(n);
                        if (Absolute(Res - prevR) < 0.00000000000000000000000001m) { break; }
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
                    decimal[] cosSpecCase = { 1m, 0m, rt3 / 2, rt2 / 2, 1/2, 0m, 0m, 0m, 1/2, rt2/2, rt3/2, 1 };
                    int i = Convert.ToInt32((theta / pi) * 11);
                    if (cosPos) { return cosSpecCase[i]; }
                    return -cosSpecCase[i];
                }
                int len = 28;
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
                        if (Absolute(Res - prevR) < 0.00000000000000000000000001m) { break; }
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
                int len = 100;
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
                        if (Absolute(Res - prevR) < 0.00000000000000000000000001m) { break; }
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
                if (theta > 2 * pi) { theta = theta % (2 * pi); }
                sinPos = (theta < pi) ? true : false;
                cosPos = (((theta > pi/2) && (theta <= pi)) || (theta >= pi * 3/4)) ? true : false;
                theta = theta % pi;
                return theta;
            }
        }
    }
    
    public class NatLogarithm : IFunction
    {
        public NatLogarithm(decimal n)
        {
            try
            {
                if (n == 0) throw new InvalidOperationException();
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
                    decimal prevR = 0,
                            XN = n - 1,
                            XN0 = XN;

                    for (int i = 2; i < 50; i++)
                    {
                        sign = new Exponent(-1, i - 1);
                        XN = XN * XN0;
                        Res = Res + (sign.Res * XN) / i;
                        if (Absolute(Res.CompareTo(prevR)) < 0.00000000000000000000000001m) { break; }
                        prevR = Res;
                    }
                }
                Res = exp * ln2 + Res;
            }
            catch (InvalidOperationException ex) { MessageBox.Show("Loga is not defined for a = 0." + ex.ToString(), "Logarithm Error"); }
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
            if (X == 0) { return 0; }
            else
            {
                decimal res = 1;
                bool pos = (p >= 0) ? true : false;
                p = Absolute(p);

                try
                {
                    if (p > 1)
                    {
                        long exp = 0;
                        decimal temp = p;
                        while (temp > 1) { temp = temp / 2; exp++; }
                        p = temp;
                        for (int n = 0; n < exp; n++)
                        {
                            x = x * x;
                        }
                        if (p != 1) { res = dcExponent(x, p); }
                        else { res = x; }
                    }
                    else if (p == 1m) { res = x; }
                    else if (p > 0)
                    {
                        res = 0;
                        decimal tempX = x;
                        if ((1 / p) % 2 == 0 && 1 / p > 2)
                        {
                            decimal compP = 1 / p / 2;
                            for (int i = 0; i < compP; i++) { tempX = dcExponent(tempX, 1 / 2m); }
                            res = tempX;
                        }
                        else
                        {
                            NatLogarithm ln = new NatLogarithm(x);
                            decimal y0 = ln.Res * p,
                                    y = 1;
                            Exponent exp;
                            decimal prevR = 1,
                                    expY = 1;
                            for (int n = 1; n < 28; n++)
                            {
                                y = y * y0;
                                res = prevR + y / (decimal)factorial(n);
                                if (Absolute(res - prevR) < 0.0000000000000000000000000001m)
                                {
                                    break;
                                }
                                prevR = res;
                            }
                        }
                    }
                    else { return 1m; }

                    if (pos) { return res; }
                    else { return 1 / res; }
                }
                catch (Exception ex) { MessageBox.Show("Maths Error" + ex.ToString(), "Error"); }
                return 0;
            }
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