namespace CorrectorApp
{
    partial class UserControl1
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl1));
            this.txtOriginal = new System.Windows.Forms.TextBox();
            this.txtCorregido = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCorregir = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtOriginal
            // 
            this.txtOriginal.BackColor = System.Drawing.SystemColors.Control;
            this.txtOriginal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOriginal.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOriginal.Location = new System.Drawing.Point(18, 43);
            this.txtOriginal.Multiline = true;
            this.txtOriginal.Name = "txtOriginal";
            this.txtOriginal.Size = new System.Drawing.Size(590, 90);
            this.txtOriginal.TabIndex = 0;
            // 
            // txtCorregido
            // 
            this.txtCorregido.BackColor = System.Drawing.SystemColors.Control;
            this.txtCorregido.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCorregido.Enabled = false;
            this.txtCorregido.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCorregido.Location = new System.Drawing.Point(18, 182);
            this.txtCorregido.Multiline = true;
            this.txtCorregido.Name = "txtCorregido";
            this.txtCorregido.Size = new System.Drawing.Size(590, 90);
            this.txtCorregido.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Oración original";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Oración corregida";
            // 
            // btnCorregir
            // 
            this.btnCorregir.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCorregir.FlatAppearance.BorderSize = 0;
            this.btnCorregir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCorregir.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCorregir.ForeColor = System.Drawing.Color.White;
            this.btnCorregir.Location = new System.Drawing.Point(466, 385);
            this.btnCorregir.Name = "btnCorregir";
            this.btnCorregir.Size = new System.Drawing.Size(142, 50);
            this.btnCorregir.TabIndex = 4;
            this.btnCorregir.Text = "Corregir";
            this.btnCorregir.UseVisualStyleBackColor = false;
            this.btnCorregir.Click += new System.EventHandler(this.btnCorregir_Click);
            this.btnCorregir.Paint += new System.Windows.Forms.PaintEventHandler(this.btnCorregir_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-50, 351);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(321, 146);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnCorregir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCorregido);
            this.Controls.Add(this.txtOriginal);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(633, 451);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOriginal;
        private System.Windows.Forms.TextBox txtCorregido;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCorregir;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
