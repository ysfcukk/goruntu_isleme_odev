using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace odev
{
    public partial class hafta3 : Form
    {
        public hafta3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.picture3;
            yedekResim = new Bitmap(pictureBox1.Image);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.DefaultExt = ".jpg";
            file.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            file.ShowDialog();
            pictureBox1.ImageLocation = file.FileName;
            yedekResim = new Bitmap(pictureBox1.ImageLocation);
        }

        bool tiklayarakTasi = false;
        bool basiliTuatarakTasi = false;

        private void button3_Click(object sender, EventArgs e)
        {
            if (!tiklayarakTasi)
            {
                pictureBox2.Image = pictureBox1.Image;
                label2.BackColor = Color.FromArgb(192, 255, 192);
                label2.Text = "aktif";
                label1.BackColor = Color.Pink;
                label1.Text = "pasif";
                tiklayarakTasi = true;
                basiliTuatarakTasi = false;
            }
            else
            {
                label2.BackColor = Color.Pink;
                label2.Text = "pasif";
                tiklayarakTasi = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!basiliTuatarakTasi)
            {
                pictureBox2.Image = pictureBox1.Image;
                label1.BackColor = Color.FromArgb(192, 255, 192);
                label1.Text = "aktif";
                label2.BackColor = Color.Pink;
                label2.Text = "pasif";
                tiklayarakTasi = false;
                basiliTuatarakTasi = true;
            }
            else
            {
                label1.BackColor = Color.Pink;
                label1.Text = "pasif";
                basiliTuatarakTasi = false;
            }
        }

        bool ilkTiklama = true;
        private Bitmap NoktaCiz(Bitmap resim, int x, int y)
        {
            // Kesikli çizgiyi çizecek nesneyi oluşturun
            Graphics g = Graphics.FromImage(resim);

            // Çizgi özelliklerini ayarlayın
            Pen pen = new Pen(Color.Red); // Çizgi rengi
            pen.DashStyle = DashStyle.Dash; // Kesikli çizgi stili
                                            // Kare sol üst köşesinin X koordinatı // Kare sol üst köşesinin Y koordinatı
            int width = 5; // Kare genişliği
            int height = 5; // Kare yüksekliği

            if (x > (resim.Width - width))
            {
                x = resim.Width - width;
            }

            if (y > (resim.Height - height))
            {
                y = resim.Height - height;
            }

            Brush brush = new SolidBrush(Color.Red);

            // Kesikli çizgilerle kareyi çizin
            g.FillRectangle(brush, x, y, width, height);

            // Nesneleri temizleyin
            pen.Dispose();
            g.Dispose();

            return resim;
        }


        int xx, yy;

        private void button7_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null) pictureBox2.Image = pictureBox1.Image;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null) pictureBox1.Image = pictureBox2.Image;
        }

        private bool isDragging = false;
        private Point lastMousePosition;

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastMousePosition = Oranla(pictureBox2, e.Location.X, e.Location.Y);
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && tiklayarakTasi)
            {
                // Fare konumundaki değişimi hesaplayın

                // Dairenin yeni konumunu hesaplayın

                Color OkunanRenk;
                Bitmap GirisResmi, CikisResmi;

                GirisResmi = new Bitmap(pictureBox2.Image);

                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;

                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

                double x2 = 0, y2 = 0;

                int Tx = e.X - lastMousePosition.X;
                int Ty = e.Y - lastMousePosition.Y;

                for (int x1 = 0; x1 < (ResimGenisligi); x1++)
                {
                    for (int y1 = 0; y1 < (ResimYuksekligi); y1++)
                    {
                        OkunanRenk = GirisResmi.GetPixel(x1, y1);
                        x2 = x1 + Tx;
                        y2 = y1 + Ty;
                        if (x2 > 0 && x2 < ResimGenisligi && y2 > 0 && y2 < ResimYuksekligi)
                            CikisResmi.SetPixel((int)x2, (int)y2, OkunanRenk);
                    }
                }

                // Son fare konumunu güncelleyin
                lastMousePosition = e.Location;

                pictureBox2.Image = CikisResmi;
            }
            else if (isDragging && tiklayarakDonmeEtkin)
            {
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);

                int x0 = (int)(GirisResmi.Width / 2.0);
                int y0 = (int)(GirisResmi.Height / 2.0);
                Point p0 = new Point(x0, y0);

                int x1 = lastMousePosition.X;
                int y1 = GirisResmi.Height - lastMousePosition.Y;
                Point p1 = new Point(x1, y1);

                Point p2 = Oranla(pictureBox2, e.X, e.Y);
                p2.Y = GirisResmi.Height - p2.Y;

                int c = (int)UzaklikHesapla(p0, p1);
                int b = (int)UzaklikHesapla(p0, p2);
                int a = (int)UzaklikHesapla(p1, p2);

                double deger = (1.0 * b * b + c * c - a * a) / (2.0 * b * c);

                double radyanAci = Math.Acos(deger);
                int Aci = (int)(radyanAci * (180.0 / Math.PI));

                double m = 1.0 * (y0 - y1) / (x0 - x1);

                double p = m;
                double r = -1;
                double t = y1 - m * x1;

                if ((x1 > x0 && y1 > y0) || (x1 > x0 && y1 < y0))
                {
                    if (p * p2.X + r * p2.Y + t < 0)
                    {
                        Aci = 360 - Aci;
                    }
                }
                else if ((x1 < x0 && y1 > y0) || (x1 < x0 && y1 < y0))
                {
                    if (p * p2.X + r * p2.Y + t > 0)
                    {
                        Aci = 360 - Aci;
                    }
                }

                CikisResmi = Dondur(GirisResmi, x0, y0, Aci);

                pictureBox2.Image = NoktaCiz(CikisResmi, (CikisResmi.Width / 2), (CikisResmi.Height / 2));

            }
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                MouseEventArgs me = (MouseEventArgs)e;
                Point nokta = me.Location;

                Point yeniNokta = Oranla(pictureBox2, nokta.X, nokta.Y);

                int x = yeniNokta.X;
                int y = yeniNokta.Y;


                if (tiklayarakTasi)
                {
                    if (ilkTiklama)
                    {
                        Bitmap resim = new Bitmap(pictureBox1.Image);
                        pictureBox2.Image = NoktaCiz(resim, x, y);
                        ilkTiklama = false;

                        xx = x;
                        yy = y;
                    }
                    else
                    {

                        Color OkunanRenk;
                        Bitmap GirisResmi, CikisResmi;

                        GirisResmi = new Bitmap(pictureBox2.Image);

                        int ResimGenisligi = GirisResmi.Width;
                        int ResimYuksekligi = GirisResmi.Height;

                        CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

                        double x2 = 0, y2 = 0;
                        int Tx = x - xx;
                        int Ty = y - yy;

                        for (int x1 = 0; x1 < (ResimGenisligi); x1++)
                        {
                            for (int y1 = 0; y1 < (ResimYuksekligi); y1++)
                            {
                                OkunanRenk = GirisResmi.GetPixel(x1, y1);
                                x2 = x1 + Tx;
                                y2 = y1 + Ty;
                                if (x2 > 0 && x2 < ResimGenisligi && y2 > 0 && y2 < ResimYuksekligi)
                                    CikisResmi.SetPixel((int)x2, (int)y2, OkunanRenk);
                            }
                        }
                        pictureBox2.Image = NoktaCiz(CikisResmi, xx, yy);
                        ilkTiklama = true;
                    }
                }
                else if (aynalamaIcinNoktaSecimi)
                {
                    if (ilkTiklama)
                    {
                        ilkTiklama = false;
                        xx = x;
                        yy = y;
                    }
                    else
                    {
                        Color OkunanRenk;
                        Bitmap GirisResmi, CikisResmi;

                        GirisResmi = new Bitmap(pictureBox1.Image);

                        int ResimGenisligi = GirisResmi.Width;
                        int ResimYuksekligi = GirisResmi.Height;

                        CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

                        //dy = ResimGenisligi - dy;

                        double m = 1.0 * (yy - y) / (xx - x);

                        double a = m;
                        double b = -1;
                        double c = y - m * x;

                        double x2 = 0.0, y2 = 0.0;

                        for (int x1 = 0; x1 < (ResimGenisligi); x1++)
                        {
                            for (int y1 = 0; y1 < (ResimYuksekligi); y1++)
                            {
                                OkunanRenk = GirisResmi.GetPixel(x1, y1);

                                //int y_ = ResimYuksekligi - y1;

                                x2 = x1 - (2 * a * (a * x1 + b * y1 + c)) / (a * a + b * b);
                                y2 = y1 - (2 * b * (a * x1 + b * y1 + c)) / (a * a + b * b);

                                //x2 = (2.0 * m * (y1 - b) + x1) / (m * m + 1.0);
                                //y2 = 2.0 * m * x2 + 2 * b - y1;

                                if (x2 > 0 && x2 < ResimGenisligi && y2 > 0 && y2 < ResimYuksekligi)
                                    CikisResmi.SetPixel((int)x2, (int)y2, OkunanRenk);
                            }
                        }
                        pictureBox2.Image = CikisResmi;
                        ilkTiklama = true;
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = Aynalama("dikey");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = Aynalama("yatay");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = Aynalama("");
        }

        bool aynalamaIcinNoktaSecimi = false;

        private void button9_Click(object sender, EventArgs e)
        {
            if (!aynalamaIcinNoktaSecimi)
            {
                pictureBox2.Image = pictureBox1.Image;
                label3.BackColor = Color.FromArgb(192, 255, 192);
                label3.Text = "aktif";
                aynalamaIcinNoktaSecimi = true;


            }
            else
            {
                label3.BackColor = Color.Pink;
                label3.Text = "pasif";
                aynalamaIcinNoktaSecimi = false;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            pictureBox2.SizeMode = PictureBoxSizeMode.Normal;
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int BuyutmeKatsayisi = Convert.ToInt16(textBox2.Text);

            if (BuyutmeKatsayisi > 0)
            {
                Color OkunanRenk;
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;

                int yeniResimGenisligi = ResimGenisligi * BuyutmeKatsayisi;
                int yeniResimYuksekligi = ResimYuksekligi * BuyutmeKatsayisi;

                CikisResmi = new Bitmap(yeniResimGenisligi, yeniResimYuksekligi);
                int x2 = 0, y2 = 0;
                for (int x1 = 0; x1 < ResimGenisligi; x1++)
                {
                    for (int y1 = 0; y1 < ResimYuksekligi; y1++)
                    {
                        OkunanRenk = GirisResmi.GetPixel(x1, y1);
                        for (int i = 0; i < BuyutmeKatsayisi; i++)
                        {
                            for (int j = 0; j < BuyutmeKatsayisi; j++)
                            {
                                x2 = (int)(1.0 * x1 * BuyutmeKatsayisi + i);
                                y2 = (int)(1.0 * y1 * BuyutmeKatsayisi + j);
                                if (x2 > 0 && x2 < yeniResimGenisligi && y2 > 0 && y2 < yeniResimYuksekligi)
                                    CikisResmi.SetPixel(x2, y2, OkunanRenk);
                            }
                        }
                    }
                }
                textBox3.Text = yeniResimGenisligi.ToString() + "," + yeniResimYuksekligi.ToString();
                pictureBox2.Image = CikisResmi;
            }
            else
            {
                MessageBox.Show("0'dan büyük bir tam sayi giriniz!!!");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        bool kirpma = false;
        bool kirpmaTiklama = false;
        bool ikinciTiklandi = false;
        Point kirpmaIlkNokta;
        Point kirpmaSonNokta;
        Bitmap yedekResim = null;

        Point dondurmeNoktasi;
        bool dondurmeNoktasinaTiklandi = false;

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
            if (yy < 0) yy = 0;

            return new Point(x0, y0);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                MouseEventArgs me = (MouseEventArgs)e;
                Point nokta = me.Location;

                Point yeniNokta = Oranla(pictureBox1, nokta.X, nokta.Y);

                int x = yeniNokta.X;
                int y = yeniNokta.Y;

                if (kirpma)
                {
                    if (!kirpmaTiklama)
                    {
                        if (ikinciTiklandi)
                        {
                            pictureBox1.Image = yedekResim;
                            ikinciTiklandi = false;
                        }
                        else
                        {
                            yedekResim = new Bitmap(pictureBox1.Image);
                        }
                        kirpmaTiklama = true;
                        kirpmaIlkNokta = new Point(x, y);
                        pictureBox1.Image = NoktaCiz(new Bitmap(pictureBox1.Image), x - 2, y - 2);
                    }
                    else
                    {
                        pictureBox1.Image = NoktaCiz(new Bitmap(pictureBox1.Image), x - 2, y - 2);

                        kirpmaSonNokta = new Point(x, y);

                        Bitmap resim = new Bitmap(pictureBox1.Image);
                        pictureBox1.Image = DikdortgenCiz(resim, kirpmaIlkNokta.X, kirpmaIlkNokta.Y, Math.Abs(kirpmaIlkNokta.X - x), Math.Abs(kirpmaIlkNokta.Y - y));
                        kirpmaTiklama = false;
                        ikinciTiklandi = true;
                    }
                }
                else if (dondurmeEtkin)
                {
                    if (donduruldu)
                    {
                        yedekResim = new Bitmap(pictureBox1.Image);
                        donduruldu = false;
                    }
                    else
                    {
                        pictureBox1.Image = yedekResim;
                    }

                    dondurmeNoktasi.X = x;
                    dondurmeNoktasi.Y = y;
                    pictureBox1.Image = ArtiCiz(new Bitmap(pictureBox1.Image), x, y);
                    dondurmeNoktasinaTiklandi = true;
                }
            }
        }

        private Bitmap ArtiCiz(Bitmap resim, int x, int y)
        {
            Graphics g = Graphics.FromImage(resim);

            Pen pen = new Pen(Color.Red);
            //pen.DashStyle = DashStyle.Dash;

            //int width = 5; // Kare genişliği
            //int height = b; // Kare yüksekliği

            //if (x > (resim.Width - width))
            //{
            //    x = resim.Width - width;
            //}

            //if (y > (resim.Height - height))
            //{
            //    y = resim.Height - height;
            //}

            g.DrawLine(pen, x, y - 5, x, y + 5);
            g.DrawLine(pen, x - 5, y, x + 5, y);

            pen.Dispose();
            g.Dispose();

            return resim;
        }

        private Bitmap DikdortgenCiz(Bitmap resim, int x, int y, int a, int b)
        {

            Graphics g = Graphics.FromImage(resim);

            Pen pen = new Pen(Color.Red);
            pen.DashStyle = DashStyle.Dash;

            int width = a; // Kare genişliği
            int height = b; // Kare yüksekliği

            if (x > (resim.Width - width))
            {
                x = resim.Width - width;
            }

            if (y > (resim.Height - height))
            {
                y = resim.Height - height;
            }

            g.DrawRectangle(pen, x, y, width, height);

            pen.Dispose();
            g.Dispose();

            return resim;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (!kirpma)
            {
                label5.BackColor = Color.FromArgb(192, 255, 192);
                label5.Text = "aktif";
                kirpma = true;
            }
            else
            {
                label5.BackColor = Color.Pink;
                label5.Text = "pasif";
                kirpma = false;
            }
        }

        bool kirpmaYapildi = false;

        private void button15_Click(object sender, EventArgs e)
        {
            Bitmap resim = yedekResim;
            Bitmap cikisResmi = null;
            int width = Math.Abs(kirpmaSonNokta.X - kirpmaIlkNokta.X);
            int height = Math.Abs(kirpmaSonNokta.Y - kirpmaIlkNokta.Y);

            try
            {
                cikisResmi = new Bitmap(width, height);
            }
            catch
            {
                MessageBox.Show("Lutfen pictureBox1'e 2 kez tıklarak alan belirleyin!");
            }

            Color OkunanRenk;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    OkunanRenk = resim.GetPixel(x + kirpmaIlkNokta.X, y + kirpmaIlkNokta.Y);

                    cikisResmi.SetPixel(x, y, OkunanRenk);
                }
            }

            pictureBox2.Image = cikisResmi;
            pictureBox1.Image = yedekResim;
            ikinciTiklandi = false;
        }

        private void hafta3_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.picture3;
            yedekResim = new Bitmap(pictureBox1.Image);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //yedekResim = new Bitmap(pictureBox1.Image);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            int kucultmeKatsayisi = 0;
            bool hataYok = true;

            try
            {
                kucultmeKatsayisi = Convert.ToInt16(textBox4.Text);
            }
            catch
            {
                MessageBox.Show("0-100 arası 'Tamsayi' giriniz!");
                hataYok = false;
            }

            if (hataYok)
            {
                if (kucultmeKatsayisi <= 0 || kucultmeKatsayisi >= 100)
                {
                    MessageBox.Show("0-100 arasında 'Tamsayi' giriniz!");
                }
                else
                {
                    Color OkunanRenk;
                    Bitmap GirisResmi, CikisResmi;
                    GirisResmi = new Bitmap(pictureBox1.Image);
                    int ResimGenisligi = GirisResmi.Width;
                    int ResimYuksekligi = GirisResmi.Height;
                    CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
                    int x2 = 0, y2 = 0; //Çıkış resminin x ve y si olacak.

                    double KucultmeKatsayisi = 100.0 / (100.0 - kucultmeKatsayisi);

                    for (double x1 = 0; x1 < ResimGenisligi; x1 = (x1 + KucultmeKatsayisi))
                    {
                        y2 = 0;
                        for (double y1 = 0; y1 < ResimYuksekligi; y1 = (y1 + KucultmeKatsayisi))
                        {
                            OkunanRenk = GirisResmi.GetPixel((int)x1, (int)y1);
                            CikisResmi.SetPixel(x2, y2, OkunanRenk);
                            y2++;
                        }
                        x2++;
                    }
                    pictureBox2.Image = CikisResmi;
                }
            }

        }

        private Bitmap Dondur(Bitmap GirisResmi, int x0, int y0, int Aci)
        {
            Color OkunanRenk;
            Bitmap CikisResmi;
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            double RadyanAci = Aci * (Math.PI / 180);
            double x2 = 0, y2 = 0;

            //Resim merkezini buluyor. Resim merkezi etrafında döndürecek.

            for (int x1 = 0; x1 < (ResimGenisligi); x1++)
            {
                for (int y1 = 0; y1 < (ResimYuksekligi); y1++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x1, y1);
                    //Döndürme Formülleri
                    x2 = Math.Cos(RadyanAci) * (x1 - x0) - Math.Sin(RadyanAci) * (y1 - y0) + x0;
                    y2 = Math.Sin(RadyanAci) * (x1 - x0) + Math.Cos(RadyanAci) * (y1 - y0) + y0;
                    if (x2 > 0 && x2 < ResimGenisligi && y2 > 0 && y2 < ResimYuksekligi)
                        CikisResmi.SetPixel((int)x2, (int)y2, OkunanRenk);
                }
            }
            return CikisResmi;

        }

        bool donduruldu = false;

        private void button18_Click(object sender, EventArgs e)
        {
            if (dondurmeEtkin && dondurmeNoktasinaTiklandi)
            {
                int Aci = Convert.ToInt16(textBox5.Text);
                pictureBox2.Image = Dondur(yedekResim, dondurmeNoktasi.X, dondurmeNoktasi.Y, Aci);
            }
            donduruldu = true;
            pictureBox1.Image = yedekResim;
        }

        bool dondurmeEtkin = false;

        private void button17_Click(object sender, EventArgs e)
        {
            if (!dondurmeEtkin)
            {
                label7.BackColor = Color.FromArgb(192, 255, 192);
                label7.Text = "aktif";
                dondurmeEtkin = true;
            }
            else
            {
                label7.BackColor = Color.Pink;
                label7.Text = "pasif";
                dondurmeEtkin = false;
                pictureBox1.Image = yedekResim;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            //Bitmap resim = new Bitmap(pictureBox1.Image);
            //Graphics g = Graphics.FromImage(resim);

            //int tutamcBoyutu = 5;
            //int tb = tutamcBoyutu;
            //int orta_w = resim.Width / 2;
            //int orta_h = resim.Height / 2;

            //Rectangle[] tutamclar = {
            //    new Rectangle(0, 0, tb, tb),
            //    new Rectangle(resim.Width - tb, 0, tb, tb),
            //    new Rectangle(0, resim.Height - tb, tb, tb),
            //    new Rectangle(resim.Width - tb, resim.Height - tb, tb, tb),
            //    new Rectangle(0, orta_w, tb, tb),
            //    new Rectangle(orta_h, 0, tb, tb),
            //    new Rectangle(orta_h, resim.Width-tb, tb, tb),
            //    new Rectangle(resim.Height-tb, orta_w, tb, tb)
            //};

            //foreach (Rectangle tutamac in tutamclar)
            //{
            //    g.FillRectangle(Brushes.Black, tutamac);
            //}

            //pictureBox1.Image = resim;
        }

        private Bitmap Buyut_Interpolasyon(Bitmap kaynak, int olcekFaktoru)
        {
            int yeniGenislik = (int)(kaynak.Width * olcekFaktoru);
            int yeniYukseklik = (int)(kaynak.Height * olcekFaktoru);

            Bitmap yeniResim = new Bitmap(yeniGenislik, yeniYukseklik);

            using (Graphics g = Graphics.FromImage(yeniResim))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic; // İnterpolasyon modu seçimi
                g.DrawImage(kaynak, new Rectangle(0, 0, yeniGenislik, yeniYukseklik), new Rectangle(0, 0, kaynak.Width, kaynak.Height), GraphicsUnit.Pixel);
            }

            return yeniResim;
        }

        private Bitmap Buyut_Degistirme(Bitmap orijinalResim, int olcekFaktoru)
        {

            int yeniGenislik = orijinalResim.Width * olcekFaktoru;
            int yeniYukseklik = orijinalResim.Height * olcekFaktoru;

            Bitmap buyukResim = new Bitmap(yeniGenislik, yeniYukseklik);

            for (int y = 0; y < yeniYukseklik; y++)
            {
                for (int x = 0; x < yeniGenislik; x++)
                {
                    int eskiX = x / olcekFaktoru;
                    int eskiY = y / olcekFaktoru;

                    Color pikselRenk = orijinalResim.GetPixel(eskiX, eskiY);
                    buyukResim.SetPixel(x, y, pikselRenk);
                }
            }

            return buyukResim;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);
            int katsayi = 2;
            pictureBox2.Image = Buyut_Interpolasyon(resim, katsayi);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);
            int katsayi = 2;
            pictureBox2.Image = Buyut_Degistirme(resim, katsayi);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            MessageBox.Show("İki yöntemle de oluşturulmuş resimleri incelediğimde;" +
                "\nİnterpolasyon metodunun daha yumuşak geçişli ve pürüzsüz bir resim oluşturduğunu" +
                "\nDeğiştirme metodunun daha keskin ve sert geçişli resim oluşturduğunu tespit ettim.");
        }

        static double UzaklikHesapla(Point point1, Point point2)
        {
            double deltaX = point2.X - point1.X;
            double deltaY = point2.Y - point1.Y;

            // İki nokta arasındaki uzaklığı hesapla (Pitagoras Teoremi)
            double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            return distance;
        }

        bool tiklayarakDonmeEtkin = false;

        private void button24_Click(object sender, EventArgs e)
        {
            if (!tiklayarakDonmeEtkin)
            {
                label8.BackColor = Color.FromArgb(192, 255, 192);
                label8.Text = "aktif";
                tiklayarakDonmeEtkin = true;
                pictureBox2.Image = new Bitmap(pictureBox1.Image);
            }
            else
            {
                label8.BackColor = Color.Pink;
                label8.Text = "pasif";
                tiklayarakDonmeEtkin = false;
                pictureBox2.Image = new Bitmap(pictureBox1.Image);
            }

        }

        private Bitmap KaydirarakDondur(Bitmap GirisResmi, int Aci, string yon)
        {
            Color OkunanRenk;
            Bitmap CikisResmi;

            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;

            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            int x0 = ResimGenisligi / 2;
            int y0 = ResimYuksekligi / 2;

            int x2;
            int y2;

            double RadyanAci = Aci * 2 * Math.PI / 360;

            for (int x1 = 0; x1 < ResimGenisligi; x1++)
            {
                for (int y1 = 0; y1 < ResimGenisligi; y1++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x1, y1);

                    if (yon == "sag")
                    {
                        x2 = (int)((x1 - x0) - (y1 - y0) * Math.Tan(RadyanAci / 2) + x0);
                        y2 = (y1 - y0) + y0;
                    }
                    else if (yon == "sol")
                    {
                        x2 = (x1 - x0) + x0;
                        y2 = (int)((x1 - x0) * Math.Sin(RadyanAci) + (y1 - y0) + y0);
                    }
                    else
                    {
                        x2 = 0;
                        y2 = 0;
                    }

                    if (x2 >= 0 && x2 < ResimGenisligi && y2 >= 0 && y2 < ResimYuksekligi)
                        CikisResmi.SetPixel(x2, y2, OkunanRenk);
                }
            }

            return CikisResmi;
        }

        int D = 30;

        private void button25_Click(object sender, EventArgs e)
        {
            Bitmap resim;
            if (pictureBox2.Image != null)
                resim = new Bitmap(pictureBox2.Image);
            else
            {
                resim = new Bitmap(pictureBox1.Image);
            }
            int Aci = Convert.ToInt16(textBox7.Text);
            pictureBox2.Image = KaydirarakDondur(resim, Aci, "sag");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            Bitmap resim;
            if (pictureBox2.Image != null)
                resim = new Bitmap(pictureBox2.Image);
            else
            {
                resim = new Bitmap(pictureBox1.Image);
            }
            int Aci = Convert.ToInt16(textBox7.Text);
            pictureBox2.Image = KaydirarakDondur(resim, Aci, "sol");
        }

        private void button27_Click(object sender, EventArgs e)
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;

            GirisResmi = new Bitmap(pictureBox1.Image);

            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;

            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            int x0 = ResimGenisligi / 2;
            int y0 = ResimYuksekligi / 2;

            int x2;
            int y2;

            int Aci = Convert.ToInt16(textBox7.Text);

            double RadyanAci = Aci * 2 * Math.PI / 360;

            for (int x1 = 0; x1 < ResimGenisligi; x1++)
            {
                for (int y1 = 0; y1 < ResimGenisligi; y1++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x1, y1);

                    x2 = (int)((x1 - x0) - (y1 - y0) * Math.Tan(RadyanAci / 2) + x0);
                    y2 = (y1 - y0) + y0;

                    x2 = (x2 - x0) + x0;
                    y2 = (int)((x2 - x0) * Math.Sin(RadyanAci) + (y2 - y0) + y0);

                    x2 = (int)((x2 - x0) - (y2 - y0) * Math.Tan(RadyanAci / 2) + x0);
                    y2 = (y2 - y0) + y0;

                    if (x2 >= 0 && x2 < ResimGenisligi && y2 >= 0 && y2 < ResimYuksekligi)
                        CikisResmi.SetPixel(x2, y2, OkunanRenk);
                }
            }

            pictureBox2.Image = CikisResmi;
        }

        bool basiliKirpma = false;

        private void button29_Click(object sender, EventArgs e)
        {
            if (!basiliKirpma)
            {
                label12.BackColor = Color.FromArgb(192, 255, 192);
                label12.Text = "aktif";
                basiliKirpma = true;
                yedekResim = new Bitmap(pictureBox1.Image);
            }
            else
            {
                label12.BackColor = Color.Pink;
                label12.Text = "pasif";
                basiliKirpma = false;
                pictureBox1.Image = yedekResim;
            }
        }

        Point ilkNokta;
        Point sonNokta;
        bool tiklandi = false;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ilkNokta = Oranla(pictureBox1, e.Location.X, e.Location.Y);
            if (basiliKirpma) tiklandi = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (tiklandi && basiliKirpma)
            {
                int x1 = ilkNokta.X;
                int y1 = ilkNokta.Y;

                sonNokta = Oranla(pictureBox1, e.X, e.Y);
                int x2 = sonNokta.X;
                int y2 = sonNokta.Y;

                Bitmap resim = NoktaCiz(new Bitmap(yedekResim), x1 - 2, y1 - 2);
                resim = NoktaCiz(new Bitmap(resim), x2 - 2, y2 - 2);
                resim = NoktaCiz(new Bitmap(resim), x2 - 2, y1 - 2);
                resim = NoktaCiz(new Bitmap(resim), x1 - 2, y2 - 2);

                int kucukX = x1;
                int kucukY = y1;
                int buyukX = x2;
                int buyukY = y2;

                if (x1 > x2)
                {
                    kucukX = x2;
                    buyukX = x1;
                }
                if (y1 > y2)
                {
                    kucukY = y2;
                    buyukY = y1;
                }

                pictureBox1.Image = DikdortgenCiz(resim, kucukX, kucukY, Math.Abs(x1 - x2), Math.Abs(y1 - y2));

            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            tiklandi = false;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            Bitmap resim = yedekResim;
            Bitmap cikisResmi = null;
            int width = Math.Abs(sonNokta.X - ilkNokta.X);
            int height = Math.Abs(sonNokta.Y - ilkNokta.Y);

            try
            {
                cikisResmi = new Bitmap(width, height);
            }
            catch
            {
                MessageBox.Show("HATA");
            }

            Color OkunanRenk;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    OkunanRenk = resim.GetPixel(x + ilkNokta.X, y + ilkNokta.Y);

                    cikisResmi.SetPixel(x, y, OkunanRenk);
                }
            }

            pictureBox2.Image = cikisResmi;
            pictureBox1.Image = yedekResim;
            ikinciTiklandi = false;
        }

        private Bitmap Aynalama(string tur)
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            double Aci = Convert.ToDouble(textBox1.Text);
            double RadyanAci = Aci * 2 * Math.PI / 360;
            double x2 = 0, y2 = 0;
            //Resim merkezini buluyor. Resim merkezi etrafında döndürecek.
            int x0 = ResimGenisligi / 2;
            int y0 = ResimYuksekligi / 2;

            for (int x1 = 0; x1 < (ResimGenisligi); x1++)
            {
                for (int y1 = 0; y1 < (ResimYuksekligi); y1++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x1, y1);


                    //----A-Orta dikey eksen etrafında aynalama ----------------

                    if (tur == "dikey")
                    {
                        x2 = Convert.ToInt16(-x1 + 2 * x0);
                        y2 = Convert.ToInt16(y1);
                    }
                    //----B-Orta yatay eksen etrafında aynalama ----------------
                    else if (tur == "yatay")
                    {
                        x2 = Convert.ToInt16(x1);
                        y2 = Convert.ToInt16(-y1 + 2 * y0);
                    }
                    //----C-Ortadan geçen 45 açılı çizgi etrafında aynalama----------
                    else
                    {
                        double Delta = (x1 - x0) * Math.Sin(RadyanAci) - (y1 - y0) * Math.Cos(RadyanAci);
                        x2 = Convert.ToInt16(x1 + 2 * Delta * (-Math.Sin(RadyanAci)));
                        y2 = Convert.ToInt16(y1 + 2 * Delta * (Math.Cos(RadyanAci)));

                    }
                    if (x2 > 0 && x2 < ResimGenisligi && y2 > 0 && y2 < ResimYuksekligi)
                        CikisResmi.SetPixel((int)x2, (int)y2, OkunanRenk);

                }

            }
            return CikisResmi;

        }
    }
}
