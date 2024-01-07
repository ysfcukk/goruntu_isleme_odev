using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace odev
{
    public partial class hafta7 : Form
    {
        public hafta7()
        {
            InitializeComponent();
        }

        private Bitmap Sobel(Bitmap GirisResmi)
        {
            Color OkunanRenk;
            Bitmap CikisResmiX, CikisResmiY, CikisResmiXY;
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmiX = new Bitmap(ResimGenisligi, ResimYuksekligi);
            CikisResmiY = new Bitmap(ResimGenisligi, ResimYuksekligi);
            CikisResmiXY = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = 3;
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int x, y, i, j; int Gri = 0;
            int[] MatrisX = { -1, 0, 1, -2, 0, 2, -1, 0, 1 };
            int[] MatrisY = { 1, 2, 1, 0, 0, 0, -1, -2, -1 };
            int RenkX, RenkY, RenkXY;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    int toplamGriX = 0, toplamGriY = 0;
                    //Şablon bölgesi (çekirdek matris) içindeki pikselleri tarıyor.
                    int k = 0; //matris içindeki elemanları sırayla okurken kullanılacak.
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            Gri = (OkunanRenk.R + OkunanRenk.G + OkunanRenk.B) / 3;
                            toplamGriX = toplamGriX + Gri * MatrisX[k];
                            toplamGriY = toplamGriY + Gri * MatrisY[k];
                            k++;
                        }
                    }
                    RenkX = Math.Abs(toplamGriX);
                    RenkY = Math.Abs(toplamGriY);
                    RenkXY = Math.Abs(toplamGriX) + Math.Abs(toplamGriY);
                    //===========================================================
                    //Renkler sınırların dışına çıktıysa, sınır değer alınacak.
                    if (RenkX > 255) RenkX = 255;
                    if (RenkY > 255) RenkY = 255;
                    if (RenkXY > 255) RenkXY = 255;
                    if (RenkX < 0) RenkX = 0;
                    if (RenkY < 0) RenkY = 0;
                    if (RenkXY < 0) RenkXY = 0;
                    //===========================================================
                    CikisResmiX.SetPixel(x, y, Color.FromArgb(RenkX, RenkX, RenkX));
                    CikisResmiY.SetPixel(x, y, Color.FromArgb(RenkY, RenkY, RenkY));
                    CikisResmiXY.SetPixel(x, y, Color.FromArgb(RenkXY, RenkXY, RenkXY));
                }
            }
            return CikisResmiXY;
        }

        private Bitmap Prewitt(Bitmap GirisResmi)
        {
            Bitmap CikisResmi;
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = 3;
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int x, y; Color Renk;
            int P1, P2, P3, P4, P5, P6, P7, P8, P9;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    Renk = GirisResmi.GetPixel(x - 1, y - 1);
                    P1 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x, y - 1);
                    P2 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x + 1, y - 1);
                    P3 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x - 1, y);
                    P4 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x, y);
                    P5 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x + 1, y);
                    P6 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x - 1, y + 1);
                    P7 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x, y + 1);
                    P8 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x + 1, y + 1);
                    P9 = (Renk.R + Renk.G + Renk.B) / 3;
                    int Gx = Math.Abs(-P1 + P3 - P4 + P6 - P7 + P9); //Dikey çizgileri Bulur
                    int Gy = Math.Abs(P1 + P2 + P3 - P7 - P8 - P9); //Yatay Çizgileri Bulur.
                    int PrewittDegeri = 0;
                    PrewittDegeri = Gx + Gy;
                    if (PrewittDegeri > 255) PrewittDegeri = 255;

                    CikisResmi.SetPixel(x, y, Color.FromArgb(PrewittDegeri, PrewittDegeri, PrewittDegeri));
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

        private Bitmap MakeGrayscale(Bitmap original)
        {
            try
            {
                Color originalColor; Color newColor;
                Bitmap newBitmap = new Bitmap(original.Width, original.Height);
                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        originalColor = original.GetPixel(i, j);
                        int grayScale = (int)((originalColor.R * 0.3) + (originalColor.G * 0.59) + (originalColor.B * 0.11));
                        newColor = Color.FromArgb(grayScale, grayScale, grayScale);
                        newBitmap.SetPixel(i, j, newColor);
                    }
                return newBitmap;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        private Bitmap MakeSmooth(Bitmap original
)
        {
            int runningSum
            = 0; int tempSum
            = 0; int xcoord
            ;
            int ycoord; Color newColor
            ;
            int[,] kernel
            = new int[5, 5]
            {
{1,4,7,4,1}, {4,16,26,16,4}, {7,26,41,26,7}, {4,16,26,16,4}, {1,4,7,4,1} };
            Color[,] pixels
            = new Color[5, 5];
            Bitmap newBitmap
            = new Bitmap(original.Width
            , original.Height);
            for
            (int
            i
            = 0;
            i
            < original.Width
            ; i++)
                for
                (int
                j
                = 0;
                j
                < original.Height
                ; j++)
                {
                    for
                    (int
                    x
                    =
                    -2;
                    x
                    < 3; x++)
                        for
                        (int
                        y
                        =
                        -2;
                        y
                        < 3; y++)
                        {
                            xcoord
                            =
                            i
                            + x;
                            ycoord
                            =
                            j
                            + y;
                            if
                            (xcoord
                            <
                            0 || xcoord
                            > original.Width
                            - 1)
                                xcoord
                                =
                                i
                                - x;
                            if
                            (ycoord
                            <
                            0 || ycoord
                            > original.Height
                            - 1)
                                ycoord
                                =
                                j
                                - y;
                            pixels[x
                            + 2,
                            y
                            + 2]
                            = original.GetPixel
                            (xcoord
                            , ycoord);
                        }
                    for
                    (int
                    k
                    = 0;
                    k
                    < 5; k++)
                        for
                        (int
                        l
                        = 0;
                        l
                        < 5; l++)
                            tempSum += kernel[k, l]
                            * pixels[k, l].R;
                    runningSum
                    = tempSum
                    / 273;
                    newColor
                    = Color.FromArgb
                    (runningSum
                    , runningSum
                    , runningSum);
                    newBitmap.SetPixel(i
                    , j, newColor);
                    tempSum
                    = 0;
                    runningSum
                    = 0;
                }
            return newBitmap
            ;
        }

        private Bitmap DetectEdge(Bitmap original
)
        {
            Bitmap newBitmap
            = new Bitmap(original.Width
            , original.Height);
            int xleft
            ;
            int xright
            ;
            int ytop;
            int ybot;
            double gx;
            double gy
            ;
            double tempAngle; Color color1, color2;
            double[,] magnitudes
            = new double
            [original.Width
            , original.Height];
            double[,] angles
            = new double
            [original.Width
            , original.Height];
            bool[,] isEdge
            = new bool
            [original.Width
            , original.Height];
            double maxMag
            = 0;
            for
            (int
            i
            = 0;
            i
            < original.Width
            ; i++)
                for
                (int
                j
                = 0;
                j
                < original.Height
                ; j++)
                {
                    xleft
                    =
                    i
                    - 1; xright
                    =
                    i
                    + 1; ytop
                    =
                    j
                    - 1; ybot
                    =
                    j
                    + 1;
                    if
                    (xleft
                    < 0)
                        xleft
                        = xright
                        ;
                    if
                    (xright
                    > original.Width
                    -
                    1
                    )
                        xright
                        = xleft
                        ;
                    if
                    (ytop
                    < 0)
                        ytop
                        = ybot
                        ;
                    if
                    (ybot
                    > original.Height
                    - 1)
                        ybot
                        = ytop
                        ;
                    color1 = original.GetPixel
                    (xright
                    , j);
                    color2 = original.GetPixel
                    (xleft
                    , j);
                    gx
                    = (color1.R
                    - color2.R)
                    / 2;
                    color1 = original.GetPixel(i, ybot);
                    color2 = original.GetPixel(i, ytop);
                    gy
                    = (color1.R
                    - color2.R)
                    / 2;
                    magnitudes[i, j]
                    = Math.Abs
                    (gx
                    )
                    + Math.Abs
                    (gy);

                    if (magnitudes[i, j] > maxMag)
                        maxMag = magnitudes[i, j];
                    tempAngle = Math.Atan(gy / gx); tempAngle = tempAngle * 180 / Math.PI;
                    if ((tempAngle >= 0 && tempAngle < 22.5) || (tempAngle > 157.5 && tempAngle <= 180)
                    || (tempAngle <= 0 && tempAngle > -22.5) || (tempAngle < -157.5 && tempAngle >= -180)) tempAngle = 0.0;
                    else if ((tempAngle > 22.5 && tempAngle < 67.5) || (tempAngle < -22.5 && tempAngle >
                    -67.5))
                        tempAngle = 45.0;
                    else if ((tempAngle > 67.5 && tempAngle < 112.5) || (tempAngle < -67.5 && tempAngle
                    > -112.5))
                        tempAngle = 90.0;
                    else if ((tempAngle > 112.5 && tempAngle < 157.5) || (tempAngle < -112.5 && tempAngle > -157.5))
                        tempAngle = 135.0; angles[i, j] = tempAngle;
                }
            for (int i = 0; i < original.Width; i++)
                for (int j = 0; j < original.Height; j++)
                {
                    if ((i - 1 < 0 || i + 1 > original.Width - 1 || j - 1 < 0 || j + 1 > original.Height
                    - 1))
                    {
                        isEdge[i, j] = false;
                    }
                    else if (angles[i, j] == 0.0)
                    {
                        if (magnitudes[i, j] > magnitudes[i - 1, j] && magnitudes[i, j] > magnitudes[i +
                        1, j])
                            isEdge[i, j] = true;
                        else
                            isEdge[i, j] = false;
                    }
                    else if (angles[i, j] == 90.0)
                    {
                        if (magnitudes[i, j] > magnitudes[i, j - 1] && magnitudes[i, j] > magnitudes[i, j + 1])
                            isEdge[i, j] = true;
                        else
                            isEdge[i, j] = false;
                    }
                    else if (angles[i, j] == 135.0)
                    {
                        if (magnitudes[i, j] > magnitudes[i - 1, j - 1] && magnitudes[i, j] > magnitudes[i + 1, j + 1])
                            isEdge[i, j] = true;
                        else
                            isEdge[i, j] = false;
                    }
                    else if (angles[i, j] == 45.0)
                    {
                        if (magnitudes[i, j] > magnitudes[i + 1, j - 1] && magnitudes[i, j] > magnitudes[i - 1, j + 1])
                            isEdge[i, j] = true;
                        else
                            isEdge[i, j] = false;
                    }
                }
            double lowerThreshold = maxMag * 0.10;
            for (int i = 0; i < original.Width; i++)
                for (int j = 0; j < original.Height; j++)
                {
                    if (isEdge[i, j] && magnitudes[i, j] > lowerThreshold)
                    {
                        if (angles[i, j] == 0.0)
                        {
                            if (angles[i, j] == angles[i - 1, j] || angles[i, j] == angles[i + 1, j])
                            {
                                if (magnitudes[i - 1, j] > lowerThreshold) newBitmap.SetPixel(i - 1, j, Color.White);
                                else
                                    newBitmap.SetPixel(i - 1, j, Color.Black); if (magnitudes[i + 1, j] > lowerThreshold)
                                    newBitmap.SetPixel(i + 1, j, Color.White);
                                else
                                    newBitmap.SetPixel(i + 1, j, Color.Black);
                            }
                        }
                        else if (angles[i, j] == 90.0)
                        {
                            if (angles[i, j] == angles[i, j - 1] || angles[i, j] == angles[i, j + 1])
                            {
                                if (magnitudes[i, j - 1] > lowerThreshold) newBitmap.SetPixel(i, j - 1, Color.White);
                                else
                                    newBitmap.SetPixel(i, j - 1, Color.Black); if (magnitudes[i, j + 1] > lowerThreshold)
                                    newBitmap.SetPixel(i, j + 1, Color.White);
                                else
                                    newBitmap.SetPixel(i, j + 1, Color.Black);
                            }
                        }
                        else if (angles[i, j] == 135.0)
                        {
                            if (angles[i, j] == angles[i - 1, j - 1] || angles[i, j] == angles[i + 1, j
                            + 1])
                            {
                                if (magnitudes[i - 1, j - 1] > lowerThreshold) newBitmap.SetPixel(i - 1, j - 1, Color.White);
                                else
                                    newBitmap.SetPixel(i - 1, j - 1, Color.Black);

                                if (magnitudes[i + 1, j + 1] > lowerThreshold)
                                    newBitmap.SetPixel(i + 1, j + 1, Color.White);
                                else
                                    newBitmap.SetPixel(i + 1, j + 1, Color.Black);
                            }
                        }
                        else if (angles[i, j] == 45.0)
                        {
                            if (angles[i, j] == angles[i + 1, j - 1] || angles[i, j] == angles[i - 1, j
                            + 1])
                            {
                                if (magnitudes[i + 1, j - 1] > lowerThreshold) newBitmap.SetPixel(i + 1, j - 1, Color.White);
                                else
                                    newBitmap.SetPixel(i + 1, j - 1, Color.Black);
                                if (magnitudes[i - 1, j + 1] > lowerThreshold)
                                    newBitmap.SetPixel(i - 1, j + 1, Color.White);
                                else
                                    newBitmap.SetPixel(i - 1, j + 1, Color.Black);
                            }
                        }
                    }
                    else
                        newBitmap.SetPixel(i, j, Color.Black);
                }
            return newBitmap;
        }

        public Bitmap BulaniklastirmaKenar(Bitmap OrjinalResim, Bitmap BulanikResim, bool norm)
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

        private Bitmap ResmiGriTonaDonusturEsiklemeYap(Bitmap GirisResmi)
        {
            int sayi = 255;

            int r, g, b;

            Color okunanRenk, donusenRenk;

            Bitmap girisResmi = GriyeDonustur(GirisResmi);

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
            return cikisResmi;
        }

        private Bitmap GenisletmeSinir(Bitmap GirisResmi)
        {
            Color OkunanRenk;
            Bitmap CikisResmi;
            Bitmap OrjinalResim, GenislemisResim;
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            Bitmap SiyahBeyazResim = ResmiGriTonaDonusturEsiklemeYap(GirisResmi);
            pictureBox2.Image = SiyahBeyazResim;
            GirisResmi = SiyahBeyazResim;
            int x, y, i, j;
            int SablonBoyutu = 3;
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    bool RenkSiyah = false;
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            if (OkunanRenk.R < 128) RenkSiyah = true;
                        }
                    }
                    if (RenkSiyah == true) //Komşularda siyah varsa
                    {
                        Color KendiRengi = GirisResmi.GetPixel(x, y);
                        if (KendiRengi.R > 128) //kendi rengin beyaz ise onu da siyah yap.
                            CikisResmi.SetPixel(x, y, Color.FromArgb(255, 0, 0)); //siyah yerine kırmızı kullandık. Genişleyen bölgeyi görmek için
                    }
                    else //komşularda siyah yok ise kendi rengi yine aynı beyaz kalmalı.
                        CikisResmi.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                }
            }
            //pictureBox3.Image = CikisResmi;
            //SiyahBeyazResim = new Bitmap(pictureBox2.Image);
            GenislemisResim = new Bitmap(CikisResmi);
            Bitmap KenarGoruntuResmi = OrjinalResimdenGenislemisResmiCikar(SiyahBeyazResim, GenislemisResim);
            return KenarGoruntuResmi;
        }

        public Bitmap OrjinalResimdenGenislemisResmiCikar(Bitmap SiyahBeyazResim, Bitmap GenislemisResim)
        {
            Bitmap CikisResmi;
            int ResimGenisligi = SiyahBeyazResim.Width;
            int ResimYuksekligi = SiyahBeyazResim.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int x, y;
            int Fark;
            for (x = 0; x < ResimGenisligi; x++) //Resmi taramaya şablonun yarısı kadar dış kenarlardan içeride başlayacak ve bitirecek.
            {
                for (y = 0; y < ResimYuksekligi; y++)
                {
                    Color OrjinalRenk = SiyahBeyazResim.GetPixel(x, y);
                    Color GenislemisResimRenk = GenislemisResim.GetPixel(x, y);
                    int OrjinalGri = (OrjinalRenk.R + OrjinalRenk.G + OrjinalRenk.B) / 3;
                    int GenislemisGri = (GenislemisResimRenk.R + GenislemisResimRenk.G + GenislemisResimRenk.B) / 3;
                    Fark = Math.Abs(OrjinalGri - GenislemisGri);
                    CikisResmi.SetPixel(x, y, Color.FromArgb(Fark, Fark, Fark));
                }
            }
            return CikisResmi;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = Sobel(resim);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = Prewitt(resim);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap newBitmap = (Bitmap)pictureBox1.Image;
            Bitmap newBitmap1 = new Bitmap(newBitmap.Width, newBitmap.Height);
            newBitmap1 = MakeGrayscale(newBitmap);
            newBitmap1 = MakeSmooth(newBitmap1);
            newBitmap1 = DetectEdge(newBitmap1);
            pictureBox2.Image = newBitmap1;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);

            pictureBox2.Image = BulaniklastirmaKenar(resim, GaussFiltresi(resim, 3, 1), false);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = GenisletmeSinir(resim);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.marylyn;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.fence;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.viewv;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.noisy;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string metin = "Canny algoritmasında hata aldım fakat araştırdığım kadarıyla insan fotoğraflarında güzel sonuçlar veriyor." +
                " Bulanıklaştırma algoritması aralında en kötüsü bence. Genellikle kötü sonuçlar veriyor. Aşındırma genel olarak gayet iyi." +
                " Sobel ve Prewit özellikle manzara resimlerinde iyi sonuç veriyor";
            MessageBox.Show(metin);
        }

        static Bitmap CompassEdgeDetector(Bitmap inputBitmap, bool renkli)
        {
            // Convert the bitmap to grayscale
            Bitmap grayscaleBitmap = ToGrayscale(inputBitmap);

            // Calculate gradients with Sobel
            Bitmap gradientX = Sobel(grayscaleBitmap, true);
            Bitmap gradientY = Sobel(grayscaleBitmap, false);

            // Calculate edges using a simple compass operator
            Bitmap edgesBitmap;
            if (renkli)
                edgesBitmap = CompassOperator(gradientX, gradientY);
            else
                edgesBitmap = RenksizCompassOperator(gradientX, gradientY);

            return edgesBitmap;
        }

        static Bitmap ToGrayscale(Bitmap inputBitmap)
        {
            Bitmap grayscaleBitmap = new Bitmap(inputBitmap.Width, inputBitmap.Height);

            for (int x = 0; x < inputBitmap.Width; x++)
            {
                for (int y = 0; y < inputBitmap.Height; y++)
                {
                    Color pixelColor = inputBitmap.GetPixel(x, y);
                    int grayscaleValue = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11);
                    grayscaleBitmap.SetPixel(x, y, Color.FromArgb(grayscaleValue, grayscaleValue, grayscaleValue));
                }
            }

            return grayscaleBitmap;
        }

        static Bitmap Sobel(Bitmap inputBitmap, bool calculateX)
        {
            int[,] sobelOperatorX = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] sobelOperatorY = { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };

            int[,] operatorToUse = calculateX ? sobelOperatorX : sobelOperatorY;

            int width = inputBitmap.Width;
            int height = inputBitmap.Height;
            Bitmap resultBitmap = new Bitmap(width, height);

            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    int sum = 0;
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            Color pixelColor = inputBitmap.GetPixel(x + i, y + j);
                            int grayscaleValue = pixelColor.R; // Assuming grayscale image
                            sum += grayscaleValue * operatorToUse[i + 1, j + 1];
                        }
                    }

                    int newValue = Math.Min(255, Math.Max(0, sum));
                    resultBitmap.SetPixel(x, y, Color.FromArgb(newValue, newValue, newValue));
                }
            }

            return resultBitmap;
        }

        static Bitmap RenksizCompassOperator(Bitmap gradientX, Bitmap gradientY)
        {
            int width = gradientX.Width;
            int height = gradientX.Height;
            Bitmap resultBitmap = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixelColorX = gradientX.GetPixel(x, y);
                    Color pixelColorY = gradientY.GetPixel(x, y);

                    int valueX = pixelColorX.R;
                    int valueY = pixelColorY.R;

                    int newValue = Math.Min(255, Math.Max(0, Math.Abs(valueX) + Math.Abs(valueY)));
                    resultBitmap.SetPixel(x, y, Color.FromArgb(newValue, newValue, newValue));
                }
            }

            return resultBitmap;
        }
        static Bitmap CompassOperator(Bitmap gradientX, Bitmap gradientY)
        {
            int width = gradientX.Width;
            int height = gradientX.Height;
            Bitmap resultBitmap = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixelColorX = gradientX.GetPixel(x, y);
                    Color pixelColorY = gradientY.GetPixel(x, y);

                    int valueX = pixelColorX.R;
                    int valueY = pixelColorY.R;

                    // Calculate edge magnitude
                    int magnitude = (int)Math.Sqrt(valueX * valueX + valueY * valueY);

                    // Apply threshold to determine edges
                    int threshold = 50;
                    int edgeValue = magnitude > threshold ? magnitude : 0;

                    // Calculate edge direction angle (in radians)
                    double angle = Math.Atan2(valueY, valueX);

                    // Map the angle to a color
                    Color color = AngleToColor(angle);

                    // Apply color to edge pixels
                    if (edgeValue > 0)
                        resultBitmap.SetPixel(x, y, color);
                    else
                        resultBitmap.SetPixel(x, y, Color.Black);
                }
            }

            return resultBitmap;
        }

        static Color AngleToColor(double angle)
        {
            // Convert angle to hue value (0 to 360 degrees)
            double hue = (angle + Math.PI) * 180.0 / Math.PI;

            // Map hue to RGB color
            Color color = new HSLColor(hue, 1.0, 0.5).ToRGB();

            return color;
        }

        // Helper class for HSL to RGB color conversion
        public class HSLColor
        {
            public double Hue { get; set; }
            public double Saturation { get; set; }
            public double Lightness { get; set; }

            public HSLColor(double hue, double saturation, double lightness)
            {
                Hue = hue;
                Saturation = saturation;
                Lightness = lightness;
            }

            public Color ToRGB()
            {
                double c = (1 - Math.Abs(2 * Lightness - 1)) * Saturation;
                double x = c * (1 - Math.Abs((Hue / 60) % 2 - 1));
                double m = Lightness - c / 2;

                double r, g, b;
                if (Hue < 60)
                {
                    r = c;
                    g = x;
                    b = 0;
                }
                else if (Hue < 120)
                {
                    r = x;
                    g = c;
                    b = 0;
                }
                else if (Hue < 180)
                {
                    r = 0;
                    g = c;
                    b = x;
                }
                else if (Hue < 240)
                {
                    r = 0;
                    g = x;
                    b = c;
                }
                else if (Hue < 300)
                {
                    r = x;
                    g = 0;
                    b = c;
                }
                else
                {
                    r = c;
                    g = 0;
                    b = x;
                }

                byte red = (byte)((r + m) * 255);
                byte green = (byte)((g + m) * 255);
                byte blue = (byte)((b + m) * 255);

                return Color.FromArgb(red, green, blue);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int[,] ilkMatris = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = Compass(resim, ilkMatris, true);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.line;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int[,] ilkMatris = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = Compass(resim, ilkMatris, false);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = Sobel(resim);

            Bitmap image = new Bitmap(resim); // Resim dosyanızın yolunu buraya ekleyin

        }

        private Bitmap RobertCross(Bitmap GirisResmi)
        {
            Bitmap CikisResmi;
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int x, y;
            Color Renk;
            int P1, P2, P3, P4;
            for (x = 0; x < ResimGenisligi - 1; x++) //Resmi taramaya şablonun yarısı kadar dış kenarlardan içeride başlayacak ve bitirecek.
            {
                for (y = 0; y < ResimYuksekligi - 1; y++)
                {
                    Renk = GirisResmi.GetPixel(x, y);
                    P1 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x + 1, y);
                    P2 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x, y + 1);
                    P3 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x + 1, y + 1);
                    P4 = (Renk.R + Renk.G + Renk.B) / 3;
                    int Gx = Math.Abs(P1 - P4); //45 derece açı ile duran çizgileri bulur.
                    int Gy = Math.Abs(P2 - P3); //135 derece açı ile duran çizgileri bulur.
                    int RobertCrossDegeri = 0;
                    RobertCrossDegeri = Gx + Gy; //1. Formül
                                                 //RobertCrossDegeri = Convert.ToInt16(Math.Sqrt(Gx * Gx + Gy * Gy)); //2.Formül
                                                 //Renkler sınırların dışına çıktıysa, sınır değer alınacak.
                    if (RobertCrossDegeri > 255) RobertCrossDegeri = 255; //Mutlak değer kullanıldığı için negatif değerler oluşmaz.
                                                                          //Eşikleme
                                                                          //if (RobertCrossDegeri > 50)
                                                                          // RobertCrossDegeri = 255;
                                                                          //else
                                                                          // RobertCrossDegeri = 0;
                    CikisResmi.SetPixel(x, y, Color.FromArgb(RobertCrossDegeri, RobertCrossDegeri, RobertCrossDegeri));
                }
            }
            return CikisResmi;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = RobertCross(resim);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string metin = "Canny algoritması insan fotoğraflarında güzel sonuçlar veriyor." +
                " Bulanıklaştırma algoritması aralında en kötüsü bence. Genellikle kötü sonuçlar veriyor. Aşındırma genel olarak gayet iyi." +
                " Sobel ve Prewit özellikle manzara resimlerinde iyi sonuç veriyor." +
                " Robert Cross ise ızgara tarzı resimlerde çok iyi sonuç veriyor.";
            MessageBox.Show(metin);
        }

        private int[,] MatrisDondur(int[,] matrix)
        {
            int[,] gecici = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            gecici[0, 0] = matrix[0, 1];
            gecici[0, 1] = matrix[0, 2];
            gecici[0, 2] = matrix[1, 2];
            gecici[1, 0] = matrix[0, 0];
            gecici[1, 1] = matrix[1, 1];
            gecici[1, 2] = matrix[2, 2];
            gecici[2, 0] = matrix[1, 0];
            gecici[2, 1] = matrix[2, 0];
            gecici[2, 2] = matrix[2, 1];
            return gecici;
        }

        private void MultiplyMatrixByScalar(int[,] matrix, int scalar)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] *= scalar;
                }
            }
        }

        private Bitmap Compass(Bitmap GirisResmi, int[,] ilkMatris, bool renkli)
        {
            Color OkunanRenk;
            Bitmap CikisResmiX, CikisResmiY, CikisResmiXY;
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmiX = new Bitmap(ResimGenisligi, ResimYuksekligi);
            CikisResmiY = new Bitmap(ResimGenisligi, ResimYuksekligi);
            CikisResmiXY = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = 3;
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int x, y, i, j; int Gri = 0;



            List<int[,]> Matrisler = new List<int[,]>();
            Matrisler.Add(ilkMatris);
            for (int k = 1; k < 8; k++)
            {
                ilkMatris = MatrisDondur(ilkMatris);
                Matrisler.Add(ilkMatris);
            }

            int[] Renkler = new int[8];
            int sonRenk = 0;
            int r = 0, g = 0, b = 0;

            int aci = 0;

            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    r = 0; g = 0; b = 0;
                    int toplamGri0 = 0, toplamGri1 = 0, toplamGri2 = 0, toplamGri3 = 0, toplamGri4 = 0, toplamGri5 = 0, toplamGri6 = 0, toplamGri7 = 0;
                    //Şablon bölgesi (çekirdek matris) içindeki pikselleri tarıyor.
                    int k = 0;
                    int l = 0;
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            Gri = (OkunanRenk.R + OkunanRenk.G + OkunanRenk.B) / 3;

                            toplamGri0 = toplamGri0 + Gri * Matrisler[0][k, l];
                            toplamGri1 = toplamGri1 + Gri * Matrisler[1][k, l];
                            toplamGri2 = toplamGri2 + Gri * Matrisler[2][k, l];
                            toplamGri3 = toplamGri3 + Gri * Matrisler[3][k, l];
                            toplamGri4 = toplamGri4 + Gri * Matrisler[4][k, l];
                            toplamGri5 = toplamGri5 + Gri * Matrisler[5][k, l];
                            toplamGri6 = toplamGri6 + Gri * Matrisler[6][k, l];
                            toplamGri7 = toplamGri7 + Gri * Matrisler[7][k, l];


                            //toplamGriX = toplamGriX + Gri * MatrisX[k];
                            //toplamGriY = toplamGriY + Gri * MatrisY[k];

                            if (l == 2)
                            {
                                l = 0;
                                k++;
                            }
                            else l++;
                        }
                    }

                    int d = 50;
                    int esik = 50;

                    if (Math.Abs(toplamGri0) >= esik) { r += d; };
                    if (Math.Abs(toplamGri1) >= esik) { g += d; };
                    if (Math.Abs(toplamGri2) >= esik) { b += d; };
                    if (Math.Abs(toplamGri3) >= esik) { r += d / 2; g += d; }
                    if (Math.Abs(toplamGri4) >= esik) { r += d / 2; b += d; }
                    if (Math.Abs(toplamGri5) >= esik) { g += d; r += d / 2; }
                    if (Math.Abs(toplamGri6) >= esik) { r += d; g += d; b += d; }
                    if (Math.Abs(toplamGri7) >= esik) { r += d; g += d / 2; b += d; }

                    //if (Math.Abs(toplamGri0) >= esik) { r = d; g = 0; b = 0;}
                    //else if (Math.Abs(toplamGri1) >= esik) { r = d; g = d; b = 0; }
                    //else if (Math.Abs(toplamGri2) >= esik) { r = d; g = d; b = d; }
                    //else if (Math.Abs(toplamGri3) >= esik) { r = 0; g = d; b = d; }
                    //else if (Math.Abs(toplamGri4) >= esik) { r = 0; g = d; b = 0; }
                    //else if (Math.Abs(toplamGri5) >= esik) { r = 0; g = 0; b = d; }
                    //else if (Math.Abs(toplamGri6) >= esik) { r = d; g = 0; b = d; }
                    //else if (Math.Abs(toplamGri7) >= esik) { r += 0; g += d * 2; b += 0; }


                    //===========================================================
                    //Renkler sınırların dışına çıktıysa, sınır değer alınacak.
                    sonRenk = Math.Abs(toplamGri0) + Math.Abs(toplamGri1) + Math.Abs(toplamGri2) + Math.Abs(toplamGri3) + Math.Abs(toplamGri4) +
                            Math.Abs(toplamGri5) + Math.Abs(toplamGri6) + Math.Abs(toplamGri7);

                    if (sonRenk > 255) sonRenk = 255;
                    else if (sonRenk < 255) sonRenk = 0;

                    if (r > 255) r = 255;
                    else if (r < 0) r = 0;
                    if (g > 255) g = 255;
                    else if (g < 0) g = 0;
                    if (b > 255) b = 255;
                    else if (b < 0) b = 0;

                    //sonRenk = (int)ScaleTo255(sonRenk);

                    //===========================================================

                    if (renkli) CikisResmiXY.SetPixel(x, y, Color.FromArgb(r, g, b));
                    else CikisResmiXY.SetPixel(x, y, Color.FromArgb(sonRenk, sonRenk, sonRenk));
                }
            }
            return CikisResmiXY;

        }

        static double ScaleTo255(double number)
        {
            // Sayıyı 0 ile 255 arasına ölçekle
            return Math.Max(0, Math.Min(255, number * 255.0 / 100.0));
        }

        private void button17_Click(object sender, EventArgs e)
        {
            int[,] ilkMatris = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = Compass(resim, ilkMatris, false);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            int[,] ilkMatris = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = Compass(resim, ilkMatris, true);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            int[,] ilkMatris = { { -3, -3, 5 }, { -3, 0, 5 }, { -3, -3, 5 } };
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = Compass(resim, ilkMatris, true);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            int[,] ilkMatris = { { -1, 0, 1 }, { -1, 0, 1 }, { -1, 0, 1 } };
            Bitmap resim = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = Compass(resim, ilkMatris, true);
        }

        private double FindEdgeAngle(Bitmap image)
        {
            double result = 0.0;
            int sayac = 0;
            // Sobel operatörü ile kenarları bul
            int[,] sobelX = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] sobelY = { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    int gx = Convolution(image, sobelX, x, y);
                    int gy = Convolution(image, sobelY, x, y);

                    result += Math.Atan2(gy, gx) * (180.0 / Math.PI);
                    sayac++;
                }
            }

            return result / sayac;

            // Kenarın açısını hesapla
        }

        private int Convolution(Bitmap image, int[,] kernel, int x, int y)
        {
            int result = 0;
            int kernelSize = kernel.GetLength(0);

            for (int i = 0; i < kernelSize; i++)
            {
                for (int j = 0; j < kernelSize; j++)
                {
                    int pixelX = x + i - kernelSize / 2;
                    int pixelY = y + j - kernelSize / 2;

                    if (pixelX >= 0 && pixelX < image.Width && pixelY >= 0 && pixelY < image.Height)
                    {
                        Color pixelColor = image.GetPixel(pixelX, pixelY);
                        int grayValue = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11);
                        result += grayValue * kernel[i, j];
                    }
                }
            }

            return result;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.DefaultExt = ".jpg";
            file.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            file.ShowDialog();
            pictureBox1.ImageLocation = file.FileName;
        }



        private void button22_Click(object sender, EventArgs e)
        {
            Bitmap newBitmap = (Bitmap)pictureBox1.Image;
            Bitmap newBitmap1 = new Bitmap(newBitmap.Width, newBitmap.Height);
            newBitmap1 = MakeGrayscale(newBitmap);
            newBitmap1 = MakeSmooth(newBitmap1);
            newBitmap1 = DetectEdge(newBitmap1);
            pictureBox2.Image = newBitmap1;

        }
    }
}
