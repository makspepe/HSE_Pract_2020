using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Задача_6
{
    class Program
    {
        public static int[] MakeArray(int[] allElems, int[] reqdElems, int[] elemNums, out int[] elements, out int[] eNums) //одна функция to rule them all
        {
            int nMin = 2;  //минимальный номер элемента по условию
            int n = N; //всего
            int pos = 0;
            do
            {   //Расширение массива
                if (nMin > 2)
                {
                    int[] allElemsTMP = new int[allElems.Length + 1];
                    for (int j = 0; j < allElems.Length; j++) allElemsTMP[j] = allElems[j];
                    allElems = new int[allElemsTMP.Length];
                    for (int j = 0; j < allElems.Length; j++) allElems[j] = allElemsTMP[j];
                }
                //рекурсивно вычисл. элемент в общ. массив
                allElems[nMin - 1] = choseElem(a1, a2, a3, nMin);
                
                if (Math.Abs(choseElem(a1, a2, a3, nMin) - choseElem(a1, a2, a3, (nMin - 1))) > e)
                {
                    reqdElems[pos] = choseElem(a1, a2, a3, nMin); //мас. подх. элем.
                    elemNums[pos] = nMin; //мас. номеров подход. элем.
                    pos++;
                    n--; //--кол-во элем для поиска
                }
                nMin++;
            } while (n != 0);
            elements = new int[reqdElems.Length];
            for (int j = 0; j < reqdElems.Length; j++) elements[j] = reqdElems[j];
            eNums = new int[elemNums.Length];
            for (int j = 0; j < elemNums.Length; j++) eNums[j] = elemNums[j];
            return allElems;
        }
        public static int choseElem(int a1, int a2, int a3, int k)
        {
            if (k == 1) return a1;
            if (k == 2) return a2;
            if (k == 3) return a3;

            return (choseElem(a1, a2, a3, (k - 1)) + (2 * choseElem(a1, a2, a3, (k - 2)) * choseElem(a1, a2, a3, (k - 3))));
        }//рекурсивное вычисление элементов
        public static void Print(int[] allElements, int[] nesElements, int[] numbersOfElements)
        {
            Console.WriteLine();
            for (int i = 0; i < allElements.Length; i++)
            {
                Console.Write(allElements[i] + " ");
            }
            Console.WriteLine("\nИскомые элементы: ");
            for (int i = 0; i < nesElements.Length; i++)
            {
                Console.Write(nesElements[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine("\nНомера элементов: ");
            for (int i = 0; i < numbersOfElements.Length; i++)
            {
                Console.Write(numbersOfElements[i] + " ");
            }
        }
        static int chkN(int number)
        {
            bool ok = false;
            do
            {
                if (number < 1)
                {
                    ok = false;
                    number = ImpIntNumber("Ошибка - введите число большее 0: ");
                }
                else ok = true;
            } while (!ok);
            return number;
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
                    ok = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Ошибка - введите целое число");
                    ok = false;
                }
            } while (!ok);
            return number;
        }

        //Переменные тут, чтобы массивы работали
        public static int a1 = ImpIntNumber("Введите 1-ый элемент последовательности: ");
        public static int a2 = ImpIntNumber("Введите 2-ой элемент последовательности: ");
        public static int a3 = ImpIntNumber("Введите 3-ий элемент последовательности: ");
        public static int N = chkN(ImpIntNumber("Введите количество элементов: "));
        public static int e = ImpIntNumber("Введите число, меньшее разности первых искомых элементов с предыдущими: ");
        static void Main(string[] args)
        {
            //Общий массив
            int[] allElems = new int[2]; //мин 2 элем. по условию
            allElems[0] = a1;
            allElems[1] = a2;
            //Массив с нужными элем.
            int[] reqdElems = new int[N];
            //Массив с номерами нужн. элем.
            int[] elemNums = new int[N];
            //Осн. алг. для рекурсий
            allElems = MakeArray(allElems, reqdElems, elemNums, out reqdElems, out elemNums);
         
            Print(allElems, reqdElems, elemNums);
            Console.ReadLine();
        }
    }
}






