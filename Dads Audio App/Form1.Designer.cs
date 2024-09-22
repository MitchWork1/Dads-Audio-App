namespace Dads_Audio_App
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
            components = new System.ComponentModel.Container();
            playButton = new Button();
            selectMusicFileDialog = new OpenFileDialog();
            audioTrackLocationProgressBar = new ProgressBar();
            timer1 = new System.Windows.Forms.Timer(components);
            currentTimeLabel = new Label();
            flagButton = new Button();
            lyricTextBox = new RichTextBox();
            dataGridView1 = new DataGridView();
            FlagNumber = new DataGridViewTextBoxColumn();
            FlagName = new DataGridViewTextBoxColumn();
            FlagTime = new DataGridViewTextBoxColumn();
            ProgressBarValue = new DataGridViewTextBoxColumn();
            deltaLabel = new Label();
            deltaTimeLabel = new Label();
            treeView1 = new TreeView();
            treePanel = new Panel();
            setListSongsListBox = new ListBox();
            setlistListBox = new ListBox();
            textBox1 = new TextBox();
            setlistDeleteButton = new Button();
            addSongsButton = new Button();
            newSetListButton = new Button();
            timer2 = new System.Windows.Forms.Timer(components);
            controlPanel = new Panel();
            generateWaveLabel = new Label();
            coolDwon = new System.Windows.Forms.Timer(components);
            panel1 = new Panel();
            label1 = new Label();
            editingCheckBox = new CheckBox();
            fontDialog1 = new FontDialog();
            fontButton = new Button();
            flagTextCoolDown = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            treePanel.SuspendLayout();
            controlPanel.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // playButton
            // 
            playButton.Location = new Point(28, 478);
            playButton.Name = "playButton";
            playButton.Size = new Size(75, 23);
            playButton.TabIndex = 0;
            playButton.Text = "Play";
            playButton.UseVisualStyleBackColor = true;
            playButton.Click += playButton_Click;
            // 
            // selectMusicFileDialog
            // 
            selectMusicFileDialog.FileName = "openFileDialog1";
            selectMusicFileDialog.Multiselect = true;
            selectMusicFileDialog.FileOk += selectMusicFileDialog_FileOk;
            // 
            // audioTrackLocationProgressBar
            // 
            audioTrackLocationProgressBar.ForeColor = SystemColors.MenuHighlight;
            audioTrackLocationProgressBar.Location = new Point(29, 36);
            audioTrackLocationProgressBar.Name = "audioTrackLocationProgressBar";
            audioTrackLocationProgressBar.Size = new Size(1101, 23);
            audioTrackLocationProgressBar.TabIndex = 4;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // currentTimeLabel
            // 
            currentTimeLabel.AutoSize = true;
            currentTimeLabel.Location = new Point(29, 606);
            currentTimeLabel.Name = "currentTimeLabel";
            currentTimeLabel.Size = new Size(38, 15);
            currentTimeLabel.TabIndex = 6;
            currentTimeLabel.Text = "label1";
            currentTimeLabel.Click += currentTimeLabel_Click;
            // 
            // flagButton
            // 
            flagButton.Location = new Point(110, 478);
            flagButton.Name = "flagButton";
            flagButton.Size = new Size(75, 23);
            flagButton.TabIndex = 7;
            flagButton.Text = "Add Flag";
            flagButton.UseVisualStyleBackColor = true;
            flagButton.Click += flagButton_Click;
            // 
            // lyricTextBox
            // 
            lyricTextBox.Enabled = false;
            lyricTextBox.Location = new Point(711, 35);
            lyricTextBox.Name = "lyricTextBox";
            lyricTextBox.ReadOnly = true;
            lyricTextBox.Size = new Size(444, 446);
            lyricTextBox.TabIndex = 8;
            lyricTextBox.Text = "";
            lyricTextBox.TextChanged += lyricTextBox_TextChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { FlagNumber, FlagName, FlagTime, ProgressBarValue });
            dataGridView1.Location = new Point(366, 37);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(341, 332);
            dataGridView1.TabIndex = 9;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            dataGridView1.RowsRemoved += dataGridView1_RowsRemoved;
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            // 
            // FlagNumber
            // 
            FlagNumber.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            FlagNumber.HeaderText = "FlagNumber";
            FlagNumber.MinimumWidth = 6;
            FlagNumber.Name = "FlagNumber";
            FlagNumber.ReadOnly = true;
            FlagNumber.Width = 98;
            // 
            // FlagName
            // 
            FlagName.HeaderText = "FlagName";
            FlagName.MinimumWidth = 6;
            FlagName.Name = "FlagName";
            FlagName.Width = 125;
            // 
            // FlagTime
            // 
            FlagTime.HeaderText = "FlagTime";
            FlagTime.MinimumWidth = 6;
            FlagTime.Name = "FlagTime";
            FlagTime.Width = 125;
            // 
            // ProgressBarValue
            // 
            ProgressBarValue.HeaderText = "ProgressBarValue";
            ProgressBarValue.MinimumWidth = 6;
            ProgressBarValue.Name = "ProgressBarValue";
            ProgressBarValue.Visible = false;
            ProgressBarValue.Width = 125;
            // 
            // deltaLabel
            // 
            deltaLabel.AutoSize = true;
            deltaLabel.BackColor = SystemColors.ButtonHighlight;
            deltaLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            deltaLabel.Location = new Point(100, 5);
            deltaLabel.Name = "deltaLabel";
            deltaLabel.Size = new Size(20, 25);
            deltaLabel.TabIndex = 11;
            deltaLabel.Text = "-";
            deltaLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // deltaTimeLabel
            // 
            deltaTimeLabel.AutoSize = true;
            deltaTimeLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            deltaTimeLabel.ForeColor = Color.Red;
            deltaTimeLabel.Location = new Point(169, 5);
            deltaTimeLabel.Name = "deltaTimeLabel";
            deltaTimeLabel.Size = new Size(20, 25);
            deltaTimeLabel.TabIndex = 12;
            deltaTimeLabel.Text = "-";
            deltaTimeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // treeView1
            // 
            treeView1.AllowDrop = true;
            treeView1.LabelEdit = true;
            treeView1.Location = new Point(3, 29);
            treeView1.Name = "treeView1";
            treeView1.ShowNodeToolTips = true;
            treeView1.Size = new Size(350, 440);
            treeView1.TabIndex = 13;
            treeView1.BeforeLabelEdit += treeView1_BeforeLabelEdit;
            treeView1.AfterLabelEdit += treeView1_AfterLabelEdit;
            treeView1.BeforeExpand += treeView1_BeforeExpand;
            treeView1.ItemDrag += treeView1_ItemDrag;
            treeView1.AfterSelect += treeView1_AfterSelect;
            treeView1.NodeMouseClick += treeView1_NodeMouseClick;
            treeView1.DragDrop += treeView1_DragDrop;
            treeView1.DragEnter += treeView1_DragEnter_1;
            treeView1.DoubleClick += treeView1_DoubleClick;
            treeView1.Leave += treeView1_Leave;
            // 
            // treePanel
            // 
            treePanel.Controls.Add(setListSongsListBox);
            treePanel.Controls.Add(setlistListBox);
            treePanel.Controls.Add(textBox1);
            treePanel.Controls.Add(setlistDeleteButton);
            treePanel.Controls.Add(addSongsButton);
            treePanel.Controls.Add(newSetListButton);
            treePanel.Controls.Add(treeView1);
            treePanel.Location = new Point(0, 6);
            treePanel.Name = "treePanel";
            treePanel.Size = new Size(358, 470);
            treePanel.TabIndex = 14;
            // 
            // setListSongsListBox
            // 
            setListSongsListBox.FormattingEnabled = true;
            setListSongsListBox.ItemHeight = 15;
            setListSongsListBox.Location = new Point(154, 192);
            setListSongsListBox.Name = "setListSongsListBox";
            setListSongsListBox.Size = new Size(190, 439);
            setListSongsListBox.TabIndex = 20;
            setListSongsListBox.MouseClick += setListSongsListBox_MouseClick;
            // 
            // setlistListBox
            // 
            setlistListBox.FormattingEnabled = true;
            setlistListBox.ItemHeight = 15;
            setlistListBox.Location = new Point(0, 192);
            setlistListBox.Name = "setlistListBox";
            setlistListBox.Size = new Size(154, 439);
            setlistListBox.TabIndex = 19;
            setlistListBox.MouseClick += setlistListBox_MouseClick;
            setlistListBox.SelectedIndexChanged += setlistListBox_SelectedIndexChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(244, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 18;
            // 
            // setlistDeleteButton
            // 
            setlistDeleteButton.Location = new Point(163, 3);
            setlistDeleteButton.Name = "setlistDeleteButton";
            setlistDeleteButton.Size = new Size(75, 23);
            setlistDeleteButton.TabIndex = 17;
            setlistDeleteButton.Text = "Delete Setlist";
            setlistDeleteButton.UseVisualStyleBackColor = true;
            setlistDeleteButton.Click += setlistDeleteButton_Click;
            // 
            // addSongsButton
            // 
            addSongsButton.Enabled = false;
            addSongsButton.Location = new Point(82, 3);
            addSongsButton.Name = "addSongsButton";
            addSongsButton.Size = new Size(75, 23);
            addSongsButton.TabIndex = 15;
            addSongsButton.Text = "Add songs";
            addSongsButton.UseVisualStyleBackColor = true;
            addSongsButton.Click += addSongsButton_Click;
            // 
            // newSetListButton
            // 
            newSetListButton.Location = new Point(3, 3);
            newSetListButton.Name = "newSetListButton";
            newSetListButton.Size = new Size(73, 23);
            newSetListButton.TabIndex = 14;
            newSetListButton.Text = "New Setlist";
            newSetListButton.UseVisualStyleBackColor = true;
            newSetListButton.Click += newSetListButton_Click;
            // 
            // timer2
            // 
            timer2.Interval = 2000;
            timer2.Tick += timer2_Tick;
            // 
            // controlPanel
            // 
            controlPanel.Controls.Add(audioTrackLocationProgressBar);
            controlPanel.Location = new Point(0, 502);
            controlPanel.Name = "controlPanel";
            controlPanel.Size = new Size(1176, 101);
            controlPanel.TabIndex = 15;
            // 
            // generateWaveLabel
            // 
            generateWaveLabel.AutoSize = true;
            generateWaveLabel.BackColor = SystemColors.MenuBar;
            generateWaveLabel.Font = new Font("Arial Narrow", 18F, FontStyle.Bold, GraphicsUnit.Point);
            generateWaveLabel.Location = new Point(436, 470);
            generateWaveLabel.Name = "generateWaveLabel";
            generateWaveLabel.Size = new Size(189, 29);
            generateWaveLabel.TabIndex = 19;
            generateWaveLabel.Text = "Generating Wave...";
            generateWaveLabel.TextAlign = ContentAlignment.MiddleCenter;
            generateWaveLabel.Visible = false;
            // 
            // coolDwon
            // 
            coolDwon.Interval = 200;
            coolDwon.Tick += coolDwon_Tick;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonHighlight;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(deltaLabel);
            panel1.Controls.Add(deltaTimeLabel);
            panel1.Location = new Point(366, 375);
            panel1.Name = "panel1";
            panel1.Size = new Size(259, 39);
            panel1.TabIndex = 16;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(-2, 5);
            label1.Name = "label1";
            label1.Size = new Size(84, 25);
            label1.TabIndex = 13;
            label1.Text = "Up Next:";
            // 
            // editingCheckBox
            // 
            editingCheckBox.AutoSize = true;
            editingCheckBox.Location = new Point(364, 12);
            editingCheckBox.Name = "editingCheckBox";
            editingCheckBox.Size = new Size(101, 19);
            editingCheckBox.TabIndex = 17;
            editingCheckBox.Text = "Enable Editing";
            editingCheckBox.UseVisualStyleBackColor = true;
            editingCheckBox.CheckedChanged += editingCheckBox_CheckedChanged;
            editingCheckBox.KeyDown += editingCheckBox_KeyDown;
            editingCheckBox.PreviewKeyDown += editingCheckBox_PreviewKeyDown;
            // 
            // fontButton
            // 
            fontButton.Enabled = false;
            fontButton.Location = new Point(711, 9);
            fontButton.Name = "fontButton";
            fontButton.Size = new Size(99, 23);
            fontButton.TabIndex = 18;
            fontButton.Text = "Change Font";
            fontButton.UseVisualStyleBackColor = true;
            fontButton.Click += button1_Click_2;
            // 
            // flagTextCoolDown
            // 
            flagTextCoolDown.Interval = 2000;
            flagTextCoolDown.Tick += flagTextCoolDown_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1174, 639);
            Controls.Add(generateWaveLabel);
            Controls.Add(fontButton);
            Controls.Add(editingCheckBox);
            Controls.Add(panel1);
            Controls.Add(currentTimeLabel);
            Controls.Add(treePanel);
            Controls.Add(dataGridView1);
            Controls.Add(lyricTextBox);
            Controls.Add(flagButton);
            Controls.Add(playButton);
            Controls.Add(controlPanel);
            KeyPreview = true;
            Name = "Form1";
            Text = "Form1";
            KeyDown += Form1_KeyDown;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            treePanel.ResumeLayout(false);
            treePanel.PerformLayout();
            controlPanel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button playButton;
        private OpenFileDialog selectMusicFileDialog;
        private ProgressBar audioTrackLocationProgressBar;
        private System.Windows.Forms.Timer timer1;
        private Label currentTimeLabel;
        private Button flagButton;
        private RichTextBox lyricTextBox;
        private DataGridView dataGridView1;
        private Label deltaLabel;
        private Label deltaTimeLabel;
        private DataGridViewTextBoxColumn FlagNumber;
        private DataGridViewTextBoxColumn FlagName;
        private DataGridViewTextBoxColumn FlagTime;
        private DataGridViewTextBoxColumn ProgressBarValue;
        private TreeView treeView1;
        private Panel treePanel;
        private Button newSetListButton;
        private Button addSongsButton;
        private System.Windows.Forms.Timer timer2;
        private Panel controlPanel;
        private System.Windows.Forms.Timer coolDwon;
        private Panel panel1;
        private Label label1;
        private CheckBox editingCheckBox;
        private Button setlistDeleteButton;
        private FontDialog fontDialog1;
        private Button fontButton;
        private TextBox textBox1;
        private ListBox setlistListBox;
        private ListBox setListSongsListBox;
        private Label generateWaveLabel;
        private System.Windows.Forms.Timer flagTextCoolDown;
    }
}