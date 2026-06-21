using System;
using System.Threading;

Console.Write("Введите длину массива: ");

int length;

try
{
    length = Convert.ToInt32(Console.ReadLine());
}
catch
{
    Console.WriteLine("Ошибка: нужно ввести целое число.");
    return;
}

if (length <= 0)
{
    Console.WriteLine("Ошибка: длина массива должна быть больше нуля.");
    return;
}

int[] array = new int[length];
long[] resultArray = new long[length];

Random random = new Random();

for (int i = 0; i < array.Length; i++)
{
    array[i] = random.Next(10, 10001);
}

Console.WriteLine();
Console.WriteLine("Исходный массив:");

for (int i = 0; i < array.Length; i++)
{
    Console.Write(array[i] + " ");
}

int threadsCount = 4;

if (length < threadsCount)
{
    threadsCount = length;
}

Thread[] threads = new Thread[threadsCount];

int part = length / threadsCount;

for (int i = 0; i < threads.Length; i++)
{
    int start = i * part;
    int end;

    if (i == threads.Length - 1)
    {
        end = length;
    }
    else
    {
        end = start + part;
    }

    int localStart = start;
    int localEnd = end;

    threads[i] = new Thread(() =>
    {
        for (int j = localStart; j < localEnd; j++)
        {
            if (array[j] > 0)
            {
                resultArray[j] = (long)array[j] * array[j];
            }
        }
    });
}

for (int i = 0; i < threads.Length; i++)
{
    threads[i].Start();
}

for (int i = 0; i < threads.Length; i++)
{
    threads[i].Join();
}

Console.WriteLine();
Console.WriteLine();
Console.WriteLine("Массив после возведения положительных чисел в квадрат:");

for (int i = 0; i < resultArray.Length; i++)
{
    Console.Write(resultArray[i] + " ");
}