using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if((textBox1.Text != String.Empty)|| (textBox2.Text != String.Empty) || (textBox3.Text != String.Empty))
            {
                try
                {
                    FileStream tok = new FileStream("seznam.dat", FileMode.OpenOrCreate, FileAccess.Write);
                    BinaryWriter zapis = new BinaryWriter(tok, Encoding.GetEncoding(1252));
                    string One_Mega_Write_String = (textBox2.Text + ";" + textBox3.Text + ";" + textBox1.Text);
                    zapis.Write(One_Mega_Write_String);
                    tok.Close();
                    zapis.Close();
                    
                }
                catch
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox5.Text != String.Empty)
            {
                listBox1.Items.Clear();
                FileStream tok2 = new FileStream("seznam.dat", FileMode.Open, FileAccess.Read);
                BinaryReader read = new BinaryReader(tok2, Encoding.GetEncoding(1252));
                tok2.Position = 0;
                int pocet_znamek = 0;
                int soucet_znamek = 0;
                while (tok2.Position < tok2.Length)
                {
                    string[] pole = read.ReadString().Split(';'); //pole[0] - pocet znamek, 1 - známky, 2 - jméno;
                    if (pole[2] == textBox5.Text)
                    {
                        if (pole[1].Contains(' '))
                        {
                            pole[1].Replace(' ', '\0');
                        }
                        string[] pole2 = pole[1].Split(',');
                        foreach (string thing in pole2)
                        {
                            string[] pole3 = thing.Split('-');
                            pocet_znamek += int.Parse(pole3[0]);
                            soucet_znamek += (int.Parse(pole3[1]) * int.Parse(pole3[0]));
                            listBox1.Items.Add(pole3[0]);
                        }


                        break;
                    }
                }
                if (pocet_znamek > 0)
                {
                    int prumer = soucet_znamek / pocet_znamek;
                    if (prumer == 1)
                    {
                        MessageBox.Show("Congratulation");
                    }
                    else if (prumer == 4)
                    {
                        MessageBox.Show("Warming!");
                    }
                    else if (prumer == 5)
                    {
                        textBox5.Text = "John Doe";
                    }
                }
                tok2.Close();
                read.Close();
            }
        }
    }
}
