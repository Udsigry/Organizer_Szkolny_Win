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
        public int daynum = (int) new DateTime().DayOfWeek;

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
                label6.Text = nazwaDnia();
                label4.Text = "Plan dla klasy: " + settings.IniReadValue("Plan", "nazwa");
                if (daynum != 0 || daynum != 6)
                {
                    if (!File.Exists(currentPath + @"/pliki/" + settings.IniReadValue("Plan", "kod") + "-" + daynum + ".txt"))
                    {
                        Aktualizuj();
                    }
                    if (File.Exists(currentPath + @"/pliki/" + settings.IniReadValue("Plan", "kod") + "-" + daynum + ".txt"))
                    {
                        string plan = System.IO.File.ReadAllText(currentPath + @"/pliki/" + settings.IniReadValue("Plan", "kod") + "-" + daynum + ".txt");
                        label5.Text = plan;
                        ustawWielk();
                    }
                }
                else
                {
                    daynum = 1;
                    if (!File.Exists(currentPath + @"/pliki/" + settings.IniReadValue("Plan", "kod") + "-" + daynum + ".txt"))
                    {
                        Aktualizuj();
                    }
                    if (File.Exists(currentPath + @"/pliki/" + settings.IniReadValue("Plan", "kod") + "-" + daynum + ".txt"))
                    {
                        string plan = System.IO.File.ReadAllText(currentPath + @"/pliki/" + settings.IniReadValue("Plan", "kod") + "-" + daynum + ".txt");
                        label5.Text = plan;
                        ustawWielk();
                    }
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

        public void Aktualizuj()   //pobieranie wszystkich plikow
        {
            Download.DownloadFile(new Uri("http://zs-1.pl/android/pliki/" + settings.IniReadValue("Plan", "kod") + "-1.txt"), currentPath + @"/pliki/" + settings.IniReadValue("Plan", "kod") + "-1.txt");
            Download.DownloadFile(new Uri("http://zs-1.pl/android/pliki/" + settings.IniReadValue("Plan", "kod") + "-2.txt"), currentPath + @"/pliki/" + settings.IniReadValue("Plan", "kod") + "-2.txt");
            Download.DownloadFile(new Uri("http://zs-1.pl/android/pliki/" + settings.IniReadValue("Plan", "kod") + "-3.txt"), currentPath + @"/pliki/" + settings.IniReadValue("Plan", "kod") + "-3.txt");
            Download.DownloadFile(new Uri("http://zs-1.pl/android/pliki/" + settings.IniReadValue("Plan", "kod") + "-4.txt"), currentPath + @"/pliki/" + settings.IniReadValue("Plan", "kod") + "-4.txt");
            Download.DownloadFile(new Uri("http://zs-1.pl/android/pliki/" + settings.IniReadValue("Plan", "kod") + "-5.txt"), currentPath + @"/pliki/" + settings.IniReadValue("Plan", "kod") + "-5.txt");
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

        private void button2_Click(object sender, EventArgs e)          //gdy chcemy zobaczyc plan na poprzedni dzien tygodnia roboczego
        {
            if (daynum == 1)
            {
                daynum = 5;
            }
            else
            {
                daynum--;
            }
            label6.Text = nazwaDnia();
            if (!File.Exists(currentPath + @"/pliki/" + settings.IniReadValue("Plan", "kod") + "-" + daynum + ".txt"))
            {
                Aktualizuj();
            }
            if (File.Exists(currentPath + @"/pliki/" + settings.IniReadValue("Plan", "kod") + "-" + daynum + ".txt"))
            {
                string plan = System.IO.File.ReadAllText(currentPath + @"/pliki/" + settings.IniReadValue("Plan", "kod") + "-" + daynum + ".txt");
                label5.Text = plan;
                ustawWielk();
            }
        }

        private void button3_Click(object sender, EventArgs e)          //gdy chcemy zobaczyć plan na następny dzień tygodnia roboczego
        {
            if (daynum == 5)
            {
                daynum = 1;
            }
            else
            {
                daynum++;
            }
            label6.Text = nazwaDnia();
            if (!File.Exists(currentPath + @"/pliki/" + settings.IniReadValue("Plan", "kod") + "-" + daynum + ".txt"))
            {
                Aktualizuj();
            }
            if (File.Exists(currentPath + @"/pliki/" + settings.IniReadValue("Plan", "kod") + "-" + daynum + ".txt"))
            {
                string plan = System.IO.File.ReadAllText(currentPath + @"/pliki/" + settings.IniReadValue("Plan", "kod") + "-" + daynum + ".txt");
                label5.Text = plan;
                ustawWielk();
            }
        }

        public String nazwaDnia()                       //funkcja zwaracjaca nazwe dnia na podstawie numeru dnia
        {
            string dzien = "";
            if (daynum == 1)
            {
                dzien = "Poniedziałek";
            }
            else if (daynum == 2)
            {
                dzien = "Wtorek";
            }
            else if (daynum == 3)
            {
                dzien = "Środa";
            }
            else if (daynum == 4)
            {
                dzien = "Czwartek";
            }
            else if (daynum == 5)
            {
                dzien = "Piątek";
            }
            return (dzien);
        }

        public void ustawWielk() {
            short odlewej = 21;
            short margin = 20;

            int lokacjax = 0;
            lokacjax = odlewej + label3.Width + margin;
            button2.Location = new Point(
                 lokacjax,
                 button2.Location.Y
                 );
            lokacjax = lokacjax + margin + button2.Width + margin;
            label4.Location = new Point(
                 lokacjax,
                 label4.Location.Y
                 );
            label5.Location = new Point(
                 lokacjax,
                 label5.Location.Y
                 );
            label6.Location = new Point(
                 lokacjax,
                 label6.Location.Y
                 );
            lokacjax = lokacjax + margin + label5.Width + margin;
            button3.Location = new Point(
                 lokacjax,
                 button3.Location.Y
                 );
            button1.Location = new Point(
                 lokacjax,
                 button1.Location.Y
                 );
            lokacjax = lokacjax + button3.Width + odlewej;
            int lokacjay = 0;
            if (label3.Height + 41 <= label5.Height + 73)
            {
                lokacjay = 73 + label5.Height + 42 + 24;

            }
            else
            {
                lokacjay = 41 + label3.Height + 42 + 24;
            }
            this.Size = new Size(lokacjax, lokacjay);
        }


    }
}
