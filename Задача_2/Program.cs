using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Задача_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 1;
            //string input = @"c:\temp\INPUT.txt", output = @"c:\temp\OUTPUT.txt";
            string input = "INPUT.txt", output = "OUTPUT.txt";
            using (FileStream fs = new FileStream(input, FileMode.OpenOrCreate)) { }
                using (StreamReader reader = new StreamReader(input, Encoding.Default))
                {
                    while (reader.Peek() > -1)
                    {
                    string S1 = reader.ReadLine();
                    string S2 = reader.ReadLine();

                    string[] Convert(string tmp)
                    {
                        char[] tmpChar = tmp.ToCharArray();
                        string[] tmpString = new string[tmpChar.Length];
                        for (int i =0; i< tmpString.Length; i++)
                        {
                            if (tmpChar[i] == 'a') tmpString[i] = "0123";
                            if (tmpChar[i] == 'b') tmpString[i] = "1234";
                            if (tmpChar[i] == 'c') tmpString[i] = "2345";
                            if (tmpChar[i] == 'd') tmpString[i] = "3456";
                            if (tmpChar[i] == 'e') tmpString[i] = "4567";
                            if (tmpChar[i] == 'f') tmpString[i] = "5678";
                            if (tmpChar[i] == 'g') tmpString[i] = "6789";
                            if (tmpChar[i] == '?') tmpString[i] = "0123456789";
                            if (tmpChar[i] == '0') tmpString[i] = "0";
                            if (tmpChar[i] == '1') tmpString[i] = "1";
                            if (tmpChar[i] == '2') tmpString[i] = "2";
                            if (tmpChar[i] == '3') tmpString[i] = "3";
                            if (tmpChar[i] == '4') tmpString[i] = "4";
                            if (tmpChar[i] == '5') tmpString[i] = "5";
                            if (tmpChar[i] == '6') tmpString[i] = "6";
                            if (tmpChar[i] == '7') tmpString[i] = "7";
                            if (tmpChar[i] == '8') tmpString[i] = "8";
                            if (tmpChar[i] == '9') tmpString[i] = "9";
                        }
                        //tmp = String.Join("", tmpString);
                        //return tmp;
                        return tmpString;
                    } //применяем правила задачи, переводим строки в массив строк
                    string[] s1 = Convert(S1);
                    string[] s2 = Convert(S2);
                    
                    //основной алгоритм
                    for (int i = 0; i < s1.Length; i++)
                    {
                        int proizvedenie = 0;
                        for (char d = '0'; d<= '9'; d++)
                        {
                            if (s1[i].Contains(d) && s2[i].Contains(d)) proizvedenie++;
                        }
                        count *= proizvedenie;
                    }
                    
                    }
                }
                //вывод
                using (FileStream fs = new FileStream(output, FileMode.OpenOrCreate)) { }
                using (StreamWriter writer = new StreamWriter(output))
                {
                    writer.Write(count);
                }
        
    
      }
    }
}
