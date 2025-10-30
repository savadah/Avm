using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("РЕШЕНИЕ СИСТЕМ ЛИНЕЙНЫХ УРАВНЕНИЙ\n");

        // Пример системы уравнений:
        // 4x + y + z = 1
        // x + 3y + z = 2  
        // x + y + 5z = 3

        double[,] A = {
            {4.8, 1, 1},
            {1, 3.1, 1},
            {1, 1, 5.3}
        };

        double[] f = { 1, 2, 3.2 };

        Console.WriteLine("Матрица A:");
        PrintMatrix(A);
        Console.WriteLine("\nВектор f: [" + string.Join(", ", f) + "]");

        // 1. Метод Гаусса
        Console.WriteLine("\n" + new string('=', 40));
        Console.WriteLine("МЕТОД ГАУССА");
        Console.WriteLine(new string('=', 40));

        double[] xGauss = SolveGauss(A, f);
        Console.WriteLine("Решение: " + FormatSolution(xGauss));
        PrintResidual(A, xGauss, f);

        // 2. Метод Холецкого
        Console.WriteLine("\n" + new string('=', 40));
        Console.WriteLine("МЕТОД ХОЛЕЦКОГО");
        Console.WriteLine(new string('=', 40));

        double[] xCholesky = SolveCholesky(A, f);
        Console.WriteLine("Решение: " + FormatSolution(xCholesky));
        PrintResidual(A, xCholesky, f);

        // Проверка решений
        Console.WriteLine("\n" + new string('=', 40));
        Console.WriteLine("ПРОВЕРКА РЕШЕНИЙ");
        Console.WriteLine(new string('=', 40));
        CheckSolutions(A, f, xGauss, xCholesky);
    }

    // Замена очень малых чисел на 0
    static string FormatSolution(double[] x)
    {
        return "[" + string.Join(", ", x.Select(val =>
            Math.Abs(val) < 1e-10 ? "0" : val.ToString("F6"))) + "]";
    }

    // 1. Метод Гаусса
    static double[] SolveGauss(double[,] A, double[] f)
    {
        int n = f.Length;
        double[,] extendedMatrix = new double[n, n + 1];

        // Создание расширенной матрицы [A | f]
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
                extendedMatrix[i, j] = A[i, j];
            extendedMatrix[i, n] = f[i];
        }

        // Прямой ход: приводим к треугольному виду
        for (int i = 0; i < n; i++)
        {
            // Нормализация текущей строки (диагональный элемент = 1)
            double pivot = extendedMatrix[i, i];
            for (int j = i; j <= n; j++)
                extendedMatrix[i, j] /= pivot;

            // Исключение переменной из нижних строк
            for (int k = i + 1; k < n; k++)
            {
                double factor = extendedMatrix[k, i];
                for (int j = i; j <= n; j++)
                    extendedMatrix[k, j] -= factor * extendedMatrix[i, j];
            }
        }

        // Обратный ход: находим решение
        double[] x = new double[n];
        for (int i = n - 1; i >= 0; i--)
        {
            x[i] = extendedMatrix[i, n];
            for (int j = i + 1; j < n; j++)
                x[i] -= extendedMatrix[i, j] * x[j];
        }

        return x;
    }

    // 2. Метод Холецкого - для симметричных матриц
    static double[] SolveCholesky(double[,] A, double[] f)
    {
        int n = f.Length;
        double[,] L = new double[n, n];

        // Разложение Холецкого: A = L * L^T
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                double sum = 0;

                // Сумма произведений элементов
                for (int k = 0; k < j; k++)
                    sum += L[i, k] * L[j, k];

                if (i == j)
                    // Диагональный элемент
                    L[i, j] = Math.Sqrt(A[i, i] - sum);
                else
                    // Недиагональный элемент
                    L[i, j] = (A[i, j] - sum) / L[j, j];
            }
        }

        // Решение L * y = f (прямой ход)
        double[] y = new double[n];
        for (int i = 0; i < n; i++)
        {
            double sum = 0;
            for (int j = 0; j < i; j++)
                sum += L[i, j] * y[j];
            y[i] = (f[i] - sum) / L[i, i];
        }

        // Решение L^T * x = y (обратный ход)
        double[] x = new double[n];
        for (int i = n - 1; i >= 0; i--)
        {
            double sum = 0;
            for (int j = i + 1; j < n; j++)
                sum += L[j, i] * x[j];  // L[j,i] - это транспонирование
            x[i] = (y[i] - sum) / L[i, i];
        }

        return x;
    }

    // Вычисление вектора невязки и нормы
    static void PrintResidual(double[,] A, double[] x, double[] f)
    {
        int n = f.Length;
        double[] r = new double[n];

        // r = A*x - f
        for (int i = 0; i < n; i++)
        {
            double sum = 0;
            for (int j = 0; j < n; j++)
                sum += A[i, j] * x[j];
            r[i] = sum - f[i];
        }

        Console.WriteLine("Вектор невязки: " + FormatSolution(r));

        // Норма невязки (корень из суммы квадратов компонент)
        double norm = Math.Sqrt(r.Sum(val => val * val));
        Console.WriteLine("Норма невязки: " + (norm < 1e-10 ? "~0" : norm.ToString("E2")));
    }

    // Проверка решений
    static void CheckSolutions(double[,] A, double[] f, double[] x1, double[] x2)
    {
        int n = f.Length;

        Console.WriteLine("\nПодстановка решений в исходные уравнения:\n");

        for (int i = 0; i < n; i++)
        {
            // Вычисление левой части для метода Гаусса
            double left1 = 0;
            for (int j = 0; j < n; j++)
                left1 += A[i, j] * x1[j];

            // Вычисление левой части для метода Холецкого
            double left2 = 0;
            for (int j = 0; j < n; j++)
                left2 += A[i, j] * x2[j];

            Console.WriteLine($"Уравнение {i + 1}:");
            Console.WriteLine($"  Метод Гаусса:  {left1:F6} = {f[i]}");
            Console.WriteLine($"  Метод Холецкого: {left2:F6} = {f[i]}");
        }

        // Проверка: совпадают ли решения
        bool solutionsMatch = true;
        for (int i = 0; i < n; i++)
        {
            if (Math.Abs(x1[i] - x2[i]) > 1e-10)
            {
                solutionsMatch = false;
                break;
            }
        }

        Console.WriteLine($"\nРешения совпадают: {(solutionsMatch ? "ДА" : "НЕТ")}");
    }

    // Вывод матрицы
    static void PrintMatrix(double[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
                Console.Write(matrix[i, j].ToString("F1") + "\t");
            Console.WriteLine();
        }
    }
}