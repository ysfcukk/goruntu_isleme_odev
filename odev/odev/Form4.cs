using DevExpress.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace odev
{
    public partial class hafta4 : Form
    {
        public hafta4()
        {
            InitializeComponent();
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

            int width = 10; // Kare genişliği
            int height = 10; // Kare yüksekliği

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

        bool noktaCizimi = true;
        int noktaSayac = 0;
        Bitmap yedekResim;
        List<Point> noktalar = new List<Point>();
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point nokta = me.Location;

            Point yeniNokta = Oranla(pictureBox1, nokta.X, nokta.Y);

            int x = yeniNokta.X;
            int y = yeniNokta.Y;

            if (noktaSayac == 0) yedekResim = new Bitmap(pictureBox1.Image);

            Bitmap resim = new Bitmap(pictureBox1.Image);

            if (noktaCizimi && noktaSayac < 8)
            {
                Color color;

                if (noktaSayac < 4) color = Color.Red;
                else color = Color.White;

                noktalar.Add(new Point(x, y));

                pictureBox1.Image = NoktaCiz(resim, x, y, color);

                noktaSayac++;
            }
        }
        public void PerspektifDuzelt(double a00, double a01, double a02, double a10, double a11, double a12, double a20, double a21, double a22)
        {
            Bitmap GirisResmi, CikisResmi; Color OkunanRenk;
            GirisResmi = new Bitmap(yedekResim);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            double X, Y, z;
            for (int x = 0; x < (ResimGenisligi); x++)
            {
                for (int y = 0; y < (ResimYuksekligi); y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);
                    z = a20 * x + a21 * y + 1;
                    X = (a00 * x + a01 * y + a02) / z; Y = (a10 * x + a11 * y + a12) / z;
                    if (X > 0 && X < ResimGenisligi && Y > 0 && Y < ResimYuksekligi)
                        //Picturebox ın dışına çıkan kısımlar oluşturulmayacak.
                        CikisResmi.SetPixel((int)X, (int)Y, OkunanRenk);
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        public double[,] MatrisTersiniAl(double[,] GirisMatrisi)
        {
            int MatrisBoyutu = Convert.ToInt16(Math.Sqrt(GirisMatrisi.Length));

            double[,] CikisMatrisi = new double[MatrisBoyutu, MatrisBoyutu];

            for (int i = 0; i < MatrisBoyutu; i++)
            {
                for (int j = 0; j < MatrisBoyutu; j++)
                {
                    if (i == j)
                        CikisMatrisi[i, j] = 1;
                    else
                        CikisMatrisi[i, j] = 0;
                }
            }
            double d, k;
            for (int i = 0; i < MatrisBoyutu; i++)
            {
                d = GirisMatrisi[i, i];


                for (int j = 0; j < MatrisBoyutu; j++)
                {
                    if (d == 0)
                    {
                        d = 0.0001; //0 bölme hata veriyordu.
                    }
                    GirisMatrisi[i, j] = GirisMatrisi[i, j] / d; CikisMatrisi[i, j] = CikisMatrisi[i, j] / d;
                }

                for (int x = 0; x < MatrisBoyutu; x++)
                {
                    if (x != i)
                    {
                        k = GirisMatrisi[x, i];
                        for (int j = 0; j < MatrisBoyutu; j++)
                        {
                            GirisMatrisi[x, j] = GirisMatrisi[x, j] - GirisMatrisi[i, j] * k; CikisMatrisi[x, j] = CikisMatrisi[x, j] - CikisMatrisi[i, j] * k;
                        }
                    }
                }
            }
            return CikisMatrisi;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (noktaSayac < 8)
            {
                MessageBox.Show("Lutfen 8 nokta seciniz!");
                noktaSayac = 0;
                if (yedekResim != null) pictureBox1.Image = new Bitmap(yedekResim);
                noktalar.Clear();
            }
            else
            {
                double x1 = Convert.ToDouble(noktalar[0].X);
                double y1 = Convert.ToDouble(noktalar[0].Y);
                double x2 = Convert.ToDouble(noktalar[1].X);
                double y2 = Convert.ToDouble(noktalar[1].Y);
                double x3 = Convert.ToDouble(noktalar[2].X);
                double y3 = Convert.ToDouble(noktalar[2].Y);
                double x4 = Convert.ToDouble(noktalar[3].X);
                double y4 = Convert.ToDouble(noktalar[3].Y);

                double X1 = Convert.ToDouble(noktalar[4].X);
                double Y1 = Convert.ToDouble(noktalar[4].Y);
                double X2 = Convert.ToDouble(noktalar[5].X);
                double Y2 = Convert.ToDouble(noktalar[5].Y);
                double X3 = Convert.ToDouble(noktalar[6].X);
                double Y3 = Convert.ToDouble(noktalar[6].Y);
                double X4 = Convert.ToDouble(noktalar[7].X);
                double Y4 = Convert.ToDouble(noktalar[7].Y);

                double[,] GirisMatrisi = new double[8, 8];

                GirisMatrisi[0, 0] = x1;
                GirisMatrisi[0, 1] = y1;
                GirisMatrisi[0, 2] = 1;
                GirisMatrisi[0, 3] = 0;
                GirisMatrisi[0, 4] = 0;
                GirisMatrisi[0, 5] = 0;
                GirisMatrisi[0, 6] = -x1 * X1;
                GirisMatrisi[0, 7] = -y1 * Y1;

                GirisMatrisi[1, 0] = 0;
                GirisMatrisi[1, 1] = 0;
                GirisMatrisi[1, 2] = 0;
                GirisMatrisi[1, 3] = x1;
                GirisMatrisi[1, 4] = y1;
                GirisMatrisi[1, 5] = 1;
                GirisMatrisi[1, 6] = -x1 * Y1;
                GirisMatrisi[1, 7] = -y1 * Y1;

                ////
                ///
                GirisMatrisi[2, 0] = x2;
                GirisMatrisi[2, 1] = y2;
                GirisMatrisi[2, 2] = 1;
                GirisMatrisi[2, 3] = 0;
                GirisMatrisi[2, 4] = 0;
                GirisMatrisi[2, 5] = 0;
                GirisMatrisi[2, 6] = -x2 * X2;
                GirisMatrisi[2, 7] = -y2 * X2;

                GirisMatrisi[3, 0] = 0;
                GirisMatrisi[3, 1] = 0;
                GirisMatrisi[3, 2] = 0;
                GirisMatrisi[3, 3] = x2;
                GirisMatrisi[3, 4] = y2;
                GirisMatrisi[3, 5] = 1;
                GirisMatrisi[3, 6] = -x2 * Y2;
                GirisMatrisi[3, 7] = -y2 * Y2;

                GirisMatrisi[4, 0] = x3;
                GirisMatrisi[4, 1] = y3;
                GirisMatrisi[4, 2] = 1;
                GirisMatrisi[4, 3] = 0;
                GirisMatrisi[4, 4] = 0;
                GirisMatrisi[4, 5] = 0;
                GirisMatrisi[4, 6] = -x3 * X3;
                GirisMatrisi[4, 7] = -y3 * X3;

                GirisMatrisi[5, 0] = 0;
                GirisMatrisi[5, 1] = 0;
                GirisMatrisi[5, 2] = 0;
                GirisMatrisi[5, 3] = x3;
                GirisMatrisi[5, 4] = y3;
                GirisMatrisi[5, 5] = 1;
                GirisMatrisi[5, 6] = -x3 * Y3;
                GirisMatrisi[5, 7] = -y3 * Y3;

                GirisMatrisi[6, 0] = x4;
                GirisMatrisi[6, 1] = y4;
                GirisMatrisi[6, 2] = 1;
                GirisMatrisi[6, 3] = 0;
                GirisMatrisi[6, 4] = 0;
                GirisMatrisi[6, 5] = 0;
                GirisMatrisi[6, 6] = -x4 * X4;
                GirisMatrisi[6, 7] = -y4 * X4;

                GirisMatrisi[7, 0] = 0;
                GirisMatrisi[7, 1] = 0;
                GirisMatrisi[7, 2] = 0;
                GirisMatrisi[7, 3] = x4;
                GirisMatrisi[7, 4] = y4;
                GirisMatrisi[7, 5] = 1;
                GirisMatrisi[7, 6] = -x4 * Y4;
                GirisMatrisi[7, 7] = -y4 * Y4;

                double[,] matrisBTersi = MatrisTersiniAl(GirisMatrisi);

                double a00 = 0, a01 = 0, a02 = 0, a10 = 0, a11 = 0, a12 = 0, a20 = 0, a21 = 0, a22 = 0;

                a00 = matrisBTersi[0, 0] * X1 + matrisBTersi[0, 1] * Y1 + matrisBTersi[0, 2] * X2 + matrisBTersi[0, 3] * Y2 +
                matrisBTersi[0, 4] * X3 + matrisBTersi[0, 5] * Y3 + matrisBTersi[0, 6] * X4 + matrisBTersi[0, 7] * Y4;

                a01 = matrisBTersi[1, 0] * X1 + matrisBTersi[1, 1] * Y1 + matrisBTersi[1, 2] * X2 + matrisBTersi[1, 3] * Y2 +
                matrisBTersi[1, 4] * X3 + matrisBTersi[1, 5] * Y3 + matrisBTersi[1, 6] * X4 + matrisBTersi[1, 7] * Y4;

                a02 = matrisBTersi[2, 0] * X1 + matrisBTersi[2, 1] * Y1 + matrisBTersi[2, 2] * X2 + matrisBTersi[2, 3] * Y2 +
                matrisBTersi[2, 4] * X3 + matrisBTersi[2, 5] * Y3 + matrisBTersi[2, 6] * X4 + matrisBTersi[2, 7] * Y4;

                a10 = matrisBTersi[3, 0] * X1 + matrisBTersi[3, 1] * Y1 + matrisBTersi[3, 2] * X2 + matrisBTersi[3, 3] * Y2 +
                matrisBTersi[3, 4] * X3 + matrisBTersi[3, 5] * Y3 + matrisBTersi[3, 6] * X4 + matrisBTersi[3, 7] * Y4;

                a11 = matrisBTersi[4, 0] * X1 + matrisBTersi[4, 1] * Y1 + matrisBTersi[4, 2] * X2 + matrisBTersi[4, 3] * Y2 +
                matrisBTersi[4, 4] * X3 + matrisBTersi[4, 5] * Y3 + matrisBTersi[4, 6] * X4 + matrisBTersi[4, 7] * Y4;

                a12 = matrisBTersi[5, 0] * X1 + matrisBTersi[5, 1] * Y1 + matrisBTersi[5, 2] * X2 + matrisBTersi[5, 3] * Y2 +
                matrisBTersi[5, 4] * X3 + matrisBTersi[5, 5] * Y3 + matrisBTersi[5, 6] * X4 + matrisBTersi[5, 7] * Y4;

                a20 = matrisBTersi[6, 0] * X1 + matrisBTersi[6, 1] * Y1 + matrisBTersi[6, 2] * X2 + matrisBTersi[6, 3] * Y2 +
                matrisBTersi[6, 4] * X3 + matrisBTersi[6, 5] * Y3 + matrisBTersi[6, 6] * X4 + matrisBTersi[6, 7] * Y4;

                a21 = matrisBTersi[7, 0] * X1 + matrisBTersi[7, 1] * Y1 + matrisBTersi[7, 2] * X2 + matrisBTersi[7, 3] * Y2 +
                matrisBTersi[7, 4] * X3 + matrisBTersi[7, 5] * Y3 + matrisBTersi[7, 6] * X4 + matrisBTersi[7, 7] * Y4;

                a22 = 1;

                PerspektifDuzelt(a00, a01, a02, a10, a11, a12, a20, a21, a22);

                noktaSayac = 0;
                pictureBox1.Image = new Bitmap(yedekResim);
                noktalar.Clear();
            }
        }

        private Bitmap AliasDuzelt(Bitmap GirisResmi)
        {
            //Image originalImage = new Bitmap(GirisResmi);

            //// Anti-aliasing uygulamak için bir Bitmap ve Graphics oluşturun
            //Bitmap antiAliasedBitmap = new Bitmap(originalImage.Width, originalImage.Height);
            //using (Graphics g = Graphics.FromImage(antiAliasedBitmap))
            //{
            //    // Anti-aliasing özelliklerini ayarlayın
            //    g.SmoothingMode = SmoothingMode.AntiAlias;
            //    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //    // Resmi orijinal boyutuna çiz
            //    g.DrawImage(originalImage, new Rectangle(0, 0, antiAliasedBitmap.Width, antiAliasedBitmap.Height));
            //}

            //return antiAliasedBitmap;

            Bitmap CikisResmi;
            Color OkunanRenk;

            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;

            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            Color ortalamaRenk;

            for (int x = 0; x < (ResimGenisligi); x++)
            {
                for (int y = 0; y < (ResimYuksekligi); y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);

                    if (OkunanRenk == Color.FromArgb(0,0,0,0))
                    {
                        ortalamaRenk = OrtalamaRenkGetir(GirisResmi, x, y);
                        CikisResmi.SetPixel(x, y, ortalamaRenk);
                    }
                    else CikisResmi.SetPixel(x, y, OkunanRenk);
                }
            }
            return CikisResmi;
        }

        private Color OrtalamaRenkGetir(Bitmap GirisResmi, int x, int y)
        {
            int deger = 3;
            int totalRed = 0;
            int totalGreen = 0;
            int totalBlue = 0;
            int count = 0;

            Color renk = GirisResmi.GetPixel(x, y);

            for (int i = x; i < GirisResmi.Width; i++)
            {
                for (int j = y; j < GirisResmi.Height; j++)
                {
                    renk = GirisResmi.GetPixel(i, j);
                    if (renk!=Color.FromArgb(0,0,0,0))
                    {
                        return renk;
                    }
                }
            }

            return renk;


            //for (int i = x - deger; i <= x + deger; i++)
            //{
            //    for (int j = y - deger; j <= y + deger; j++)
            //    {
            //        if (i >= 0 && i < GirisResmi.Width && j >= 0 && j < GirisResmi.Height)
            //        {
            //            Color neighborColor = GirisResmi.GetPixel(i, j);
            //            totalRed += neighborColor.R;
            //            totalGreen += neighborColor.G;
            //            totalBlue += neighborColor.B;
            //            count++;
            //        }
            //    }
            //}
            //int averageRed = totalRed / count;
            //int averageGreen = totalGreen / count;
            //int averageBlue = totalBlue / count;
            //return Color.FromArgb(averageRed, averageGreen, averageBlue);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap resim;
            if (pictureBox2.Image != null)
            {
                resim = new Bitmap(pictureBox2.Image);
                pictureBox2.Image = AliasDuzelt(resim);
            }
            else MessageBox.Show("PictureBox2 Boş!");

        }

        private void button12_Click(object sender, EventArgs e)
        {
            pictureBox2.SizeMode = PictureBoxSizeMode.Normal;
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Alias giderme işlemi için kendim bir yöntem denedim. Bu yöntemde aliaslı bir piksele;" +
                " sağındaki en yakın aliaslı olmayan pikselin rengini verdim. Bazen ufak hatalar olsa da, sonuç fena olmadı bence.");
        }
    }
}
