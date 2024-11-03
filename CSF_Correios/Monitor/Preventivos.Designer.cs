namespace Monitor
{
    partial class Preventivos
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
            this.tbFile = new System.Windows.Forms.TextBox();
            this.btSelecionar = new System.Windows.Forms.Button();
            this.btSolicitar = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // tbFile
            // 
            this.tbFile.Location = new System.Drawing.Point(12, 24);
            this.tbFile.Name = "tbFile";
            this.tbFile.Size = new System.Drawing.Size(341, 20);
            this.tbFile.TabIndex = 0;
            // 
            // btSelecionar
            // 
            this.btSelecionar.Location = new System.Drawing.Point(362, 24);
            this.btSelecionar.Name = "btSelecionar";
            this.btSelecionar.Size = new System.Drawing.Size(75, 23);
            this.btSelecionar.TabIndex = 1;
            this.btSelecionar.Text = "Selecionar";
            this.btSelecionar.UseVisualStyleBackColor = true;
            this.btSelecionar.Click += new System.EventHandler(this.btSelecionar_Click);
            // 
            // btSolicitar
            // 
            this.btSolicitar.Location = new System.Drawing.Point(362, 62);
            this.btSolicitar.Name = "btSolicitar";
            this.btSolicitar.Size = new System.Drawing.Size(75, 23);
            this.btSolicitar.TabIndex = 2;
            this.btSolicitar.Text = "Solicitar";
            this.btSolicitar.UseVisualStyleBackColor = true;
            this.btSolicitar.Click += new System.EventHandler(this.btSolicitar_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 62);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(341, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // Preventivos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 108);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btSolicitar);
            this.Controls.Add(this.btSelecionar);
            this.Controls.Add(this.tbFile);
            this.Name = "Preventivos";
            this.Text = "Preventivos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFile;
        private System.Windows.Forms.Button btSelecionar;
        private System.Windows.Forms.Button btSolicitar;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}