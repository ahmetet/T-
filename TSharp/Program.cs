using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Ahmetet;
using System.Drawing;

namespace TSharp
{
    class Program
    {
        public static List<String> Kimlikler = new List<string>();
        public static List<String> Objeler = new List<string>();

        public static List<Form> Formlar = new List<Form>();
        public static List<int> Formlar_index = new List<int>();

        public static List<Label> Etiketler = new List<Label>();
        public static List<int> Etiketler_index = new List<int>();

        public static List<Button> Butonlar = new List<Button>();
        public static List<int> Butonlar_index = new List<int>();

        public static List<TextBox> Metinkutuları = new List<TextBox>();
        public static List<int> Metinkutuları_index = new List<int>();

        public static List<ProgressBar> Islemcubukları = new List<ProgressBar>();
        public static List<int> Islemcubukları_index = new List<int>();

        public static List<CheckBox> OnayIsaretkutuları = new List<CheckBox>();
        public static List<int> OnayIsaretKutuları_index = new List<int>();

        public static List<String> Objelerin_Mesajları = new List<string>();

        public static int idsi;
        public static string yedek_veri;
        public static string Obje_Adı;
        public static string m_baslik = "";
        public static string m_mesaj = "";
        static void Main(string[] args)
        {

            try
            {
                if (args[0].Length > 0)
                {

                    string yol = args[0];
                    string[] parcalar = yol.Split('\\');
                    string uzantı = parcalar[parcalar.Length - 1].Split('.')[1];

                    if (uzantı == "tr")
                    {

                        StreamReader okuyucu = new StreamReader(yol, Encoding.UTF8, false);
                        string veri = okuyucu.ReadToEnd();
                        yedek_veri = veri.Trim();
                        veri = veri.Trim().ToLower();
                        veri = veri.Replace(" ", String.Empty);

                        int neredesin = 0;
                        int isaret1 = 0;
                        int isaret2 = 0;
                        int lokasyon1 = 0;
                        int lokasyon2 = 0;

                        // BÖLÜM 1                              : ANA SINIFLARI BUL
                        int sinifid = 0;
                        foreach (char karakter in veri)
                        {

                            neredesin += 1;

                            if (karakter == '#')
                            {
                                isaret1 = neredesin;
                            }
                            if (karakter == '{')
                            {
                                isaret2 = neredesin;
                                lokasyon1 = neredesin;
                            }
                            if (karakter == '}')
                            {
                                lokasyon2 = neredesin;
                                string Kimlik = veri.Substring(isaret1, isaret2 - isaret1 - 1).Trim().ToString();
                                sinifid += 1;
                                Objeler.Add(Kimlik + ":" + lokasyon1.ToString() + "," + lokasyon2.ToString() + "," + sinifid.ToString());
                                isaret1 = 0;
                                isaret2 = 0;
                                lokasyon1 = 0;
                                lokasyon2 = 0;
                            }

                        }

                        Objeler = Objeler.Distinct().ToList();


                        // BÖLÜM                                : SINIFLARIN İÇİNE TEKER TEKER GİR VE İÇLERİNİ AYIKLA
                        /*
                        int kac_kere = Objeler.Count;
                        int tempsay = 0;
                        while (tempsay < kac_kere)
                        {
                            Console.WriteLine(Objeler[tempsay].ToString());
                            tempsay += 1;
                        }*/


                        foreach (string objecik in Objeler)
                        {
                            // Pencere:5,15,1 (Örnek)

                            try
                            {

                                Obje_Adı = objecik.Split(':')[0].Trim().ToString();
                                idsi = Convert.ToInt32(objecik.Split(':')[1].Trim().ToString().Split(',')[2]);

                                if (Obje_Adı == "pencere")
                                {
                                    Form Pencere = new Form();
                                    Pencere.Width = 700;
                                    Pencere.Height = 300;
                                    Formlar.Add(Pencere);
                                    Formlar_index.Add(idsi);
                                    //   MessageBox.Show(idsi.ToString()+":"+objecik.ToString());
                                }
                                if (Obje_Adı == "etiket")
                                {
                                    Label Etiket = new Label();
                                    Etiket.Location = new Point(15, 15);
                                    Etiketler.Add(Etiket);
                                    Etiketler_index.Add(idsi);
                                }
                                if (Obje_Adı == "buton")
                                {
                                    Button Buton = new Button();
                                    Buton.Size = new Size(100, 50);
                                    Buton.Location = new Point(15, 15);
                                    Butonlar.Add(Buton);
                                    Butonlar_index.Add(idsi);
                                }
                                if (Obje_Adı == "metinkutusu")
                                {
                                    TextBox Metinkutusu = new TextBox();
                                    Metinkutusu.Location = new Point(15, 15);
                                    Metinkutusu.Size = new Size(100, 50);
                                    Metinkutuları.Add(Metinkutusu);
                                    Metinkutuları_index.Add(idsi);
                                }
                                if (Obje_Adı == "işlemçubuğu")
                                {
                                    ProgressBar Islemcubugu = new ProgressBar();
                                    Islemcubugu.Location = new Point(15, 15);
                                    Islemcubugu.Size = new Size(200, 20);
                                    Islemcubugu.Maximum = 100;
                                    Islemcubugu.Value = 0;
                                    Islemcubukları.Add(Islemcubugu);
                                    Islemcubukları_index.Add(idsi);

                                }
                                if (Obje_Adı == "onaykutusu" || Obje_Adı == "işaretkutusu")
                                {
                                    CheckBox Onayİşaretkutusu = new CheckBox();
                                    Onayİşaretkutusu.Location = new Point(15, 15);
                                    Onayİşaretkutusu.AutoSize = true;
                                    Onayİşaretkutusu.Text = "";
                                    OnayIsaretkutuları.Add(Onayİşaretkutusu);
                                    OnayIsaretKutuları_index.Add(idsi);
                                }


                            }
                            catch
                            {

                            }

                            int süslü_ac = Convert.ToInt32(objecik.Split(':')[1].Trim().Split(',')[0].Trim());
                            int süslü_kapat = Convert.ToInt32(objecik.Split(':')[1].Trim().Split(',')[1].Trim());
                            // Console.WriteLine("{0}  -->   {1}  ,  {2}   ;", Obje_Adı.ToString(), süslü_ac.ToString(), süslü_kapat.ToString());/
                            // Console.WriteLine(veri.Substring(süslü_ac,süslü_kapat-süslü_ac-1));
                            string Obje_İçerik = veri.Substring(süslü_ac, süslü_kapat - süslü_ac - 1).ToString().Trim();
                            // Console.WriteLine(Obje_İçerik);
                            foreach (string satırcık in Obje_İçerik.Split(';'))
                            {
                                try
                                {
                                    string proto = satırcık;
                                    proto = proto.Replace(" ", String.Empty);
                                    if (!(String.IsNullOrEmpty(proto) || String.IsNullOrWhiteSpace(proto)))
                                    {
                                        //PROTO şu anda her bir satırın ham verisi.
                                        // Mesela;
                                        // ad="ahmet"
                                        // Bundan sonra ad,ikon vs özellik tokenlerini arayacaksın.
                                        // AMA BUNDAN ÖNCE PARS YAPACAKSIN ÖRNEĞİN;
                                        // ad = "ahmet" gibi peki bunu nasıl yapacağız?
                                        //   



                                        string[] Parcaciklar = Regex.Split(proto, "->|=");//regexe gerek yok sanırım ama neyse çaktırma..
                                        string parcacik_1 = Parcaciklar[0].Trim();
                                        string parcacik_2 = Parcaciklar[1].Trim();

                                        int kacinci_form = 0;
                                        Form ilgilenilen = null;

                                        int kacinci_etiket = 0;
                                        Label ilgilenilen_etiket = null;

                                        int kacinci_buton = 0;
                                        Button ilgilenilen_buton = null;

                                        int kacinci_metinkutusu = 0;
                                        TextBox ilgilenilen_metinkutusu = null;

                                        int kacinci_islemcubugu = 0;
                                        ProgressBar ilgilenilen_islemcubugu = null;

                                        int kacinci_onayişaretkutusu = 0;
                                        CheckBox ilgilenilen_onayişaretkutusu = null;

                                        Control evrensel = null;

                                        if (Obje_Adı == "pencere")
                                        {
                                            kacinci_form = Formlar_index.IndexOf(idsi); // atıyorum 5
                                            ilgilenilen = Formlar[kacinci_form]; //
                                            evrensel = ilgilenilen;
                                        }
                                        if (Obje_Adı == "etiket")
                                        {
                                            kacinci_etiket = Etiketler_index.IndexOf(idsi);
                                            ilgilenilen_etiket = Etiketler[kacinci_etiket];
                                            evrensel = ilgilenilen_etiket;
                                        }
                                        if (Obje_Adı == "buton")
                                        {
                                            kacinci_buton = Butonlar_index.IndexOf(idsi);
                                            ilgilenilen_buton = Butonlar[kacinci_buton];
                                            evrensel = ilgilenilen_buton;

                                        }
                                        if (Obje_Adı == "metinkutusu")
                                        {
                                            kacinci_metinkutusu = Metinkutuları_index.IndexOf(idsi);
                                            ilgilenilen_metinkutusu = Metinkutuları[kacinci_metinkutusu];
                                            evrensel = ilgilenilen_metinkutusu;
                                        }
                                        if (Obje_Adı == "işlemçubuğu")
                                        {

                                            kacinci_islemcubugu = Islemcubukları_index.IndexOf(idsi);
                                            ilgilenilen_islemcubugu = Islemcubukları[kacinci_islemcubugu];
                                            evrensel = ilgilenilen_islemcubugu;
                                        }
                                        if (Obje_Adı == "onaykutusu" || Obje_Adı == "işaretkutusu")
                                        {
                                            kacinci_onayişaretkutusu = OnayIsaretKutuları_index.IndexOf(idsi);
                                            ilgilenilen_onayişaretkutusu = OnayIsaretkutuları[kacinci_onayişaretkutusu];
                                            evrensel = ilgilenilen_onayişaretkutusu;
                                        }



                                        // SINIF ID DEN KAÇINCI İNDEX OLDUĞUNU BULUP İLGİLİ FORMU BUL YESS
                                        // ilgili form ;  Formlar[kacinci_form]

                                        string secilen_obje = Obje_Adı;

                                        // TEXT ÖZELLİĞİ ---------------------------------------------------------------------------------------------------------------->
                                        if (parcacik_1 == "@ad" || parcacik_1 == "@başlık" || parcacik_1 == "@yazı")
                                        {
                                            try
                                            {
                                                string adcik = Araçlar.StringBul(parcacik_2);
                                                evrensel.Text = adcik.Replace("_", " ");
                                            }
                                            catch
                                            {

                                            }
                                        }
                                        // TEXT ÖZELLİĞİ ---------------------------------------------------------------------------------------------------------------->

                                        // İKON GÖSTER GİZLE ÖZELLİĞİ  ---------------------------------------------------------------------------------------------------------------->
                                        if (parcacik_1 == "@simge")
                                        {
                                            try
                                            {
                                                bool İkonGosterGizle = parcacik_2 == "@aktif" ? true : false;
                                                if (evrensel is Form)
                                                {

                                                    ilgilenilen.ShowIcon = İkonGosterGizle;
                                                }

                                            }
                                            catch
                                            {

                                            }
                                        }
                                        // İKON GÖSTER GİZLE ÖZELLİĞİ  ---------------------------------------------------------------------------------------------------------------->

                                        // GENİŞLİĞİ  ---------------------------------------------------------------------------------------------------------------->
                                        if (parcacik_1 == "@genişlik" || parcacik_1 == "@en")
                                        {
                                            try
                                            {
                                                int genislik = Araçlar.SayıBul(parcacik_2);
                                                evrensel.Width = genislik;
                                            }
                                            catch
                                            {

                                            }
                                        }
                                        // GENİŞLİĞİ  ---------------------------------------------------------------------------------------------------------------->

                                        // YÜKSEKLİK  ---------------------------------------------------------------------------------------------------------------->
                                        if (parcacik_1 == "@yükseklik" || parcacik_1 == "@boy")
                                        {
                                            try
                                            {
                                                int yükseklik = Araçlar.SayıBul(parcacik_2);
                                                evrensel.Height = yükseklik;
                                            }
                                            catch
                                            {

                                            }
                                        }
                                        // YÜKSEKLİK  ---------------------------------------------------------------------------------------------------------------->

                                        // ARKAPLAN RENGİ  ---------------------------------------------------------------------------------------------------------------->
                                        if (parcacik_1 == "@arkaplan" || (parcacik_1.Contains("@arkaplan") && (parcacik_1.Contains("renk") || parcacik_1.Contains("reng"))))
                                        {
                                            // Arkaplan Rengi = (128,128,128) ya da 128,128,128
                                            try
                                            {
                                                parcacik_2 = parcacik_2.Replace("(", String.Empty).Replace(")", String.Empty);
                                                int R, G, B;
                                                R = Convert.ToInt32(parcacik_2.Split(',')[0]);
                                                G = Convert.ToInt32(parcacik_2.Split(',')[1]);
                                                B = Convert.ToInt32(parcacik_2.Split(',')[2]);

                                                evrensel.BackColor = Color.FromArgb(R, G, B);
                                            }
                                            catch
                                            {

                                            }
                                        }
                                        // ARKAPLAN RENGİ  ---------------------------------------------------------------------------------------------------------------->

                                        // LOKASYON   ---------------------------------------------------------------------------------------------------------------->
                                        if (parcacik_1 == "@konum" || parcacik_1 == "@lokasyon")
                                        {
                                            try
                                            {
                                                parcacik_2 = parcacik_2.Replace("(", String.Empty).Replace(")", String.Empty);
                                                int X, Y;
                                                X = Convert.ToInt32(parcacik_2.Split(',')[0]);
                                                Y = Convert.ToInt32(parcacik_2.Split(',')[1]);
                                                //   MessageBox.Show(X.ToString() + "," + Y.ToString());
                                                if (evrensel is Form)
                                                {
                                                    ilgilenilen.StartPosition = FormStartPosition.Manual;
                                                    ilgilenilen.Location = new Point(X, Y);
                                                }
                                                else
                                                {
                                                    evrensel.Location = new Point(X, Y);
                                                }

                                            }
                                            catch
                                            {

                                            }

                                        }
                                        // LOKASYON   ---------------------------------------------------------------------------------------------------------------->

                                        //YAZI RENGİ ÖZELLİĞİ ----------------------------------------------------------------------------------------------------------------------------------------->
                                        if (parcacik_1 == "@renk" || ((parcacik_1.Contains("@yazı") || parcacik_1 == "@metin") && (parcacik_1.Contains("renk") || parcacik_1.Contains("reng"))))
                                        {
                                            // Arkaplan Rengi = (128,128,128) ya da 128,128,128
                                            try
                                            {
                                                parcacik_2 = parcacik_2.Replace("(", String.Empty).Replace(")", String.Empty);
                                                int R, G, B;
                                                R = Convert.ToInt32(parcacik_2.Split(',')[0]);
                                                G = Convert.ToInt32(parcacik_2.Split(',')[1]);
                                                B = Convert.ToInt32(parcacik_2.Split(',')[2]);

                                                evrensel.ForeColor = Color.FromArgb(R, G, B);
                                            }
                                            catch
                                            {

                                            }
                                        }
                                        //YAZI RENGİ ÖZELLİĞİ ----------------------------------------------------------------------------------------------------------------------------------------->


                                        // YAZI BOYUTU ----------------------------------------------------------------------------------------------------------------------------------------->
                                        if (parcacik_1 == "@boyut" || (parcacik_1 == "@yazı" && (parcacik_1 == "boyut" || parcacik_1 == "büyüklük" || parcacik_1 == "büyüklüğ")))
                                        {
                                            try
                                            {

                                                int yazıboyut = Araçlar.SayıBul(parcacik_2);
                                                if (evrensel is Label)
                                                {
                                                    ilgilenilen_etiket.AutoSize = true;
                                                    ilgilenilen_etiket.Font = new Font(ilgilenilen_etiket.Font.FontFamily, yazıboyut);
                                                }
                                                else
                                                {
                                                    evrensel.Font = new Font(evrensel.Font.FontFamily, yazıboyut);
                                                }


                                            }
                                            catch
                                            {

                                            }
                                        }
                                        // YAZI BOYUTU ----------------------------------------------------------------------------------------------------------------------------------------->


                                        // VALUE ----------------------------------------------------------------------------------------------------------------------------------------->
                                        if (parcacik_1 == "@değer")
                                        {

                                            try
                                            {
                                                int val_deger = Araçlar.SayıBul(parcacik_2);


                                                if (evrensel is ProgressBar)
                                                {
                                                    ilgilenilen_islemcubugu.Value = val_deger;
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                        // VALUE ----------------------------------------------------------------------------------------------------------------------------------------->

                                        // VALUE MAX ----------------------------------------------------------------------------------------------------------------------------------------->
                                        if (parcacik_1 == "@maksimumdeğer" || ((parcacik_1.Contains("@max") || parcacik_1.Contains("@maks")) && parcacik_1.Contains("değer")))
                                        {

                                            try
                                            {
                                                int val_deger = Araçlar.SayıBul(parcacik_2);


                                                if (evrensel is ProgressBar)
                                                {
                                                    ilgilenilen_islemcubugu.Maximum = val_deger;
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                        // VALUE MAX ----------------------------------------------------------------------------------------------------------------------------------------->

                                        // VALUE MİN ----------------------------------------------------------------------------------------------------------------------------------------->
                                        if (parcacik_1 == "@minimumdeğer" || (parcacik_1.Contains("@min") && parcacik_1.Contains("değer")))
                                        {

                                            try
                                            {
                                                int val_deger = Araçlar.SayıBul(parcacik_2);


                                                if (evrensel is ProgressBar)
                                                {
                                                    ilgilenilen_islemcubugu.Minimum = val_deger;
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                        // VALUE MİN----------------------------------------------------------------------------------------------------------------------------------------->

                                        // ERİŞİM ----------------------------------------------------------------------------------------------------------------------------------------->
                                        if (parcacik_1 == "@erişim")
                                        {
                                            try
                                            {
                                                bool durum = parcacik_2 == "@aktif" ? true : false;
                                                evrensel.Enabled = durum;
                                            }
                                            catch
                                            {

                                            }
                                        }
                                        // ERİŞİM ----------------------------------------------------------------------------------------------------------------------------------------->

                                        // GÖRÜNÜRLÜK  ----------------------------------------------------------------------------------------------------------------------------------------->
                                        if (parcacik_1 == "@görünürlük" || parcacik_1.Contains("@görünür") || parcacik_1.Contains("@gizli"))
                                        {
                                            try
                                            {
                                                bool durum = parcacik_2 == "@aktif" ? true : false;
                                                evrensel.Visible = durum;
                                            }
                                            catch
                                            {

                                            }
                                        }
                                        // GÖRÜNÜRLÜK  ----------------------------------------------------------------------------------------------------------------------------------------->

                                        // İŞARETLE - İŞARETLİ - ONAYLI - ONAYLA  ----------------------------------------------------------------------------------------------------------------------------------------->
                                        if (parcacik_1.Contains("@işaret") || parcacik_1.Contains("@onay"))
                                        {
                                            try
                                            {
                                                bool durum = parcacik_2 == "@aktif" ? true : false;
                                                if (evrensel is CheckBox)
                                                {
                                                    ilgilenilen_onayişaretkutusu.Checked = durum;
                                                }
                                            }
                                            catch
                                            {

                                            }
                                        }
                                        // GÖRÜNÜRLÜK  ----------------------------------------------------------------------------------------------------------------------------------------->



                                        // KİMLİK
                                        if (parcacik_1 == "@kimlik")
                                        {
                                            try
                                            {
                                                //FALANCA:2
                                                //    MessageBox.Show(Araçlar.StringBul(parcacik_2).Trim().ToString());
                                                //  MessageBox.Show(idsi.ToString());
                                                string hamveri_kimlik;
                                                hamveri_kimlik = Obje_Adı.ToString() + ":" + Araçlar.StringBul(parcacik_2).Trim().ToString() + ":" + idsi.ToString();
                                            
                                                Kimlikler.Add(hamveri_kimlik);
                                            }
                                            catch
                                            {

                                            }
                                        }
                                        // KİMLİK


                                        // FONKSİYONEL OLAYLAR
                                        if (parcacik_1.Contains("@tıkla"))
                                        {
                                            try
                                            {

                                                foreach (string x in Objeler)
                                                {
                                                    if (x.Contains(parcacik_2.ToString().Trim()))
                                                    {

                                                        // ahmetet:2423,25124,3124124

                                                        string fonkadi = x.Split(':')[0].ToString().Trim();
                                                        string fonkicerik = veri.Substring(Convert.ToInt32(x.Split(':')[1].ToString().Trim().Split(',')[0]), Convert.ToInt32(x.Split(':')[1].ToString().Trim().Split(',')[1]) - Convert.ToInt32(x.Split(':')[1].ToString().Trim().Split(',')[0]) - 1);

                                                        foreach (string satır in fonkicerik.Split('\n'))
                                                        {
                                                            if (satır.Contains("$mesajkutusu"))
                                                            {
                                                                // fonksiyonu sadeleştirmek adına stringbul kullanılabilir
                                                                string mesaj = satır.Split('[')[1].ToString().Split(',')[0].Replace('"', ' ').Trim().ToString().Replace('_', ' ');
                                                                string başlık = satır.Split('[')[1].ToString().Split(',')[1].Split(']')[0].Replace('"', ' ').Trim().ToString().Replace('_', ' ');
                                                                // buton,idsi,mesaj,başlık
                                                                evrensel.Click += (sender, e) => Evrensel_Click_Mesaj(sender, e, mesaj, başlık);
                                                            }



                                                            //ARKAPLAN AMK
                                                            if (satır.Contains("$arkaplan"))
                                                            {
                                                                //$Arkaplan["Falanca",(100,100,200)];
                                                                try
                                                                {
                                                                    string kimlik_bul = satır.Split('[')[1].Split(',')[0].Replace('"', ' ').Trim();
                                                                    string rgbler = satır.Split('[')[1].Split('(')[1].Split(')')[0];
                                                                    string obje_temp_adı = "";
                                                                    foreach (string atom in Kimlikler)
                                                                    {
                                                                        if (atom.Contains(kimlik_bul.Trim()))
                                                                        {
                                                                            // pencere:falanca:1
                                                                            idsi = Convert.ToInt32(atom.Split(':')[2]);
                                                                            obje_temp_adı = atom.Split(':')[0];
                                                                            break;
                                                                        }
                                                                    }



                                                                    // heheh

                                                                    int R, G, B;
                                                                    R = Convert.ToInt32(rgbler.Split(',')[0]);
                                                                    G = Convert.ToInt32(rgbler.Split(',')[1]);
                                                                    B = Convert.ToInt32(rgbler.Split(',')[2]);



                                                                    if (obje_temp_adı == "pencere")
                                                                    {
                                                                        kacinci_form = Formlar_index.IndexOf(idsi); // atıyorum 5
                                                                        evrensel.Click += (sender, e) => Arkaplan_Click(sender, e, R, G, B, Formlar[kacinci_form]);

                                                                    }
                                                                    if (obje_temp_adı == "etiket")
                                                                    {
                                                                        kacinci_etiket = Etiketler_index.IndexOf(idsi);
                                                                        evrensel.Click += (sender, e) => Arkaplan_Click(sender, e, R, G, B, Etiketler[kacinci_etiket]);

                                                                    }
                                                                    if (obje_temp_adı == "buton")
                                                                    {
                                                                        kacinci_buton = Butonlar_index.IndexOf(idsi);
                                                                        evrensel.Click += (sender, e) => Arkaplan_Click(sender, e, R, G, B, Butonlar[kacinci_buton]);
                                                                    }
                                                                    if (obje_temp_adı == "metinkutusu")
                                                                    {
                                                                        kacinci_metinkutusu = Metinkutuları_index.IndexOf(idsi);
                                                                        evrensel.Click += (sender, e) => Arkaplan_Click(sender, e, R, G, B, Metinkutuları[kacinci_metinkutusu]);
                                                                    }
                                                                    if (obje_temp_adı == "işlemçubuğu")
                                                                    {
                                                                        kacinci_islemcubugu = Islemcubukları_index.IndexOf(idsi);
                                                                        evrensel.Click += (sender, e) => Arkaplan_Click(sender, e, R, G, B, Islemcubukları[kacinci_islemcubugu]);
                                                                    }
                                                                    if (obje_temp_adı == "onaykutusu" || Obje_Adı == "işaretkutusu")
                                                                    {
                                                                        kacinci_onayişaretkutusu = OnayIsaretKutuları_index.IndexOf(idsi);
                                                                        evrensel.Click += (sender, e) => Arkaplan_Click(sender, e, R, G, B, OnayIsaretkutuları[kacinci_onayişaretkutusu]);
                                                                    }

                                                                    // hehe

                                                                }
                                                                catch
                                                                {

                                                                }

                                                            }

                                                            //ARKAPLAN SON



                                                            //RENK


                                                            if (satır.Contains("$renk"))
                                                            {
                                                                //$Arkaplan["Falanca",(100,100,200)];
                                                                try
                                                                {
                                                                    string kimlik_bul = satır.Split('[')[1].Split(',')[0].Replace('"', ' ').Trim();
                                                                    string rgbler = satır.Split('[')[1].Split('(')[1].Split(')')[0];
                                                                    string obje_temp_adı = "";
                                                                    foreach (string atom in Kimlikler)
                                                                    {
                                                                        if (atom.Contains(kimlik_bul.Trim()))
                                                                        {
                                                                            // pencere:falanca:1
                                                                            idsi = Convert.ToInt32(atom.Split(':')[2]);
                                                                            obje_temp_adı = atom.Split(':')[0];
                                                                            break;
                                                                        }
                                                                    }



                                                                    // heheh

                                                                    int R, G, B;
                                                                    R = Convert.ToInt32(rgbler.Split(',')[0]);
                                                                    G = Convert.ToInt32(rgbler.Split(',')[1]);
                                                                    B = Convert.ToInt32(rgbler.Split(',')[2]);



                                                                    if (obje_temp_adı == "pencere")
                                                                    {
                                                                        kacinci_form = Formlar_index.IndexOf(idsi); // atıyorum 5
                                                                        evrensel.Click += (sender, e) => Renk_Click(sender, e, R, G, B, Formlar[kacinci_form]);

                                                                    }
                                                                    if (obje_temp_adı == "etiket")
                                                                    {
                                                                        kacinci_etiket = Etiketler_index.IndexOf(idsi);
                                                                        evrensel.Click += (sender, e) => Renk_Click(sender, e, R, G, B, Etiketler[kacinci_etiket]);

                                                                    }
                                                                    if (obje_temp_adı == "buton")
                                                                    {
                                                                        kacinci_buton = Butonlar_index.IndexOf(idsi);
                                                                        evrensel.Click += (sender, e) => Renk_Click(sender, e, R, G, B, Butonlar[kacinci_buton]);
                                                                    }
                                                                    if (obje_temp_adı == "metinkutusu")
                                                                    {
                                                                        kacinci_metinkutusu = Metinkutuları_index.IndexOf(idsi);
                                                                        evrensel.Click += (sender, e) => Renk_Click(sender, e, R, G, B, Metinkutuları[kacinci_metinkutusu]);
                                                                    }
                                                                    if (obje_temp_adı == "işlemçubuğu")
                                                                    {
                                                                        kacinci_islemcubugu = Islemcubukları_index.IndexOf(idsi);
                                                                        evrensel.Click += (sender, e) => Renk_Click(sender, e, R, G, B, Islemcubukları[kacinci_islemcubugu]);
                                                                    }
                                                                    if (obje_temp_adı == "onaykutusu" || Obje_Adı == "işaretkutusu")
                                                                    {
                                                                        kacinci_onayişaretkutusu = OnayIsaretKutuları_index.IndexOf(idsi);
                                                                        evrensel.Click += (sender, e) => Renk_Click(sender, e, R, G, B, OnayIsaretkutuları[kacinci_onayişaretkutusu]);
                                                                    }

                                                                    // hehe

                                                                }
                                                                catch
                                                                {

                                                                }

                                                            }




                                                            // RENKSON







                                                        }


                                                        break;
                                                    }
                                                }

                                            }
                                            catch
                                            {

                                            }
                                        }


                                    }
                                }
                                catch
                                {

                                }
                            }


                        }




                        ÇALIŞTIR();

                        Console.ReadKey();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            catch
            {
                return;
            }



        }

        private static void Evrensel_Click_Mesaj(object sender, EventArgs e, string mesaj, string başlık)
        {
            MessageBox.Show(mesaj, başlık);
        }
     
        private static void Arkaplan_Click(object sender, EventArgs e, int R, int G, int B, Control kontrol)
        {
            kontrol.BackColor = Color.FromArgb(R, G, B);
        }
        private static void Renk_Click(object sender, EventArgs e, int R, int G, int B, Control kontrol)
        {
            kontrol.ForeColor = Color.FromArgb(R, G, B);
        }



        // Main bitiş




        // ÇALIŞTIR BAŞLA
        public static void ÇALIŞTIR()
        {

            foreach (Form itemcik in Formlar)
            {

                foreach (Label etiketcik in Etiketler)
                {
                    itemcik.AutoSize = true;
                    itemcik.Controls.Add(etiketcik);
                }
                foreach (Button butoncuk in Butonlar)
                {
                    butoncuk.FlatStyle = FlatStyle.Popup;
                    itemcik.Controls.Add(butoncuk);
                }
                foreach (TextBox metinkutusucuk in Metinkutuları)
                {
                    itemcik.Controls.Add(metinkutusucuk);
                }
                foreach (ProgressBar Islemcubukcukları in Islemcubukları)
                {

                    itemcik.Controls.Add(Islemcubukcukları);
                }
                foreach (CheckBox oi in OnayIsaretkutuları)
                {

                    itemcik.Controls.Add(oi);
                }

                itemcik.ShowDialog();

            }



        }










    }
}
