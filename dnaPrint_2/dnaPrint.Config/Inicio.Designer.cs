namespace dnaPrint.Config
{
    partial class Inicio
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inicio));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.tbSenha = new System.Windows.Forms.TextBox();
            this.btEntrar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lbMessage = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbTotalMes = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lbTotal = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbDuracaoCilindro = new System.Windows.Forms.Label();
            this.lbDuracaoToner = new System.Windows.Forms.Label();
            this.lbSerialCilindro = new System.Windows.Forms.Label();
            this.lbSerialToner = new System.Windows.Forms.Label();
            this.lbErro = new System.Windows.Forms.Label();
            this.lbCilindro = new System.Windows.Forms.Label();
            this.lbToner = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.atualizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enviarDadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postagensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuário:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Senha:";
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(130, 81);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(208, 20);
            this.tbUser.TabIndex = 2;
            // 
            // tbSenha
            // 
            this.tbSenha.Location = new System.Drawing.Point(130, 129);
            this.tbSenha.Name = "tbSenha";
            this.tbSenha.Size = new System.Drawing.Size(208, 20);
            this.tbSenha.TabIndex = 3;
            this.tbSenha.UseSystemPasswordChar = true;
            // 
            // btEntrar
            // 
            this.btEntrar.Location = new System.Drawing.Point(81, 181);
            this.btEntrar.Name = "btEntrar";
            this.btEntrar.Size = new System.Drawing.Size(75, 23);
            this.btEntrar.TabIndex = 4;
            this.btEntrar.Text = "Entrar";
            this.btEntrar.UseVisualStyleBackColor = true;
            this.btEntrar.Click += new System.EventHandler(this.btEntrar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(97, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(228, 26);
            this.label3.TabIndex = 5;
            this.label3.Text = "Configuração dnaPrint";
            // 
            // lbMessage
            // 
            this.lbMessage.AutoSize = true;
            this.lbMessage.ForeColor = System.Drawing.Color.Red;
            this.lbMessage.Location = new System.Drawing.Point(84, 155);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(15, 13);
            this.lbMessage.TabIndex = 6;
            this.lbMessage.Text = "**";
            this.lbMessage.Visible = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(438, 335);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.menuStrip1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(430, 309);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Informações";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbTotalMes);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.lbTotal);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 205);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(424, 101);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Contadores";
            this.groupBox2.Visible = false;
            // 
            // lbTotalMes
            // 
            this.lbTotalMes.AutoSize = true;
            this.lbTotalMes.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalMes.Location = new System.Drawing.Point(258, 52);
            this.lbTotalMes.Name = "lbTotalMes";
            this.lbTotalMes.Size = new System.Drawing.Size(108, 46);
            this.lbTotalMes.TabIndex = 6;
            this.lbTotalMes.Text = "1000";
            this.lbTotalMes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(205, 31);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(93, 13);
            this.label15.TabIndex = 5;
            this.label15.Text = "Produção do mês:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 31);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(34, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "Total:";
            // 
            // lbTotal
            // 
            this.lbTotal.AutoSize = true;
            this.lbTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotal.Location = new System.Drawing.Point(49, 52);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(108, 46);
            this.lbTotal.TabIndex = 3;
            this.lbTotal.Text = "1000";
            this.lbTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.lbDuracaoCilindro);
            this.groupBox1.Controls.Add(this.lbDuracaoToner);
            this.groupBox1.Controls.Add(this.lbSerialCilindro);
            this.groupBox1.Controls.Add(this.lbSerialToner);
            this.groupBox1.Controls.Add(this.lbErro);
            this.groupBox1.Controls.Add(this.lbCilindro);
            this.groupBox1.Controls.Add(this.lbToner);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(424, 178);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Suprimentos";
            this.groupBox1.Visible = false;
            // 
            // lbDuracaoCilindro
            // 
            this.lbDuracaoCilindro.AutoSize = true;
            this.lbDuracaoCilindro.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDuracaoCilindro.Location = new System.Drawing.Point(271, 100);
            this.lbDuracaoCilindro.Name = "lbDuracaoCilindro";
            this.lbDuracaoCilindro.Size = new System.Drawing.Size(69, 24);
            this.lbDuracaoCilindro.TabIndex = 8;
            this.lbDuracaoCilindro.Text = "10 dias";
            this.lbDuracaoCilindro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDuracaoToner
            // 
            this.lbDuracaoToner.AutoSize = true;
            this.lbDuracaoToner.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDuracaoToner.Location = new System.Drawing.Point(62, 100);
            this.lbDuracaoToner.Name = "lbDuracaoToner";
            this.lbDuracaoToner.Size = new System.Drawing.Size(69, 24);
            this.lbDuracaoToner.TabIndex = 7;
            this.lbDuracaoToner.Text = "10 dias";
            this.lbDuracaoToner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbSerialCilindro
            // 
            this.lbSerialCilindro.AutoSize = true;
            this.lbSerialCilindro.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSerialCilindro.Location = new System.Drawing.Point(222, 138);
            this.lbSerialCilindro.Name = "lbSerialCilindro";
            this.lbSerialCilindro.Size = new System.Drawing.Size(181, 24);
            this.lbSerialCilindro.TabIndex = 6;
            this.lbSerialCilindro.Text = "CRUM-16090278853";
            // 
            // lbSerialToner
            // 
            this.lbSerialToner.AutoSize = true;
            this.lbSerialToner.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSerialToner.Location = new System.Drawing.Point(21, 138);
            this.lbSerialToner.Name = "lbSerialToner";
            this.lbSerialToner.Size = new System.Drawing.Size(181, 24);
            this.lbSerialToner.TabIndex = 5;
            this.lbSerialToner.Text = "CRUM-16090278853";
            // 
            // lbErro
            // 
            this.lbErro.AutoSize = true;
            this.lbErro.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbErro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbErro.Location = new System.Drawing.Point(6, 66);
            this.lbErro.Name = "lbErro";
            this.lbErro.Size = new System.Drawing.Size(46, 17);
            this.lbErro.TabIndex = 4;
            this.lbErro.Text = "label6";
            this.lbErro.Visible = false;
            // 
            // lbCilindro
            // 
            this.lbCilindro.AutoSize = true;
            this.lbCilindro.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCilindro.Location = new System.Drawing.Point(267, 54);
            this.lbCilindro.Name = "lbCilindro";
            this.lbCilindro.Size = new System.Drawing.Size(99, 46);
            this.lbCilindro.TabIndex = 3;
            this.lbCilindro.Text = "90%";
            // 
            // lbToner
            // 
            this.lbToner.AutoSize = true;
            this.lbToner.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbToner.Location = new System.Drawing.Point(58, 54);
            this.lbToner.Name = "lbToner";
            this.lbToner.Size = new System.Drawing.Size(99, 46);
            this.lbToner.TabIndex = 2;
            this.lbToner.Text = "90%";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(205, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Cilindro:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Toner:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.atualizarToolStripMenuItem,
            this.enviarDadosToolStripMenuItem,
            this.postagensToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(3, 3);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(424, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // atualizarToolStripMenuItem
            // 
            this.atualizarToolStripMenuItem.BackColor = System.Drawing.Color.Green;
            this.atualizarToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.atualizarToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.atualizarToolStripMenuItem.Name = "atualizarToolStripMenuItem";
            this.atualizarToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.atualizarToolStripMenuItem.Text = "Atualizar";
            this.atualizarToolStripMenuItem.Click += new System.EventHandler(this.atualizarToolStripMenuItem_Click);
            // 
            // enviarDadosToolStripMenuItem
            // 
            this.enviarDadosToolStripMenuItem.Name = "enviarDadosToolStripMenuItem";
            this.enviarDadosToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.enviarDadosToolStripMenuItem.Text = "Enviar dados";
            this.enviarDadosToolStripMenuItem.Click += new System.EventHandler(this.enviarDadosToolStripMenuItem_Click);
            // 
            // postagensToolStripMenuItem
            // 
            this.postagensToolStripMenuItem.Name = "postagensToolStripMenuItem";
            this.postagensToolStripMenuItem.Size = new System.Drawing.Size(120, 20);
            this.postagensToolStripMenuItem.Text = "Histórico de Envios";
            this.postagensToolStripMenuItem.Click += new System.EventHandler(this.postagensToolStripMenuItem_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.lbMessage);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.btEntrar);
            this.tabPage2.Controls.Add(this.tbUser);
            this.tabPage2.Controls.Add(this.tbSenha);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(430, 309);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Administrador";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(430, 309);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Contatos";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(28, 182);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(365, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Categoria: Equipamentos de TI.Impressoras Corporativas - Preto e Branco.%";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(28, 159);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(201, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Grupo: Serviço de Impressão Corporativa";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(8, 135);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(271, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Abertura de Requisições (Demandas Internas):";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(28, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(142, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Email: sac@csfdigital.com.br";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(28, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(191, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Telefones: (85) 3299-5516 | 3299-5517";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Ramais: *5516 | *5517";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Suporte:";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "dnaPrint";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 335);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Inicio";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "dnaPrint 2.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Inicio_FormClosing);
            this.Move += new System.EventHandler(this.Inicio_Move);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.TextBox tbSenha;
        private System.Windows.Forms.Button btEntrar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbMessage;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbCilindro;
        private System.Windows.Forms.Label lbToner;
        private System.Windows.Forms.Label lbErro;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem atualizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enviarDadosToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbSerialCilindro;
        private System.Windows.Forms.Label lbSerialToner;
        private System.Windows.Forms.Label lbTotalMes;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbTotal;
        private System.Windows.Forms.Label lbDuracaoCilindro;
        private System.Windows.Forms.Label lbDuracaoToner;
        private System.Windows.Forms.ToolStripMenuItem postagensToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}