namespace WinFormsApp2
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
            videoView1 = new VisioForge.Core.UI.WinForms.VideoView();
            videoView2 = new VisioForge.Core.UI.WinForms.VideoView();
            button1 = new Button();
            SuspendLayout();
            // 
            // videoView1
            // 
            videoView1.BackColor = Color.Black;
            videoView1.Location = new Point(12, 12);
            videoView1.Name = "videoView1";
            videoView1.Size = new Size(480, 272);
            videoView1.StatusOverlay = null;
            videoView1.TabIndex = 0;
            // 
            // videoView2
            // 
            videoView2.BackColor = Color.Black;
            videoView2.Location = new Point(498, 12);
            videoView2.Name = "videoView2";
            videoView2.Size = new Size(480, 272);
            videoView2.StatusOverlay = null;
            videoView2.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(460, 290);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "Switch";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(994, 322);
            Controls.Add(button1);
            Controls.Add(videoView2);
            Controls.Add(videoView1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private VisioForge.Core.UI.WinForms.VideoView videoView1;
        private VisioForge.Core.UI.WinForms.VideoView videoView2;
        private Button button1;
    }
}