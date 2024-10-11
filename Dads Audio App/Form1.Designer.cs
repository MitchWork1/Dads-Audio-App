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
            audioBar = new ProgressBar();
            timer1 = new System.Windows.Forms.Timer(components);
            currentTimeLabel = new Label();
            flagButton = new Button();
            lyricTextBox = new RichTextBox();
            deltaLabel = new Label();
            deltaTimeLabel = new Label();
            treePanel = new Panel();
            setListSearchLabel = new Label();
            songSearchLabel = new Label();
            songsListBox = new ListBox();
            setListHeader = new Label();
            setListListBox = new ListBox();
            songsHeader = new Label();
            setlistDeleteButton = new Button();
            addSongsButton = new Button();
            newSetListButton = new Button();
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
            deleteToolStripMenuItem2 = new ToolStripMenuItem();
            textPanel = new Panel();
            saveScrollPos = new Button();
            scrollCoolDown = new System.Windows.Forms.Timer(components);
            treePanel.SuspendLayout();
            controlPanel.SuspendLayout();
            panel1.SuspendLayout();
            setListContextMenu.SuspendLayout();
            songsContextMenu.SuspendLayout();
            flagsContextStrip.SuspendLayout();
            textPanel.SuspendLayout();
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
            // audioBar
            // 
            audioBar.ForeColor = SystemColors.MenuHighlight;
            audioBar.Location = new Point(29, 36);
            audioBar.Name = "audioBar";
            audioBar.Size = new Size(1101, 23);
            audioBar.TabIndex = 4;
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
            currentTimeLabel.Size = new Size(0, 15);
            currentTimeLabel.TabIndex = 6;
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
            flagButton.Visible = false;
            flagButton.Click += flagButton_Click;
            // 
            // lyricTextBox
            // 
            lyricTextBox.BackColor = SystemColors.ButtonHighlight;
            lyricTextBox.BorderStyle = BorderStyle.None;
            lyricTextBox.Enabled = false;
            lyricTextBox.Location = new Point(4, 4);
            lyricTextBox.Name = "lyricTextBox";
            lyricTextBox.ReadOnly = true;
            lyricTextBox.Size = new Size(444, 446);
            lyricTextBox.TabIndex = 8;
            lyricTextBox.Text = "";
            lyricTextBox.TextChanged += lyricTextBox_TextChanged;
            lyricTextBox.Leave += lyricTextBox_Leave;
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
            // treePanel
            // 
            treePanel.Controls.Add(setListSearchLabel);
            treePanel.Controls.Add(songSearchLabel);
            treePanel.Controls.Add(songsListBox);
            treePanel.Controls.Add(setListHeader);
            treePanel.Controls.Add(setListListBox);
            treePanel.Controls.Add(songsHeader);
            treePanel.Controls.Add(setlistDeleteButton);
            treePanel.Controls.Add(addSongsButton);
            treePanel.Controls.Add(newSetListButton);
            treePanel.Location = new Point(0, 6);
            treePanel.Name = "treePanel";
            treePanel.Size = new Size(409, 469);
            treePanel.TabIndex = 14;
            // 
            // setListSearchLabel
            // 
            setListSearchLabel.AutoSize = true;
            setListSearchLabel.BackColor = SystemColors.GradientActiveCaption;
            setListSearchLabel.BorderStyle = BorderStyle.FixedSingle;
            setListSearchLabel.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            setListSearchLabel.Location = new Point(5, 336);
            setListSearchLabel.Name = "setListSearchLabel";
            setListSearchLabel.Size = new Size(71, 27);
            setListSearchLabel.TabIndex = 22;
            setListSearchLabel.Text = "Search";
            setListSearchLabel.Visible = false;
            // 
            // songSearchLabel
            // 
            songSearchLabel.AutoSize = true;
            songSearchLabel.BackColor = SystemColors.GradientActiveCaption;
            songSearchLabel.BorderStyle = BorderStyle.FixedSingle;
            songSearchLabel.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            songSearchLabel.Location = new Point(157, 348);
            songSearchLabel.Name = "songSearchLabel";
            songSearchLabel.Size = new Size(71, 27);
            songSearchLabel.TabIndex = 21;
            songSearchLabel.Text = "Search";
            songSearchLabel.Visible = false;
            // 
            // songsListBox
            // 
            songsListBox.AllowDrop = true;
            songsListBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            songsListBox.FormattingEnabled = true;
            songsListBox.ItemHeight = 21;
            songsListBox.Location = new Point(157, 27);
            songsListBox.Name = "songsListBox";
            songsListBox.Size = new Size(249, 424);
            songsListBox.TabIndex = 20;
            songsListBox.MouseClick += setListSongsListBox_MouseClick;
            songsListBox.SelectedIndexChanged += songsListBox_SelectedIndexChanged;
            songsListBox.DragDrop += songsListBox_DragDrop;
            songsListBox.DragEnter += songsListBox_DragEnter;
            songsListBox.DragOver += songsListBox_DragOver;
            songsListBox.Enter += songsListBox_Enter;
            songsListBox.KeyDown += songsListBox_KeyDown;
            songsListBox.Leave += songsListBox_Leave;
            songsListBox.MouseDown += songsListBox_MouseDown;
            songsListBox.MouseMove += songsListBox_MouseMove;
            songsListBox.MouseUp += songsListBox_MouseUp;
            songsListBox.PreviewKeyDown += songsListBox_PreviewKeyDown;
            // 
            // setListHeader
            // 
            setListHeader.AutoSize = true;
            setListHeader.BorderStyle = BorderStyle.FixedSingle;
            setListHeader.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            setListHeader.Location = new Point(269, 278);
            setListHeader.Name = "setListHeader";
            setListHeader.Size = new Size(110, 34);
            setListHeader.TabIndex = 23;
            setListHeader.Text = "Set-Lists";
            setListHeader.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // setListListBox
            // 
            setListListBox.AllowDrop = true;
            setListListBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            setListListBox.FormattingEnabled = true;
            setListListBox.ItemHeight = 21;
            setListListBox.Location = new Point(3, 27);
            setListListBox.Name = "setListListBox";
            setListListBox.Size = new Size(154, 424);
            setListListBox.TabIndex = 19;
            setListListBox.MouseClick += setlistListBox_MouseClick;
            setListListBox.SelectedIndexChanged += setlistListBox_SelectedIndexChanged;
            setListListBox.DragDrop += setlistListBox_DragDrop;
            setListListBox.DragEnter += setListListBox_DragEnter;
            setListListBox.DragOver += setListListBox_DragOver;
            setListListBox.MouseDown += setlistListBox_MouseDown;
            setListListBox.MouseHover += setListListBox_MouseHover;
            setListListBox.MouseMove += setListListBox_MouseMove;
            setListListBox.MouseUp += setlistListBox_MouseUp;
            setListListBox.PreviewKeyDown += setlistListBox_PreviewKeyDown;
            // 
            // songsHeader
            // 
            songsHeader.AutoSize = true;
            songsHeader.BorderStyle = BorderStyle.FixedSingle;
            songsHeader.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            songsHeader.Location = new Point(238, 199);
            songsHeader.Name = "songsHeader";
            songsHeader.Size = new Size(85, 34);
            songsHeader.TabIndex = 22;
            songsHeader.Text = "Songs";
            songsHeader.TextAlign = ContentAlignment.MiddleCenter;
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
            // controlPanel
            // 
            controlPanel.Controls.Add(audioBar);
            controlPanel.Location = new Point(0, 502);
            controlPanel.Name = "controlPanel";
            controlPanel.Size = new Size(1176, 101);
            controlPanel.TabIndex = 15;
            controlPanel.MouseDoubleClick += controlPanel_DoubleClick;
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
            panel1.Location = new Point(436, 428);
            panel1.Name = "panel1";
            panel1.Size = new Size(259, 39);
            panel1.TabIndex = 16;
            panel1.Visible = false;
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
            editingCheckBox.Location = new Point(606, 13);
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
            // setListContextMenu
            // 
            setListContextMenu.ImageScalingSize = new Size(20, 20);
            setListContextMenu.Items.AddRange(new ToolStripItem[] { addToolStripMenuItem, deleteToolStripMenuItem });
            setListContextMenu.Name = "contextMenuStrip1";
            setListContextMenu.Size = new Size(150, 48);
            setListContextMenu.Opening += contextMenuStrip1_Opening;
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.Size = new Size(149, 22);
            addToolStripMenuItem.Text = "Add Set-List";
            addToolStripMenuItem.Click += addToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(149, 22);
            deleteToolStripMenuItem.Text = "Delete Set-List";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // songsContextMenu
            // 
            songsContextMenu.ImageScalingSize = new Size(20, 20);
            songsContextMenu.Items.AddRange(new ToolStripItem[] { addToolStripMenuItem1, deleteToolStripMenuItem1 });
            songsContextMenu.Name = "songsContextMenu";
            songsContextMenu.Size = new Size(164, 48);
            // 
            // addToolStripMenuItem1
            // 
            addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            addToolStripMenuItem1.Size = new Size(163, 22);
            addToolStripMenuItem1.Text = "Add Song/Songs";
            addToolStripMenuItem1.Click += addToolStripMenuItem1_Click;
            // 
            // deleteToolStripMenuItem1
            // 
            deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            deleteToolStripMenuItem1.Size = new Size(163, 22);
            deleteToolStripMenuItem1.Text = "Delete Song";
            deleteToolStripMenuItem1.Click += deleteToolStripMenuItem1_Click;
            // 
            // flagsContextStrip
            // 
            flagsContextStrip.ImageScalingSize = new Size(20, 20);
            flagsContextStrip.Items.AddRange(new ToolStripItem[] { deleteToolStripMenuItem2 });
            flagsContextStrip.Name = "flagsContextStrip";
            flagsContextStrip.Size = new Size(133, 26);
            flagsContextStrip.Opening += flagsContextStrip_Opening;
            // 
            // deleteToolStripMenuItem2
            // 
            deleteToolStripMenuItem2.Name = "deleteToolStripMenuItem2";
            deleteToolStripMenuItem2.Size = new Size(132, 22);
            deleteToolStripMenuItem2.Text = "Delete Flag";
            deleteToolStripMenuItem2.Click += deleteToolStripMenuItem2_Click;
            // 
            // textPanel
            // 
            textPanel.BackColor = SystemColors.ActiveCaption;
            textPanel.BorderStyle = BorderStyle.FixedSingle;
            textPanel.Controls.Add(lyricTextBox);
            textPanel.Location = new Point(711, 33);
            textPanel.Name = "textPanel";
            textPanel.Size = new Size(454, 456);
            textPanel.TabIndex = 20;
            // 
            // saveScrollPos
            // 
            saveScrollPos.Location = new Point(816, 9);
            saveScrollPos.Name = "saveScrollPos";
            saveScrollPos.Size = new Size(75, 23);
            saveScrollPos.TabIndex = 21;
            saveScrollPos.Text = "Save Scroll";
            saveScrollPos.UseVisualStyleBackColor = true;
            saveScrollPos.Visible = false;
            saveScrollPos.Click += saveScrollPos_Click;
            // 
            // scrollCoolDown
            // 
            scrollCoolDown.Interval = 1000;
            scrollCoolDown.Tick += scrollCoolDown_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1174, 639);
            Controls.Add(saveScrollPos);
            Controls.Add(textPanel);
            Controls.Add(generateWaveLabel);
            Controls.Add(fontButton);
            Controls.Add(editingCheckBox);
            Controls.Add(panel1);
            Controls.Add(currentTimeLabel);
            Controls.Add(treePanel);
            Controls.Add(flagButton);
            Controls.Add(playButton);
            Controls.Add(controlPanel);
            KeyPreview = true;
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            KeyDown += Form1_KeyDown;
            Resize += Form1_Resize;
            treePanel.ResumeLayout(false);
            treePanel.PerformLayout();
            controlPanel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            setListContextMenu.ResumeLayout(false);
            songsContextMenu.ResumeLayout(false);
            flagsContextStrip.ResumeLayout(false);
            textPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button playButton;
        private OpenFileDialog selectMusicFileDialog;
        private ProgressBar audioBar;
        private System.Windows.Forms.Timer timer1;
        private Label currentTimeLabel;
        private Button flagButton;
        private RichTextBox lyricTextBox;
        private Label deltaLabel;
        private Label deltaTimeLabel;
        private Panel treePanel;
        private Button newSetListButton;
        private Button addSongsButton;
        private Panel controlPanel;
        private System.Windows.Forms.Timer coolDwon;
        private Panel panel1;
        private Label label1;
        private CheckBox editingCheckBox;
        private Button setlistDeleteButton;
        private FontDialog fontDialog1;
        private Button fontButton;
        private ListBox setListListBox;
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
        private ToolStripMenuItem deleteToolStripMenuItem2;
        private Label songSearchLabel;
        private Label setListSearchLabel;
        private Panel textPanel;
        private Button saveScrollPos;
        private System.Windows.Forms.Timer scrollCoolDown;
        private Label songsHeader;
        private Label setListHeader;
    }
}