using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Задача_5
{
    class Program
    {
        static int ImpIntNumber(string tmp, int min, int max)
        {
            bool ok = false;
            int number = 0;
            do
            {
                Console.Write(tmp);
                try
                {
                    string tmp1 = Console.ReadLine();
                    number = Convert.ToInt32(tmp1);
                    if ((max >=number) && (number >= min)) ok = true;
                    else
                    {
                        Console.WriteLine($"Ошибка - введите целое число в пределах от {min} по {max}");
                        ok = false;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Ошибка - введите целое число в пределах от {min} по {max}");
                    ok = false;
                }
            } while (!ok);
            return number;
        }
        static int[,] crArr(int n)
        {
            int[,] arr = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++) arr[i, j] = ImpIntNumber($"Элемент {i + 1} строки, {j + 1} столбца: ", -999, 999);
            }
            return arr;
        } //cоздание 
        private static void Show(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)                                             //условие задачи
            { 
                for (int j = 0; j < array.GetLength(1); j++) Console.Write(array[i, j] + " ");      //
                Console.WriteLine();
            }
        }
        
        //нахождение максимального значения в области, заданной условием
        static int SrchMax(int[,] arr)
        {
            int max = arr[0, 0];
            int size = arr.GetLength(0);
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (arr[i, j] > max && (i == j || i + j == size - 1 || (j > i && i + j < size - 1) || (i > j && i + j > size - 1))) //сравнение с макс элементом
                        max = arr[i, j]; //смена макс. если arr[ij] находился внутри условленной задачей зоны
            return max;
        }

        public static void Main(string[] args)
        {
            int n = ImpIntNumber("Введите размер матрицы: ", 1, 20);
            int[,] arr = crArr(n);
            Show(arr);
            int max = SrchMax(arr);
            Console.WriteLine($"\nМаксимальное значение: {max}");
            Console.ReadLine();
        }
    }
}

