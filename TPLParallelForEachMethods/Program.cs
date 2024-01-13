using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

// Использование типов ParallelLoopResult и ParallelLoopState и
// метода Break() для параллельного выполнения цикла.
// ParallelLoopState - позволяет управлять итерациями параллельных циклов,
// экземпляр этого класса предоставляется каждому циклу автоматически.
// Метод parallelLoopState.Break() - прерывает выполнение цикла.
// ParallelLoopResult - предоставляет состояние выполнения цикла Parallel.
// Свойство parallelLoopResult.IsCompleted == true, если цикл доработал до конца,
// иначе, если цикл был прерван, то IsCompleted == false.

namespace TPLParallelForEachMethods
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
            Action<int> initialize = (i) => elements.Add(new Element() { A = i });
            // Инициализация коллекции в 10 000 элементов.
            Parallel.For(0, 1000, initialize);
            // Помещение отрицательного значения в коллекцию.
            elements[300].A = -1;
            Action<Element, ParallelLoopState> transform = (element, state) =>
            {
                // ЕСЛИ: Отрицательное значение
                // ТО:   Прервать цикл
                if (element.A < 0)
                    state.Break();
                Thread.Sleep(1);
                element.A = 111 * 222 * 333 / 444;
            };

            // Использование цикла, параллельно выполняемого методом ForeEach, для отображения данных на экране.
            ParallelLoopResult loopResult = Parallel.ForEach(elements, transform);
            if (!loopResult.IsCompleted)
            {
                Console.WriteLine("\nОбход коллекции завершился преждевременно." + " Элемент {0} имеет отрицательное значение.\n",
                    loopResult.LowestBreakIteration);
            }
            Console.WriteLine("Основной поток завершен.");

            // Задержка
            Console.ReadKey();
        }
    }
}