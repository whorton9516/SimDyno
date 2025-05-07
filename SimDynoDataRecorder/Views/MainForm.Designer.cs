namespace SimDynoDataRecorder;

partial class MainForm
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
        IPAddressTextBox = new TextBox();
        IPAdressLabel = new Label();
        PortLabel = new Label();
        PortTextBox = new TextBox();
        BroadcastButton = new Button();
        ViewDataButton = new Button();
        RecordButton = new Button();
        ListenButton = new Button();
        comboBoxRecordings = new ComboBox();
        label1 = new Label();
        labelPacketsReceived = new Label();
        SuspendLayout();
        // 
        // IPAddressTextBox
        // 
        IPAddressTextBox.Location = new Point(115, 52);
        IPAddressTextBox.Name = "IPAddressTextBox";
        IPAddressTextBox.Size = new Size(92, 31);
        IPAddressTextBox.TabIndex = 0;
        IPAddressTextBox.TabStop = false;
        IPAddressTextBox.Text = "127.0.0.1";
        IPAddressTextBox.TextChanged += IPAddressTextBox_TextChanged;
        // 
        // IPAdressLabel
        // 
        IPAdressLabel.AutoSize = true;
        IPAdressLabel.Location = new Point(8, 52);
        IPAdressLabel.Name = "IPAdressLabel";
        IPAdressLabel.Size = new Size(101, 25);
        IPAdressLabel.TabIndex = 6;
        IPAdressLabel.Text = "IP Address:";
        // 
        // PortLabel
        // 
        PortLabel.AutoSize = true;
        PortLabel.Location = new Point(61, 94);
        PortLabel.Name = "PortLabel";
        PortLabel.Size = new Size(48, 25);
        PortLabel.TabIndex = 4;
        PortLabel.Text = "Port:";
        // 
        // PortTextBox
        // 
        PortTextBox.Location = new Point(115, 94);
        PortTextBox.Name = "PortTextBox";
        PortTextBox.Size = new Size(92, 31);
        PortTextBox.TabIndex = 1;
        PortTextBox.TabStop = false;
        PortTextBox.Text = "5555";
        PortTextBox.TextChanged += PortTextBox_TextChanged;
        // 
        // BroadcastButton
        // 
        BroadcastButton.Location = new Point(213, 92);
        BroadcastButton.Name = "BroadcastButton";
        BroadcastButton.Size = new Size(112, 34);
        BroadcastButton.TabIndex = 3;
        BroadcastButton.TabStop = false;
        BroadcastButton.Text = "Broadcast";
        BroadcastButton.UseVisualStyleBackColor = true;
        BroadcastButton.Click += BroadcastButton_Click;
        // 
        // ViewDataButton
        // 
        ViewDataButton.Location = new Point(12, 131);
        ViewDataButton.Name = "ViewDataButton";
        ViewDataButton.Size = new Size(317, 34);
        ViewDataButton.TabIndex = 5;
        ViewDataButton.TabStop = false;
        ViewDataButton.Text = "View Data";
        ViewDataButton.UseVisualStyleBackColor = true;
        ViewDataButton.Click += ViewDataButton_Click;
        // 
        // RecordButton
        // 
        RecordButton.Location = new Point(213, 52);
        RecordButton.Name = "RecordButton";
        RecordButton.Size = new Size(112, 34);
        RecordButton.TabIndex = 2;
        RecordButton.TabStop = false;
        RecordButton.Text = "Record";
        RecordButton.UseVisualStyleBackColor = true;
        RecordButton.Click += RecordButton_Click;
        // 
        // ListenButton
        // 
        ListenButton.Location = new Point(12, 12);
        ListenButton.Name = "ListenButton";
        ListenButton.Size = new Size(317, 34);
        ListenButton.TabIndex = 4;
        ListenButton.TabStop = false;
        ListenButton.Text = "Start Listening";
        ListenButton.UseVisualStyleBackColor = true;
        ListenButton.Click += ListenButton_Click;
        // 
        // comboBoxRecordings
        // 
        comboBoxRecordings.FormattingEnabled = true;
        comboBoxRecordings.Location = new Point(12, 171);
        comboBoxRecordings.Name = "comboBoxRecordings";
        comboBoxRecordings.Size = new Size(313, 33);
        comboBoxRecordings.TabIndex = 6;
        comboBoxRecordings.TabStop = false;
        comboBoxRecordings.Text = "Select a recording";
        comboBoxRecordings.SelectedIndexChanged += comboBoxRecordings_SelectedIndexChanged;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(12, 207);
        label1.Name = "label1";
        label1.Size = new Size(148, 25);
        label1.TabIndex = 7;
        label1.Text = "Packets Received:";
        // 
        // labelPacketsReceived
        // 
        labelPacketsReceived.AutoSize = true;
        labelPacketsReceived.Location = new Point(166, 207);
        labelPacketsReceived.Name = "labelPacketsReceived";
        labelPacketsReceived.Size = new Size(22, 25);
        labelPacketsReceived.TabIndex = 8;
        labelPacketsReceived.Text = "0";
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(337, 245);
        Controls.Add(labelPacketsReceived);
        Controls.Add(label1);
        Controls.Add(comboBoxRecordings);
        Controls.Add(ListenButton);
        Controls.Add(RecordButton);
        Controls.Add(ViewDataButton);
        Controls.Add(BroadcastButton);
        Controls.Add(PortTextBox);
        Controls.Add(PortLabel);
        Controls.Add(IPAdressLabel);
        Controls.Add(IPAddressTextBox);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "MainForm";
        Text = "SimDyno Data Recorder";
        FormClosing += MainForm_FormClosing;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TextBox IPAddressTextBox;
    private Label IPAdressLabel;
    private Label PortLabel;
    private TextBox PortTextBox;
    private Button BroadcastButton;
    private Button ViewDataButton;
    private Button RecordButton;
    private Button ListenButton;
    private ComboBox comboBoxRecordings;
    private Label label1;
    private Label labelPacketsReceived;
}
