using System;
using System.Threading;
using System.Threading.Tasks;

namespace TPL
{
   class Program
   {
      // Метод который будет выполнен асинхронно/синхронно.
      static void MyTask()
      {
         int threadId = Thread.CurrentThread.ManagedThreadId;
         Console.WriteLine("\nMyTask: запущен в потоке # {0}", threadId);
         for (int i = 0; i < 10; i++)
         {
            Thread.Sleep(200);
            Console.Write("+ ");
         }
         Console.WriteLine("\nMyTask: завершен в потоке # {0}", threadId);
      }

      static void Main()
      {
         int threadId = Thread.CurrentThread.ManagedThreadId;
         Console.WriteLine("Main: запущен в потоке # {0}", threadId);
         Action action = MyTask;
         // Создание экземпляра задачи. 
         Task task = new Task(action);
         // Запуск задачи на выполнение асинхронно.
         //task.Start();              
         // Запуск задачи на выполнение синхронно.
         task.RunSynchronously();
         for (int i = 0; i < 10; i++)
         {
            Console.Write(". ");
            Thread.Sleep(200);
         }
         Console.WriteLine("\nMain: завершен в потоке # {0}", threadId);
         // Задержка
         Console.ReadKey();
      }
   }
}