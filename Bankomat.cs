using System;
using System.Threading;

Account account = new Account(0m);

bool work = true;

while (work)
{
    Console.WriteLine("1. Пополнить счет");
    Console.WriteLine("2. Снять со счета");
    Console.WriteLine("3. Показать баланс");
    Console.WriteLine("0. Выход");
    Console.Write("Выберите пункт: ");

    int choice;

    try
    {
        choice = Convert.ToInt32(Console.ReadLine());
    }
    catch
    {
        Console.WriteLine("Ошибка: нужно ввести число.");
        continue;
    }
    finally
    {
    }

    Thread thread = null;

    if (choice == 1)
    {
        thread = new Thread(() =>
        {
            Console.Write("Введите сумму пополнения: ");

            decimal money;

            try
            {
                money = Convert.ToDecimal(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Ошибка: нужно ввести число.");
                return;
            }
            finally
            {
            }

            if (money <= 0)
            {
                Console.WriteLine("Ошибка: сумма должна быть больше нуля.");
                return;
            }

            account.Credit(money);

            Console.WriteLine("Счет пополнен.");
        });
    }
    else if (choice == 2)
    {
        thread = new Thread(() =>
        {
            Console.Write("Введите сумму списания: ");

            decimal money;

            try
            {
                money = Convert.ToDecimal(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Ошибка: нужно ввести число.");
                return;
            }
            finally
            {
            }

            if (money <= 0)
            {
                Console.WriteLine("Ошибка: сумма должна быть больше нуля.");
                return;
            }

            bool result = account.Debit(money);

            if (result)
            {
                Console.WriteLine("Списание выполнено.");
            }
            else
            {
                Console.WriteLine("Недостаточно средств.");
            }
        });
    }
    else if (choice == 3)
    {
        thread = new Thread(() =>
        {
            Console.WriteLine("Баланс: " + account.Balance);
        });
    }
    else if (choice == 0)
    {
        work = false;
    }
    else
    {
        Console.WriteLine("Ошибка: такого пункта меню нет.");
    }

    if (thread != null)
    {
        thread.Start();
        thread.Join();
    }
}

class Account
{
    decimal balance;
    object locker = new();

    public Account(decimal startBalance)
    {
        balance = startBalance;
    }

    public decimal Balance
    {
        get
        {
            bool lockToken = false;
            Monitor.Enter(locker, ref lockToken);

            try
            {
                decimal result = balance;
                return result;
            }
            finally
            {
                Monitor.Exit(locker);
            }
        }
    }

    public void Credit(decimal money)
    {
        bool lockToken = false;
        Monitor.Enter(locker, ref lockToken);

        try
        {
            balance = balance + money;
        }
        finally
        {
            Monitor.Exit(locker);
        }
    }

    public bool Debit(decimal money)
    {
        bool lockToken = false;
        Monitor.Enter(locker, ref lockToken);

        try
        {
            if (balance >= money)
            {
                balance = balance - money;
                return true;
            }
            else
            {
                return false;
            }
        }
        finally
        {
            Monitor.Exit(locker);
        }
    }
}