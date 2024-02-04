using System;
using System.Linq;
using System.Numerics;
using System.Text;

namespace GigaInteger
{
    internal class Program
    {

        static void Main(string[] args)
        {
            /*            Console.WriteLine("Write the first input: ");
                        GigaInt n1 = Console.ReadLine();

                        Console.WriteLine("Write the second input: ");
                        GigaInt n2 = Console.ReadLine();

                        Console.WriteLine("Sum: " + (n1 + n2).ToString());
            */

            Console.WriteLine("Press any key to start addition test");
            Console.ReadKey();


            Console.WriteLine("Testing....");

            bool testPassed = true;
            int progress = 0;
            for (int i = 0; i < BigInteger.Parse("10000000"); i++)
            {
                BigInteger a = i;
                BigInteger b = i;

                GigaInt ag = i;
                GigaInt bg = i;

                if (a + b != Convert.ToInt32((ag + bg).ToString()))
                {
                    testPassed = false;
                    Console.WriteLine($"WRONG OUTPUT. i = {i}. i + i = {a + b}. GigaInt returned {ag + bg}");
                }

                if (progress % 100000 == 0) Console.WriteLine(progress + " tests done.");
                progress++;



            }
            Console.WriteLine($"{(testPassed ? "All tests passed" : "Some or all tests failed")}");

        }
    }

    public class GigaInt
    {
        public readonly string value;

        #region Constructors
        private GigaInt(string value) => this.value = value;

        private GigaInt(int value) => this.value = value.ToString();

        private GigaInt(long value) => this.value = value.ToString();

        public int[] StringToDigits(string value)
        {

            int[] digits = new int[value.Length];
            for (int i = 0; i < value.Length; i++)
                digits[i] = int.Parse(value[i].ToString());

            return digits;
        }

        #endregion


        #region Value Setting
        public static implicit operator GigaInt(int value) => new GigaInt(value);
        public static implicit operator GigaInt(long value) => new GigaInt(value);
        public static implicit operator GigaInt(string value) => new GigaInt(value);
        #endregion


        #region Operations

        //Will implement the operators later. These are placeholders


        /// <summary>
        /// Adds two specified GigaInts.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>The sum of left and right</returns>
        public static GigaInt operator +(GigaInt a, GigaInt b)
        {
            StringBuilder result = new StringBuilder();

            bool carry = false;

            for (int i = 0; i < Math.Max(a.value.Length, b.value.Length); i++)
            {
                int r
                    = (i < a.value.Length ? a.value[a.value.Length - 1 - i] - '0' : 0)
                    + (i < b.value.Length ? b.value[b.value.Length - 1 - i] - '0' : 0)
                    + (carry ? 1 : 0);

                result.Append(r%10);
               
                carry = r >= 10;
            }
            if(carry)result.Append("1");    
            char[] chars = result.ToString().ToCharArray();
            Array.Reverse(chars);
            return new GigaInt(new string(chars));
        }

        /// <summary>
        /// Negates two specified GigaInts.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Negation of right from left</returns>
        public static GigaInt operator -(GigaInt a, GigaInt b) => a.value + b.value;

        /// <summary>
        /// Multiplies two specified GigaInts.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Multiplication of left and right</returns>
        public static GigaInt operator *(GigaInt a, GigaInt b) => a.value + b.value;

        /// <summary>
        /// Divides two specified GigaInts.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Left divided by right</returns>
        public static GigaInt operator /(GigaInt a, GigaInt b) => a.value + b.value;

        /// <summary>
        /// Raises a GigaInt to the power of another GigaInt.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Left raised to the power of right</returns>
        public static GigaInt operator ^(GigaInt a, GigaInt b) => a.value + b.value;

        #endregion
        #region Misc
        public override string ToString() => value;
        #endregion
    }
}
