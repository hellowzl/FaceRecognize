namespace FaceRecognize
{
    partial class VedioDetect
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
            this.label1 = new System.Windows.Forms.Label();
            this.nextImage = new System.Windows.Forms.Button();
            this.preImage = new System.Windows.Forms.Button();
            this.SaveSampleImage = new System.Windows.Forms.Button();
            this.fullname = new System.Windows.Forms.TextBox();
            this.sampleBox = new System.Windows.Forms.PictureBox();
            this.picShow = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.recognizerType = new System.Windows.Forms.ComboBox();
            this.setClass = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sampleBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picShow)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(433, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "姓名：";
            // 
            // nextImage
            // 
            this.nextImage.Location = new System.Drawing.Point(559, 190);
            this.nextImage.Name = "nextImage";
            this.nextImage.Size = new System.Drawing.Size(25, 24);
            this.nextImage.TabIndex = 17;
            this.nextImage.Text = ">>";
            this.nextImage.UseVisualStyleBackColor = true;
            this.nextImage.Click += new System.EventHandler(this.NextImage_Click);
            // 
            // preImage
            // 
            this.preImage.Location = new System.Drawing.Point(431, 190);
            this.preImage.Name = "preImage";
            this.preImage.Size = new System.Drawing.Size(25, 24);
            this.preImage.TabIndex = 18;
            this.preImage.Text = "<<";
            this.preImage.UseVisualStyleBackColor = true;
            this.preImage.Click += new System.EventHandler(this.PreImage_Click);
            // 
            // SaveSampleImage
            // 
            this.SaveSampleImage.Location = new System.Drawing.Point(462, 190);
            this.SaveSampleImage.Name = "SaveSampleImage";
            this.SaveSampleImage.Size = new System.Drawing.Size(91, 24);
            this.SaveSampleImage.TabIndex = 16;
            this.SaveSampleImage.Text = "保存样本图片";
            this.SaveSampleImage.UseVisualStyleBackColor = true;
            this.SaveSampleImage.Click += new System.EventHandler(this.SaveSampleImage_Click);
            // 
            // fullname
            // 
            this.fullname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fullname.Location = new System.Drawing.Point(478, 163);
            this.fullname.Name = "fullname";
            this.fullname.Size = new System.Drawing.Size(104, 21);
            this.fullname.TabIndex = 15;
            // 
            // sampleBox
            // 
            this.sampleBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sampleBox.Location = new System.Drawing.Point(432, 13);
            this.sampleBox.Name = "sampleBox";
            this.sampleBox.Size = new System.Drawing.Size(150, 144);
            this.sampleBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.sampleBox.TabIndex = 14;
            this.sampleBox.TabStop = false;
            this.sampleBox.Click += new System.EventHandler(this.SampleBox_Click);
            // 
            // picShow
            // 
            this.picShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picShow.Location = new System.Drawing.Point(7, 12);
            this.picShow.Name = "picShow";
            this.picShow.Size = new System.Drawing.Size(415, 329);
            this.picShow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picShow.TabIndex = 13;
            this.picShow.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            // 
            // recognizerType
            // 
            this.recognizerType.FormattingEnabled = true;
            this.recognizerType.Items.AddRange(new object[] {
            "EigenFaceRecognizer",
            "FisherFaceRecognizer",
            "LBPHFaceRecognizer"});
            this.recognizerType.Location = new System.Drawing.Point(432, 223);
            this.recognizerType.Name = "recognizerType";
            this.recognizerType.Size = new System.Drawing.Size(149, 20);
            this.recognizerType.TabIndex = 24;
            this.recognizerType.Text = "EigenFaceRecognizer";
            // 
            // setClass
            // 
            this.setClass.Location = new System.Drawing.Point(432, 249);
            this.setClass.Name = "setClass";
            this.setClass.Size = new System.Drawing.Size(150, 23);
            this.setClass.TabIndex = 23;
            this.setClass.Text = "设置识别类";
            this.setClass.UseVisualStyleBackColor = true;
            this.setClass.Click += new System.EventHandler(this.SetClass_Click);
            // 
            // VedioDetect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 352);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nextImage);
            this.Controls.Add(this.preImage);
            this.Controls.Add(this.SaveSampleImage);
            this.Controls.Add(this.fullname);
            this.Controls.Add(this.sampleBox);
            this.Controls.Add(this.picShow);
            this.Controls.Add(this.recognizerType);
            this.Controls.Add(this.setClass);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VedioDetect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "摄像头检测人脸";
            ((System.ComponentModel.ISupportInitialize)(this.sampleBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picShow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button nextImage;
        private System.Windows.Forms.Button preImage;
        private System.Windows.Forms.Button SaveSampleImage;
        private System.Windows.Forms.TextBox fullname;
        private System.Windows.Forms.PictureBox sampleBox;
        private System.Windows.Forms.PictureBox picShow;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox recognizerType;
        private System.Windows.Forms.Button setClass;
    }
}