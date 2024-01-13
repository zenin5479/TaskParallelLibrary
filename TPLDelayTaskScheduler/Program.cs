using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

// Планировщик задач

namespace TPLDelayTaskScheduler
{
    internal class Program
    {
        static void MyTask()
        {
            Console.WriteLine("MyTask ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(200);
                Console.Write("+ ");
            }
        }

        static void Main()
        {
            Console.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            TaskScheduler scheduler = new DelayTaskScheduler();
            TaskFactory factory = new TaskFactory(scheduler);
            Task task = factory.StartNew(MyTask);
            TaskAwaiter awaiter = task.GetAwaiter();
            while (!awaiter.IsCompleted)
            {
                Console.Write(".");
                Thread.Sleep(100);
            }
            //task.Wait(); // Не вызывать так как в DelayTaskScheduler используется AutoResetEvent
            Console.WriteLine("\nВсе задачи завершены.");
        }
    }

    class DelayTaskScheduler : TaskScheduler
    {
        private readonly Queue<Task> _queue = new Queue<Task>();
        private readonly AutoResetEvent _auto = new AutoResetEvent(false);
        protected override void QueueTask(Task task) // Вызывается автоматически фабрикой задач.
        {
            Console.WriteLine("QueueTask ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            _queue.Enqueue(task);
            WaitOrTimerCallback callback = (state, timedOut) => TryExecuteTask(_queue.Dequeue());
            // Асинхронный вызов задачи с задержкой в 2 секунды.
            #region Аргументы
            /*     1. _auto - от кого ждать сингнал.
                   2. callback - что выполнять.
                   3. null - 1-й аргумент Callback метода.
                   4. 2000 - интервал между вызовами Callback метода.
                   5. true - вызвать Callback метод один раз. false - вызывать Callback метод с интервалом.  */
            #endregion
            ThreadPool.RegisterWaitForSingleObject(_auto, callback, null, 2000, true);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return false;
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return _queue;
        }
    }
}