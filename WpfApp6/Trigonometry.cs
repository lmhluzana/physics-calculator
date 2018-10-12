using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfApp6
{
    /// <summary>
    /// Base interface for functions
    /// </summary>
    public class IFunction : Sci_Calculator
    {
        public decimal X { get; set; }
        public decimal Res { get; set; }
    }

    /// <summary>
    /// Class for all trig functions
    /// </summary>
    public class Trig : IFunction
    {
        public string Funct { get; private set; }
        public bool specCase = false; // Special angle indicator
        public bool sinPos;
        public bool cosPos; // Sign indicators

        /// <summary>
        /// Call the required trig function
        /// </summary>
        /// <param name="x"></param>
        /// <param name="funct"></param>
        public Trig(decimal x, string funct)
        {
            Funct = funct;
            X = reductionForm(x); // Get x in more basic form
            List<decimal> specAngles = new List<decimal> { 0m, pi/3, pi/2, pi*2/3, pi, pi*4/3, pi*3/2, pi*5/3, 2*pi }; // List of special angles
            if (specAngles.Contains(x)) { specCase = true; } // Check if this is a special case

            try
            {
                switch (Funct)
                {
                    case "sin":
                        Res = sin(X); break;
                    case ("cos"):
                        Res = cos(X); break;
                    case ("tan"):
                        if (X == pi/2 || X == pi*3/2) throw new InvalidOperationException("tan limit"); // Check for the limits
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
                        throw new InvalidOperationException("no funct"); // In case no function is identified
                }
            }
            catch (InvalidOperationException ex )
            {
                if (ex.Message == "no funct") { MessageBox.Show("No function recognised", "Read Error"); }
                else if (ex.Message == "arcsin limit") { MessageBox.Show($"arcsinx is not defined for x = {x}", "Trigonometry Error"); }
                else if (ex.Message == "arccos limit") { MessageBox.Show($"arccosx is not defined for x = {x}", "Trigonometry Error"); }
                else { MessageBox.Show($"tanx is not defined for x = {x}", "Trigonometry Error"); }
            }

            decimal sin(decimal theta)
            {
                if (specCase == true)
                {
                    decimal[] sinSpecCase = { 0m, 0m, 0.5m, rt2 / 2, rt3 / 2, 0m, 1, 0m, rt3/2, rt2/2, 1/2, 0m, 0m }; // Special cases for sin, some elements are 0 as each element represents pi/12 to simplify identification
                    int i = Convert.ToInt32((theta / pi) * 11);
                    if (sinPos) { return sinSpecCase[i]; }
                    return -sinSpecCase[i];
                }
                // Taylor series Implementation
                int len = 28;
                decimal Res = theta; // Start at theta as the first element of the taylor series is x
                decimal prevR = 0;
                int[] exp = new int[len];
                int I = -1, // A seperate counter for the alternating sign
                    I0 = I;
                Exponent t; 
                
                for (int n = 3; n < len; n++) // Iterate until res is found, or until a given maximum
                {
                    if (n % 2 == 1) // Only use odd exponents
                    {
                        I = I * I0; // Alternate sign
                        t = new Exponent(theta, n); 
                        Res += I * t.Res / (decimal)factorial(n); // Add result to previous result
                        if (Absolute(Res - prevR) < 0.00000000000000000000000001m) { break; } // Compare result with previous result
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
                    decimal[] cosSpecCase = { 1m, 0m, rt3 / 2, rt2 / 2, 1/2, 0m, 0m, 0m, 1/2, rt2/2, rt3/2, 1 }; // Special cases for cos
                    int i = Convert.ToInt32((theta / pi) * 11);
                    if (cosPos) { return cosSpecCase[i]; }
                    return -cosSpecCase[i];
                }
                // Taylor series Implementation
                int len = 28;
                decimal Res = 1; // Start with res = 1 as the first element of the taylor series is 1
                decimal prevR = 0;
                int[] exp = new int[len];
                int I = 1,
                    I0 = -1; // Alternating sign
                Exponent t;
                
                for (int n = 2; n < len; n++) // Loop till result found
                {
                    if (n % 2 == 0)
                    {
                        I = I * I0; // Alternate sign
                        t = new Exponent(theta, n);
                        Res += I * t.Res / (decimal)factorial(n); // Add result to previous result
                        if (Absolute(Res - prevR) < 0.00000000000000000000000001m) { break; }
                        prevR = Res;
                    }
                }
                Res = Absolute(Res);
                if (cosPos) return Res;
                return -Res;
            }

            decimal arcsin(decimal theta)
            {
                List<decimal> arcsinSpecCase = new List<decimal> { 0m, 0m, 0.5m, rt2 / 2, rt3 / 2, 0m, 1, 0m, rt3 / 2, rt2 / 2, 1 / 2, 0m, 0m }; //Arcsin special cases
                if (arcsinSpecCase.Contains(theta))
                {
                    foreach (decimal y in specAngles) // Go through special angles, checking the value of sin at that angle against theta
                    {
                        if (theta == sin(y))
                        {
                            if (sinPos) { return y; }
                            return -y;
                        }
                    }
                }
                int len = 28;
                decimal Res = theta; // First element is theta
                decimal prevR = 0;
                int[] exp = new int[len];
                Exponent t;
                Exponent bin;

                for (int n = 3; n < len; n++)
                {
                    if (n % 2 == 1) // Only odd 
                    {
                        t = new Exponent(theta, n);
                        bin = new Exponent(2, n - 1);
                        Res += t.Res / (bin.Res * (n + 1));
                        if (Absolute(Res - prevR) < 0.00000000000000000000000001m) { break; }
                        prevR = Res;
                    }
                }
                return Res;
            }

            decimal arccos(decimal theta)
            {
                List<decimal> arccosSpecCase = new List<decimal> { 0m, 0m, 0.5m, rt2 / 2, rt3 / 2, 0m, 1 }; // Arccos special cases
                Trig cosComp;
                decimal res = 0m;
                try
                {
                    if (arccosSpecCase.Contains(theta))
                    {
                        foreach (decimal y in specAngles)
                        {
                            if (theta == cos(y)) // Check theta against cos at that angle
                            {
                                return res;
                            }
                        }
                    }
                else { res = pi / 2 - arcsin(theta); }
                }
                catch { MessageBox.Show("The program experienced a computational error, please retry", "Computational Error"); }
                return res;
            }

            decimal reductionForm(decimal theta)
            {
                if (theta > 2 * pi) { theta = theta % (2 * pi); } // Reduce the angle to less than 2 pi
                sinPos = (theta < pi);
                cosPos = ((theta > pi*3/2) || (theta <= pi/2)); // Get sign indicators for sin and cos
                theta = theta % pi; // Reduce the angle to pi/2
                return theta;
            }
        }
    }
    
    /// <summary>
    /// Natural logs class, base class for logarithms
    /// </summary>
    public class NatLogarithm : IFunction
    {
        public NatLogarithm(decimal n)
        {
            try
            {
                if (n == 0) throw new InvalidOperationException(); // Check for limits
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
                    2.30258509299404568401799145468m }; // List of ln from 1-10 to speed up program
                decimal ln2 = lns[1];

                ulong exp = 0;
                decimal temp = n;
                while (temp > 2) { temp = temp / 2; exp++; } // Reduce n by dividing by 2, count the number of times it is divided
                n = temp;

                int intN = (int)n;
                if ( intN == n) { Res = lns[intN - 1]; } // If just a common int, use list
                else
                {
                    Res = n - 1; // This taylor series is defined for ln(x - 1), to avoid ln(0)
                    decimal prevR = 0,
                            XN = n - 1,
                            XN0 = XN;
                    int I = 1,
                        I0 = -1;

                    for (int i = 2; i < 50; i++)
                    {
                        I = I * I0;
                        XN = XN * XN0;
                        Res = Res + (I * XN) / i;
                        if (Absolute(Res.CompareTo(prevR)) < 0.00000000000000000000000001m) { break; }
                        prevR = Res;
                    }
                }
                Res = exp * ln2 + Res; // Return res plus ln2 times the number of divisions by 2
            }
            catch (InvalidOperationException ex) { MessageBox.Show("Loga is not defined for a = 0." + ex.ToString(), "Logarithm Error"); }
        }
    }

    /// <summary>
    /// Derived logarithm class for all non natural logarithms
    /// </summary>
    public class Logarithm : NatLogarithm
    {
        public decimal B;

        public Logarithm(decimal b, decimal n) : base(n)
        {
            B = b;
            long count = 0;
            decimal tempN = n;
            decimal tempB = b;
            bool pos = (b > 1) ? true : false; // Check if pos

            try
            {
                if (b == 1 || b < 0 || n == 0) { throw new InvalidOperationException(); } 
                if (n == 1) { Res = 0m; } // Check for limits and specials
                else if (n % b == 0) // Check if  basic logarithm
                {
                    if (!pos) { tempB = 1 / tempB; }
                    while (true)
                    {
                        tempN = tempN / tempB;
                        count++; // Count number of divisions
                        if (tempN == 1) break;
                    }
                    if (pos) { Res = count; }
                    else { Res = -count; }
                }
                else
                {
                    b = (new NatLogarithm(b)).Res; // Get logarithm by taking the natural log of the base and x, and dividing them
                    if (pos) { Res = n / b; }
                    else { Res = -n / b; }
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
            if (intX == x && intP == p) { Res = intExponent(intX, intP); } // Check for a basic int problem
            else { Res = dcExponent(x, p); } // Decimal problems
        }

        public decimal dcExponent(decimal x, decimal p)
        {
            if (X == 0) { return 0; } // Check for 0
            else
            {
                decimal res = 1;
                bool pos = (p >= 0); // Check if exponent is positive, then use absolute value
                p = Absolute(p);

                try
                {
                    if (p > 1) // For non roots
                    {
                        long exp = 0;
                        decimal temp = p;
                        while (temp > 1) { temp = temp / 2; exp++; } // Reduce p to smaller more manageable num
                        p = temp;
                        for (int n = 0; n < exp; n++)
                        {
                            x = x * x;
                        }
                        if (p != 1) { res = dcExponent(x, p); } // Get remainder
                        else { res = x; }
                    }
                    else if (p == 1m) { res = x; } // Check for p = 1 
                    else if (p > 0) // Roots
                    {
                        res = 0;
                        decimal tempX = x;
                        if ((1 / p) % 2 == 0 && 1 / p > 2) // Check for some form of square roots
                        {
                            decimal compP = 1 / p / 2;
                            for (int i = 0; i < compP; i++) { tempX = dcExponent(tempX, 1 / 2m); } // Take square recursively
                            res = tempX; 
                        }
                        else // Taylor series implementation of e^n, with n = p*lnx
                        {
                            NatLogarithm ln = new NatLogarithm(x); // Get lnx
                            decimal y0 = ln.Res * p, // == ln x^p
                                    y = 1;
                            decimal prevR = 1;
                            for (int n = 1; n < 28; n++)
                            {
                                y = y * y0; 
                                res = prevR + y / (decimal)factorial(n);
                                if (Absolute(res - prevR) < 0.0000000000000000000000000001m) { break; }
                                prevR = res;
                                if (y > 1000000000000000000000000000m) // Catch y exceeding limits of decimals
                                {
                                    y = y / 1000000000000000000000000000m;
                                    for (int N = n; N < 28; N++) // Continue with reduced y and factor
                                    {
                                        y = y * y0;
                                        res = prevR + (y / (decimal)factorial(n))* 1000000000000000000000000000m;
                                        if (Absolute(res - prevR) < 0.0000000000000000000000000001m) { break; }
                                        prevR = res;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else { return 1m; } // Check for zero

                    if (pos) { return res; }
                    else { return 1 / res; } 
                }
                catch (Exception ex) { MessageBox.Show("Maths Error" + ex.ToString(), "Error"); }
                return 0;
            }
        }

        public decimal intExponent(int x, int p)
        {
            if (x == 0) { return  0; } // Check for specials
            if ( p == 0) { return 1; }
            bool pos = (p >= 0); 
            p = Absolute(p);
            Res = 1;

            for (int b = 0; b < p; b++)
            {
                Res = Res * x;
            }

            if (pos) return Res;
            else { return 1 / Res; }
        }
    }

}