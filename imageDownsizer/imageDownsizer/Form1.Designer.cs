namespace imageDownsizer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSelectImage = new Button();
            btnDownscale = new Button();
            btnCancel = new Button();
            txtScaleFactor = new TextBox();
            progressBar = new ProgressBar();
            picBoxOriginal = new PictureBox();
            picBoxDownscaled = new PictureBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)picBoxOriginal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBoxDownscaled).BeginInit();
            SuspendLayout();
            // 
            // btnSelectImage
            // 
            btnSelectImage.BackColor = SystemColors.HotTrack;
            btnSelectImage.Font = new Font("Sitka Small", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnSelectImage.ForeColor = SystemColors.ButtonHighlight;
            btnSelectImage.Location = new Point(259, 17);
            btnSelectImage.Margin = new Padding(3, 2, 3, 2);
            btnSelectImage.Name = "btnSelectImage";
            btnSelectImage.Size = new Size(129, 26);
            btnSelectImage.TabIndex = 0;
            btnSelectImage.Text = "SELECT IMAGE";
            btnSelectImage.UseVisualStyleBackColor = false;
            btnSelectImage.Click += btnSelectImage_Click_1;
            // 
            // btnDownscale
            // 
            btnDownscale.BackColor = SystemColors.HotTrack;
            btnDownscale.Font = new Font("Sitka Small", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnDownscale.ForeColor = Color.White;
            btnDownscale.Location = new Point(907, 15);
            btnDownscale.Margin = new Padding(3, 2, 3, 2);
            btnDownscale.Name = "btnDownscale";
            btnDownscale.Size = new Size(114, 30);
            btnDownscale.TabIndex = 1;
            btnDownscale.Text = "DOWNSCALE";
            btnDownscale.UseVisualStyleBackColor = false;
            btnDownscale.Click += btnDownscale_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(1193, 494);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(82, 22);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "CANCEL";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtScaleFactor
            // 
            txtScaleFactor.Location = new Point(609, 41);
            txtScaleFactor.Margin = new Padding(3, 2, 3, 2);
            txtScaleFactor.Name = "txtScaleFactor";
            txtScaleFactor.Size = new Size(72, 23);
            txtScaleFactor.TabIndex = 3;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(349, 494);
            progressBar.Margin = new Padding(3, 2, 3, 2);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(571, 22);
            progressBar.TabIndex = 4;
            // 
            // picBoxOriginal
            // 
            picBoxOriginal.Location = new Point(55, 68);
            picBoxOriginal.Margin = new Padding(3, 2, 3, 2);
            picBoxOriginal.Name = "picBoxOriginal";
            picBoxOriginal.Size = new Size(563, 414);
            picBoxOriginal.TabIndex = 5;
            picBoxOriginal.TabStop = false;
            // 
            // picBoxDownscaled
            // 
            picBoxDownscaled.Location = new Point(668, 68);
            picBoxDownscaled.Margin = new Padding(3, 2, 3, 2);
            picBoxDownscaled.Name = "picBoxDownscaled";
            picBoxDownscaled.Size = new Size(556, 414);
            picBoxDownscaled.TabIndex = 6;
            picBoxDownscaled.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Lucida Fax", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(428, 9);
            label1.Name = "label1";
            label1.Size = new Size(433, 17);
            label1.TabIndex = 7;
            label1.Text = "INPUT DESIRED DOWNSIZING SCALE DOWN HERE,EG:0.5\r\n";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1287, 527);
            Controls.Add(label1);
            Controls.Add(picBoxDownscaled);
            Controls.Add(picBoxOriginal);
            Controls.Add(progressBar);
            Controls.Add(txtScaleFactor);
            Controls.Add(btnCancel);
            Controls.Add(btnDownscale);
            Controls.Add(btnSelectImage);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)picBoxOriginal).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBoxDownscaled).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSelectImage;
        private Button btnDownscale;
        private Button btnCancel;
        private TextBox txtScaleFactor;
        private ProgressBar progressBar;
        private PictureBox picBoxOriginal;
        private PictureBox picBoxDownscaled;
        private Label label1;
    }
}