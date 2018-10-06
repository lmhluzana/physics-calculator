using System;



public class Function
{
    public decimal x { get; public set; }
    public decimal res { get; public set; }

    public Function(decimal x)
    {
        this.x = x;
    }
}

public class Trigonometry : Function
{
    public string funct { get; private set; }
    public decimal theta { get; private set; }

	public Trigonometry( string funct, decimal x)
	{
        this.funct = funct;
        this.theta = x;
        Enum<int> efunct = new Enum() {"sin", "cos", "tan", "arcsin", "arccos", "arctan", "cosec", "sec", "cot" };
        switch(funct)
        {
            case (efunct[0]):
                this = new Sin();
            case (efunct[1]):
                res = Cos(theta);
            case (efunct[2]):
                res = Tan(theta);
            case (efunct[3]):
                res = Arcsin(theta);
            case (efunct[4]):
                res = Arccos(theta);
            case (efunct[5]):
                res = Arctan(theta);
            case (efunct[6]):
                res = Cosec(theta);
            case (efunct[7]):
                res = Sec(theta);
            case (efunct[8]):
                res = Cot(theta);
        }
	}
}

public class Sin : Trigonometry
{
    public Sin(decimal theta)
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
        double prevR = 0;
        int[] exp = new int[len];
        for (int num = 0; num < len; num++) { exp[num] = 2 * num + 1; };
        foreach (int x in exp)
        {
            n = x / 2;
            dbSinRes += exponent(-1, n) * exponent(theta, x) / factorial(x);
            if (dbSinRes == prevR) { break; }
            prevR = dbSinRes;
        }
        //dbSinRes += Convert.ToDouble(lsSinRes.Sum());
        res = dbSinRes;
    }
}

