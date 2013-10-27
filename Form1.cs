using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ini;

namespace Organizer_Szkolny
{
    public partial class Form1 : Form
    {

        public WebClient Download = new WebClient();
        public string currentPath = Directory.GetCurrentDirectory();
        public IniFile settings;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Download.DownloadFileCompleted += Download_DownloadFileCompleted;

            if (!Directory.Exists(currentPath + @"/pliki/"))
            {
                Directory.CreateDirectory(currentPath + @"/pliki/");
            }

                try
                {
                    Download.DownloadFile(new Uri("http://android.zs-1.edu.pl/zmiany.txt"), currentPath + @"/pliki/zmiany.txt");
                    Download.DownloadFile(new Uri("http://zs-1.pl/android/pliki/uczniowienazwy.txt"), currentPath + @"/pliki/uczniowienazwy.txt");

                }
                catch (Exception ex)
                {

                }

                try
                {
                    Download.DownloadFile(new Uri("http://zs-1.pl/android/pliki/uczniowiepliki.txt"), currentPath + @"/pliki/uczniowiepliki.txt");
                    Download.DownloadFile(new Uri("http://zs-1.pl/android/pliki/nauczycielenazwy.txt"), currentPath + @"/pliki/nauczycielenazwy.txt");
                    Download.DownloadFile(new Uri("http://zs-1.pl/android/pliki/nauczycielepliki.txt"), currentPath + @"/pliki/nauczycielepliki.txt");

                }
                catch (Exception ex)
                {

                }
                if (!File.Exists(currentPath + @"/pliki/settings.ini"))
                {
                    new Form1().Close();
                    new Form2().ShowDialog();
                }
                settings = new IniFile(currentPath + @"/pliki/settings.ini");
                LoadFiles();
                
                
            
        }

        private void Download_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            LoadFiles();
            throw new NotImplementedException();
            
        }


        public void LoadFiles()
        {
            try
            {
                string rtfText = System.IO.File.ReadAllText(currentPath + @"/pliki/zmiany.txt");
                label3.Text = rtfText;
                label4.Text = "Plan dla klasy: " + settings.IniReadValue("Plan", "nazwa");
                if (!File.Exists(currentPath + @"/pliki/" + settings.IniReadValue("Plan", "kod") + ".html"))
                {
                    Download.DownloadFile(new Uri("http://zs-1.pl/plany/plany/" + settings.IniReadValue("Plan", "kod") + ".html"), currentPath + @"/pliki/" + settings.IniReadValue("Plan", "kod") + ".html");
                    LoadFiles();
                }
                if (File.Exists(currentPath + @"/pliki/" + settings.IniReadValue("Plan", "kod") + ".html"))
                {
                    webBrowser1.Url = new Uri("file:///" + currentPath + @"/pliki/" + settings.IniReadValue("Plan", "kod") + ".html") ;
                    //webBrowser1.Refresh();
                    //webBrowser1.Update();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void klasaNauczycielToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form2().ShowDialog();  ;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            LoadFiles();
        }

        private void aktualizujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Aktualizuj();
        }

        public void Aktualizuj()
        {
            Download.DownloadFile(new Uri("http://zs-1.pl/plany/plany/" + settings.IniReadValue("Plan", "kod") + ".html"), currentPath + @"/pliki/" + settings.IniReadValue("Plan", "kod") + ".html");
            Download.DownloadFile(new Uri("http://zs-1.pl/android/pliki/uczniowiepliki.txt"), currentPath + @"/pliki/uczniowiepliki.txt");
            Download.DownloadFile(new Uri("http://zs-1.pl/android/pliki/nauczycielenazwy.txt"), currentPath + @"/pliki/nauczycielenazwy.txt");
            Download.DownloadFile(new Uri("http://zs-1.pl/android/pliki/nauczycielepliki.txt"), currentPath + @"/pliki/nauczycielepliki.txt");
            Download.DownloadFile(new Uri("http://android.zs-1.edu.pl/zmiany.txt"), currentPath + @"/pliki/zmiany.txt");
            Download.DownloadFile(new Uri("http://zs-1.pl/android/pliki/uczniowienazwy.txt"), currentPath + @"/pliki/uczniowienazwy.txt");
            LoadFiles();
        }

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Organizer Szkolny program stworzony dla Zespołu Szkół nr1 w Piekarach Śląskich http://android.zs-1.edu.pl/");
        }

        public void webClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs ex)
        {
            //zakończono pobieranie pliku
            LoadFiles();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadFiles();
        }


    }
}
