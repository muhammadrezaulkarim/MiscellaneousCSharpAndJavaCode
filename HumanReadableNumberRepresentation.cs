using System;
using System.Text;

public class Test
{
   /* public static void Main()
    {
        while (true)
        {
            long value = long.Parse(Console.ReadLine());
            int decimalPlaces = int.Parse(Console.ReadLine());
            bool addDecimalForSingleDigit = (Console.ReadLine() == "1") ? true : false;
            string output = GraduatedValue(value, decimalPlaces, addDecimalForSingleDigit);
            Console.WriteLine(output);
            Console.WriteLine("Press any key to quit...");
            //Console.ReadKey();
        }
        
    } */

    public static string GraduatedValue(long value, int decimalPlaces, bool addDecimalForSingleDigit)
    {
        string tempVal = value.ToString();
        int numLen = tempVal.Length;

        Boolean singleK = false;

        String repFormat = "";

        long quotient = 0;
        double valueInK = 0;


        // specially handle number between -999 and 1000

        if (value >= -999 && value < 1000)
        {
            if (addDecimalForSingleDigit)
                decimalPlaces++;

            if (value != 0) // for non zero number
            {
                double valueLessThanOneK = Math.Round((double)value, decimalPlaces, MidpointRounding.ToEven);
                return valueLessThanOneK.ToString();
            }
            else // for 0
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("0");

                for (int i = 0; i < decimalPlaces; i++)
                {
                    if (i == 0)
                        sb.Append(".0");
                    else
                        sb.Append("0");
                }

                return sb.ToString();

            }
        }

        // convert negative number into positive number to process in the same way as positive numbers
        Boolean isNegativeNumber = false;

        if (value < -999)
        {
            isNegativeNumber = true;
            value = value * -1;
        }


        if (value < 1000000)
        {
            repFormat = "K";

            quotient = value / 1000;
            valueInK = value / 1000.0;

            //Console.WriteLine(valueInK);

            if (quotient <= 9)
            {
                singleK = true;

            }
        }
        else if (value >= 1000000 && value < 1000000000)
        {
            repFormat = "M";
            quotient = value / 1000000;
            valueInK = value / 1000000.0;
        }
        else if (value >= 1000000000 && value < 1000000000000)
        {
            repFormat = "B";
            quotient = value / 1000000000;
            valueInK = value / 1000000000.0;
        }
        else if (value >= 1000000000000 && value < 1000000000000000)
        {
            repFormat = "T";
            quotient = value / 1000000000000;
            valueInK = value / 1000000000000.0;
        }
        else if (value >= 1000000000000000)
        {
            repFormat = "Q";
            quotient = value / 1000000000000000;
            valueInK = value / 1000000000000000.0;
        }






        double value1 = -1;

        if (addDecimalForSingleDigit && singleK)
            value1 = Math.Round(valueInK, decimalPlaces + 1, MidpointRounding.ToEven);

        else if (addDecimalForSingleDigit && !singleK && decimalPlaces > 0)
            value1 = Math.Round(valueInK, decimalPlaces + 1, MidpointRounding.ToEven);
        else
            value1 = Math.Round(valueInK, decimalPlaces, MidpointRounding.ToEven);


        if (singleK)
        {

            String addExtra = "";

            if (decimalPlaces == 2 && tempVal.Substring(tempVal.Length - 3).Equals("000"))
            {
                if (addDecimalForSingleDigit)
                    addExtra = ".000";
                else
                    addExtra = ".00";
            }
            else if (decimalPlaces == 2 && tempVal.Substring(tempVal.Length - 2).Equals("00"))
            {
                if (addDecimalForSingleDigit)
                    addExtra = "00";
                else
                    addExtra = "0";
            }
            else if (decimalPlaces == 2 && tempVal.Substring(tempVal.Length - 1).Equals("0"))
            {
                if (addDecimalForSingleDigit)
                    addExtra = "0";
            }

            if (decimalPlaces == 1 && tempVal.Substring(tempVal.Length - 3).Equals("000"))
            {
                if (addDecimalForSingleDigit)
                    addExtra = ".00";
                else
                    addExtra = ".0";
            }

            else if (decimalPlaces == 1 && tempVal.Substring(tempVal.Length - 2).Equals("00"))
            {
                if (addDecimalForSingleDigit)
                    addExtra = "0";
            }
            

            if (isNegativeNumber)
                return "-" + value1.ToString() + addExtra + "K";
            else
                return value1.ToString() + addExtra + "K";

        }

        if (isNegativeNumber)
            return "-" + value1.ToString() + repFormat;
        else
            return value1.ToString() + repFormat;
    }
}
/* TestGraduatedValue("Simplest Low Value", 123, 0, false, "123");
TestGraduatedValue("Zero", 0, 0, false, "0");
TestGraduatedValue("Decimal Zeros", 0, 4, false, "0.0000");

TestGraduatedValue("Simple One Grad", 12345, 0, false, "12K");
TestGraduatedValue("Simple With Decimals", 12345, 3, false, "12.345K");

TestGraduatedValue("AddDec Shouldnt Affect", 12345, 0, true, "12K");
TestGraduatedValue("Zero AddDec", 0, 0, true, "0.0");

TestGraduatedValue("Round Integer Up", 1880, 0, false, "2K");
TestGraduatedValue("Round Decimal Up", 1880, 1, false, "1.9K");
TestGraduatedValue("Round After AddDec", 1880, 0, true, "1.9K");

TestGraduatedValue("Bankers Rounding Up", 1500, 0, false, "2K");
TestGraduatedValue("Bankers Rounding Down", 4500, 0, false, "4K");

TestGraduatedValue("Small Negatives", -123, 0, false, "-123");
TestGraduatedValue("Negatives", -1000, 0, false, "-1K");
TestGraduatedValue("Negatives Bankers Up", -1500, 0, false, "-2K");
TestGraduatedValue("Bankers Bankers Down", -4500, 0, false, "-4K");

TestGraduatedValue("Large With Decimals", 9372036854775807, 1, true, "9.37Q");
TestGraduatedValue("Large No More Grad", 9223372036854775807, 0, true, "9223Q");

TestGraduatedValue("No Premature Grad", 999, 0, false, "999");*/

/*Failed for Simplest Low Value: Failed with [0K] rather than [123]
Failed for Zero: Failed with [0K] rather than [0]
Failed for Decimal Zeros: Failed with [0K] rather than [0.0000]
Succeeded for Simple One Grad
Succeeded for Simple With Decimals
Succeeded for AddDec Shouldnt Affect
Failed for Zero AddDec: Failed with [0K] rather than [0.0]
Succeeded for Round Integer Up
Succeeded for Round Decimal Up
Succeeded for Round After AddDec
Succeeded for Bankers Rounding Up
Succeeded for Bankers Rounding Down
Failed for Small Negatives: Failed with [0K] rather than [-123]
Succeeded for Negatives
Succeeded for Negatives Bankers Up
Succeeded for Bankers Bankers Down
Failed for Large With Decimals: Failed with [9Q] rather than [9.37Q]
Succeeded for Large No More Grad
Failed for No Premature Grad: Failed with [1K] rather than [999]
*/