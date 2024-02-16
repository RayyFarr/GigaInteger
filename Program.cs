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
            bool DoHandTest = true;

            int autoTestSpan = 1000;

            bool audio = true;

            if (DoHandTest)
            {
                Console.WriteLine("Write the first input: ");
                GigaInt n1 = Console.ReadLine();
                BigInteger in1 = BigInteger.Parse(n1.ToString());
                Console.WriteLine("Write the second input: ");
                GigaInt n2 = Console.ReadLine();
                BigInteger in2 = BigInteger.Parse(n2.ToString());

                Console.WriteLine($"BigInteger   : ({in1})^({in2})={BigInteger.Log(in1, int.Parse(in2.ToString()))}");
                Console.WriteLine($"GigaInt      : ({n1})^({n2})={GigaInt.Log(n1, n2)}");

                Main(args);
            }

            if (!DoHandTest)
            {
                Console.WriteLine("Press any key to start division test");
                Console.ReadKey();


                Console.WriteLine("Testing....");

                int progress = 0;
                int passed = 0;
                int failed = 0;
                for (int i = -autoTestSpan; i < autoTestSpan; i++)
                {
                    for (int j = -autoTestSpan; j < autoTestSpan; j++)
                    {
                        if (i is 0 || j is 0) continue;

                        int a = i;
                        int b = j;

                        int iResult = a / b;

                        GigaInt ag = i;
                        GigaInt bg = j;

                        GigaInt gResult = ag / bg;

                        if (progress % 10000 == 0) Console.WriteLine(progress + " tests done.");
                        progress++;


                        if (new GigaInt(iResult) != gResult)
                        {
                            Console.WriteLine($"WRONG OUTPUT. i = {i},j = {j} | i + j = {iResult}. GigaInt returned {gResult}");
                            if (audio) Console.Beep();
                            failed++;
                        }
                        else passed++;
                    }
                }
                Console.WriteLine($"Test Count: {progress} | Passed: {passed} | Failed : {failed}");
            }
        }

    }


    public class GigaInt
    {

        #region Variables

        private string Value { get; set; }
        public short Sign { get; set; }

        public const int NEGATIVE = -1;
        public const int POSITIVE = 1;

        #endregion


        #region Constructors
        public GigaInt(string Value)
        {


            if (Value.First() == '-')
            {
                if (Value == "-0" || Value == "-")
                {
                    this.Value = "0";
                    Sign = 1;
                }
                else
                {
                    Sign = -1;
                    this.Value = Value.Substring(1).TrimStart('0');
                }
            }
            else
            {
                Sign = 1;
                this.Value = Value == "0" ? "0" : Value.TrimStart('0');
            }
            if (this.Value == "") this.Value = "0";
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
        public static GigaInt operator +(GigaInt a, GigaInt b) => Add(a, b);



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
        public static GigaInt operator -(GigaInt a, GigaInt b) => Subtract(a, b);



        /// <summary>
        /// Unary negation operator for the GigaInt class. 
        /// If the GigaInt is negative, it returns the absolute Value. 
        /// If the GigaInt is positive, it returns the negated Value.
        /// </summary>
        /// <param name="a">The GigaInt to negate.</param>
        /// <returns>A new GigaInt that is the negation of the input GigaInt.</returns>
        public static GigaInt operator -(GigaInt a) => a.Sign == -1 ? a.Value : "-" + a.Value;



        /// <summary>
        /// Multiplies two specified GigaInts.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Multiplication of left and right</returns>
        public static GigaInt operator *(GigaInt a, GigaInt b) => Multiply(a, b);



        /// <summary>
        /// Divides two specified GigaInts.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Left divided by right</returns>
        public static GigaInt operator /(GigaInt a, GigaInt b) => Divide(a, b);


        /// <summary>
        /// Does the modulo operation on two GigaInts.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>The remainder when left is divided by right</returns>
        public static GigaInt operator %(GigaInt a, GigaInt b) => Mod(a, b);

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


        #region Public Static Methods



        /// <summary>
        /// Adds two GigaInts
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>right added to left</returns>
        public static GigaInt Add(GigaInt a, GigaInt b)
        {
            //Checks if any of the values have a sign associated with it then it adds or subtracts the absolute value
            //Depending on the signs.

            //Standard format a+b.

            //when (a>0,b<0) => |a|-|b|
            if (a.Sign is POSITIVE && b.Sign is NEGATIVE) return Subtract(a.Value, b.Value);
            //(a < 0,b > 0)  => |b|-|a|
            else if (a.Sign is NEGATIVE && b.Sign is POSITIVE) return Subtract(b.Value, a.Value);
            //(a < 0,b < 0)  => -(|a| + |b|)
            else if (a.Sign is NEGATIVE && b.Sign is NEGATIVE) return "-" + Add(a.Value, b.Value).ToString();
            //(a > 0, b > 0) => |a| + |b|
            else
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
        }



        /// <summary>
        /// Subtracts a GigaInt value from a GigaInt value.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Right subtracted from Left</returns>
        public static GigaInt Subtract(GigaInt a, GigaInt b)
        {
            //Handles a-b.
            //a.value gives absolute.

            //for (a>0,b<0) =>  |a| + |b|
            if (a.Sign is POSITIVE && b.Sign is NEGATIVE) return Add(a.Value, b.Value);
            //(a < 0,b > 0) =>  -(|a| + |b|)
            else if (a.Sign is NEGATIVE && b.Sign is POSITIVE) return '-' + Add(a.Value, b.Value).ToString();
            //(a<0,b<0) => |b| - |a|
            else if (a.Sign is NEGATIVE && b.Sign is NEGATIVE) return Subtract(b.Value, a.Value);
            //(a>0,b>0) => |a| - |b|
            else
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
        }



        /// <summary>
        /// Multiplies two GigaInt value
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Left multiplied by right</returns>
        public static GigaInt Multiply(GigaInt a, GigaInt b)
        {
            if (a.Sign is NEGATIVE && b.Sign is NEGATIVE) return Multiply(a.Value, b.Value);
            else if (a.Sign is NEGATIVE || b.Sign is NEGATIVE) return "-" + Multiply(a.Value, b.Value).ToString();
            else
            {
                GigaInt result = 0;

                GigaInt multiplied = a > b ? a : b;
                GigaInt multiplier = a < b ? a : b;

                string zeros = "";

                for (int i = multiplier.Length - 1; i >= 0; i--)
                {
                    StringBuilder layerSum = new StringBuilder();
                    int d1 = multiplier.Value[i] - '0';

                    int carry = 0;

                    for (int j = multiplied.Length - 1; j >= 0; j--)
                    {
                        int d2 = multiplied.Value[j] - '0';
                        int product = d1 * d2 + carry;
                        int val = product % 10;
                        carry = product / 10;

                        layerSum.Insert(0, val.ToString());
                    }

                    if (multiplied.Length >= 2 || multiplied.Length >= 2)
                    {
                        int first = layerSum[0] - '0';
                        layerSum.Remove(0, 1);
                        layerSum.Insert(0, carry.ToString() + first.ToString());
                    }
                    else
                    {
                        layerSum.Insert(0, carry == 0 ? "" : carry.ToString());
                    }
                    result += layerSum.ToString() + zeros;

                    zeros += "0";
                }

                return result;
            }
        }



        /// <summary>
        /// Divides a GigaInt value by another GigaInt value.
        /// </summary>
        /// <param name="a">Left</param>
        /// <param name="b">Right</param>
        /// <returns>Left divided by right</returns>
        /// <exception cref="DivideByZeroException">When right is 0</exception>
        public static GigaInt Divide(GigaInt a, GigaInt b)
        {
            if (a.Sign is NEGATIVE && b.Sign is NEGATIVE) return Divide(a.Value, b.Value).ToString();
            else if (a.Sign is NEGATIVE || b.Sign is NEGATIVE) return "-" + Divide(a.Value, b.Value).ToString();
            else
            {
                if (b == 0) throw new DivideByZeroException("Attempted Division by zero");
                else if (b > a) return 0;
                else if (a == b) return 1;
                else
                {
                    string quotient = "";

                    string remainder = a.Value;

                    while (remainder.ToString() >= b)
                    {

                        string shortest = "";

                        for (int i = 0; i < remainder.Length; i++)
                        {
                            shortest += remainder[i];

                            string constShortest;
                            if (shortest >= b)
                            {
                                constShortest = shortest;
                                int count = 0;
                                while (shortest >= b)
                                {
                                    shortest = (shortest - b).Value;
                                    count++;
                                }

                                int zeroCount = remainder.Length - constShortest.Length;

                                quotient
                                    = quotient != "" ?
                                    ((GigaInt)quotient + (count + (zeroCount >= 1 ? new string('0', zeroCount) : ""))).Value
                                    : (count + (zeroCount >= 1 ? new string('0', zeroCount) : ""));


                                remainder = (constShortest - (b * count)).Value + remainder.Substring(constShortest.Length);
                            }
                        }
                    }

                    return quotient;
                }
            }
        }



        /// <summary>
        /// Does Modulo operation on two GigaInts.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Left Mod Right A.K.A the remainder of Left divided by Right.</returns>
        /// <exception cref="DivideByZeroException"></exception>
        public static GigaInt Mod(GigaInt a, GigaInt b) => a - (b * Divide(a, b));


        /// <summary>
        /// Raises a GigaInt to the power of another Gigaint
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="n">n</param>
        /// <returns>x raised to the power of n</returns>
        public static GigaInt Pow(GigaInt x, GigaInt n)
        {
            if (n < 0) throw new Exception("Exponent must be non-negative!");

            if (n == 0 || x == 1) return 1;
            int count = 0;
            GigaInt mult = 1;
            while (n > 1)
            {
                count++;
                if (n % 2 != 0) mult *= x;

                x *= x;
                n /= 2;
            }
            return mult * x;
        }


        /// <summary>
        /// Gets the square root of a GigaInt.
        /// </summary>
        /// <param name="x"></param>
        /// <returns>The square root of x</returns>
        public static GigaInt Sqrt(GigaInt x)
        {
            if (x <= 1) return 0;

            GigaInt start = 0;
            GigaInt end = x / 2;

            GigaInt mid;

            while (start < end)
            {
                mid = (start + end) / 2;

                if (mid * mid < x)
                {
                    start = mid + 1;
                    if (start * start > x) return mid;
                    else if (start * start == x) return start;
                }
                else if (mid * mid < x)
                {
                    end = mid - 1;
                    if (end * end <= x) return end;
                }
                else return mid;
            }
            return start;
        }
        

        /// <summary>
        /// A method to get the Log of GigaInt.
        /// </summary>
        /// <param name="x"></param>
        /// <returns>The Base-10 Logarithm of the x</returns>
        public static GigaInt Log10(GigaInt x)
        {
            if (x <= 0)
                throw new ArithmeticException("Cannot compute the logarithm of a negative number or zero.");

            int i = 0;
            while ((x/=10) >= 1) i++;
            return i;
        }


        /// <summary>
        /// Gets the log of a GigaInt value.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="n"></param>
        /// <returns>The Base-n log of x</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArithmeticException"></exception>

        public static GigaInt Log(GigaInt x,GigaInt n)
        {
            //Exception handling.
            if(x < 0)
                throw new ArithmeticException("Cannot compute the logarithm of a negative number or zero.");
            else if (n <= 1)
                throw new ArgumentException("GigaInt does not support logarithms with the base equal or less than 1.");
                             
            int i = 0;
            while ((x/=n) >= 1) i++;
            return i;
        }
        #endregion


        #region Special Methods

        /// <summary>
        /// Returns the sign of the value "-1" if negative "1" if positive.
        /// </summary>
        public string StringSign => Sign == -1 ? "-" : "";

        /// <summary>
        /// Returns the sign of the value -1 if negative 1 if positive.
        /// </summary>
        public int IntSign => Sign;

        /// <summary>
        /// Returns the length of the absolute value.
        /// </summary>
        private int Length => Value.Length;


        /// <summary>
        /// Returns the value parsed to an int.
        /// </summary>
        public int IntValue => int.Parse(Sign.ToString() + Value);
        /// <summary>
        /// Returns the value parsed to a long.
        /// </summary>
        public long LongValue => long.Parse(Sign.ToString() + Value);

        /// <summary>
        /// Returns the absolute value of this GigaInt.
        /// </summary>
        public string AbsoluteValue => Value;

        /// <summary>
        /// Represents the GigaInt value 1.
        /// </summary>
        public static GigaInt One => 1;

        /// <summary>
        /// Sets the sign to the sign of the input integer.
        /// </summary>
        /// <param name="Sign"></param>
        public void SetSign(int Sign) => this.Sign = (short)Math.Sign(Sign);


        public override bool Equals(object obj) => obj is GigaInt gigaInt ? Value == gigaInt.Value && Sign == gigaInt.Sign : false;


        public override int GetHashCode() => HashCode.Combine(Value, Sign);


        #endregion


        #region Misc
        public override string ToString() => StringSign + (Value == "" ? "0" : Value);
        #endregion
    }
}
