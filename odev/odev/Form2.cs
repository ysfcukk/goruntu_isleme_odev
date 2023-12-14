using DevExpress.Data.Linq.Helpers;
using DevExpress.XtraExport.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using static DevExpress.Skins.SolidColorHelper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace odev
{
    public partial class hafta2 : Form
    {
        bool sekil_cizimi = false;
        bool kapali_alan_cizimi = false;

        List<Point> pointList = new List<Point>();

        int kare_X = 0;
        int kare_Y = 0;
        int kare_Z = 0;


        public hafta2()
        {
            InitializeComponent();
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

        public void ResminHistograminiCiz(Bitmap resim, string renk, PictureBox pictureBox)
        {
            ArrayList DiziPiksel = new ArrayList();
            int OrtalamaRenk = 0;
            Color OkunanRenk;
            Bitmap GirisResmi;

            GirisResmi = resim;

            int ResimGenisligi = GirisResmi.Width; //GirisResmi global tanımlandı.
            int ResimYuksekligi = GirisResmi.Height;

            for (int x = 0; x < GirisResmi.Width; x++)
            {
                for (int y = 0; y < GirisResmi.Height; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);
                    if (renk == "gri")
                    {
                        OrtalamaRenk = (int)(OkunanRenk.R + OkunanRenk.G + OkunanRenk.B) / 3; //Griton resimde üç kanal rengi aynı değere sahiptir.
                        DiziPiksel.Add(OrtalamaRenk); //Resimdeki tüm noktaları diziye atıyor.
                    }
                    else if (renk == "kirmizi")
                    {
                        DiziPiksel.Add(OkunanRenk.R);
                    }
                    else if (renk == "yesil")
                    {
                        DiziPiksel.Add(OkunanRenk.G);
                    }
                    else if (renk == "mavi")
                    {
                        DiziPiksel.Add(OkunanRenk.B);
                    }
                }
            }
            int[] DiziPikselSayilari = new int[256];
            for (int r = 0; r <= 255; r++) //256 tane renk tonu için dönecek.
            {
                int PikselSayisi = 0;
                for (int s = 0; s < DiziPiksel.Count; s++) //resimdeki piksel sayısınca dönecek.
                {
                    if (r == Convert.ToInt16(DiziPiksel[s]))
                        PikselSayisi++;
                }
                DiziPikselSayilari[r] = PikselSayisi;
            }
            //Değerleri listbox'a ekliyor.
            int RenkMaksPikselSayisi = 0; //Grafikte y eksenini ölçeklerken kullanılacak.


            for (int k = 0; k <= 255; k++)
            {

                if (renk == "gri") listBox1.Items.Add("Renk:" + k + "=" + DiziPikselSayilari[k]);
                //Maksimum piksel sayısını bulmaya çalışıyor.
                if (DiziPikselSayilari[k] > RenkMaksPikselSayisi)
                {
                    RenkMaksPikselSayisi = DiziPikselSayilari[k];
                }
            }
            if (renk == "gri") textBox1.Text = "Maks.Piks=" + RenkMaksPikselSayisi.ToString();

            //Grafiği çiziyor.
            Graphics CizimAlani;
            Pen Kalem1 = new Pen(System.Drawing.Color.Yellow, 1);
            Pen Kalem2 = new Pen(System.Drawing.Color.Red, 1);
            CizimAlani = pictureBox.CreateGraphics();

            pictureBox.Refresh();
            int GrafikYuksekligi = pictureBox.Height - 1;
            double OlcekY = 1.0 * RenkMaksPikselSayisi / GrafikYuksekligi;
            double OlcekX = 1;
            int X_kaydirma = 0;
            for (int x = 0; x <= 255; x++)
            {
                if (x % 50 == 0)
                    CizimAlani.DrawLine(Kalem2, (int)(X_kaydirma + x * OlcekX),
                    GrafikYuksekligi, (int)(X_kaydirma + x * OlcekX), 0);
                CizimAlani.DrawLine(Kalem1, (int)(X_kaydirma + x * OlcekX), GrafikYuksekligi,
                (int)(X_kaydirma + x * OlcekX), (GrafikYuksekligi - (int)(DiziPikselSayilari[x] / OlcekY)));
                //Dikey kırmızı çizgiler.
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
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


        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox1.Image = GriyeDonustur(resim);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.DefaultExt = ".jpg";
            file.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            file.ShowDialog();
            pictureBox1.ImageLocation = file.FileName;
        }

        static Bitmap Esikle(Bitmap resim, int a, int b, string c)
        { 

            int min = 0;
            int max = 0;

            if (a > b)
            {
                max = a;
                min = b;
            }
            else
            {
                max = b;
                min = a;
            }

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

                    if (c == "gri")
                    {
                        int renk = OkunanRenk.R;

                        if (renk < min || renk > max)
                        {
                            R = 0; G = 0; B = 0;
                        }
                        else
                        {
                            R = renk; G = renk; B = renk;
                        }
                    }

                    else if (c == "odev3")
                    {
                        double renk = OkunanRenk.R;
                        renk = min + ((renk / 255) * (max - min));
                        R = (int)(renk); G = (int)(renk); B = (int)(renk);
                    }

                    DonusenRenk = Color.FromArgb(R, G, B);
                    CikisResmi.SetPixel(x, y, DonusenRenk);
                }
            }
            return (CikisResmi);

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar2.Value.ToString();
            label2.Text = trackBar3.Value.ToString();
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = Esikle(resim, trackBar2.Value, trackBar3.Value, "gri");
        }

        private void hafta2_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.picture3;
            SkalaOlustur();
        }

        private Bitmap RenkliEsikle(Bitmap resim, int rx1, int rx2, int gx1, int gx2, int bx1, int bx2)
        {
            int R = 0, G = 0, B = 0;

            Color OkunanRenk, DonusenRenk;

            int ResimGenisligi = resim.Width;
            int ResimYuksekligi = resim.Height;

            Bitmap CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            int rmin = rx1;
            int rmax = rx2;
            int gmin = gx1;
            int gmax = gx2;
            int bmin = bx1;
            int bmax = bx2;

            if (rx1 > rx2) { rmin = rx2; rmax = rx1; }
            if (gx1 > gx2) { gmin = gx2; gmax = gx1; }
            if (bx1 > bx2) { bmin = bx2; bmax = bx1; }

            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk = resim.GetPixel(x, y);

                    R = OkunanRenk.R;
                    G = OkunanRenk.G;
                    B = OkunanRenk.B;

                    if (OkunanRenk.R < rmin || OkunanRenk.R > rmax) R = 0;
                    if (OkunanRenk.G < gmin || OkunanRenk.G > gmax) G = 0;
                    if (OkunanRenk.B < bmin || OkunanRenk.B > bmax) B = 0;

                    CikisResmi.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }
            return CikisResmi;
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            label4.Text = trackBar5.Value.ToString();
            label3.Text = trackBar4.Value.ToString();
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = RenkliEsikle(resim, trackBar5.Value, trackBar4.Value, trackBar13.Value, trackBar14.Value,
                                                    trackBar16.Value, trackBar15.Value);


        }

        private void trackBar13_Scroll(object sender, EventArgs e)
        {
            label7.Text = trackBar13.Value.ToString();
            label8.Text = trackBar14.Value.ToString();
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = RenkliEsikle(resim, trackBar5.Value, trackBar4.Value, trackBar13.Value, trackBar14.Value,
                                                    trackBar16.Value, trackBar15.Value);
        }

        private void trackBar16_Scroll(object sender, EventArgs e)
        {
            label30.Text = trackBar16.Value.ToString();
            label31.Text = trackBar15.Value.ToString();
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = RenkliEsikle(resim, trackBar5.Value, trackBar4.Value, trackBar13.Value, trackBar14.Value,
                                                    trackBar16.Value, trackBar15.Value);
        }

        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = Esikle(resim, 0, trackBar6.Value, "odev3");
            listBox1.Items.Clear();
            ResminHistograminiCiz(new Bitmap(pictureBox2.Image), "gri", pictureBox3);
        }

        private void trackBar7_Scroll(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = Esikle(resim, trackBar7.Value, trackBar8.Value, "odev3");
            listBox1.Items.Clear();
            ResminHistograminiCiz(new Bitmap(pictureBox2.Image), "gri", pictureBox3);
        }

        private void trackBar8_Scroll(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = Esikle(resim, trackBar7.Value, trackBar8.Value, "odev3");
            listBox1.Items.Clear();
            ResminHistograminiCiz(new Bitmap(pictureBox2.Image), "gri", pictureBox3);
        }

        private int RenkFarki(Color renk1, Color renk2)
        {
            int r = Math.Abs(renk1.R - renk2.R);
            int g = Math.Abs(renk1.G - renk2.G);
            int b = Math.Abs(renk1.B - renk2.B);

            return r + g + b;
        }
        List<Point> gezilenNoktalar = new List<Point>();
        private void KapaliAlanCiz(Bitmap GirisResmi, int x, int y)
        {
            gezilenNoktalar.Add(new Point(x, y));


            Color renk = GirisResmi.GetPixel(x, y);
            Color renk_sol = GirisResmi.GetPixel(x - 1, y);
            Color renk_ust = GirisResmi.GetPixel(x, y - 1);
            Color renk_sag = GirisResmi.GetPixel(x + 1, y);
            Color renk_alt = GirisResmi.GetPixel(x, y + 1);

            bool sol = true, ust = true, sag = true, alt = true, noktaEklendi = false;

            if (RenkFarki(renk, renk_sol) > 200)
            {
                pointList.Add(new Point(x, y));
                sol = false;
                noktaEklendi = true;
            }
            if (RenkFarki(renk, renk_ust) > 200)
            {
                if (!noktaEklendi) pointList.Add(new Point(x, y));
                ust = false;
                noktaEklendi = true;
            }
            if (RenkFarki(renk, renk_sag) > 200)
            {
                if (!noktaEklendi) pointList.Add(new Point(x, y));
                sag = false;
                noktaEklendi = true;
            }
            if (RenkFarki(renk, renk_alt) > 200)
            {
                if (!noktaEklendi) pointList.Add(new Point(x, y));
                alt = false;
                noktaEklendi = true;
            }

            if (sol && !gezilenNoktalar.Contains(new Point(x - 1, y)))
                KapaliAlanCiz(GirisResmi, x - 1, y);
            else if (ust && !gezilenNoktalar.Contains(new Point(x, y - 1)))
                KapaliAlanCiz(GirisResmi, x, y - 1);
            else if (sag && !gezilenNoktalar.Contains(new Point(x + 1, y)))
                KapaliAlanCiz(GirisResmi, x + 1, y);
            else if (alt && !gezilenNoktalar.Contains(new Point(x, y + 1)))
                KapaliAlanCiz(GirisResmi, x, y + 1);

            else return;

        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point nokta = me.Location;

            int x = nokta.X;
            int y = nokta.Y;

            Bitmap resim1 = new Bitmap(pictureBox2.Image);

            int p_w = pictureBox2.Width;
            int p_h = pictureBox2.Height;
            int r_w = resim1.Width;
            int r_h = resim1.Height;

            int x1 = x;
            int y1 = y;

            if (r_h > p_h)
            {
                y = (int)Math.Round(y * (1.0 * r_h / p_h));
            }
            else
            {
                y = (int)Math.Round(y * (1.0 * r_h / p_h));
            }
            if (r_w > p_w)
            {
                x = (int)Math.Round(x * (1.0 * r_w / p_w));
            }
            else
            {
                x = (int)Math.Round(x * (1.0 * r_w / p_w));
            }

            if (x < 0) x = 0;
            if (y < 0) y = 0;

            //MessageBox.Show(x.ToString() + "," + y.ToString());

            if (sekil_cizimi)
            {
                Bitmap resim;
                resim = new Bitmap(pictureBox1.Image);
                pictureBox2.Image = KareCiz(resim, x, y, 70);
            }
            else if (kapali_alan_cizimi)
            {
                Bitmap resim = new Bitmap(pictureBox2.Image);
                KapaliAlanCiz(resim, x, y);
                pointList.Add(new Point(x, y));
                Graphics gr = Graphics.FromImage(resim);
                Pen pen = new Pen(Color.Red); // Çizgi rengi
                pen.DashStyle = DashStyle.Dash;

                foreach (Point p in pointList)
                {
                    gr.FillEllipse(Brushes.Black, p.X, p.Y, 2, 2);
                }

                // Nesneleri temizleyin
                pen.Dispose();
                gr.Dispose();

                pictureBox2.Image = resim;
            }

            if (pictureBox2.Image != null)
            {

                Color okunanRenk;

                okunanRenk = resim1.GetPixel(x, y);

                string renk = "R:" + okunanRenk.R + " , " +
                              "G:" + okunanRenk.G + " , " +
                              "B:" + okunanRenk.B;

                textBox2.Text = "(" + x1.ToString() + "," + y1.ToString() + ") -> " + renk;
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = GriyeDonustur(new Bitmap(pictureBox1.Image));
            listBox1.Items.Clear();
            ResminHistograminiCiz(new Bitmap(pictureBox2.Image), "gri", pictureBox3);
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = e.X;
                if (x > 255)
                {
                    textBox3.Text = "255";
                    textBox10.Text = "255";
                    if (sekil_cizimi) textBox20.Text = "255";
                }
                else
                {
                    textBox3.Text = e.X.ToString();
                    textBox10.Text = e.X.ToString();
                    if (sekil_cizimi) textBox20.Text = e.X.ToString();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                int x = e.X;
                if (x > 255)
                {
                    textBox4.Text = "255";
                    textBox9.Text = "255";
                    if (sekil_cizimi) textBox19.Text = "255";
                }
                else
                {
                    textBox4.Text = e.X.ToString();
                    textBox9.Text = e.X.ToString();
                    if (sekil_cizimi) textBox19.Text = e.X.ToString();
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //pictureBox1.Image = Properties.Resources.picture3;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;
            pictureBox5.Image = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt16(textBox3.Text);
            int x2 = Convert.ToInt16(textBox4.Text);

            int y1 = Convert.ToInt16(textBox6.Text);
            int y2 = Convert.ToInt16(textBox5.Text);

            Bitmap resim = new Bitmap(pictureBox1.Image);

            pictureBox2.Image = KontrastUygula(resim, "gri", x1, x2, y1, y2);
        }

        private void button6_Click(object sender, EventArgs e)
        {

            int rx1 = Convert.ToInt16(textBox10.Text);
            int rx2 = Convert.ToInt16(textBox9.Text);

            int gx1 = Convert.ToInt16(textBox8.Text);
            int gx2 = Convert.ToInt16(textBox7.Text);

            int bx1 = Convert.ToInt16(textBox14.Text);
            int bx2 = Convert.ToInt16(textBox13.Text);


            Bitmap resim = new Bitmap(pictureBox1.Image);

            resim = KontrastUygula(resim, "kirmizi", rx1, rx2, 0, 255);
            resim = KontrastUygula(resim, "yesil", gx1, gx2, 0, 255);
            resim = KontrastUygula(resim, "mavi", bx1, bx2, 0, 255);

            pictureBox2.Image = resim;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Color OkunanRenk, DonusenRenk;
            int R = 0, G = 0, B = 0;

            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);

            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;

            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            double c = Convert.ToDouble(textBox11.Text);
            double f = (259 * (c + 255)) / (255 * (259 - c));

            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);

                    R = OkunanRenk.R;
                    G = OkunanRenk.G;
                    B = OkunanRenk.B;

                    double r = f * (double)(R - 128) + 128;
                    double g = f * (double)(G - 128) + 128;
                    double b = f * (double)(B - 128) + 128;

                    if (r < 0) r = 0;
                    else if (r > 255) r = 255;
                    if (g < 0) g = 0;
                    else if (g > 255) g = 255;
                    if (b < 0) b = 0;
                    else if (b > 255) b = 255;


                    DonusenRenk = Color.FromArgb((int)r, (int)g, (int)b);

                    CikisResmi.SetPixel(x, y, DonusenRenk);
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        private Bitmap KareCiz(Bitmap resim, int x, int y, int z)
        {
            
            // Kesikli çizgiyi çizecek nesneyi oluşturun
            Graphics g = Graphics.FromImage(resim);

            // Çizgi özelliklerini ayarlayın
            Pen pen = new Pen(Color.Red); // Çizgi rengi
            pen.DashStyle = DashStyle.Dash; // Kesikli çizgi stili
                                            // Kare sol üst köşesinin X koordinatı // Kare sol üst köşesinin Y koordinatı

            int width = z; // Kare genişliği
            int height = z; // Kare yüksekliği

            if (x > (resim.Width - width))
            {
                x = resim.Width - width;
            }

            if (y > (resim.Height - height))
            {
                y = resim.Height - height;
            }

            // Kesikli çizgilerle kareyi çizin
            g.DrawRectangle(pen, x, y, width, height);

            // Nesneleri temizleyin
            pen.Dispose();
            g.Dispose();

            kare_X = x;
            kare_Y = y;
            kare_Z = z;

            trackBar9.Value = 0;
            trackBar9.Enabled = true;
            trackBar10.Value = 0;
            trackBar10.Enabled = true;
            trackBar11.Value = 0;
            trackBar11.Enabled = true;
            trackBar12.Value = 0;
            trackBar12.Enabled = true;

            button9.Enabled = true;
            textBox12.Enabled = true;

            textBox15.Enabled = true;
            textBox16.Enabled = true;
            textBox17.Enabled = true;
            textBox18.Enabled = true;
            textBox19.Enabled = true;
            textBox20.Enabled = true;
            button21.Enabled = true;
            button22.Enabled = true;

            return resim;
        }

        private void SekilCizimiPasiflestir()
        {
            trackBar9.Value = 0;
            trackBar9.Enabled = false;
            trackBar10.Value = 0;
            trackBar10.Enabled = false;
            trackBar11.Value = 0;
            trackBar11.Enabled = false;
            trackBar12.Value = 0;
            trackBar12.Enabled = false;

            button9.Enabled = false;
            textBox12.Enabled = false;

            textBox15.Enabled = false;
            textBox16.Enabled = false;
            textBox17.Enabled = false;
            textBox18.Enabled = false;
            textBox19.Enabled = false;
            textBox20.Enabled = false;
            button21.Enabled = false;
            button22.Enabled = false;

            sekil_cizimi = false;

            label20.Text = "pasif";
            label20.BackColor = Color.Pink;

            pictureBox2.Image = pictureBox1.Image;
        }

        private void SekilCizimiAktiflestir()
        {
            sekil_cizimi = true;
            label20.Text = "aktif";
            label20.BackColor = Color.FromArgb(192, 255, 192);
            if (pictureBox2.Image == null) pictureBox2.Image = pictureBox1.Image;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (sekil_cizimi)
            {
                SekilCizimiPasiflestir();
            }

            else
            {
                KapaliAlanCizimiPasiflestir();
                SekilCizimiAktiflestir();
            }
        }

        private void trackBar9_Scroll(object sender, EventArgs e)
        {
            int value = trackBar9.Value;

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
                    R = OkunanRenk.R;
                    G = OkunanRenk.G;
                    B = OkunanRenk.B;

                    if (x >= kare_X && x < kare_X + kare_Z && y >= kare_Y && y < kare_Y + kare_Z)
                    {
                        R = R + value;
                        G = G + value;
                        B = B + value;

                        if (R > 255) R = 255;
                        if (G > 255) G = 255;
                        if (B > 255) B = 255;

                        if (R < 0) R = 0;
                        if (G < 0) G = 0;
                        if (B < 0) B = 0;
                    }

                    DonusenRenk = Color.FromArgb(R, G, B); CikisResmi.SetPixel(x, y, DonusenRenk);
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Color OkunanRenk, DonusenRenk;
            double R = 0.0, G = 0.0, B = 0.0;

            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);

            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;

            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            double c = Convert.ToDouble(textBox12.Text);
            double f = (259 * (c + 255)) / (255 * (259 - c));

            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);

                    R = OkunanRenk.R;
                    G = OkunanRenk.G;
                    B = OkunanRenk.B;

                    if (x >= kare_X && x < kare_X + kare_Z && y >= kare_Y && y < kare_Y + kare_Z)
                    {
                        R = f * (double)(R - 128) + 128;
                        G = f * (double)(G - 128) + 128;
                        B = f * (double)(B - 128) + 128;

                        if (R < 0) R = 0;
                        else if (R > 255) R = 255;
                        if (G < 0) G = 0;
                        else if (G > 255) G = 255;
                        if (B < 0) B = 0;
                        else if (B > 255) B = 255;
                    }

                    DonusenRenk = Color.FromArgb((int)R, (int)G, (int)B);

                    CikisResmi.SetPixel(x, y, DonusenRenk);
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        private void trackBar10_Scroll(object sender, EventArgs e)
        {
            int r_value = trackBar10.Value;
            int g_value = trackBar11.Value;
            int b_value = trackBar12.Value;

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
                    R = OkunanRenk.R;
                    G = OkunanRenk.G;
                    B = OkunanRenk.B;

                    if (x >= kare_X && x < kare_X + kare_Z && y >= kare_Y && y < kare_Y + kare_Z)
                    {
                        R = R + r_value;
                        G = G + g_value;
                        B = B + b_value;

                        if (R > 255) R = 255;
                        if (G > 255) G = 255;
                        if (B > 255) B = 255;

                        if (R < 0) R = 0;
                        if (G < 0) G = 0;
                        if (B < 0) B = 0;
                    }

                    DonusenRenk = Color.FromArgb(R, G, B); CikisResmi.SetPixel(x, y, DonusenRenk);
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        private void KapaliAlanCizimiPasiflestir()
        {
            kapali_alan_cizimi = false;
            //label27.Text = "pasif";
            //label27.BackColor = Color.Pink;
        }

        private void KapaliAlanCizimiAktiflestir()
        {
            kapali_alan_cizimi = true;
            //label27.Text = "aktif";
            //label27.BackColor = Color.FromArgb(192, 255, 192);
            pictureBox2.Image = pictureBox1.Image;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (kapali_alan_cizimi)
            {
                KapaliAlanCizimiPasiflestir();
            }
            else
            {
                SekilCizimiPasiflestir();
                KapaliAlanCizimiAktiflestir();
            }
            //KapaliAlanCiz(GirisResmi,x,y);

        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null) pictureBox2.Image = pictureBox1.Image;
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Histogramlar hangi renklerin daha baskın olduğunu, renk dengesinin nasıl olduğunu gösterir." +
                " Bu da; Kontrast, Parlaklık, Renk Düzeltme, Segmentasyon gibi işlemler için kullanışlıdır. ");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            // 215 -> 225,210,150
            // 160 -> 120,165,60
            // 190 -> 165,120,90
            // 235 -> 90,190,255
            // 80 -> 155,95,50

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

                    if (r >= 230)
                    {
                        r = 90;
                        g = 190;
                        b = 255;
                    }
                    else if (r >= 200 && r < 230)
                    {
                        r = 225;
                        g = 210;
                        b = 150;
                    }
                    else if (r >= 185 && r < 200)
                    {
                        r = 135; //165
                        g = 150; //120
                        b = 100; //90
                    }
                    else if (r >= 165 && r < 185)
                    {
                        r = 120;
                        g = 165;
                        b = 60;
                    }
                    else if (r >= 135 && r < 165)
                    {
                        r = 90;
                        g = 130;
                        b = 40;
                    }
                    else if (r >= 100 && r < 135)
                    {
                        r = 145;
                        g = 100;
                        b = 90;
                    }
                    else if (r >= 80 && r < 100)
                    {
                        r = 220;
                        g = 175;
                        b = 140;
                    }
                    else if (r >= 55 && r < 80)
                    {
                        r = 155;
                        g = 95;
                        b = 80;
                    }
                    else if (r >= 25 && r < 55)
                    {
                        r = 60;
                        g = 45;
                        b = 40;
                    }
                    else
                    {
                        r = 25;
                        g = 10;
                        b = 5;
                    }
                    //if (r > 255) r = 255;
                    //else if (r < 0) r = 0;
                    //if (g > 255) g = 255;
                    //else if (g < 0) b = 0;
                    //if (b > 255) b = 225;
                    //else if (b < 0) b = 0;


                    donusenRenk = Color.FromArgb(r, g, b);

                    cikisResmi.SetPixel(i, j, donusenRenk);
                }
            }
            pictureBox2.Image = cikisResmi;

        }

        private void button15_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.eski;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.gece2;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);

            int konX = Convert.ToInt16(textBox23.Text);
            int konY = Convert.ToInt16(textBox24.Text);

            resim = KontrastUygula(resim, "kirmizi", konX, konY, 0, 255);
            resim = KontrastUygula(resim, "yesil", konX, konY, 0, 255);
            resim = KontrastUygula(resim, "mavi", konX, konY, 0, 255);

            Color OkunanRenk, DonusenRenk;
            int R = 0, G = 0, B = 0;

            Bitmap GirisResmi, CikisResmi;
            GirisResmi = resim;

            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;

            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            int X1 = Convert.ToInt16(textBox21.Text);
            int X2 = Convert.ToInt16(textBox22.Text);

            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);

                    R = OkunanRenk.R;
                    G = OkunanRenk.G;
                    B = OkunanRenk.B;

                    if (R <= X1)
                    {
                        R = (int)(1.0 * R / X1 * X2);
                    }
                    else
                    {
                        R = (int)(1.0 * (R - (X1 + 1)) / (255 - (X1 + 1)) * (255 - X2) + X2);
                    }

                    if (G <= X1)
                    {
                        G = (int)(1.0 * G / X1 * X2);
                    }
                    else
                    {
                        G = (int)(1.0 * (G - (X1 + 1)) / (255 - (X1 + 1)) * (255 - X2) + X2);
                    }

                    if (B <= X1)
                    {
                        B = (int)(1.0 * B / X1 * X2);
                    }
                    else
                    {
                        B = (int)(1.0 * (B - (X1 + 1)) / (255 - (X1 + 1)) * (255 - X2) + X2);
                    }

                    DonusenRenk = Color.FromArgb(R, G, B);
                    CikisResmi.SetPixel(x, y, DonusenRenk);
                }
            }
            pictureBox2.Image = CikisResmi;



            //for (int y = 0; y < image.Height; y++)
            //{
            //    for (int x = 0; x < image.Width; x++)
            //    {
            //        Color pixelColor = image.GetPixel(x, y);

            //        int newRed = pixelColor.R;
            //        int newGreen = pixelColor.G;
            //        int newBlue = pixelColor.B;

            //        int deger = 180;
            //        double oran = 0.6;

            //        newRed = (int)Math.Min(pixelColor.R * brightnessFactor, 255);
            //        newGreen = (int)Math.Min(pixelColor.G * brightnessFactor, 255);
            //        newBlue = (int)Math.Min(pixelColor.B * brightnessFactor, 255);

            //        image.SetPixel(x, y, Color.FromArgb(newRed, newGreen, newBlue));
            //    }
            //}
            //pictureBox2.Image = image;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Bitmap resim;
            resim = new Bitmap(pictureBox1.Image);
            ResminHistograminiCiz(resim, "kirmizi", pictureBox3);
            ResminHistograminiCiz(resim, "yesil", pictureBox4);
            ResminHistograminiCiz(resim, "mavi", pictureBox5);

        }

        private void button19_Click(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);
            listBox1.Items.Clear();
            ResminHistograminiCiz(new Bitmap(pictureBox1.Image), "gri", pictureBox3);
            pictureBox2.Image = pictureBox1.Image;
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = e.X;
                if (x > 255)
                {
                    textBox8.Text = "255";
                    if (sekil_cizimi) textBox18.Text = "255";
                }
                else
                {
                    textBox8.Text = e.X.ToString();
                    if (sekil_cizimi) textBox18.Text = e.X.ToString();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                int x = e.X;
                if (x > 255)
                {
                    textBox7.Text = "255";
                    if (sekil_cizimi) textBox17.Text = "255";
                }
                else
                {
                    textBox7.Text = e.X.ToString();
                    if (sekil_cizimi) textBox17.Text = e.X.ToString();
                }
            }
        }

        private void pictureBox5_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = e.X;
                if (x > 255)
                {
                    textBox14.Text = "255";
                    if (sekil_cizimi) textBox16.Text = "255";
                }
                else
                {
                    textBox14.Text = e.X.ToString();
                    if (sekil_cizimi) textBox16.Text = e.X.ToString();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                int x = e.X;
                if (x > 255)
                {
                    textBox13.Text = "255";
                    if (sekil_cizimi) textBox15.Text = "255";
                }
                else
                {
                    textBox13.Text = e.X.ToString();
                    if (sekil_cizimi) textBox15.Text = e.X.ToString();
                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Renk aralıklarını (x1-x2) geniş tuttuğumda daha iyi sonuçlar elde ettim. Formül ile yapılan" +
                " kontrast işleminde 0-40 arasında değerler seçtiğimde benimkinden çok daha iyi sonuçlar elde ettim. Renkler daha doğal kaldı.");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Color OkunanRenk;

            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);

            CikisResmi = new Bitmap(kare_Z, kare_Z);

            for (int x = kare_X; x < kare_X + kare_Z; x++)
            {
                for (int y = kare_Y; y < kare_Y + kare_Z; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);
                    CikisResmi.SetPixel(x - kare_X, y - kare_Y, OkunanRenk);
                }
            }

            ResminHistograminiCiz(CikisResmi, "kirmizi", pictureBox3);
            ResminHistograminiCiz(CikisResmi, "yesil", pictureBox4);
            ResminHistograminiCiz(CikisResmi, "mavi", pictureBox5);

        }

        private Bitmap AlanaKontrastUygula(Bitmap resim, int X1, int X2, int Y1, int Y2)
        {
            Color OkunanRenk, DonusenRenk;
            int R = 0, G = 0, B = 0;

            Bitmap GirisResmi, CikisResmi;
            GirisResmi = resim;
            CikisResmi = GirisResmi;


            for (int x = kare_X; x < kare_X + kare_Z; x++)
            {
                for (int y = kare_Y; y < kare_Y + kare_Z; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);

                    R = OkunanRenk.R;
                    G = OkunanRenk.G;
                    B = OkunanRenk.B;

                    if (X1 != X2)
                    {
                        double r = (((double)(R - X1) / (X2 - X1)) * (Y2 - Y1)) + Y1;
                        if (r > 255) r = 255;
                        if (r < 0) r = 0;
                        double g = (((double)(G - X1) / (X2 - X1)) * (Y2 - Y1)) + Y1;
                        if (g > 255) g = 255;
                        if (g < 0) g = 0;
                        double b = (((double)(B - X1) / (X2 - X1)) * (Y2 - Y1)) + Y1;
                        if (b > 255) b = 255;
                        if (b < 0) b = 0;

                        DonusenRenk = Color.FromArgb((int)r, (int)g, (int)b);
                    }
                    else
                    {
                        MessageBox.Show("Degerler ayni olmamali!");
                        return resim;
                    }
                    CikisResmi.SetPixel(x, y, DonusenRenk);
                }
            }

            return CikisResmi;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            int rx1 = Convert.ToInt16(textBox20.Text);
            int rx2 = Convert.ToInt16(textBox19.Text);

            int gx1 = Convert.ToInt16(textBox18.Text);
            int gx2 = Convert.ToInt16(textBox17.Text);

            int bx1 = Convert.ToInt16(textBox16.Text);
            int bx2 = Convert.ToInt16(textBox15.Text);


            Bitmap resim = new Bitmap(pictureBox1.Image);

            resim = AlanaKontrastUygula(resim, rx1, rx2, 0, 255);
            resim = AlanaKontrastUygula(resim, gx1, gx2, 0, 255);
            resim = AlanaKontrastUygula(resim, bx1, bx2, 0, 255);

            pictureBox2.Image = resim;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null) pictureBox1.Image = pictureBox2.Image;
        }


        private void button24_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Önce belli değerleri kullanarak kontrast işlemi uyguladım." +
                " Ardından belli sınır değerlerle renklerin arasını açtım." +
                " Böylelikle belli olmayan bazı nesneler belirginleşti (ornek : durak ve insan) " +
                " \nNot: Bu değerler sadece bu resim için geçerlidir!");
        }

        private void SkalaOlustur()
        {
            int boyutY = 256 * 6;
            int boyutX = 128;

            Bitmap resim = new Bitmap(boyutY, boyutX);

            int R, G, B;

            int ilkR = 255;
            int ilkG = 1;
            int ilkB = 1;

            double sayi2;

            int xR, xG, xB;

            for (int x = 0; x < boyutX; x++)
            {
                xR = ilkR - x;
                xG = ilkG + x;
                xB = ilkB + x;

                R = xR; G = xG; B = xB;

                sayi2 = 1.0 * (xR - xG) / (256 - 1);

                for (int y = 0; y < boyutY; y++)
                {
                    if (y < 255)
                    {
                        G = (int)Math.Round(xG + (y * sayi2));
                    }
                    else if (y < 255 * 2)
                    {
                        R = (int)Math.Round(xR - ((y - 255) * sayi2));
                    }
                    else if (y < 255 * 3)
                    {
                        B = (int)Math.Round(xB + ((y - 255 * 2) * sayi2));
                    }
                    else if (y < 255 * 4)
                    {
                        G = (int)Math.Round((255 - xG) - ((y - 255 * 3) * sayi2));
                    }
                    else if (y < 255 * 5)
                    {
                        R = (int)Math.Round((255 - xR) + ((y - 255 * 4) * sayi2));
                    }
                    else
                    {
                        B = (int)Math.Round((255 - xB) - ((y - 255 * 5) * sayi2));
                    }

                    if (R > 255) R = 255;
                    else if (R < 0) R = 0;
                    if (G > 255) G = 255;
                    else if (G < 0) G = 0;
                    if (B > 255) B = 255;
                    else if (B < 0) B = 0;

                    Color renk = Color.FromArgb(R, G, B);

                    resim.SetPixel(y, x, renk);
                }
            }
            pictureBox6.Image = resim;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point nokta = me.Location;

            Bitmap resim = new Bitmap(pictureBox6.Image);

            Color okunanRenk;

            int x = nokta.X;
            int y = nokta.Y;

            int p_w = pictureBox2.Width;
            int p_h = pictureBox2.Height;
            int r_w = resim.Width;
            int r_h = resim.Height;

            int x1 = x;
            int y1 = y;

            if (r_h > p_h)
            {
                y = (int)Math.Round(y * (1.0 * r_h / p_h));
            }
            else
            {
                y = (int)Math.Round(y * (1.0 * r_h / p_h));
            }
            if (r_w > p_w)
            {
                x = (int)Math.Round(x * (1.0 * r_w / p_w));
            }
            else
            {
                x = (int)Math.Round(x * (1.0 * r_w / p_w));
            }


            okunanRenk = resim.GetPixel(x, y);

            textBox25.Text = okunanRenk.R.ToString();
            textBox26.Text = okunanRenk.G.ToString();
            textBox27.Text = okunanRenk.B.ToString();

            int width = pictureBox7.Width;
            int height = pictureBox7.Height;

            Bitmap renk_resim = new Bitmap(width, height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    renk_resim.SetPixel(i, j, okunanRenk);
                }
            }

            pictureBox7.Image = renk_resim;

            width = 16;
            height = 256;

            Bitmap skala = new Bitmap(width, height);

            int R = 255;
            int G = 255;
            int B = 255;

            double kR = 1.0 * (255 - okunanRenk.R) / ((height - 1) / 2);
            double kG = 1.0 * (255 - okunanRenk.G) / ((height - 1) / 2);
            double kB = 1.0 * (255 - okunanRenk.B) / ((height - 1) / 2);

            double kR2 = 1.0 * okunanRenk.R / ((height - 1) / 2);
            double kG2 = 1.0 * okunanRenk.G / ((height - 1) / 2);
            double kB2 = 1.0 * okunanRenk.B / ((height - 1) / 2);

            bool bayrak = true;

            for (int i = 0; i < height; i++)
            {
                if (i < (height / 2))
                {
                    R = (int)Math.Round(255 - (i * kR));
                    G = (int)Math.Round(255 - (i * kG));
                    B = (int)Math.Round(255 - (i * kB));
                }
                else
                {
                    R = (int)Math.Round(okunanRenk.R - ((i - (height / 2)) * kR2));
                    G = (int)Math.Round(okunanRenk.G - ((i - (height / 2)) * kG2));
                    B = (int)Math.Round(okunanRenk.B - ((i - (height / 2)) * kB2));
                }


                for (int j = 0; j < width; j++)
                {
                    if (R < 0) R = 0;
                    else if (R > 255) R = 255;
                    if (G < 0) G = 0;
                    else if (G > 255) G = 255;
                    if (B < 0) B = 0;
                    else if (B > 255) B = 255;
                    skala.SetPixel(j, i, Color.FromArgb(R, G, B));
                }
            }

            pictureBox6.Refresh();
            using (Graphics g = pictureBox6.CreateGraphics())
            {
                // Daireyi çizin
                g.DrawEllipse(new Pen(Color.Black, 2), x1 - 5, y1 - 5, 10, 10);
            }

            pictureBox8.Image = skala;
            trackBar17.Value = 128;

        }

        private void trackBar17_Scroll(object sender, EventArgs e)
        {
            int x = 255 - trackBar17.Value;

            Bitmap skala = new Bitmap(pictureBox8.Image);

            Color renk = skala.GetPixel(0, x);

            Bitmap renk_resim = new Bitmap(pictureBox7.Image);

            for (int i = 0; i < renk_resim.Width; i++)
            {
                for (int j = 0; j < renk_resim.Height; j++)
                {
                    renk_resim.SetPixel(i, j, renk);
                }
            }
            textBox25.Text = renk.R.ToString();
            textBox26.Text = renk.G.ToString();
            textBox27.Text = renk.B.ToString();
            pictureBox7.Image = renk_resim;
        }

        private string controlName = "palet1";

        private void button34_Click(object sender, EventArgs e)
        {
            if (controlName != "")
            {
                if (pictureBox7.Image != null)
                {
                    Bitmap resim = new Bitmap(pictureBox7.Image);
                    Color renk = resim.GetPixel(0, 0);
                    tabControl1.Controls[9].Controls[controlName].BackColor = renk;
                }
            }
        }

        private void palet_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btn = (System.Windows.Forms.Button)sender;
            controlName = btn.Name;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.picture3;
        }
    }
}


