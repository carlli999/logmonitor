using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LogMonitorC
{
    public partial class Form1 : Form
    {
        string file;
        int size = -1;
        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = true;
            timer2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                file = openFileDialog1.FileName;
                textBox1.Text=Path.GetFileName(file);

                try {
                    FileStream stream = File.Open(file,FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
                    StreamReader reader = new StreamReader(stream, Encoding.Default);
                    textBox2.Text = reader.ReadToEnd();
                    textBox2.Text += DateTime.Now + "...";
                    textBox2.Focus();
                    textBox2.SelectionStart = textBox2.Text.Length;
                    textBox2.ScrollToCaret();
                    timer2.Enabled = true;
                }
                catch (IOException){
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("h:mm:ss tt");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader reader = new StreamReader(stream, Encoding.Default);
            textBox2.Text = reader.ReadToEnd();
            textBox2.Text += DateTime.Now + "...";
            textBox2.Focus();
            textBox2.SelectionStart = textBox2.Text.Length;
            textBox2.ScrollToCaret();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (timer2.Enabled==true) {
                button2.Text = "Start Scroll";
                timer1.Enabled = false;
                timer2.Enabled = false;
            } else {
                button2.Text = "Stop Scroll";
                timer1.Enabled = true;
                timer2.Enabled = true;
            }
        }
    }
}
