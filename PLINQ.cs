using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Задачи на класс Parallel и PLINQ");
        Console.WriteLine();

        ParallelFactorials();
        Console.WriteLine();

        ParallelFibonacci();
        Console.WriteLine();

        CountEvenNumbersPLINQ();
        Console.WriteLine();

        FindPrimeNumbersPLINQ();
        Console.WriteLine();

        SumOfSquaresPLINQ();

        Console.ReadLine();
    }

    // Задача 1.1: Параллельное вычисление факториалов
    static void ParallelFactorials()
    {
        Console.WriteLine("Задача Parallel 1.1: Факториалы");

        int[] numbers = { 3, 4, 5, 6, 7 };

        Parallel.ForEach(numbers, number =>
        {
            long result = Factorial(number);
            Console.WriteLine($"Факториал {number} = {result}");
        });
    }

    static long Factorial(int number)
    {
        long result = 1;

        for (int i = 1; i <= number; i++)
        {
            result *= i;
        }

        return result;
    }

    // Задача 1.2: Параллельное вычисление чисел Фибоначчи
    static void ParallelFibonacci()
    {
        Console.WriteLine("Задача Parallel 1.2: Числа Фибоначчи");

        int[] numbers = { 5, 6, 7, 8, 9, 10 };

        Parallel.ForEach(numbers, number =>
        {
            long result = Fibonacci(number);
            Console.WriteLine($"Фибоначчи {number} = {result}");
        });
    }

    static long Fibonacci(int number)
    {
        if (number <= 1)
        {
            return number;
        }

        long first = 0;
        long second = 1;
        long result = 0;

        for (int i = 2; i <= number; i++)
        {
            result = first + second;
            first = second;
            second = result;
        }

        return result;
    }

    // Задача PLINQ 1.1: Подсчёт чётных чисел
    static void CountEvenNumbersPLINQ()
    {
        Console.WriteLine("Задача PLINQ 1.1: Подсчёт чётных чисел");

        List<int> numbers = Enumerable.Range(1, 100).ToList();

        int count = numbers
            .AsParallel()
            .Count(number => number % 2 == 0);

        Console.WriteLine($"Количество чётных чисел: {count}");
    }

    // Задача PLINQ 1.2: Нахождение простых чисел
    static void FindPrimeNumbersPLINQ()
    {
        Console.WriteLine("Задача PLINQ 1.2: Нахождение простых чисел");

        List<int> numbers = Enumerable.Range(1, 100).ToList();

        var primeNumbers = numbers
            .AsParallel()
            .Where(number => IsPrime(number))
            .ToList();

        Console.WriteLine("Простые числа:");
        Console.WriteLine(string.Join(", ", primeNumbers));
    }

    static bool IsPrime(int number)
    {
        if (number < 2)
        {
            return false;
        }

        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0)
            {
                return false;
            }
        }

        return true;
    }

    // Задача PLINQ 1.3: Сумма квадратов
    static void SumOfSquaresPLINQ()
    {
        Console.WriteLine("Задача PLINQ 1.3: Сумма квадратов");

        List<int> numbers = Enumerable.Range(1, 10).ToList();

        int sum = numbers
            .AsParallel()
            .Select(number => number * number)
            .Sum();

        Console.WriteLine($"Сумма квадратов чисел от 1 до 10 = {sum}");
    }
}