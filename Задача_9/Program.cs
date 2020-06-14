using System;
using System.Collections.Generic;

namespace Задача_9
{
    class CircularList  //циклич. лист
    {
        public CircularListEntry First { get; set; } 

        public int Count   //
        {
            get
            {
                int count = 0;
                if (First != null)
                {
                    CircularListEntry entry = First.Next;
                    count++;
                    while (entry.Key != First.Key)
                    {
                        count++;
                        entry = entry.Next;
                    }
                }
                return count;
            }
        }
        public CircularList()  //без парам.
        {
            First = null;
        }
        public CircularList(CircularListEntry first) //с парам.
        {
            First = first;
        }
        public CircularList(ICollection<CircularListEntry> col) //
        {
            CircularListEntry help = new CircularListEntry();
            First = help;
            foreach (CircularListEntry entry in col)
            {
                help.Value = entry.Value;
                help.Key = entry.Key;
                help.Next = new CircularListEntry();
                help = help.Next;
            }
                      //help = First;
        }
        public CircularListEntry SearchKey(int key)
        {
            CircularListEntry help = First;
            if (key == help.Key) return help;
            help = help.Next;
            while (help.Key != First.Key) if (key == help.Key) return help;
                else help = help.Next;
            return null;
        }
        public void Remove(CircularListEntry entry)
        {
            if (First != null)
            {
                CircularListEntry help = First;
                if (help.Value != entry.Value)
                {
                    CircularListEntry last = help;
                    help = help.Next;
                    while (help.Key != First.Key)
                    {
                        if (help.Value == entry.Value)
                        {
                            last.Next = help.Next;
                            for (int i = help.Key; i < Count - 1; i++)
                            {
                                help = help.Next;
                                help.Key = i;
                            }
                            break;
                        }
                        else
                        {
                            last = help;
                            help = help.Next;
                        }
                    }
                }
                else
                {
                    SearchKey(Count - 1).Next = help.Next;
                    First = help.Next;
                    First.Key = 0;
                    for (int i = 0; i < Count - 1; i++)
                    {
                        help = help.Next;
                        help.Key = i;
                    }
                }
            }
        }
        public void Add(CircularListEntry entry)
        {
            if (First != null)
            {
                CircularListEntry help = this.SearchKey(Count - 1);
                help.Next = entry;
                entry.Next = First;
                entry.Key = help.Key + 1;
            }
            else
            {
                First = entry;
                First.Key = 0;
                First.Next = First;
            }
        }
        public override string ToString()
        {
            if (First != null)
            {
                string ans = "";
                CircularListEntry help = First;
                ans += First.ToString() + " ";
                help = help.Next;
                while (help.Key != First.Key)
                {
                    ans += help.ToString() + " ";
                    help = help.Next;
                }
                return ans;
            }
            else return "Список пуст";
        }
        public void Show()
        {
            Console.WriteLine(ToString());
        }
    }
    class CircularListEntry //
    {
        public int Value { get; set; }
        public int Key { get; set; } //является индексатором
        public CircularListEntry Next { get; set; }
        public CircularListEntry()
        {
            Key = 0;
            Value = 0;
            Next = null;
        }
        public CircularListEntry(int value)
        {
            Value = value;
            Next = null;
        }
        public override string ToString()
        {
            return Convert.ToString(Value);
        }
        public void Show()
        {
            Console.WriteLine(ToString());
        }
    } 

    //INPUT
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
                    if(number < 1)
                    {
                        Console.WriteLine($"Ошибка - введите положительное целое число");
                        ok = false;
                    }
                    ok = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Ошибка - введите положительное целое число");
                    ok = false;
                }
            } while (!ok);
            return number;
        }

        static void Main(string[] args)
        {
            CircularList list = new CircularList();
            int N = ImpIntNumber("Введите число, до которого последует заполние списка: ");
            
            for (int i = 0; i < N; i++)
                list.Add(new CircularListEntry(i + 1));
            //---------------------VV TODO --------------------
            Console.WriteLine("Полученный список:\n" + list.ToString() + "\n\nУдаление члена из списка\n\nПолученный список: ");
            list.Remove(new CircularListEntry(list.Count));
            list.Show();
            Console.WriteLine("Поиск 4 элемента: ");
            if (list.SearchKey(3) == null) Console.WriteLine("Такого элемента нет в списке");
            else list.SearchKey(3).Show();
            Console.ReadKey();
        }
    }
}


