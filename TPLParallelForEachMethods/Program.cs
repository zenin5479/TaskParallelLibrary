using System;
using System.Collections.Generic;
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
      //private static int[] _data;
      static void Main()
      {
         // Вариант 1
         Console.WriteLine("Основной поток запущен.");
         IList<Element> elements = new List<Element>();
         // Недопустимо инициализировать коллекцию в цикле foreach.
         //foreach (Element element in elements)            
         //    element = new Element();

         //Инициализация коллекции в 10 000 000 элементов.
         for (int i = 0; i < 10000000; i++)
            elements.Add(new Element { A = i });
         // Помещение отрицательного значения в коллекцию.
         elements[300].A = -7;
         Action<Element, ParallelLoopState> transform = (element, state) =>
         {
            // ЕСЛИ: Отрицательное значение
            // ТО:   Прервать цикл
            if (element.A < 0)
               state.Break();
            Console.WriteLine("Значение: " + element.A);
         };
         // Использование цикла, параллельно выполняемого методом ForeEach, для отображения данных на экране.
         ParallelLoopResult loopResult = Parallel.ForEach(elements, transform);
         if (!loopResult.IsCompleted)
         {
            Console.WriteLine("Цикл завершился преждевременно." + " Элемент {0} имеет отрицательное значение.\n",
                loopResult.LowestBreakIteration);
         }
         Console.WriteLine("Основной поток завершен.");

         // Вариант 2
         //Console.WriteLine("Основной поток запущен.");
         //_data = new int[100000000];
         //// Инициализация массива в цикле.
         //for (int i = 0; i < _data.Length; i++)
         //    _data[i] = i;
         //// Помещение отрицательного значения в массив _data.
         //_data[300] = -7;
         //// Использование цикла, параллельно выполняемого методом ForeEach, для отображения данных на экране.
         //ParallelLoopResult loopResult = Parallel.ForEach(_data, (data, state) =>
         //{
         //    // Прервать цикл при обнаружении отрицательного значения.
         //    if (data < 0)
         //        state.Break();
         //    Console.WriteLine("Значение: " + data);
         //});
         //// Проверить, завершился ли цикл. 
         //if (!loopResult.IsCompleted)
         //    Console.WriteLine("Цикл завершился преждевременно." + " Элемент {0} имеет отрицательное значение.\n",
         //        loopResult.LowestBreakIteration);
         //Console.WriteLine("Основной поток завершен.");

         // Задержка
         Console.ReadKey();
      }
   }
}