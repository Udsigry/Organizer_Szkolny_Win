using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ini;
using System.IO;
using Organizer_Szkolny.Properties;

namespace Organizer_Szkolny
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            StreamReader Read;
            try
            {
                Read = new StreamReader(new Form1().currentPath + @"/pliki/uczniowienazwy.txt");
                while (Read.Peek() >= 0)
                {
                    listBox1.Items.Add(Read.ReadLine());
                }
                Read.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex.Message));
                return;
            }
            try
            {
                Read = new StreamReader(new Form1().currentPath + @"/pliki/nauczycielenazwy.txt");
                while (Read.Peek() >= 0)
                {
                    listBox1.Items.Add(Read.ReadLine());
                }
                Read.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex.Message));
                return;
            }

            try
            {
                Read = new StreamReader(new Form1().currentPath + @"/pliki/uczniowiepliki.txt");
                while (Read.Peek() >= 0)
                {
                    listBox2.Items.Add(Read.ReadLine());
                }
                Read.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex.Message));
                return;
            }

            try
            {
                Read = new StreamReader(new Form1().currentPath + @"/pliki/nauczycielepliki.txt");
                while (Read.Peek() >= 0)
                {
                    listBox2.Items.Add(Read.ReadLine());
                }
                Read.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex.Message));
                return;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.SelectedIndex = listBox1.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IniFile klasa = new IniFile(new Form1().currentPath + @"/pliki/settings.ini");
            klasa.IniWriteValue("Plan", "kod", listBox2.Text);
            klasa.IniWriteValue("Plan", "nazwa", listBox1.Text);
            new Form1().LoadFiles();
            this.Close();

        }
    }
}
