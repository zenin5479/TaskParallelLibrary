using System;
using System.Threading;
using System.Threading.Tasks;
// Для использования AsyncResult
//using System.Runtime.Remoting;

namespace TPLWait
{
   class Program
   {
      static void MyTask()
      {
         for (int i = 0; i < 80; i++)
         {
            Thread.Sleep(25);
            Console.Write(".");
         }
      }

      static void Main()
      {
         Task task = new Task(MyTask);
         task.Start();
         Thread.Sleep(500);
         Console.WriteLine("\ntask.IsCompleted = " + task.IsCompleted);

         // Ожидание завершения асинхронной задачи.
         // Вариант 1:
         task.Wait();
         // Вариант 2:
         //while (!task.IsCompleted)
         //    Thread.Sleep(100);
         // Вариант 3: 
         //IAsyncResult asynkResult = task;
         //ManualResetEvent waitHandle = (ManualResetEvent)asynkResult.AsyncWaitHandle;
         //waitHandle.WaitOne();

         Console.WriteLine("\ntask.IsCompleted = " + task.IsCompleted);

         // Задержка
         Console.ReadKey();
      }
   }
}