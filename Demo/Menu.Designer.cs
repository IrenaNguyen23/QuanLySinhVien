namespace Demo
{
    partial class Menu
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
            this.btn01 = new System.Windows.Forms.Button();
            this.btn02 = new System.Windows.Forms.Button();
            this.btn03 = new System.Windows.Forms.Button();
            this.btn04 = new System.Windows.Forms.Button();
            this.btn05 = new System.Windows.Forms.Button();
            this.btn06 = new System.Windows.Forms.Button();
            this.btn07 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn01
            // 
            this.btn01.Location = new System.Drawing.Point(82, 61);
            this.btn01.Name = "btn01";
            this.btn01.Size = new System.Drawing.Size(75, 23);
            this.btn01.TabIndex = 0;
            this.btn01.Text = "Bai01";
            this.btn01.UseVisualStyleBackColor = true;
            this.btn01.Click += new System.EventHandler(this.btn01_Click);
            // 
            // btn02
            // 
            this.btn02.Location = new System.Drawing.Point(230, 61);
            this.btn02.Name = "btn02";
            this.btn02.Size = new System.Drawing.Size(75, 23);
            this.btn02.TabIndex = 1;
            this.btn02.Text = "Bai02";
            this.btn02.UseVisualStyleBackColor = true;
            this.btn02.Click += new System.EventHandler(this.btn02_Click);
            // 
            // btn03
            // 
            this.btn03.Location = new System.Drawing.Point(404, 61);
            this.btn03.Name = "btn03";
            this.btn03.Size = new System.Drawing.Size(75, 23);
            this.btn03.TabIndex = 2;
            this.btn03.Text = "Bai03";
            this.btn03.UseVisualStyleBackColor = true;
            this.btn03.Click += new System.EventHandler(this.btn03_Click);
            // 
            // btn04
            // 
            this.btn04.Location = new System.Drawing.Point(566, 61);
            this.btn04.Name = "btn04";
            this.btn04.Size = new System.Drawing.Size(75, 23);
            this.btn04.TabIndex = 3;
            this.btn04.Text = "Bai04";
            this.btn04.UseVisualStyleBackColor = true;
            this.btn04.Click += new System.EventHandler(this.btn04_Click);
            // 
            // btn05
            // 
            this.btn05.Location = new System.Drawing.Point(81, 144);
            this.btn05.Name = "btn05";
            this.btn05.Size = new System.Drawing.Size(75, 23);
            this.btn05.TabIndex = 4;
            this.btn05.Text = "Bai05";
            this.btn05.UseVisualStyleBackColor = true;
            this.btn05.Click += new System.EventHandler(this.btn05_Click);
            // 
            // btn06
            // 
            this.btn06.Location = new System.Drawing.Point(230, 143);
            this.btn06.Name = "btn06";
            this.btn06.Size = new System.Drawing.Size(75, 23);
            this.btn06.TabIndex = 5;
            this.btn06.Text = "Bai06";
            this.btn06.Click += new System.EventHandler(this.btn06_Click);
            // 
            // btn07
            // 
            this.btn07.Location = new System.Drawing.Point(404, 143);
            this.btn07.Name = "btn07";
            this.btn07.Size = new System.Drawing.Size(75, 23);
            this.btn07.TabIndex = 6;
            this.btn07.Text = "Bai07";
            this.btn07.UseVisualStyleBackColor = true;
            this.btn07.Click += new System.EventHandler(this.btn07_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn07);
            this.Controls.Add(this.btn06);
            this.Controls.Add(this.btn05);
            this.Controls.Add(this.btn04);
            this.Controls.Add(this.btn03);
            this.Controls.Add(this.btn02);
            this.Controls.Add(this.btn01);
            this.Name = "Menu";
            this.Text = "Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn01;
        private System.Windows.Forms.Button btn02;
        private System.Windows.Forms.Button btn03;
        private System.Windows.Forms.Button btn04;
        private System.Windows.Forms.Button btn05;
        private System.Windows.Forms.Button btn06;
        private System.Windows.Forms.Button btn07;
    }
}