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

namespace Dads_Audio_App
{
    public partial class Form1 : Form
    {
        WaveOutEvent outputDevice;
        MediaFoundationReader mediaPlayer;
        string mediaAudioFileLocation;
        TimeSpan songLength;
        bool isPlayingAudio = false;
        private AudioFileReader audioFileReader;
        private int flagCount = 0;
        public string fileName = "";
        private string songLengthFormatted;
        private string folderDirectory;
        private Dictionary<string, string[]> CSFD = new(); //CurrentSongFlagDictionary
        Bitmap image;
        Bitmap lineImage;
        Bitmap lineImageChangedColor;
        private Point mainLineLocation;
        private PictureBox mainLine;
        bool userDeletedRow = false;
        string nextFlagName = null;
        int nextFlagValue;
        int nextFlagTime;
        private string previousSelectedNodeName;
        private bool coolDown;
        private bool stillTyping;
        List<Control[]> flagControls = new List<Control[]>();
        private int draggingIndex;
        bool dragging;
        int xoffset;
        private PictureBox currentBehindWave;
        private string selectedSetList;
        private string selectedSong;
        private List<string[]> allFlagsInfo = new List<string[]>();
        private string currentSongName;
        private bool selectedSongLast;
        private bool flagTextIsTyping = false;
        private int selectedFlagTextIndex;


        //Must add selecting audio and moving that file into songs folder
        //Questions
        //Do you always select your setlist songs or do u want them to autoplay next song (playlist)?
        //Do you like flags with transparent red bars?
        //Do you want wave to move or have wave change color?

        public Form1()
        {
            InitializeComponent();
            firstTimeStartUp();
            string launch = AppDomain.CurrentDomain.BaseDirectory;
            image = new Bitmap(launch + "\\Flag.png");
            lineImage = new Bitmap(launch + "lineImage.png");
            image.MakeTransparent(Color.White);
            lineImage.MakeTransparent(Color.White);
            loadSetList();
            loadSetList2();
            createLine();
            editMode(false);
            dragging = false;
        }


        private void editMode(bool toggle)
        {
            dataGridView1.Visible = toggle;
            dataGridView1.Enabled = toggle;
            flagButton.Enabled = toggle;
            flagButton.Visible = toggle;
            newSetListButton.Enabled = toggle;
            newSetListButton.Visible = toggle;
            addSongsButton.Enabled = toggle;
            addSongsButton.Visible = toggle;
            setlistDeleteButton.Visible = toggle;
            setlistDeleteButton.Enabled = toggle;
            fontButton.Visible = toggle;
            lyricTextBox.ReadOnly = !toggle;
        }

        private void ApplyImageTransparency(Image img, float transparency)
        {
            // Create a ColorMatrix with the desired alpha (transparency) value
            ColorMatrix colorMatrix = new ColorMatrix
            {
                Matrix33 = transparency // Alpha channel (0.0 to 1.0)
            };

            // Create ImageAttributes and set the ColorMatrix
            ImageAttributes imgAttributes = new ImageAttributes();
            imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            // Create a new Bitmap to hold the transparent image
            Bitmap transparentBitmap = new Bitmap(img.Width, img.Height);

            // Use Graphics to apply the ColorMatrix
            using (Graphics g = Graphics.FromImage(transparentBitmap))
            {
                g.DrawImage(
                    img,
                    new Rectangle(0, 0, img.Width, img.Height), // Destination rectangle
                    0, 0, img.Width, img.Height,                 // Source rectangle
                    GraphicsUnit.Pixel,
                    imgAttributes);                              // Apply the transparency
            }

            // Set the PictureBox's image to the new transparent image
            lineImage = transparentBitmap;
        }


        private void createLine()
        {
            PictureBox line = new PictureBox();
            line.Image = lineImage;
            line.Width = 2;
            line.Height = 110;
            line.Location = new Point(audioTrackLocationProgressBar.Location.X, controlPanel.Location.Y + audioTrackLocationProgressBar.Location.Y - 25);
            line.SizeMode = PictureBoxSizeMode.Normal;
            this.Controls.Add(line);
            line.BringToFront();
            ApplyImageTransparency(lineImage, 0.7f);
            lineImageChangedColor = ChangeImageColorToBlue(lineImage);
            mainLineLocation = line.Location;
            mainLine = line;
        }
        private Bitmap ChangeImageColorToBlue(Bitmap originalImage)
        {
            Bitmap blueImage = new Bitmap(originalImage.Width, originalImage.Height);

            for (int y = 0; y < originalImage.Height; y++)
            {
                for (int x = 0; x < originalImage.Width; x++)
                {
                    // Get the pixel color from the original image
                    Color originalColor = originalImage.GetPixel(x, y);

                    int blue = (originalColor.R + originalColor.G + originalColor.B) / 3; // Use the average brightness of the original pixel
                    blueImage.SetPixel(x, y, Color.FromArgb(0, 0, blue)); // Set the new color as pure blue
                }
            }

            return blueImage;
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
            dataGridView1.Rows.Clear();
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
                    playSongV2(fromPlay, mediaAudioFileLocation); //V1
                    //mediaPlayer = new MediaFoundationReader(mediaAudioFileLocation);
                    //outputDevice.Init(mediaPlayer);
                    //audioTrackLocationProgressBar.Maximum = (int)mediaPlayer.TotalTime.TotalMilliseconds;
                    //fileName = Path.GetFileName(mediaAudioFileLocation);





                    ////openInfoFile(fileName);
                    //openInfoFile(fileName);


                    //timer1.Start();

                    //songLength = mediaPlayer.TotalTime;
                    //if (fromPlay)
                    //{
                    //    outputDevice.Play();
                    //    isPlayingAudio = true;
                    //}
                    //songLengthFormatted = mediaPlayer.TotalTime.ToString(@"mm\:ss");
                }
                catch
                {
                    MessageBox.Show("Invalid file format");
                }


            }
        }

        private void playSong(bool fromPlay, string songLocation)
        {
            mediaAudioFileLocation = songLocation;
            nextFlagName = null;
            lyricTextBox.Rtf = string.Empty;
            dataGridView1.Rows.Clear();
            controlPanel.Controls.Clear();
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
            mediaPlayer = new MediaFoundationReader(songLocation);
            outputDevice.Init(mediaPlayer);
            audioTrackLocationProgressBar.Maximum = (int)mediaPlayer.TotalTime.TotalMilliseconds;
            fileName = Path.GetFileName(songLocation);





            //openInfoFile(fileName);
            openInfoFile(fileName);


            timer1.Start();

            songLength = mediaPlayer.TotalTime;
            if (fromPlay)
            {
                outputDevice.Play();
                isPlayingAudio = true;
            }
            songLengthFormatted = mediaPlayer.TotalTime.ToString(@"mm\:ss");
        }

        private void firstTimeStartUp() //creates folder to store song info
        {
            try
            {
                folderDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mitchells Audio App");
                //folderDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\Mitchells Audio App";
                Directory.CreateDirectory(folderDirectory);
                Directory.CreateDirectory(folderDirectory + "\\Set Lists");
                Directory.CreateDirectory(folderDirectory + "\\Waves");
                Directory.CreateDirectory(folderDirectory + "\\Songs");
                Directory.CreateDirectory(folderDirectory + "\\SongInfo");
                Directory.CreateDirectory(folderDirectory + "\\ProgramInfo");
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

        public async Task createAndSaveWave(string location, string songName)
        {
            AveragePeakProvider averagePeakProvider = new AveragePeakProvider(4); // e.g. 4

            StandardWaveFormRendererSettings myRendererSettings = new StandardWaveFormRendererSettings();
            myRendererSettings.Width = audioTrackLocationProgressBar.Width;
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

            myRendererSettings2.Width = audioTrackLocationProgressBar.Width;
            myRendererSettings2.TopHeight = 32;
            myRendererSettings2.BottomHeight = 32;

            myRendererSettings2.BackgroundColor = Color.Transparent;
            myRendererSettings2.TopPeakPen = new Pen(Color.FromArgb(255, 107, 46));
            myRendererSettings2.BottomPeakPen = new Pen(Color.FromArgb(255, 164, 38));
            WaveFormRenderer renderer2 = new WaveFormRenderer();

            System.Drawing.Image playingImage = renderer2.Render(audioFilePath, averagePeakProvider, myRendererSettings2);

            string name = songName.Split('.')[0];

            if (!Directory.Exists(folderDirectory + "\\Waves\\" + name))
            {
                Directory.CreateDirectory(folderDirectory + "\\Waves\\" + name);
            }

            playingImage.Save(folderDirectory + "\\Waves\\" + name + "\\while_wave_" + name + ".png", ImageFormat.Png);
            afterImage.Save(folderDirectory + "\\Waves\\" + name + "\\after_wave_" + name + ".png", ImageFormat.Png);
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

            if (mediaPlayer.Position < mediaPlayer.Length && currentBehindWave != null)
            {
                var currentMilliseconds = (int)mediaPlayer.CurrentTime.TotalMilliseconds;
                audioTrackLocationProgressBar.Value = currentMilliseconds;

                float proportion = (float)audioTrackLocationProgressBar.Value / audioTrackLocationProgressBar.Maximum;
                int xLocation = (int)(proportion * audioTrackLocationProgressBar.Size.Width);

                currentBehindWave.Size = new Size(xLocation, currentBehindWave.Height);
                mainLine.Location = new Point(mainLineLocation.X + xLocation, mainLine.Location.Y);
            }

            currentTimeLabel.Text = mediaPlayer.CurrentTime.ToString(@"mm\:ss");

            if (CSFD.Count > 0 && (nextFlagName == null || audioTrackLocationProgressBar.Value > nextFlagValue))
            {
                foreach (var array in CSFD.Values)
                {
                    int flagValue = int.Parse(array[2]);
                    if (flagValue > audioTrackLocationProgressBar.Value)
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
            //flagCount++;
            //string flagCreationTime = mediaPlayer.CurrentTime.ToString(@"mm\:ss");
            //string value = audioTrackLocationProgressBar.Value.ToString(@"mm\:ss");

            //dataGridView1.Rows.Add(flagCount, $"flag {flagCount}", flagCreationTime, audioTrackLocationProgressBar.Value.ToString());
            //createFlagAt(audioTrackLocationProgressBar.Value, $"flag {flagCount}");
            //saveFile();
            flagCount++;
            allFlagsInfo.Add(new string[] { audioTrackLocationProgressBar.Value.ToString(), $"flag {flagCount}" });
            createFlagAt(audioTrackLocationProgressBar.Value, $"flag {flagCount}");
            saveFileV2(currentSongName);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void saveFile()
        {
            string[] splitString = Path.GetFileName(mediaAudioFileLocation).Split(".");
            fileName = splitString[0] + ".txt";
            string text = getFlagsFromTable();
            text = text + "$" + lyricTextBox.Rtf;
            System.IO.File.WriteAllText(folderDirectory + "\\SongInfo" + "\\" + fileName, text);
        }

        private string getFlagsFromTable()
        {
            List<string> flags = new List<string>();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string part1 = "";
                string part2 = "";
                string part3 = "";
                string part4 = "";

                try
                {
                    part1 = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    part2 = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    part3 = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    part4 = dataGridView1.Rows[i].Cells[3].Value.ToString();

                    string line = "^" + part1 + ";" + part2 + ";" + part3 + ";" + part4;

                    flags.Add(line);
                }
                catch
                {

                }

            };
            return String.Join("", flags);
        }



        private void openInfoFile(string songName) //checks for txt file with same name as song selected to load flag info
        {
            flagCount = 0;
            lyricTextBox.Enabled = true;
            fontButton.Enabled = true;

            getWave(songName);
            string[] splitString = Path.GetFileName(songName).Split(".");
            songName = splitString[0];
            string filePath = folderDirectory + "\\SongInfo" + "\\" + songName + ".txt";

            FileInfo fileinfo = new FileInfo(filePath);

            if (fileinfo.Exists)
            {
                //if (fileinfo.Length > 1)
                {
                    string info = System.IO.File.ReadAllText(filePath);
                    string[] infoSplit = info.Split('$');
                    string allFlags = infoSplit[0];
                    string allLyrics = infoSplit[1];
                    string[] flagBreakDown = allFlags.Split('^');
                    for (int i = 1; i < flagBreakDown.Count(); i++)
                    {
                        string[] split = flagBreakDown[i].Split(';');
                        CSFD.Add(split[0], new string[] { split[1], split[2], split[3] });
                    }
                    lyricTextBox.Rtf = allLyrics;
                    populateTableWithFlags();
                }
                //else
                {
                    saveFile();
                    flagCount = CSFD.Count();
                }
            }

        }

        private async void getWave(string songName)
        {
            generateWaveLabel.BringToFront();
            generateWaveLabel.Visible = true;
            string name = songName.Split('.')[0];
            if (Directory.Exists(folderDirectory + "\\Waves\\" + name))
            {
                System.IO.FileInfo whileWaveInfo = new FileInfo(folderDirectory + "\\Waves\\" + name + "\\while_wave_" + name + ".png");
                System.IO.FileInfo afterWaveInfo = new FileInfo(folderDirectory + "\\Waves\\" + name + "\\after_wave_" + name + ".png");
                if (whileWaveInfo.Exists && whileWaveInfo.Exists)
                {
                    loadAfterWave(afterWaveInfo.FullName);
                    loadWave(whileWaveInfo.FullName);
                }
                else
                {
                    await Task.Run(() => createAndSaveWave(folderDirectory + "\\Songs\\" + songName, songName));

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
                await Task.Run(() => createAndSaveWave(folderDirectory + "\\Songs\\" + songName, songName));

                System.IO.FileInfo whileWaveInfo = new FileInfo(folderDirectory + "\\Waves\\" + name + "\\while_wave_" + name + ".png");
                System.IO.FileInfo afterWaveInfo = new FileInfo(folderDirectory + "\\Waves\\" + name + "\\after_wave_" + name + ".png");
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
            audioTrackLocationProgressBar.Visible = false;
            pictureBox1.Location = audioTrackLocationProgressBar.Location;

            controlPanel.Controls.Add(pictureBox1);
            pictureBox1.SendToBack();
            currentBehindWave = pictureBox1;
            pictureBox1.Size = new Size(0, pictureBox1.Size.Height);
        }

        private void loadWave(string fullName)
        {
            Image wave = new Bitmap(fullName);
            PictureBox pictureBox1 = new PictureBox();
            pictureBox1.Size = wave.Size;
            pictureBox1.Image = wave;
            audioTrackLocationProgressBar.Visible = false;
            pictureBox1.Location = audioTrackLocationProgressBar.Location;

            controlPanel.Controls.Add(pictureBox1);
            pictureBox1.SendToBack();
            generateWaveLabel.Visible = false;
        }

        private void populateTableWithFlags()
        {
            for (int i = 1; i < CSFD.Count + 1; i++)
            {
                dataGridView1.Rows.Add(i, CSFD[i.ToString()][0], CSFD[i.ToString()][1], CSFD[i.ToString()][2]);
            }
            flagCount = CSFD.Count();
            createAllFlags();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //saveFile();
            saveFile();
        }

        private void createFlags(int position, string label)
        {
            int flagPicWidth = 16;
            int flagPicHeight = 15;
            int positionX = position - flagPicWidth / 2;
            int positionY = audioTrackLocationProgressBar.Location.Y - 17;
            //Text
            TextBox flagText = new TextBox();
            flagText.Text = label;
            flagText.Font = new Font("Arial", 13, FontStyle.Regular);
            flagText.BorderStyle = BorderStyle.None;


            // Create a Graphics object to measure the string size
            using (Graphics g = flagText.CreateGraphics())
            {
                SizeF stringSize = g.MeasureString(flagText.Text, flagText.Font);
                int offset = 0;

                if(stringSize.Width < 16)
                {
                    stringSize.Width = 0;
                    offset = 16;
                }
                // Set the TextBox size based on the measured text size
                flagText.Width = (int)stringSize.Width + offset;  // Adjust the padding as needed
                flagText.Height = (int)stringSize.Height;
            }
            flagText.Location = new Point(positionX, positionY - 1 * flagText.Size.Height);

            flagText.TextChanged += FlagText_TextChanged;

            PictureBox line = new PictureBox();
            line.Image = lineImageChangedColor;
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
            moveBtn.BackgroundImage = image;
            moveBtn.Size = new Size(flagPicWidth, flagPicHeight);
            moveBtn.GotFocus += (s, e) => moveBtn.Parent.Focus();

            moveBtn.MouseDown += MoveBtn_MouseDown;
            moveBtn.MouseMove += MoveBtn_MouseMove;
            moveBtn.MouseUp += MoveBtn_MouseUp;
            moveBtn.Capture = true;

            Control[] controls = new Control[3];

            controls[0] = flagText;
            controls[1] = line;
            controls[2] = moveBtn;

            flagControls.Add(controls);

            controlPanel.Controls.Add(flagText);
            controlPanel.Controls.Add(line);
            controlPanel.Controls.Add(moveBtn);


            flagText.BringToFront();
            line.BringToFront();
            moveBtn.BringToFront();


        }

        private void FlagText_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            textBox.Size = new Size(150,textBox.Size.Height);

            flagTextIsTyping = true;
            flagTextCoolDown.Start();
            selectedFlagTextIndex = flagControls.FindIndex(x => x[0].Text == textBox.Text);
        }

        private void MoveBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if (!dragging) { return; }
            dragging = false;
            int xLocation = flagControls[draggingIndex][2].Location.X;

            int PBLocX = audioTrackLocationProgressBar.Location.X;
            int PBSizeX = audioTrackLocationProgressBar.Size.Width;
            int PBmaxValue = audioTrackLocationProgressBar.Maximum;

            int relativeX = xLocation - PBLocX; // Get the relative x position on the progress bar
            float proportion = (float)relativeX / PBSizeX; // Proportion of the xLocation on the progress bar
            int PBValue = (int)(proportion * PBmaxValue); // Calculate the PBValue

            allFlagsInfo[draggingIndex][0] = PBValue.ToString();
            saveFileV2(currentSongName);
        }

        private void MoveBtn_MouseMove(object sender, MouseEventArgs e)
        {
            if (!dragging) return;

            Button b = (Button)sender;
            int Xmoved = e.Location.X - xoffset;

            if (Math.Abs(Xmoved) > 1) // Skip very small moves
            {
                if (flagControls[draggingIndex][1].Location.X + Xmoved <= audioTrackLocationProgressBar.Location.X || flagControls[draggingIndex][1].Location.X + Xmoved >= audioTrackLocationProgressBar.Location.X + audioTrackLocationProgressBar.Width)
                {
                    Xmoved = 0;
                }
                controlPanel.SuspendLayout();

                // Update locations of all the controls in the group (Label, PictureBox, Button)
                flagControls[draggingIndex][0].Location = new Point(flagControls[draggingIndex][0].Location.X + Xmoved, flagControls[draggingIndex][0].Location.Y);
                flagControls[draggingIndex][1].Location = new Point(flagControls[draggingIndex][1].Location.X + Xmoved, flagControls[draggingIndex][1].Location.Y);
                flagControls[draggingIndex][2].Location = new Point(flagControls[draggingIndex][2].Location.X + Xmoved, flagControls[draggingIndex][2].Location.Y);

                // Resume layout after update
                controlPanel.ResumeLayout();

                // Force immediate refresh of the panel to update display
                controlPanel.Refresh();

            }

        }

        private void MoveBtn_MouseDown(object sender, MouseEventArgs e)
        {
            if (editingCheckBox.Checked)
            {
                Button b;
                b = (Button)sender;
                draggingIndex = flagControls.FindIndex(x => x[2] == b);

                dragging = true;
                xoffset = e.X;
            }
            else
            {
                dragging = false;
            }


        }

        private void createAllFlags() //Determines were the progress bar is and therefore where to place flags 
        {

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string val = dataGridView1.Rows[i].Cells[3].Value.ToString();
                int PBcurrentValue = Int32.Parse(val);
                string flagLabel = dataGridView1.Rows[i].Cells[1].Value.ToString();
                createFlagAt(PBcurrentValue, flagLabel);
            }

        }


        private void createFlagAt(int PBValue, string flagLabel)
        {
            int PBLocX = audioTrackLocationProgressBar.Location.X;
            int PBSizeX = audioTrackLocationProgressBar.Size.Width;
            int PBmaxValue = audioTrackLocationProgressBar.Maximum;

            float proportion = (float)PBValue / PBmaxValue;
            int xLocation = PBLocX + (int)(proportion * PBSizeX);


            createFlags(xLocation, flagLabel);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        { //need to add if user deleted or code deleted check
            if (userDeletedRow)
            {
                userDeletedRow = false;
                for (int i = 0; i < e.RowCount; i++)
                {
                    if ((int)dataGridView1.Rows[i].Cells[0].Value > e.RowIndex + 1)
                    {
                        dataGridView1.Rows[i].Cells[0].Value = ((int)dataGridView1.Rows[i].Cells[0].Value - 1).ToString();
                    }
                }
                saveFile();
                CSFD.Clear();
                dataGridView1.Rows.Clear();
                controlPanel.Controls.Clear();
                openInfoFile(fileName);
            }
        }

        private void lyricTextBox_TextChanged(object sender, EventArgs e)
        {
            stillTyping = true;
            timer2.Start();
        }


        private void treeView1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void newSetListButton_Click(object sender, EventArgs e)
        {
            UserInputForm userInputForm = new UserInputForm();
            userInputForm.Text = "Add SetList";
            userInputForm.ShowDialog();
            string result = GlobalVariables.stringResult;
            if (result != null && result != "")
            {
                TreeNode newNode = new TreeNode(result);
                treeView1.Nodes.Add(newNode);
                treeView1.SelectedNode = newNode;
                saveSetList(result);

                //New Implement
                setlistListBox.Items.Add(result);
                saveSetList2(result);
            }

        }
        private void saveSetList2(string setListName) //This is for List Box
        {
            List<string> songs = new List<string>();
            for (int i = 0; i < setListSongsListBox.Items.Count; i++)
            {
                songs.Add(setListSongsListBox.Items[i].ToString());
                MessageBox.Show(setListSongsListBox.Items[i].ToString());
            }
            if (setListName != null || setListName != "")
            {
                System.IO.File.WriteAllLines(folderDirectory + "\\Set Lists" + "\\" + "SetList_" + setListName + ".txt", songs);
            }
        }

        private void saveSetList(string setListName)
        {
            List<string> songs = new List<string>();
            for (int i = 0; i < treeView1.SelectedNode.Nodes.Count; i++)
            {
                songs.Add(treeView1.SelectedNode.Nodes[i].Text);
            }

            if (setListName != null || setListName != "")
            {
                System.IO.File.WriteAllLines(folderDirectory + "\\Set Lists" + "\\" + "SetList_" + setListName + ".txt", songs);
            }
        }

        private void addSongsButton_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.InitialDirectory = folderDirectory + "\\Songs";
                dialog.Multiselect = true;
                DialogResult result = dialog.ShowDialog();
                List<string> selectedFiles = new List<string>();
                if (result == DialogResult.OK)
                {
                    selectedFiles = dialog.FileNames.Select(x => Path.GetFileName(x)).ToList();
                    foreach (var item in selectedFiles)
                    {
                        treeView1.SelectedNode.Nodes.Add(item);
                    }
                }
                treeView1.SelectedNode.ExpandAll();

                saveSetList(previousSelectedNodeName);
            }
            else
            {
                MessageBox.Show("Select SetList");
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            previousSelectedNodeName = e.Node.Text;
            if (treeView1.SelectedNode != null)
            {
                treeView1.SelectedNode.ExpandAll();
                if (treeView1.SelectedNode.Level > 0)
                {
                    playSongV2(false, folderDirectory + "\\Songs" + "\\" + treeView1.SelectedNode.Text); //V1
                    addSongsButton.Enabled = false;
                }
                else
                {
                    addSongsButton.Enabled = true;
                }

            }
        }

        private void treeView1_Leave(object sender, EventArgs e)
        {

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private void loadSetList()
        {
            string[] files = Directory.GetFiles(folderDirectory + "\\Set Lists");
            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                string name = fileName.Split("SetList_")[1].Split('.')[0];
                TreeNode newNode = new TreeNode(name);
                string[] songList = System.IO.File.ReadAllLines(file);
                foreach (string song in songList)
                {
                    newNode.Nodes.Add(song);
                }
                treeView1.Nodes.Add(newNode);
            }
        }

        private void loadSetList2() //For ListBox
        {
            string[] files = Directory.GetFiles(folderDirectory + "\\Set Lists");
            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                string name = fileName.Split("SetList_")[1].Split('.')[0];
                setlistListBox.Items.Add(name);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (!stillTyping)
            {
                saveFileV2(currentSongName);
                timer2.Stop();
            }
            stillTyping = false;

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Space && !coolDown && mediaPlayer != null & !lyricTextBox.Focused)
            {
                deltaLabel.Focus();
                mediaPlayer.CurrentTime = new TimeSpan(0);
                playButton.PerformClick();

                coolDown = true;
                coolDwon.Start();
            }
            if (e.KeyCode == Keys.K && !coolDown)
            {
                playButton.PerformClick();
                coolDown = true;
                coolDwon.Start();
            }
        }

        private void coolDwon_Tick(object sender, EventArgs e)
        {
            coolDown = false;
            coolDwon.Stop();
        }

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void treeView1_DragEnter_1(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            // Get the dragged node
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            // Get the position of the drop
            Point pt = treeView1.PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = treeView1.GetNodeAt(pt);

            // Check if the target node is valid and is within the same parent
            if (targetNode != null && draggedNode.Parent == targetNode.Parent)
            {
                TreeNode parentNode = draggedNode.Parent;

                // Remove the dragged node from the current position
                draggedNode.Remove();

                // Insert the dragged node at the target position
                int targetIndex = targetNode.Index;
                parentNode.Nodes.Insert(targetIndex, draggedNode);

                saveSetList(parentNode.Text);

            }
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

        private void treeView1_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node.Level > 0)
            {
                e.CancelEdit = true;
            }

        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node.Text != e.Label)
            {

                saveSetList(e.Label);
                System.IO.File.Delete(folderDirectory + "\\Set Lists" + "\\" + "SetList_" + e.Node.Text + ".txt");
            }
        }

        private void setlistDeleteButton_Click(object sender, EventArgs e)
        {
            //if (treeView1.SelectedNode != null)
            //{
            //    DialogResult result = MessageBox.Show("Are you sure you want to delete setlist: " + treeView1.SelectedNode.Text + "?",
            //                           "Confirm Deletion",
            //                           MessageBoxButtons.YesNo,
            //                           MessageBoxIcon.Warning);
            //    if (result == DialogResult.Yes)
            //    {
            //        System.IO.File.Delete(folderDirectory + "\\Set Lists" + "\\" + "SetList_" + treeView1.SelectedNode.Text + ".txt");
            //        treeView1.Nodes.Clear();
            //        loadSetList();
            //    }
            //}
            if (selectedSongLast != null)
            {
                string val = string.Empty;
                string val2 = string.Empty;
                if (!selectedSongLast)
                {
                    val = selectedSetList;
                    val2 = folderDirectory + "\\Set Lists" + "\\" + "SetList_" + val + ".txt";
                }
                else
                {

                    val = selectedSong;
                    val2 = folderDirectory + "\\Set Lists" + "\\" + "SetList_" + val + ".txt";
                }
                DialogResult result = MessageBox.Show("Are you sure you want to delete setlist: " + val + "?",
                       "Confirm Deletion",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    System.IO.File.Delete(val2);
                }
            }

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 || e.ColumnIndex == 1)
            {
                string time = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                string[] timeParts = time.Split(':');
                int minutes = int.Parse(timeParts[0]);
                int seconds = int.Parse(timeParts[1]);

                int totalMilliSeconds = (minutes * 60 * 1000) + (seconds * 1000);

                dataGridView1.Rows[e.RowIndex].Cells[3].Value = totalMilliSeconds;
            }
            //saveFile();
            saveFile();
            CSFD.Clear();
            dataGridView1.Rows.Clear();
            controlPanel.Controls.Clear();
            //openInfoFile(fileName);
            openInfoFile(fileName);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                userDeletedRow = true;
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            fontDialog1 = new FontDialog();
            fontDialog1.Font = lyricTextBox.Font;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                if (lyricTextBox.SelectedText.Length > 0)
                {
                    lyricTextBox.SelectionFont = fontDialog1.Font;
                }
                else
                {
                    lyricTextBox.Font = fontDialog1.Font;
                }
                saveFile();
            }

        }

        private void editingCheckBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void editingCheckBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {

        }

        private void setlistListBox_MouseClick(object sender, MouseEventArgs e)
        {
            int index = setlistListBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                setListSongsListBox.Items.Clear();
                string selectedItem = setlistListBox.Items[index].ToString();
                loadSongs(selectedItem);
                selectedSetList = selectedItem;
                selectedSongLast = false;
            }
        }

        private void loadSongs(string setListName)
        {
            string file = folderDirectory + "\\Set Lists" + "\\" + "SetList_" + setListName + ".txt";
            string[] songList = System.IO.File.ReadAllLines(file);
            foreach (string song in songList)
            {

                setListSongsListBox.Items.Add(song);
            }
        }

        private void setlistListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void setListSongsListBox_MouseClick(object sender, MouseEventArgs e)
        {
            int index = setListSongsListBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                string selectedItem = setListSongsListBox.Items[index].ToString();
                selectedSong = selectedItem;
                playSongV2(false, selectedItem);
                selectedSongLast = true;
            }
        }

        private void playSongV2(bool fromPlay, string songName)
        {

            allFlagsInfo.Clear();
            controlPanel.Controls.Clear();

            lyricTextBox.Enabled = true;
            fontButton.Enabled = true;
            string songNameNoExt = songName.Split(".")[0];
            currentSongName = songNameNoExt;


            nextFlagName = null;
            lyricTextBox.Rtf = string.Empty;

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
            mediaPlayer = new MediaFoundationReader(folderDirectory + "\\Songs" + "\\" + songName);
            outputDevice.Init(mediaPlayer);
            audioTrackLocationProgressBar.Maximum = (int)mediaPlayer.TotalTime.TotalMilliseconds;

            string songInfoFileDirect = folderDirectory + "\\SongInfo" + "\\" + songNameNoExt + ".txt";

            FileInfo fileinfo = new FileInfo(songInfoFileDirect);
            getWave(songName);

            if (fileinfo.Exists)
            {
                string info = System.IO.File.ReadAllText(songInfoFileDirect);
                string[] infoSplit = info.Split('$');
                string allFlags = infoSplit[0];
                string allLyrics = infoSplit[1];
                string[] flagBreakDown = allFlags.Split('^');

                for (int i = 1; i < flagBreakDown.Count(); i++)
                {
                    string[] split = flagBreakDown[i].Split(';');
                    allFlagsInfo.Add(new string[] { split[0], split[1] });
                }

                lyricTextBox.Rtf = allLyrics;
            }
            flagCount = allFlagsInfo.Count();
            createAllFlagsV2();
            timer1.Start();
        }

        private void createAllFlagsV2()
        {
            foreach (var flag in allFlagsInfo)
            {
                createFlagAt(int.Parse(flag[0]), flag[1]);
            }

        }

        private void saveFileV2(string songName)
        {
            string flagText = string.Empty;
            foreach (var flag in allFlagsInfo)
            {
                flagText += "^" + flag[0] + ";" + flag[1]; //flag[0] = name, flag[1] = PBvalue
            }

            string text = flagText + "$" + lyricTextBox.Rtf;
            System.IO.File.WriteAllText(folderDirectory + "\\SongInfo" + "\\" + songName + ".txt", text);
        }

        private void flagTextCoolDown_Tick(object sender, EventArgs e)
        {        
            
            if (!flagTextIsTyping)
            {
                allFlagsInfo[selectedFlagTextIndex][1] = flagControls[selectedFlagTextIndex][0].Text;
                saveFileV2(currentSongName);
                flagTextCoolDown.Stop();
                TextBox textBox = (TextBox)flagControls[selectedFlagTextIndex][0];

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

                textBox.SelectionStart = 0; // Move the caret to the end of the text
                textBox.ScrollToCaret();
            }
            flagTextIsTyping = false;
        }
    }
}