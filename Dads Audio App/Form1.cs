using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio;
using NAudio.Utils;
using NAudio.WaveFormRenderer;
using System.Drawing.Imaging;
using System.Drawing;
using NAudio.Wave;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Emit;
using static System.Windows.Forms.LinkLabel;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Net.Mime.MediaTypeNames;
using Label = System.Windows.Forms.Label;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Security.Policy;
using Timer = System.Windows.Forms.Timer;
using System.IO.Packaging;
using TreeView = System.Windows.Forms.TreeView;
using Image = System.Drawing.Image;
using System.IO;
using Button = System.Windows.Forms.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using NAudio.CoreAudioApi;
using TextBox = System.Windows.Forms.TextBox;
using System.Diagnostics;
using System.Configuration;
using System.Reflection;


namespace Dads_Audio_App
{
    public partial class Form1 : Form
    {
        WaveOutEvent outputDevice;
        MediaFoundationReader mediaPlayer;
        string mediaAudioFileLocation;
        bool isPlayingAudio = false;
        private AudioFileReader audioFileReader;
        private int flagCount = 0;
        public string fileName = "";
        private string songLengthFormatted;
        private string folderDirectory;
        private Dictionary<string, string[]> CSFD = new(); //CurrentSongFlagDictionary
        Bitmap flagImage;
        Bitmap mainLineImage;
        Bitmap scrollFlagImage;
        Bitmap flagLineImage;
        Bitmap scrollIndicatorImage;
        private Point mainLineLocation;
        private PictureBox mainLine;
        bool userDeletedRow = false;
        string nextFlagName = string.Empty;
        int nextFlagValue;
        int nextFlagTime;
        private string previousSelectedNodeName;
        private bool coolDown;
        List<Control[]> flagControls = new List<Control[]>();
        List<Control[]> flagScrollControls = new List<Control[]>();
        private int draggingIndexMoveBtn;
        private int draggingIndexScrollBtn;
        private bool draggingScrollBtn;
        private int xoffsetScrollBtn;
        private int yoffsetScrollBtn;
        private int flagsButtonToDeleteIndex;
        bool draggingMoveBtn;
        private bool draggingMainLineButton;
        private int xoffsetMainLineButton;
        private int yoffsetMainLineButton;
        int xoffsetMoveBtn;
        private PictureBox currentBehindWave;
        private string selectedSetList;
        private int selectedSetListIndex;
        private string selectedSong;
        private List<string[]> allFlagsInfo = new List<string[]>();
        private string currentSongNameNoExt;
        private bool selectedSongLast;
        private bool flagTextIsTyping = false;
        private int selectedFlagTextIndex;
        private int yoffsetMoveBtn;
        private int _dragIndex;
        private bool _isDragging;
        private int _dragIndexSetList;
        private bool _isDraggingSetList;
        private List<string> currentSongList = new();
        private bool inSongSeach;
        private string songSearchString = string.Empty;
        private string setListSearchString = string.Empty;
        private List<string> setListList = new();
        private Button mainLineButton;
        private Point mainLineButtonLocation;
        private bool wasPlayingBeforeMove;
        private PictureBox currentInfrontWave;
        private int waveX;
        private int waveY;
        private bool fromSearch;
        private List<string[]> lyricScrollInfo = new List<string[]>();
        private bool scrollCoolDownBool;
        private int highlightedScrollStart;
        private int highlightedScrollLength;
        private List<(int CharIndex, Button Button)> scrollIndicators = new List<(int, Button)>();
        private ScrollableRichTextBox lyricTextBox = new ScrollableRichTextBox();
        private int tick_amt;
        private List<Theme> themesList = new List<Theme>();


        //Must add selecting audio and moving that file into songs folder
        //Questions
        //Do you always select your setlist songs or do u want them to autoplay next song (playlist)?
        //Do you like flags with transparent red bars?
        //Do you want wave to move or have wave change color?

        public Form1()
        {
            InitializeComponent();
            loadThemes();
            replaceLyricBox();
            this.WindowState = FormWindowState.Maximized; // Maximize the window
            createFolders();
            loadImages();
            loadSetList2();
            createMainLine();
            editMode(false);
            draggingMoveBtn = false;
            setListHeader.AutoSize = false;
            songsHeader.AutoSize = false;
            songSearchLabel.Text = "";
            setListSearchLabel.Text = "";
        }

        public class Theme
        {
            public Color backColorStrong { get; set; }
            public Color backColorWeak { get; set; }
            public Color foreColor { get; set; }
            public Color baseColor { get; set; }

            public Theme(Color backColorStrong, Color backColorWeak, Color foreColor, Color baseColor)
            {
                this.backColorStrong = backColorStrong;
                this.backColorWeak = backColorWeak;
                this.foreColor = foreColor;
                this.baseColor = baseColor;
            }
        }

        private void applyTheme(Theme theme)
        {
            this.BackColor = theme.baseColor;
            this.ForeColor = theme.foreColor;

            songsHeader.BackColor = theme.backColorStrong;
            songsHeader.ForeColor = theme.foreColor;
            setListHeader.BackColor = songsHeader.BackColor;
            setListHeader.ForeColor = songsHeader.ForeColor;

            setListListBox.BackColor = theme.backColorWeak;
            setListListBox.ForeColor = theme.foreColor;
            songsListBox.BackColor = setListListBox.BackColor;
            songsListBox.ForeColor = setListListBox.ForeColor;

            songsListBox.BorderStyle = BorderStyle.None;
            setListListBox.BorderStyle = BorderStyle.None;
            songsHeader.BorderStyle = BorderStyle.None;
            setListHeader.BorderStyle = BorderStyle.None;

            textPanel.BackColor = theme.baseColor;
            lyricTextBoxOutline.BackColor = theme.backColorWeak;

            //All Buttons
            addSongsButton.BackColor = theme.backColorWeak;
            addSongsButton.FlatStyle = FlatStyle.Popup;

            playButton.BackColor = theme.backColorWeak;
            playButton.FlatStyle = FlatStyle.Popup;

            saveScrollPos.BackColor = theme.backColorWeak;
            saveScrollPos.FlatStyle = FlatStyle.Popup;

            fontButton.BackColor = theme.backColorWeak;
            fontButton.FlatStyle = FlatStyle.Popup;

            setlistDeleteButton.BackColor = theme.backColorWeak;
            setlistDeleteButton.FlatStyle = FlatStyle.Popup;

            newSetListButton.BackColor = theme.backColorWeak;
            newSetListButton.FlatStyle = FlatStyle.Popup;

            flagButton.BackColor = theme.backColorWeak;
            flagButton.FlatStyle = FlatStyle.Popup;
        }


        private void replaceLyricBox()
        {
            lyricTextBox.BackColor = SystemColors.ButtonHighlight;
            lyricTextBox.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            lyricTextBox.Enabled = false;
            lyricTextBox.ShowSelectionMargin = true;
            lyricTextBox.AcceptsTab = false;
            lyricTextBox.Location = new Point(22, 4);
            lyricTextBox.Size = new Size(444, 446);
            textPanel.Controls.Add(lyricTextBox);
            lyricTextBox.BringToFront();
            lyricTextBox.Scrolled += (s, e) => UpdateScrollIndicatorPositions();
            lyricTextBox.Leave += lyricTextBox_Leave;
            lyricTextBox.MouseDown += lyricTextBox_MouseDown;
            lyricTextBox.KeyDown += lyricTextBox_KeyDown;
            lyricTextBox.PreviewKeyDown += lyricTextBox_PreviewKeyDown;

        }

        private void loadImages()
        {
            string launch = AppDomain.CurrentDomain.BaseDirectory;
            flagImage = new Bitmap(launch + "\\Flag.png");
            mainLineImage = new Bitmap(launch + "\\mainLineImage.png");
            scrollFlagImage = new Bitmap(launch + "\\ScrollFlag.png");
            flagLineImage = new Bitmap(launch + "\\flagLineImage.png");
            scrollIndicatorImage = new Bitmap(launch + "\\ScrollIndicator.png");
            flagImage.MakeTransparent(Color.White);
            scrollFlagImage.MakeTransparent(Color.White);
            scrollIndicatorImage.MakeTransparent(Color.White);
        }

        private void ListOutputDevices() //Not needed
        {
            int deviceCount = WaveOut.DeviceCount;
            for (int i = 0; i < deviceCount; i++)
            {
                var capabilities = WaveOut.GetCapabilities(i);
                string deviceName = capabilities.ProductName;
                MessageBox.Show($"Device {i}: {deviceName}");
            }
        }


        private void editMode(bool toggle)
        {
            newSetListButton.Enabled = toggle;
            newSetListButton.Visible = toggle;
            addSongsButton.Enabled = toggle;
            addSongsButton.Visible = toggle;
            setlistDeleteButton.Visible = toggle;
            setlistDeleteButton.Enabled = toggle;
            lyricTextBox.ReadOnly = !toggle;
            fontButton.Visible = toggle;
            if (selectedSong != null)
            {
                mainLineButton.Visible = toggle;
                flagButton.Enabled = toggle;
                flagButton.Visible = toggle;
                saveScrollPos.Visible = toggle;

                flagScrollControls.ForEach(controlArray => Array.ForEach(controlArray, control => control.Visible = toggle));
            }
            dummyButton.Focus();
        }


        private void createMainLine()
        {
            int flagPicWidth = 16;
            int flagPicHeight = 15;
            int positionX = audioBar.Location.X - flagPicWidth / 2 + 1;
            int positionY = controlPanel.Location.Y + audioBar.Location.Y + 75;

            mainLineButton = new Button();
            mainLineButton.Text = "";
            mainLineButton.BackColor = Color.Transparent;
            mainLineButton.FlatStyle = FlatStyle.Flat;
            mainLineButton.Location = new Point(positionX, positionY);
            mainLineButton.FlatAppearance.BorderSize = 0;
            mainLineButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            mainLineButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            mainLineButton.BackgroundImageLayout = ImageLayout.Zoom;
            mainLineButton.BackgroundImage = flagImage;
            mainLineButton.Size = new Size(flagPicWidth, flagPicHeight);
            mainLineButtonLocation = mainLineButton.Location;

            mainLineButton.MouseDown += mainLineButton_MouseDown;
            mainLineButton.MouseUp += mainLineButton_MouseUp;
            mainLineButton.MouseMove += mainLineButton_MouseMove;

            this.Controls.Add(mainLineButton);
            mainLineButton.Visible = false;

            PictureBox line = new PictureBox();
            line.Image = mainLineImage;
            line.Width = 2;
            line.Height = 110;
            line.Location = new Point(audioBar.Location.X, controlPanel.Location.Y + audioBar.Location.Y - 25);
            line.SizeMode = PictureBoxSizeMode.Normal;
            this.Controls.Add(line);
            line.BringToFront();
            mainLineLocation = line.Location;
            mainLine = line;
            mainLineButton.BringToFront();
        }



        private void playButton_Click(object sender, EventArgs e)
        {
            if (mediaPlayer != null)
            {
                if (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    outputDevice.Stop();
                }
                else
                {
                    outputDevice.Play();
                }
                dummyButton.Focus();
                timer1_Tick(this, EventArgs.Empty);

            }
            else
            {
                //selectAudioFileDialog(true);
            }

        }


        private void selectAudioDialogButton_Click(object sender, EventArgs e)
        {
            selectAudioFileDialog(false);
        }

        private void selectAudioFileDialog(bool fromPlay)
        {
            nextFlagName = null;
            lyricTextBox.Rtf = string.Empty;
            CSFD.Clear();
            if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
            {
                outputDevice.Stop();
                outputDevice.Dispose();
                isPlayingAudio = false;
            }

            if (outputDevice == null)
            {
                outputDevice = new WaveOutEvent();
            }
            if (selectMusicFileDialog.ShowDialog() == DialogResult.OK)
            {
                mediaAudioFileLocation = selectMusicFileDialog.FileName;
                try
                {
                    playSongV2(fromPlay, mediaAudioFileLocation);
                }
                catch
                {
                    MessageBox.Show("Invalid file format");
                }


            }
        }


        private void createFolders() //creates folder to store song info
        {
            try
            {
                //folderDirectory = @"C:\Users\mitch\AppData\Local\Mitchells Audio App";
                folderDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Mitchells Audio App");

                //folderDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mitchells Audio App");
                //folderDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\Mitchells Audio App";
                Directory.CreateDirectory(folderDirectory);
                Directory.CreateDirectory(folderDirectory + "\\Set Lists");
                Directory.CreateDirectory(folderDirectory + "\\Waves");
                Directory.CreateDirectory(folderDirectory + "\\Songs");
                Directory.CreateDirectory(folderDirectory + "\\SongInfo");
                Directory.CreateDirectory(folderDirectory + "\\ProgramInfo");
                if (!File.Exists(folderDirectory + "\\ProgramInfo" + "\\setListOrder.txt"))
                {
                    System.IO.File.Create(folderDirectory + "\\ProgramInfo" + "\\setListOrder.txt");
                }

            }
            catch
            {
                MessageBox.Show("Could not create folders needed. Make sure you are running with Elevated Privliages (Admin)");
                System.Windows.Forms.Application.Exit();
            }
        }


        private void waveViewer1_Load(object sender, EventArgs e)
        {

        }

        public async Task createAndSaveWave(string location, string songNameNoExt)
        {
            AveragePeakProvider averagePeakProvider = new AveragePeakProvider(4); // e.g. 4

            StandardWaveFormRendererSettings myRendererSettings = new StandardWaveFormRendererSettings();
            myRendererSettings.Width = audioBar.Width;
            myRendererSettings.TopHeight = 32;
            myRendererSettings.BottomHeight = 32;

            myRendererSettings.BackgroundColor = Color.Transparent;
            myRendererSettings.TopPeakPen = new Pen(Color.FromArgb(52, 52, 52)); //204, 204, 204)
            myRendererSettings.BottomPeakPen = new Pen(Color.FromArgb(154, 154, 154)); //255, 213, 199

            WaveFormRenderer renderer = new WaveFormRenderer();
            WaveStream audioFilePath = new MediaFoundationReader(location);

            System.Drawing.Image afterImage = renderer.Render(audioFilePath, averagePeakProvider, myRendererSettings);

            audioFilePath.Position = 0;

            StandardWaveFormRendererSettings myRendererSettings2 = new StandardWaveFormRendererSettings();

            myRendererSettings2.Width = audioBar.Width;
            myRendererSettings2.TopHeight = 32;
            myRendererSettings2.BottomHeight = 32;

            myRendererSettings2.BackgroundColor = Color.Transparent;
            myRendererSettings2.TopPeakPen = new Pen(Color.FromArgb(255, 107, 46));
            myRendererSettings2.BottomPeakPen = new Pen(Color.FromArgb(255, 164, 38));
            WaveFormRenderer renderer2 = new WaveFormRenderer();

            System.Drawing.Image playingImage = renderer2.Render(audioFilePath, averagePeakProvider, myRendererSettings2);

            if (!Directory.Exists(folderDirectory + "\\Waves\\" + songNameNoExt))
            {
                Directory.CreateDirectory(folderDirectory + "\\Waves\\" + songNameNoExt);
            }
            playingImage.Save(folderDirectory + "\\Waves\\" + songNameNoExt + "\\while_wave_" + songNameNoExt + ".png", ImageFormat.Png);
            afterImage.Save(folderDirectory + "\\Waves\\" + songNameNoExt + "\\after_wave_" + songNameNoExt + ".png", ImageFormat.Png);

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            playButton.Text = outputDevice.PlaybackState != PlaybackState.Playing ? "Play" : "Pause";

            //if (mediaPlayer.Position < mediaPlayer.Length)
            //{
            //    var currentMilliseconds = (int)mediaPlayer.CurrentTime.TotalMilliseconds;
            //    audioTrackLocationProgressBar.Value = currentMilliseconds;

            //    float proportion = (float)audioTrackLocationProgressBar.Value / audioTrackLocationProgressBar.Maximum;
            //    int xLocation = (int)(proportion * audioTrackLocationProgressBar.Size.Width);

            //    controlPanel.Location = new Point(-1 * xLocation, controlPanel.Location.Y);
            //}

            if (mediaPlayer.Position < mediaPlayer.Length && currentBehindWave != null && !draggingMainLineButton)
            {
                var currentMilliseconds = (int)mediaPlayer.CurrentTime.TotalMilliseconds;
                audioBar.Value = currentMilliseconds;

                float proportion = (float)audioBar.Value / audioBar.Maximum;
                int xLocation = (int)(proportion * audioBar.Size.Width);

                currentBehindWave.Size = new Size(xLocation, currentBehindWave.Height);
                mainLine.Location = new Point((int)(ClientSize.Width * 0.02) + xLocation, mainLine.Location.Y);
                mainLineButton.Location = new Point(mainLineButtonLocation.X + mainLineButton.Size.Width / 2 + xLocation, mainLineButton.Location.Y);
            }

            currentTimeLabel.Text = mediaPlayer.CurrentTime.ToString(@"mm\:ss") + " - " + mediaPlayer.TotalTime.ToString(@"mm\:ss");

            if (CSFD.Count > 0 && (nextFlagName == null || audioBar.Value > nextFlagValue))
            {
                foreach (var array in CSFD.Values)
                {
                    int flagValue = int.Parse(array[2]);
                    if (flagValue > audioBar.Value)
                    {
                        nextFlagName = array[0];
                        nextFlagValue = flagValue;
                        var timeParts = array[1].Split(':');
                        nextFlagTime = int.Parse(timeParts[0]) * 60 + int.Parse(timeParts[1]);

                        deltaLabel.Text = $"{nextFlagName}";
                        deltaTimeLabel.Text = (nextFlagTime - (int)mediaPlayer.CurrentTime.TotalSeconds).ToString();
                        break;
                    }
                }
            }

            int remainingTime = nextFlagTime - (int)mediaPlayer.CurrentTime.TotalSeconds;
            if (remainingTime > 0)
            {
                deltaTimeLabel.Text = remainingTime.ToString();
            }
            else
            {
                deltaLabel.Text = "-";
                deltaTimeLabel.Text = "-";
            }
            if (outputDevice != null)
            {
                if (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    int index = lyricScrollInfo.FindIndex(x => int.Parse(x[0]) <= audioBar.Value + 100 && int.Parse(x[0]) >= audioBar.Value - 100);
                    if (index != -1 && !scrollCoolDownBool)
                    {
                        scrollCoolDownBool = true;
                        scrollCoolDown.Start();
                        lyricTextBox.SelectionStart = int.Parse(lyricScrollInfo[index][2]);
                        lyricTextBox.ScrollToCaret();
                    }
                }
            }
        }


        private void audioLocationTrackBar_Scroll(object sender, EventArgs e)
        {

        }


        private void currentTimeLabel_Click(object sender, EventArgs e)
        {

        }

        private void selectMusicFileDialog_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {

        }

        private void flagButton_Click(object sender, EventArgs e)
        {
            if (selectedSong != null)
            {
                flagCount++;
                allFlagsInfo.Add(new string[] { audioBar.Value.ToString(), $"Flag {flagCount}" });
                createFlagAt(audioBar.Value, $"Flag {flagCount}", false);
                saveFileV2(currentSongNameNoExt);

            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void getWave(string songName)
        {
            generateWaveLabel.BringToFront();
            generateWaveLabel.Visible = true;
            string songNameNoExt = Path.GetFileNameWithoutExtension(songName);
            if (Directory.Exists(folderDirectory + "\\Waves\\" + songNameNoExt))
            {
                System.IO.FileInfo whileWaveInfo = new FileInfo(folderDirectory + "\\Waves\\" + songNameNoExt + "\\while_wave_" + songNameNoExt + ".png");
                System.IO.FileInfo afterWaveInfo = new FileInfo(folderDirectory + "\\Waves\\" + songNameNoExt + "\\after_wave_" + songNameNoExt + ".png");
                if (whileWaveInfo.Exists && whileWaveInfo.Exists)
                {
                    loadAfterWave(afterWaveInfo.FullName);
                    loadWave(whileWaveInfo.FullName);
                }
                else
                {
                    await Task.Run(() => createAndSaveWave(folderDirectory + "\\Songs\\" + songName, songNameNoExt));

                    try
                    {
                        loadAfterWave(afterWaveInfo.FullName);
                        loadWave(whileWaveInfo.FullName);
                    }
                    catch
                    {
                        MessageBox.Show("Error loading wave");
                    }
                }
            }
            else
            {
                await Task.Run(() => createAndSaveWave(folderDirectory + "\\Songs\\" + songName, songNameNoExt));

                System.IO.FileInfo whileWaveInfo = new FileInfo(folderDirectory + "\\Waves\\" + songNameNoExt + "\\while_wave_" + songNameNoExt + ".png");
                System.IO.FileInfo afterWaveInfo = new FileInfo(folderDirectory + "\\Waves\\" + songNameNoExt + "\\after_wave_" + songNameNoExt + ".png");
                try
                {
                    loadAfterWave(afterWaveInfo.FullName);
                    loadWave(whileWaveInfo.FullName);
                }
                catch
                {
                    MessageBox.Show("Error loading wave");
                }
            }
        }

        private void loadAfterWave(string fullName)
        {
            Image wave = new Bitmap(fullName);
            PictureBox pictureBox1 = new PictureBox();
            pictureBox1.Size = wave.Size;
            pictureBox1.Image = wave;
            audioBar.Visible = false;
            pictureBox1.Location = audioBar.Location;

            pictureBox1.DoubleClick += wavePic_DoubleClick;

            controlPanel.Controls.Add(pictureBox1);
            pictureBox1.SendToBack();
            currentBehindWave = pictureBox1;
            pictureBox1.Size = new Size(0, pictureBox1.Size.Height);
        }

        private void wavePic_DoubleClick(object? sender, EventArgs e)
        {
            if (editingCheckBox.Checked)
            {
                Control control = (Control)sender;
                Point mousePoint = this.PointToClient(MousePosition);

                if (mousePoint.X > audioBar.Location.X && mousePoint.X < audioBar.Location.X + audioBar.Width)
                {
                    int PBLocX = audioBar.Location.X;
                    int PBSizeX = audioBar.Size.Width;
                    int PBmaxValue = audioBar.Maximum;

                    int relativeX = mousePoint.X - PBLocX;
                    float proportion = (float)relativeX / PBSizeX;
                    int PBValue = (int)(proportion * PBmaxValue);

                    flagCount++;
                    allFlagsInfo.Add(new string[] { PBValue.ToString(), $"Flag {flagCount}" });
                    createFlagAt(PBValue, $"Flag {flagCount}", false);
                    saveFileV2(currentSongNameNoExt);
                }
            }
        }

        private int xLocationToPBValue(int xLoc)
        {
            int PBLocX = audioBar.Location.X;
            int PBSizeX = audioBar.Size.Width;
            int PBmaxValue = audioBar.Maximum;
            int relativeX = xLoc - PBLocX;
            float proportion = (float)relativeX / PBSizeX;
            int PBValue = (int)(proportion * PBmaxValue);
            if (PBValue < 300)
            {
                PBValue = 0;
            }
            return PBValue;
        }

        private void loadWave(string fullName)
        {
            Image wave = new Bitmap(fullName);
            currentInfrontWave = new PictureBox();
            currentInfrontWave.Size = wave.Size;
            currentInfrontWave.Image = wave;
            audioBar.Visible = false;
            currentInfrontWave.Location = audioBar.Location;

            currentInfrontWave.DoubleClick += wavePic_DoubleClick;

            waveX = wave.Size.Width;
            waveY = wave.Size.Height;

            controlPanel.Controls.Add(currentInfrontWave);
            currentInfrontWave.SendToBack();
            generateWaveLabel.Visible = false;
        }

        private void createFlags(int position, string label)
        {
            int flagPicWidth = 16;
            int flagPicHeight = 15;
            int positionX = position - flagPicWidth / 2;
            int positionY = audioBar.Location.Y - 17;
            //Text
            TextBox flagText = new TextBox();
            flagText.Text = label;
            flagText.Font = new Font("Arial", 13, FontStyle.Regular);
            //flagText.BorderStyle = BorderStyle.None;
            flagText.TextAlign = HorizontalAlignment.Center;

            // Create a Graphics object to measure the string size
            graphicsText(flagText);

            flagText.Location = new Point(positionX, positionY - 1 * flagText.Size.Height);

            flagText.TextChanged += FlagText_TextChanged;
            flagText.Enter += flagText_Enter;
            flagText.PreviewKeyDown += flagText_PreviewKeyDown;
            flagText.MouseDoubleClick += FlagText_MouseDoubleClick;
            flagText.MouseDown += FlagText_MouseDown;
            flagText.ContextMenuStrip = new ContextMenuStrip();

            PictureBox line = new PictureBox();
            line.Image = flagLineImage;
            line.Width = 1;
            line.Height = 90;
            line.Location = new Point(position, positionY);
            line.SizeMode = PictureBoxSizeMode.Normal;

            Button moveBtn = new Button();
            moveBtn.Text = "";
            moveBtn.BackColor = Color.Transparent;
            moveBtn.FlatStyle = FlatStyle.Flat;
            moveBtn.Location = new Point(positionX, positionY);
            moveBtn.FlatAppearance.BorderSize = 0;
            moveBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            moveBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            moveBtn.BackgroundImageLayout = ImageLayout.Zoom;
            moveBtn.BackgroundImage = flagImage;
            moveBtn.Size = new Size(flagPicWidth, flagPicHeight);

            Control[] controls = new Control[3];

            controls[0] = flagText;
            controls[1] = line;
            controls[2] = moveBtn;

            flagControls.Add(controls);

            controlPanel.Controls.Add(flagText);
            controlPanel.Controls.Add(line);
            controlPanel.Controls.Add(moveBtn);


            moveBtn.MouseDown += MoveBtn_MouseDown;
            moveBtn.MouseMove += MoveBtn_MouseMove;
            moveBtn.MouseUp += MoveBtn_MouseUp;


            flagText.BringToFront();
            line.BringToFront();
            moveBtn.BringToFront();

        }

        private void FlagText_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && editingCheckBox.Checked)
            {
                flagsContextStrip.Show(Cursor.Position);
                TextBox textBox = sender as TextBox;
                Control[] group = flagControls.FirstOrDefault(group => group[0] == textBox);
                Button b = (Button)group[2];
                flagsButtonToDeleteIndex = flagControls.FindIndex(x => x[2] == b);
            }
        }

        private void FlagText_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            if (editingCheckBox.Checked)
            {
                TextBox flagTextBox = (TextBox)sender;
                flagTextBox.SelectAll();
            }
        }

        private void createScrollFlag(int position, string label)
        {
            int flagPicWidth = 16;
            int flagPicHeight = 15;
            int positionX = position - flagPicWidth / 2;
            int positionY = audioBar.Location.Y + currentInfrontWave.Size.Height;
            //Text
            TextBox flagText = new TextBox();
            flagText.Text = label;
            flagText.Font = new Font("Arial", 13, FontStyle.Regular);
            flagText.BorderStyle = BorderStyle.None;


            // Create a Graphics object to measure the string size
            graphicsText(flagText);

            flagText.Location = new Point(positionX, positionY + flagPicHeight);
            flagText.ReadOnly = true;

            flagText.ContextMenuStrip = new ContextMenuStrip();

            //flagText.TextChanged += FlagText_TextChanged;
            //flagText.Enter += flagText_Enter;
            //flagText.PreviewKeyDown += flagText_PreviewKeyDown;

            PictureBox line = new PictureBox();
            line.Image = flagLineImage;
            line.Width = 1;
            line.Height = 90;
            line.Location = new Point(position, positionY - line.Height + flagPicHeight);
            line.SizeMode = PictureBoxSizeMode.Normal;

            Button moveBtn = new Button();
            moveBtn.Text = "";
            moveBtn.BackColor = Color.Transparent;
            moveBtn.FlatStyle = FlatStyle.Flat;
            moveBtn.Location = new Point(positionX, audioBar.Location.Y + currentInfrontWave.Size.Height);
            moveBtn.FlatAppearance.BorderSize = 0;
            moveBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            moveBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            moveBtn.BackgroundImageLayout = ImageLayout.Zoom;
            moveBtn.BackgroundImage = scrollFlagImage;
            moveBtn.Size = new Size(flagPicWidth, flagPicHeight);

            Control[] controls = new Control[3];

            controls[0] = flagText;
            controls[1] = line;
            controls[2] = moveBtn;

            flagScrollControls.Add(controls);

            controlPanel.Controls.Add(flagText);
            controlPanel.Controls.Add(line);
            controlPanel.Controls.Add(moveBtn);


            moveBtn.MouseDown += ScrollBtn_MouseDown;
            moveBtn.MouseMove += ScrollBtn_MouseMove;
            moveBtn.MouseUp += ScrollBtn_MouseUp;


            flagText.BringToFront();
            line.BringToFront();
            moveBtn.BringToFront();

        }

        private void ScrollBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if (!draggingScrollBtn) { return; }
            draggingScrollBtn = false;
            int xLocation = flagScrollControls[draggingIndexScrollBtn][1].Location.X + flagScrollControls[draggingIndexScrollBtn][1].Width;
            int PBValue = xLocationToPBValue(xLocation);
            lyricScrollInfo[draggingIndexScrollBtn][0] = PBValue.ToString();
            saveFileV2(currentSongNameNoExt);
        }

        private void ScrollBtn_MouseMove(object sender, MouseEventArgs e)
        {
            if (!draggingScrollBtn) return;

            Button b = (Button)sender;
            int Xmoved = e.Location.X - xoffsetScrollBtn;
            int Ymoved = e.Location.Y - yoffsetScrollBtn;

            if (Math.Abs(Xmoved) > 1) // Skip very small moves
            {
                if (flagScrollControls[draggingIndexScrollBtn][1].Location.X + Xmoved <= audioBar.Location.X || flagScrollControls[draggingIndexScrollBtn][1].Location.X + Xmoved >= audioBar.Location.X + audioBar.Width)
                {
                    Xmoved = 0;
                }
                //if(flagControls[draggingIndex][1].Location.Y + Ymoved >= currentBehindWave.Location.Y + currentBehindWave.Size.Height || flagControls[draggingIndex][1].Location.Y + Ymoved <= controlPanel.Location.Y + controlPanel.Size.Height)
                {
                    Ymoved = 0;
                }
                controlPanel.SuspendLayout();

                // Update locations of all the controls in the group (Label, PictureBox, Button)
                flagScrollControls[draggingIndexScrollBtn][0].Location = new Point(flagScrollControls[draggingIndexScrollBtn][0].Location.X + Xmoved, flagScrollControls[draggingIndexScrollBtn][0].Location.Y + Ymoved);
                flagScrollControls[draggingIndexScrollBtn][1].Location = new Point(flagScrollControls[draggingIndexScrollBtn][1].Location.X + Xmoved, flagScrollControls[draggingIndexScrollBtn][1].Location.Y + Ymoved);
                flagScrollControls[draggingIndexScrollBtn][2].Location = new Point(flagScrollControls[draggingIndexScrollBtn][2].Location.X + Xmoved, flagScrollControls[draggingIndexScrollBtn][2].Location.Y + Ymoved);

                // Resume layout after update
                controlPanel.ResumeLayout();

                // Force immediate refresh of the panel to update display
                controlPanel.Refresh();

            }
        }

        private void ScrollBtn_MouseDown(object sender, MouseEventArgs e)
        {
            if (editingCheckBox.Checked)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Button b;
                    b = (Button)sender;

                    draggingIndexScrollBtn = flagScrollControls.FindIndex(x => x[2] == b);
                    draggingScrollBtn = true;
                    xoffsetScrollBtn = e.X;
                    yoffsetScrollBtn = e.Y;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    Point mousePos = Control.MousePosition;
                    scrollContextStrip.Show(mousePos);
                    Button b = (Button)sender;
                    draggingIndexScrollBtn = flagScrollControls.FindIndex(x => x[2] == b);
                    draggingScrollBtn = false;
                }

            }
            else
            {
                draggingScrollBtn = false;
            }
        }

        private void flagText_PreviewKeyDown(object? sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                allFlagsInfo[selectedFlagTextIndex][1] = flagControls[selectedFlagTextIndex][0].Text;
                saveFileV2(currentSongNameNoExt);
                flagTextCoolDown.Stop();
                TextBox textBox = (TextBox)flagControls[selectedFlagTextIndex][0];
                textBox.TextAlign = HorizontalAlignment.Center;
                graphicsText(textBox);

                textBox.SelectionStart = 0; // Move the caret to the end of the text
                textBox.ScrollToCaret();
                dummyButton.Focus();
                flagTextIsTyping = false;
            }
        }

        private void flagText_Enter(object sender, EventArgs e)
        {
            if (!editingCheckBox.Checked)
            {
                this.ActiveControl = null;
            }
            else
            {
                flagTextIsTyping = true;
                flagTextCoolDown.Stop();
                flagTextCoolDown.Start();
            }
        }

        private void FlagText_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            textBox.Size = new Size(150, textBox.Size.Height);
            textBox.TextAlign = HorizontalAlignment.Left;
            flagTextIsTyping = true;
            flagTextCoolDown.Stop();
            flagTextCoolDown.Start();
            selectedFlagTextIndex = flagControls.FindIndex(x => x[0].Text == textBox.Text);
        }
        private void mainLineButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (!draggingMainLineButton) { return; }
            draggingMainLineButton = false;
            if (wasPlayingBeforeMove)
            {
                outputDevice.Play();
            }
        }

        private void MoveBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if (!draggingMoveBtn) { return; }
            draggingMoveBtn = false;
            int xLocation = flagControls[draggingIndexMoveBtn][1].Location.X + flagControls[draggingIndexMoveBtn][1].Width;
            int PBValue = xLocationToPBValue(xLocation);
            allFlagsInfo[draggingIndexMoveBtn][0] = PBValue.ToString();
            saveFileV2(currentSongNameNoExt);
        }
        private void mainLineButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (!draggingMainLineButton) return;

            Button b = (Button)sender;
            int Xmoved = e.Location.X - xoffsetMainLineButton;
            int Ymoved = e.Location.Y - yoffsetMainLineButton;

            if (Math.Abs(Xmoved) > 1) // Skip very small moves
            {
                if (mainLineButton.Location.X + Xmoved < audioBar.Location.X)
                {
                    // Restrict to the minimum position of the progress bar
                    Xmoved = audioBar.Location.X - mainLineButton.Location.X;
                }
                else if (mainLineButton.Location.X + Xmoved > audioBar.Location.X + audioBar.Width - mainLineButton.Width)
                {
                    // Restrict to the maximum position of the progress bar
                    Xmoved = (audioBar.Location.X + audioBar.Width) - mainLineButton.Location.X - mainLineButton.Width;
                }
                controlPanel.SuspendLayout();
                mainLineButton.Location = new Point(mainLineButton.Location.X + Xmoved, mainLineButton.Location.Y);
                mainLine.Location = new Point(mainLine.Location.X + Xmoved, mainLine.Location.Y);
                currentBehindWave.Size = new Size(currentBehindWave.Size.Width + Xmoved, currentBehindWave.Height);
                int PBValue = xLocationToPBValue(mainLine.Location.X);
                TimeSpan timespan = TimeSpan.FromMilliseconds(PBValue);
                mediaPlayer.CurrentTime = timespan;
                // Resume layout after update
                controlPanel.ResumeLayout();

                // Force immediate refresh of the panel to update display
                controlPanel.Refresh();

            }
        }
        private void MoveBtn_MouseMove(object sender, MouseEventArgs e)
        {
            if (!draggingMoveBtn) return;

            Button b = (Button)sender;
            int Xmoved = e.Location.X - xoffsetMoveBtn;
            int Ymoved = e.Location.Y - yoffsetMoveBtn;

            if (Math.Abs(Xmoved) > 1) // Skip very small moves
            {
                if (flagControls[draggingIndexMoveBtn][1].Location.X + Xmoved <= audioBar.Location.X || flagControls[draggingIndexMoveBtn][1].Location.X + Xmoved >= audioBar.Location.X + audioBar.Width)
                {
                    Xmoved = 0;
                }
                //if(flagControls[draggingIndex][1].Location.Y + Ymoved >= currentBehindWave.Location.Y + currentBehindWave.Size.Height || flagControls[draggingIndex][1].Location.Y + Ymoved <= controlPanel.Location.Y + controlPanel.Size.Height)
                {
                    Ymoved = 0;
                }
                controlPanel.SuspendLayout();

                // Update locations of all the controls in the group (Label, PictureBox, Button)
                flagControls[draggingIndexMoveBtn][0].Location = new Point(flagControls[draggingIndexMoveBtn][0].Location.X + Xmoved, flagControls[draggingIndexMoveBtn][0].Location.Y + Ymoved);
                flagControls[draggingIndexMoveBtn][1].Location = new Point(flagControls[draggingIndexMoveBtn][1].Location.X + Xmoved, flagControls[draggingIndexMoveBtn][1].Location.Y + Ymoved);
                flagControls[draggingIndexMoveBtn][2].Location = new Point(flagControls[draggingIndexMoveBtn][2].Location.X + Xmoved, flagControls[draggingIndexMoveBtn][2].Location.Y + Ymoved);

                // Resume layout after update
                controlPanel.ResumeLayout();

                // Force immediate refresh of the panel to update display
                controlPanel.Refresh();

            }

        }

        private void mainLineButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (outputDevice != null)
            {
                if (editingCheckBox.Checked)
                {
                    Button b;
                    b = (Button)sender;

                    draggingMainLineButton = true;
                    xoffsetMainLineButton = e.X;
                    yoffsetMainLineButton = e.Y;
                    if (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        outputDevice.Pause();
                        wasPlayingBeforeMove = true;
                    }
                    else
                    {
                        wasPlayingBeforeMove = false;
                    }
                }
                else
                {
                    draggingMainLineButton = false;
                }
            }
        }

        private void MoveBtn_MouseDown(object sender, MouseEventArgs e)
        {
            if (editingCheckBox.Checked)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Button b;
                    b = (Button)sender;

                    draggingIndexMoveBtn = flagControls.FindIndex(x => x[2] == b);
                    draggingMoveBtn = true;
                    xoffsetMoveBtn = e.X;
                    yoffsetMoveBtn = e.Y;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    Point mousePos = Control.MousePosition;
                    flagsContextStrip.Show(mousePos);
                    Button b = (Button)sender;
                    flagsButtonToDeleteIndex = flagControls.FindIndex(x => x[2] == b);
                    draggingMoveBtn = false;
                }

            }
            else
            {
                draggingMoveBtn = false;
            }


        }


        private void createFlagAt(int PBValue, string flagLabel, bool isScrollFlag)
        {
            int PBLocX = audioBar.Location.X;
            int PBSizeX = audioBar.Size.Width;
            int PBmaxValue = audioBar.Maximum;

            float proportion = (float)PBValue / PBmaxValue;
            int xLocation = PBLocX + (int)(proportion * PBSizeX);

            if (!isScrollFlag)
            {
                createFlags(xLocation, flagLabel);
            }
            else
            {
                createScrollFlag(xLocation, flagLabel);
            }
        }


        private void lyricTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void newSetListButton_Click(object sender, EventArgs e)
        {
            addSetList();
        }

        private string addSetList()
        {
            GlobalVariables.stringResult = null;
            UserInputForm userInputForm = new UserInputForm();
            userInputForm.Text = "Add SetList";
            userInputForm.ShowDialog();
            string result = GlobalVariables.stringResult;
            if (result != null && result != "")
            {
                //New Implement
                setListListBox.Items.Add(result);
                setListListBox.SelectedItem = result;
                songsListBox.Items.Clear();
                saveSetList2(result);
                setListList.Add(result);
                return result;
            }
            else
            {
                return "";
            }

        }

        private void saveSetList2(string setListName) //This is for List Box
        {
            List<string> songs = new List<string>();
            for (int i = 0; i < songsListBox.Items.Count; i++)
            {
                songs.Add(songsListBox.Items[i].ToString());
            }
            if (setListName != null || setListName != "")
            {
                List<string> setList = new();
                System.IO.File.WriteAllLines(folderDirectory + "\\Set Lists" + "\\" + "SetList_" + setListName + ".txt", songs);
                foreach (var item in setListListBox.Items)
                {
                    setList.Add(item.ToString());
                }
                System.IO.File.WriteAllLines(folderDirectory + "\\ProgramInfo\\setListOrder.txt", setList.ToArray());
            }
        }

        private void addSongsButton_Click(object sender, EventArgs e)
        {
            if (setListListBox.SelectedItem != null)
            {
                addSong();
            }
            else
            {
                MessageBox.Show("Select SetList");
            }
        }

        private void loadSetList2() //For ListBox
        {
            GlobalVariables.setLists.Clear();
            FileInfo info = new FileInfo(folderDirectory + "\\ProgramInfo" + "\\setListOrder.txt");
            if (info.Exists)
            {
                string[] setListOrder = System.IO.File.ReadAllLines(folderDirectory + "\\ProgramInfo" + "\\setListOrder.txt");
                foreach (var setList in setListOrder)
                {
                    setListListBox.Items.Add(setList);
                    GlobalVariables.setLists.Add(setList.ToLower());
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !coolDown && mediaPlayer != null && !lyricTextBox.Focused && !flagTextIsTyping/* && !songsListBox.Focused && !setListListBox.Focused*/)
            {
                setListListBox.SelectedItem = selectedSetList;
                songsListBox.SelectedItem = selectedSong;
                dummyButton.Focus();
                mediaPlayer.CurrentTime = new TimeSpan(0);
                playButton.PerformClick();
                coolDown = true;
                coolDwon.Start();
                songsListBox.Focus();
            }

            if (e.KeyCode == Keys.K && !coolDown && !songsListBox.Focused && !lyricTextBox.Focused && !setListListBox.Focused && !flagTextIsTyping)
            {
                playButton.PerformClick();
                coolDown = true;
                coolDwon.Start();
            }

            if (!editingCheckBox.Checked)
            {
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                {
                    if (!setListListBox.Focused)
                    {
                        songsListBox.Focus();
                    }
                    else
                    {
                        setListListBox.Focus();
                    }

                }
                if (e.KeyCode == Keys.Left)
                {
                    if (!setListListBox.Focused)
                    {
                        songsListBox.ClearSelected();
                        setListListBox.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Right)
                {
                    if (setListListBox.Focused)
                    {
                        string selectedItem = setListListBox.SelectedItem.ToString();
                        loadSongs(selectedItem);
                        selectedSetList = selectedItem;
                        selectedSetListIndex = setListListBox.SelectedIndex;
                        selectedSongLast = false;
                        songsListBox.Focus();
                    }
                    if (!songsListBox.Focused && !songsListBox.Focused)
                    {
                        songsListBox.Focus();
                    }

                }
            }


        }

        private void coolDwon_Tick(object sender, EventArgs e)
        {
            coolDown = false;
            coolDwon.Stop();
        }


        private void editingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (editingCheckBox.Checked)
            {
                editMode(true);
            }
            else
            {
                editMode(false);
            }
        }


        private void setlistDeleteButton_Click(object sender, EventArgs e)
        {
            if (setListListBox.SelectedItem != null && songsListBox.SelectedItem == null)
            {
                deleteSetList();
            }
            else if (setListListBox.SelectedItem != null && songsListBox.SelectedItem != null)
            {

                deleteSongs();
            }
            else
            {
                MessageBox.Show("Select Setlist to delete");
            }

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            fontDialog1 = new FontDialog();
            fontDialog1.ShowColor = true;
            fontDialog1.Font = lyricTextBox.SelectionFont;
            fontDialog1.Color = lyricTextBox.SelectionColor;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                if (lyricTextBox.SelectedText.Length > 0)
                {
                    lyricTextBox.SelectionFont = fontDialog1.Font;
                    lyricTextBox.SelectionColor = fontDialog1.Color;
                }
                else
                {
                    lyricTextBox.Font = fontDialog1.Font;
                }
                saveFileV2(currentSongNameNoExt);
            }

        }

        private void editingCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void editingCheckBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void setlistListBox_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void loadSongs(string setListName)
        {
            currentSongList.Clear();
            songsListBox.Items.Clear();
            string file = folderDirectory + "\\Set Lists" + "\\" + "SetList_" + setListName + ".txt";
            string[] songList = System.IO.File.ReadAllLines(file);
            foreach (string song in songList)
            {
                songsListBox.Items.Add(song);
                currentSongList.Add(song);
            }
        }
        private void loadSongsForSearch(string setListName)
        {
            string file = folderDirectory + "\\Set Lists" + "\\" + "SetList_" + setListName + ".txt";
            string[] songList = System.IO.File.ReadAllLines(file);
            foreach (string song in songList)
            {
                songsListBox.Items.Add(song);
            }
        }

        private void setlistListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void setListSongsListBox_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void playSongV2(bool fromPlay, string songName)
        {
            foreach (var (_, button) in scrollIndicators)
            {
                textPanel.Controls.Remove(button);
                button.Dispose();
            }
            scrollIndicators.Clear();
            allFlagsInfo.Clear();
            controlPanel.Controls.Clear();
            flagControls.Clear();
            lyricScrollInfo.Clear();
            flagScrollControls.Clear();
            string songNameNoExt = Path.GetFileNameWithoutExtension(songName);

            lyricTextBox.Enabled = true;
            fontButton.Enabled = true;
            currentSongNameNoExt = songNameNoExt;


            nextFlagName = string.Empty;
            lyricTextBox.Rtf = string.Empty;

            if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
            {
                outputDevice.Stop();
                outputDevice.Dispose();
                isPlayingAudio = false;
                audioBar.Value = 0;
                mediaPlayer.Dispose();
            }

            if (outputDevice == null)
            {
                outputDevice = new WaveOutEvent();
            }
            if (File.Exists(folderDirectory + "\\Songs" + "\\" + songName))
            {
                mediaPlayer = new MediaFoundationReader(folderDirectory + "\\Songs" + "\\" + songName);
                mediaPlayer = new MediaFoundationReader(folderDirectory + "\\Songs" + "\\" + songName);
                outputDevice.Init(mediaPlayer);
                audioBar.Maximum = (int)mediaPlayer.TotalTime.TotalMilliseconds;

                string songInfoFileDirect = folderDirectory + "\\SongInfo" + "\\" + songNameNoExt + ".txt";

                FileInfo fileinfo = new FileInfo(songInfoFileDirect);
                getWave(songName);

                if (fileinfo.Exists)
                {
                    string info = System.IO.File.ReadAllText(songInfoFileDirect);
                    string[] infoSplit = info.Split('$');
                    string allFlags = infoSplit[0];
                    string allLyrics = infoSplit[1];
                    string[] lyricScroll = infoSplit[2].Split('^');
                    string[] flagBreakDown = allFlags.Split('^');

                    for (int i = 1; i < flagBreakDown.Count(); i++)
                    {
                        string[] split = flagBreakDown[i].Split(';');
                        allFlagsInfo.Add(new string[] { split[0], split[1] });
                    }
                    for (int i = 1; i < lyricScroll.Count(); i++)
                    {
                        string[] split = lyricScroll[i].Split(';');
                        lyricScrollInfo.Add(new string[] { split[0], split[1], split[2] }); //[0]-progressbarvalue [1]-name [2]-scrollcaretvalue
                    }

                    lyricTextBox.Rtf = allLyrics;
                }
                flagCount = allFlagsInfo.Count();
                createAllFlagsV2();
                createAllScrollFlags();
                timer1.Start();
                tick_amt = 0;
                updateTimer.Start();
            }
            else
            {
                MessageBox.Show("Media file no longer exsits");
            }


            if (editingCheckBox.Checked)
            {
                editMode(true);
            }
            else
            {
                editMode(false);
            }
        }

        private void createAllScrollFlags()
        {
            foreach (var flag in lyricScrollInfo)
            {
                createFlagAt(int.Parse(flag[0]), flag[1], true);
                createScrollIndicator(int.Parse(flag[2]));
            }
        }

        private void createAllFlagsV2()
        {
            foreach (var flag in allFlagsInfo)
            {
                createFlagAt(int.Parse(flag[0]), flag[1], false);
            }

        }

        private void saveFileV2(string songName)
        {
            string flagText = string.Empty;
            string lyricScroll = string.Empty;
            foreach (var flag in allFlagsInfo)
            {
                flagText += "^" + flag[0] + ";" + flag[1]; //flag[0] = name, flag[1] = PBvalue
            }
            foreach (var scroll in lyricScrollInfo) //[0]-progressbarvalue [1]-name [2]-scrollcaretvalue [3] - fontHeightPx
            {
                lyricScroll += "^" + scroll[0] + ";" + scroll[1] + ";" + scroll[2];
            }
            string text = flagText + "$" + lyricTextBox.Rtf + "$" + lyricScroll;
            try
            {
                System.IO.File.WriteAllText(folderDirectory + "\\SongInfo" + "\\" + songName + ".txt", text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Save failed: " + ex.Message);
            }

        }

        private void flagTextCoolDown_Tick(object sender, EventArgs e)
        {
            if (!flagTextIsTyping || this.ActiveControl != flagControls[selectedFlagTextIndex][0])
            {
                allFlagsInfo[selectedFlagTextIndex][1] = flagControls[selectedFlagTextIndex][0].Text;
                saveFileV2(currentSongNameNoExt);
                flagTextCoolDown.Stop();
                TextBox textBox = (TextBox)flagControls[selectedFlagTextIndex][0];

                graphicsText(textBox);

                textBox.SelectionStart = 0; // Move the caret to the end of the text
                textBox.ScrollToCaret();
                textBox.TextAlign = HorizontalAlignment.Center;
                dummyButton.Focus();
            }
            flagTextIsTyping = false;

        }

        private void graphicsText(TextBox textBox)
        {
            using (Graphics g = textBox.CreateGraphics())
            {
                // Measure the size of the text
                SizeF stringSize = g.MeasureString(textBox.Text, textBox.Font);
                int offset = 0;
                if (stringSize.Width < 16)
                {
                    stringSize.Width = 0;
                    offset = 16;
                }

                textBox.Width = (int)stringSize.Width + offset;
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void setlistListBox_MouseDown(object sender, MouseEventArgs e)
        {
            songSearchString = "";
            songSearchLabel.Text = "";
            int index = setListListBox.IndexFromPoint(e.Location);
            _dragIndexSetList = index;
            if (index != ListBox.NoMatches && editingCheckBox.Checked)
            {
                _isDraggingSetList = true;
            }
            else
            {
                _isDraggingSetList = false;
            }

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteSetList();
        }

        private void songsListBox_MouseDown(object sender, MouseEventArgs e)
        {
            int index = songsListBox.IndexFromPoint(e.Location);
            _dragIndex = index;
            if (index != ListBox.NoMatches && editingCheckBox.Checked)
            {
                _isDragging = true;
            }
            else
            {
                _isDragging = false;
            }
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            deleteSongs();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addSetList();
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addSong();

        }

        private void setlistListBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //if(e.KeyCode == Keys.Up)
            //{
            //    if(setListListBox.SelectedIndex < setListListBox.Items.Count)
            //    {
            //        setListListBox.SelectedIndex = setListListBox.SelectedIndex + 1;
            //    }
            //}
            //if(e.KeyCode == Keys.Down)
            //{
            //    if (setListListBox.SelectedIndex > 0)
            //    {
            //        setListListBox.SelectedIndex = setListListBox.SelectedIndex - 1;
            //    }
            //}

            string selectedItem = setListListBox.SelectedItem?.ToString()?.ToLower();
            if (e.KeyCode == Keys.Delete /*|| e.KeyCode == Keys.Back*/)
            {
                if (setListListBox.SelectedItem != null)
                {
                    deleteSetList();
                }
            }
            else
            {
                if (e.KeyCode == Keys.Back && setListSearchString.Length > 0)
                {
                    setListSearchString = setListSearchString.Substring(0, setListSearchString.Length - 1);
                }
                else if (e.KeyCode.ToString().Length == 1)
                {
                    setListSearchString += e.KeyCode.ToString().ToLower();
                    setListSearchLabel.Visible = true;
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (setListListBox.SelectedItem != null)
                    {
                        string selectedItem2 = setListListBox.SelectedItem.ToString();
                        selectedSetList = selectedItem2;
                        selectedSetListIndex = setListListBox.SelectedIndex;
                        setListSearchLabel.Text = "";
                        setListSearchString = "";
                        songsListBox.Items.Clear();
                        loadSongs(selectedItem2);
                        selectedSongLast = false;
                        songsListBox.Focus();
                        if (songsListBox.Items.Count > 0)
                        {
                            songsListBox.SelectedIndex = 0;
                        }
                    }
                }
                if (setListSearchString.Length > 0)
                {
                    setListListBox.Items.Clear();
                    foreach (var setList in GlobalVariables.setLists)
                    {
                        if (setList.ToLower().StartsWith(setListSearchString.ToString().ToLower()))
                        {
                            setListListBox.Items.Add(setList);
                        }
                    }
                    setListSearchLabel.Text = setListSearchString;
                }
                else
                {
                    setListListBox.Items.Clear();
                    loadSetList2();
                    setListSearchLabel.Text = "";
                    setListSearchString = "";
                    setListSearchLabel.Visible = false;
                }

                if (selectedItem != null)
                {
                    foreach (var item in setListListBox.Items)
                    {
                        if (item.ToString().ToLower() == selectedItem)
                        {
                            setListListBox.SelectedItem = item;
                            break;
                        }
                    }
                }
                else if (setListListBox.Items.Count > 0)
                {
                    setListListBox.SelectedIndex = 0;
                }

            }
        }

        private void songsListBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            string selectedItem = songsListBox.SelectedItem?.ToString()?.ToLower();
            if (e.KeyCode == Keys.Delete/* || e.KeyCode == Keys.Back*/)
            {
                if (songsListBox.SelectedItem != null)
                {
                    deleteSongs();
                }
            }
            else if (setListListBox.SelectedItem != null)
            {
                if (e.KeyCode == Keys.Back && songSearchString.Length > 0)
                {
                    songSearchString = songSearchString.Substring(0, songSearchString.Length - 1);
                }
                else if (e.KeyCode.ToString().Length == 1)
                {
                    songSearchString += e.KeyCode.ToString().ToLower();
                    songSearchLabel.Visible = true;
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (songsListBox.SelectedItem != null)
                    {
                        int index = songsListBox.SelectedIndex;
                        string songToPlay = songsListBox.SelectedItem.ToString();
                        selectedSong = songToPlay;
                        songsListBox.Items.Clear();
                        loadSongsForSearch(setListListBox.SelectedItem.ToString());
                        songSearchLabel.Text = "";
                        songSearchString = "";
                        playSongV2(false, songToPlay);
                        fromSearch = true;
                        dummyButton.Focus();
                        songsListBox.Focus();
                    }
                }
                if (songSearchString.Length > 0)
                {
                    songsListBox.Items.Clear();
                    foreach (var song in currentSongList)
                    {
                        if (song.ToLower().StartsWith(songSearchString.ToString().ToLower()))
                        {
                            songsListBox.Items.Add(song);
                        }
                    }
                    songSearchLabel.Text = songSearchString;
                }
                else
                {
                    songsListBox.Items.Clear();
                    loadSongsForSearch(setListListBox.SelectedItem.ToString());
                    songSearchLabel.Text = "";
                    songSearchString = "";
                    songSearchLabel.Visible = false;
                }
            }

            if (selectedItem != null)
            {
                foreach (var item in songsListBox.Items)
                {
                    if (item.ToString().ToLower() == selectedItem)
                    {
                        songsListBox.SelectedItem = item;
                        break;
                    }
                }
            }
            else if (songsListBox.Items.Count > 0)
            {
                songsListBox.SelectedIndex = 0;
            }


        }


        private void deleteSongs()
        {
            string setList = setListListBox.SelectedItem.ToString();
            string songName = songsListBox.SelectedItem.ToString();
            DialogResult result = MessageBox.Show($"Are you sure you want to delete {songName} from setlist: " + setList + "?",
           "Confirm Deletion",
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                currentSongList.Remove(songName);
                songsListBox.Items.Remove(songsListBox.SelectedItem.ToString());
                saveSetList2(setList);

            }
        }
        private void deleteSetList()
        {
            string val = setListListBox.SelectedItem.ToString();
            string val2 = folderDirectory + "\\Set Lists" + "\\" + "SetList_" + val + ".txt";
            DialogResult result = MessageBox.Show("Are you sure you want to delete setlist " + val + "?",
           "Confirm Deletion",
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                System.IO.File.Delete(val2);
                setListListBox.Items.Remove(val);
                GlobalVariables.setLists.Remove(val.ToLower());
                songsListBox.Items.Clear();
                setListList.Remove(val);
            }
            saveSetListOrder();
        }

        private void saveSetListOrder()
        {
            List<string> setListItems = new();
            foreach (var setList in setListListBox.Items)
            {
                setListItems.Add(setList.ToString());
            }
            System.IO.File.WriteAllLines(folderDirectory + "\\ProgramInfo\\setListOrder.txt", setListItems.ToArray());
        }

        private void addSong()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = folderDirectory + "\\Songs";
            dialog.Multiselect = true;
            DialogResult result = dialog.ShowDialog();
            List<string> selectedFiles = new List<string>();
            if (result == DialogResult.OK)
            {
                selectedFiles = dialog.FileNames.ToList();
                var selectedFileNames = selectedFiles.Select(x => Path.GetFileName(x));
                if (!selectedFileNames.Any(fileName => songsListBox.Items.Contains(fileName)))
                {
                    foreach (var item in selectedFiles.Select(x => Path.GetFileName(x)))
                    {
                        if (songsListBox.SelectedIndex >= 0)
                        {
                            //If there is a song selected then add it above that one
                            int index = songsListBox.SelectedIndex;
                            songsListBox.Items.Insert(index, item);
                        }
                        else
                        {
                            //If none selected add to bottom of the list
                            songsListBox.Items.Add(item);
                        }
                    }
                    foreach (var item in dialog.FileNames)
                    {
                        string songDirect = Path.GetFullPath(item);
                        string correctDirect = folderDirectory + "\\Songs" + "\\" + Path.GetFileName(item);
                        if (!File.Exists(correctDirect))
                        {
                            File.Copy(songDirect, correctDirect, false);
                        }
                        //Change in final version
                        //if (songDirect != correctDirect)
                        //{
                        //    File.Move(songDirect, correctDirect);
                        //}
                    }
                    songsListBox.SelectedItem = Path.GetFileName(selectedFiles[0]);
                    saveSetList2(setListListBox.SelectedItem.ToString());
                }
                else
                {
                    List<string> didNotAddList = new List<string>();
                    didNotAddList = selectedFiles.Where(x => songsListBox.Items.Contains(Path.GetFileName(x))).ToList();
                    selectedFiles = selectedFiles.Where(x => !didNotAddList.Contains(x)).ToList();

                    if (selectedFiles.Count > 0)
                    {
                        foreach (var item in selectedFiles.Select(x => Path.GetFileName(x)))
                        {
                            songsListBox.Items.Add(item);
                        }
                        foreach (var item in dialog.FileNames)
                        {
                            string songDirect = Path.GetFullPath(item);
                            string correctDirect = folderDirectory + "\\Songs" + "\\" + Path.GetFileName(item);
                            if (songDirect != correctDirect)
                            {
                                File.Copy(songDirect, correctDirect);
                            }
                        }
                        saveSetList2(setListListBox.SelectedItem.ToString());
                    }

                    MessageBox.Show($"Did not add the following songs as they already exist in this setlist:\n\n-{string.Join("\n-", didNotAddList.Select(x => Path.GetFileName(x)))}", "Files not added");


                    songsListBox.SelectedItem = Path.GetFileName(didNotAddList[0]);
                }

            }


        }

        private void songsListBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void songsListBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (setListListBox.SelectedItem != null)
            {
                int index = songsListBox.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        string selectedItem = songsListBox.Items[index].ToString();
                        selectedSong = selectedItem;
                        playSongV2(false, selectedItem);
                        selectedSongLast = true;
                        songsListBox.Focus();
                    }
                    else if (e.Button == MouseButtons.Right && editingCheckBox.Checked)
                    {
                        songsContextMenu.Items[1].Visible = true;
                        songsListBox.SelectedIndex = index;
                        Point mousePos = Control.MousePosition;
                        songsContextMenu.Show(mousePos);
                    }
                }
                else if (e.Button == MouseButtons.Right && editingCheckBox.Checked && setListListBox.SelectedItem != null)
                {
                    songsContextMenu.Items[1].Visible = false;
                    Point mousePos = Control.MousePosition;
                    songsContextMenu.Show(mousePos);
                }
            }
        }

        private void songsListBox_DragDrop(object sender, DragEventArgs e)
        {
            Point point = songsListBox.PointToClient(new Point(e.X, e.Y));
            int index = songsListBox.IndexFromPoint(point);

            if (index < 0) index = songsListBox.Items.Count - 1;

            object data = e.Data.GetData(typeof(string));

            // Remove the dragged item and insert it at the new index
            songsListBox.Items.RemoveAt(_dragIndex);
            songsListBox.Items.Insert(index, data);
            saveSetList2(selectedSetList);
        }


        private void songsListBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging && e.Button == MouseButtons.Left)
            {
                songsListBox.DoDragDrop(songsListBox.Items[_dragIndex], DragDropEffects.Move);
            }
        }

        private void songsListBox_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void setlistListBox_MouseUp(object sender, MouseEventArgs e)
        {
            int index = setListListBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                if (e.Button == MouseButtons.Left)
                {
                    songsListBox.Items.Clear();
                    string selectedItem = setListListBox.Items[index].ToString();
                    loadSongs(selectedItem);
                    selectedSetList = selectedItem;
                    selectedSetListIndex = index;
                    selectedSongLast = false;

                }
                else if (e.Button == MouseButtons.Right && editingCheckBox.Checked)
                {
                    setListContextMenu.Items[1].Visible = true;
                    setListListBox.SelectedIndex = index;
                    Point mousePos = Control.MousePosition;
                    setListContextMenu.Show(mousePos);
                }
            }
            else if (e.Button == MouseButtons.Right && editingCheckBox.Checked)
            {

                setListContextMenu.Items[1].Visible = false;
                Point mousePos = Control.MousePosition;
                setListContextMenu.Show(mousePos);
            }
        }

        private void controlPanel_DoubleClick(object sender, MouseEventArgs e)
        {
            if (editingCheckBox.Checked)
            {

                Control control = (Control)sender;
                Point mousePoint = this.PointToClient(MousePosition);

                if (mousePoint.X > audioBar.Location.X && mousePoint.X < audioBar.Location.X + audioBar.Width)
                {
                    int PBLocX = audioBar.Location.X;
                    int PBSizeX = audioBar.Size.Width;
                    int PBmaxValue = audioBar.Maximum;

                    int relativeX = mousePoint.X - PBLocX;
                    float proportion = (float)relativeX / PBSizeX; // Proportion of the xLocation on the progress bar
                    int PBValue = (int)(proportion * PBmaxValue); // Calculate the PBValue

                    flagCount++;
                    allFlagsInfo.Add(new string[] { PBValue.ToString(), $"Flag {flagCount}" });
                    createFlagAt(PBValue, $"Flag {flagCount}", false);
                    saveFileV2(currentSongNameNoExt);
                }
            }
        }

        private void setlistListBox_DragDrop(object sender, DragEventArgs e)
        {
            Point point = setListListBox.PointToClient(new Point(e.X, e.Y));
            int index = setListListBox.IndexFromPoint(point);

            if (index < 0) index = setListListBox.Items.Count - 1;

            object data = e.Data.GetData(typeof(string));

            // Remove the dragged item and insert it at the new index
            setListListBox.Items.RemoveAt(_dragIndexSetList);
            setListListBox.Items.Insert(index, data);
            saveSetListOrder();
        }

        private void setListListBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDraggingSetList && e.Button == MouseButtons.Left)
            {
                setListListBox.DoDragDrop(setListListBox.Items[_dragIndexSetList], DragDropEffects.Move);
            }
        }

        private void setListListBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void setListListBox_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }


        private void setListListBox_MouseHover(object sender, EventArgs e)
        {

        }

        private void songsListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void flagsContextStrip_Opening(object sender, CancelEventArgs e)
        {

        }

        private void deleteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Control[] controlsToDelete = flagControls[flagsButtonToDeleteIndex];
            int PBValue = xLocationToPBValue(controlsToDelete[1].Location.X + controlsToDelete[1].Width);
            int beforeDeleteCount = allFlagsInfo.Count();

            allFlagsInfo.RemoveAll(x => x[1].ToString() == controlsToDelete[0].Text && x[0] == PBValue.ToString());

            if (beforeDeleteCount == allFlagsInfo.Count())
            {
                MessageBox.Show($"ERROR:\nNo flag with the PBValue: {PBValue}.\nOR\nNo Flag with name: {controlsToDelete[0].Text}");
            }
            for (int i = 0; i <= 2; i++)
            {
                Control controlToDelete = controlsToDelete[i];

                if (controlToDelete != null && controlToDelete.Parent != null)
                {
                    controlToDelete.Parent.Controls.Remove(controlToDelete);
                    controlToDelete.Dispose();
                }
            }
            flagControls.RemoveAt(flagsButtonToDeleteIndex);
            saveFileV2(currentSongNameNoExt);

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            setControlSizes();
        }

        private void setControlSizes()
        {
            setTreePanelAndChildrenSizes(new Point((int)(ClientSize.Width * 0.01), treePanel.Location.Y), new Size((int)(ClientSize.Width * 0.35), (int)(ClientSize.Height * 0.75)));
            setControlPanelAndChildrenSizes(new Point(0, (int)(ClientSize.Height * 0.80)), new Size(ClientSize.Width, controlPanel.Size.Height));
            setTextPanelSizes();
        }

        private void setTextPanelSizes()
        {
            textPanel.Size = new Size((int)(ClientSize.Width * 0.6), songsListBox.Size.Height + songsHeader.Size.Height / 2);
            textPanel.Location = new Point((int)(ClientSize.Width - textPanel.Size.Width - ((int)(ClientSize.Width * 0.02))), songsHeader.Location.Y + treePanel.Location.Y);
            lyricTextBox.Size = new Size(textPanel.Size.Width - 32, textPanel.Size.Height - 10);
            lyricTextBox.Location = new Point(22, 4);
            lyricTextBoxOutline.Size = new Size(lyricTextBox.Size.Width + 10, lyricTextBox.Size.Height + 10);
            fontButton.Location = new Point(textPanel.Location.X + lyricTextBoxOutline.Location.X, textPanel.Location.Y - fontButton.Size.Height - 4);
            saveScrollPos.Location = new Point(textPanel.Location.X + lyricTextBoxOutline.Location.X + fontButton.Size.Width + 4, fontButton.Location.Y);
        }

        private void setTreePanelAndChildrenSizes(Point treePanelLoc, Size treePanelSize)
        {
            treePanel.Location = treePanelLoc;
            treePanel.Size = treePanelSize;
            //SetList            
            setListListBox.Location = new Point(treePanel.Location.X, treePanel.Location.Y + addSongsButton.Height + songSearchLabel.Size.Height + setListHeader.Size.Height);
            setListListBox.Size = new Size((int)(treePanel.Width * 0.35), treePanel.Size.Height - 7 - addSongsButton.Height - songSearchLabel.Height - setListHeader.Size.Height);
            setListSearchLabel.Location = new Point(treePanel.Location.X, treePanel.Location.Y + addSongsButton.Height);
            setListHeader.Location = new Point(setListListBox.Location.X, setListListBox.Location.Y - setListHeader.Size.Height + 1);
            setListHeader.Size = new Size(setListListBox.Size.Width, setListHeader.Size.Height);
            //Songs
            songsListBox.Location = new Point(treePanel.Location.X + setListListBox.Width + 10, treePanel.Location.Y + addSongsButton.Height + songSearchLabel.Size.Height + songsHeader.Size.Height);
            songsListBox.Size = new Size((int)(treePanel.Width * 0.60), treePanel.Size.Height - addSongsButton.Height - songSearchLabel.Height - 7 - songsHeader.Size.Height);
            songSearchLabel.Location = new Point(songsListBox.Location.X, treePanel.Location.Y + addSongsButton.Height - 4);
            songsHeader.Location = new Point(songsListBox.Location.X, songsListBox.Location.Y - songsHeader.Size.Height + 1);
            songsHeader.Size = new Size(songsListBox.Size.Width, setListHeader.Size.Height);
            //Buttons
            newSetListButton.Location = new Point(treePanel.Location.X - 1, treePanel.Location.Y);
            addSongsButton.Location = new Point(newSetListButton.Location.X + newSetListButton.Size.Width + 4, treePanel.Location.Y);
            setlistDeleteButton.Location = new Point(addSongsButton.Location.X + addSongsButton.Width + 4, treePanel.Location.Y);
            editingCheckBox.Location = new Point(treePanel.Size.Width + 18 - editingCheckBox.Size.Width, treePanel.Location.Y + 10);
            generateWaveLabel.Location = new Point((int)(controlPanel.Size.Width - generateWaveLabel.Size.Width) / 2, treePanel.Location.Y);

        }
        private void setControlPanelAndChildrenSizes(Point controlPanelLocation, Size controlPanelSize)
        {
            if (mainLine != null)
            {
                controlPanel.Location = controlPanelLocation;
                controlPanel.Size = controlPanelSize;
                audioBar.Size = new Size((int)(controlPanelSize.Width * 0.958), audioBar.Height);
                audioBar.Location = new Point((int)(ClientSize.Width * 0.02), audioBar.Location.Y);
                mainLine.Location = new Point(audioBar.Location.X, controlPanel.Location.Y + audioBar.Location.Y - 25);
                mainLineButton.Location = new Point(audioBar.Location.X, controlPanel.Location.Y + audioBar.Location.Y + 75);
                currentTimeLabel.Location = new Point(audioBar.Location.X, controlPanelLocation.Y + controlPanelSize.Height);
                playButton.Location = new Point(audioBar.Location.X, controlPanelLocation.Y - 30);
                flagButton.Location = new Point(playButton.Location.X + playButton.Size.Width + 7, playButton.Location.Y);
                if (currentInfrontWave != null && currentInfrontWave != null)
                {
                    currentInfrontWave.Size = new Size(audioBar.Size.Width, yNewAspectRatio(audioBar.Size.Width, waveX, waveY));
                    //currentInfrontWave.SizeMode = PictureBoxSizeMode.StretchImage;
                    currentBehindWave.Size = currentInfrontWave.Size;
                    //currentBehindWave.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }
        public static int yNewAspectRatio(int newXWave, int xWave, int yWave)
        {
            int newYWave = (int)((newXWave * yWave) / xWave);
            return newYWave;
        }

        private void songsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lyricTextBox_Leave(object sender, EventArgs e)
        {
            saveFileV2(currentSongNameNoExt);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (songsListBox.SelectedItem != null && songsListBox.SelectedIndex > -1)
            {
                saveFileV2(currentSongNameNoExt);
            }
        }

        private void saveScrollPos_Click(object sender, EventArgs e)
        {
            if (currentSongNameNoExt != null && currentSongNameNoExt != "" && lyricTextBox.Text.Length > 0)
            {
                int caretIndex = lyricTextBox.SelectionStart;

                int count = lyricScrollInfo.Count + 1;
                string label = "Scroll " + count;

                Font caretFont = lyricTextBox.SelectionFont ?? lyricTextBox.Font;
                Point screenCaretTop = lyricTextBox.PointToScreen(lyricTextBox.GetPositionFromCharIndex(caretIndex));
                Point clientCaretTop = this.PointToClient(screenCaretTop);
                float dpiY = lyricTextBox.CreateGraphics().DpiY;
                float fontHeightPx = caretFont.SizeInPoints * dpiY / 72f;
                int centeredY = (int)(clientCaretTop.Y - textPanel.Location.Y + fontHeightPx / 2 - 8); // 8 is half the icon height
                Button scrollIndicatorButton = new Button();
                scrollIndicatorButton.Text = "";
                scrollIndicatorButton.BackColor = Color.Transparent;
                scrollIndicatorButton.FlatStyle = FlatStyle.Flat;
                scrollIndicatorButton.FlatAppearance.BorderSize = 0;
                scrollIndicatorButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
                scrollIndicatorButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
                scrollIndicatorButton.BackgroundImageLayout = ImageLayout.Zoom;
                scrollIndicatorButton.BackgroundImage = scrollIndicatorImage;
                scrollIndicatorButton.Size = new Size(16, 16);
                scrollIndicatorButton.Location = new Point(0, centeredY);

                textPanel.Controls.Add(scrollIndicatorButton);
                scrollIndicatorButton.BringToFront();

                scrollIndicators.Add((caretIndex, scrollIndicatorButton));

                lyricScrollInfo.Add(new string[] { audioBar.Value.ToString(), label, caretIndex.ToString() });
                createFlagAt(audioBar.Value, label, true);
                saveFileV2(currentSongNameNoExt);
            }
            if (lyricTextBox.Text.Length == 0)
            {
                MessageBox.Show("Cannot add position to scroll as text box is empty");
            }
        }

        private void UpdateScrollIndicatorPositions()
        {
            using (Graphics g = lyricTextBox.CreateGraphics())
            {
                float dpiY = g.DpiY;

                foreach (var (charIndex, button) in scrollIndicators)
                {
                    Font font = lyricTextBox.SelectionFont ?? lyricTextBox.Font;

                    // Get on-screen position of character
                    Point screenPos = lyricTextBox.PointToScreen(lyricTextBox.GetPositionFromCharIndex(charIndex));
                    Point clientPos = this.PointToClient(screenPos);

                    float fontHeight = font.SizeInPoints * dpiY / 72f;
                    int centeredY = (int)(clientPos.Y - textPanel.Location.Y + fontHeight / 2 - button.Height / 2);

                    button.Location = new Point(button.Location.X, centeredY);
                }
            }
            tick_amt = 0;
            updateTimer.Stop();
            updateTimer.Start();

        }

        private void scrollCoolDown_Tick(object sender, EventArgs e)
        {
            scrollCoolDownBool = false;
            scrollCoolDown.Stop();
        }

        private void songsListBox_Enter(object sender, EventArgs e)
        {

        }

        private void songsListBox_Leave(object sender, EventArgs e)
        {
        }

        private void deleteToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Control[] controlsToDelete = flagScrollControls[draggingIndexScrollBtn];
            int PBValue = xLocationToPBValue(controlsToDelete[1].Location.X + controlsToDelete[1].Width);

            var matchingItems = lyricScrollInfo.Where(x => x[1].ToString() == controlsToDelete[0].Text && x[0].ToString() == PBValue.ToString()).ToList();

            foreach (var item in matchingItems)
            {
                int target = int.Parse(item[2]);
                foreach (var scroll in scrollIndicators.ToList())
                {
                    if (scroll.CharIndex == target)
                    {
                        textPanel.Controls.Remove(scroll.Button);
                        scroll.Button.Dispose();
                        break;
                    }
                }
            }

            lyricScrollInfo.RemoveAll(x => x[1].ToString() == controlsToDelete[0].Text && x[0] == PBValue.ToString());

            for (int i = 0; i <= 2; i++)
            {
                Control controlToDelete = controlsToDelete[i];

                if (controlToDelete != null && controlToDelete.Parent != null)
                {
                    controlToDelete.Parent.Controls.Remove(controlToDelete);
                    controlToDelete.Dispose();
                }
            }

            flagScrollControls.RemoveAt(draggingIndexScrollBtn);
            saveFileV2(currentSongNameNoExt);
        }

        private void showScrollPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lyricTextBox.SelectionStart = int.Parse(lyricScrollInfo[draggingIndexScrollBtn][2]);
            lyricTextBox.ScrollToCaret();
            int caretIndex = lyricTextBox.SelectionStart;
            int line = lyricTextBox.GetLineFromCharIndex(caretIndex);
            try
            {
                highlightedScrollStart = lyricTextBox.GetFirstCharIndexFromLine(line);
                highlightedScrollLength = lyricTextBox.Lines[line].Length;
                lyricTextBox.Select(highlightedScrollStart, highlightedScrollLength);
                lyricTextBox.SelectionBackColor = Color.LightBlue;
                scrollBlinkTimer.Start();
            }
            catch
            {
                deleteToolStripMenuItem3_Click(null, EventArgs.Empty);
                MessageBox.Show("Scroll text has been deleted. Deleting scroll.");
            }
        }

        private void playButton_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void lyricTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && editingCheckBox.Checked)
            {
                Point mousePos = Control.MousePosition;
                lyricsBoxContextMenu.Show(mousePos);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SendKeys.Send("^v");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            fontButton.PerformClick();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            saveScrollPos.PerformClick();
        }

        private void scrollBlinkTimer_Tick(object sender, EventArgs e)
        {
            lyricTextBox.Select(highlightedScrollStart, highlightedScrollLength);
            lyricTextBox.SelectionBackColor = Color.White;
            scrollBlinkTimer.Stop();
        }

        private void songsListBox_MouseHover(object sender, EventArgs e)
        {

        }

        private void lyricTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!editingCheckBox.Checked)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void lyricTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                e.IsInputKey = true;
            }
        }

        private void lyricTextBox_MouseEnter(object sender, EventArgs e)
        {

        }

        private void setListListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            checkedSongs.Items.Clear();
            songsListBox.Items.Clear();
            Point screenPos = Cursor.Position;
            Point clientPos = this.PointToClient(screenPos);
            checkSongsPanel.Location = clientPos;
            checkSongsPanel.Visible = true;
            checkSongsPanel.BringToFront();
            string selectedItem = setListListBox.SelectedItem.ToString();
            loadSongs(selectedItem);
            selectedSetList = selectedItem;
            selectedSetListIndex = setListListBox.SelectedIndex;
            selectedSongLast = false;

            foreach (var song in songsListBox.Items)
            {
                checkedSongs.Items.Add(song, false);
            }

        }

        private void checkAllBox_CheckStateChanged(object sender, EventArgs e)
        {
            bool check = checkAllBox.Checked;

            for (int i = 0; i < checkedSongs.Items.Count; i++)
            {
                checkedSongs.SetItemChecked(i, check);
            }
        }

        private void checkCancelButton_Click(object sender, EventArgs e)
        {
            checkSongsPanel.Visible = false;
        }

        private void checkDoneButton_Click(object sender, EventArgs e)
        {
            string result = addSetList();

            if (result != "")
            {
                setListListBox.SelectedItem = result;
                foreach (var song in checkedSongs.CheckedItems)
                {
                    songsListBox.Items.Add(song.ToString());
                }
                saveSetList2(result);
            }

            checkSongsPanel.Visible = false;
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            int threshold = 1000;
            if (tick_amt == threshold)
            {
                updateTimer.Stop();
            }
            else
            {
                UpdateScrollIndicatorPositions();
                tick_amt += 1;
            }
        }

        private void createScrollIndicator(int caretIndex)
        {
            using (Graphics g = lyricTextBox.CreateGraphics())
            {
                Button scrollIndicatorButton = new Button();
                scrollIndicatorButton.Text = "";
                scrollIndicatorButton.BackColor = Color.Transparent;
                scrollIndicatorButton.FlatStyle = FlatStyle.Flat;
                scrollIndicatorButton.FlatAppearance.BorderSize = 0;
                scrollIndicatorButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
                scrollIndicatorButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
                scrollIndicatorButton.BackgroundImageLayout = ImageLayout.Zoom;
                scrollIndicatorButton.BackgroundImage = scrollIndicatorImage;
                scrollIndicatorButton.Size = new Size(16, 16);

                float dpiY = g.DpiY;

                Font font = lyricTextBox.SelectionFont ?? lyricTextBox.Font;

                // Get on-screen position of character
                Point screenPos = lyricTextBox.PointToScreen(lyricTextBox.GetPositionFromCharIndex(caretIndex));
                Point clientPos = this.PointToClient(screenPos);

                float fontHeight = font.SizeInPoints * dpiY / 72f;
                int centeredY = (int)(clientPos.Y - textPanel.Location.Y + fontHeight / 2 - scrollIndicatorButton.Height / 2);

                scrollIndicatorButton.Location = new Point(0, centeredY);

                textPanel.Controls.Add(scrollIndicatorButton);
                scrollIndicatorButton.BringToFront();

                scrollIndicators.Add((caretIndex, scrollIndicatorButton));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(themesList.Count > 0)
            {
                applyTheme(themesList[0]);
            }
        }

        private void loadThemes()
        {
            //foreach (var item in collection)
            //{
            //    themesList.Add(new Theme())
            //}

            themesList.Add(new Theme(Color.FromArgb(17, 21, 38), Color.FromArgb(24, 30, 54), Color.FromArgb(0, 126, 249), Color.FromArgb(46, 51, 73)));
        }
    }
}