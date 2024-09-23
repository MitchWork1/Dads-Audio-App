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
            songsListBox = new ListBox();
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
            setListContextMenu = new ContextMenuStrip(components);
            addToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            songsContextMenu = new ContextMenuStrip(components);
            addToolStripMenuItem1 = new ToolStripMenuItem();
            deleteToolStripMenuItem1 = new ToolStripMenuItem();
            flagsContextStrip = new ContextMenuStrip(components);
            addToolStripMenuItem2 = new ToolStripMenuItem();
            deleteToolStripMenuItem2 = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            treePanel.SuspendLayout();
            controlPanel.SuspendLayout();
            panel1.SuspendLayout();
            setListContextMenu.SuspendLayout();
            songsContextMenu.SuspendLayout();
            flagsContextStrip.SuspendLayout();
            SuspendLayout();
            // 
            // playButton
            // 
            playButton.Location = new Point(32, 637);
            playButton.Margin = new Padding(3, 4, 3, 4);
            playButton.Name = "playButton";
            playButton.Size = new Size(86, 31);
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
            audioTrackLocationProgressBar.Location = new Point(33, 48);
            audioTrackLocationProgressBar.Margin = new Padding(3, 4, 3, 4);
            audioTrackLocationProgressBar.Name = "audioTrackLocationProgressBar";
            audioTrackLocationProgressBar.Size = new Size(1258, 31);
            audioTrackLocationProgressBar.TabIndex = 4;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // currentTimeLabel
            // 
            currentTimeLabel.AutoSize = true;
            currentTimeLabel.Location = new Point(33, 808);
            currentTimeLabel.Name = "currentTimeLabel";
            currentTimeLabel.Size = new Size(50, 20);
            currentTimeLabel.TabIndex = 6;
            currentTimeLabel.Text = "label1";
            currentTimeLabel.Click += currentTimeLabel_Click;
            // 
            // flagButton
            // 
            flagButton.Location = new Point(126, 637);
            flagButton.Margin = new Padding(3, 4, 3, 4);
            flagButton.Name = "flagButton";
            flagButton.Size = new Size(86, 31);
            flagButton.TabIndex = 7;
            flagButton.Text = "Add Flag";
            flagButton.UseVisualStyleBackColor = true;
            flagButton.Click += flagButton_Click;
            // 
            // lyricTextBox
            // 
            lyricTextBox.Enabled = false;
            lyricTextBox.Location = new Point(813, 47);
            lyricTextBox.Margin = new Padding(3, 4, 3, 4);
            lyricTextBox.Name = "lyricTextBox";
            lyricTextBox.ReadOnly = true;
            lyricTextBox.Size = new Size(507, 593);
            lyricTextBox.TabIndex = 8;
            lyricTextBox.Text = "";
            lyricTextBox.TextChanged += lyricTextBox_TextChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { FlagNumber, FlagName, FlagTime, ProgressBarValue });
            dataGridView1.Location = new Point(418, 49);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(390, 443);
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
            FlagNumber.Width = 120;
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
            deltaLabel.Location = new Point(114, 7);
            deltaLabel.Name = "deltaLabel";
            deltaLabel.Size = new Size(24, 32);
            deltaLabel.TabIndex = 11;
            deltaLabel.Text = "-";
            deltaLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // deltaTimeLabel
            // 
            deltaTimeLabel.AutoSize = true;
            deltaTimeLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            deltaTimeLabel.ForeColor = Color.Red;
            deltaTimeLabel.Location = new Point(193, 7);
            deltaTimeLabel.Name = "deltaTimeLabel";
            deltaTimeLabel.Size = new Size(24, 32);
            deltaTimeLabel.TabIndex = 12;
            deltaTimeLabel.Text = "-";
            deltaTimeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // treeView1
            // 
            treeView1.AllowDrop = true;
            treeView1.LabelEdit = true;
            treeView1.Location = new Point(3, 39);
            treeView1.Margin = new Padding(3, 4, 3, 4);
            treeView1.Name = "treeView1";
            treeView1.ShowNodeToolTips = true;
            treeView1.Size = new Size(399, 585);
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
            treePanel.Controls.Add(songsListBox);
            treePanel.Controls.Add(setlistListBox);
            treePanel.Controls.Add(textBox1);
            treePanel.Controls.Add(setlistDeleteButton);
            treePanel.Controls.Add(addSongsButton);
            treePanel.Controls.Add(newSetListButton);
            treePanel.Controls.Add(treeView1);
            treePanel.Location = new Point(0, 8);
            treePanel.Margin = new Padding(3, 4, 3, 4);
            treePanel.Name = "treePanel";
            treePanel.Size = new Size(409, 627);
            treePanel.TabIndex = 14;
            // 
            // songsListBox
            // 
            songsListBox.AllowDrop = true;
            songsListBox.FormattingEnabled = true;
            songsListBox.ItemHeight = 20;
            songsListBox.Location = new Point(176, 256);
            songsListBox.Margin = new Padding(3, 4, 3, 4);
            songsListBox.Name = "songsListBox";
            songsListBox.Size = new Size(217, 584);
            songsListBox.TabIndex = 20;
            songsListBox.MouseClick += setListSongsListBox_MouseClick;
            songsListBox.DragDrop += songsListBox_DragDrop;
            songsListBox.DragEnter += songsListBox_DragEnter;
            songsListBox.DragOver += songsListBox_DragOver;
            songsListBox.MouseDown += songsListBox_MouseDown;
            songsListBox.MouseMove += songsListBox_MouseMove;
            songsListBox.MouseUp += songsListBox_MouseUp;
            songsListBox.PreviewKeyDown += songsListBox_PreviewKeyDown;
            // 
            // setlistListBox
            // 
            setlistListBox.FormattingEnabled = true;
            setlistListBox.ItemHeight = 20;
            setlistListBox.Location = new Point(0, 256);
            setlistListBox.Margin = new Padding(3, 4, 3, 4);
            setlistListBox.Name = "setlistListBox";
            setlistListBox.Size = new Size(175, 584);
            setlistListBox.TabIndex = 19;
            setlistListBox.MouseClick += setlistListBox_MouseClick;
            setlistListBox.SelectedIndexChanged += setlistListBox_SelectedIndexChanged;
            setlistListBox.MouseDown += setlistListBox_MouseDown;
            setlistListBox.MouseUp += setlistListBox_MouseUp;
            setlistListBox.PreviewKeyDown += setlistListBox_PreviewKeyDown;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(279, 4);
            textBox1.Margin = new Padding(3, 4, 3, 4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(114, 27);
            textBox1.TabIndex = 18;
            // 
            // setlistDeleteButton
            // 
            setlistDeleteButton.Location = new Point(186, 4);
            setlistDeleteButton.Margin = new Padding(3, 4, 3, 4);
            setlistDeleteButton.Name = "setlistDeleteButton";
            setlistDeleteButton.Size = new Size(86, 31);
            setlistDeleteButton.TabIndex = 17;
            setlistDeleteButton.Text = "Delete Setlist";
            setlistDeleteButton.UseVisualStyleBackColor = true;
            setlistDeleteButton.Click += setlistDeleteButton_Click;
            // 
            // addSongsButton
            // 
            addSongsButton.Enabled = false;
            addSongsButton.Location = new Point(94, 4);
            addSongsButton.Margin = new Padding(3, 4, 3, 4);
            addSongsButton.Name = "addSongsButton";
            addSongsButton.Size = new Size(86, 31);
            addSongsButton.TabIndex = 15;
            addSongsButton.Text = "Add songs";
            addSongsButton.UseVisualStyleBackColor = true;
            addSongsButton.Click += addSongsButton_Click;
            // 
            // newSetListButton
            // 
            newSetListButton.Location = new Point(3, 4);
            newSetListButton.Margin = new Padding(3, 4, 3, 4);
            newSetListButton.Name = "newSetListButton";
            newSetListButton.Size = new Size(83, 31);
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
            controlPanel.Location = new Point(0, 669);
            controlPanel.Margin = new Padding(3, 4, 3, 4);
            controlPanel.Name = "controlPanel";
            controlPanel.Size = new Size(1344, 135);
            controlPanel.TabIndex = 15;
            // 
            // generateWaveLabel
            // 
            generateWaveLabel.AutoSize = true;
            generateWaveLabel.BackColor = SystemColors.MenuBar;
            generateWaveLabel.Font = new Font("Arial Narrow", 18F, FontStyle.Bold, GraphicsUnit.Point);
            generateWaveLabel.Location = new Point(498, 627);
            generateWaveLabel.Name = "generateWaveLabel";
            generateWaveLabel.Size = new Size(239, 35);
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
            panel1.Location = new Point(418, 500);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(296, 51);
            panel1.TabIndex = 16;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(-2, 7);
            label1.Name = "label1";
            label1.Size = new Size(106, 32);
            label1.TabIndex = 13;
            label1.Text = "Up Next:";
            // 
            // editingCheckBox
            // 
            editingCheckBox.AutoSize = true;
            editingCheckBox.Location = new Point(416, 16);
            editingCheckBox.Margin = new Padding(3, 4, 3, 4);
            editingCheckBox.Name = "editingCheckBox";
            editingCheckBox.Size = new Size(127, 24);
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
            fontButton.Location = new Point(813, 12);
            fontButton.Margin = new Padding(3, 4, 3, 4);
            fontButton.Name = "fontButton";
            fontButton.Size = new Size(113, 31);
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
            // setListContextMenu
            // 
            setListContextMenu.ImageScalingSize = new Size(20, 20);
            setListContextMenu.Items.AddRange(new ToolStripItem[] { addToolStripMenuItem, deleteToolStripMenuItem });
            setListContextMenu.Name = "contextMenuStrip1";
            setListContextMenu.Size = new Size(123, 52);
            setListContextMenu.Opening += contextMenuStrip1_Opening;
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.Size = new Size(122, 24);
            addToolStripMenuItem.Text = "Add";
            addToolStripMenuItem.Click += addToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(122, 24);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // songsContextMenu
            // 
            songsContextMenu.ImageScalingSize = new Size(20, 20);
            songsContextMenu.Items.AddRange(new ToolStripItem[] { addToolStripMenuItem1, deleteToolStripMenuItem1 });
            songsContextMenu.Name = "songsContextMenu";
            songsContextMenu.Size = new Size(123, 52);
            // 
            // addToolStripMenuItem1
            // 
            addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            addToolStripMenuItem1.Size = new Size(122, 24);
            addToolStripMenuItem1.Text = "Add";
            addToolStripMenuItem1.Click += addToolStripMenuItem1_Click;
            // 
            // deleteToolStripMenuItem1
            // 
            deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            deleteToolStripMenuItem1.Size = new Size(122, 24);
            deleteToolStripMenuItem1.Text = "Delete";
            deleteToolStripMenuItem1.Click += deleteToolStripMenuItem1_Click;
            // 
            // flagsContextStrip
            // 
            flagsContextStrip.ImageScalingSize = new Size(20, 20);
            flagsContextStrip.Items.AddRange(new ToolStripItem[] { addToolStripMenuItem2, deleteToolStripMenuItem2 });
            flagsContextStrip.Name = "flagsContextStrip";
            flagsContextStrip.Size = new Size(211, 80);
            // 
            // addToolStripMenuItem2
            // 
            addToolStripMenuItem2.Name = "addToolStripMenuItem2";
            addToolStripMenuItem2.Size = new Size(210, 24);
            addToolStripMenuItem2.Text = "Add";
            // 
            // deleteToolStripMenuItem2
            // 
            deleteToolStripMenuItem2.Name = "deleteToolStripMenuItem2";
            deleteToolStripMenuItem2.Size = new Size(210, 24);
            deleteToolStripMenuItem2.Text = "Delete";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1342, 852);
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
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Form1";
            KeyDown += Form1_KeyDown;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            treePanel.ResumeLayout(false);
            treePanel.PerformLayout();
            controlPanel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            setListContextMenu.ResumeLayout(false);
            songsContextMenu.ResumeLayout(false);
            flagsContextStrip.ResumeLayout(false);
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
        private ListBox songsListBox;
        private Label generateWaveLabel;
        private System.Windows.Forms.Timer flagTextCoolDown;
        private ContextMenuStrip setListContextMenu;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ContextMenuStrip songsContextMenu;
        private ToolStripMenuItem deleteToolStripMenuItem1;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem addToolStripMenuItem1;
        private ContextMenuStrip flagsContextStrip;
        private ToolStripMenuItem addToolStripMenuItem2;
        private ToolStripMenuItem deleteToolStripMenuItem2;
    }
}