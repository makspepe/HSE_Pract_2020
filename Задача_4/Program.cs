using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Задача_4
{
    class Program
    {
        static double ImpDoubleNum(string tmp)
        {
            bool ok = false;
            double number = 0;
            do
            {
                Console.Write(tmp);
                try
                {
                    string tmp1 = Console.ReadLine();
                    number = Convert.ToDouble(tmp1);
                    if (number > 0) ok = true;
                    else
                    {
                        Console.WriteLine("Ошибка - введите положительное число");
                        ok = false;
                    }

                    }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка - введите положительное число");
                    ok = false;
                }
            } while (!ok);
            return number;
        }
        static void Main(string[] args)
        {
            double eps = ImpDoubleNum("Введите требуемую точность: ");
            double sum = 0;
            double znach;
            double i = 1;
            do
            {
                znach = (1 / (i * (i + 1)));  //ряд задачи
                    i++;
                sum += znach;
            } while (znach >= eps);  //условие задачи
            sum -= znach;            //

            Console.WriteLine($"Сумма ряда: {sum:N11}");
            Console.ReadLine();
        }
    }
}
