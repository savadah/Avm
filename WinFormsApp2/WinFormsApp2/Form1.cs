using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Windows.Forms;
using ZedGraph;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxFunction.SelectedIndex = 0;
            comboBoxMethod.SelectedIndex = 0;
            SetupGraph();
        }

        private void comboBoxFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Показываем параметр k только для функции e^(-kx)
            bool showK = comboBoxFunction.SelectedIndex == 1;
            numericUpDownK.Visible = showK;
            label4.Visible = showK;
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            CalculateAndPlot();
        }

        private void SetupGraph()
        {
            GraphPane pane = zedGraphControl.GraphPane;
            pane.Title.Text = "Приближение функций";
            pane.XAxis.Title.Text = "x";
            pane.YAxis.Title.Text = "f(x)";
            pane.CurveList.Clear();
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }

        private void CalculateAndPlot()
        {
            int n = (int)numericUpDownNodes.Value;
            double k = (double)numericUpDownK.Value;
            int functionIndex = comboBoxFunction.SelectedIndex;
            int methodIndex = comboBoxMethod.SelectedIndex;

            // Генерируем узлы интерполяции
            double[] xNodes, yNodes;

            if (methodIndex == 1) // Чебышевские узлы
            {
                xNodes = GenerateChebyshevNodes(n);
            }
            else // Равноотстоящие узлы
            {
                xNodes = GenerateEquidistantNodes(n);
            }

            // Вычисляем значения функции в узлах
            yNodes = new double[n];
            for (int i = 0; i < n; i++)
            {
                yNodes[i] = CalculateFunction(xNodes[i], functionIndex, k);
            }

            // Строим исходную функцию
            PointPairList originalPoints = new PointPairList();
            for (double x = -1.0; x <= 1.0; x += 0.01)
            {
                originalPoints.Add(x, CalculateFunction(x, functionIndex, k));
            }

            // Строим аппроксимацию
            PointPairList approximationPoints = new PointPairList();
            if (methodIndex == 2) // Кубический сплайн
            {
                approximationPoints = CalculateCubicSpline(xNodes, yNodes, functionIndex, k);
            }
            else // Полином Лагранжа
            {
                for (double x = -1.0; x <= 1.0; x += 0.01)
                {
                    double y = LagrangeInterpolation(x, xNodes, yNodes);
                    approximationPoints.Add(x, y);
                }
            }

            // Отображаем узлы интерполяции
            PointPairList nodePoints = new PointPairList();
            for (int i = 0; i < n; i++)
            {
                nodePoints.Add(xNodes[i], yNodes[i]);
            }

            // Очищаем график и добавляем новые кривые
            GraphPane pane = zedGraphControl.GraphPane;
            pane.CurveList.Clear();

            LineItem originalCurve = pane.AddCurve("Исходная функция",
                originalPoints, System.Drawing.Color.Blue, SymbolType.None);
            originalCurve.Line.Width = 2f;

            LineItem approxCurve = pane.AddCurve(GetMethodName(methodIndex),
                approximationPoints, System.Drawing.Color.Red, SymbolType.None);
            approxCurve.Line.Width = 1.5f;

            LineItem nodesCurve = pane.AddCurve("Узлы интерполяции",
                nodePoints, System.Drawing.Color.Green, SymbolType.Circle);
            nodesCurve.Line.IsVisible = false;
            nodesCurve.Symbol.Fill = new Fill(System.Drawing.Color.Green);
            nodesCurve.Symbol.Size = 5;

            pane.Title.Text = $"Приближение функции: {comboBoxFunction.Text}";
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }

        private double[] GenerateEquidistantNodes(int n)
        {
            double[] nodes = new double[n];
            for (int i = 0; i < n; i++)
            {
                nodes[i] = -1.0 + 2.0 * i / (n - 1);
            }
            return nodes;
        }

        private double[] GenerateChebyshevNodes(int n)
        {
            double[] nodes = new double[n];
            for (int i = 0; i < n; i++)
            {
                nodes[i] = Math.Cos(Math.PI * (2 * i + 1) / (2 * n));
            }
            return nodes;
        }

        private double CalculateFunction(double x, int functionIndex, double k)
        {
            switch (functionIndex)
            {
                case 0: // f(x) = x^2
                    return x * x;
                case 1: // f(x) = e^(-kx)
                    return Math.Exp(-k * x);
                case 2: // f(x) = 1/(1+25x^2)
                    return 1.0 / (1.0 + 25 * x * x);
                case 3: // f(x) = |x|
                    return Math.Abs(x);
                default:
                    return 0;
            }
        }

        private double LagrangeInterpolation(double x, double[] xNodes, double[] yNodes)
        {
            double result = 0;
            int n = xNodes.Length;

            for (int i = 0; i < n; i++)
            {
                double term = yNodes[i];
                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        term *= (x - xNodes[j]) / (xNodes[i] - xNodes[j]);
                    }
                }
                result += term;
            }
            return result;
        }

        private PointPairList CalculateCubicSpline(double[] xNodes, double[] yNodes, int functionIndex, double k)
        {
            PointPairList splinePoints = new PointPairList();
            int n = xNodes.Length;

            // Реализация кубического сплайна
            for (double x = -1.0; x <= 1.0; x += 0.01)
            {
                // Находим интервал, в который попадает x
                int interval = 0;
                for (int i = 0; i < n - 1; i++)
                {
                    if (x >= xNodes[i] && x <= xNodes[i + 1])
                    {
                        interval = i;
                        break;
                    }
                }

                // Линейная интерполяция
                double t = (x - xNodes[interval]) / (xNodes[interval + 1] - xNodes[interval]);
                double y = (1 - t) * yNodes[interval] + t * yNodes[interval + 1];

                splinePoints.Add(x, y);
            }

            return splinePoints;
        }

        private string GetMethodName(int methodIndex)
        {
            switch (methodIndex)
            {
                case 0: return "Полином Лагранжа (равноот.)";
                case 1: return "Полином Лагранжа (Чебышев)";
                case 2: return "Кубический сплайн";
                default: return "Аппроксимация";
            }
        }
    }
}