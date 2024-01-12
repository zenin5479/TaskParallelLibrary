using System;
using System.Threading;
using System.Threading.Tasks;

// С завершением метода Main завершается недовыполненная задача MyTask
// [завершается работа вторичного потока].
// По умолчанию IsBackground == true

namespace TPLIsBackground
{
    class Program
    {
        static void MyTask()
        {
            // Снять комментарий.
            Thread.CurrentThread.IsBackground = false;
            for (int i = 0; i < 80; i++)
            {
                Thread.Sleep(100);
                Console.Write(".");
            }
        }

        static void Main()
        {
            Task task = new Task(MyTask);
            task.Start();
            // Время на запуск задачи.
            Thread.Sleep(500);
            Console.WriteLine("\nMain завершен.");

            // Задержка
            Console.ReadKey();
        }
    }
}