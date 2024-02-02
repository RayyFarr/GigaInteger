using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaInteger
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Write the first input: ");
            GigaInt n1 = Console.ReadLine();

            Console.WriteLine("Write the second input: ");    
            GigaInt n2 = Console.ReadLine();

            Console.WriteLine("Sum: " + (n1 + n2).ToString());

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

        private GigaInt(int[] digits)
        {
            this.digits = digits;
            string v = "";
            foreach (int digit in digits) v += digit.ToString();
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
        public static GigaInt operator +(GigaInt a, GigaInt b)
        {
            string result = "";

            bool carry = false;

            for(int i = Math.Max(a.digits.Length,b.digits.Length)-1;i>=0;i--)
            {
                int r = (i<a.digits.Length?a.digits[i]:0) + (i<b.digits.Length?b.digits[i]:0) + (carry?1:0);
                result = r%10==0?"0":(r % 10).ToString() + result;
                if (r >= 10)
                {
                    carry = true;
                }
            }

            return new GigaInt((carry?"1":"") + result);
        }
        public static GigaInt operator -(GigaInt a, GigaInt b) => a.value + b.value;
        public static GigaInt operator *(GigaInt a, GigaInt b) => a.value + b.value;
        public static GigaInt operator /(GigaInt a, GigaInt b) => a.value + b.value;
        public static GigaInt operator ^(GigaInt a, GigaInt b) => a.value + b.value;

        #endregion
        #region Misc
        public override string ToString() => value;
        #endregion
    }
}
