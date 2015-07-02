using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinRotate
{
 
    public partial class Form1 : Form
    {
        

        public void Log(string s)
        {
            textBox3.AppendText("["+Convert.ToString(DateTime.Now)+"] "+ s + "\n");
        }

        public string[] textbox2list;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox2.AppendText(folderBrowserDialog1.SelectedPath+System.Environment.NewLine);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {       // заполняем список папками из тексбокса
                textbox2list = new string[textBox2.Lines.Length];
                textBox2.ReadOnly = true;
                string sum = "";
                Log("Starting");

               
                if (textBox2.Lines.Length < 1000)
                {
                    for (int i = 0; i < textBox2.Lines.Length; i++)
                    {
                        textbox2list[i] = textBox2.Lines[i];
                        sum += " " + textbox2list[i];
                    }

                    string textres = Convert.ToString(textBox2.Lines.Length);
                    //System.Windows.Forms.MessageBox.Show("Added " + sum + " strings!", "Info",
                    //System.Windows.Forms.MessageBoxButtons.OK,
                    //System.Windows.Forms.MessageBoxIcon.Information);

                }
                else System.Windows.Forms.MessageBox.Show("More than we can handle", "Error",
                           System.Windows.Forms.MessageBoxButtons.OK,
                           System.Windows.Forms.MessageBoxIcon.Information);

                // перебираем в цикле папки

                for (int i = 0; i < textBox2.Lines.Length; i++)
                {
                    toolStripProgressBar1.Maximum = textBox2.Lines.Length;
                    
                    if (!String.IsNullOrEmpty(textbox2list[i]))
                    {
                       Log("Working on folder " + textbox2list[i]);

                        string[] filepaths = System.IO.Directory.GetFiles(textbox2list[i]);
                        toolStripProgressBar1.Value = i;

                        for (int j = 0; j < filepaths.Length; j++)
                        {
                            if (System.IO.File.Exists(filepaths[j]) && (System.IO.File.GetCreationTime(filepaths[j])) < dateTimePicker1.Value)
                            {
                                try
                                {
                                    // удаляем файл
                                    Log("Trying to delete " + filepaths[j]);
                                    System.IO.File.Delete(filepaths[j]);
                                    // count++;
                                    // toolStripProgressBar1.Value = i;
                                    // label1.Text = "Deleting " + DelFile + " now!";
                                   Log("Deleted " + filepaths[j]);
                                }
                                catch (Exception ex)
                                {
                                    System.Windows.Forms.MessageBox.Show(ex.Message + " Problem:" + filepaths[j], "Error",
                                    System.Windows.Forms.MessageBoxButtons.OK,
                                    System.Windows.Forms.MessageBoxIcon.Warning);
                                    // 
                                }
                                
                            }
                        }
                    }
                    //------------------------ перебор внутри папки ------------------------------
                   
                    
                    // ----------------------- end of folder ---------------------------------
                }
                textBox2.ReadOnly = false;
                toolStripProgressBar1.Value = 0;
                Log("Done!");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + " Path:", "Error",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            Log("Cleared the box!");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today.AddDays(-30);
            Log("Set date to 30 days ago!");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today.AddDays(-7);
            Log("Set date to 7 days ago!");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today.AddDays(-5);
            Log("Set date to 5 days ago!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today.AddDays(-3);
            Log("Set date to 3 days ago!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today.AddDays(-2); 
            Log("Set date to 2 days ago!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today.AddDays(-1);
            Log("Set date to 1 days ago!");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today;
        }

        public void StartTimer()
        {
            timer1.Start();
            textBox2.ReadOnly = true;
            Log("Timer started for " + comboBox1.SelectedText + " and will ring at " + Convert.ToString(DateTime.Now.AddMilliseconds(timer1.Interval)));
            button11.Text = "Stop job!";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Log("Trying to start job...");
            if (timer1.Enabled == false)
            {
                Log("Checking if the timer is enabled: false ");
                
                    // 1000 = 1s
                    // 1h = 1s*60*60 = 3600000
                    // 1d = 1h*24 = 86400000
                    // 2d = 172800000
                    // 5d = 432000000
                    // 7d = 604800000
                    // 30d = 2592000000
                //Log(Convert.ToString(comboBox1.SelectedIndex));
                switch (comboBox1.SelectedIndex)
                {
                    case 1: timer1.Interval = 3600000; StartTimer(); break;
                    case 2: timer1.Interval = 86400000; StartTimer(); break;
                    case 3: timer1.Interval = 172800000; StartTimer(); break;
                    case 4: timer1.Interval = 432000000; StartTimer(); break;
                    case 5: timer1.Interval = 604800000; StartTimer(); break;
                    default: Log("No entry was selected! ");
                        break;
                }
                          
                
            }
            else
            {
                button11.Text = "Run this every";
                textBox2.ReadOnly = false;
                timer1.Stop();
                Log("Job stoped by user!");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddDays(-Convert.ToInt32(numericUpDown1.Value));
            button2.PerformClick();
            textBox2.ReadOnly = false;
            button11.Text = "Run this every";
            timer1.Enabled = false;
            Log("It is time! Job is done!");
            if (checkBox1.Checked)
            {
                button11.PerformClick();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Lines.Length > 4000)
            {
                textBox2.Clear();
            }
        }

       
       }
}
