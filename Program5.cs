using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        double[,] A = new double[,]
        {
            { 4.0, 1.0, 1.0 },
            { 1.0, 3.0, 0.0 },
            { 1.0, 0.0, 2.0 }
        };

        Console.WriteLine("Поиск собственных чисел матрицы A итерационными методами.");
        Console.WriteLine("Матрица A:");
        PrintMatrix(A);

        Console.WriteLine();
        Console.Write("Введите eps (например 0.0001): ");
        double eps = double.Parse(Console.ReadLine() ?? "0.0001", CultureInfo.InvariantCulture);

        double lambdaPower;
        int iterPower;
        PowerIteration(A, eps, out lambdaPower, out iterPower);

        Console.WriteLine();
        Console.WriteLine("Метод прямой итерации:");
        Console.WriteLine("Наибольшее собственное значение ≈ " + lambdaPower.ToString("F6", CultureInfo.InvariantCulture));
        Console.WriteLine("Количество итераций: " + iterPower);

        double[,] AforJacobi = (double[,])A.Clone();
        int iterJacobi;
        double[] eigenJacobi = JacobiRotations(AforJacobi, eps, out iterJacobi);

        Console.WriteLine();
        Console.WriteLine("Метод вращений (Якоби):");
        for (int i = 0; i < eigenJacobi.Length; i++)
            Console.WriteLine("lambda" + (i + 1) + " ≈ " + eigenJacobi[i].ToString("F6", CultureInfo.InvariantCulture));
        Console.WriteLine("Количество итераций: " + iterJacobi);

        Console.WriteLine();
        Console.WriteLine("Нажмите Enter для выхода...");
        Console.ReadLine();
    }

    static void PrintMatrix(double[,] A)
    {
        int n = A.GetLength(0);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
                Console.Write(A[i, j].ToString("F2", CultureInfo.InvariantCulture) + "  ");
            Console.WriteLine();
        }
    }

    // 1) Метод прямой итерации
    static void PowerIteration(double[,] A, double eps, out double lambda, out int iter)
    {
        int n = A.GetLength(0);
        double[] x = new double[n];
        for (int i = 0; i < n; i++) x[i] = 1.0;

        double lambdaOld = 0.0;
        lambda = 0.0;

        int maxIter = 1000;
        iter = 0;

        for (int k = 1; k <= maxIter; k++)
        {
            double[] y = new double[n];
            for (int i = 0; i < n; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < n; j++) sum += A[i, j] * x[j];
                y[i] = sum;
            }

            double norm = 0.0;
            for (int i = 0; i < n; i++) norm += y[i] * y[i];
            norm = Math.Sqrt(norm);
            if (norm == 0) break;

            for (int i = 0; i < n; i++) x[i] = y[i] / norm;

            double[] Ax = new double[n];
            for (int i = 0; i < n; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < n; j++) sum += A[i, j] * x[j];
                Ax[i] = sum;
            }

            lambda = 0.0;
            for (int i = 0; i < n; i++) lambda += x[i] * Ax[i];

            iter = k;
            if (Math.Abs(lambda - lambdaOld) < eps) break;
            lambdaOld = lambda;
        }
    }

    // 2) Метод вращений (Якоби)
    static double[] JacobiRotations(double[,] A, double eps, out int iter)
    {
        int n = A.GetLength(0);
        int maxIter = 1000;
        iter = 0;

        while (iter < maxIter)
        {
            int p = 0, q = 1;
            double max = Math.Abs(A[p, q]);

            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                {
                    double v = Math.Abs(A[i, j]);
                    if (v > max)
                    {
                        max = v;
                        p = i;
                        q = j;
                    }
                }

            if (max < eps) break;

            double app = A[p, p];
            double aqq = A[q, q];
            double apq = A[p, q];

            double phi = 0.5 * Math.Atan2(2.0 * apq, app - aqq);
            double c = Math.Cos(phi);
            double s = Math.Sin(phi);

            for (int i = 0; i < n; i++)
            {
                if (i == p || i == q) continue;

                double aip = A[i, p];
                double aiq = A[i, q];

                double aipNew = c * aip + s * aiq;
                double aiqNew = -s * aip + c * aiq;

                A[i, p] = aipNew; A[p, i] = aipNew;
                A[i, q] = aiqNew; A[q, i] = aiqNew;
            }

            double appNew = c * c * app + 2.0 * c * s * apq + s * s * aqq;
            double aqqNew = s * s * app - 2.0 * c * s * apq + c * c * aqq;

            A[p, p] = appNew;
            A[q, q] = aqqNew;
            A[p, q] = 0.0;
            A[q, p] = 0.0;

            iter++;
        }

        double[] eigen = new double[n];
        for (int i = 0; i < n; i++) eigen[i] = A[i, i];
        return eigen;
    }
}
