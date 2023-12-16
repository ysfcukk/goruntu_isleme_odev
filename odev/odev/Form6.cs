using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Effects;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace odev
{
    public partial class hafta6 : Form
    {
        public hafta6()
        {
            InitializeComponent();
        }

        public Bitmap OrjianalResimdenBulanikResmiCikarma(Bitmap OrjinalResim, Bitmap BulanikResim, bool norm)
        {
            int EnBuyukR = 0;
            int EnBuyukG = 0;
            int EnBuyukB = 0;
            int EnKucukR = 0;
            int EnKucukG = 0;
            int EnKucukB = 0;

            Color OkunanRenk1, OkunanRenk2, DonusenRenk; Bitmap CikisResmi;
            int ResimGenisligi = OrjinalResim.Width;
            int ResimYuksekligi = OrjinalResim.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int R, G, B;

            double Olcekleme = 2; //Keskin kenaları daha iyi görmek için değerini artırıyoruz.
            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk1 = OrjinalResim.GetPixel(x, y); OkunanRenk2 = BulanikResim.GetPixel(x, y);
                    R = Convert.ToInt16(Olcekleme * Math.Abs(OkunanRenk1.R - OkunanRenk2.R));
                    G = Convert.ToInt16(Olcekleme * Math.Abs(OkunanRenk1.G - OkunanRenk2.G));
                    B = Convert.ToInt16(Olcekleme * Math.Abs(OkunanRenk1.B - OkunanRenk2.B));

                    int YeniR = R, YeniG = G, YeniB = B;

                    //// ==========================================================
                    if (norm)
                    {
                        if (EnBuyukR < R) EnBuyukR = R;
                        if (EnBuyukG < G) EnBuyukG = G;
                        if (EnBuyukB < B) EnBuyukB = B;
                        if (EnKucukR > R) EnKucukR = R;
                        if (EnKucukG > G) EnKucukG = G;
                        if (EnKucukB > B) EnKucukB = B;

                        int EnBuyuk = 0, EnKucuk = 0;
                        if (EnBuyukR > EnBuyuk) EnBuyuk = EnBuyukR;
                        if (EnBuyukG > EnBuyuk) EnBuyuk = EnBuyukG;
                        if (EnBuyukB > EnBuyuk) EnBuyuk = EnBuyukB;
                        if (EnKucukR > EnKucuk) EnKucuk = EnKucukR;
                        if (EnKucukG > EnKucuk) EnKucuk = EnKucukG;
                        if (EnKucukB > EnKucuk) EnKucuk = EnKucukB;

                        YeniR = (255 * (R - EnKucuk)) / (EnBuyuk - EnKucuk);
                        YeniG = (255 * (G - EnKucuk)) / (EnBuyuk - EnKucuk);
                        YeniB = (255 * (B - EnKucuk)) / (EnBuyuk - EnKucuk);

                        if (YeniR > 255) YeniR = 255;
                        if (YeniG > 255) YeniG = 255;
                        if (YeniB > 255) YeniB = 255;

                        if (YeniR < 0) YeniR = 0;
                        if (YeniG < 0) YeniG = 0;
                        if (YeniB < 0) YeniB = 0;
                    }
                    else
                    {
                        if (R > 255) YeniR = 255;
                        if (G > 255) YeniG = 255;
                        if (B > 255) YeniB = 255;
                        if (R < 0) YeniR = 0; if (G < 0) YeniG = 0; if (B < 0) YeniB = 0;
                    }
                    // ==========================================================
                    DonusenRenk = Color.FromArgb(YeniR, YeniG, YeniB);
                    CikisResmi.SetPixel(x, y, DonusenRenk);
                }
            }
            return CikisResmi;
        }

        public Bitmap KenarGoruntusuIleOrjinalResmiBirlestir(Bitmap OrjinalResim, Bitmap KenarGoruntusu, bool norm)
        {
            int EnBuyukR = 0;
            int EnBuyukG = 0;
            int EnBuyukB = 0;
            int EnKucukR = 0;
            int EnKucukG = 0;
            int EnKucukB = 0;

            Color OkunanRenk1, OkunanRenk2, DonusenRenk; Bitmap CikisResmi;
            int ResimGenisligi = OrjinalResim.Width; int ResimYuksekligi = OrjinalResim.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int R, G, B;
            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk1 = OrjinalResim.GetPixel(x, y);
                    OkunanRenk2 = KenarGoruntusu.GetPixel(x, y);
                    R = OkunanRenk1.R + OkunanRenk2.R;
                    G = OkunanRenk1.G + OkunanRenk2.G;
                    B = OkunanRenk1.B + OkunanRenk2.B;
                    int YeniR = R, YeniG = G, YeniB = B;

                    //// ==========================================================
                    if (norm)
                    {
                        if (EnBuyukR < R) EnBuyukR = R;
                        if (EnBuyukG < G) EnBuyukG = G;
                        if (EnBuyukB < B) EnBuyukB = B;
                        if (EnKucukR > R) EnKucukR = R;
                        if (EnKucukG > G) EnKucukG = G;
                        if (EnKucukB > B) EnKucukB = B;

                        int EnBuyuk = 0, EnKucuk = 0;
                        if (EnBuyukR > EnBuyuk) EnBuyuk = EnBuyukR;
                        if (EnBuyukG > EnBuyuk) EnBuyuk = EnBuyukG;
                        if (EnBuyukB > EnBuyuk) EnBuyuk = EnBuyukB;
                        if (EnKucukR > EnKucuk) EnKucuk = EnKucukR;
                        if (EnKucukG > EnKucuk) EnKucuk = EnKucukG;
                        if (EnKucukB > EnKucuk) EnKucuk = EnKucukB;

                        YeniR = (255 * (R - EnKucuk)) / (EnBuyuk - EnKucuk);
                        YeniG = (255 * (G - EnKucuk)) / (EnBuyuk - EnKucuk);
                        YeniB = (255 * (B - EnKucuk)) / (EnBuyuk - EnKucuk);

                        if (YeniR > 255) YeniR = 255;
                        if (YeniG > 255) YeniG = 255;
                        if (YeniB > 255) YeniB = 255;

                        if (YeniR < 0) YeniR = 0;
                        if (YeniG < 0) YeniG = 0;
                        if (YeniB < 0) YeniB = 0;
                    }
                    else
                    {
                        if (R > 255) YeniR = 255;
                        if (G > 255) YeniG = 255;
                        if (B > 255) YeniB = 255;
                        if (R < 0) YeniR = 0; if (G < 0) YeniG = 0; if (B < 0) YeniB = 0;
                    }

                    DonusenRenk = Color.FromArgb(YeniR, YeniG, YeniB);

                    CikisResmi.SetPixel(x, y, DonusenRenk);
                }
            }
            return CikisResmi;
        }

        private Bitmap MeanFiltresi(Bitmap GirisResmi, int k)
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

        private Bitmap MedyanFiltresi(Bitmap GirisResmi, int l)
        {
            Color OkunanRenk;
            Bitmap CikisResmi;
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

        private Bitmap GaussFiltresi(Bitmap GirisResmi, int l, double std)
        {
            Color OkunanRenk;
            Bitmap CikisResmi;
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

        private Bitmap KonvolsiyonNetlestirme(Bitmap GirisResmi, int l, bool norm, int g)
        {
            Color OkunanRenk;
            Bitmap CikisResmi;
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            int x, y, i, j, toplamR, toplamG, toplamB;
            int R, G, B;

            int SablonBoyutu = l;
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            double[] Matris;

            int gecici;
            if (l == 3)
            {
                if (g == 13) gecici = 5;
                else if (g == 11) gecici = 7;
                else if (g == 9) gecici = 9;
                else if (g == 7) gecici = 11;
                else if (g == 5) gecici = 13;
                else gecici = 5;

                Matris = new double[]
                {
                    0, -1, 0,
                    -1, gecici, -1,
                    0, -1, 0
                };
            }
            else if (l == 5)
            {
                Matris = new double[]
                {
                    -1, -1, -1, -1, -1,
                    -1,  2,  2,  2, -1,
                    -1,  2,  8,  2, -1,
                    -1,  2,  2,  2, -1,
                    -1, -1, -1, -1, -1
                };
            }
            else
            {
                Matris = new double[]
                {
                    -1, -1, -1, -1, -1, -1, -1,
                    -1, -1, -1, -1, -1, -1, -1,
                    -1, -1,  3,  3,  3, -1, -1,
                    -1, -1,  3, 20,  3, -1, -1,
                    -1, -1,  3,  3,  3, -1, -1,
                    -1, -1, -1, -1, -1, -1, -1,
                    -1, -1, -1, -1, -1, -1, -1
                };
            }

            double MatrisToplami = 0.0;
            foreach (double sayi in Matris)
            {
                MatrisToplami += sayi;
            }

            int EnBuyukR = 0;
            int EnBuyukG = 0;
            int EnBuyukB = 0;
            int EnKucukR = 0;
            int EnKucukG = 0;
            int EnKucukB = 0;

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
                            toplamR = (int)(toplamR + OkunanRenk.R * Matris[k]);
                            toplamG = (int)(toplamG + OkunanRenk.G * Matris[k]);
                            toplamB = (int)(toplamB + OkunanRenk.B * Matris[k]);
                            k++;
                        }
                    }
                    R = (int)(toplamR / MatrisToplami);
                    G = (int)(toplamG / MatrisToplami);
                    B = (int)(toplamB / MatrisToplami);
                    //===========================================================
                    int YeniR = R, YeniG = G, YeniB = B;

                    if (norm)
                    {
                        if (EnBuyukR < R) EnBuyukR = R;
                        if (EnBuyukG < G) EnBuyukG = G;
                        if (EnBuyukB < B) EnBuyukB = B;
                        if (EnKucukR > R) EnKucukR = R;
                        if (EnKucukG > G) EnKucukG = G;
                        if (EnKucukB > B) EnKucukB = B;

                        int EnBuyuk = 0, EnKucuk = 0;
                        if (EnBuyukR > EnBuyuk) EnBuyuk = EnBuyukR;
                        if (EnBuyukG > EnBuyuk) EnBuyuk = EnBuyukG;
                        if (EnBuyukB > EnBuyuk) EnBuyuk = EnBuyukB;
                        if (EnKucukR > EnKucuk) EnKucuk = EnKucukR;
                        if (EnKucukG > EnKucuk) EnKucuk = EnKucukG;
                        if (EnKucukB > EnKucuk) EnKucuk = EnKucukB;

                        YeniR = (255 * (R - EnKucuk)) / (EnBuyuk - EnKucuk);
                        YeniG = (255 * (G - EnKucuk)) / (EnBuyuk - EnKucuk);
                        YeniB = (255 * (B - EnKucuk)) / (EnBuyuk - EnKucuk);

                        if (YeniR > 255) YeniR = 255;
                        if (YeniG > 255) YeniG = 255;
                        if (YeniB > 255) YeniB = 255;

                        if (YeniR < 0) YeniR = 0;
                        if (YeniG < 0) YeniG = 0;
                        if (YeniB < 0) YeniB = 0;
                    }
                    else
                    {
                        if (R > 255) YeniR = 255;
                        if (G > 255) YeniG = 255;
                        if (B > 255) YeniB = 255;
                        if (R < 0) YeniR = 0; if (G < 0) YeniG = 0; if (B < 0) YeniB = 0;
                    }
                    //===========================================================
                    CikisResmi.SetPixel(x, y, Color.FromArgb(YeniR, YeniG, YeniB));
                }
            }
            return CikisResmi;
        }



        private void button5_Click(object sender, EventArgs e)
        {
            bool norm = false;
            Bitmap OrjinalResim = new Bitmap(pictureBox1.Image);
            Bitmap BulanikResim = MeanFiltresi(OrjinalResim, 3);
            //Bitmap BulanikResim = GaussFiltresi();
            Bitmap KenarGoruntusu = OrjianalResimdenBulanikResmiCikarma(OrjinalResim, BulanikResim, norm);
            Bitmap NetlesmisResim = KenarGoruntusuIleOrjinalResmiBirlestir(OrjinalResim, KenarGoruntusu, norm);
            pictureBox2.Image = NetlesmisResim;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool norm = false;
            Bitmap OrjinalResim = new Bitmap(pictureBox1.Image);
            Bitmap BulanikResim = MedyanFiltresi(OrjinalResim, 3);
            Bitmap KenarGoruntusu = OrjianalResimdenBulanikResmiCikarma(OrjinalResim, BulanikResim, norm);
            Bitmap NetlesmisResim = KenarGoruntusuIleOrjinalResmiBirlestir(OrjinalResim, KenarGoruntusu, norm);
            pictureBox2.Image = NetlesmisResim;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool norm = false;
            Bitmap OrjinalResim = new Bitmap(pictureBox1.Image);
            Bitmap BulanikResim = GaussFiltresi(OrjinalResim, 3, 1);
            Bitmap KenarGoruntusu = OrjianalResimdenBulanikResmiCikarma(OrjinalResim, BulanikResim, norm);
            Bitmap NetlesmisResim = KenarGoruntusuIleOrjinalResmiBirlestir(OrjinalResim, KenarGoruntusu, norm);
            pictureBox2.Image = NetlesmisResim;
        }

        private void hafta6_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int k = 3;
            int index = comboBox1.SelectedIndex;
            if (index == 0) k = 3;
            else if (index == 1) k = 5;
            else k = 7;

            bool norm = false;

            Bitmap OrjinalResim = new Bitmap(pictureBox1.Image);
            Bitmap NetlesmisResim = KonvolsiyonNetlestirme(OrjinalResim, k, norm, -1);
            pictureBox2.Image = NetlesmisResim;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool norm = true;
            Bitmap OrjinalResim = new Bitmap(pictureBox1.Image);
            Bitmap BulanikResim = MeanFiltresi(OrjinalResim, 3);
            //Bitmap BulanikResim = GaussFiltresi();
            Bitmap KenarGoruntusu = OrjianalResimdenBulanikResmiCikarma(OrjinalResim, BulanikResim, norm);
            Bitmap NetlesmisResim = KenarGoruntusuIleOrjinalResmiBirlestir(OrjinalResim, KenarGoruntusu, norm);
            pictureBox2.Image = NetlesmisResim;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            bool norm = false;

            Bitmap OrjinalResim = new Bitmap(pictureBox1.Image);
            Bitmap NetlesmisResim = KonvolsiyonNetlestirme(OrjinalResim, 3, norm, -1);
            pictureBox2.Image = NetlesmisResim;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool norm = true;
            Bitmap OrjinalResim = new Bitmap(pictureBox1.Image);
            Bitmap BulanikResim = MedyanFiltresi(OrjinalResim, 3);
            Bitmap KenarGoruntusu = OrjianalResimdenBulanikResmiCikarma(OrjinalResim, BulanikResim, norm);
            Bitmap NetlesmisResim = KenarGoruntusuIleOrjinalResmiBirlestir(OrjinalResim, KenarGoruntusu, norm);
            pictureBox2.Image = NetlesmisResim;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool norm = false;
            Bitmap OrjinalResim = new Bitmap(pictureBox1.Image);
            Bitmap BulanikResim = GaussFiltresi(OrjinalResim, 3, 1);
            Bitmap KenarGoruntusu = OrjianalResimdenBulanikResmiCikarma(OrjinalResim, BulanikResim, norm);
            Bitmap NetlesmisResim = KenarGoruntusuIleOrjinalResmiBirlestir(OrjinalResim, KenarGoruntusu, norm);
            pictureBox2.Image = NetlesmisResim;
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

        Point nokta0;
        Point nokta1;
        byte sayac = 0;
        bool alanSecme = false;
        bool ilkTiklama = true;
        Bitmap yedekResim;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (alanSecme)
            {
                if (sayac == 2)
                {
                    pictureBox1.Image = new Bitmap(yedekResim);
                    sayac = 0;
                }

                MouseEventArgs me = (MouseEventArgs)e;
                Point nokta = me.Location;

                Point yeniNokta = Oranla(pictureBox1, nokta.X, nokta.Y);

                int x = yeniNokta.X;
                int y = yeniNokta.Y;

                if (ilkTiklama)
                {
                    yedekResim = new Bitmap(pictureBox1.Image);
                    ilkTiklama = false;
                    pictureBox1.Image = NoktaCiz(new Bitmap(pictureBox1.Image), x - 2, y - 2);
                    nokta0 = new Point(x, y);
                    sayac++;
                }
                else
                {
                    if (x > nokta0.X && y > nokta0.X)
                    {
                        nokta1.X = x;
                        nokta1.Y = y;
                    }
                    else if (x > nokta0.X && y < nokta0.Y)
                    {
                        nokta1.X = x;
                        nokta1.Y = nokta0.Y;
                        nokta0.Y = y;
                    }
                    else if (x < nokta0.X && y < nokta0.Y)
                    {
                        nokta1 = nokta0;

                        nokta0.X = x;
                        nokta0.Y = y;
                    }
                    else
                    {
                        nokta1.Y = y;
                        nokta1.X = nokta0.X;
                        nokta0.X = x;
                    }

                    pictureBox1.Image = NoktaCiz(new Bitmap(pictureBox1.Image), x - 2, y - 2);
                    Bitmap resim = new Bitmap(pictureBox1.Image);
                    pictureBox1.Image = DikdortgenCiz(resim, nokta0.X, nokta0.Y, Math.Abs(nokta0.X - nokta1.X), Math.Abs(nokta0.Y - nokta1.Y));
                    ilkTiklama = true;
                    sayac++;
                }

            }
        }

        private Bitmap NetBlur(Bitmap GirisResmi, string net, string blur, string ic)
        {
            Color OkunanRenk;
            Bitmap CikisResmi;
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            Bitmap blurlu = new Bitmap(ResimGenisligi, ResimYuksekligi);
            Bitmap netKisim;

            if (ic == "net")
            {
                if (blur == "mean")
                    blurlu = MeanFiltresi(new Bitmap(GirisResmi), 5);
                else if (blur == "medyan")
                    blurlu = MedyanFiltresi(new Bitmap(GirisResmi), 5);
                else
                    blurlu = GaussFiltresi(new Bitmap(GirisResmi), 5, 1);

                int alanWidth = Math.Abs(nokta1.X - nokta0.X);
                int alanHeight = Math.Abs(nokta1.Y - nokta0.Y);

                Bitmap alan = new Bitmap(alanWidth, alanHeight);

                for (int x = nokta0.X; x < nokta1.X; x++)
                {
                    for (int y = nokta0.Y; y < nokta1.Y; y++)
                    {
                        alan.SetPixel(x - nokta0.X, y - nokta0.Y, GirisResmi.GetPixel(x, y));
                    }
                }

                netKisim = new Bitmap(alanWidth, alanHeight);

                if (net == "kenar")
                {
                    bool norm = false;
                    Bitmap OrjinalResim = new Bitmap(alan);
                    Bitmap BulanikResim = new Bitmap(MeanFiltresi(OrjinalResim, 3));
                    Bitmap KenarGoruntusu = new Bitmap(OrjianalResimdenBulanikResmiCikarma(OrjinalResim, BulanikResim, norm));
                    netKisim = new Bitmap((KenarGoruntusuIleOrjinalResmiBirlestir(OrjinalResim, KenarGoruntusu, norm)));
                }
                else
                {
                    bool norm = false;
                    Bitmap OrjinalResim = new Bitmap(alan);
                    netKisim = new Bitmap(KonvolsiyonNetlestirme(OrjinalResim, 3, norm, -1));
                }

                CikisResmi = new Bitmap(blurlu);

                for (int x = 0; x < alanWidth; x++)
                {
                    for (int y = 0; y < alanHeight; y++)
                    {
                        CikisResmi.SetPixel(x + nokta0.X, y + nokta0.Y, netKisim.GetPixel(x, y));
                    }
                }
            }
            else
            {
                if (net == "kenar")
                {
                    bool norm = false;
                    Bitmap OrjinalResim = new Bitmap(GirisResmi);
                    Bitmap BulanikResim = new Bitmap(MeanFiltresi(OrjinalResim, 3));
                    Bitmap KenarGoruntusu = new Bitmap(OrjianalResimdenBulanikResmiCikarma(OrjinalResim, BulanikResim, norm));
                    netKisim = new Bitmap((KenarGoruntusuIleOrjinalResmiBirlestir(OrjinalResim, KenarGoruntusu, norm)));
                }
                else
                {
                    bool norm = false;
                    Bitmap OrjinalResim = new Bitmap(GirisResmi);
                    netKisim = new Bitmap(KonvolsiyonNetlestirme(OrjinalResim, 3, norm, -1));
                }

                int alanWidth = Math.Abs(nokta1.X - nokta0.X);
                int alanHeight = Math.Abs(nokta1.Y - nokta0.Y);

                Bitmap alan = new Bitmap(alanWidth, alanHeight);

                for (int x = nokta0.X; x < nokta1.X; x++)
                {
                    for (int y = nokta0.Y; y < nokta1.Y; y++)
                    {
                        alan.SetPixel(x - nokta0.X, y - nokta0.Y, GirisResmi.GetPixel(x, y));
                    }
                }

                if (blur == "mean")
                    blurlu = MeanFiltresi(new Bitmap(alan), 5);
                else if (blur == "medyan")
                    blurlu = MedyanFiltresi(new Bitmap(alan), 5);
                else
                    blurlu = GaussFiltresi(new Bitmap(alan), 5, 1);

                CikisResmi = new Bitmap(netKisim);

                for (int x = 0; x < alanWidth; x++)
                {
                    for (int y = 0; y < alanHeight; y++)
                    {
                        CikisResmi.SetPixel(x + nokta0.X, y + nokta0.Y, blurlu.GetPixel(x, y));
                    }
                }
            }
            return CikisResmi;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (sayac < 2)
            {
                MessageBox.Show("Lutfen 2 noktaya tiklayin");
            }
            else
            {

                string net;
                string blur;
                string ic;

                if (radioButton1.Checked)
                {
                    net = "kenar";
                }
                else
                {
                    net = "konv";
                }

                if (radioButton3.Checked)
                {
                    blur = "mean";
                }
                else if (radioButton4.Checked)
                {
                    blur = "medyan";
                }
                else
                {
                    blur = "gauss";
                }

                if (radioButton10.Checked)
                {
                    ic = "net";
                }
                else
                {
                    ic = "blur";
                }

                pictureBox2.Image = NetBlur(new Bitmap(yedekResim), net, blur, ic);

                pictureBox1.Image = new Bitmap(yedekResim);
                sayac = 0;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (!alanSecme)
            {
                label3.BackColor = Color.FromArgb(192, 255, 192);
                label3.Text = "aktif";
                alanSecme = true;
            }
            else
            {
                label3.BackColor = Color.Pink;
                label3.Text = "pasif";
                alanSecme = false;
                pictureBox1.Image = new Bitmap(yedekResim);
                sayac = 0;
                ilkTiklama = true;

            }
        }

        bool mouseNet = false;
        private void button17_Click(object sender, EventArgs e)
        {
            if (!mouseNet)
            {
                label8.BackColor = Color.FromArgb(192, 255, 192);
                label8.Text = "aktif";
                mouseNet = true;
                yedekResim = new Bitmap(pictureBox1.Image);
            }
            else
            {
                label8.BackColor = Color.Pink;
                label8.Text = "pasif";
                mouseNet = false;
                pictureBox1.Image = yedekResim;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point nokta = me.Location;

            Point yeniNokta = Oranla(pictureBox1, nokta.X, nokta.Y);

            int x0 = yeniNokta.X;
            int y0 = yeniNokta.Y;

            if (mouseNet)
            {
                int boyut = Convert.ToInt16(numericUpDown1.Value);
                int kenar = Convert.ToInt16(numericUpDown2.Value);
                int yogunluk = Convert.ToInt16(numericUpDown3.Value);

                string sekil = "daire";
                string net = "konv";

                if (radioButton8.Checked == true) { sekil = "daire"; boyut /= 2; }
                else if (radioButton9.Checked == true) sekil = "kare";

                Bitmap resim = new Bitmap(yedekResim);

                //for (int i = 0; i < resim.Width; i++)
                //{
                //    for (int j = 0; j < resim.Height; j++)
                //    {
                //        Color renk;
                //        bool kontrol = i > (x0 - (boyut / 2)) && i < (x0 + (boyut / 2)) && j > (y0 - (boyut / 2)) && j < (y0 + (boyut / 2));
                //        if (kontrol) renk = Color.FromArgb(255, 255, 255);
                //        else renk = Color.FromArgb(0, 0, 0);
                //        resim.SetPixel(i, j, renk);
                //    }
                //}
                //pictureBox1.Image = resim;

                pictureBox1.Image = MouseEtrafinaNetUygula(resim, net, yeniNokta, boyut, kenar, yogunluk, sekil);

            }
        }

        Bitmap mean5 = null;
        Bitmap mean7 = null;
        Bitmap mean9 = null;
        Bitmap mean11 = null;
        Bitmap mean13 = null;
        Bitmap meanGecici = null;
        Bitmap medyan5 = null;
        Bitmap medyan7 = null;
        Bitmap medyan9 = null;
        Bitmap medyan11 = null;
        Bitmap medyan13 = null;
        Bitmap medyanGecici = null;

        private Bitmap MouseEtrafinaNetUygula(Bitmap resim, string netTuru, Point nokta, int boyut, int kenar, int yogunluk, string sekil)
        {
            int x0 = nokta.X;
            int y0 = nokta.Y;

            int w = resim.Width;
            int h = resim.Height;

            Color OkunanRenk;
            Bitmap CikisResmi;
            int ResimGenisligi = resim.Width;
            int ResimYuksekligi = resim.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            int kk;

            bool norm = false;

            if (yogunluk <= 20)
            {
                kk = 5;
                if (netTuru == "konv")
                {
                    if (mean5 == null)
                        mean5 = KonvolsiyonNetlestirme(resim, 3, norm, 5);
                    meanGecici = mean5;
                }
                else
                {
                    if (medyan5 == null)
                        //medyan5 = Mean(resim, 5);
                        medyanGecici = medyan5;
                }


            }
            else if (yogunluk <= 40)
            {
                kk = 7;
                if (netTuru == "konv")
                {
                    if (mean7 == null)
                        mean7 = KonvolsiyonNetlestirme(resim, 3, norm, 7);
                    meanGecici = mean7;
                }
                else
                {
                    if (medyan7 == null)
                        //medyan7 = Mean(resim, 7);
                        medyanGecici = medyan7;
                }
            }
            else if (yogunluk <= 60)
            {
                kk = 9;
                if (netTuru == "konv")
                {
                    if (mean9 == null)
                        mean9 = KonvolsiyonNetlestirme(resim, 3, norm, 9);
                    meanGecici = mean7;
                }
                else
                {
                    if (medyan9 == null)
                        //medyan9 = Mean(resim, 9);
                        medyanGecici = medyan9;
                }
            }
            else if (yogunluk <= 80)
            {
                kk = 11;
                if (netTuru == "konv")
                {
                    if (mean11 == null)
                        mean11 = KonvolsiyonNetlestirme(resim, 3, norm, 11);
                    meanGecici = mean11;
                }
                else
                {
                    if (medyan11 == null)
                        //medyan11 = Mean(resim, 11);
                        medyanGecici = medyan11;
                }
            }
            else
            {
                kk = 13;
                if (netTuru == "konv")
                {
                    if (mean13 == null)
                        mean13 = KonvolsiyonNetlestirme(resim, 3, norm, 13);
                    meanGecici = mean13;
                }
                else
                {
                    if (medyan13 == null)
                        //medyan13 = Mean(resim, 13);
                        medyanGecici = medyan13;
                }
            }

            int SablonBoyutu = kk;

            bool kontrol;
            //double kontrol = Math.Sqrt(Math.Pow((i - (x0)), 2) + Math.Pow((j - (y0)), 2));
            //if (kontrol < boyut) //renk = //MouseEtrafinaBlurUygula(i, j, resim.GetPixel(i, j));
            //                else renk = Color.FromArgb(0, 0, 0, 0);

            if (netTuru == "konv")
            {
                int x, y, i, j;

                for (x = 0; x < ResimGenisligi; x++)
                {
                    for (y = 0; y < ResimYuksekligi; y++)
                    {
                        if (sekil == "daire")
                        {
                            double hesap = Math.Sqrt(Math.Pow(x - x0, 2) + Math.Pow(y - y0, 2));
                            kontrol = hesap < boyut;

                            double d = 150.0 / kenar;
                            double hesap2 = (boyut / d);
                            double v = boyut - hesap2;
                            if (kk == 7)
                            {
                                if (hesap > v + hesap2)
                                {
                                    if (mean5 == null) mean5 = KonvolsiyonNetlestirme(resim, 3, norm, 5);
                                    meanGecici = mean5;
                                }
                                else
                                {
                                    if (mean7 == null) mean7 = KonvolsiyonNetlestirme(resim, 3, norm, 7);
                                    meanGecici = mean7;
                                }
                            }
                            else if (kk == 9)
                            {
                                if (hesap > v + hesap2)
                                {
                                    if (mean5 == null) mean5 = KonvolsiyonNetlestirme(resim, 3, norm, 5);
                                    meanGecici = mean5;
                                }
                                else if (hesap > v + (hesap2 / 2))
                                {
                                    if (mean7 == null) mean7 = KonvolsiyonNetlestirme(resim, 3, norm, 7);
                                    meanGecici = mean7;
                                }
                                else
                                {
                                    if (mean9 == null) mean9 = KonvolsiyonNetlestirme(resim, 3, norm, 9);
                                    meanGecici = mean9;
                                }
                            }
                            else if (kk == 11)
                            {
                                if (hesap > v + 3 * (hesap2 / 3)) { if (mean5 == null) mean5 = KonvolsiyonNetlestirme(resim, 3, norm, 5); meanGecici = mean5; }
                                else if (hesap > v + 2 * (hesap2 / 3)) { if (mean7 == null) mean7 = KonvolsiyonNetlestirme(resim, 3, norm, 7); meanGecici = mean7; }
                                else if (hesap > v + (hesap2 / 3)) { if (mean9 == null) mean9 = KonvolsiyonNetlestirme(resim, 3, norm, 9); meanGecici = mean9; }
                                else { if (mean11 == null) mean11 = KonvolsiyonNetlestirme(resim, 3, norm, 11); meanGecici = mean11; }
                            }
                            else if (kk == 13)
                            {
                                if (hesap > v + hesap2) { if (mean5 == null) mean5 = KonvolsiyonNetlestirme(resim, 3, norm, 5); meanGecici = mean5; }
                                else if (hesap > v + 3 * (hesap2 / 4)) { if (mean7 == null) mean7 = KonvolsiyonNetlestirme(resim, 3, norm, 7); meanGecici = mean7; }
                                else if (hesap > v + 2 * (hesap2 / 4)) { if (mean9 == null) mean9 = KonvolsiyonNetlestirme(resim, 3, norm, 9); meanGecici = mean9; }
                                else if (hesap > v + (hesap2 / 4)) { if (mean11 == null) mean11 = KonvolsiyonNetlestirme(resim, 3, norm, 11); meanGecici = mean11; }
                                else { if (mean13 == null) mean13 = KonvolsiyonNetlestirme(resim, 3, norm, 13); meanGecici = mean13; }
                            }

                        }
                        else
                        {
                            kontrol = x > (x0 - (boyut / 2)) && x < (x0 + (boyut / 2)) && y > (y0 - (boyut / 2)) && y < (y0 + (boyut / 2));
                            if (kk == 5) { if (mean5 == null) mean5 = KonvolsiyonNetlestirme(resim, 3, norm, 5); meanGecici = mean5; }
                            else if (kk == 7) { if (mean7 == null) mean7 = KonvolsiyonNetlestirme(resim, 3, norm, 7); meanGecici = mean7; }
                            else if (kk == 9) { if (mean9 == null) mean9 = KonvolsiyonNetlestirme(resim, 3, norm, 9); meanGecici = mean9; }
                            else if (kk == 11) { if (mean11 == null) mean11 = KonvolsiyonNetlestirme(resim, 3, norm, 11); meanGecici = mean11; }
                            else if (kk == 13) { if (mean13 == null) mean13 = KonvolsiyonNetlestirme(resim, 3, norm, 13); meanGecici = mean13; }
                        }

                        if (kontrol)
                        {
                            CikisResmi.SetPixel(x, y, meanGecici.GetPixel(x, y));
                        }
                        else
                        {
                            CikisResmi.SetPixel(x, y, resim.GetPixel(x, y));
                        }
                    }
                }
            }
            return CikisResmi;
        }
    }
}
