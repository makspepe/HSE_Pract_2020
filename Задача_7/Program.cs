using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Задача_7
{
    class Program   
    {

        public static List<char> impList(string tmp, ref string nullStr, ref char[] elems, ref List<char> listElems)  // Запись начальной строки
        {
            bool ok = false;
            do
            {
                listElems.Clear();
                Console.Write(tmp);
                try
                {
                    nullStr = Console.ReadLine();
                    if (nullStr != null)
                    {
                        elems = nullStr.ToCharArray();
                        for (int i = 0; i < elems.Length; i++)
                            if (elems[i] > 47 && elems[i] < 50)
                            {
                                listElems.Add(elems[i]);
                                ok = true;
                            }
                            else ok = false;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Ошибка - введите строку нулей и единиц");
                    ok = false;
                }
            } while (!ok);
            return listElems;
        }
        public static void formatStr(ref List<char>[] str, ref List<char> listElem, ref int numElems) //обработка строк
        {
            for (int i = 0; i < listElem.Count / 2; i++) str[0].Add('0');  //1я строка
            for (int i = 0; i < listElem.Count; i += 2) str[0].Insert(i, '1');
            Console.WriteLine();
            foreach (var item in str[0]) 
                Console.Write(item); //show 1
            Console.WriteLine(); //#
            for (int j = 2; j < str.Length; j++)   //остальные стр.
            {
                if (numElems >= j)
                {
                    for (int i = 0; i < listElem.Count; i++) str[j - 1].Add('0');   //выписывание нулей в массив

                    for (int i = (int)Math.Pow(2, j - 1) - 1; i < listElem.Count; i += (int)Math.Pow(2, j))
                        for (int k = 0; k < (int)Math.Pow(2, j - 1); k++)
                            str[j - 1].Insert(i + k, '1');  //каждые k символов вставлять 1

                    if (str[j - 1].Count > str[j - 2].Count)   //обрезка лишних
                    {
                        int deletedIndex = str[j - 2].Count;
                        while (str[j - 1].Count != str[j - 2].Count) str[j - 1].RemoveAt(deletedIndex);
                    }

                    else if (str[j - 1].Count < str[j - 2].Count) str[j - 1].Add('0'); //добавляем 0
                    foreach (var item in str[j - 1]) Console.Write(item);
                }
                else break;
                Console.WriteLine();
            }
        }
        public static void printL(List<char> list) //Вывод в консоль
        {
            for (int i = 0; i < list.Count; i++) Console.Write(list[i]);
        }
        public static List<int[]>[] ToList(ref List<char> listElems, ref List<char>[] str, ref int numElem, List<int[]>[] listInt)
        {
           for (int i = 1, j = 0; i < listElems.Count; i = (int)Math.Pow(2, j), j++)
            {
                int[] tmpInt = new int[listElems.Count];
                tmpInt[j] = j;
                List<int[]> tmpList = new List<int[]> { tmpInt };
                listInt[j] = tmpList;
            }
            for (int i = 0; i < str.Length - 1; i++)
            {
                for (int j = 0, k = 0; j < listElems.Count; j++, k++)
                    if (numElem >= i)
                    {
                        int n = 0;
                        listInt[i][n][k] = Convert.ToInt32(str[i][j].ToString());
                    }
            }
            return listInt;
        }
        public static string[] ToString(ref List<char>listElems, ref int numElement, ref int[] aElem, List<int[]>[] listInt, out string[] tmpX)
        {   tmpX = new string[numElement];   // Массив контрольных значений
            for (int j = 0; j < numElement; j++)
            {
                int temp = 0;
                for (int i = 0; i < listElems.Count; i++)
                {
                    int n = 0;
                    temp += listInt[j][n][i] * aElem[i];   //Слева рабочие элементы, необходимые для нахождения кода Хэмминга. Справа изначальная строка с контрольными разрядами равными нулю
                }
                if (temp > 1) tmpX[j] = Convert.ToString(temp % 2); //если тмп больше 1, то выписываем 1   
                else tmpX[j] = Convert.ToString(temp);              //иначе 0 

                Console.WriteLine($"{j + 1} контрольный элемент - " + tmpX[j]);   //выписываем контрольные элементы
            }
            return tmpX;
        }//Поиск контрольных элем.
        
        static void Main(string[] args)
        {
            List<char> listElems = new List<char>();      //Коллекция первых элементов
            List<char> nxtElems = new List<char>();       //Коллекция новых элементов с X
            List<char> processedElems = new List<char>(); //Обработанная

            string nullStr = "";
            char[] elems = new char[nullStr.Length];   //массив для символов
            impList("Введите последовательность из нулей и единиц: ",ref nullStr, ref elems, ref listElems);   //функция создания первой строки

            for (int i = 0; i < listElems.Count; i++) processedElems.Add(listElems[i]);
            for (int i = 0; i < listElems.Count; i++) nxtElems.Add(listElems[i]);

            nxtElems.Insert(0, 'X'); //Добавляем символы для удобства
            nxtElems.Insert(1, 'X');
            listElems.Insert(0, '0');
            listElems.Insert(1, '0');
            int numElem = 2;   //Начало с

            for (int i = 4, j = 3; i < listElems.Count; i = (int)Math.Pow(2, j), j++)
            {  
                listElems.Insert(i - 1, '0'); //заменяем кодовые номера на X и 0
                nxtElems.Insert(i - 1, 'X');
                numElem++;
            }
            Console.WriteLine("Кол-во контрольных разрядов: " + numElem);   //Вывод контрольных значений
            printL(listElems); Console.WriteLine();   
            printL(nxtElems); Console.WriteLine();   //Показ листа с X
            int size = 0;

            for (int i = 1, j = 0; i < listElems.Count; i = (int)Math.Pow(2, j), j++) size = j + 1;   //Цикл для нахождения размера

            List<char>[] str = new List<char>[size]; //Массив символов

            for (int i = 1, j = 0; i < listElems.Count; i = (int)Math.Pow(2, j), j++)
            {
                List<char> emptyList = new List<char>{Convert.ToChar(j)};
                str[j] = emptyList;
                str[j].Clear();
            }

            formatStr(ref str, ref listElems, ref numElem);   //Функция обработки с строк

            string[] tmpNull = new string[0];
            int[] aElem = listElems.Select(item => int.Parse(item.ToString())).ToArray(); //Linq запрос на запись в массив строки с контрольными разрядами

            List<int[]>[] listInt = new List<int[]>[size];
            listInt = ToList(ref listElems, ref str, ref numElem, listInt);
            tmpNull = ToString(ref listElems, ref numElem, ref aElem, listInt, out tmpNull);

            for (int i = 1, j = 1, k = 0; i < processedElems.Count; i = (int)Math.Pow(2, j), j++, k++)
                processedElems.Insert(i - 1, Convert.ToChar(tmpNull[k]));   //Обработанная строка

            Console.Write("\n Финальное состояние: ");   //Выписываем строку
            foreach (var item in processedElems) Console.Write(item);
            Console.ReadKey();
        }
    }
}
