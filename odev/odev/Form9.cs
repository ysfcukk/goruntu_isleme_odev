using DevExpress.Utils.DirectXPaint;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace odev
{
    public partial class hafta9 : Form
    {
        public hafta9()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.picture3;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.DefaultExt = ".jpg";
            file.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            file.ShowDialog();
            pictureBox1.ImageLocation = file.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            parlaklik();
            normAktif = false;
            parlaklikAktif = true;
            timer1.Start();
        }

        private void parlaklik()
        {
            int value = trackBar1.Value;

            int R = 0, G = 0, B = 0;

            Color OkunanRenk, DonusenRenk;

            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);

            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;

            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);
                    R = OkunanRenk.R + value;
                    G = OkunanRenk.G + value;
                    B = OkunanRenk.B + value;

                    if (R > 255) R = 255;
                    if (G > 255) G = 255;
                    if (B > 255) B = 255;

                    if (R < 0) R = 0;
                    if (G < 0) G = 0;
                    if (B < 0) B = 0;
                    DonusenRenk = Color.FromArgb(R, G, B); CikisResmi.SetPixel(x, y, DonusenRenk);
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (parlaklikAktif)
                parlaklik();
            else
                Normalizasyon();
        }

        bool parlaklikAktif = false;
        bool normAktif = false;

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Normalizasyon();
            normAktif = true;
            parlaklikAktif = false;
            timer1.Start();
        }

        private void Normalizasyon()
        {
            int value = trackBar2.Value;

            int R = 0, G = 0, B = 0;

            Color OkunanRenk, DonusenRenk;

            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);

            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;

            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);

                    R = (int)((OkunanRenk.R / 255.0) * (255 - value) + value);
                    G = (int)((OkunanRenk.G / 255.0) * (255 - value) + value);
                    B = (int)((OkunanRenk.B / 255.0) * (255 - value) + value);

                    CikisResmi.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }

            pictureBox2.Image = CikisResmi;
        }

        private Bitmap YaziDuzelt(Bitmap Resim1, Bitmap Resim2)
        {
            int R = 0, G = 0, B = 0;

            Color Renk1, Renk2;
            Color OkunanRenk, DonusenRenk;

            Bitmap CikisResmi;

            int ResimGenisligi = Resim1.Width;
            int ResimYuksekligi = Resim1.Height;

            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    Renk1 = Resim1.GetPixel(x, y);
                    Renk2 = Resim2.GetPixel(x, y);

                    R = Math.Abs(Renk1.R - Renk2.R);
                    G = Math.Abs(Renk1.G - Renk2.G);
                    B = Math.Abs(Renk1.B - Renk2.B);

                    CikisResmi.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }
            return CikisResmi;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.text;
        }

        private Bitmap Esikle(Bitmap resim, int a)
        {

            int R = 0, G = 0, B = 0;

            Color OkunanRenk, DonusenRenk;

            int ResimGenisligi = resim.Width;
            int ResimYuksekligi = resim.Height;

            Bitmap CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk = resim.GetPixel(x, y);

                    int renk = OkunanRenk.R;

                    if (renk < a)
                    {
                        R = 0; G = 0; B = 0;
                    }
                    else
                    {
                        R = renk; G = renk; B = renk;
                    }

                    DonusenRenk = Color.FromArgb(R, G, B);
                    CikisResmi.SetPixel(x, y, DonusenRenk);
                }
            }
            return (CikisResmi);

        }

        private int[] Gx3D(int k, double std)
        {
            int baslangic = (k / 2);

            int[] sayilar = new int[k];

            for (int i = 0; i < k; i++)
            {
                sayilar[i] = i - baslangic;
            }

            double deger1 = 1.0 / (Math.Sqrt(2 * Math.PI) * (std * std));
            double deger2 = 0.0;

            List<int> son_degerler = new List<int>();

            int x;
            int y;

            double olcek = 0.0;

            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    x = sayilar[j];
                    y = sayilar[i];

                    deger2 = Math.Pow(Math.E, (x * x + y * y) / (-2.0 * (std * std)));

                    if (i == 0 && j == 0)
                        olcek = (1.0 / (deger1 * deger2));

                    son_degerler.Add(Convert.ToInt32(Math.Round(olcek * (deger2 * deger1))));

                    //string text = x.ToString() + "," + y.ToString() + " || " + deger1 + " || " + deger2 + " || " + (olcek * (deger2 * deger1));
                    //MessageBox.Show(text);

                }
            }
            //MessageBox.Show(sayilar[2].ToString());

            return son_degerler.ToArray();

        }

        private Bitmap Gauss(Bitmap GirisResmi, int l, double std)
        {
            Color OkunanRenk;
            Bitmap CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = l; //Çekirdek matrisin boyutu
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int x, y, i, j, toplamR, toplamG, toplamB, ortalamaR, ortalamaG, ortalamaB;

            int[] Matris = Gx3D(l, std);

            int MatrisToplami = 0;
            foreach (int sayi in Matris)
            {
                MatrisToplami = MatrisToplami + sayi;
            }

            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    toplamR = 0;
                    toplamG = 0;
                    toplamB = 0;
                    int k = 0;
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            toplamR = toplamR + OkunanRenk.R * Matris[k];
                            toplamG = toplamG + OkunanRenk.G * Matris[k];
                            toplamB = toplamB + OkunanRenk.B * Matris[k];
                            k++;
                        }
                    }
                    ortalamaR = toplamR / MatrisToplami;
                    ortalamaG = toplamG / MatrisToplami;
                    ortalamaB = toplamB / MatrisToplami;

                    if (ortalamaR < 0) ortalamaR = 0;
                    if (ortalamaG < 0) ortalamaG = 0;
                    if (ortalamaB < 0) ortalamaB = 0;

                    CikisResmi.SetPixel(x, y, Color.FromArgb(ortalamaR, ortalamaG, ortalamaB));
                }
            }
            return CikisResmi;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            int k = Convert.ToInt16(textBox1.Text);

            int a = Convert.ToInt16(textBox2.Text);

            Bitmap resim = new Bitmap(pictureBox1.Image);
            Bitmap arkaResim = Properties.Resources.text_arka;
            //Bitmap arkaResim = Gauss(resim, 15, 2);
            resim = YaziDuzelt(resim, arkaResim);
            resim = Esikle(resim, k);
            resim = TersAl(resim);
            resim = KontrastUygula(resim, "gri", a, 255, 0, 250);
            pictureBox2.Image = resim;
            //pictureBox1.Image = resim;

        }

        private Bitmap TersAl(Bitmap Resim1)
        {
            int R = 0, G = 0, B = 0;

            Color OkunanRenk, DonusenRenk;

            Bitmap CikisResmi;

            int ResimGenisligi = Resim1.Width;
            int ResimYuksekligi = Resim1.Height;

            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk = Resim1.GetPixel(x, y);
                    R = 255 - OkunanRenk.R;
                    G = 255 - OkunanRenk.G;
                    B = 255 - OkunanRenk.B;

                    CikisResmi.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }
            return CikisResmi;
        }

        static Bitmap KontrastUygula(Bitmap resim, string tur, int X1, int X2, int Y1, int Y2)
        {
            Color OkunanRenk, DonusenRenk;
            int R = 0, G = 0, B = 0;

            Bitmap GirisResmi, CikisResmi;
            GirisResmi = resim;

            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;

            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            int i = 0, j = 0;

            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);

                    R = OkunanRenk.R;
                    G = OkunanRenk.G;
                    B = OkunanRenk.B;

                    //MessageBox.Show(R.ToString() + " " + G.ToString() + " " + B.ToString());

                    if (tur == "gri")
                    {

                        if (X1 != X2)
                        {
                            //MessageBox.Show(R.ToString()+ " " +X1.ToString() + " " + X2.ToString() + " " + Y1.ToString() + " "+ Y2.ToString());
                            double Y = (((double)(R - X1) / (X2 - X1)) * (Y2 - Y1)) + Y1;
                            if (Y > 255) Y = 255;
                            if (Y < 0) Y = 0;
                            //MessageBox.Show(X.ToString());
                            DonusenRenk = Color.FromArgb((int)Y, (int)Y, (int)Y);
                            CikisResmi.SetPixel(x, y, DonusenRenk);
                        }
                        else
                        {
                            MessageBox.Show("Degerler ayni olmamali!");
                            return resim;
                        }
                    }
                    else
                    {
                        //MessageBox.Show(R.ToString() + " " + G.ToString() + " " + B.ToString());
                        if (X1 != X2)
                        {
                            double r, g, b;
                            if (tur == "kirmizi")
                            {
                                r = (((double)(R - X1) / (X2 - X1)) * (Y2 - Y1)) + Y1;
                                if (r > 255) r = 255;
                                if (r < 0) r = 0;

                                R = (int)r;
                            }
                            else if (tur == "yesil")
                            {
                                g = (((double)(G - X1) / (X2 - X1)) * (Y2 - Y1)) + Y1;
                                if (g > 255) g = 255;
                                if (g < 0) g = 0;

                                G = (int)g;
                            }
                            else if (tur == "mavi")
                            {
                                b = (((double)(B - X1) / (X2 - X1)) * (Y2 - Y1)) + Y1;
                                if (b > 255) b = 255;
                                if (b < 0) b = 0;

                                B = (int)b;
                            }

                            DonusenRenk = Color.FromArgb(R, G, B);
                        }
                        else
                        {
                            MessageBox.Show("Degerler ayni olmamali!");
                            return resim;
                        }
                        //MessageBox.Show(r.ToString() + " " + g.ToString() + " " + b.ToString());


                        CikisResmi.SetPixel(x, y, DonusenRenk);

                    }
                }
            }
            return CikisResmi;
        }

        private Bitmap PikselToplama(Bitmap Resim1, Bitmap Resim2, double a, double b, bool norm)
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
                    if (Renk2.R > 20 && Renk2.G > 20 && Renk2.B > 20)
                    {
                        R = Convert.ToInt16(Renk1.R * a + Renk2.R * b);
                        G = Convert.ToInt16(Renk1.G * a + Renk2.G * b);
                        B = Convert.ToInt16(Renk1.B * a + Renk2.B * b);
                    }
                    else
                    {
                        R = Renk1.R; G = Renk1.G; B = Renk1.B;
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
                        if (G > 255) G = 255;
                        if (B > 255) B = 255;
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

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.marylyn;
            pictureBox2.Image = Properties.Resources.view2;
        }

        Bitmap resim1;
        Bitmap resim2;

        private void button7_Click(object sender, EventArgs e)
        {
            resim1 = new Bitmap(pictureBox1.Image);
            resim2 = new Bitmap(pictureBox2.Image);

            double a = trackBar3.Value / 100.0;
            double b = trackBar4.Value / 100.0;

            pictureBox2.Image = PikselToplama(resim1, resim2, a, b,false);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            pictureBox2.Image = PikselToplama(resim1, resim2, trackBar3.Value / 100.0, trackBar4.Value / 100.0,false);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            resim1 = new Bitmap(pictureBox1.Image);
            resim2 = new Bitmap(pictureBox2.Image);

            double a;
            double b;

            (a,b) = OtomatikOlceklendirme(resim1, resim2);

            pictureBox2.Image = PikselToplama(resim1, resim2, a, b,false);
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

        private void button9_Click(object sender, EventArgs e)
        {
            resim1 = new Bitmap(pictureBox1.Image);
            resim2 = new Bitmap(pictureBox2.Image);

            pictureBox2.Image = PikselToplama(resim1, resim2, 1, 1,true);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tüm yöntemler birbirine yakın sonuçlar verdi. Sanırım en sağlıklısı ölçeği resme göre manuel ayarlamak olacaktır.");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Stop();
            MessageBox.Show("degisti");
        }
    }
}

