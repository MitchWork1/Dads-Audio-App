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
            GlobalVariables.stringResult = textBox1.Text;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GlobalVariables.stringResult = null;
            this.Close();
        }
    }
}
