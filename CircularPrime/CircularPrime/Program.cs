using System.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;


namespace euler
{
    class Problem
    {

        public static void Main(string[] args)
        {
            SortedSet<int> circularPrimes = new CircularPrimes().GetCircularPrimes(1000000);
            SortedSet<int> primes = new CircularPrimes().GetPrimes(1000000);
            int columnCount = 0;
            foreach (int prime in circularPrimes)
            {
                Console.Write(prime + ", ");
                columnCount++;
                if (columnCount == 4)
                {
                    columnCount = 0;
                    Console.WriteLine();
                }
            }
            Console.Write("Circular count=" + circularPrimes.Count);
            Console.Read();
        }
    }

    class CircularPrimes
    {
        private SortedSet<int> primes = new SortedSet<int>();
        public SortedSet<int> GetPrimes(int number)
        {
            SortedSet<int> primes = new SortedSet<int>();
            BitArray input = new BitArray(number + 1);
            Dictionary<int, int> findedCounter = new Dictionary<int, int>(number + 1);
            int sqrtBound = (int)Math.Sqrt((double)number);
            primes.Add(2);
            primes.Add(3);
            primes.Add(5);

            for (int x = 0; x <= sqrtBound; x++)
            {
                int x2 = (int)Math.Pow(x, 2);
                for (int y = 0; y <= sqrtBound; y++)
                {
                    int y2 = (int)Math.Pow(y, 2);
                    int firstExp = 4 * x2 + y2;
                    int secondExp = 3 * x2 + y2;
                    int thirdExp = 3 * x2 - y2;
                    if (firstExp <= number
                        && (firstExp % 12 == 1 || firstExp % 12 == 5)
                        && firstExp % 2 != 0
                        && firstExp % 3 != 0
                        && firstExp % 5 != 0)
                    {
                        if (!findedCounter.ContainsKey(firstExp))
                        {
                            input.Set(firstExp, true);
                            findedCounter.Add(firstExp, 1);
                        }
                        else
                        {
                            input.Set(firstExp, false);
                            findedCounter[firstExp] += 1;
                        }
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
                        if (!findedCounter.ContainsKey(secondExp))
                        {
                            input.Set(secondExp, true);
                            findedCounter.Add(secondExp, 1);
                        }
                        else
                        {
                            input.Set(secondExp, false);
                            findedCounter[secondExp] += 1;
                        }
                        if (Math.Pow(secondExp, 2) < input.Length)
                        {
                            input.Set((int)Math.Pow(secondExp, 2)
                                , false);
                        }
                    }
                    if (x > y && thirdExp <= number && thirdExp % 12 == 11
                        && thirdExp % 2 != 0
                        && thirdExp % 3 != 0
                        && thirdExp % 5 != 0)
                    {
                        if (!findedCounter.ContainsKey(thirdExp))
                        {
                            input.Set(thirdExp, true);
                            findedCounter.Add(thirdExp, 1);
                        }
                        else
                        {
                            input.Set(thirdExp, false);
                            findedCounter[thirdExp] += 1;
                        }
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

        public SortedSet<int> GetCircularPrimes(int number)
        {
            primes = GetPrimes(number);
            SortedSet<int> result = FilterPrimes(primes);
            return result;
        }

        public SortedSet<int> FilterPrimes(SortedSet<int> inputPrimes)
        {
            SortedSet<int> result = new SortedSet<int>();
            foreach (int item in inputPrimes)
            {
                int rotateCount = (int)(Math.Floor(Math.Log10(item) + 1) - 1);
                List<int> rotates = new List<int> { item };
                int rotatedItem = item;
                for (int i = 0; i < rotateCount; i++)
                {
                    rotatedItem = Rotate(rotatedItem);
                    if (inputPrimes.Contains(rotatedItem))
                    {
                        rotates.Add(rotatedItem);
                    }
                }
                if (rotates.Count - 1 == rotateCount)
                {
                    foreach (int circularItem in rotates)
                    {
                        result.Add(circularItem);

                    }
                }
            }
            return result;
        }

        public int Rotate(int number)
        {
            return number % 10 * (int)Math.Pow(10, Math.Floor(Math.Log10(number) + 1) - 1)
                + number / 10;

        }
    }
}
