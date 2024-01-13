using System;
using System.Threading;
using System.Threading.Tasks;

// Продолжение - автоматический запуск новой задачи, после завершения первой задачи.

namespace TPLContinuation
{
    internal class Program
    {
        // Метод который будет выполнен как задача.
        static void MyTask()
        {
            for (int count = 0; count < 10; count++)
            {
                Thread.Sleep(100);
                Console.Write("+");
            }
        }

        // Метод исполняемый как продолжение задачи.
        static void ContinuationTask(Task task)
        {
            for (int count = 0; count < 10; count++)
            {
                Thread.Sleep(100);
                Console.Write("-");
            }
        }

        static void Main()
        {
            // Создание задачи.
            Action action = MyTask;
            Task task = new Task(action);
            // Создание продолжения задачи.
            Action<Task> continuation = ContinuationTask;
            task.ContinueWith(continuation);
            // Выполнение последовательности задач.
            task.Start();

            // Задержка
            Console.ReadKey();
        }
    }
}