using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Schema;

namespace Задача_8
{
    class Program
    {
        public static List<List<string>> loadGraph()  //загрузка графа в виде листа, из файла
        {
            string fileName = "graph.txt";
            List<List<string>> graph = new List<List<string>>(); 
            string input;

            // Загрузка матрицы инциденций
            try
            {
                using (StreamReader sr = new StreamReader(fileName, System.Text.Encoding.Default))
                {
                    input = sr.ReadToEnd();
                }
            }
            catch
            {
                Console.WriteLine("Ошибка в файле graph.txt / файл отсутствует или поврежден");
                Console.ReadLine();
                return null;
            }

            input = input.Replace("\r", "");
            string[] points = input.Split('\n'); //Вершины графа в мас.

            //Разбиение строк на двумерный массив
            foreach (var p in points)
            {
                List<string> point = new List<string>();
                string[] incidences = p.Split(' ');
                foreach (var item in incidences)
                {
                    point.Add(item);
                }
                graph.Add(point);
            }
            return graph;
        }
        public static bool graphValid(List<List<string>> matrix)  //проверка перед работой
        {
            if (matrix[0].Count == 0)
            {
                Console.WriteLine("\nОшибка - матрица пуста");
                return false;
            }

            for (int i = 0; i < matrix.Count; i++)
            {
                if (matrix[i].Count != matrix[0].Count)
                {
                    Console.WriteLine("\nОшибка - столбцы матрицы разной длины");
                    return false;
                }

                for (int j = 0; j < matrix[i].Count; j++)
                {
                    if (matrix[i][j] != "1" && matrix[i][j] != "0")  //1 есть, 0 нет; другие числа не принимаются
                    {
                        Console.WriteLine("\nОшибка - матрица должна состоять из 1 и 0");
                        return false;
                    }
                }
            }

            //проверка по столбцам на наличие ровно двух единиц
            for (int j = 0; j < matrix[0].Count; j++)
            {
                int oneCount = 0;
                for (int i = 0; i < matrix.Count; i++)
                {
                    if (matrix[i][j] == "1")
                    {
                        oneCount++;
                    }
                }

                if (oneCount != 2)
                {
                    Console.WriteLine(
                        $"\nОшибка - столбец {j + 1} содержит не ровно 2 единицы" +
                        $"\nребро должно быть связано с двумя вершинами");
                    return false;
                }
            }
            return true;
        }
        static List<List<int>> strListTo_IntList(List<List<string>> matrix)
        {
            List<List<int>> tmp = new List<List<int>>();

            foreach (var row in matrix)
            {
                List<int> tmp1 = new List<int>();

                foreach (var col in row)
                {
                    tmp1.Add(Convert.ToInt32(col));
                }

                tmp.Add(tmp1);
            }
            return tmp;
        }
        static void Print<T>(List<List<T>> graph)
        {
            foreach (var row in graph)
            {
                foreach (var col in row)
                {
                    Console.Write(col + " ");
                }

                Console.WriteLine();
            }
        }
        static List<List<int>> ToAdjacent(List<List<int>> incidence) //Матрицу инцинденций в смежности
        {
            List<List<int>> adjacency = new List<List<int>>();
            for (int i = 0; i < incidence.Count; i++) // Заполнение матрццы смежности
            {
                List<int> tmp = new List<int>();
                for (int j = 0; j < incidence.Count; j++) tmp.Add(0);
                adjacency.Add(tmp);
            }
            //Заполнение матрицы смежности из ициденций
            for (int col = 0; col < incidence[0].Count; col++)
            {
                for (int row = 0; row < incidence.Count; row++)
                {
                    if (incidence[row][col] == 1)
                    {
                        int secondRow = row + 1;
                        //Поиск следующей единицы
                        while (incidence[secondRow][col] != 1) secondRow++;

                        adjacency[row][secondRow] = 1;
                        adjacency[secondRow][row] = 1;
                        break;
                    }
                }
            }
            return adjacency;
        }
        static List<List<int>> MultiMirrElems(List<List<int>> first, List<List<int>> second) //Перемножение зеркальных элементов
        {
            int matrixLenght = first.Count;
            List<List<int>> tmp = new List<List<int>>();

            
            for (int i = 0; i < matrixLenght; i++) //Заполнение квадратной матрицы
            {
                List<int> temp = new List<int>();

                for (int j = 0; j < matrixLenght; j++) temp.Add(0);
                tmp.Add(temp);
            }

            for (int i = 0; i < matrixLenght; i++) // Перемножение зеркальных элементов матриц 
            {
                for (int j = 0; j < matrixLenght; j++)
                {
                    for (int k = 0; k < matrixLenght; k++) tmp[i][j] += first[i][k] * second[k][j];
                }
            }
            return tmp;
        }
        static List<List<int>> matrPwr(List<List<int>> matrix, int pwr) //Возв. в степень зеркальных элементов 
        {
            List<List<int>> result = null;

            for (int i = 1; i < pwr; i++) result = MultiMirrElems(matrix, matrix);
            return result;
        }
        static bool IsCycles(List<List<int>> matrix,  out List<int> vershin)  //Проверка на наличие значений на главной диагонали (наличие циклов)
        {
            bool isCycleFound = false;
            vershin = new List<int>();

            for (int i = 0; i < matrix.Count; i++)
            {
                if (matrix[i][i] != 0)
                {
                    isCycleFound = true;
                    vershin.Add(i);
                }
            }
            return isCycleFound;
        }
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
                    if( number > max || number < min)
                    {
                        Console.WriteLine($"Ошибка - введите целое число от {min} по {max}");
                        ok = false;
                    }
                    ok = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Ошибка - введите целое число от {min} по {max}");
                    ok = false;
                }
            } while (!ok);
            return number;
        }

        static void Main(string[] args)
        {
            List<List<string>> rawGraph = new List<List<string>>();

            Console.WriteLine();
            int method = ImpIntNumber(@"Выберите действие:
1) Получить матрицу инциденций из файла
2 Сгенерировать матрицу инциденций", 1, 2);
            #region exmpl
            switch (method)
            {
                case 1:
                    rawGraph = loadGraph();
                    if (rawGraph == null)
                    {
                        Console.WriteLine("Ошибка - в файле отстутствует граф");
                        Console.ReadLine();
                        return;
                    }
                    break;
                case 2:
                    List<List<List<string>>> rndG = new List<List<List<string>>>
                {
                    new List<List<string>>
                    {
                        new List<string>{"1", "0", "1"},
                        new List<string>{"1", "1", "0"},
                        new List<string>{"0", "1", "1"}
                    },
                    new List<List<string>>
                    {
                        new List<string>{"1", "0", "1"},
                        new List<string>{"1", "1", "0"},
                        new List<string>{"0", "1", "1"},
                        new List<string>{"1", "0", "1"}
                    },
                    new List<List<string>>
                    {
                        new List<string>{"1", "0", "0", "1"},
                        new List<string>{"1", "1", "0", "0"},
                        new List<string>{"0", "1", "1", "0"},
                        new List<string>{"0", "0", "1", "1"}
                    },
                    new List<List<string>>
                    {
                        new List<string>{"1", "0", "0", "1"},
                        new List<string>{"1", "1", "0", "1"},
                        new List<string>{"0", "1", "1", "0"},
                        new List<string>{"0", "0", "1", "1"}
                    },
                    new List<List<string>>
                    {
                        new List<string>{"1", "0", "0", "1"},
                        new List<string>{"1", "1", "0", "0"},
                        new List<string>{"0", "1", "1", "1"},
                        new List<string>{"0", "0", "1", "0"}
                    },
                    new List<List<string>>
                    {
                        new List<string>{"1", "0", "0", "1", "0"},
                        new List<string>{"1", "1", "0", "0", "1"},
                        new List<string>{"0", "1", "1", "1", "0"},
                        new List<string>{"0", "0", "1", "0", "1"}
                    }
                };

                    rawGraph = rndG[new Random().Next(0, rndG.Count - 1)];
                    break;
            }
            #endregion

            Console.WriteLine("Матрица инциденций:");
            Print(rawGraph);

            if (!graphValid(rawGraph))
            {
                Console.ReadLine();
                return;
            }

            List<List<int>> graph = strListTo_IntList(rawGraph);

            Console.WriteLine("\nМатрица смежности:");
            var adjacencyGraph = ToAdjacent(graph);
            Print(adjacencyGraph);

            while (true)
            {
                int cycleLenght = ImpIntNumber($"\nВведите длину цикла для поиска: от 3 до {adjacencyGraph.Count}; введите 999 для выхода: ", 3, adjacencyGraph.Count);
                if (cycleLenght == 999) return;

                adjacencyGraph = matrPwr(adjacencyGraph, cycleLenght);

                if (!IsCycles(adjacencyGraph, out var vershin))
                {
                    Console.WriteLine($"\nВ графе нет циклов длины {cycleLenght}");
                }
                else
                {
                    // Найденный цикл
                    var foundCycle = Cycle.ListCycleTrjct(graph, vershin, cycleLenght);
                    if (foundCycle.Count != 0)
                    {
                        Console.WriteLine("\nНайденный цикл: ");
                        foreach (var versh in foundCycle)
                        {
                            Console.Write((versh + 1) + " ");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\nВ графе нет циклов длины {cycleLenght}");
                    }
                }
            }
        }
    }

    class Cycle
    {
        private static List<int> allowedVershin;
        private static List<List<int>> graph;

        public static int CycleLenght { get; set; }

        
        public static List<int> ListCycleTrjct(List<List<int>> g, List<int> v, int cycleL)
        {
            //Граф задается матрицей инцид?
            allowedVershin = v;
            CycleLenght = cycleL;
            graph = g;

            foreach (var item in allowedVershin)
            {
                var tmp = CycleTrjctRecover(item, cycleL, new List<int>());
                if (tmp.Count != 0) return tmp;
            }
            return new List<int>();
        } //Поиск цикла по его заданной длине
        private static List<int> CycleTrjctRecover(int curVershNum, int nSteps, List<int> LastVersh)
        {
            //Попали в старт. вершину и не достигли нужной длины
            if (LastVersh.Count != 0 && nSteps != 0 && LastVersh.Contains(curVershNum))   return new List<int>();

            if (nSteps == 0 && curVershNum == LastVersh[0])
            {
                LastVersh.Add(curVershNum);
                return LastVersh;
            }

            if (nSteps == 0) return new List<int>();

            LastVersh.Add(curVershNum);

            
            foreach (var versh in gNearVersh(curVershNum)) //Для всех связанных с этой вершиной
            {
                if (versh == curVershNum) continue;

                var nextStepReslt = CycleTrjctRecover(versh, nSteps - 1, CloneList(LastVersh));

                if (nextStepReslt.Count != 0)   return nextStepReslt;
            }
            return new List<int>();
        }
        private static List<int> gNearVersh(int curVershNum) // Получение ближайших вершин
        {
            List<int> tmp = new List<int>();
            for (int i = 0; i < graph[curVershNum].Count; i++)
            {
                if (i == curVershNum) continue;

                if (graph[curVershNum][i] == 1)
                {
                    for (int j = 0; j < graph.Count; j++)
                    {
                        if (graph[j][i] == 1 && j != curVershNum && allowedVershin.Contains(j)) tmp.Add(j);
                    }
                }
            }
            return tmp;
        }
        public static List<int> CloneList(List<int> list) //Клонирование списка вершин
        {
            List<int> res = new List<int>();
            foreach (var v in list) res.Add(v);
            return res;
        }
    }
}
