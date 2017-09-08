namespace CS4750HW2
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
            this.btnIDS = new System.Windows.Forms.Button();
            this.btnDFS = new System.Windows.Forms.Button();
            this.btnAStar = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.rtxtResults = new System.Windows.Forms.RichTextBox();
            this.lblResults = new System.Windows.Forms.Label();
            this.btnClearResults = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnIDS
            // 
            this.btnIDS.Location = new System.Drawing.Point(12, 353);
            this.btnIDS.Name = "btnIDS";
            this.btnIDS.Size = new System.Drawing.Size(75, 23);
            this.btnIDS.TabIndex = 0;
            this.btnIDS.Text = "IDS";
            this.btnIDS.UseVisualStyleBackColor = true;
            this.btnIDS.Click += new System.EventHandler(this.btnIDS_Click);
            // 
            // btnDFS
            // 
            this.btnDFS.Location = new System.Drawing.Point(93, 353);
            this.btnDFS.Name = "btnDFS";
            this.btnDFS.Size = new System.Drawing.Size(75, 23);
            this.btnDFS.TabIndex = 1;
            this.btnDFS.Text = "DFS";
            this.btnDFS.UseVisualStyleBackColor = true;
            this.btnDFS.Click += new System.EventHandler(this.btnDFS_Click);
            // 
            // btnAStar
            // 
            this.btnAStar.Location = new System.Drawing.Point(174, 353);
            this.btnAStar.Name = "btnAStar";
            this.btnAStar.Size = new System.Drawing.Size(75, 23);
            this.btnAStar.TabIndex = 2;
            this.btnAStar.Text = "A*";
            this.btnAStar.UseVisualStyleBackColor = true;
            this.btnAStar.Click += new System.EventHandler(this.btnAStar_Click);
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(255, 353);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(75, 23);
            this.btnAll.TabIndex = 3;
            this.btnAll.Text = "Run All";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // rtxtResults
            // 
            this.rtxtResults.Location = new System.Drawing.Point(12, 25);
            this.rtxtResults.Name = "rtxtResults";
            this.rtxtResults.Size = new System.Drawing.Size(399, 322);
            this.rtxtResults.TabIndex = 4;
            this.rtxtResults.Text = "";
            // 
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.Location = new System.Drawing.Point(12, 9);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(42, 13);
            this.lblResults.TabIndex = 5;
            this.lblResults.Text = "Results";
            // 
            // btnClearResults
            // 
            this.btnClearResults.Location = new System.Drawing.Point(336, 353);
            this.btnClearResults.Name = "btnClearResults";
            this.btnClearResults.Size = new System.Drawing.Size(75, 23);
            this.btnClearResults.TabIndex = 6;
            this.btnClearResults.Text = "Clear";
            this.btnClearResults.UseVisualStyleBackColor = true;
            this.btnClearResults.Click += new System.EventHandler(this.btnClearResults_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 388);
            this.Controls.Add(this.btnClearResults);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.rtxtResults);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.btnAStar);
            this.Controls.Add(this.btnDFS);
            this.Controls.Add(this.btnIDS);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIDS;
        private System.Windows.Forms.Button btnDFS;
        private System.Windows.Forms.Button btnAStar;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.RichTextBox rtxtResults;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.Button btnClearResults;
    }
}

