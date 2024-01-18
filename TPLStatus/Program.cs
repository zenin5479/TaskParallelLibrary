using System;
using System.Threading;
using System.Threading.Tasks;

// TaskStatus - статусы задачи.

namespace TPLStatus
{
   class Program
   {
      static void MyTask()
      {
         Thread.Sleep(1000);
      }

      static void Main()
      {
         Task task = new Task(MyTask);
         // Задача не запущена.
         Console.WriteLine("1. " + task.Status);
         // Задача в процессе запуска.
         task.Start();
         Console.WriteLine("2. " + task.Status);
         // Задача выполняется.
         Thread.Sleep(1000);
         Console.WriteLine("3. " + task.Status);
         // Задача завершилась.
         Thread.Sleep(1000);
         Console.WriteLine("4. " + task.Status);

         // Задержка
         Console.ReadKey();
      }
   }
}