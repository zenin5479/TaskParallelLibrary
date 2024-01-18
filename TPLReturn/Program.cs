using System;
using System.Threading;
using System.Threading.Tasks;

// Возвращение значения из задачи.

namespace TPLReturn
{
   struct Context
   {
      public int A;
      public int B;
   }

   internal class Program
   {
      // Метод который будет возвращать результат.
      static int Sum(object arg)
      {
         int a = ((Context)arg).A;
         int b = ((Context)arg).B;
         Thread.Sleep(1000);
         return a + b;
      }

      static void Main()
      {
         Console.WriteLine("Основной поток запущен.");
         Context context;
         context.A = 3;
         context.B = 5;
         Task<int> task;

         // 1 вариант
         task = new Task<int>(Sum, context);
         task.Start();
         // 2 вариант
         //TaskFactory<int> factory = new TaskFactory<int>();
         //task = factory.StartNew(Sum, context);
         // 3 вариант
         //task = Task<int>.Factory.StartNew(Sum, context);
         Console.WriteLine("Результат выполнения задачи Sum = " + task.Result);
         Console.WriteLine("Основной поток завершен.");

         // Задержка
         Console.ReadKey();
      }
   }
}