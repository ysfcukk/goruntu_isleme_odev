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
    public partial class hafta8 : Form
    {
        public hafta8()
        {
            InitializeComponent();
        }

        private (Bitmap, int) BolgeBul(Bitmap GirisResmi, int esik)
        {
            Bitmap CikisResmi;
            int KomsularinEnKucukEtiketDegeri = 0;
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int PikselSayisi = ResimGenisligi * ResimYuksekligi;
            Bitmap SiyahBeyazResim = ResmiGriTonaDonusturEsiklemeYap(GirisResmi, esik);
            GirisResmi = SiyahBeyazResim;
            pictureBox2.Image = SiyahBeyazResim;
            int x, y, i, j, EtiketNo = 0;
            int[,] EtiketNumarasi = new int[ResimGenisligi, ResimYuksekligi];

            for (x = 0; x < ResimGenisligi; x++)
            {
                for (y = 0; y < ResimYuksekligi; y++)
                {
                    EtiketNumarasi[x, y] = 0;
                }
            }
            int IlkDeger = 0, SonDeger = 0;
            bool DegisimVar = false; //Etiket numaralarında değişim olmayana kadar dönmesi için sonsuz döngüyü kontrol edecek.
            int Esikleme = esik;

            do //etiket numaralarında değişim kalmayana kadar dönecek.
            {
                DegisimVar = false;
                // Resmi tarıyor
                for (y = 1; y < ResimYuksekligi - 1; y++) //Resmin 1 piksel içerisinden başlayıp, bitirecek. Çünkü çekirdek şablon en dış kenardan başlamalı.
                {
                    for (x = 1; x < ResimGenisligi - 1; x++)
                    {
                        if (GirisResmi.GetPixel(x, y).R > Esikleme)
                        {
                            IlkDeger = EtiketNumarasi[x, y];
                            //Komşular arasında en küçük etiket numarasını bulacak.
                            KomsularinEnKucukEtiketDegeri = 0;
                            for (j = -1; j <= 1; j++) //Çekirdek şablon 3x3 lük bir matris. Dolayısı ile x,y nin -1 den başlayıp +1 ne kadar yer kaplar.
                            {
                                for (i = -1; i <= 1; i++)
                                {
                                    if (EtiketNumarasi[x + i, y + j] != 0 && KomsularinEnKucukEtiketDegeri == 0)
                                    {
                                        KomsularinEnKucukEtiketDegeri = EtiketNumarasi[x + i, y + j];
                                    }
                                    else if (EtiketNumarasi[x + i, y + j] < KomsularinEnKucukEtiketDegeri && EtiketNumarasi[x + i, y + j] != 0 && KomsularinEnKucukEtiketDegeri != 0)
                                    {
                                        KomsularinEnKucukEtiketDegeri = EtiketNumarasi[x + i, y + j];
                                    }
                                }
                            }

                            if (KomsularinEnKucukEtiketDegeri != 0)
                            {
                                EtiketNumarasi[x, y] = KomsularinEnKucukEtiketDegeri;
                            }
                            else if (KomsularinEnKucukEtiketDegeri == 0)
                            {
                                EtiketNo = EtiketNo + 1; EtiketNumarasi[x, y] = EtiketNo;
                            }
                            SonDeger = EtiketNumarasi[x, y];

                            if (IlkDeger != SonDeger) DegisimVar = true;

                        }
                    }
                }
            } while (DegisimVar == true);

            int[] DiziEtiket = new int[PikselSayisi];
            i = 0;
            for (x = 1; x < ResimGenisligi - 1; x++)
            {
                for (y = 1; y < ResimYuksekligi - 1; y++)
                {
                    i++;
                    DiziEtiket[i] = EtiketNumarasi[x, y];
                }
            }
            //Dizideki etiket numaralarını sıralıyor. Hazır fonksiyon kullanıyor.
            Array.Sort(DiziEtiket);
            //Tekrar eden etiket numaralarını çıkarıyor. Hazır fonksiyon kullanıyor. Tekil numaraları diziye atıyor.
            int[] TekrarsizEtiketNumaralari = DiziEtiket.Distinct().ToArray();
            //DİKKAT BURADA RenkDizisi ihtiyaç değil gibi. Renk adedi direk Tekrarsız numaralardan alınabilir.
            int[] RenkDizisi = new int[TekrarsizEtiketNumaralari.Length]; //Tekil numaralar aynı boyutta renk dizisini oluşturuyor.

            for (j = 0; j < TekrarsizEtiketNumaralari.Length; j++)
            {
                RenkDizisi[j] = TekrarsizEtiketNumaralari[j]; //sıradaki ilk renge, ait olacağı etiketin kaç numara olacağını atıyor.
            }
            int RenkSayisi = RenkDizisi.Length; //kaç tane numara varsa o kadar renk var demektir.
            Color[] Renkler = new Color[RenkSayisi]; Random Rastgele = new Random();
            int Kirmizi, Yesil, Mavi;
            for (int r = 0; r < RenkSayisi; r++) //sonraki renkler.
            {
                Kirmizi = Rastgele.Next(15, 25) * 10; //Açık renkler elde etmek ve 10 katları şeklinde olmasını sağlıyor. yani 150-250 arasındaki sayıları atıyor.
                Yesil = Rastgele.Next(15, 25) * 10;
                Mavi = Rastgele.Next(15, 25) * 10;
                Renkler[r] = Color.FromArgb(Kirmizi, Yesil, Mavi); //Renkler dizisi Color tipinde renkleri tutan bir dizidir.
            }

            for (x = 1; x < ResimGenisligi - 1; x++)
            {
                for (y = 1; y < ResimYuksekligi - 1; y++)
                {
                    int RenkSiraNo = Array.IndexOf(RenkDizisi, EtiketNumarasi[x, y]);
                    if (GirisResmi.GetPixel(x, y).R < Esikleme) //Eğer bu pikselin rengi siyah ise aynı pikselin CikisResmi resmide siyah yapılacak.
                    {
                        CikisResmi.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        CikisResmi.SetPixel(x, y, Renkler[RenkSiraNo]);
                    }
                }
            }
            return (CikisResmi, RenkSayisi);

        }

        private Bitmap ResmiGriTonaDonusturEsiklemeYap(Bitmap GirisResmi, int esik)
        {
            Color OkunanRenk, DonusenRenk;
            Bitmap CikisResmi;
            int ResimGenisligi = GirisResmi.Width; //GirisResmi global tanımlandı. İçerisine görüntü yüklendi.
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi); //Cikis resmini oluşturuyor. Boyutları giriş resmi ile aynı olur. Tanımlaması globalde yapıldı.
            int i = 0, j = 0; //Çıkış resminin x ve y si olacak.
            int R = 0, G = 0, B = 0;
            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);
                    R = OkunanRenk.R; G = OkunanRenk.G; B = OkunanRenk.B;
                    int Gri = Convert.ToInt16(R * 0.3 + G * 0.6 + B * 0.1);
                    int Esikleme = esik;
                    if (Gri > Esikleme) Gri = 255;
                    else
                        Gri = 0;
                    DonusenRenk = Color.FromArgb(Gri, Gri, Gri); CikisResmi.SetPixel(x, y, DonusenRenk);
                }
            }
            return CikisResmi;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);
            Bitmap yeniResim;
            int sayi;

            int esik = Convert.ToInt16(textBox2.Text);

            (yeniResim, sayi) = BolgeBul(resim, esik);

            pictureBox2.Image = yeniResim;
            textBox1.Text = sayi.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.DefaultExt = ".jpg";
            file.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            file.ShowDialog();
            pictureBox1.ImageLocation = file.FileName;
        }

        private Bitmap GenisletmeSinir(Bitmap GirisResmi, int esik)
        {
            Color OkunanRenk;
            Bitmap CikisResmi;
            Bitmap OrjinalResim, GenislemisResim;
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            Bitmap SiyahBeyazResim = ResmiGriTonaDonusturEsiklemeYap(GirisResmi, esik);
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

        private Bitmap SIYAH_BOLGEYI_GENISLET(Bitmap SiyahBeyazResim, int esik)
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            int ResimGenisligi = SiyahBeyazResim.Width;
            int ResimYuksekligi = SiyahBeyazResim.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            GirisResmi = SiyahBeyazResim; int x, y, i, j;
            int SablonBoyutu = 3;
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int Esikleme = esik;

            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);
                    if (OkunanRenk.R > Esikleme)
                    {
                        bool KomsulardaSiyahVarMi = false;
                        for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                        {
                            for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                            {
                                OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                                if (OkunanRenk.R < Esikleme) //Komşularda Siyah Var ise
                                    KomsulardaSiyahVarMi = true;
                            }
                        }
                        //Komşularda Beyaz var ise, Kendi rengimizi Beyaz yapalım.
                        if (KomsulardaSiyahVarMi == true) //Komşularda Beyaz varsa
                        {
                            Color KendiRengi = GirisResmi.GetPixel(x, y);
                            CikisResmi.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                        }
                        else //komşularda siyah yok ise kendi rengin yine beyaz kalmalı.
                            CikisResmi.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    }
                }
            }
            return CikisResmi;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);
            int esik = Convert.ToInt16(textBox2.Text);
            pictureBox2.Image = GenisletmeSinir(resim, esik);
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);
            int esik = Convert.ToInt16(textBox2.Text);
            pictureBox2.Image = SIYAH_BOLGEYI_GENISLET(resim, esik);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null) pictureBox1.Image = pictureBox2.Image;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap resim = new Bitmap(pictureBox1.Image);
            int esik = 250;
            Bitmap genislemisResim = GenisletmeSinir(resim, esik);
            esik = 10;
            int k;
            Bitmap bolgeResim;
            (bolgeResim, k) = BolgeBul(genislemisResim, esik);

            int findik, ceviz;
            (findik, ceviz) = FindikCevizSayisi(bolgeResim);

            textBox3.Text = findik.ToString();
            textBox4.Text = ceviz.ToString();

            pictureBox2.Image = bolgeResim;
        }

        private (int, int) FindikCevizSayisi(Bitmap resim)
        {
            int w = resim.Width;
            int h = resim.Height;

            Dictionary<Color, int> renkSayac = new Dictionary<Color, int>();

            Color okunanRenk;

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    okunanRenk = resim.GetPixel(x, y);

                    if (!(okunanRenk.R < 20 && okunanRenk.G < 20 && okunanRenk.B < 20))
                    {
                        if (renkSayac.ContainsKey(okunanRenk))
                        {
                            renkSayac[okunanRenk]++;
                        }
                        else
                        {
                            renkSayac.Add(okunanRenk, 1);
                        }
                    }
                }
            }

            if (StandartSapma(renkSayac.Values) < 15)
            {
                MessageBox.Show("Nesne Boyutları birbirine çok yakın. Hepsi ceviz veya hepsi fındık olabilir.");
                return (0, 0);
            }
            else
            {
                int findikSayisi = 0;
                int cevizSayisi = 0;

                double ort = renkSayac.Values.Average();

                foreach (KeyValuePair<Color, int> kvp in renkSayac)
                {
                    if (kvp.Value > ort) cevizSayisi++;
                    else findikSayisi++;
                }

                return (findikSayisi, cevizSayisi);
            }
        }

        private double StandartSapma(IEnumerable<int> values)
        {
            try
            {
                double ortalama = values.Average();
                double toplamKareFark = values.Sum(value => Math.Pow(value - ortalama, 2));
                double varyans = toplamKareFark / values.Count();
                double standartSapma = Math.Sqrt(varyans);
                return standartSapma;
            }
            catch
            {
                MessageBox.Show("Tespit edilemedi!");
                return 0;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.findik_ceviz;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources._5findik_4ceviz;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.pose2;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Bitmap insan = Properties.Resources.pose2;
            Bitmap manzara = Properties.Resources.view2;

            pictureBox2.Image = ArkaPlanDegistir(insan, manzara);

        }

        private Bitmap ArkaPlanDegistir(Bitmap insan, Bitmap manzara)
        {
            int esik = 156;
            int k;
            Bitmap bolgeResim;
            (bolgeResim, k) = BolgeBul(insan, esik);
            Bitmap genisletilmis = SIYAH_BOLGEYI_GENISLET(bolgeResim, 128);

            Bitmap resim = new Bitmap(genisletilmis);

            int w = resim.Width;
            int h = resim.Height;

            Dictionary<Color, int> renkSayac = new Dictionary<Color, int>();

            Color okunanRenk;

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    okunanRenk = resim.GetPixel(x, y);

                    if (!(okunanRenk.R < 20 && okunanRenk.G < 20 && okunanRenk.B < 20))
                    {
                        if (renkSayac.ContainsKey(okunanRenk))
                        {
                            renkSayac[okunanRenk]++;
                        }
                        else
                        {
                            renkSayac.Add(okunanRenk, 1);
                        }
                    }
                }
            }

            Bitmap cikisResmi = new Bitmap(manzara.Width,manzara.Height);
            Color sinirRenk = renkSayac.OrderByDescending(kv => kv.Value).First().Key; //en çok geçen renk (yani sınırlar.)

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    okunanRenk = resim.GetPixel(x, y);

                    if (okunanRenk == Color.FromArgb(255,255,255))
                    {
                        cikisResmi.SetPixel(x, y, manzara.GetPixel(x, y));
                    }
                    else
                    {
                        cikisResmi.SetPixel(x, y, insan.GetPixel(x, y));
                    }
                }
            }
            return cikisResmi;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.view2;
        }
    }
}
