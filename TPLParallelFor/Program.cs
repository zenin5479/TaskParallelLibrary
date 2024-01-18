using System;
using System.Diagnostics;
using System.Threading.Tasks;

// Применение метода Parallel.For() для организации параллельно выполняемого цикла обработки данных.

namespace TPLParallelFor
{
   internal class Program
   {
      static void Main()
      {
         int[] data = new int[100000000];
         Stopwatch timer = new Stopwatch();
         timer.Start();
         for (int i = 0; i < data.Length; i++)
         {
            // Инициализация данных в обычном цикле for.                
            data[i] = i * i * i / 123;
         }
         timer.Stop();
         Console.WriteLine("Обычный цикл for: " + timer.Elapsed.TotalMilliseconds);
         timer.Reset();
         Action<int> transform = i => { data[i] = i * i * i / 123; };
         timer.Start();
         // Инициализация данных в параллельном цикле for.
         Parallel.For(0, data.Length, transform);
         timer.Stop();
         Console.WriteLine("Параллельный цикл for: " + timer.Elapsed.TotalMilliseconds);

         // Внимание!!!
         // Выполнение метода Main() приостанавливается, пока не произойдет завершение работы метода For().
         Console.WriteLine("Основной поток завершен.");

         // Задержка
         Console.ReadKey();
      }
   }
}