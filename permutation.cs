using System;
using System.IO;
using System.Text;
using System.Collections.Generic;


namespace problem1
{
  
    class Permutation
    {
        static string number1, number2;
        static List<int> number1List = new List<int>(), number2List = new List<int>();
        static int[] num1;
        static int[] num2;
        static bool [] flag ;
        static bool foundFlag = false;
        static StringBuilder sb = new StringBuilder();
        static int[] permutationHolder;
        static char[] permutationCharHolder;

        public static void Main()
         {


              foundFlag = false;

              number1 = Console.ReadLine();  // read as a string
              //number2 = Console.ReadLine();
              number2 = Console.ReadLine(); // read as a string
              GenerateAndCheckWithPermutation();

             // if (foundFlag == true)
               //   Console.WriteLine("YES");
              //else
                 // Console.WriteLine("NO"); 

            Console.ReadKey(); 
          // checkdata("1,3,2,1,4,5,7,6,9,5,6,7");

      } 

        static void checkdata(string a)
        {

            HashSet<string> valueSet = new HashSet<string>();

            Char[] cr = new Char[1];
            cr[0] = ',';
            string[] splittedNumbers = a.Split(cr);

            for (int i = 0; i < splittedNumbers.Length; i++) // automatically avoid all duplicates
                valueSet.Add(splittedNumbers[i]);

            List<string> tempList = new List<string>();

            foreach (string value in valueSet)
            {
                tempList.Add(value);

            }

            tempList.Sort();

            string output = "";
            for (int i = 0; i < tempList.Count; i++)
            {

                if (i > 0)
                    output += "," + tempList[i];
                else
                    output += tempList[i];
            }
            Console.WriteLine(output);
            Console.ReadKey();

        }

        public static void GenerateAndCheckWithPermutation()
        {
            Char[] cr = new Char[1];
            cr[0] = ' ';

            string[] inputNumbers1 = number1.Split(cr);
            string[] inputNumbers2 = number2.Split(cr);

            for (int i = 0; i < inputNumbers1.Length; i++)
            {
                if (!inputNumbers1.Equals(" "))
                    number1List.Add(Convert.ToInt32(inputNumbers1[i]));

                if (!inputNumbers2.Equals(" "))
                    number2List.Add(Convert.ToInt32(inputNumbers2[i]));
            }


            num1 = number1List.ToArray();
            num2 = number2List.ToArray();


            // permuteDigits(num1, 0, num1.Length - 1);

          flag = new bool[num1.Length];
          permutationHolder = new int[num1.Length];

            for (int i = 0; i < flag.Length; i++)
            {
                flag[i] = false;
            }
            // only required if we are matching with a specific number
           // foundFlag = false;  
            for (int i = 0; i < num1.Length; i++)
            {
                //	second parameter level=0;
                permuteDigit(i, 0);
               // permuteWithNumber(i, 0);
                
                // only required if we are matching with a specific number
               // if (foundFlag == true)
                // break;
            }

        }

        public static void permuteDigit(int k, int posInNextPerm)
        {
            //permutationHolder: holds the next permutation
            flag[k] = true; //at the beginning set the flag to true
            permutationHolder[posInNextPerm] = num1[k];
            posInNextPerm++;

            if (posInNextPerm == num1.Length)
            {
                
                flag[k] = false; //before returning set the flag to false

                Console.WriteLine();
                for (int l = 0; l < permutationHolder.Length; l++)
                    Console.Write(permutationHolder[l] + " ");

                return;
            }


            for (int i = 0; i < num1.Length; i++)
            {
                if (flag[i] == false)
                {
                    permuteDigit(i, posInNextPerm);
                }
            }

            flag[k] = false; //before returning set the flag to false
        }

       /* public static void permuteChar(int k, int posInNextPerm)
        {
             //permutationHolder: holds the next permutation
            flag[k] = true; //at the beginning set the flag to true
            permutationCharHolder[posInNextPerm] = num1[k];
            posInNextPerm++;

            if (posInNextPerm == num1.Length)
            {

                Console.WriteLine();
                for (int l = 0; l < permutationCharHolder.Length; l++)
                    Console.Write(permutationCharHolder[l] + " ");

                flag[k] = false; //before returning set the flag to false
                return;
            }


            for (int i = 0; i < num1.Length; i++)
            {
                if (flag[i] == false)
                {
                    permuteChar(i, posInNextPerm);
                }
            }

            flag[k] = false; //before returning set the flag to false
        } */

        public static void permuteWithNumber(int k, int posInNextPerm)
        {
            //permutationHolder: holds the next permutation
            // only required if we are matching with a specific number
            // if (foundFlag == true)
            // return;

            flag[k] = true;
            permutationHolder[posInNextPerm] = num1[k];
            posInNextPerm++;

            if (posInNextPerm == num1.Length)
            {

                flag[k] = false; //before returning set the flag to false
                
                // print the permutation
                Console.WriteLine();
                for (int l = 0; l < permutationHolder.Length; l++)
                    Console.Write(permutationHolder[l] +" " );

               /* // This part is required if we are matching with a specific number
                 int matchCount = 0;

                              for (int l = 0; l < permutationHolder.Length; l++)
                                  if (permutationHolder[l] == num2[l])
                                      matchCount++;

                              if (matchCount == num2.Length)
                              {
                                  foundFlag = true;
                            
                              } */

                return;
            }


            for (int i = 0; i < num1.Length; i++)
            {
                if (flag[i] == false)
                {
                    permuteWithNumber(i, posInNextPerm);
                }
            }

            flag[k] = false; //before returning set the flag to false
         }


    }
}