namespace WinFormsApp1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cbFunction;
        private System.Windows.Forms.TextBox txtA;
        private System.Windows.Forms.TextBox txtB;
        private System.Windows.Forms.TextBox txtEps;
        private System.Windows.Forms.ComboBox cbMethod;
        private System.Windows.Forms.Button btnCompute;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.NumericUpDown nudN;
        private System.Windows.Forms.Label lblFunction;
        private System.Windows.Forms.Label lblA;
        private System.Windows.Forms.Label lblB;
        private System.Windows.Forms.Label lblEps;
        private System.Windows.Forms.Label lblMethod;
        private System.Windows.Forms.Label lblN;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            cbFunction = new ComboBox();
            txtA = new TextBox();
            txtB = new TextBox();
            txtEps = new TextBox();
            cbMethod = new ComboBox();
            btnCompute = new Button();
            lblResult = new Label();
            nudN = new NumericUpDown();
            lblFunction = new Label();
            lblA = new Label();
            lblB = new Label();
            lblEps = new Label();
            lblMethod = new Label();
            lblN = new Label();
            ((System.ComponentModel.ISupportInitialize)nudN).BeginInit();
            SuspendLayout();
            // 
            // cbFunction
            // 
            cbFunction.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFunction.Items.AddRange(new object[] { "sin(x)", "cos(x)", "x^2", "exp(-x^2)", "ln(1+x)", "sqrt(x)" });
            cbFunction.Location = new Point(120, 20);
            cbFunction.Name = "cbFunction";
            cbFunction.Size = new Size(200, 23);
            cbFunction.TabIndex = 1;
            // 
            // txtA
            // 
            txtA.Location = new Point(120, 60);
            txtA.Name = "txtA";
            txtA.Size = new Size(100, 23);
            txtA.TabIndex = 3;
            txtA.Text = "0";
            // 
            // txtB
            // 
            txtB.Location = new Point(120, 90);
            txtB.Name = "txtB";
            txtB.Size = new Size(100, 23);
            txtB.TabIndex = 5;
            txtB.Text = "3.14159265";
            // 
            // txtEps
            // 
            txtEps.Location = new Point(120, 150);
            txtEps.Name = "txtEps";
            txtEps.Size = new Size(100, 23);
            txtEps.TabIndex = 9;
            txtEps.Text = "1e-6";
            // 
            // cbMethod
            // 
            cbMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMethod.Items.AddRange(new object[] { "левый", "трапеций" });
            cbMethod.Location = new Point(120, 180);
            cbMethod.Name = "cbMethod";
            cbMethod.Size = new Size(200, 23);
            cbMethod.TabIndex = 11;
            // 
            // btnCompute
            // 
            btnCompute.Location = new Point(120, 220);
            btnCompute.Name = "btnCompute";
            btnCompute.Size = new Size(82, 23);
            btnCompute.TabIndex = 12;
            btnCompute.Text = "Вычислить";
            btnCompute.Click += btnCompute_Click;
            // 
            // lblResult
            // 
            lblResult.Location = new Point(20, 260);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(400, 120);
            lblResult.TabIndex = 13;
            lblResult.Text = "Результат:";
            // 
            // nudN
            // 
            nudN.Location = new Point(120, 120);
            nudN.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudN.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudN.Name = "nudN";
            nudN.Size = new Size(120, 23);
            nudN.TabIndex = 7;
            nudN.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // lblFunction
            // 
            lblFunction.Location = new Point(20, 20);
            lblFunction.Name = "lblFunction";
            lblFunction.Size = new Size(100, 23);
            lblFunction.TabIndex = 0;
            lblFunction.Text = "Функция:";
            // 
            // lblA
            // 
            lblA.Location = new Point(20, 60);
            lblA.Name = "lblA";
            lblA.Size = new Size(100, 23);
            lblA.TabIndex = 2;
            lblA.Text = "Левая граница a:";
            // 
            // lblB
            // 
            lblB.Location = new Point(20, 90);
            lblB.Name = "lblB";
            lblB.Size = new Size(100, 23);
            lblB.TabIndex = 4;
            lblB.Text = "Правая граница b:";
            // 
            // lblEps
            // 
            lblEps.Location = new Point(20, 150);
            lblEps.Name = "lblEps";
            lblEps.Size = new Size(100, 23);
            lblEps.TabIndex = 8;
            lblEps.Text = "Точность eps:";
            // 
            // lblMethod
            // 
            lblMethod.Location = new Point(20, 180);
            lblMethod.Name = "lblMethod";
            lblMethod.Size = new Size(100, 23);
            lblMethod.TabIndex = 10;
            lblMethod.Text = "Метод:";
            // 
            // lblN
            // 
            lblN.Location = new Point(20, 120);
            lblN.Name = "lblN";
            lblN.Size = new Size(100, 23);
            lblN.TabIndex = 6;
            lblN.Text = "Разбиений n:";
            // 
            // Form1
            // 
            ClientSize = new Size(460, 400);
            Controls.Add(lblFunction);
            Controls.Add(cbFunction);
            Controls.Add(lblA);
            Controls.Add(txtA);
            Controls.Add(lblB);
            Controls.Add(txtB);
            Controls.Add(lblN);
            Controls.Add(nudN);
            Controls.Add(lblEps);
            Controls.Add(txtEps);
            Controls.Add(lblMethod);
            Controls.Add(cbMethod);
            Controls.Add(btnCompute);
            Controls.Add(lblResult);
            Name = "Form1";
            Text = "Численное интегрирование";
            ((System.ComponentModel.ISupportInitialize)nudN).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
