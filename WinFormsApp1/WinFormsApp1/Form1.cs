using System;
using System.Globalization;
using System.Windows.Forms;
using System.Collections.Generic;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private readonly Dictionary<string, Func<double, double>> _funcs;

        public Form1()
        {
            InitializeComponent();

            _funcs = new Dictionary<string, Func<double, double>>()
            {
                { "sin(x)", x => Math.Sin(x) },
                { "cos(x)", x => Math.Cos(x) },
                { "x^2", x => x * x },
                { "exp(-x^2)", x => Math.Exp(-x * x) },
                { "ln(1+x)", x => Math.Log(1 + x) },
                { "sqrt(x)", x => Math.Sqrt(x) }
            };
        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = cbFunction.SelectedItem?.ToString() ?? "sin(x)";
                if (!_funcs.TryGetValue(selected, out var f))
                {
                    MessageBox.Show("Выберите корректную функцию.");
                    return;
                }

                if (!double.TryParse(txtA.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double a) ||
                    !double.TryParse(txtB.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double b))
                {
                    MessageBox.Show("Неверные границы интегрирования.");
                    return;
                }

                if (b <= a)
                {
                    MessageBox.Show("Правая граница должна быть больше левой (b > a).");
                    return;
                }

                // Проверка области допустимости
                if (selected == "ln(1+x)" && a <= -1)
                {
                    MessageBox.Show("Для ln(1+x) требуется a > -1 на интервале.");
                    return;
                }
                if (selected == "sqrt(x)" && a < 0)
                {
                    MessageBox.Show("Для sqrt(x) требуется a >= 0 на интервале.");
                    return;
                }

                string method = cbMethod.SelectedItem?.ToString() ?? "trapezoid";
                int n = (int)nudN.Value;

                bool useRunge = double.TryParse(txtEps.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double eps) && eps > 0;

                if (useRunge)
                {
                    var res = Integrator.RungeAdaptive(method, f, a, b, eps, n0: n);
                    lblResult.Text = $"Функция: {selected}\nМетод: {method}\nИнтеграл ≈ {res.value:G12}\nРазбиений: {res.n}\nПогрешность: {res.error:E6}";
                }
                else
                {
                    double val = Integrator.IntegrateByName(method, f, a, b, n);
                    lblResult.Text = $"Функция: {selected}\nМетод: {method}\nИнтеграл ≈ {val:G12}\nРазбиений: {n}\n(Введите eps для Рунге)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }

    // Методы интегрирования
    public static class Integrator
    {
        // Левые прямоугольники
        public static double LeftRect(Func<double, double> f, double a, double b, int n)
        {
            double h = (b - a) / n;
            double s = 0;
            for (int i = 0; i < n; i++)
                s += f(a + i * h);
            return s * h;
        }

        // Метод трапеций
        public static double Trapezoid(Func<double, double> f, double a, double b, int n)
        {
            double h = (b - a) / n;
            double s = 0.5 * (f(a) + f(b));
            for (int i = 1; i < n; i++)
                s += f(a + i * h);
            return s * h;
        }

        // Выбор метода
        public static double IntegrateByName(string method, Func<double, double> f, double a, double b, int n)
        {
            return method switch
            {
                "левый" => LeftRect(f, a, b, n),
                "трапеций" => Trapezoid(f, a, b, n),
                _ => throw new ArgumentException("Неизвестный метод")
            };
        }

        // Уточнение по правилу Рунге
        public static (double value, int n, double error) RungeAdaptive(
            string method, Func<double, double> f, double a, double b, double eps, int n0 = 4, int maxIter = 20)
        {
            int n = Math.Max(1, n0);
            int p = method == "trapezoid" ? 2 : 1;
            double I_n = IntegrateByName(method, f, a, b, n);

            for (int iter = 0; iter < maxIter; iter++)
            {
                int n2 = n * 2;
                double I_2n = IntegrateByName(method, f, a, b, n2);
                double err = Math.Abs((I_2n - I_n) / (Math.Pow(2, p) - 1));
                if (err < eps) return (I_2n, n2, err);
                n = n2;
                I_n = I_2n;
            }

            return (I_n, n, Math.Abs(I_n));
        }
    }
}
