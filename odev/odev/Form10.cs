using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace odev
{
    public partial class hafta10 : Form
    {
        public hafta10()
        {
            InitializeComponent();
        }

        private Bitmap GriyeDonustur(Bitmap girisResmi)
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

        private void button5_Click(object sender, EventArgs e)
        {
            Bitmap resim1 = new Bitmap(pictureBox1.Image);
            Bitmap resim2 = new Bitmap(pictureBox2.Image);

            pictureBox3.Image = RenkliTespit(resim1, resim2);
        }

        private Bitmap RenkliTespit(Bitmap Resim1, Bitmap Resim2)
        {
            Bitmap CikisResmi;
            int ResimGenisligi = Resim1.Width;
            int ResimYuksekligi = Resim1.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi); Color Renk1, Renk2;
            int x, y;
            int R = 0, G = 0, B = 0;
            for (x = 0; x < ResimGenisligi; x++) //Resmi taramaya şablonun yarısı kadar dış kenarlardan içeride başlayacak ve bitirecek.
            {
                for (y = 0; y < ResimYuksekligi; y++)
                {
                    Renk1 = Resim1.GetPixel(x, y);
                    Renk2 = Resim2.GetPixel(x, y);
                    string binarySayi1R = Convert.ToString(Renk1.R, 2).PadLeft(8, '0'); //Gri renk olduğundan tek kanal üzerinden yapılıyor.
                    string binarySayi2R = Convert.ToString(Renk2.R, 2).PadLeft(8, '0');
                    string binarySayi1G = Convert.ToString(Renk1.G, 2).PadLeft(8, '0'); //Gri renk olduğundan tek kanal üzerinden yapılıyor.
                    string binarySayi2G = Convert.ToString(Renk2.G, 2).PadLeft(8, '0');
                    string binarySayi1B = Convert.ToString(Renk1.B, 2).PadLeft(8, '0'); //Gri renk olduğundan tek kanal üzerinden yapılıyor.
                    string binarySayi2B = Convert.ToString(Renk2.B, 2).PadLeft(8, '0');
                    string Bit1R = null, Bit1G = null, Bit1B = null, Bit2R = null, Bit2G = null, Bit2B = null;
                    string StringIkiliSayiR = null, StringIkiliSayiG = null, StringIkiliSayiB = null;
                    for (int i = 0; i < 8; i++)
                    {
                        Bit1R = binarySayi1R.Substring(i, 1); Bit2R = binarySayi2R.Substring(i, 1);

                        if (Bit1R == "1" && Bit2R == "0")
                            StringIkiliSayiR = StringIkiliSayiR + "0";
                        else
                            StringIkiliSayiR = StringIkiliSayiR + "1";

                        if (Bit1G == "1" && Bit2G == "0")
                            StringIkiliSayiG = StringIkiliSayiG + "0";
                        else
                            StringIkiliSayiG = StringIkiliSayiG + "1";

                        if (Bit1B == "1" && Bit2B == "0")
                            StringIkiliSayiB = StringIkiliSayiB + "0";
                        else
                            StringIkiliSayiB = StringIkiliSayiB + "1";

                    }
                    R = Convert.ToInt32(StringIkiliSayiR, 2); //İkili sayıyı tam sayıya dönüştürüyor.
                    G = Convert.ToInt32(StringIkiliSayiG, 2); //İkili sayıyı tam sayıya dönüştürüyor.
                    B = Convert.ToInt32(StringIkiliSayiB, 2); //İkili sayıyı tam sayıya dönüştürüyor.
                    CikisResmi.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }
            return CikisResmi;
        }

        private Bitmap SiyahAritmetikOperator(Bitmap Resim1, Bitmap Resim2, string op)
        {
            Bitmap CikisResmi;
            int ResimGenisligi = Resim1.Width;
            int ResimYuksekligi = Resim1.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            Color Renk1, Renk2;
            int x, y;
            int R = 0, G = 0, B = 0;
            for (x = 0; x < ResimGenisligi; x++) //Resmi taramaya şablonun yarısı kadar dış kenarlardan içeride başlayacak ve bitirecek.
            {
                for (y = 0; y < ResimYuksekligi; y++)
                {
                    Renk1 = Resim1.GetPixel(x, y); Renk2 = Resim2.GetPixel(x, y);
                    string binarySayi1 = Convert.ToString(Renk1.R, 2).PadLeft(8, '0'); //Gri renk olduğundan tek kanal üzerinden yapılıyor.
                    string binarySayi2 = Convert.ToString(Renk2.R, 2).PadLeft(8, '0');
                    string Bit1 = null, Bit2 = null, StringIkiliSayi = null;
                    for (int i = 0; i < 8; i++)
                    {
                        Bit1 = binarySayi1.Substring(i, 1);
                        Bit2 = binarySayi2.Substring(i, 1);

                        if (op == "and")
                        {
                            if (Bit1 == "0" && Bit2 == "0") StringIkiliSayi = StringIkiliSayi + "0";
                            else if (Bit1 == "1" && Bit2 == "1") StringIkiliSayi = StringIkiliSayi + "1";
                            else StringIkiliSayi = StringIkiliSayi + "0";
                        }
                        else if (op == "nand")
                        {
                            if (Bit1 == "0" && Bit2 == "0")
                                StringIkiliSayi = StringIkiliSayi + "1";
                            else if (Bit1 == "1" && Bit2 == "1")
                                StringIkiliSayi = StringIkiliSayi + "0";
                            else
                                StringIkiliSayi = StringIkiliSayi + "1";
                        }
                        else if (op == "or")
                        {
                            if (Bit1 == "0" && Bit2 == "0") StringIkiliSayi += "0";
                            else StringIkiliSayi += "1";
                        }
                        else if (op == "nor")
                        {
                            if (Bit1 == "0" && Bit2 == "0") StringIkiliSayi += "1";
                            else StringIkiliSayi += "0";
                        }
                        else
                        {
                            MessageBox.Show("Hatali Operator!");
                            return null;
                        }
                    }
                    R = Convert.ToInt32(StringIkiliSayi, 2); //İkili sayıyı tam sayıya dönüştürüyor.
                    CikisResmi.SetPixel(x, y, Color.FromArgb(R, R, R)); //Gri resim
                }
            }
            return CikisResmi;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.DefaultExt = ".jpg";
            file.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            file.ShowDialog();
            pictureBox1.ImageLocation = file.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.DefaultExt = ".jpg";
            file.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            file.ShowDialog();
            pictureBox2.ImageLocation = file.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.cars2;
            pictureBox2.Image = Properties.Resources.cars1;
        }

        private Bitmap BitDilimle(Bitmap Resim1, int bitSira, int olcek)
        {
            Bitmap CikisResmi;
            int ResimGenisligi = Resim1.Width;
            int ResimYuksekligi = Resim1.Height;
            CikisResmi = new Bitmap(ResimGenisligi,
            ResimYuksekligi);
            Color Renk1;
            int x, y;
            int R = 0, G = 0, B = 0;
            for (x = 0; x < ResimGenisligi; x++) //Resmi taramaya şablonun yarısı kadar dış kenarlardan içeride başlayacak ve bitirecek.
            {
                for (y = 0; y < ResimYuksekligi; y++)
                {
                    Renk1 = Resim1.GetPixel(x, y);
                    string binarySayi1R = Convert.ToString(Renk1.R, 2).PadLeft(8, '0'); //Gri renk olduğundan tek kanal üzerinden yapılıyor.
                    string binarySayi1G = Convert.ToString(Renk1.G, 2).PadLeft(8, '0'); //Gri renk olduğundan tek kanal üzerinden yapılıyor.
                    string binarySayi1B = Convert.ToString(Renk1.B, 2).PadLeft(8, '0'); //Gri renk olduğundan tek kanal üzerinden yapılıyor.
                    string Bit1R = null, Bit1G = null, Bit1B = null;
                    string StringIkiliSayiR = null, StringIkiliSayiG = null, StringIkiliSayiB = null;
                    int BitSiraNo = bitSira;

                    for (int i = 0; i < 8; i++)
                    {
                        if (i == BitSiraNo)
                        {
                            Bit1R = binarySayi1R.Substring(i, 1); StringIkiliSayiR = StringIkiliSayiR + Bit1R;
                            Bit1G = binarySayi1G.Substring(i, 1); StringIkiliSayiG = StringIkiliSayiG + Bit1G;
                            Bit1B = binarySayi1B.Substring(i, 1); StringIkiliSayiB = StringIkiliSayiB + Bit1B;
                        }
                        else
                        {
                            StringIkiliSayiR = StringIkiliSayiR + "0";
                            StringIkiliSayiG = StringIkiliSayiG + "0";
                            StringIkiliSayiB = StringIkiliSayiB + "0";
                        }
                    }
                    R = Convert.ToInt32(StringIkiliSayiR, 2); //İkili sayıyı tam sayıya dönüştürüyor.
                    G = Convert.ToInt32(StringIkiliSayiG, 2); //İkili sayıyı tam sayıya dönüştürüyor.
                    B = Convert.ToInt32(StringIkiliSayiB, 2); //İkili sayıyı tam sayıya dönüştürüyor.
                    int Olcek = olcek;
                    R = R * Olcek;
                    G = G * Olcek;
                    B = B * Olcek;
                    if (R > 255) R = 255;
                    if (G > 255) G = 255;
                    if (B > 255) B = 255;
                    CikisResmi.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }
            return CikisResmi;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);
            Bitmap resim1 = BitDilimle(resim, 0, 10);
            Bitmap resim2 = BitDilimle(resim, 5, 50);

            double a;
            double b;

            (a, b) = OtomatikOlceklendirme(resim1, resim2);

            pictureBox3.Image = PikselToplama(resim1, resim2, a, b, false, true);
        }

        private (double, double) OtomatikOlceklendirme(Bitmap Resim1, Bitmap Resim2)
        {
            Bitmap CikisResmi;
            int ResimGenisligi = Resim1.Width;
            int ResimYuksekligi = Resim1.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            Color renk1, renk2;
            int x, y;
            int R = 0, G = 0, B = 0;


            int renkToplam1 = 0;
            int renkToplam2 = 0;

            for (x = 0; x < ResimGenisligi; x++)
            {
                for (y = 0; y < ResimYuksekligi; y++)
                {
                    renk1 = Resim1.GetPixel(x, y);
                    renk2 = Resim2.GetPixel(x, y);

                    renkToplam1 += (renk1.R + renk1.G + renk1.B);
                    renkToplam2 += (renk2.R + renk2.G + renk2.B);
                }
            }

            double renk1Ort = renkToplam1 / (Resim1.Width * Resim1.Height * 3.0);
            double renk2Ort = renkToplam1 / (Resim2.Width * Resim2.Height * 3.0);

            double a = 1.0 / (renk1Ort / renk2Ort);
            double b = 1.0;

            double deger1 = a / (a + b);
            double deger2 = b / (b + a);

            return (deger1, deger2);


        }

        private Bitmap PikselToplama(Bitmap Resim1, Bitmap Resim2, double a, double b, bool norm, bool toplama)
        {
            Bitmap CikisResmi;
            int ResimGenisligi = Resim1.Width;
            int ResimYuksekligi = Resim1.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            Color Renk1, Renk2;
            int x, y;
            int R = 0, G = 0, B = 0;
            for (x = 0; x < ResimGenisligi; x++) //Resmi taramaya şablonun yarısı kadar dış kenarlardan içeride başlayacak ve bitirecek.
            {
                for (y = 0; y < ResimYuksekligi; y++)
                {
                    Renk1 = Resim1.GetPixel(x, y); Renk2 = Resim2.GetPixel(x, y);
                    //İki resmi direk toplama
                    //R = Renk1.R + Renk2.R;
                    //G = Renk1.G + Renk2.G;
                    //B = Renk1.B + Renk2.B;

                    //İki resmi ölçekli olarak toplama
                    if (toplama)
                    {
                        R = Convert.ToInt16(Renk1.R * a + Renk2.R * b);
                        G = Convert.ToInt16(Renk1.G * a + Renk2.G * b);
                        B = Convert.ToInt16(Renk1.B * a + Renk2.B * b);

                    }
                    else
                    {

                        R = Convert.ToInt16(Renk1.R * a - Renk2.R * b);
                        G = Convert.ToInt16(Renk1.G * a - Renk2.G * b);
                        B = Convert.ToInt16(Renk1.B * a - Renk2.B * b);
                    }
                    if (norm)
                    {
                        // kendi renk değerlerini toplayacağımızdan en fazla 510 olabilir(255+255);
                        R = (int)((R / 510.0) * 255.0);
                        G = (int)((G / 510.0) * 255.0);
                        B = (int)((B / 510.0) * 255.0);
                    }
                    else
                    {
                        if (R > 255) R = 255;
                        else if (R < 0) R = 0;
                        if (G > 255) G = 255;
                        else if (G < 0) G = 0;
                        if (B > 255) B = 255;
                        else if (B < 0) B = 0;
                    }

                    //Sınırı aşan değerleri Başa sarma şeklinde ayarlama
                    //if (R > 255) R = (R - 255);
                    //if (G > 255) G = (G - 255);
                    //if (B > 255) B = (B - 255);
                    CikisResmi.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }
            return CikisResmi;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = BitDilimle(resim, 0, 10);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = BitDilimle(resim, 5, 50);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);
            Bitmap resim1 = BitDilimle(resim, 0, 10);
            Bitmap resim2 = BitDilimle(resim, 5, 50);

            double a;
            double b;

            (a, b) = OtomatikOlceklendirme(resim1, resim2);

            pictureBox3.Image = PikselToplama(resim1, resim2, a, b, false, false);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.picture3;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Bitmap Resim1 = new Bitmap(pictureBox1.Image);

            Bitmap CikisResmi;
            int ResimGenisligi = Resim1.Width;
            int ResimYuksekligi = Resim1.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            Color renk1, renk2;
            int x, y;
            int R = 0, G = 0, B = 0;

            int a = Convert.ToInt16(textBox1.Text);

            for (x = 0; x < ResimGenisligi; x++)
            {
                for (y = 0; y < ResimYuksekligi; y++)
                {
                    renk1 = Resim1.GetPixel(x, y);

                    R = renk1.R << a;
                    G = renk1.G << a;
                    B = renk1.B << a;

                    if (R > 255) R = 255;
                    if (G > 255) G = 255;
                    if (B > 255) B = 255;

                    CikisResmi.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Bitmap Resim1 = new Bitmap(pictureBox1.Image);

            Bitmap CikisResmi;
            int ResimGenisligi = Resim1.Width;
            int ResimYuksekligi = Resim1.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            Color renk1, renk2;
            int x, y;
            int R = 0, G = 0, B = 0;

            int a = Convert.ToInt16(textBox1.Text);

            for (x = 0; x < ResimGenisligi; x++)
            {
                for (y = 0; y < ResimYuksekligi; y++)
                {
                    renk1 = Resim1.GetPixel(x, y);

                    R = renk1.R >> a;
                    G = renk1.G >> a;
                    B = renk1.B >> a;

                    if (R > 255) R = 255;
                    if (G > 255) G = 255;
                    if (B > 255) B = 255;

                    CikisResmi.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }
            pictureBox2.Image = CikisResmi;
        }
    }
}
