namespace TimeTrack
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            groupBox1 = new GroupBox();
            btnSaveAs = new Button();
            txtSavePath = new TextBox();
            label2 = new Label();
            btnWeightSetting = new Button();
            btnOpenDialog = new Button();
            btnCancel = new Button();
            btnOK = new Button();
            txtFilePath = new TextBox();
            label1 = new Label();
            openFileDialog1 = new OpenFileDialog();
            folderBrowserDialog1 = new FolderBrowserDialog();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackgroundImage = Properties.Resources.images;
            groupBox1.Controls.Add(btnSaveAs);
            groupBox1.Controls.Add(txtSavePath);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnWeightSetting);
            groupBox1.Controls.Add(btnOpenDialog);
            groupBox1.Controls.Add(btnCancel);
            groupBox1.Controls.Add(btnOK);
            groupBox1.Controls.Add(txtFilePath);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(9, 9);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(616, 180);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Work Hour Calculator:";
            // 
            // btnSaveAs
            // 
            btnSaveAs.Location = new Point(558, 75);
            btnSaveAs.Name = "btnSaveAs";
            btnSaveAs.Size = new Size(48, 34);
            btnSaveAs.TabIndex = 8;
            btnSaveAs.Text = "...";
            btnSaveAs.UseVisualStyleBackColor = true;
            btnSaveAs.Click += btnSaveAs_Click;
            // 
            // txtSavePath
            // 
            txtSavePath.Location = new Point(113, 78);
            txtSavePath.Name = "txtSavePath";
            txtSavePath.Size = new Size(439, 31);
            txtSavePath.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 84);
            label2.Name = "label2";
            label2.Size = new Size(78, 25);
            label2.TabIndex = 6;
            label2.Text = "Save As:";
            // 
            // btnWeightSetting
            // 
            btnWeightSetting.Location = new Point(6, 126);
            btnWeightSetting.Name = "btnWeightSetting";
            btnWeightSetting.Size = new Size(112, 45);
            btnWeightSetting.TabIndex = 5;
            btnWeightSetting.Text = "Rules";
            btnWeightSetting.UseVisualStyleBackColor = true;
            btnWeightSetting.Click += btnWeightSetting_Click;
            // 
            // btnOpenDialog
            // 
            btnOpenDialog.Location = new Point(558, 34);
            btnOpenDialog.Name = "btnOpenDialog";
            btnOpenDialog.Size = new Size(48, 35);
            btnOpenDialog.TabIndex = 4;
            btnOpenDialog.Text = "...";
            btnOpenDialog.UseVisualStyleBackColor = true;
            btnOpenDialog.Click += btnOpenDialog_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(495, 126);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(112, 45);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(360, 126);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(112, 45);
            btnOK.TabIndex = 2;
            btnOK.Text = "Confirm";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // txtFilePath
            // 
            txtFilePath.Location = new Point(113, 36);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.Size = new Size(439, 31);
            txtFilePath.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 36);
            label1.Name = "label1";
            label1.Size = new Size(101, 25);
            label1.TabIndex = 0;
            label1.Text = "Select Path:";
            // 
            // openFileDialog1
            // 
            openFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx";
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.images__2_;
            ClientSize = new Size(637, 192);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Attendance Calculation Tool (code by Andy 1.2)";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnCancel;
        private Button btnOK;
        private TextBox txtFilePath;
        private Label label1;
        private OpenFileDialog openFileDialog1;
        private Button btnOpenDialog;
        private Button btnWeightSetting;
        private Label label2;
        private Button btnSaveAs;
        private TextBox txtSavePath;
        private FolderBrowserDialog folderBrowserDialog1;
    }
}
