namespace WinFormsApp2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private ZedGraph.ZedGraphControl zedGraphControl;
        private System.Windows.Forms.ComboBox comboBoxFunction;
        private System.Windows.Forms.ComboBox comboBoxMethod;
        private System.Windows.Forms.NumericUpDown numericUpDownNodes;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBoxParameters;
        private System.Windows.Forms.NumericUpDown numericUpDownK;
        private System.Windows.Forms.Label label4;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.zedGraphControl = new ZedGraph.ZedGraphControl();
            this.comboBoxFunction = new System.Windows.Forms.ComboBox();
            this.comboBoxMethod = new System.Windows.Forms.ComboBox();
            this.numericUpDownNodes = new System.Windows.Forms.NumericUpDown();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxParameters = new System.Windows.Forms.GroupBox();
            this.numericUpDownK = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNodes)).BeginInit();
            this.groupBoxParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownK)).BeginInit();
            this.SuspendLayout();

            // zedGraphControl
            this.zedGraphControl.Location = new System.Drawing.Point(12, 12);
            this.zedGraphControl.Name = "zedGraphControl";
            this.zedGraphControl.ScrollGrace = 0D;
            this.zedGraphControl.ScrollMaxX = 0D;
            this.zedGraphControl.ScrollMaxY = 0D;
            this.zedGraphControl.ScrollMaxY2 = 0D;
            this.zedGraphControl.ScrollMinX = 0D;
            this.zedGraphControl.ScrollMinY = 0D;
            this.zedGraphControl.ScrollMinY2 = 0D;
            this.zedGraphControl.Size = new System.Drawing.Size(600, 400);
            this.zedGraphControl.TabIndex = 0;

            // comboBoxFunction
            this.comboBoxFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFunction.FormattingEnabled = true;
            this.comboBoxFunction.Items.AddRange(new object[] {
            "f(x) = x² (малая производная)",
            "f(x) = e^(-kx) (большая производная)",
            "f(x) = 1/(1+25x²) (большая производная)",
            "f(x) = |x|"});
            this.comboBoxFunction.Location = new System.Drawing.Point(120, 25);
            this.comboBoxFunction.Name = "comboBoxFunction";
            this.comboBoxFunction.Size = new System.Drawing.Size(200, 21);
            this.comboBoxFunction.TabIndex = 1;
            this.comboBoxFunction.SelectedIndexChanged += new System.EventHandler(this.comboBoxFunction_SelectedIndexChanged);

            // comboBoxMethod
            this.comboBoxMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMethod.FormattingEnabled = true;
            this.comboBoxMethod.Items.AddRange(new object[] {
            "Полином Лагранжа (равноотстоящие узлы)",
            "Полином Лагранжа (чебышевские узлы)",
            "Кубический сплайн"});
            this.comboBoxMethod.Location = new System.Drawing.Point(120, 55);
            this.comboBoxMethod.Name = "comboBoxMethod";
            this.comboBoxMethod.Size = new System.Drawing.Size(200, 21);
            this.comboBoxMethod.TabIndex = 2;

            // numericUpDownNodes
            this.numericUpDownNodes.Location = new System.Drawing.Point(120, 85);
            this.numericUpDownNodes.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownNodes.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownNodes.Name = "numericUpDownNodes";
            this.numericUpDownNodes.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownNodes.TabIndex = 3;
            this.numericUpDownNodes.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});

            // buttonCalculate
            this.buttonCalculate.Location = new System.Drawing.Point(200, 115);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(120, 30);
            this.buttonCalculate.TabIndex = 4;
            this.buttonCalculate.Text = "Построить графики";
            this.buttonCalculate.UseVisualStyleBackColor = true;
            this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Функция:";

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Метод:";

            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Число узлов:";

            // groupBoxParameters
            this.groupBoxParameters.Controls.Add(this.numericUpDownK);
            this.groupBoxParameters.Controls.Add(this.label4);
            this.groupBoxParameters.Controls.Add(this.label1);
            this.groupBoxParameters.Controls.Add(this.label3);
            this.groupBoxParameters.Controls.Add(this.comboBoxFunction);
            this.groupBoxParameters.Controls.Add(this.label2);
            this.groupBoxParameters.Controls.Add(this.comboBoxMethod);
            this.groupBoxParameters.Controls.Add(this.buttonCalculate);
            this.groupBoxParameters.Controls.Add(this.numericUpDownNodes);
            this.groupBoxParameters.Location = new System.Drawing.Point(620, 12);
            this.groupBoxParameters.Name = "groupBoxParameters";
            this.groupBoxParameters.Size = new System.Drawing.Size(350, 160);
            this.groupBoxParameters.TabIndex = 8;
            this.groupBoxParameters.TabStop = false;
            this.groupBoxParameters.Text = "Параметры";

            // numericUpDownK
            this.numericUpDownK.Location = new System.Drawing.Point(280, 85);
            this.numericUpDownK.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownK.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownK.Name = "numericUpDownK";
            this.numericUpDownK.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownK.TabIndex = 9;
            this.numericUpDownK.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownK.Visible = false;

            // label4
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(190, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Параметр k:";
            this.label4.Visible = false;

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.groupBoxParameters);
            this.Controls.Add(this.zedGraphControl);
            this.Name = "Form1";
            this.Text = "Приближение функций";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNodes)).EndInit();
            this.groupBoxParameters.ResumeLayout(false);
            this.groupBoxParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownK)).EndInit();
            this.ResumeLayout(false);
        }
    }
}