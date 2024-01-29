using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaInteger
{
    internal class Program
    {
        static GigaInt num1 = 123;
        static GigaInt num2 = "321";
        static GigaInt num3 = "230124856092837675328479773473675489679234672572439031023712763123";

        static void Main(string[] args)
        {
            Console.WriteLine(num1);
            Console.WriteLine(num2);
            Console.WriteLine(num3);

        }
    }

    public class GigaInt
    {
        public readonly string value;
        public readonly int[] digits;

        #region Constructors
        private GigaInt(string value)
        {
            this.value = value;
            digits = StringToDigits(value);
        }
        private GigaInt(int value)
        {
            this.value = value.ToString();
            digits = StringToDigits(value.ToString());
        }
        private GigaInt(long value)
        {         
            this.value = value.ToString();
            digits = StringToDigits(value.ToString());
        }

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
        public static GigaInt operator +(GigaInt a, GigaInt b) => a.value + b.value;
        public static GigaInt operator -(GigaInt a, GigaInt b) => a.value + b.value;
        public static GigaInt operator *(GigaInt a, GigaInt b)
        {
            int[] digitsA = a.digits;
            int[] digitsB = b.digits;

            int[] multiplied = a.digits.Length > b.digits.Length ? a.digits : b.digits;
            for (int i = 0; i < Math.Min(digitsA.Length, digitsB.Length); i++)
            {

            }
        }
        public static GigaInt operator /(GigaInt a, GigaInt b) => a.value + b.value;
        public static GigaInt operator ^(GigaInt a, GigaInt b) => a.value + b.value;

        #endregion

        
        #region Misc
        public override string ToString() => value;
        #endregion
    }
}
