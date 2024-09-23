using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dads_Audio_App
{
    public partial class UserInputForm : Form
    {
        public UserInputForm()
        {
            InitializeComponent();
            GlobalVariables.stringResult = null;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Form1.ActiveForm.Location.X + 25, Form1.ActiveForm.Location.Y + 100);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.Text == "Add SetList")
            {
                if (!GlobalVariables.setLists.Contains(textBox1.Text.ToLower()))
                {
                    GlobalVariables.setLists.Add(textBox1.Text.ToLower());
                    GlobalVariables.stringResult = textBox1.Text;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("There is already a setlist with that name!");
                }
            }
            else
            {
                GlobalVariables.stringResult = textBox1.Text;
                this.Close();
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            GlobalVariables.stringResult = null;
            this.Close();
        }

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
