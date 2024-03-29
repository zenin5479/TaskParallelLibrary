﻿using System;
using System.Threading;
using System.Threading.Tasks;

// Применение метода Parallel.Invoke() для параллельного выполнения двух методов.

namespace TPLParallelInvoke
{
   internal static class Program
   {
      static void MyTask1()
      {
         Console.WriteLine("MyTask1: запущен.");
         for (int i = 0; i < 80; i++)
         {
            Thread.Sleep(10);
            Console.Write("+");
         }
         Console.WriteLine("\nMyTask1: завершен.");
      }

      static void MyTask2()
      {
         Console.WriteLine("MyTask2: запущен.");
         for (int i = 0; i < 80; i++)
         {
            Thread.Sleep(10);
            Console.Write("-");
         }
         Console.WriteLine("\nMyTask2: завершен.");
      }

      static void Main()
      {
         Console.WriteLine("Основной поток запущен.");
         ParallelOptions options = new ParallelOptions();
         // Выделить определенное количество процессорных ядер.
         //options.MaxDegreeOfParallelism = Environment.ProcessorCount > 2 ? Environment.ProcessorCount - 1 : 1;

         // ParallelOptions.MaxDegreeOfParallelism
         // Получает или задает максимальное число параллельных задач, допускаемое этим экземпляром
         // Выполнить параллельно 1 задачу. (или 2)
         options.MaxDegreeOfParallelism = 1;
         Console.WriteLine("Количество логических ядер CPU:" + Environment.ProcessorCount);
         Console.ReadKey();

         // Выполнить параллельно два метода.
         //Parallel.Invoke(options, MyTask1, MyTask2);
         // Выполнить параллельно четыре метода.
         Parallel.Invoke(options, MyTask1, MyTask2, MyTask1, MyTask2);

         // Внимание!!!
         // Выполнение метода Main() приостанавливается, пока не произойдет завершение задач.
         Console.WriteLine("Основной поток завершен.");

         // Задержка
         Console.ReadKey();
      }
   }
}