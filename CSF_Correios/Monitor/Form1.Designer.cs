namespace Monitor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.cbServico = new System.Windows.Forms.ComboBox();
            this.tbcepOrigem = new System.Windows.Forms.TextBox();
            this.tbcepDestino = new System.Windows.Forms.TextBox();
            this.btPrazo = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.btCeps = new System.Windows.Forms.Button();
            this.btReversos = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(260, 20);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Rastrear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 67);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(260, 57);
            this.textBox2.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 170);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Verificar Peças";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(147, 170);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(125, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Verificar Suprimentos";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 130);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(260, 29);
            this.progressBar1.TabIndex = 5;
            // 
            // cbServico
            // 
            this.cbServico.FormattingEnabled = true;
            this.cbServico.Items.AddRange(new object[] {
            "SEDEX",
            "PAC"});
            this.cbServico.Location = new System.Drawing.Point(12, 253);
            this.cbServico.Name = "cbServico";
            this.cbServico.Size = new System.Drawing.Size(125, 21);
            this.cbServico.TabIndex = 6;
            // 
            // tbcepOrigem
            // 
            this.tbcepOrigem.Location = new System.Drawing.Point(12, 227);
            this.tbcepOrigem.Name = "tbcepOrigem";
            this.tbcepOrigem.Size = new System.Drawing.Size(125, 20);
            this.tbcepOrigem.TabIndex = 7;
            // 
            // tbcepDestino
            // 
            this.tbcepDestino.Location = new System.Drawing.Point(147, 227);
            this.tbcepDestino.Name = "tbcepDestino";
            this.tbcepDestino.Size = new System.Drawing.Size(125, 20);
            this.tbcepDestino.TabIndex = 8;
            // 
            // btPrazo
            // 
            this.btPrazo.Location = new System.Drawing.Point(147, 253);
            this.btPrazo.Name = "btPrazo";
            this.btPrazo.Size = new System.Drawing.Size(125, 23);
            this.btPrazo.TabIndex = 9;
            this.btPrazo.Text = "Prazo";
            this.btPrazo.UseVisualStyleBackColor = true;
            this.btPrazo.Click += new System.EventHandler(this.btPrazo_Click);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(12, 280);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(260, 48);
            this.textBox5.TabIndex = 10;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(82, 38);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(109, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "Confirmar Entrega";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btCeps
            // 
            this.btCeps.Location = new System.Drawing.Point(197, 38);
            this.btCeps.Name = "btCeps";
            this.btCeps.Size = new System.Drawing.Size(75, 23);
            this.btCeps.TabIndex = 12;
            this.btCeps.Text = "Ceps";
            this.btCeps.UseVisualStyleBackColor = true;
            this.btCeps.Click += new System.EventHandler(this.btCeps_Click);
            // 
            // btReversos
            // 
            this.btReversos.Location = new System.Drawing.Point(12, 199);
            this.btReversos.Name = "btReversos";
            this.btReversos.Size = new System.Drawing.Size(125, 23);
            this.btReversos.TabIndex = 13;
            this.btReversos.Text = "Verificar Reversos";
            this.btReversos.UseVisualStyleBackColor = true;
            this.btReversos.Click += new System.EventHandler(this.btReversos_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 340);
            this.Controls.Add(this.btReversos);
            this.Controls.Add(this.btCeps);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.btPrazo);
            this.Controls.Add(this.tbcepDestino);
            this.Controls.Add(this.tbcepOrigem);
            this.Controls.Add(this.cbServico);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ComboBox cbServico;
        private System.Windows.Forms.TextBox tbcepOrigem;
        private System.Windows.Forms.TextBox tbcepDestino;
        private System.Windows.Forms.Button btPrazo;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btCeps;
        private System.Windows.Forms.Button btReversos;
    }
}

