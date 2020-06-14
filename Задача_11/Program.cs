using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Задача_11
{
    class Program
    {
        static int[] sortInsert(int[] arr)
        {
            int[] tmp = new int[arr.Length];
            for (int i = 1; i < arr.Length; i++) tmp[i] = arr[i];
            for (int i = 1; i < tmp.Length; i++)
            {
                for (int j = i; j > 0; j--)
                {
                    if (tmp[j - 1] > tmp[j])
                    {
                        int temp = tmp[j];
                        tmp[j] = tmp[j - 1];
                        tmp[j - 1] = temp;
                    }
                }
            }
            return tmp;
        }
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
                    if (number < 1 )
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
        static int[] ImpNumberS(int length)
        {
            int[] arr = new int[length];
            for (int i = 0; i < length; i++)
                arr[i] = ImpIntNumber($"Введите число {i + 1}: от 1 до {length}");
            int[] tmp = sortInsert(arr);
            for (int i = 0; i < length - 1; i++)
            {
                if (tmp[i + 1] - tmp[i] != 1)
                {
                    Console.WriteLine("Ошибка - заданы неверные числа");
                    ImpNumberS(length);
                    break;
                }
            }
            return arr;
        }
        static string ImpStr(string s, int length)
        {
            string str;
            do
            {
                Console.WriteLine(s);
                str = Console.ReadLine();
                if (str.Length > length) Console.WriteLine("Строка слишком длинная");
            } while (str.Length > length);
            return str;
        }
        static string Encryption(string s, int length, int[] numbers)
        {
            if (s.Length < length)
            {
                for (int i = 0; i <= length - s.Length; i++) s += " ";
            }
            char[] strHelp = new char[s.Length];
            for (int i = 0; i < numbers.Length; i++)  strHelp[i] = s[numbers[i] - 1];
            return new string(strHelp);
        }
        static string Decryption(string s, int[] numbers)
        {
            char[] strHelp = new char[s.Length]; 
            for (int i = 0; i < numbers.Length; i++) strHelp[numbers[i] - 1] = s[i];
            return new string(strHelp);
        }
        static void Main(string[] args)
        {
            int n = ImpIntNumber("Введите длину строки: ");
            string s;
            int[] numbers = ImpNumberS(n);
            s = ImpStr("Введите строку:", n);

            s = Encryption(s, n, numbers);
            Console.WriteLine("Шифровка:");
            Console.WriteLine(s);
            
            s = Decryption(s, numbers);
            Console.WriteLine("Дешифровка:");
            Console.WriteLine(s);
        }
    }
}

