using System.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace euler
{
    class Problem
    {

        public static void Main(string[] args)
        {
            SortedSet<int> primes = new CircularPrimes().GetPrimes(1000000);
            //foreach (int prime in primes)
            //{
            //    Console.Write(prime + ", ");
            //}
            Console.Read();
        }
    }

    class CircularPrimes
    {
        public SortedSet<int> GetPrimes(int number)
        {
            SortedSet<int> primes = new SortedSet<int>();
            BitArray input = new BitArray(number + 1);
            int sqrtBound = (int)Math.Sqrt((double)number);
            primes.Add(2);
            primes.Add(3);
            primes.Add(5);

            for (int x = 0; x <= sqrtBound; x++)
            {
                for (int y = 0; y <= sqrtBound; y++)
                {
                    int firstExp = (int)(4 * Math.Pow(x, 2) + Math.Pow(y, 2));
                    int secondExp = (int)(3 * Math.Pow(x, 2) + Math.Pow(y, 2));
                    int? thirdExp = x > y ? (int?)(3 * Math.Pow(x, 2) - Math.Pow(y, 2)) : null;
                    if (firstExp <= number
                        && (firstExp % 12 == 1 || firstExp % 12 == 5)
                        && firstExp % 2 != 0
                        && firstExp % 3 != 0
                        && firstExp % 5 != 0)
                    {
                        input.Set(firstExp, !input[firstExp]);
                        if (Math.Pow(firstExp, 2) < input.Length)
                        {
                            input.Set((int)Math.Pow(firstExp, 2)
                                , false);
                        }
                    }
                    if (secondExp <= number
                        && secondExp % 12 == 7
                        && secondExp % 2 != 0
                        && secondExp % 3 != 0
                        && secondExp % 5 != 0)
                    {
                        input.Set(secondExp, !input[secondExp]);
                        if (Math.Pow(secondExp, 2) < input.Length)
                        {
                            input.Set((int)Math.Pow(secondExp, 2)
                                , false);
                        }
                    }
                    if (thirdExp != null 
                        && thirdExp <= number && thirdExp % 12 == 11 
                        && thirdExp % 2 != 0
                        && thirdExp % 3 != 0
                        && thirdExp % 5 != 0)
                    {
                        input.Set((int)thirdExp, !input[(int)thirdExp]);
                        if (Math.Pow((int)thirdExp, 2) < input.Length)
                        {
                            input.Set((int)Math.Pow((int)thirdExp, 2)
                                , false);
                        }
                    }
                }
            }
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i])
                {
                    primes.Add(i);
                }
            }
            return primes;
        }
    }
}
