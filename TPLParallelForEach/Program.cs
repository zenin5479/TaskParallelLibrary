using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;


// Применение метода Parallel.ForEach() для организации параллельно выполняемого цикла обработки данных.
// Пример выполнять через (CTRL+F5).

namespace TPLParallelForEach
{
    class Element
    {
        public int A { get; set; }
    }

    internal class Program
    {
        static void Main()
        {
            IList<Element> elements = new List<Element>();
            // Недопустимо инициализировать коллекцию в цикле foreach.
            //foreach (Element element in elements)            
            //    element = new Element();

            // Инициализация коллекции в 10 000 000 элементов.
            for (int i = 0; i < 10000000; i++)
                elements.Add(new Element() { A = i });
            Stopwatch timer = new Stopwatch();
            timer.Start();
            // Последовательное преобразование.
            foreach (Element element in elements)
                element.A = 111 * 222 * 333 / 444;
            timer.Stop();
            Console.WriteLine("Обычный цикл foreach: " + timer.Elapsed.TotalMilliseconds);
            timer.Reset();
            timer.Start();

            // Параллельное преобразование.
            Parallel.ForEach(elements, element => element.A = 111 * 222 * 333 / 444);
            timer.Stop();
            Console.WriteLine("Параллельный цикл ForEach : " + timer.Elapsed.TotalMilliseconds);
            Console.WriteLine("\nОсновной поток завершен.");

            // Задержка
            Console.ReadKey();
        }
    }
}