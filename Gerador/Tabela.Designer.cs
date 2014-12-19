namespace Gerador
{
    partial class Tabela
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tabela));
            this.tabelaGridView = new System.Windows.Forms.DataGridView();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnEraseTable = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tabelaGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tabelaGridView
            // 
            this.tabelaGridView.AllowUserToAddRows = false;
            this.tabelaGridView.AllowUserToDeleteRows = false;
            this.tabelaGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.tabelaGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tabelaGridView.GridColor = System.Drawing.SystemColors.Control;
            this.tabelaGridView.Location = new System.Drawing.Point(12, 12);
            this.tabelaGridView.Name = "tabelaGridView";
            this.tabelaGridView.ReadOnly = true;
            this.tabelaGridView.Size = new System.Drawing.Size(943, 246);
            this.tabelaGridView.TabIndex = 0;
            // 
            // btnExcluir
            // 
            this.btnExcluir.Image = ((System.Drawing.Image)(resources.GetObject("btnExcluir.Image")));
            this.btnExcluir.Location = new System.Drawing.Point(12, 264);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(75, 47);
            this.btnExcluir.TabIndex = 1;
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnEraseTable
            // 
            this.btnEraseTable.Image = ((System.Drawing.Image)(resources.GetObject("btnEraseTable.Image")));
            this.btnEraseTable.Location = new System.Drawing.Point(93, 265);
            this.btnEraseTable.Name = "btnEraseTable";
            this.btnEraseTable.Size = new System.Drawing.Size(75, 46);
            this.btnEraseTable.TabIndex = 2;
            this.btnEraseTable.UseVisualStyleBackColor = true;
            this.btnEraseTable.Click += new System.EventHandler(this.button1_Click);
            // 
            // Tabela
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 313);
            this.Controls.Add(this.btnEraseTable);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.tabelaGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Tabela";
            this.Text = "Tabela";
            this.Load += new System.EventHandler(this.Tabela_Load);
            this.Resize += new System.EventHandler(this.Tabela_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.tabelaGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tabelaGridView;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnEraseTable;
    }
}