using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryFastDecisions;
using static System.Console;
using System.Reflection;

namespace GB_Algoritmen_Lesson_3
{
    class Program
    {
        static Dictionary<string, Act> dict = new Dictionary<string, Act>
        {
            { "1", new BubbleSort() },
            { "2", new BubbleSortOptimized() },
            { "3", new ShakerSort() },
        };

        static int[] mass;
        static Random r = new Random();

        static void Main(string[] args)
        {
            var ex = new Extension();
            var q = new Questions();
            var n = "";
            WriteLine("С# - Алгоритмы и структуры данных. Задание 3.");
            WriteLine("Кузнецов");


            while (n != "0")
            {
                WriteLine("Введите номер интересующей вас задачи:" + Environment.NewLine +
                    "1. Попробовать оптимизировать пузырьковую сортировку. Сравнить количество операций сравнения оптимизированной и неоптимизированной программы. Написать функции сортировки, которые возвращают количество операций." + Environment.NewLine +
                    "2." + Environment.NewLine +
                    "2. * Реализовать шейкерную сортировку." + Environment.NewLine +
                    " Реализовать бинарный алгоритм поиска в виде функции, которой передаётся отсортированный массив.Функция возвращает индекс найденного элемента или –1, если элемент не найден." + Environment.NewLine +
                    " * Подсчитать количество операций для каждой из сортировок и сравнить его с асимптотической сложностью алгоритма." + Environment.NewLine +
                    "0. Нажмите для выхода из программы.");

                n = q.Question<int>("Введите любое число", new HashSet<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' }, true);
                if (n == "0") break;

                Massiv(int.Parse(n), 0);
                WriteLine($"");
                WriteLine($"При массиве в обратном порядке:");
                WriteLine($"Пузырьковая сортировка выполнена за {dict["1"].Work((int[])mass.Clone())} операций." );
                WriteLine($"Оптимизируемая пузырьковая сортировка выполнена за {dict["2"].Work((int[])mass.Clone())} операций.");
                WriteLine($"Шейкерная сортировка выполнена за {dict["3"].Work((int[])mass.Clone())} операций.");
                RandMassiv(int.Parse(n), 0);
                WriteLine($"");
                WriteLine($"При случайном заполненом массива:");
                WriteLine($"Пузырьковая сортировка выполнена за {dict["1"].Work((int[])mass.Clone())} операций.");
                WriteLine($"Оптимизируемая пузырьковая сортировка выполнена за {dict["2"].Work((int[])mass.Clone())} операций.");
                WriteLine($"Шейкерная сортировка выполнена за {dict["3"].Work((int[])mass.Clone())} операций.");
                LastMinMassiv(int.Parse(n), 0);
                WriteLine($"");
                WriteLine($"При последнем минимальном в сортированном массиве:");
                WriteLine($"Пузырьковая сортировка выполнена за {dict["1"].Work((int[])mass.Clone())} операций.");
                WriteLine($"Оптимизируемая пузырьковая сортировка выполнена за {dict["2"].Work((int[])mass.Clone())} операций.");
                WriteLine($"Шейкерная сортировка выполнена за {dict["3"].Work((int[])mass.Clone())} операций.");

                WriteLine($"");
                WriteLine($"Индекс { FindIndexByBinarySearch(mass, 5) } числа 5 в массиве.");
                WriteLine($"");
                WriteLine($"");
            }
            
            Console.ReadKey();
        }

        static void Massiv(int start, int end)
        {
            var count = Math.Abs(end - start);
            mass = new int[count];
            var l = 1;
            if (start > end) l = -1;
            for (int i = 0; i < mass.Length; ++i)
                mass[i] = start += (1 * l);
        }

        static void RandMassiv(int start, int end)
        {
            var count = Math.Abs(end - start);
            mass = new int[count];
            for (int i = 0; i < mass.Length; ++i)
                mass[i] = r.Next(Math.Min(start, end), Math.Max(start, end));
        }
        static void LastMinMassiv(int start, int end)
        {
            var count = Math.Abs(end - start);
            mass = new int[count];
            for (int i = 0; i < mass.Length; ++i)
                mass[i] = i + 1;
            mass[mass.Length - 1] = 0;
        }
        
        static Func<int[], int, int> FindIndexByBinarySearch = (array, element) =>
        {
            int posLeft = 0,    posRight = array.Length - 1,    m = 0;
            while (posLeft < posRight)
            {
                m = (posRight + posLeft) / 2;
                m = (element <= array[m])?(posRight = m):posLeft = m + 1;
            }
            if(array[posRight] == element) return posRight;
            return -1;
        };
    }

    abstract class Act
    {
        public abstract int Work(int[] mass);
    }

    class BubbleSort : Act
    {
        public override int Work(int[] mass)
        {
            var operations = 0;
            for (int i = 0; i < mass.Length; i++)
                for (int j = 0; j < mass.Length - 1; j++)
                {
                    operations++;
                    if (mass[j] > mass[j + 1])
                    {
                        operations++;
                        TwoValuesExchange(mass, j + 1, j);
                    }
                }

            return operations;
        }

        static void TwoValuesExchange<T>(T[] x, int i1, int i2) => (x[i1], x[i2]) = (x[i2], x[i1]);
    }

    class BubbleSortOptimized : Act
    {
        public override int Work(int[] mass)
        {
            var sortingCheck = true;
            var operations = 0;
            for (int i = mass.Length - 1; i >= 0; i--)
            {
                operations++;
                for (int j = mass.Length - 1; j > 0; j--)
                {
                    operations++;
                    if (mass[j] < mass[j - 1])
                    {
                        sortingCheck = false;
                        operations++;
                        TwoValuesExchange(mass, j - 1, j);
                    }
                }
                if (sortingCheck) break;
                else sortingCheck = true;                
            }

            return operations;
        }

        static void TwoValuesExchange<T>(T[] x, int i1, int i2) => (x[i1], x[i2]) = (x[i2], x[i1]);
    }


    class ShakerSort : Act
    {
        public override int Work(int[] mass)
        {
            var posLeft = 0;
            var posRight = mass.Length - 1;
            var operations = 0;

            while (posLeft < posRight)
            {
                for (int i = posLeft; i < posRight; i++)
                {
                    operations++;
                    if (mass[i] > mass[i + 1])
                    {
                        TwoValuesExchange(mass, i, i + 1);
                        operations++;
                    }
                }
                posRight--;

                for (int i = posRight; i > posLeft; i--)
                {
                    operations++;
                    if (mass[i - 1] > mass[i])
                    {
                        TwoValuesExchange(mass, i - 1, i);
                        operations++;
                    }
                }
                posLeft++;
            }

            return operations;
        }

        static void TwoValuesExchange<T>(T[] x, int i1, int i2) => (x[i1], x[i2]) = (x[i2], x[i1]);
    }

}
