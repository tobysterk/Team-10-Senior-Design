namespace Text2Touch {
    partial class Text2TouchWindow {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.PortConnectionButton = new System.Windows.Forms.Button();
            this.PortSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.textEntryBox = new System.Windows.Forms.TextBox();
            this.returnTextLabel = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.chooseFileButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.portBox = new System.Windows.Forms.GroupBox();
            this.connectionStatusLabel = new System.Windows.Forms.Label();
            this.textSelectionBox = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.fileRadioButton = new System.Windows.Forms.RadioButton();
            this.manualRadioButton = new System.Windows.Forms.RadioButton();
            this.sendTextButton = new System.Windows.Forms.Button();
            this.portBox.SuspendLayout();
            this.textSelectionBox.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PortConnectionButton
            // 
            this.PortConnectionButton.Location = new System.Drawing.Point(340, 19);
            this.PortConnectionButton.Name = "PortConnectionButton";
            this.PortConnectionButton.Size = new System.Drawing.Size(75, 21);
            this.PortConnectionButton.TabIndex = 0;
            this.PortConnectionButton.Text = "Connect";
            this.PortConnectionButton.UseVisualStyleBackColor = true;
            this.PortConnectionButton.Click += new System.EventHandler(this.PortConnectionButton_Click);
            // 
            // PortSelectionComboBox
            // 
            this.PortSelectionComboBox.FormattingEnabled = true;
            this.PortSelectionComboBox.Location = new System.Drawing.Point(6, 19);
            this.PortSelectionComboBox.Name = "PortSelectionComboBox";
            this.PortSelectionComboBox.Size = new System.Drawing.Size(319, 21);
            this.PortSelectionComboBox.TabIndex = 1;
            this.PortSelectionComboBox.Text = "Choose your serial port...";
            this.PortSelectionComboBox.SelectedIndexChanged += new System.EventHandler(this.PortSelectionComboBox_SelectedIndexChanged);
            // 
            // textEntryBox
            // 
            this.textEntryBox.Enabled = false;
            this.textEntryBox.Location = new System.Drawing.Point(96, 48);
            this.textEntryBox.Multiline = true;
            this.textEntryBox.Name = "textEntryBox";
            this.textEntryBox.Size = new System.Drawing.Size(319, 21);
            this.textEntryBox.TabIndex = 2;
            this.textEntryBox.TextChanged += new System.EventHandler(this.textEntryBox_TextChanged);
            this.textEntryBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyPressed);
            // 
            // returnTextLabel
            // 
            this.returnTextLabel.AutoSize = true;
            this.returnTextLabel.Location = new System.Drawing.Point(156, 193);
            this.returnTextLabel.Name = "returnTextLabel";
            this.returnTextLabel.Size = new System.Drawing.Size(108, 13);
            this.returnTextLabel.TabIndex = 4;
            this.returnTextLabel.Text = "Text sent: \"Test text\"";
            // 
            // chooseFileButton
            // 
            this.chooseFileButton.Enabled = false;
            this.chooseFileButton.Location = new System.Drawing.Point(96, 19);
            this.chooseFileButton.Name = "chooseFileButton";
            this.chooseFileButton.Size = new System.Drawing.Size(75, 23);
            this.chooseFileButton.TabIndex = 5;
            this.chooseFileButton.Text = "Choose File";
            this.chooseFileButton.UseVisualStyleBackColor = true;
            this.chooseFileButton.Click += new System.EventHandler(this.chooseFileButton_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Location = new System.Drawing.Point(177, 24);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(75, 13);
            this.fileNameLabel.TabIndex = 6;
            this.fileNameLabel.Text = "No file chosen";
            // 
            // portBox
            // 
            this.portBox.Controls.Add(this.connectionStatusLabel);
            this.portBox.Controls.Add(this.PortSelectionComboBox);
            this.portBox.Controls.Add(this.PortConnectionButton);
            this.portBox.Location = new System.Drawing.Point(12, 17);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(421, 65);
            this.portBox.TabIndex = 7;
            this.portBox.TabStop = false;
            this.portBox.Text = "Serial Connection";
            // 
            // connectionStatusLabel
            // 
            this.connectionStatusLabel.AutoSize = true;
            this.connectionStatusLabel.Location = new System.Drawing.Point(6, 47);
            this.connectionStatusLabel.Name = "connectionStatusLabel";
            this.connectionStatusLabel.Size = new System.Drawing.Size(109, 13);
            this.connectionStatusLabel.TabIndex = 2;
            this.connectionStatusLabel.Text = "Status: Disconnected";
            // 
            // textSelectionBox
            // 
            this.textSelectionBox.Controls.Add(this.flowLayoutPanel1);
            this.textSelectionBox.Controls.Add(this.chooseFileButton);
            this.textSelectionBox.Controls.Add(this.textEntryBox);
            this.textSelectionBox.Controls.Add(this.fileNameLabel);
            this.textSelectionBox.Location = new System.Drawing.Point(12, 88);
            this.textSelectionBox.Name = "textSelectionBox";
            this.textSelectionBox.Size = new System.Drawing.Size(421, 75);
            this.textSelectionBox.TabIndex = 8;
            this.textSelectionBox.TabStop = false;
            this.textSelectionBox.Text = "Text Selection";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.fileRadioButton);
            this.flowLayoutPanel1.Controls.Add(this.manualRadioButton);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(9, 19);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(75, 46);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // fileRadioButton
            // 
            this.fileRadioButton.AutoSize = true;
            this.fileRadioButton.Enabled = false;
            this.fileRadioButton.Location = new System.Drawing.Point(3, 3);
            this.fileRadioButton.Name = "fileRadioButton";
            this.fileRadioButton.Size = new System.Drawing.Size(71, 17);
            this.fileRadioButton.TabIndex = 7;
            this.fileRadioButton.TabStop = true;
            this.fileRadioButton.Text = "Print a file";
            this.fileRadioButton.UseVisualStyleBackColor = true;
            this.fileRadioButton.CheckedChanged += new System.EventHandler(this.fileRadioButton_CheckedChanged);
            // 
            // manualRadioButton
            // 
            this.manualRadioButton.AutoSize = true;
            this.manualRadioButton.Enabled = false;
            this.manualRadioButton.Location = new System.Drawing.Point(3, 26);
            this.manualRadioButton.Name = "manualRadioButton";
            this.manualRadioButton.Size = new System.Drawing.Size(66, 17);
            this.manualRadioButton.TabIndex = 8;
            this.manualRadioButton.TabStop = true;
            this.manualRadioButton.Text = "Print text";
            this.manualRadioButton.UseVisualStyleBackColor = true;
            this.manualRadioButton.CheckedChanged += new System.EventHandler(this.manualRadioButton_CheckedChanged);
            // 
            // sendTextButton
            // 
            this.sendTextButton.Enabled = false;
            this.sendTextButton.Location = new System.Drawing.Point(172, 169);
            this.sendTextButton.Name = "sendTextButton";
            this.sendTextButton.Size = new System.Drawing.Size(75, 21);
            this.sendTextButton.TabIndex = 3;
            this.sendTextButton.Text = "Send";
            this.sendTextButton.UseVisualStyleBackColor = true;
            this.sendTextButton.Click += new System.EventHandler(this.sendTextButton_Click);
            // 
            // Text2TouchWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.textSelectionBox);
            this.Controls.Add(this.portBox);
            this.Controls.Add(this.sendTextButton);
            this.Controls.Add(this.returnTextLabel);
            this.Name = "Text2TouchWindow";
            this.Text = "Text2Touch";
            this.portBox.ResumeLayout(false);
            this.portBox.PerformLayout();
            this.textSelectionBox.ResumeLayout(false);
            this.textSelectionBox.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button PortConnectionButton;
        private System.Windows.Forms.ComboBox PortSelectionComboBox;
        private System.Windows.Forms.TextBox textEntryBox;
        private System.Windows.Forms.Label returnTextLabel;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button chooseFileButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.GroupBox portBox;
        private System.Windows.Forms.GroupBox textSelectionBox;
        private System.Windows.Forms.Button sendTextButton;
        private System.Windows.Forms.Label connectionStatusLabel;
        private System.Windows.Forms.RadioButton manualRadioButton;
        private System.Windows.Forms.RadioButton fileRadioButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}

