using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Задача_12
{
    public class Program
    {
        //Ввод целого положительного числа
        static int ImpIntNumber(string tmp)
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
                    if (number < 1)
                    {
                        Console.WriteLine($"Ошибка - введите целое положительное число");
                        ok = false;
                    }
                    ok = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Ошибка - введите целое положительное число");
                    ok = false;
                }
            } while (!ok);
            return number;
        }
        public static int[] Sort(int[] array) //Сортировка массива
        {
            Array.Sort(array);
            return array;
        }
        public static int[] Reverse(int[] array) //Переворот
        {
            Array.Reverse(array);
            return array;
        }
        public static int[] CreateRandomArray(int size)
        {
            Random random = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
                array[i] = random.Next(100, 1000);

            return array;
        }
        private static void Print(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
                Console.Write(array[i] + " ");

            Console.WriteLine();
        }
        //Быстрая сортировка
        public static int[] QuickSort(int[] arr, int left, int right, ref ulong movings, ref ulong comparisons)
        {
            int i = left, j = right;
            int buf = arr[(left + right) / 2];
            do
            {
                while (arr[i] < buf) i++;
                while (arr[j] > buf) j--;
                if (i <= j)
                {
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                    i++;
                    j--;
                    movings++;
                }
                comparisons++;
            } while (i <= j);
            if (left < j) QuickSort(arr, left, j, ref movings, ref comparisons);
            if (i < right) QuickSort(arr, i, right, ref movings, ref comparisons);
            return arr;
        }

        //Печать массива, сортировка массива по алгоритму быстрой сортировки 
        //и печать отсортированного массива, перемещений и сравнений
        private static void QuickSortPrintArr(int[] arr)
        {
            ulong movings = 0;
            ulong comparisons = 0;
            Console.Write("Массив: ");
            Print(arr);
            Console.Write("Отсортированный массив: ");
            arr = QuickSort(arr, 0, arr.Length - 1, ref movings, ref comparisons);
            Print(arr);
            Console.WriteLine("Перемещений: " + movings);
            Console.WriteLine("Сравнений: " + comparisons);
        }

        //Поразрядная сортировка - Radix sort - очень интересный по задумке и реализации алгоритм. 
        //Быстрый, но не универсальный.
        //Его недостаток в том, что он работает только с целыми числами, но выполняется за линейное время, с постоянным количеством сравнений. 

        //Поразрядная сортировка
        public static int[] RadSort(int[] arr, ref ulong movings, ref ulong comparisons)
        {
            int i, j;
            var tmp = new int[arr.Length];
            for (int shift = sizeof(int) * 8 - 1; shift > -1; --shift)  //поддержка сортировки длины значений int
            {
                comparisons++;  //количество сравнений для int будет неизменно, тк 4(int) * 8бит = 32
                j = 0;
                for (i = 0; i < arr.Length; i++)
                {
                    var move = (arr[i] << shift) >= 0;

                    if (shift == 0 ? !move : move)
                    {
                        arr[i - j] = arr[i];  //перемещение нулей в начало arr
                        movings++;
                    }
                    else
                    {
                        tmp[j++] = arr[i]; //перемещение не нулей в tmp
                        movings++; 
                    }
                }
                Array.Copy(tmp, 0, arr, arr.Length - j, j);
            }
            return arr;
        }

        //Печать массива, сортировка массива по алгоритму поразрядной сортировки
        //и печать отсортированного массива, перемещений и сравнений
        private static void RadSortPrintArr(int[] arr)
        {
            ulong movings = 0;
            ulong comparisons = 0;
            Console.Write("Массив: ");
           Print(arr);
            Console.Write("Отсортированный массив: ");
            arr = RadSort(arr, ref movings, ref comparisons);
            Print(arr);
            Console.WriteLine("Перемещений: " + movings);
            Console.WriteLine("Сравнений: " + comparisons);
        }

        public static void Main(string[] args)
        {
            // Ввод количества элементов в массивах
            int size = ImpIntNumber("Введите количество элементов в массиве: ");

            // Создание массивов для проверки поразрядной сортировки
            int[] Arr1 = CreateRandomArray(size);
            int[] sortArr1 = Sort((int[])Arr1.Clone());
            int[] revArr1 = Reverse((int[])sortArr1.Clone());

            // Создание массивов для проверки быстрой сортировки
            int[] Arr2 = (int[])Arr1.Clone();
            int[] sortArr2 = (int[])sortArr1.Clone();
            int[] revArr2 = (int[])revArr1.Clone();

            Console.WriteLine("\n----------------------------Поразрядная----------------------------");
            //Сортировка неупорядоченного массива        поразрядной сортировкой
            Console.WriteLine("\nПервый неупорядоченный массив");
            RadSortPrintArr(Arr1);

            //Сортировка упорядоченного по возрастанию массива   поразрядной сортировкой  
            Console.WriteLine("\nПервый упорядоченный по возрастанию массив");
            RadSortPrintArr(sortArr1);

            // Сортировка упорядоченного по убыванию массива   поразрядной сортировкой
            Console.WriteLine("\nПервый упорядоченный по убыванию массив");
            RadSortPrintArr(revArr1);

            Console.WriteLine("\n----------------------------Быстрая----------------------------");
            // Сортировка неупорядоченного массива        быстрой сортировкой
            Console.WriteLine("\nВторой неупорядоченный массив");
            QuickSortPrintArr(Arr2);

            // Сортировка упорядоченного по возрастанию массива         быстрой сортировкой
            Console.WriteLine("\nВторой упорядоченный по возрастанию массив");
            QuickSortPrintArr(sortArr2);

            // Сортировка упорядоченного по убыванию массива        быстрой сортировкой
            Console.WriteLine("\nВторой упорядоченный по убыванию массив");
            QuickSortPrintArr(revArr2);

            Console.ReadLine();
        }
    }
}
