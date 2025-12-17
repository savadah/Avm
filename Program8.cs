using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        Console.WriteLine("Выбери задачу:");
        Console.WriteLine("1 - Полином Ньютона (интерполяция)");
        Console.WriteLine("2 - Обратная матрица методом Гаусса");
        Console.Write("Ввод: ");
        int choice = int.Parse(Console.ReadLine() ?? "1");

        if (choice == 1) NewtonInterpolation();
        else if (choice == 2) InverseMatrixGauss();
        else Console.WriteLine("Неизвестный выбор.");
    }

    // Метод Ньютона (интерполяционный полином)
    static void NewtonInterpolation()
    {
        Console.WriteLine("\nПолином Ньютона.");
        Console.Write("Введите n (кол-во точек): ");
        int n = int.Parse(Console.ReadLine() ?? "0");

        double[] x = new double[n];
        double[] y = new double[n];

        Console.WriteLine("Введите точки (x, y) построчно. Дроби через точку.");
        for (int i = 0; i < n; i++)
        {
            Console.Write("x[" + i + "] = ");
            x[i] = ReadDouble();
            Console.Write("y[" + i + "] = ");
            y[i] = ReadDouble();
        }

        double[,] dd = new double[n, n];
        for (int i = 0; i < n; i++)
            dd[i, 0] = y[i];

        for (int j = 1; j < n; j++)
        {
            for (int i = 0; i < n - j; i++)
            {
                double denom = x[i + j] - x[i];
                dd[i, j] = (dd[i + 1, j - 1] - dd[i, j - 1]) / denom;
            }
        }

        double[] c = new double[n];
        for (int j = 0; j < n; j++)
            c[j] = dd[0, j];

        Console.WriteLine("\nКоэффициенты (c0..c{0}):", n - 1);
        for (int i = 0; i < n; i++)
            Console.WriteLine("c[" + i + "] = " + c[i].ToString("G17", CultureInfo.InvariantCulture));

        Console.Write("\nВведите x, где посчитать P(x): ");
        double X = ReadDouble();

        double px = EvaluateNewton(x, c, X);
        Console.WriteLine("P(" + X.ToString("G17", CultureInfo.InvariantCulture) + ") = " +
                          px.ToString("G17", CultureInfo.InvariantCulture));
    }

    static double EvaluateNewton(double[] x, double[] c, double X)
    {
        double result = 0.0;
        double mult = 1.0;
        for (int k = 0; k < c.Length; k++)
        {
            result += c[k] * mult;
            mult *= (X - x[k]);
        }
        return result;
    }

    // Метод Гаусса (Гаусс-Жордан) для обратной матрицы
    static void InverseMatrixGauss()
    {
        Console.WriteLine("\nОбратная матрица методом Гаусса.");
        Console.Write("Введите n (размер n x n): ");
        int n = int.Parse(Console.ReadLine() ?? "0");

        double[,] a = new double[n, n];
        Console.WriteLine("Введите матрицу A построчно (дроби через точку):");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write("A[" + i + "," + j + "] = ");
                a[i, j] = ReadDouble();
            }
        }

        double[,] aug = new double[n, 2 * n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
                aug[i, j] = a[i, j];

            for (int j = 0; j < n; j++)
                aug[i, n + j] = (i == j) ? 1.0 : 0.0;
        }

        for (int col = 0; col < n; col++)
        {
            int pivotRow = col;
            double best = Math.Abs(aug[col, col]);
            for (int r = col + 1; r < n; r++)
            {
                double v = Math.Abs(aug[r, col]);
                if (v > best)
                {
                    best = v;
                    pivotRow = r;
                }
            }

            if (best == 0.0)
            {
                Console.WriteLine("Матрица вырожденная, обратной нет.");
                return;
            }

            if (pivotRow != col)
                SwapRows(aug, pivotRow, col);

            double pivot = aug[col, col];
            for (int j = 0; j < 2 * n; j++)
                aug[col, j] /= pivot;

            for (int r = 0; r < n; r++)
            {
                if (r == col) continue;
                double factor = aug[r, col];
                for (int j = 0; j < 2 * n; j++)
                    aug[r, j] -= factor * aug[col, j];
            }
        }

        double[,] inv = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                inv[i, j] = aug[i, n + j];

        Console.WriteLine("\nA^{-1} =");
        PrintMatrix(inv);
    }

    static void SwapRows(double[,] m, int r1, int r2)
    {
        int cols = m.GetLength(1);
        for (int j = 0; j < cols; j++)
        {
            double t = m[r1, j];
            m[r1, j] = m[r2, j];
            m[r2, j] = t;
        }
    }

    static void PrintMatrix(double[,] m)
    {
        int n = m.GetLength(0);
        int k = m.GetLength(1);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < k; j++)
                Console.Write(m[i, j].ToString("G10", CultureInfo.InvariantCulture).PadLeft(14));
            Console.WriteLine();
        }
    }

    static double ReadDouble()
    {
        return double.Parse(Console.ReadLine() ?? "0", CultureInfo.InvariantCulture);
    }
}
