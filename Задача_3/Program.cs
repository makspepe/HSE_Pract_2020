using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Задача_3
{
    class Program
    {

        static bool insideCircle(double x, double y)
        {
            double rad = 1;
            if (x * x + y * y <= rad * rad) return true;  //координаты центра круга - 0.0, поэтому не учитываются в формуле
            return false;
        }
        static bool aboveLine(double x, double y)
        {
            if (y >= x && x > 0) return true;
            if (y >= -x && x <= 0) return true;
            return false;
        }

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
                    ok = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка - Введите число");
                    ok = false;
                }
            } while (!ok);
            return number;
        }
        static void Main(string[] args)
        {
            double x = ImpDoubleNum("Введите число x: ");
            double y = ImpDoubleNum("Введите число y: ");
            double u;
            if (aboveLine(x, y) && insideCircle(x, y))
            {
                u = Math.Sqrt(Math.Abs(x * x - 1));
            }
            else
            {
                u = x + y;
            }
            Console.WriteLine($"U = {u}");
            Console.ReadLine();
        }
    }
}
