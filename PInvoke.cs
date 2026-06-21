using System;
using System.Runtime.InteropServices;

class Program
{
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    [DllImport("MathLibrary.dll", CallingConvention = CallingConvention.Cdecl)]
    static extern double Add(double a, double b);

    [DllImport("MathLibrary.dll", CallingConvention = CallingConvention.Cdecl)]
    static extern double Subtract(double a, double b);

    [DllImport("MathLibrary.dll", CallingConvention = CallingConvention.Cdecl)]
    static extern double Multiply(double a, double b);

    [DllImport("MathLibrary.dll", CallingConvention = CallingConvention.Cdecl)]
    static extern double Divide(double a, double b);

    static void Main()
    {
        MessageBox(IntPtr.Zero, "Роман Старостин", "ФИО", 0);

        Console.Write("Введите первое число: ");
        double firstNumber = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введите второе число: ");
        double secondNumber = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine();

        Console.WriteLine("Сложение: " + Add(firstNumber, secondNumber));
        Console.WriteLine("Вычитание: " + Subtract(firstNumber, secondNumber));
        Console.WriteLine("Умножение: " + Multiply(firstNumber, secondNumber));

        if (secondNumber == 0)
        {
            Console.WriteLine("Деление: нельзя делить на ноль");
        }
        else
        {
            Console.WriteLine("Деление: " + Divide(firstNumber, secondNumber));
        }

        Console.ReadKey();
    }
}