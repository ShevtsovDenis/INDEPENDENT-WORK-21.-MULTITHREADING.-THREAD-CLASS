using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/* Имеется пустой участок земли (двумерный массив) и план сада, который необходимо реализовать. Эту задачу выполняют два садовника, которые не хотят встречаться друг с другом. 
 * Первый садовник начинает работу с верхнего левого угла сада и перемещается слева направо, сделав ряд, он спускается вниз. 
 * Второй садовник начинает работу с нижнего правого угла сада и перемещается снизу вверх, сделав ряд, он перемещается влево. 
 * Если садовник видит, что участок сада уже выполнен другим садовником, он идет дальше. 
 * Садовники должны работать параллельно. Создать многопоточное приложение, моделирующее работу садовников.*/

namespace INDEPENDENT_WORK_21.MULTITHREADING.THREAD_CLASS
{
    class Program
    {
        const int n = 2;//создаем константу для указания размера массива
        const int k = 3;//создаем константу для указания размера массива
        static int[,] path = new int[n, k] { { 0, 1, 2 }, { 3, 4, 5 } };//создаем статический массив (чтобы методы класса его видели) чисел и сразу инициализируем

        static void Main(string[] args)
        {
            ThreadStart threadStart = new ThreadStart(Gardner1);//создаем делегат и связываем его с первым методом (садовник 1)
            Thread thread = new Thread(threadStart);//создаем экземпляр класса потока (объектно ориентированное представление потока)
            thread.Start();//запускаем поток

            Gardner2();//запускаем второй метод в основном потоке

            //выводим результирующий массив с отображением занятых ячеек
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    Console.Write("{0} ", path[i, j]);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
        static void Gardner1()//создаем метод для первого садовника
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    if (path[i, j] >= 0)
                    {
                        int delay = path[i, j];
                        path[i, j] = -1;
                        Thread.Sleep(delay);
                    }
                }
            }
        }
        static void Gardner2()//создаем метод для второго садовника
        {
            for (int j = k - 1; j < k && j >= 0; j--)
            {
                for (int i = n - 1; i < n && i >= 0; i--)
                {
                    if (path[i, j] >= 0)
                    {
                        int delay = path[i, j];
                        path[i, j] = -2;
                        Thread.Sleep(delay);
                    }
                }
            }
        }

    }
}
