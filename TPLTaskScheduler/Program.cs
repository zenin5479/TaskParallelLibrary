﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

// Планировщик задач

namespace TPLTaskScheduler
{
   internal class Program
   {
      static void MyTask1()
      {
         Console.WriteLine("MyTask1 ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
         for (int i = 0; i < 10; i++)
         {
            Thread.Sleep(200);
            Console.Write("+ ");
         }
      }

      static void MyTask2()
      {
         Console.WriteLine("MyTask2 ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
         for (int i = 0; i < 10; i++)
         {
            Thread.Sleep(200);
            Console.Write("- ");
         }
      }

      static void Main()
      {
         Console.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
         List<Task> tasks = new List<Task>();
         TaskScheduler scheduler = new DelayTaskScheduler();
         TaskFactory factory = new TaskFactory(scheduler);
         tasks.Add(factory.StartNew(MyTask1));
         tasks.Add(factory.StartNew(MyTask2));
         Task.WaitAll(tasks.ToArray());
         Console.WriteLine("\nВсе задачи завершены.");
      }
   }

   class DelayTaskScheduler : TaskScheduler
   {
      private readonly Queue<Task> _queue = new Queue<Task>();
      // Вызывается 1-ым.
      protected override void QueueTask(Task task)
      {
         Console.WriteLine("QueueTask");
         _queue.Enqueue(task);
         WaitCallback callback = state => TryExecuteTask(_queue.Dequeue());
         // Асинхронный вызов задач.
         ThreadPool.QueueUserWorkItem(callback, null);
      }

      // Вызывается 2-ым.
      protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
      {
         Console.WriteLine("TryExecuteTaskInline");
         //base.TryExecuteTask(_queue.Dequeue());
         return false; // return true; - Будет исключение.
      }

      protected override IEnumerable<Task> GetScheduledTasks()
      {
         return _queue;
      }
   }
}