using System;
using System.Globalization;

namespace Avm4
{
    class Program
    {
        static double ReadDouble()
        {
            return double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Решение СЛАУ A x = b итерационными методами.");
            Console.WriteLine("Система зашита в коде:");
            Console.WriteLine("5*x1 + 1*x2 + 1*x3 = 7");
            Console.WriteLine("1*x1 + 4*x2 + 2*x3 = 4");
            Console.WriteLine("1*x1 + 2*x2 + 5*x3 = 6");
            Console.WriteLine();

            // матрица A
            double[,] A = new double[3, 3];
            A[0, 0] = 5; A[0, 1] = 1; A[0, 2] = 1;
            A[1, 0] = 1; A[1, 1] = 4; A[1, 2] = 2;
            A[2, 0] = 1; A[2, 1] = 2; A[2, 2] = 5;

            // вектор b
            double[] b = new double[3];
            b[0] = 7;
            b[1] = 4;
            b[2] = 6;

            Console.WriteLine("Выберите метод:");
            Console.WriteLine("1 - Метод Якоби");
            Console.WriteLine("2 - Метод Зейделя");
            int method = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите начальное приближение x0 (3 числа):");
            double[] x0 = new double[3];
            for (int i = 0; i < 3; i++)
            {
                Console.Write("x0[" + i + "] = ");
                x0[i] = ReadDouble();
            }

            Console.Write("Введите eps (точность): ");
            double eps = ReadDouble();

            Console.Write("Введите максимальное число итераций: ");
            int maxIter = int.Parse(Console.ReadLine());

            double[] x;
            int iters;
            bool ok;

            if (method == 1)
            {
                ok = Jacobi(A, b, x0, eps, maxIter, out x, out iters);
                Console.WriteLine("Метод Якоби:");
            }
            else if (method == 2)
            {
                ok = GaussSeidel(A, b, x0, eps, maxIter, out x, out iters);
                Console.WriteLine("Метод Зейделя:");
            }
            else
            {
                Console.WriteLine("Неизвестный метод");
                return;
            }

            Console.WriteLine("Численное решение:");
            for (int i = 0; i < x.Length; i++)
            {
                Console.WriteLine("x[" + i + "] = " +
                    x[i].ToString("F6", CultureInfo.InvariantCulture));
            }

            Console.WriteLine("Количество итераций: " + iters);

            if (!ok)
                Console.WriteLine("Заданная точность не достигнута (останов по maxIter).");

            Console.ReadLine();
        }

        static double ResidualNorm(double[,] A, double[] x, double[] b)
        {
            int n = b.Length;
            double sum = 0.0;

            for (int i = 0; i < n; i++)
            {
                double ax = 0.0;
                for (int j = 0; j < n; j++)
                    ax += A[i, j] * x[j];

                double r = ax - b[i];
                sum += r * r;
            }

            return Math.Sqrt(sum);
        }

        // Метод Якоби
        static bool Jacobi(double[,] A, double[] b, double[] x0,
                           double eps, int maxIter,
                           out double[] x, out int iters)
        {
            int n = b.Length;
            double[] xOld = new double[n];
            double[] xNew = new double[n];

            for (int i = 0; i < n; i++)
                xOld[i] = x0[i];

            for (int iter = 1; iter <= maxIter; iter++)
            {
                for (int i = 0; i < n; i++)
                {
                    double sum = 0.0;
                    for (int j = 0; j < n; j++)
                        if (j != i)
                            sum += A[i, j] * xOld[j];

                    xNew[i] = (b[i] - sum) / A[i, i];
                }

                double norm = ResidualNorm(A, xNew, b);
                if (norm < eps)
                {
                    x = xNew;
                    iters = iter;
                    return true;
                }

                for (int i = 0; i < n; i++)
                    xOld[i] = xNew[i];
            }

            x = xNew;
            iters = maxIter;
            return false;
        }

        // Метод Зейделя
        static bool GaussSeidel(double[,] A, double[] b, double[] x0,
                                double eps, int maxIter,
                                out double[] x, out int iters)
        {
            int n = b.Length;
            x = new double[n];

            for (int i = 0; i < n; i++)
                x[i] = x0[i];

            for (int iter = 1; iter <= maxIter; iter++)
            {
                for (int i = 0; i < n; i++)
                {
                    double sum1 = 0.0;
                    double sum2 = 0.0;

                    for (int j = 0; j < i; j++)
                        sum1 += A[i, j] * x[j];

                    for (int j = i + 1; j < n; j++)
                        sum2 += A[i, j] * x[j];

                    x[i] = (b[i] - sum1 - sum2) / A[i, i];
                }

                double norm = ResidualNorm(A, x, b);
                if (norm < eps)
                {
                    iters = iter;
                    return true;
                }
            }

            iters = maxIter;
            return false;
        }
    }
}
