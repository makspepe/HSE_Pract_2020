using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Задача_10
{   
    class Graph
    {
        int[,] adjMatrix;  // граф задается матрицей смежности
        int[] values;      // массив значений, записанных в вершинах
        int size;          // количество вершин графа

        public Graph(int size, int[] values, int[,] matrix)
        {
            this.size = size;
            adjMatrix = matrix;
            this.values = values;
        }
        public static Graph readGraph(string path)
        {
            try
            {
                FileStream file = new FileStream(path, FileMode.Open);
                StreamReader sr = new StreamReader(file);

                bool ok = int.TryParse(sr.ReadLine(), out int size); //правильность ввода размера графа
                string sVal = sr.ReadLine(); // массив значений

                int[] values = new int[size];
                if (sVal.Length > size * 2 - 1) sVal = sVal.Remove(size * 2 - 1);
                values = sVal.Split(' ').Select(n => int.Parse(n)).ToArray();

                int[,] matrix = new int[size, size];
                for (int i = 0; i < size; i++)
                {
                    sVal = sr.ReadLine();
                    if (sVal.Length > size * 2 - 1) sVal = sVal.Remove(size * 2 - 1);

                    
                    int[] row = sVal.Split(' ').Select(n => int.Parse(n)).ToArray();
                    for (int j = 0; j < size; j++) // Чтение строк
                    {
                        if (row[j] != 0 && row[j] != 1)
                        {
                            Console.WriteLine("Ошибка - файл поврежден или содержит некорректные данные");
                            return null;
                        }
                        matrix[i, j] = row[j];
                    }
                }
                sr.Close();
                file.Close();
                return new Graph(size, values, matrix);
            }
            catch
            {
                Console.WriteLine("Ошибка - отсутствует файл/неверно указан путь");
                return null;
            }
        }  //чтение из файла быстрее, чем при каждом запуске задавать граф заного
        public void writeGraph(string path) //Запись графа в файл
        {
            path = Path.GetDirectoryName(path) + Path.GetFileNameWithoutExtension(path) + "-output" + Path.GetExtension(path); //путь - тот же что и у input
            FileStream File;
            try //создание нового/перезапись существующ.
            {
                File = new FileStream(path, FileMode.Truncate);
            }
            catch (FileNotFoundException)
            {
                File = new FileStream(path, FileMode.CreateNew);
            }
            StreamWriter sw = new StreamWriter(File);
            sw.WriteLine(size);
            foreach (int item in values)
                sw.Write(item + " ");
            sw.WriteLine();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    sw.Write(adjMatrix[i, j] + " ");
                sw.WriteLine();
            }

            Console.WriteLine($"Обработанный граф записан в файл {path}");
            sw.Close();
            File.Close();
        }
        public void contract(int value) // Стягивание графа
        {
            if (!values.Contains(value)) //Если искомое значение отсутствует в графе
            {
                Console.WriteLine("В графе нет вершины с указанным значением.");
                return;
            }
            // Номер вершины, где впервые встречается искомое значение
            int firstVersh = Array.IndexOf(values, value);

            int i = firstVersh + 1;

            while (i < size) //Проход по оставшимся вершинам
            {
                if (values[i] == value) //нашлась еще одна вершина с искомым значением
                {
                    for (int col = 0; col < size; col++) //Перенос ребер из этой вершины
                        if (adjMatrix[i, col] == 1 && col != firstVersh) //Если из данной вершины исходит ребро, но не в точку, в которую стягиваем(иначе-петли)
                            adjMatrix[firstVersh, col] = 1; //Переносим начало ребра в вершину, куда стягиваем

                    for (int row = 0; row < size; row++) //Перенос ребер, входящих в эту вершину
                        if (adjMatrix[row, i] == 1 && row != firstVersh) //Если в данную вершину входит ребро, но не из точки, в которую стягиваем(иначе-петли)
                            adjMatrix[row, firstVersh] = 1; //Переносим конец ребра в вершину, куда стягиваем
                    delVersh(i); 
                }
                else i++;
            }
        }
        public void delVersh(int num)
        {
            //Удал. значения
            int[] newValues = new int[size - 1];

            for (int i = 0; i < num; i++) newValues[i] = values[i];
            for (int i = num + 1; i < size; i++) newValues[i - 1] = values[i];
            values = newValues;

            int[,] newMatrix = new int[size - 1, size - 1]; //Удаление вершины из матрицы

            //Копирование неизмененной части
            for (int i = 0; i < num; i++)
                for (int j = 0; j < num; j++)
                    newMatrix[i, j] = adjMatrix[i, j];

            //Удал. столбца
            for (int i = 0; i < newMatrix.GetLength(0); i++)
                for (int j = num; j < newMatrix.GetLength(1); j++)
                    newMatrix[i, j] = adjMatrix[i, j + 1];

            // Удал. строки
            for (int i = num; i < newMatrix.GetLength(0); i++)
                for (int j = 0; j < newMatrix.GetLength(1); j++)
                    newMatrix[i, j] = adjMatrix[i + 1, j];
            adjMatrix = newMatrix;

            size--;
        }
    }

    class Program
    {
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
        public static void Main(string[] args)
        {
            
            Console.Write("\nВведите путь к файлу, в котором записан граф, который Вы хотите обработать: ");
            string path = Console.ReadLine(); //Ввод пути к файлу

            Graph graph = Graph.readGraph(path);

            if (graph != null)
            {
                int value = ImpIntNumber("Введите значение для стягивания: ");

                //сжатие и запись графа
                graph.contract(value);
                graph.writeGraph(path);
            }
            Console.ReadLine();
        }
    }
}

