using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;

namespace GigaInteger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new GigaInt(1)>new GigaInt(4));
            Console.WriteLine("Write the first input: ");
            GigaInt n1 = Console.ReadLine();

            Console.WriteLine("Write the second input: ");
            GigaInt n2 = Console.ReadLine();

            Console.WriteLine($"({n1})-({n2})={n1-n2}");

            Main(args);
            /*
            Console.WriteLine("Press any key to start addition test");
            Console.ReadKey();


            Console.WriteLine("Testing....");

            bool testPassed = true;
            int progress = 0;
            for (int i = 0; i < 10; i++)
            {
                BigInteger a = i;
                BigInteger b = i;

                GigaInt ag = i;
                GigaInt bg = i;

                GigaInt e = ag + (-bg);
              
                if (a + b != Convert.ToInt32((ag + bg).ToString()))
                {
                    testPassed = false;
                    Console.WriteLine($"WRONG OUTPUT. i = {i}. i + i = {a + b}. GigaInt returned {ag + bg}");
                }

                if (progress % 100000 == 0) Console.WriteLine(progress + " tests done.");
                progress++;



            }
            Console.WriteLine($"{(testPassed ? "All tests passed" : "Some or all tests failed")}");*/
        }

    }
    public class GigaInt
    {

        #region Variables
        private string Value { get; set; }
        private short Sign { get; set; }
        #endregion



        #region Constructors
        public GigaInt(string Value)
        {
            if (Value.First() == '-')
            {
                Sign = -1;
                this.Value = Value == "-0" || Value == "-"?"0":Value.Substring(1).TrimStart('0');
            }
            else
            {
                Sign = 1;
                this.Value = Value == "0"?"0" : Value.TrimStart('0');
            }
        }

        public GigaInt(int Value) : this(Value.ToString()) { }

        public GigaInt(long Value) : this(Value.ToString()) { }


        public int[] StringToDigits(string Value)
        {

            int[] digits = new int[Value.Length];
            for (int i = 0; i < Value.Length; i++)
                digits[i] = int.Parse(Value[i].ToString());

            return digits;
        }

        #endregion



        #region Value Setting
        public static implicit operator GigaInt(int Value) => new GigaInt(Value);
        public static implicit operator GigaInt(long Value) => new GigaInt(Value);
        public static implicit operator GigaInt(string Value) => new GigaInt(Value);
        #endregion



        #region Operators


        /// <summary>
        /// Adds two specified GigaInts.
        /// </summary>
        /// <param name="a">Left</param>
        /// <param name="b">Right</param>
        /// <returns>The sum of left and right</returns>
        public static GigaInt operator +(GigaInt a, GigaInt b)
        {
            if (a.Sign == 1 && b.Sign == 1) return Add(a.Value, b.Value);
            else if (a.Sign == -1 && b.Sign == -1) return '-' + Add(a.Value, b.Value).ToString();
            else if (a.Sign == 1 && b.Sign == -1) return Negate(a.Value, b.Value);
            else if (a.Sign == -1 && b.Sign == 1) return Negate(b.Value, a.Value);
            else return Add(a.Value, b.Value);
        }



        /// <summary>
        /// Multiplies a Value by +1.
        /// </summary>
        /// <param name="a"></param>
        /// <returns>The Value</returns>    
        public static GigaInt operator +(GigaInt a) => a.Value;



        /// <summary>
        /// Negates two specified GigaInts.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Negation of right from left</returns>
        public static GigaInt operator -(GigaInt a, GigaInt b)
        {
            if (a.Sign == 1 && b.Sign == 1) return Negate(a.Value, b.Value);
            else if (a.Sign == -1 && b.Sign == 1) return "-" + Add(a.Value, b.Value).Value;
            else if (a.Sign == 1 && b.Sign == -1) return Add(a.Value,  b.Value);
            else return Negate(b.Value,a.Value);
        }



        /// <summary>
        /// Unary negation operator for the GigaInt class. 
        /// If the GigaInt is negative, it returns the absolute Value. 
        /// If the GigaInt is positive, it returns the negated Value.
        /// </summary>
        /// <param name="a">The GigaInt to negate.</param>
        /// <returns>A new GigaInt that is the negation of the input GigaInt.</returns>
        public static GigaInt operator -(GigaInt a) => a.Value.First() == '-' ? a.Value.Replace("-", "") : '-' + a.Value;



        /// <summary>
        /// Multiplies two specified GigaInts.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Multiplication of left and right</returns>
        public static GigaInt operator *(GigaInt a, GigaInt b) => a.Value + b.Value;



        /// <summary>
        /// Divides two specified GigaInts.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Left divided by right</returns>
        public static GigaInt operator /(GigaInt a, GigaInt b) => a.Value + b.Value;



        /// <summary>
        /// Raises a GigaInt to the power of another GigaInt.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Left raised to the power of right</returns>
        public static GigaInt operator ^(GigaInt a, GigaInt b) => a.Value + b.Value;



        /// <summary>
        /// Returns a value depending on the equality of two GigaInt Values
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>true if a is equal to b</returns>
        public static bool operator ==(GigaInt a, GigaInt b) => a.Equals(b);



        /// <summary>
        /// Returns a value depending on the inequality of two GigaInt values
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(GigaInt a, GigaInt b) => !(a == b);



        /// <summary>
        /// Greater than operator. Checks if a GigaInt is Greater than another GigaInt 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>true if a is greater than b and false if a is less than b</returns>
        public static bool operator >(GigaInt a, GigaInt b)
        {
            if (a.Sign > b.Sign) return true;
            else if (b.Sign > a.Sign) return false;


            if (a.Value.Length != b.Value.Length)
                return a.Sign > -1 ? a.Value.Length > b.Value.Length : b.Value.Length > a.Value.Length;
            else
                for (int i = 0; i < a.Value.Length; i++)
                    if (a.Value[i] != b.Value[i])
                        return a.Sign > 0 ? a.Value[i] > b.Value[i] : b.Value[i] > a.Value[i];

            return false;
        }



        /// <summary>
        /// Less than operator. Checks if a GigaInt is Less than another GigaInt. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>True if a is less than b and false if a is greater than b.</returns>
        public static bool operator <(GigaInt a, GigaInt b) => b > a;


        /// <summary>
        /// Greater or equal operator. Checks if a GigaInt is Greater than equal to another GigaInt.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>true if left is greater or equal to right else false.</returns>
        public static bool operator >=(GigaInt a, GigaInt b) => (a > b) || a == b;

        /// <summary>
        /// Less or equal operator. Checks if a GigaInt is Less than equal to another GigaInt.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>true if left is less or equal to right else false.</returns>
        public static bool operator <=(GigaInt a, GigaInt b) => (a < b) || a == b;
        #endregion



        #region Methods

        /// <summary>
        /// Adds two GigaInts
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>right added to left</returns>
        private static GigaInt Add(GigaInt a, GigaInt b)
        {
            StringBuilder result = new StringBuilder();

            bool carry = false;

            for (int i = 0; i < Math.Max(a.Value.Length, b.Value.Length); i++)
            {
                int r
                    = (i < a.Value.Length ? a.Value[a.Value.Length - 1 - i] - '0' : 0)
                    + (i < b.Value.Length ? b.Value[b.Value.Length - 1 - i] - '0' : 0)
                    + (carry ? 1 : 0);

                result.Append(r % 10);

                carry = r >= 10;
            }
            if (carry) result.Append("1");
            char[] chars = result.ToString().ToCharArray();
            Array.Reverse(chars);
            return new GigaInt(new string(chars));
        }

        /// <summary>
        /// Negates a GigaInt value from a GigaInt value.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>left negated by right</returns>
        private static GigaInt Negate(GigaInt a, GigaInt b)
        {
            StringBuilder result = new StringBuilder();

            int carry = 0;
            bool isNegative = false;

            if (b > a)
            {
                GigaInt temp = a;
                a = b;
                b = temp;
                isNegative = true;
            }

            for (int i = 0; i < Math.Max(a.Value.Length, b.Value.Length); i++)
            {
                int r;

                int aV = i < a.Value.Length ? a.Value[a.Value.Length - 1 - i] - '0' : 0;
                int bV = i < b.Value.Length ? b.Value[b.Value.Length - 1 - i] - '0' : 0;

                if (aV >= bV + carry)
                {
                    r = aV - (bV + carry);
                    carry = 0;
                }
                else
                {
                    r = (aV + 10) - (bV + carry);
                    carry = 1;
                }

                result.Append(r % 10);
            }

            char[] chars = result.ToString().ToCharArray();
            Array.Reverse(chars);

            return new GigaInt((isNegative ? "-" : "") + new string(chars));
        }
            #endregion



        #region Special Methods

            string StringSign => Sign == -1 ? "-" : "";

        public override bool Equals(object obj)
        {
            if (obj is GigaInt gigaInt)
            {
                return Value == gigaInt.Value && Sign == gigaInt.Sign;
            }
            return false;
        }

        public override int GetHashCode() => HashCode.Combine(Value, Sign);


        #endregion



        #region Misc
        public override string ToString() => StringSign + (Value == ""?"0":Value);
        #endregion
    }
}
