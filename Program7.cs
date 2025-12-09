using System;
using System.Globalization;

namespace NonlinearEquation
{
    class Program
    {
        static double F(double x)
        {
            return x * x * x - x - 2;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("F(x) = x^3 - x - 2");
            Console.WriteLine();

            Console.WriteLine("Выберите метод:");
            Console.WriteLine("1 - Метод деления отрезка пополам");
            Console.WriteLine("2 - Метод секущих");
            int choice = int.Parse(Console.ReadLine());

            Console.Write("Введите левую границу a: ");
            double a = double.Parse(Console.ReadLine());

            Console.Write("Введите правую границу b: ");
            double b = double.Parse(Console.ReadLine());

            Console.Write("Введите точность (eps): ");
            double eps = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            Console.Write("Введите максимальное число итераций: ");
            int maxIter = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                Bisection(a, b, eps, maxIter);
            }
            else if (choice == 2)
            {
                Secant(a, b, eps, maxIter);
            }
            else
            {
                Console.WriteLine("Неизвестный выбор");
            }

            Console.ReadLine();
        }

        // Метод деления отрезка пополам
        static void Bisection(double a, double b, double eps, int maxIter)
        {
            double fa = F(a);
            double fb = F(b);

            if (fa * fb > 0)
            {
                Console.WriteLine("На отрезке [a,b] нет смены знака F(x). Метод бисекции может не сработать.");
                return;
            }

            int iter = 0;
            double c = 0.0;

            while (Math.Abs(b - a) > eps && iter < maxIter)
            {
                c = (a + b) / 2.0;
                double fc = F(c);

                if (fa * fc <= 0)
                {
                    b = c;
                    fb = fc;
                }
                else
                {
                    a = c;
                    fa = fc;
                }

                iter++;
            }

            double root = (a + b) / 2.0;
            Console.WriteLine();
            Console.WriteLine("Метод деления отрезка пополам:");
            Console.WriteLine("x = " + root);
            Console.WriteLine("Количество итераций: " + iter);
        }

        // Метод секущих
        static void Secant(double a, double b, double eps, int maxIter)
        {
            double x0 = a;
            double x1 = b;

            double f0 = F(x0);
            double f1 = F(x1);

            int iter = 0;

            while (Math.Abs(x1 - x0) > eps && iter < maxIter)
            {
                f0 = F(x0);
                f1 = F(x1);

                double denom = (f1 - f0);
                if (denom == 0)
                {
                    Console.WriteLine("Деление на ноль");
                    return;
                }

                double x2 = x1 - f1 * (x1 - x0) / denom;

                x0 = x1;
                x1 = x2;

                iter++;
            }

            Console.WriteLine();
            Console.WriteLine("Метод секущих:");
            Console.WriteLine("x = " + x1);
            Console.WriteLine("Количество итераций: " + iter);
        }
    }
}
