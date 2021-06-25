namespace AzureCognitive
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Browse = new System.Windows.Forms.Button();
            this.txt_Filename = new System.Windows.Forms.TextBox();
            this.pic_Image = new System.Windows.Forms.PictureBox();
            this.btn_Calculate = new System.Windows.Forms.Button();
            this.txt_Results = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Detected = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Image)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Browse
            // 
            this.btn_Browse.Location = new System.Drawing.Point(524, 10);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(100, 22);
            this.btn_Browse.TabIndex = 0;
            this.btn_Browse.Text = "Browse";
            this.btn_Browse.UseVisualStyleBackColor = true;
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // txt_Filename
            // 
            this.txt_Filename.Location = new System.Drawing.Point(11, 10);
            this.txt_Filename.Name = "txt_Filename";
            this.txt_Filename.Size = new System.Drawing.Size(507, 22);
            this.txt_Filename.TabIndex = 1;
            // 
            // pic_Image
            // 
            this.pic_Image.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_Image.Location = new System.Drawing.Point(11, 38);
            this.pic_Image.Name = "pic_Image";
            this.pic_Image.Size = new System.Drawing.Size(507, 307);
            this.pic_Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Image.TabIndex = 2;
            this.pic_Image.TabStop = false;
            // 
            // btn_Calculate
            // 
            this.btn_Calculate.Location = new System.Drawing.Point(628, 10);
            this.btn_Calculate.Name = "btn_Calculate";
            this.btn_Calculate.Size = new System.Drawing.Size(100, 22);
            this.btn_Calculate.TabIndex = 0;
            this.btn_Calculate.Text = "Calculate";
            this.btn_Calculate.UseVisualStyleBackColor = true;
            this.btn_Calculate.Click += new System.EventHandler(this.btn_Calculate_Click);
            // 
            // txt_Results
            // 
            this.txt_Results.Location = new System.Drawing.Point(527, 93);
            this.txt_Results.Multiline = true;
            this.txt_Results.Name = "txt_Results";
            this.txt_Results.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Results.Size = new System.Drawing.Size(327, 252);
            this.txt_Results.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(525, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "API Response";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(524, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Describe";
            // 
            // txt_Detected
            // 
            this.txt_Detected.Location = new System.Drawing.Point(526, 53);
            this.txt_Detected.Name = "txt_Detected";
            this.txt_Detected.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Detected.Size = new System.Drawing.Size(328, 22);
            this.txt_Detected.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 355);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pic_Image);
            this.Controls.Add(this.txt_Detected);
            this.Controls.Add(this.txt_Results);
            this.Controls.Add(this.txt_Filename);
            this.Controls.Add(this.btn_Calculate);
            this.Controls.Add(this.btn_Browse);
            this.Name = "Form1";
            this.Text = "Face API";
            ((System.ComponentModel.ISupportInitialize)(this.pic_Image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Browse;
        private System.Windows.Forms.TextBox txt_Filename;
        private System.Windows.Forms.PictureBox pic_Image;
        private System.Windows.Forms.Button btn_Calculate;
        private System.Windows.Forms.TextBox txt_Results;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Detected;
    }
}

