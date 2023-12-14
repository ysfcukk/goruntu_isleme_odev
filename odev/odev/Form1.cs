using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace odev
{
    public partial class hafta1 : Form
    {
        public hafta1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int deger = 256;
            Bitmap resim = new Bitmap(deger, deger);
            for (int i = 0; i < deger; i++)
            {
                for (int j = 0; j < deger; j++)
                {
                    Color renk = Color.FromArgb(i, i, i);
                    resim.SetPixel(i, j, renk);
                }
            }
            pictureBox1.Image = resim;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int deger = 512;
            Bitmap resim = new Bitmap(deger, deger);
            for (int i = 0; i < deger; i++)
            {
                for (int j = 0; j < deger; j++)
                {
                    Color renk;
                    double kontrol = Math.Sqrt(Math.Pow((i - 256), 2) + Math.Pow((j - 256), 2));
                    if (kontrol < 100) renk = Color.FromArgb(255, 255, 255);
                    else renk = Color.FromArgb(0, 0, 0);
                    resim.SetPixel(i, j, renk);
                }
            }
            pictureBox1.Image = resim;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            int deger = Convert.ToInt32(comboBox1.SelectedItem.ToString());

            int sayi = 255 / (deger - 1);

            int r, g, b;

            Color okunanRenk, donusenRenk;

            Bitmap girisResmi = new Bitmap(pictureBox1.Image);

            int width = girisResmi.Width;
            int height = girisResmi.Height;

            Bitmap cikisResmi = new Bitmap(width, height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    okunanRenk = girisResmi.GetPixel(i, j);

                    r = okunanRenk.R;
                    g = okunanRenk.G;
                    b = okunanRenk.B;

                    if (r % sayi < (sayi / 2.0)) r = (r / sayi) * sayi;
                    else r = ((r / sayi) + 1) * sayi;
                    if (g % sayi < (sayi / 2.0)) g = (g / sayi) * sayi;
                    else g = ((g / sayi) + 1) * sayi;
                    if (b % sayi < (sayi / 2.0)) b = (b / sayi) * sayi;
                    else b = ((b / sayi) + 1) * sayi;

                    if (r > 255) r = 255;
                    if (g > 255) g = 255;
                    if (b > 255) b = 255;


                    donusenRenk = Color.FromArgb(r, g, b);

                    cikisResmi.SetPixel(i, j, donusenRenk);
                }
            }
            pictureBox2.Image = cikisResmi;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            int r, g, b;

            Color okunanRenk, donusenRenk;

            Bitmap girisResmi = new Bitmap(pictureBox1.Image);

            int width = girisResmi.Width;
            int height = girisResmi.Height;

            Bitmap cikisResmi = new Bitmap(width, height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    okunanRenk = girisResmi.GetPixel(i, j);

                    r = okunanRenk.R;
                    g = okunanRenk.G;
                    b = okunanRenk.B;

                    if (r != 255 && r % 10 >= 5) r = ((r / 10) + 1) * 10;
                    else r = (r / 10) * 10;
                    if (g != 255 && g % 10 >= 55) g = ((g / 10) + 1) * 10;
                    else g = (g / 10) * 10;
                    if (b != 255 && b % 10 >= 5) b = ((b / 10) + 1) * 10;
                    else b = (b / 10) * 10;

                    donusenRenk = Color.FromArgb(r, g, b);

                    cikisResmi.SetPixel(i, j, donusenRenk);
                }
            }
            pictureBox2.Image = cikisResmi;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.DefaultExt = ".jpg";
            file.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            file.ShowDialog();
            pictureBox1.ImageLocation = file.FileName;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int mozaikCizgiBoyutu = 5;
            int mozaikBoyutu = Convert.ToInt32(comboBox2.SelectedItem.ToString());


            Bitmap girisResmi = new Bitmap(pictureBox1.Image);

            Bitmap cikisResmi = GriyeDonustur(girisResmi);

            int width = cikisResmi.Width;
            int height = cikisResmi.Height;

            Color renk = Color.White;

            for (int x = 0; x < width; x = x + 1)
            {
                for (int y = 0; y < height; y = y + 1)
                {
                    if (y % mozaikBoyutu == 0)
                    {
                        for (int i = 0; i < mozaikCizgiBoyutu; i++)
                            if (y + i < height) cikisResmi.SetPixel(x, y + i, renk);
                    }
                    if (x % mozaikBoyutu == 0)
                    {
                        for (int i = 0; i < mozaikCizgiBoyutu; i++)
                            if (x + i < width) cikisResmi.SetPixel(x + i, y, renk);
                    }

                }
            }
            pictureBox2.Image = cikisResmi;
        }

        static Color GetAverageColor(Bitmap image, int startX, int startY, int width, int height)
        {
            int totalR = 0, totalG = 0, totalB = 0;
            int pixelCount = 0;

            for (int x = startX; x < startX + width && x < image.Width; x++)
            {
                for (int y = startY; y < startY + height && y < image.Height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    totalR += pixelColor.R;
                    totalG += pixelColor.G;
                    totalB += pixelColor.B;
                    pixelCount++;
                }
            }

            int averageR = totalR / pixelCount;
            int averageG = totalG / pixelCount;
            int averageB = totalB / pixelCount;

            return Color.FromArgb(averageR, averageG, averageB);
        }

        static Bitmap GriyeDonustur(Bitmap girisResmi)
        {
            int r, g, b;

            Color okunanRenk, donusenRenk;

            int width = girisResmi.Width;
            int height = girisResmi.Height;

            Bitmap cikisResmi = new Bitmap(width, height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    okunanRenk = girisResmi.GetPixel(i, j);

                    r = Convert.ToInt32(okunanRenk.R * 0.3);
                    g = Convert.ToInt32(okunanRenk.G * 0.6);
                    b = Convert.ToInt32(okunanRenk.B * 0.1);

                    int griDeger = r + g + b;

                    donusenRenk = Color.FromArgb(griDeger, griDeger, griDeger);

                    cikisResmi.SetPixel(i, j, donusenRenk);
                }
            }
            return cikisResmi;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            int mozaikCizgiBoyutu = 5;
            int mozaikBoyutu = Convert.ToInt32(comboBox2.SelectedItem.ToString());

            int r, g, b;

            Color okunanRenk, donusenRenk;

            Bitmap girisResmi = new Bitmap(pictureBox1.Image);
            girisResmi = GriyeDonustur(girisResmi);

            int width = girisResmi.Width;
            int height = girisResmi.Height;

            double m = (double)width / mozaikBoyutu;
            double n = (double)height / mozaikBoyutu;

            Bitmap cikisResmi = new Bitmap((int)Math.Ceiling(m), (int)Math.Ceiling(n));

            List<Color> renkler = new List<Color>();

            for (int x = 0; x < width; x = x + mozaikBoyutu)
            {
                for (int y = 0; y < height; y = y + mozaikBoyutu)
                {
                    Color ortRenk = GetAverageColor(girisResmi, x, y, mozaikBoyutu, mozaikBoyutu);
                    renkler.Add(ortRenk);
                }
            }

            int k = 0;

            for (int i = 0; i < cikisResmi.Width; i++)
            {
                for (int j = 0; j < cikisResmi.Height; j++)
                {
                    cikisResmi.SetPixel(i, j, renkler[k++]);
                }
            }
            pictureBox2.Image = cikisResmi;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int r, g, b;

            Color okunanRenk, donusenRenk;

            Bitmap girisResmi = new Bitmap(pictureBox1.Image);

            int width = girisResmi.Width;
            int height = girisResmi.Height;

            Bitmap cikisResmi = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    okunanRenk = girisResmi.GetPixel(x, y);

                    r = okunanRenk.R;
                    g = 0;
                    b = 0;

                    donusenRenk = Color.FromArgb(r, g, b);

                    cikisResmi.SetPixel(x, y, donusenRenk);

                }
            }
            pictureBox2.Image = cikisResmi;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int r, g, b;

            Color okunanRenk, donusenRenk;

            Bitmap girisResmi = new Bitmap(pictureBox1.Image);

            int width = girisResmi.Width;
            int height = girisResmi.Height;

            Bitmap cikisResmi = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    okunanRenk = girisResmi.GetPixel(x, y);

                    r = 0;
                    g = okunanRenk.G;
                    b = 0;

                    donusenRenk = Color.FromArgb(r, g, b);

                    cikisResmi.SetPixel(x, y, donusenRenk);

                }
            }
            pictureBox2.Image = cikisResmi;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int r, g, b;

            Color okunanRenk, donusenRenk;

            Bitmap girisResmi = new Bitmap(pictureBox1.Image);

            int width = girisResmi.Width;
            int height = girisResmi.Height;

            Bitmap cikisResmi = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    okunanRenk = girisResmi.GetPixel(x, y);

                    r = 0;
                    g = 0; ;
                    b = okunanRenk.B;

                    donusenRenk = Color.FromArgb(r, g, b);

                    cikisResmi.SetPixel(x, y, donusenRenk);

                }
            }
            pictureBox2.Image = cikisResmi;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int r, g, b;

            Color okunanRenk, donusenRenk;

            Bitmap girisResmi = new Bitmap(pictureBox1.Image);

            int width = girisResmi.Width;
            int height = girisResmi.Height;

            Bitmap cikisResmi = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    okunanRenk = girisResmi.GetPixel(x, y);

                    r = okunanRenk.R;
                    g = 0;
                    b = 0;


                    donusenRenk = Color.FromArgb(r, g, b);

                    cikisResmi.SetPixel(x, y, donusenRenk);

                }
            }
            cikisResmi = GriyeDonustur(cikisResmi);
            pictureBox2.Image = cikisResmi;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int r, g, b;

            Color okunanRenk, donusenRenk;

            Bitmap girisResmi = new Bitmap(pictureBox1.Image);

            int width = girisResmi.Width;
            int height = girisResmi.Height;

            Bitmap cikisResmi = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    okunanRenk = girisResmi.GetPixel(x, y);

                    r = 0;
                    g = okunanRenk.G;
                    b = 0;

                    donusenRenk = Color.FromArgb(r, g, b);

                    cikisResmi.SetPixel(x, y, donusenRenk);

                }
            }
            cikisResmi = GriyeDonustur(cikisResmi);
            pictureBox2.Image = cikisResmi;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int r, g, b;

            Color okunanRenk, donusenRenk;

            Bitmap girisResmi = new Bitmap(pictureBox1.Image);

            int width = girisResmi.Width;
            int height = girisResmi.Height;

            Bitmap cikisResmi = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    okunanRenk = girisResmi.GetPixel(x, y);

                    r = 0;
                    g = 0; ;
                    b = okunanRenk.B;

                    donusenRenk = Color.FromArgb(r, g, b);

                    cikisResmi.SetPixel(x, y, donusenRenk);

                }
            }
            cikisResmi = GriyeDonustur(cikisResmi);
            pictureBox2.Image = cikisResmi;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            int r, g, b;

            Color okunanRenk, donusenRenk;

            Bitmap girisResmi = new Bitmap(pictureBox1.Image);

            int width = girisResmi.Width;
            int height = girisResmi.Height;

            Bitmap cikisResmi = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    okunanRenk = girisResmi.GetPixel(x, y);

                    r = okunanRenk.G;
                    g = okunanRenk.R;
                    b = okunanRenk.B;

                    donusenRenk = Color.FromArgb(r, g, b);

                    cikisResmi.SetPixel(x, y, donusenRenk);

                }
            }
            pictureBox2.Image = cikisResmi;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int r, g, b;

            Color okunanRenk, donusenRenk;

            Bitmap girisResmi = new Bitmap(pictureBox1.Image);

            int width = girisResmi.Width;
            int height = girisResmi.Height;

            Bitmap cikisResmi = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    okunanRenk = girisResmi.GetPixel(x, y);

                    r = okunanRenk.R;
                    g = okunanRenk.B;
                    b = okunanRenk.G;

                    donusenRenk = Color.FromArgb(r, g, b);

                    cikisResmi.SetPixel(x, y, donusenRenk);

                }
            }
            pictureBox2.Image = cikisResmi;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int r, g, b;

            Color okunanRenk, donusenRenk;

            Bitmap girisResmi = new Bitmap(pictureBox1.Image);

            int width = girisResmi.Width;
            int height = girisResmi.Height;

            Bitmap cikisResmi = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    okunanRenk = girisResmi.GetPixel(x, y);

                    r = okunanRenk.G;
                    g = okunanRenk.B;
                    b = okunanRenk.R;

                    donusenRenk = Color.FromArgb(r, g, b);

                    cikisResmi.SetPixel(x, y, donusenRenk);

                }
            }
            pictureBox2.Image = cikisResmi;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            int deger = Convert.ToInt32(comboBox3.SelectedItem.ToString());

            int sayi = 255 / (deger - 1);

            int r, g, b;

            Color okunanRenk, donusenRenk;

            Bitmap girisResmi = new Bitmap(pictureBox1.Image);
            girisResmi = GriyeDonustur(girisResmi);

            int width = girisResmi.Width;
            int height = girisResmi.Height;

            Bitmap cikisResmi = new Bitmap(width, height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    okunanRenk = girisResmi.GetPixel(i, j);

                    r = okunanRenk.R;
                    g = okunanRenk.G;
                    b = okunanRenk.B;

                    if (r % sayi < (sayi / 2.0)) r = (r / sayi) * sayi;
                    else r = ((r / sayi) + 1) * sayi;
                    if (g % sayi < (sayi / 2.0)) g = (g / sayi) * sayi;
                    else g = ((g / sayi) + 1) * sayi;
                    if (b % sayi < (sayi / 2.0)) b = (b / sayi) * sayi;
                    else b = ((b / sayi) + 1) * sayi;

                    if (r > 255) r = 255;
                    if (g > 255) g = 255;
                    if (b > 255) b = 255;


                    donusenRenk = Color.FromArgb(r, g, b);

                    cikisResmi.SetPixel(i, j, donusenRenk);
                }
            }
            pictureBox2.Image = cikisResmi;
        }

        private void hafta1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;

        }

        private void button25_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.picture3;
        }

    }
}

