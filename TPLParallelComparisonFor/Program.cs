using System;
using System.Diagnostics;
using System.Threading.Tasks;

// Сравнение последовательного и параллельного выполнения цикла for.
// Пример выполнять через (CTRL+F5).

namespace TPLParallelComparisonFor
{
    internal class Program
    {
        static void Main()
        {
            int[] data = new int[100000000];
            Stopwatch timer = new Stopwatch();
            timer.Start();
            // Параллельная инициализация.
            Parallel.For(0, data.Length, i => data[i] = i);
            timer.Stop();
            Console.WriteLine("Параллельная инициализация: {0} секунд.", timer.Elapsed.TotalSeconds);
            timer.Reset();
            timer.Start();
            // Последовательная инициализация.
            for (int i = 0; i < data.Length; i++)
                data[i] = i;
            timer.Stop();
            Console.WriteLine("Последовательная инициализация: {0} секунд.\n", timer.Elapsed.TotalSeconds);
            timer.Reset();
            timer.Start();
            // Параллельное преобразование.
            Parallel.For(0, data.Length, i => data[i] = i * i * i / 123);
            timer.Stop();
            Console.WriteLine("Параллельное преобразование: {0} секунд.", timer.Elapsed.TotalSeconds);
            timer.Reset();
            timer.Start();
            // Последовательное преобразование.
            for (int i = 0; i < data.Length; i++)
                data[i] = i * i * i / 123;
            timer.Stop();
            Console.WriteLine("Последовательное преобразование: {0} секунд.\n", timer.Elapsed.TotalSeconds);
            timer.Reset();
            Console.WriteLine("Основной поток завершен.");

            // Задержка
            Console.ReadKey();
        }
    }
}