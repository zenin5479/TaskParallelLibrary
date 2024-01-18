using System;
using System.Threading;
using System.Threading.Tasks;

// Использование лямбда-выражения в качестве задачи.

namespace TPLLambda
{
   class Program
   {
      static void Main()
      {
         Console.WriteLine("Основной поток запущен.");
         // Использование лямбда-оператора для определения задачи.
         Task task = Task.Factory.StartNew(() =>
         {
            for (int i = 0; i < 80; i++)
            {
               Thread.Sleep(20);
               Console.Write(".");
            }
         });
         // Ожидание завершения задачи.
         task.Wait();
         // Освобождение задачи. 
         task.Dispose();
         Console.WriteLine("\nОсновной поток завершен.");

         // Задержка
         Console.ReadKey();
      }
   }
}