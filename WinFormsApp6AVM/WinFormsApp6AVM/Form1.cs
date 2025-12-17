using System;
using System.Globalization;
using System.Windows.Forms;
using ZedGraph;

namespace OduZedGraph
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Build();
        }

        double F(double x, double y) => y - x * x + 1.0;
        double Exact(double x) => (x + 1.0) * (x + 1.0) - 0.5 * Math.Exp(x);

        private void btnBuild_Click(object sender, EventArgs e)
        {
            Build();
        }

        void Build()
        {
            double a = 0.0, b = 1.0, y0 = 0.5;
            double h1 = Parse(tbH1.Text, 0.1);
            double h2 = Parse(tbH2.Text, 0.05);

            var pane = zed.GraphPane;
            pane.CurveList.Clear();
            pane.Title.Text = "ОДУ: Эйлер и РК2 на [0;1]";
            pane.XAxis.Title.Text = "x";
            pane.YAxis.Title.Text = "y";

            var exactPts = new PointPairList();
            for (int i = 0; i <= 1000; i++)
            {
                double x = a + (b - a) * i / 1000.0;
                exactPts.Add(x, Exact(x));
            }
            pane.AddCurve("Exact", exactPts, System.Drawing.Color.Black);

            var e1 = SolveEuler(a, b, y0, h1);
            var e2 = SolveEuler(a, b, y0, h2);
            var r1 = SolveRK2(a, b, y0, h1);
            var r2 = SolveRK2(a, b, y0, h2);

            pane.AddCurve("Euler h1", ToPts(e1.x, e1.y), System.Drawing.Color.Red);
            pane.AddCurve("Euler h2", ToPts(e2.x, e2.y), System.Drawing.Color.Orange);
            pane.AddCurve("RK2 h1", ToPts(r1.x, r1.y), System.Drawing.Color.Blue);
            pane.AddCurve("RK2 h2", ToPts(r2.x, r2.y), System.Drawing.Color.Green);

            double yEndExact = Exact(b);
            double errE1 = Math.Abs(e1.y[e1.y.Length - 1] - yEndExact);
            double errE2 = Math.Abs(e2.y[e2.y.Length - 1] - yEndExact);
            double errR1 = Math.Abs(r1.y[r1.y.Length - 1] - yEndExact);
            double errR2 = Math.Abs(r2.y[r2.y.Length - 1] - yEndExact);

            lblInfo.Text =
                "Ошибки в x=1: Euler(h1)=" + errE1.ToString("G6", CultureInfo.InvariantCulture) +
                "  Euler(h2)=" + errE2.ToString("G6", CultureInfo.InvariantCulture) +
                "  RK2(h1)=" + errR1.ToString("G6", CultureInfo.InvariantCulture) +
                "  RK2(h2)=" + errR2.ToString("G6", CultureInfo.InvariantCulture);

            zed.AxisChange();
            zed.Invalidate();
        }

        PointPairList ToPts(double[] xs, double[] ys)
        {
            var list = new PointPairList();
            for (int i = 0; i < xs.Length; i++)
                list.Add(xs[i], ys[i]);
            return list;
        }

        (double[] x, double[] y) SolveEuler(double a, double b, double y0, double h)
        {
            int n = (int)Math.Round((b - a) / h);
            if (n < 1) n = 1;
            h = (b - a) / n;

            double[] x = new double[n + 1];
            double[] y = new double[n + 1];
            x[0] = a; y[0] = y0;

            for (int i = 0; i < n; i++)
            {
                y[i + 1] = y[i] + h * F(x[i], y[i]);
                x[i + 1] = x[i] + h;
            }
            return (x, y);
        }

        (double[] x, double[] y) SolveRK2(double a, double b, double y0, double h)
        {
            int n = (int)Math.Round((b - a) / h);
            if (n < 1) n = 1;
            h = (b - a) / n;

            double[] x = new double[n + 1];
            double[] y = new double[n + 1];
            x[0] = a; y[0] = y0;

            for (int i = 0; i < n; i++)
            {
                double k1 = F(x[i], y[i]);
                double k2 = F(x[i] + h / 2.0, y[i] + h * k1 / 2.0);
                y[i + 1] = y[i] + h * k2;
                x[i + 1] = x[i] + h;
            }
            return (x, y);
        }

        double Parse(string s, double def)
        {
            double v;
            if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out v))
                return v;
            return def;
        }
    }
}
