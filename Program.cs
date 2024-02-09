﻿using System;
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

            if (DoHandTest)
            {
                Console.WriteLine("Write the first input: ");
                GigaInt n1 = Console.ReadLine();
                BigInteger in1 = BigInteger.Parse(n1.ToString());
                Console.WriteLine("Write the second input: ");
                GigaInt n2 = Console.ReadLine();
                BigInteger in2 = BigInteger.Parse(n2.ToString());

                Console.WriteLine($"BigInteger   : ({in1})*({in2})={in1*in2}");
                Console.WriteLine($"GigaInt      : ({n1})*({n2})={n1 * n2}");

                Main(args);
            }
            if (!DoHandTest)
            {
                Console.WriteLine("Press any key to start addition test");
                Console.ReadKey();


                Console.WriteLine("Testing....");

                int progress = 0;
                int passed = 0;
                int failed = 0;
                for (int i = -1000; i < 1000; i++)
                {
                    for (int j = -1000; j < 1000; j++)
                    {
                        int a = i;
                        int b = j;

                        int iResult = a * b;

                        GigaInt ag = i;
                        GigaInt bg = j;

                        GigaInt gResult = ag * bg;

                        if (progress % 10000 == 0) Console.WriteLine(progress + " tests done.");
                        progress++;


                        if (new GigaInt(iResult) != gResult)
                        {
                            Console.WriteLine($"WRONG OUTPUT. i = {i},j = {j}. i * j = {iResult}. GigaInt returned {gResult}");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
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
        private static GigaInt Subtract(GigaInt a, GigaInt b)
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
        private static GigaInt Multiply(GigaInt a, GigaInt b)
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

                    for (int j = multiplied.Length-1; j >= 0; j--)
                    {
                        int d2 = multiplied.Value[j] - '0';
                        int product = d1 * d2 + carry;
                        int val = product % 10;
                        carry = product / 10;

                        layerSum.Insert(0,val.ToString());
                    }

                    if (multiplied.Length >= 2 || multiplied.Length >= 2)
                    {
                        int first = layerSum[0]-'0';
                        layerSum.Remove(0, 1);
                        layerSum.Insert(0, carry.ToString()+first.ToString());
                    }
                    else
                    {
                        layerSum.Insert(0,carry==0 ? "" : carry.ToString());
                    }
                    result += layerSum.ToString() + zeros;

                    zeros += "0";
                }

                return result;
            }
        }
        #endregion



        #region Special Methods

        public string StringSign => Sign == -1 ? "-" : "";
        public int intSign => Sign;

        public int Length => Value.Length;
        public string AbsoluteValue => Value;


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
        public override string ToString() => StringSign + (Value == "" ? "0" : Value);
        #endregion
    }
}
