namespace TestApp
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGetElement = new System.Windows.Forms.Button();
            this.txtElement = new System.Windows.Forms.TextBox();
            this.sharpBrowser1 = new uSharpBrowser.SharpBrowser();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.sharpBrowser1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(680, 313);
            this.panel1.TabIndex = 0;
            // 
            // btnGetElement
            // 
            this.btnGetElement.Location = new System.Drawing.Point(12, 357);
            this.btnGetElement.Name = "btnGetElement";
            this.btnGetElement.Size = new System.Drawing.Size(148, 36);
            this.btnGetElement.TabIndex = 1;
            this.btnGetElement.Text = "Get Element";
            this.btnGetElement.UseVisualStyleBackColor = true;
            this.btnGetElement.Click += new System.EventHandler(this.btnGetElement_Click);
            // 
            // txtElement
            // 
            this.txtElement.Location = new System.Drawing.Point(12, 331);
            this.txtElement.Name = "txtElement";
            this.txtElement.Size = new System.Drawing.Size(148, 20);
            this.txtElement.TabIndex = 2;
            // 
            // sharpBrowser1
            // 
            this.sharpBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sharpBrowser1.Location = new System.Drawing.Point(0, 0);
            this.sharpBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.sharpBrowser1.Name = "sharpBrowser1";
            this.sharpBrowser1.Size = new System.Drawing.Size(680, 313);
            this.sharpBrowser1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 400);
            this.Controls.Add(this.txtElement);
            this.Controls.Add(this.btnGetElement);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGetElement;
        private uSharpBrowser.SharpBrowser sharpBrowser1;
        private System.Windows.Forms.TextBox txtElement;
    }
}

