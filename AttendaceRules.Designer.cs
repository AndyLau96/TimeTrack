namespace TimeTrack
{
    partial class AttendaceRules
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
            groupBox1 = new GroupBox();
            requiredTime = new DateTimePicker();
            lunchTime = new DateTimePicker();
            endTime = new DateTimePicker();
            startTime = new DateTimePicker();
            label4 = new Label();
            label3 = new Label();
            btnCancelRules = new Button();
            btnConfirmRules = new Button();
            label2 = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(requiredTime);
            groupBox1.Controls.Add(lunchTime);
            groupBox1.Controls.Add(endTime);
            groupBox1.Controls.Add(startTime);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(btnCancelRules);
            groupBox1.Controls.Add(btnConfirmRules);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.ForeColor = Color.Black;
            groupBox1.Location = new Point(15, 15);
            groupBox1.Margin = new Padding(4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4);
            groupBox1.Size = new Size(591, 274);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Calculation Parameters:";
            // 
            // requiredTime
            // 
            requiredTime.CustomFormat = "HH:mm";
            requiredTime.Enabled = false;
            requiredTime.Format = DateTimePickerFormat.Custom;
            requiredTime.Location = new Point(222, 181);
            requiredTime.Name = "requiredTime";
            requiredTime.ShowUpDown = true;
            requiredTime.Size = new Size(183, 31);
            requiredTime.TabIndex = 16;
            // 
            // lunchTime
            // 
            lunchTime.CustomFormat = "HH:mm";
            lunchTime.Format = DateTimePickerFormat.Custom;
            lunchTime.Location = new Point(222, 131);
            lunchTime.Name = "lunchTime";
            lunchTime.ShowUpDown = true;
            lunchTime.Size = new Size(183, 31);
            lunchTime.TabIndex = 15;
            lunchTime.ValueChanged += TimeSettings_ValueChanged;
            // 
            // endTime
            // 
            endTime.CustomFormat = "HH:mm:ss";
            endTime.Format = DateTimePickerFormat.Custom;
            endTime.Location = new Point(225, 82);
            endTime.Margin = new Padding(4);
            endTime.Name = "endTime";
            endTime.ShowUpDown = true;
            endTime.Size = new Size(180, 31);
            endTime.TabIndex = 12;
            endTime.ValueChanged += TimeSettings_ValueChanged;
            // 
            // startTime
            // 
            startTime.CustomFormat = "HH:mm:ss";
            startTime.Format = DateTimePickerFormat.Custom;
            startTime.Location = new Point(225, 34);
            startTime.Margin = new Padding(4);
            startTime.Name = "startTime";
            startTime.ShowUpDown = true;
            startTime.Size = new Size(180, 31);
            startTime.TabIndex = 11;
            startTime.ValueChanged += TimeSettings_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(8, 186);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(197, 25);
            label4.TabIndex = 8;
            label4.Text = "Required Time (Hours) :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(8, 136);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(173, 25);
            label3.TabIndex = 7;
            label3.Text = "Lunch Time (Hours) :";
            // 
            // btnCancelRules
            // 
            btnCancelRules.Location = new Point(341, 228);
            btnCancelRules.Margin = new Padding(4);
            btnCancelRules.Name = "btnCancelRules";
            btnCancelRules.Size = new Size(118, 36);
            btnCancelRules.TabIndex = 5;
            btnCancelRules.Text = "Cancel";
            btnCancelRules.UseVisualStyleBackColor = true;
            btnCancelRules.Click += btnCancelRules_Click;
            // 
            // btnConfirmRules
            // 
            btnConfirmRules.Location = new Point(466, 228);
            btnConfirmRules.Margin = new Padding(4);
            btnConfirmRules.Name = "btnConfirmRules";
            btnConfirmRules.Size = new Size(118, 36);
            btnConfirmRules.TabIndex = 4;
            btnConfirmRules.Text = "Confirm";
            btnConfirmRules.UseVisualStyleBackColor = true;
            btnConfirmRules.Click += btnConfirmRules_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 89);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(147, 25);
            label2.TabIndex = 1;
            label2.Text = "End Time (Time) :";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 40);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(153, 25);
            label1.TabIndex = 0;
            label1.Text = "Start Time (Time) :";
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            pictureBox1.Image = Properties.Resources._1186255;
            pictureBox1.Location = new Point(414, 30);
            pictureBox1.Margin = new Padding(4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(170, 186);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // AttendaceRules
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(621, 304);
            Controls.Add(groupBox1);
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "AttendaceRules";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Attendance Rules";
            Load += AttendaceRules_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private PictureBox pictureBox1;
        private Button btnCancelRules;
        private Button btnConfirmRules;
        private Label label2;
        private Label label1;
        private Label label3;
        private Label label4;
        private DateTimePicker startTime;
        private DateTimePicker endTime;
        private DateTimePicker requiredTime;
        private DateTimePicker lunchTime;
    }
}