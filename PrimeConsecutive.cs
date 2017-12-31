using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
      /*  public static void Main()
        {
            Char[] cr = new Char[1];
            cr[0] = ' ';

            String sb = Console.ReadLine();
            string[] inputNumbers = sb.Split(cr);
           int count= GeneratePrimeNumbers(Convert.ToInt32(inputNumbers[0]), Convert.ToInt32(inputNumbers[1]));
           Console.WriteLine(count);
            Console.ReadKey();

        } */

        private static int GeneratePrimeNumbers(int minimum, int maximum)
        {

            List<int> resultCollection = new List<int>();

            if (minimum<=2 && maximum>=2)
                resultCollection.Add(2);
         
            for (int i = 3; i<=maximum; i++)
            {
                if (i < 3)
                    continue;

                if (IsPrime(i))
                   resultCollection.Add(i);
            }



            int count = 0;
            for (int i = 0; i < resultCollection.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {

                        if(resultCollection[j]+ resultCollection[j+1] +1 == resultCollection[i])
                        {
                            count++;
                            break;
                        }
                    

                } 
            }
            return count;

        }


        static bool IsPrime(int num)
        {
            if (num % 2 == 0)
                return false;

            int divisorLimit = (int)Math.Sqrt(num); // we have at least one divisior below square root of num 

            for (int i = 3; i <= divisorLimit; i += 2)
            {
                if (num % i == 0)
                    return false;
            }

            return true;
        }
    }
}
