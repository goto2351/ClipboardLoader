﻿namespace ClipboardAutoloader
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.FilepathBox = new System.Windows.Forms.TextBox();
            this.SelectButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.BackButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FilepathBox
            // 
            this.FilepathBox.Location = new System.Drawing.Point(21, 32);
            this.FilepathBox.Name = "FilepathBox";
            this.FilepathBox.Size = new System.Drawing.Size(604, 31);
            this.FilepathBox.TabIndex = 0;
            // 
            // SelectButton
            // 
            this.SelectButton.Location = new System.Drawing.Point(632, 32);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(69, 39);
            this.SelectButton.TabIndex = 1;
            this.SelectButton.Text = "...";
            this.SelectButton.UseVisualStyleBackColor = true;
            this.SelectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(707, 32);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(138, 39);
            this.LoadButton.TabIndex = 2;
            this.LoadButton.Text = "読み込み";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // StartButton
            // 
            this.StartButton.Enabled = false;
            this.StartButton.Location = new System.Drawing.Point(21, 85);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(238, 53);
            this.StartButton.TabIndex = 3;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // BackButton
            // 
            this.BackButton.Enabled = false;
            this.BackButton.Location = new System.Drawing.Point(317, 85);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(238, 53);
            this.BackButton.TabIndex = 4;
            this.BackButton.Text = "前へ";
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.Enabled = false;
            this.NextButton.Location = new System.Drawing.Point(607, 85);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(238, 53);
            this.NextButton.TabIndex = 5;
            this.NextButton.Text = "次へ";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 177);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(857, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 199);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.SelectButton);
            this.Controls.Add(this.FilepathBox);
            this.Name = "Form1";
            this.Text = "ClipboardLoader";
            this.TopMost = true;
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FilepathBox;
        private System.Windows.Forms.Button SelectButton;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

