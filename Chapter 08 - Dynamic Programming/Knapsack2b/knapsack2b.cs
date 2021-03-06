﻿namespace Knapsack2b
{
    using System;

    public class Knapsack2B
    {
        private const int NotCalculated = -1;
        private const int MaxN = 30;
        private const int MaxCapacity = 1000;
        private const int TotalCapacity = 70; /* Обща вместимост на раницата */
        private const int N = 8; /* Брой предмети */

        private static readonly int[,] Set = new int[MaxCapacity, MaxN / 8];
        private static readonly int[] Fn = new int[MaxCapacity];
        private static readonly int[] Weights = new int[] { 0, 30, 15, 50, 10, 20, 40, 5, 65 }; /* Тегла */
        private static readonly int[] Values = new int[] { 0, 5, 3, 9, 1, 2, 7, 1, 12 }; /* Стойности */

        internal static void Main()
        {
            Console.WriteLine("Брой предмети: {0}", N);
            Console.WriteLine("Вместимост на раницата: {0}", TotalCapacity);
            Calculate();
        }

        private static void Calculate()
        {
            int maxValue,
                /* Максимална постигната стойност */
                maxIndex; /* Индекс, за който е постигната */

            /* Пресмятаме стойностите на целевата функция */
            for (int i = 1; i <= TotalCapacity; i++)
            {
                /* Търсене макс. стойност на F(i) */
                maxValue = maxIndex = 0;
                for (int j = 1; j <= N; j++)
                {
                    if (Weights[j] <= i && ((Set[i - Weights[j], j >> 3] & (1 << (j & 7))) == 0))
                    {
                        if (Values[j] + Fn[i - Weights[j]] > maxValue)
                        {
                            maxValue = Values[j] + Fn[i - Weights[j]];
                            maxIndex = j;
                        }
                    }
                }

                if (maxIndex > 0)
                {
                    /* Има ли предмет с тегло по-малко от i? */
                    Fn[i] = maxValue;
                    /* Новото множество set[i] се получава от set[i-m[maxIndex]]
                * чрез прибавяне на елемента maxIndex */
                    int count = (N >> 3) + 1;
                    for (int p = 0; p < count; p++)
                    {
                        Set[i, p] = Set[i - Weights[maxIndex], p];
                    }

                    Set[i, maxIndex >> 3] |= 1 << (maxIndex & 7);
                }

                if (Fn[i] < Fn[i - 1])
                {
                    /* Побират се всички предмети и още */
                    Fn[i] = Fn[i - 1];
                    int count = (N >> 3) + 1;
                    for (int p = 0; p < count; p++)
                    {
                        Set[i, p] = Set[i - 1, p];
                    }
                }
            }

            /* Извеждане на резултата */
            Console.Write("Вземете предметите с номера: ");
            for (int i = 1; i <= N; i++)
            {
                if ((Set[TotalCapacity, i >> 3] & (1 << (i & 7))) != 0)
                {
                    Console.Write("{0} ", i);
                }
            }

            Console.WriteLine("\nМаксимална постигната стойност: {0}", Fn[TotalCapacity]);
        }
    }
}