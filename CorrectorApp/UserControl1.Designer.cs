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
            this.txtOriginal = new System.Windows.Forms.TextBox();
            this.txtCorregido = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCorregir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtOriginal
            // 
            this.txtOriginal.BackColor = System.Drawing.SystemColors.Control;
            this.txtOriginal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOriginal.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.txtCorregido.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCorregido.Location = new System.Drawing.Point(18, 182);
            this.txtCorregido.Multiline = true;
            this.txtCorregido.Name = "txtCorregido";
            this.txtCorregido.Size = new System.Drawing.Size(590, 90);
            this.txtCorregido.TabIndex = 1;
            this.txtCorregido.Enabled = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Oración original";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Oración corregida";
            // 
            // btnCorregir
            // 
            this.btnCorregir.BackColor = System.Drawing.Color.SteelBlue;
            this.btnCorregir.FlatAppearance.BorderSize = 0;
            this.btnCorregir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCorregir.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCorregir.ForeColor = System.Drawing.Color.White;
            this.btnCorregir.Location = new System.Drawing.Point(445, 370);
            this.btnCorregir.Name = "btnCorregir";
            this.btnCorregir.Size = new System.Drawing.Size(142, 50);
            this.btnCorregir.TabIndex = 4;
            this.btnCorregir.Text = "Corregir";
            this.btnCorregir.UseVisualStyleBackColor = false;
            this.btnCorregir.Click += new System.EventHandler(this.btnCorregir_Click);
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnCorregir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCorregido);
            this.Controls.Add(this.txtOriginal);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(633, 451);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOriginal;
        private System.Windows.Forms.TextBox txtCorregido;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCorregir;
    }
}
