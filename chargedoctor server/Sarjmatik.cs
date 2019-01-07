using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace chargedoctor_server
{
    class Sarjmatik
    {
        /// <summary>
        /// asd
        /// </summary>
        public Sarjmatik(string CihazNo,Socket Socket,DateTime BaglanmaZamani,string Ip)
        {
            this.CihazNo = CihazNo;
            this.Socket = Socket;
            this.BaglanmaZamani1 = BaglanmaZamani;
            this.Ip1 = Ip;
        }
        DateTime BaglanmaZamani1;
        string Ip1;
        string CihazNo1;
        string SeriNo1;
        string UretimTarihi1;
        string ModelNo1;
        byte MinimumAkim1;
        byte MaksimumAkim1;
        byte CalismaParaSayisi1;
        bool Aktif1;
        bool TestModu1;
        bool Kilit11;
        bool Kilit21;
        bool Kilit31;
        byte[] CalismaSureleri1 = new byte[8];
        byte[] NekadarCalissin1 = new byte[8];
        byte[] Acikmi1 = new byte[8];
        int[] KalanSure1 = new int[8];
        string IslemSifresi1;
        Socket Socket;
        bool AnlikIzleme1;
        int AnlikIzlemeAraligi1;
        public DateTime BaglanmaZamani { get { return BaglanmaZamani1; }set { BaglanmaZamani1 = value; } }
        public string CihazNo { get;}
        public string Ip { get { return Ip1; } }
        public string SeriNo { get { return SeriNo1; } }
        public string UretimTarihi { get { return UretimTarihi1; } }
        public string ModelNo { get { return ModelNo1; } }
        public byte MinimumAkim { get { return MinimumAkim1; }}
        public byte MaksimumAkim { get { return MaksimumAkim1; } }
        public byte CalismaParaSayisi { get { return CalismaParaSayisi1; } }
        public bool Aktif { get { return Aktif1; } }
        public bool TestModu { get {return TestModu1; } }
        public bool Kilit1 { get { return Kilit11; } }
        public bool Kilit2 { get { return Kilit21; } }
        public bool Kilit3 { get { return Kilit31; } }
        public byte[] CalismaSuresi { get { return CalismaSureleri1; } }
        public byte [] NekadarCalissin { get { return NekadarCalissin1; } }
        public byte[] Acikmi { get { return Acikmi1; } }
        public int[] KalanSure { get
            {
                for (int i = 0; i < KalanSure1.Length; i++)
                {
                    KalanSure1[i] = NekadarCalissin1[i] - CalismaSureleri1[i];
                }
                return KalanSure1;
            }
        }
        public string IslemSifresi { get { return IslemSifresi1; } }
        public bool AnlikIzleme { get { return AnlikIzleme1;} }
        public int AnlikIzlemeAraligi { get { return AnlikIzlemeAraligi; } }
        /// <summary>
        /// Cihazdan gelen kilit sensörleri durumlarını kaydeder
        /// </summary>
        /// <param name="Kilit1"></param>
        /// <param name="Kilit2"></param>
        /// <param name="Kilit3"></param>
        public void KilitDurumuListele(bool Kilit1,bool Kilit2,bool Kilit3)
        {
            Kilit11 = Kilit1;
            Kilit21 = Kilit2;
            Kilit31 = Kilit3;
        }
        /// <summary>
        /// Cihazdan gelen anlık soket durumlarını kaydeder.
        /// </summary>
        /// <param name="Acikmi">Şarj soketlerinin açık veya kapalı olma durumu</param>
        /// <param name="NekadarCalissin">Şarj soketlerinin ne kadar süre çalışacağı</param>
        /// <param name="Calismasureleri">Şarj soketlerinin ne kadar süre çalıştığı</param>
        public void SoketDurumuListele(byte[] Acikmi,byte[] NekadarCalissin,byte[] CalismaSuresi)
        {
            CalismaSureleri1 = CalismaSuresi;
            Acikmi1 = Acikmi;
            NekadarCalissin1 = NekadarCalissin;
        }    
        /// <summary>
        /// Cihazdan gelen ayarları nesneye kaydeder.
        /// </summary>
        /// <param name="MinimumAkim"></param>
        /// <param name="MaksimumAkim"></param>
        /// <param name="CalismaParaSayisi"></param>
        /// <param name="Aktif"></param>
        /// <param name="TestModu"></param>
        /// <param name="AnlikIzleme"></param>
       public void AyarListele(byte MinimumAkim,byte MaksimumAkim,byte CalismaParaSayisi,bool Aktif,bool TestModu,bool AnlikIzleme)
        {
            MinimumAkim1 = MinimumAkim;
            MaksimumAkim1 = MaksimumAkim;
            CalismaParaSayisi1 = CalismaParaSayisi;
            Aktif1 = Aktif;
            TestModu1 = TestModu;
            AnlikIzleme1 = AnlikIzleme;
        }
        /// <summary>
        /// Anlık izleme bilgisini değiştirir. Anlık izleme bilgisi cihazdan eş zamanlı bilgi alışverişi için kullanılır
        /// 
        /// </summary>
        /// <param name="AnlikIzleme">Anlık izleme özelliğini aktif veya pasif eder (Veri True ise aktif False ise pasif olacaktır.)</param>
        /// <param name="IzlemeAraligi">Veri iletim intervali (Saniye cinsinden değer verilerek verilen değer aralığında bir veri gönderecektir.)</param>
        /// <returns></returns>
       public bool AnlikIzlemeDegistir(bool AnlikIzleme,int IzlemeAraligi)
        {
            JObject AnlikIzlemeJson = new JObject();
            AnlikIzlemeJson.Add("is","AnlikIzleme");
            AnlikIzlemeJson.Add("IzlemeAraligi", IzlemeAraligi);
            AnlikIzlemeJson.Add("Aktif", Convert.ToByte(AnlikIzleme));
            if (VeriYolla(AnlikIzlemeJson.ToString()) == true)
            {
                return true;
            }
            else
            {
                return false;
                throw new System.ArgumentException("Veri cihaza iletilemedi cihaz bağlantısı kapalı olabilir", "HATA");
            }
        }
        /// <summary>
        /// Cihaza ait şarj çıkışlarının açık olduğunu ve kalan süre bilgisini cihazdan ister.
        /// </summary>
        /// <exception cref="System.ArgumentException"> Socket üzerinden veri gönderirken bir hata oluştuğunda bu exception döner</exception>
        /// <returns>Değer True döndüğünde veri başarıyla gönderilmiş demektir.</returns>
        bool DurumOku()
        {
            JObject DurumOkuJson = new JObject();
            DurumOkuJson.Add("is", "do");
            if (VeriYolla(DurumOkuJson.ToString()) == true)
            {
                return true;
            }
            else
            {
                return false;
                throw new System.ArgumentException("Veri cihaza iletilemedi cihaz bağlantısı kapalı olabilir", "HATA");
            }
        }
        /// <summary>
        /// Cihaza ait bilgileri cihazdan ister.(Örn: Seri numarası,ayarlar v.s)
        /// </summary>
        /// <returns> Değer True döndüğünde veri başarıyla gönderilmiş demektir</returns>
      public  bool BilgiAl()
        {
            JObject BilgiAlJson = new JObject();
            BilgiAlJson.Add("is", "ba");
            if (VeriYolla(BilgiAlJson.ToString()) == true)
            {
                return true;
            }
            else
            {
                return false;
                throw new System.ArgumentException("Veri cihaza iletilemedi cihaz bağlantısı kapalı olabilir", "HATA");
            }
        }
        /// <summary>
        /// Herhangibir şarj çıkışını açmak için kullanılır.
        /// </summary>
        /// <param name="HizmetTipi">Açılacak şarj çıkışı için hizmet tipini belirler değer 1 ise normal paralı hizmet değer 2 ise veri kaydı tutulmayan ücretsiz hizmet</param>
        /// <param name="ReferansKodu">Gönderilen hizmet açma işlemini doğrulamak için işlem bittiğinde geri dönecek olan referans kodu</param>
        /// <param name="Sure">Kaç dakika açık kalacağı.</param>
        /// <returns>Değer True döndüğünde  veri başarıyla gönderilmiştir.</returns>
      public  bool HizmetAktifEt(byte HizmetTipi,string ReferansKodu, byte Sure)
        {
            JObject HizmetJson = new JObject();
            HizmetJson.Add("is","ha");
            HizmetJson.Add("ht",HizmetTipi);
            HizmetJson.Add("hs",Sure);
            HizmetJson.Add("rk",ReferansKodu);
            if (VeriYolla(HizmetJson.ToString()) == true)
            {
                return true;
            }
            else
            {
                return false;
                throw new System.ArgumentException("Veri cihaza iletilemedi cihaz bağlantısı kapalı olabilir", "HATA");
            }
        }
        /// <summary>
        /// Herhangibir şarj çıkışını açmak için kullanılır. Burada çalışma süresi otomatik olarak 1 hizmet birimidir
        /// </summary>
        /// <param name="HizmetTipi">Açılacak şarj çıkışı için hizmet tipini belirler değer 1 ise normal paralı hizmet değer 2 ise veri kaydı tutulmayan ücretsiz hizmet</param>
        /// <param name="ReferansKodu">Gönderilen hizmet açma işlemini doğrulamak için işlem bittiğinde geri dönecek olan referans kodu</param>
        /// <returns> Değer True döndüğünde  veri başarıyla gönderilmiştir.</returns>
      public  bool HizmetAktifEt(byte HizmetTipi, string ReferansKodu)
        {
            JObject HizmetJson = new JObject();
            HizmetJson.Add("is", "ha");
            HizmetJson.Add("ht", HizmetTipi);
            HizmetJson.Add("hs", "1b");
            HizmetJson.Add("rk", ReferansKodu);
            if (VeriYolla(HizmetJson.ToString()) == true)
            {
                return true;
            }
            else
            {
                return false;
                throw new System.ArgumentException("Veri cihaza iletilemedi cihaz bağlantısı kapalı olabilir", "HATA");
            }
        }
        /// <summary>
        /// Cihaz üzerindeki 3 adet kilitten herhangibirini açabilmek için kullanılır
        /// </summary>
        /// <param name="Kilit1">1 Numaralı kilit durumu değer 1 ise 1 numaralı kilit açılacaktır.</param>
        /// <param name="Kilit2">2 Numaralı kilit durumu değer 1 ise 2 numaralı kilit açılacaktır.</param>
        /// <param name="Kilit3">3 Numaralı kilit durumu değer 1 ise 3 numaralı kilit açılacaktır.</param>
        /// <returns>Değer True döndüğünde  veri başarıyla gönderilmiştir.</returns>
       public bool KilitAc(bool Kilit1,bool Kilit2,bool Kilit3)
        {
            JObject KilitJson=new JObject();
            KilitJson.Add("is","kd");
            KilitJson.Add("k1", Convert.ToByte(Kilit1));
            KilitJson.Add("k2", Convert.ToByte(Kilit2));
            KilitJson.Add("k3", Convert.ToByte(Kilit1));
            KilitJson.Add("sfr",IslemSifresi1);

            if (VeriYolla(KilitJson.ToString()))
            {
                return true;
            }
            else
            {
                return false;
                throw new System.ArgumentException("Veri cihaza iletilemedi cihaz bağlantısı kapalı olabilir", "HATA");
            }
            
        }
        /// <summary>
        /// Cihazı test moduna almak veya test modundan çıkarmak için kullanılır.
        /// </summary>
        /// <param name="TestModu">Değre True ise test modu aktif olur  False ise test modu kapanır</param>
        /// <returns>Değer True döndüğünde  veri başarıyla gönderilmiştir.</returns>
      public  bool TestModuDegistir(bool TestModu)
        {
            JObject TestModuJson = new JObject();
            TestModuJson.Add("is", "tmd");
            TestModuJson.Add("tmb",Convert.ToByte(TestModu));
            if (VeriYolla(TestModuJson.ToString())==true)
            {
                return true;
            }
            else
            {
                return false;
                throw new System.ArgumentException("Veri cihaza iletilemedi cihaz bağlantısı kapalı olabilir","HATA");
            }

        }
        /// <summary>
        /// Cihazın aktif pasif durumunu değiştirir. Cihaz pasif yapılırsa anakart fonksiyonları çalışmayacak ve hizmet almayacaktır.
        /// </summary>
        /// <param name="Aktif">Değer True ise cihaz aktif duruma getirilir False ise pasif duruma getirilir.</param>
        /// <returns>Değer True döndüğünde  veri başarıyla gönderilmiştir.</returns>
       public bool AktifPasifDegistir(bool Aktif)
        {
            JObject AktifJson = new JObject();
            AktifJson.Add("is", "apd");
            AktifJson.Add("sfr",IslemSifresi1);
            AktifJson.Add("abp",Aktif);

            if (VeriYolla(AktifJson.ToString()) == true)
            {
                return true;
            }
            else
            {
                return false;
                throw new System.ArgumentException("Veri cihaza iletilemedi cihaz bağlantısı kapalı olabilir", "HATA");
            }
        }
        /// <summary>
        /// Cihaz şarj çıkışlarındaki algılama akım değerini ayarlar.
        /// </summary>
        /// <param name="MinimumAkim">Minimum algılama değeri</param>
        /// <returns>Değer True döndüğünde  veri başarıyla gönderilmiştir.</returns>
       public bool MinimumAkimDegistir (byte MinimumAkim)
        {
            JObject AkimJson = new JObject();
            AkimJson.Add("is","mnad");
            AkimJson.Add("sfr",IslemSifresi1);
            AkimJson.Add("akm", MinimumAkim);
            if (VeriYolla(AkimJson.ToString()) == true)
            {
                return true;
            }
            else
            {
                return false;
                throw new System.ArgumentException("Veri cihaza iletilemedi cihaz bağlantısı kapalı olabilir", "HATA");
            }
        }
        /// <summary>
        /// Cihaz şarj çıkışlarındaki maksimum algılama değerini algılar
        /// </summary>
        /// <param name="MaksimumAkim">Maksimum algılama değeri</param>
        /// <returns>Değer True döndüğünde  veri başarıyla gönderilmiştir.</returns>
       public bool MaksimumAkimDegistir(byte MaksimumAkim)
        {
            JObject AkimJson = new JObject();
            AkimJson.Add("is","mkd");
            AkimJson.Add("sfr", IslemSifresi1);
            AkimJson.Add("akm",MaksimumAkim);
            if (VeriYolla(AkimJson.ToString()) == true)
            {
                return true;
            }
            else
            {
                return false;
                throw new System.ArgumentException("Veri cihaza iletilemedi cihaz bağlantısı kapalı olabilir", "HATA");
            }
        }
        /// <summary>
        /// Cihazın kaç adet para ile çalışacağını ayarlar.
        /// </summary>
        /// <param name="ParaSayisi">Cihazdan hizmet alabilmek için atılacak olan toplam minimum para miktarı</param>
        /// <returns>Değer True döndüğünde  veri başarıyla gönderilmiştir.</returns>
       public bool CalismaParaSayisiDegistir(byte ParaSayisi)
        {
            JObject ParaJson = new JObject();
            ParaJson.Add("is", "pd");
            ParaJson.Add("sfr",IslemSifresi1);
            ParaJson.Add("mkt", ParaSayisi);
            if (VeriYolla(ParaJson.ToString()) == true)
            {
                return true;
            }
            else
            {
                return false;
                throw new System.ArgumentException("Veri cihaza iletilemedi cihaz bağlantısı kapalı olabilir", "HATA");
            }
        }
        public bool CalismaSuresiDegistir(byte sarjsuresi)
        {
            JObject SureJson = new JObject();
            SureJson.Add("is", "srd");
            SureJson.Add("sfr", IslemSifresi1);
            SureJson.Add("sr", sarjsuresi);
            if (VeriYolla(SureJson.ToString()) == true)
            {
                return true;
            }
            else
            {
                return false;
                throw new System.ArgumentException("Veri cihaza iletilemedi cihaz bağlantısı kapalı olabilir", "HATA");
            }
        }
        /// <summary>
        /// Cihazın işlem şifresi değiştirilir. İşlem şifresi 10 haneli rakam ve harf karışımı değerden oluşmalıdır
        /// </summary>
        /// <param name="sifre">Yeni cihaz şifresi</param>
        /// <returns>Değer True döndüğünde  veri başarıyla gönderilmiştir.</returns>
        public bool IslemSifresiDegistir(string sifre)
        {
            for (int i = 0; i < sifre.Length; i++)
            {
                if (char.IsSymbol(sifre[i]))
                {
                    throw new System.ArgumentNullException("İşlem şifresi özel karakter içeremez ! Sadece harf ve sayı girin", "HATA !");
                }
            }
            if (sifre.Length!=10)
            {
                throw new System.ArgumentNullException("İşlem şifresi değiştirilemedi (İşlem şifresi rakam ve harflerden oluşan 10 haneli bir değer olmalıdır)","HATA !");
            }
            else
            {
                JObject SifreJson = new JObject();
                SifreJson.Add("is","srd");
                SifreJson.Add("sfr", sifre);
                SifreJson.Add("sr",IslemSifresi1);
                if (VeriYolla(SifreJson.ToString()) == true)
                {
                    return true;
                }
                else
                {
                    throw new System.ArgumentException("Veri cihaza iletilemedi cihaz bağlantısı kapalı olabilir", "HATA");
                }
            }
            
        }
        public bool BaglantiKopar()
        {
            try
            {
                Socket.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
                
            }
            
        }
        /// <summary>
        /// Cihaza veri gönderir
        /// </summary>
        /// <param name="veri">Cihaza gönderilecek veri</param>
        /// <returns>Değer True döndüğünde  veri başarıyla gönderilmiştir.</returns>
        bool VeriYolla(string veri)
        {
            try
            {
                byte[] buffer = Encoding.ASCII.GetBytes(veri);
                Socket.Send(buffer);
                return true;
            }
            catch (SocketException)
            {

                return false;
            }
            catch (ObjectDisposedException)
            {
                return false;
            }
        }
    }
}
