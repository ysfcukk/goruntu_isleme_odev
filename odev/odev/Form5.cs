using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Skins.SolidColorHelper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace odev
{
    public partial class hafta5 : Form
    {
        public hafta5()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.marylyn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.fence;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.viewv;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.noisy;
        }

        private void hafta5_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private int comboBoxDeger(int index)
        {
            int k;

            if (index == 0) k = 3;
            else if (index == 1) k = 5;
            else if (index == 2) k = 7;
            else k = 9;

            return k;
        }

        private Bitmap Mean(Bitmap GirisResmi, int k)
        {
            Color OkunanRenk;
            Bitmap CikisResmi;
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = k; //şablon boyutu 3 den büyük tek rakam olmalıdır (3,5,7 gibi).
            int x, y, i, j, toplamR, toplamG, toplamB, ortalamaR, ortalamaG, ortalamaB;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    toplamR = 0;
                    toplamG = 0;
                    toplamB = 0;

                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            toplamR = toplamR + OkunanRenk.R;
                            toplamG = toplamG + OkunanRenk.G;
                            toplamB = toplamB + OkunanRenk.B;
                        }
                    }
                    ortalamaR = toplamR / (SablonBoyutu * SablonBoyutu);
                    ortalamaG = toplamG / (SablonBoyutu * SablonBoyutu);
                    ortalamaB = toplamB / (SablonBoyutu * SablonBoyutu);
                    CikisResmi.SetPixel(x, y, Color.FromArgb(ortalamaR, ortalamaG, ortalamaB));
                }
            }
            return CikisResmi;
        }

        private Bitmap Medyan(Bitmap GirisResmi, int l)
        {
            Color OkunanRenk;
            Bitmap CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = 0;
            try
            {
                SablonBoyutu = l;
            }
            catch
            {
                SablonBoyutu = 3;
            }
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int[] R = new int[ElemanSayisi];
            int[] G = new int[ElemanSayisi];
            int[] B = new int[ElemanSayisi];
            int[] Gri = new int[ElemanSayisi];

            int x, y, i, j;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    int k = 0;
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            R[k] = OkunanRenk.R;
                            G[k] = OkunanRenk.G;
                            B[k] = OkunanRenk.B;
                            Gri[k] = Convert.ToInt16(R[k] * 0.299 + G[k] * 0.587 + B[k] * 0.114); //Gri ton formülü
                            k++;
                        }
                    }
                    int GeciciSayi = 0;
                    for (i = 0; i < ElemanSayisi; i++)
                    {
                        for (j = i + 1; j < ElemanSayisi; j++)
                        {
                            if (Gri[j] < Gri[i])
                            {
                                GeciciSayi = Gri[i];
                                Gri[i] = Gri[j];
                                Gri[j] = GeciciSayi;
                                GeciciSayi = R[i];
                                R[i] = R[j];
                                R[j] = GeciciSayi;
                                GeciciSayi = G[i];
                                G[i] = G[j];
                                G[j] = GeciciSayi;
                                GeciciSayi = B[i];
                                B[i] = B[j];
                                B[j] = GeciciSayi;
                            }
                        }
                    }
                    CikisResmi.SetPixel(x, y, Color.FromArgb(R[(ElemanSayisi - 1) / 2], G[(ElemanSayisi - 1) / 2], B[(ElemanSayisi - 1) / 2]));
                }
            }
            return CikisResmi;
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
        private void button5_Click(object sender, EventArgs e)
        {
            int k = comboBoxDeger(comboBox1.SelectedIndex);
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = Mean(resim, k);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int k = comboBoxDeger(comboBox1.SelectedIndex);
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = Medyan(resim, k);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int k = comboBoxDeger(comboBox1.SelectedIndex);
            Bitmap resim = new Bitmap(pictureBox1.Image);
            double std = Convert.ToDouble(textBox3.Text);
            pictureBox2.Image = Gauss(resim, k, std);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string metin = "Not: 9x9 Gauss yöntemi hatalı çalışıyor. Nedenini Çözemedim!" +
                "\nMedyan gürültü yok etme konusunda çok iyi. (Ornegin insandaki beni yok ediyor.) Fakat çizgili resimlerde çizgileri" +
                " neredeyse tamamen yok ediyor. \nMean diğerlerine göre hızlı ve basit ama çok iyi sonuçlar vermiyor. \nGauss ise genel olarak iyi fakat yavaş. Ayrıca" +
                " kenarları yok edebiliyor.";
            MessageBox.Show(metin);
        }

        private Bitmap MotionBlur(Bitmap originalImage, int blurDistance, int blurAngleDegrees)
        {
            int width = originalImage.Width;
            int height = originalImage.Height;

            // Yeni bir Bitmap oluştur
            Bitmap blurredImage = new Bitmap(width, height);

            // Radyan cinsinden açıyı hesapla
            double blurAngleRadians = blurAngleDegrees * Math.PI / 180.0;

            // Kaydırma miktarını belirle
            int dx = (int)(blurDistance * Math.Cos(blurAngleRadians));
            int dy = (int)(blurDistance * Math.Sin(blurAngleRadians));

            // Her piksel için motion blur efektini uygula
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Hesaplanan kaydırma miktarındaki piksellerin ortalamasını al
                    Color averageColor = GetAverageColor(originalImage, x - dx, y - dy, x + dx, y + dy);
                    blurredImage.SetPixel(x, y, averageColor);
                }
            }

            return blurredImage;
        }

        private Color GetAverageColor(Bitmap image, int startX, int startY, int endX, int endY)
        {
            int totalR = 0, totalG = 0, totalB = 0;
            int count = 0;

            // Verilen koordinat aralığındaki piksellerin renk değerlerini topla
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    if (x >= 0 && x < image.Width && y >= 0 && y < image.Height)
                    {
                        Color pixelColor = image.GetPixel(x, y);
                        totalR += pixelColor.R;
                        totalG += pixelColor.G;
                        totalB += pixelColor.B;
                        count++;
                    }
                }
            }

            // Ortalama renk değerlerini hesapla
            int averageR = totalR / count;
            int averageG = totalG / count;
            int averageB = totalB / count;

            return Color.FromArgb(averageR, averageG, averageB);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int blurDistance = Convert.ToInt16(textBox1.Text);
            int blurAngleDegrees = Convert.ToInt16(textBox2.Text);

            Bitmap originalImage = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = MotionBlur(originalImage, blurDistance, blurAngleDegrees);
        }

        private Point Oranla(PictureBox pb, int x, int y)
        {
            Bitmap resim1 = new Bitmap(pictureBox1.Image);

            int p_w = pictureBox1.Width;
            int p_h = pictureBox1.Height;
            int r_w = resim1.Width;
            int r_h = resim1.Height;

            int x0;
            int y0;

            if (r_h > p_h)
            {
                y0 = (int)Math.Round(y * (1.0 * r_h / p_h));
            }
            else
            {
                y0 = (int)Math.Round(y * (1.0 * r_h / p_h));
            }
            if (r_w > p_w)
            {
                x0 = (int)Math.Round(x * (1.0 * r_w / p_w));
            }
            else
            {
                x0 = (int)Math.Round(x * (1.0 * r_w / p_w));
            }

            if (x0 < 0) x0 = 0;
            if (y0 < 0) y0 = 0;

            return new Point(x0, y0);
        }

        private Bitmap NoktaCiz(Bitmap resim, int x, int y, Color color)
        {
            // Kesikli çizgiyi çizecek nesneyi oluşturun
            Graphics g = Graphics.FromImage(resim);

            // Çizgi özelliklerini ayarlayın
            Pen pen = new Pen(Color.Red);

            int width = resim.Width / 48; // Kare genişliği
            int height = resim.Height / 48; // Kare yüksekliği

            if (x > (resim.Width - width))
            {
                x = resim.Width - width;
            }

            if (y > (resim.Height - height))
            {
                y = resim.Height - height;
            }

            Brush brush = new SolidBrush(color);

            // Kesikli çizgilerle kareyi çizin
            g.FillRectangle(brush, x, y, width, height);

            // Nesneleri temizleyin
            pen.Dispose();
            g.Dispose();

            return resim;
        }

        bool noktaCizimi;
        int noktaSayac = 0;
        Bitmap yedekResim;
        List<Point> noktalar = new List<Point>();
        List<Point> sekilNoktalar = new List<Point>();

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point nokta = me.Location;

            Point yeniNokta = Oranla(pictureBox1, nokta.X, nokta.Y);

            int x = yeniNokta.X;
            int y = yeniNokta.Y;


            Bitmap resim = new Bitmap(pictureBox1.Image);

            if (noktaCizimi && noktaSayac < 4)
            {
                if (noktaSayac == 0) yedekResim = new Bitmap(pictureBox1.Image);

                Color color = Color.Red;

                noktalar.Add(new Point(x, y));

                pictureBox1.Image = NoktaCiz(resim, x, y, color);

                noktaSayac++;
            }
            else if (noktaSecme)
            {
                if (sekilNoktalar.Count == 0) yedekResim = new Bitmap(pictureBox1.Image);

                Color color = Color.Blue;

                sekilNoktalar.Add(new Point(x, y));

                pictureBox1.Image = NoktaCiz(resim, x, y, color);
            }
        }

        private Bitmap DortgenCiz(Bitmap resim, Point[] rectanglePoints)
        {
            // PictureBox'tan bir bitmap al
            Bitmap bitmap = new Bitmap(resim);

            int x0 = rectanglePoints[0].X;
            int y0 = rectanglePoints[0].Y;
            int x1 = rectanglePoints[1].X;
            int y1 = rectanglePoints[1].Y;
            int x2 = rectanglePoints[2].X;
            int y2 = rectanglePoints[2].Y;
            int x3 = rectanglePoints[3].X;
            int y3 = rectanglePoints[3].Y;

            // Dörtgeni çiz
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    g.DrawLine(pen, x0, y0, x1 + 10, y1);
                    g.DrawLine(pen, x1 + 10, y1, x2 + 10, y2 + 10);
                    g.DrawLine(pen, x2 + 10, y2 + 10, x3, y3 + 10);
                    g.DrawLine(pen, x3, y3 + 10, x0, y0);

                }
            }

            noktaSayac = 0;

            return bitmap;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.billboard2;
        }

        private Bitmap AlanBulaniklastir(Bitmap GirisResmi, int k, List<double[]> katsayilar)
        {
            Color OkunanRenk;
            Bitmap CikisResmi;
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = k; //şablon boyutu 3 den büyük tek rakam olmalıdır (3,5,7 gibi).
            int x, y, i, j, toplamR, toplamG, toplamB, ortalamaR, ortalamaG, ortalamaB;

            int h = ResimYuksekligi;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    bool kosul;
                    if (noktalar[1].X < noktalar[2].X)
                    {
                        if (noktalar[0].X < noktalar[3].X)
                            kosul = ((katsayilar[0][0] * x + katsayilar[0][1] * (h - y) + katsayilar[0][2]) > 0) &&    //<
                                  ((katsayilar[1][0] * x + katsayilar[1][1] * (h - y) + katsayilar[1][2]) > 0) &&    //>
                                  ((katsayilar[2][0] * x + katsayilar[2][1] * (h - y) + katsayilar[2][2]) < 0) &&    //>
                                  ((katsayilar[3][0] * x + katsayilar[3][1] * (h - y) + katsayilar[3][2]) < 0);      //<
                        else
                            kosul = ((katsayilar[0][0] * x + katsayilar[0][1] * (h - y) + katsayilar[0][2]) > 0) &&    //<
                              ((katsayilar[1][0] * x + katsayilar[1][1] * (h - y) + katsayilar[1][2]) > 0) &&    //>
                              ((katsayilar[2][0] * x + katsayilar[2][1] * (h - y) + katsayilar[2][2]) < 0) &&    //>
                              ((katsayilar[3][0] * x + katsayilar[3][1] * (h - y) + katsayilar[3][2]) > 0);      //<
                    }

                    else
                    {
                        if (noktalar[0].X < noktalar[3].X)
                            kosul = ((katsayilar[0][0] * x + katsayilar[0][1] * (h - y) + katsayilar[0][2]) > 0) &&    //<
                                  ((katsayilar[1][0] * x + katsayilar[1][1] * (h - y) + katsayilar[1][2]) < 0) &&    //>
                                  ((katsayilar[2][0] * x + katsayilar[2][1] * (h - y) + katsayilar[2][2]) < 0) &&    //>
                                  ((katsayilar[3][0] * x + katsayilar[3][1] * (h - y) + katsayilar[3][2]) < 0);      //<
                        else
                            kosul = ((katsayilar[0][0] * x + katsayilar[0][1] * (h - y) + katsayilar[0][2]) > 0) &&    //<
                                  ((katsayilar[1][0] * x + katsayilar[1][1] * (h - y) + katsayilar[1][2]) < 0) &&    //>
                                  ((katsayilar[2][0] * x + katsayilar[2][1] * (h - y) + katsayilar[2][2]) < 0) &&    //>
                                  ((katsayilar[3][0] * x + katsayilar[3][1] * (h - y) + katsayilar[3][2]) > 0);      //<
                    }

                    if (kosul)
                    {
                        toplamR = 0;
                        toplamG = 0;
                        toplamB = 0;

                        for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                        {
                            for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                            {
                                OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                                toplamR = toplamR + OkunanRenk.R;
                                toplamG = toplamG + OkunanRenk.G;
                                toplamB = toplamB + OkunanRenk.B;
                            }
                        }
                        ortalamaR = toplamR / (SablonBoyutu * SablonBoyutu);
                        ortalamaG = toplamG / (SablonBoyutu * SablonBoyutu);
                        ortalamaB = toplamB / (SablonBoyutu * SablonBoyutu);

                        CikisResmi.SetPixel(x, y, Color.FromArgb(ortalamaR, ortalamaG, ortalamaB));
                    }
                    else
                    {
                        CikisResmi.SetPixel(x, y, GirisResmi.GetPixel(x, y));
                    }

                }
            }
            return CikisResmi;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(yedekResim);
            if (noktaSayac < 4)
            {
                MessageBox.Show("Lutfen 4 nokta seciniz!");
                noktaSayac = 0;
                if (yedekResim != null) pictureBox1.Image = new Bitmap(yedekResim);
                noktalar.Clear();
            }
            else
            {
                List<double[]> katsayilar = new List<double[]>();

                double a; //= m;
                double b;// = -1;
                double c;// = y1 - m * x1;

                int x0, y0, x1, y1;

                int h = resim.Height;

                for (int i = 0; i < 4; i++)
                {
                    if (i == 3)
                    {
                        x0 = noktalar[3].X;
                        y0 = h - noktalar[3].Y;
                        x1 = noktalar[0].X;
                        y1 = h - noktalar[0].Y;
                    }
                    else
                    {
                        x0 = noktalar[i].X;
                        y0 = h - noktalar[i].Y;
                        x1 = noktalar[i + 1].X;
                        y1 = h - noktalar[i + 1].Y;
                    }

                    a = 1.0 * (y0 - y1) / (x0 - x1);
                    b = -1;
                    c = y1 - a * x1;

                    double[] temp = { a, b, c };

                    katsayilar.Add(temp);
                }


                //pictureBox2.Image = DortgenCiz(resim, noktalar.ToArray());
                pictureBox2.Image = AlanBulaniklastir(new Bitmap(resim), 11, katsayilar);
                noktaSayac = 0;
                pictureBox1.Image = new Bitmap(yedekResim);
                noktalar.Clear();

                //foreach (double[] dizi in katsayilar)
                //{
                //    string metin = dizi[0].ToString() + " " + dizi[1].ToString() + " " + dizi[2].ToString();
                //    MessageBox.Show(metin);
                //}
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);
            double std = Convert.ToDouble(textBox3.Text);
            pictureBox2.Image = Gauss(resim, 5, std);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string metin = "Standart sapmayı arttırdıkça bulanıklık artıyor.";
            MessageBox.Show(metin);
        }

        bool noktaSecme = false;

        private void button29_Click(object sender, EventArgs e)
        {
            if (!noktaSecme)
            {
                label12.BackColor = Color.FromArgb(192, 255, 192);
                label12.Text = "aktif";
                noktaSecme = true;
                yedekResim = new Bitmap(pictureBox1.Image);
            }
            else
            {
                label12.BackColor = Color.Pink;
                label12.Text = "pasif";
                noktaSecme = false;
                pictureBox1.Image = yedekResim;
            }
        }

        private Bitmap ArkaPlanBulaniklastir(Bitmap GirisResmi, int l, List<double[]> k)
        {
            Color OkunanRenk;
            Bitmap CikisResmi;
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            int SablonBoyutu = l; //şablon boyutu 3 den büyük tek rakam olmalıdır (3,5,7 gibi).
            int x, y, i, j, toplamR, toplamG, toplamB, ortalamaR, ortalamaG, ortalamaB;

            int h = ResimYuksekligi;

            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    bool kosul = true;
                    double deger;
                    for (int p = 0; p < k.Count(); p++)
                    {
                        deger = k[p][0] * x + k[p][1] * (h - y) + k[p][2];
                        if (k[p][0] > 0)
                        {
                            if (deger > 0)
                            {
                                kosul = false;
                                break;
                            }

                        }
                        else
                        {
                            if (deger < 0)
                            {
                                kosul = false;
                                break;
                            }
                        }
                    }

                    if (kosul)
                    {
                        CikisResmi.SetPixel(x, y, GirisResmi.GetPixel(x, y));
                    }
                    else
                    {
                        toplamR = 0;
                        toplamG = 0;
                        toplamB = 0;

                        for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                        {
                            for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                            {
                                OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                                toplamR = toplamR + OkunanRenk.R;
                                toplamG = toplamG + OkunanRenk.G;
                                toplamB = toplamB + OkunanRenk.B;
                            }
                        }
                        ortalamaR = toplamR / (SablonBoyutu * SablonBoyutu);
                        ortalamaG = toplamG / (SablonBoyutu * SablonBoyutu);
                        ortalamaB = toplamB / (SablonBoyutu * SablonBoyutu);

                        CikisResmi.SetPixel(x, y, Color.FromArgb(ortalamaR, ortalamaG, ortalamaB));
                    }
                }
            }
            return CikisResmi;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            MessageBox.Show((false || false).ToString());

            Bitmap resim = new Bitmap(yedekResim);

            if (sekilNoktalar.Count() < 2)
            {
                MessageBox.Show("1'den fazla nokta seciniz!");
                sekilNoktalar.Clear();
                if (yedekResim != null) pictureBox1.Image = new Bitmap(yedekResim);
            }
            else
            {
                List<double[]> k = new List<double[]>();

                double a; //= m;
                double b;// = -1;
                double c;// = y1 - m * x1;

                int x0, y0, x1, y1;

                int h = resim.Height;

                int len = sekilNoktalar.Count();

                for (int i = 0; i < len; i++)
                {
                    if (i == len - 1)
                    {
                        x0 = sekilNoktalar[len - 1].X;
                        y0 = h - sekilNoktalar[len - 1].Y;
                        x1 = sekilNoktalar[0].X;
                        y1 = h - sekilNoktalar[0].Y;
                    }
                    else
                    {
                        x0 = sekilNoktalar[i].X;
                        y0 = h - sekilNoktalar[i].Y;
                        x1 = sekilNoktalar[i + 1].X;
                        y1 = h - sekilNoktalar[i + 1].Y;
                    }

                    a = 1.0 * (y0 - y1) / (x0 - x1);
                    b = -1;
                    c = y1 - a * x1;

                    double[] temp = { a, b, c };

                    k.Add(temp);
                }

                pictureBox2.Image = ArkaPlanBulaniklastir(new Bitmap(resim), 11, k);
                pictureBox1.Image = new Bitmap(yedekResim);
                sekilNoktalar.Clear();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (!noktaCizimi)
            {
                label5.BackColor = Color.FromArgb(192, 255, 192);
                label5.Text = "aktif";
                noktaCizimi = true;
                yedekResim = new Bitmap(pictureBox1.Image);
            }
            else
            {
                label5.BackColor = Color.Pink;
                label5.Text = "pasif";
                noktaCizimi = false;
                pictureBox1.Image = yedekResim;
            }
        }
    }
}

