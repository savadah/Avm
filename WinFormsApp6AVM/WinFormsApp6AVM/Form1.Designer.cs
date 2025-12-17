using System.Windows.Forms;

namespace OduZedGraph
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panel1;
        private Label label1;
        private Label label2;
        private TextBox tbH1;
        private TextBox tbH2;
        private Button btnBuild;
        private Label lblInfo;
        private ZedGraph.ZedGraphControl zed;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panel1 = new Panel();
            this.label1 = new Label();
            this.label2 = new Label();
            this.tbH1 = new TextBox();
            this.tbH2 = new TextBox();
            this.btnBuild = new Button();
            this.lblInfo = new Label();
            this.zed = new ZedGraph.ZedGraphControl();

            this.panel1.SuspendLayout();
            this.SuspendLayout();

            this.panel1.Dock = DockStyle.Top;
            this.panel1.Height = 50;

            this.label1.Left = 10; this.label1.Top = 15; this.label1.Width = 30; this.label1.Text = "h1:";
            this.tbH1.Left = 40; this.tbH1.Top = 12; this.tbH1.Width = 80; this.tbH1.Text = "0.1";

            this.label2.Left = 130; this.label2.Top = 15; this.label2.Width = 30; this.label2.Text = "h2:";
            this.tbH2.Left = 160; this.tbH2.Top = 12; this.tbH2.Width = 80; this.tbH2.Text = "0.05";

            this.btnBuild.Left = 260; this.btnBuild.Top = 10; this.btnBuild.Width = 120; this.btnBuild.Height = 28;
            this.btnBuild.Text = "Построить";
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);

            this.lblInfo.Left = 400; this.lblInfo.Top = 15; this.lblInfo.Width = 560; this.lblInfo.Text = "";

            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbH1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbH2);
            this.panel1.Controls.Add(this.btnBuild);
            this.panel1.Controls.Add(this.lblInfo);

            this.zed.Dock = DockStyle.Fill;

            this.Controls.Add(this.zed);
            this.Controls.Add(this.panel1);

            this.Text = "ОДУ: Эйлер и РК2 (ZedGraph)";
            this.Width = 1000;
            this.Height = 650;

            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
