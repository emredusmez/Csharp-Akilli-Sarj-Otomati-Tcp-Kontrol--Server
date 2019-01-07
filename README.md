# C# Akıllı şarj otomatı kontrol sunucusu. 
###### Akıllı Şarj Otomatı Sunucusu Csharp multi threading socket programlama hazır stabil örnek

# Açıklama:

Akıllı şarj otomatı sunucusu  online çalışan  akıllı şarj otomatları için geliştirilmiş ve çalışır halde tamamlanmış bir tcp sunucusudur.
Yazılımın işlevi  kendisine tcp protokolü ile bağlanan şarj otomat cihazlarından gelen verileri toplamak,cihaz üzerindeki sensör kilit v.b modülleri izlemek
ve cihazların uzaktan kontrolünü sağlamaktır.  Sunucuya birden fazla istemci cihaz eş zamanlı olarak bağlanabilir ve birbirinden bağımsız şekilde kontrol edilebilir.

## İçerisindeki sınıflar ve temel işlevleri şu şekildedir:

``` server.cs: ``` Bu sınıf içerisinde temel tcp server başlatma yapısı mevcut. Belirlenen ip ve port ile socket sunucusu başlatılarak cihazlar ile iletişim kurmaya hazır hale gelir

```Dogrulama.cs:``` Bu sınıf içerisinde client doğrulama kontrolleri yapılarak clientin hangi işleme koyulacağına yön verilir.  server.cs sınıfı tcp sunucusunu başlatarak ilk bağlantıyı kabul eder burada kabul edilen bağlantılar  Dogrulama.cs  sınıfına yönlendirilir burada bağlanan istemcinin kimlik doğrulama işlemi yapılır.  Bağlanan istemcinin ilk gönderdiği paket ile birlikte gelen kimlik bilgileri veritabanındakiler ile karşılaştırılır kimlik bilgileri kayıtlar ile eşleşirse  istemcinin cihaz türü belirlenerek doğrulama kontrolünden geçer ve artık güvenli istemciler listesine eklenerek  tüm işlemlere hazır hale gelir.

```kripto.cs:``` İstemci üzerinden gelen şifreli verilerin şifresi bu sınıf ile açılır ve işlenebilir hale getirilir.

```istemcilistesi.cs:``` Bu sınıf içerisinde kimlik doğrulamasından başarılı bir şekilde geçen istemciler listelenir. Bu listede bulunan tüm istemciler artık veri alışverişine hazırdır ve listeden istemci seçilerek veri gönderme işlemleri yapılabilir.

```Enumlar.cs:``` haberleşem json yapısındaki verilerin başlıkları burada belirlenmiştir.

```Sarjmatikv1isleyici.cs:``` Bu sınıf bağlanan istemcinin(Şarj otomatının) verilerinin işleneceği yerdir. Dogrulama.cs sınıfında kimlik doğrulamasından geçen istemcinin  türü belirlenir türü belirlendikten sonra eğer istemci  türü sarjmatikv1 ile uyumlu ise artık gelen paketler  Sarjmatikv1isleyici.cs sınıfına yönlendirilir ve istemci ile yapılan tüm haberleşme bu sınıf üzerinden yürütülür.
## ```Sarjmatikv1isleyici.cs:``` Sınıfı içeriğinde bulunan fonksiyonlar.

```void VeriAlisverisiBaslat(): 
void Islemyap(string GelenString):
void KilitDurumuOku(JObject KilitDurumuJson):
void SoketDurumuOku(JObject SoketDurumuJson)
void CihazBilgisiOku(JObject CihazBilgisiJson):
void HizmetEkle(JObject HizmetJson): 
```
```AdminIsleyici.cs:``` Dogrulama.cs içerisinde doğrulanan client türü admin ise gelen paketler buraya yönlendirilir. Bu sınıf içerisinde  adminden gelen komutler ayrıştırılarak Admin.cs üzerinde gerekli işlemler yaptırılır

```Admin.cs:```  Adminden gelen komutların işlenip Sarjmatikv1isleyici.cs sınıfına yönlendirildiği bölüm. Burada adminden gelen komutlar işlenerek gerekli işlemler yaptırılır.


```Sarjmatik.cs:``` Bağlanan istemciyi temsil eden sınıfır. Dogrulama.cs içerisinde bir bağlantı kimlik doğrulamasından geçtikten sonra  Sarjmatik.cs sınıfı türetilir ve  bağlantı bilgileri, cihaz bilgileri v.s  nesne içerisine aktarılır.
İstemcilere veri gönderme işlemi bu sınıf üzerinden yapılır.
## ```Sarjmatik.cs:``` Sınıfı içerisindeki fonksiyonlar:
```public  bool HizmetAktifEt(byte HizmetTipi,string ReferansKodu, byte Sure):``` Çalıştırıldığında cihaza üzerindeki  parametre olarak gönderilen şarj kablosunu aktif etmesi için gerekli bilgi gönderilir. Bu fonksiyon ile cihaz üzerindeki şarj kabloları aktif hale getirileblir.

```public void KilitDurumuListele(bool Kilit1,bool Kilit2,bool Kilit3):``` Cihaz üzerindeki kilitlerin açık veya kapalı olduğu bilgisini ister

```public void SoketDurumuListele(byte[] Acikmi,byte[] NekadarCalissin,byte[] CalismaSuresi):``` Cihaz üzerindeki şarj kablolarından birini aktif et

```public void AyarListele(byte MinimumAkim,byte MaksimumAkim,byte CalismaParaSayisi,bool Aktif,bool TestModu,bool AnlikIzleme):``` Cihaz ayarlarını listele

```public bool AnlikIzlemeDegistir(bool AnlikIzleme,int IzlemeAraligi):``` Anlık izleme modunu aç kapat

```public  bool BilgiAl():``` Cihaz bilgilerini listele

```public bool KilitAc(bool Kilit1,bool Kilit2,bool Kilit3):```Cihaz üzerindeki kilitleri açar veya kapatır

```public  bool TestModuDegistir(bool TestModu):``` Cihazı test moduna alır veya test modunu kapatır

```public bool AktifPasifDegistir(bool Aktif):``` Cihazı kullanım dışı yapar veya kullanıma alabililir.

```public bool MinimumAkimDegistir (byte MinimumAkim):```Cihaz üzerindeki şarj kablolarının çalışması için gerekli olan minimum akım miktarını ayarlar

```public bool MaksimumAkimDegistir(byte MaksimumAkim):```Cihaz üzerindeki şarj kablolarının çalışması için gerekli olan maksimum akım miktarını ayarlar

```public bool CalismaParaSayisiDegistir(byte ParaSayisi):``` Cihazın kullanılması için gerekli para sayısını değiştirir

```public bool CalismaSuresiDegistir(byte sarjsuresi):``` Cihazın çalışma süresini ayarlar

```public bool IslemSifresiDegistir(string sifre):``` Cihaz üzerinden  kilit açma v.b işlemler yapabilmek için gerekli şifreyi değiştirir

```public bool BaglantiKopar():``` Cihaz bağlantısını koparır```



![alt text](https://lh3.googleusercontent.com/lAFed7g4OM6nE-GkkUmsF-ijxeYdHueRKOLqLGKVKB0fca-B-7fPwM-nAf2p7CCedh7tFgULC9rmZ3JVrY1MIumyEgIUSmnksMWTCwUgb_4Nxg-2vzFWXJzv_9D5MzdH-4EH9VmkHNMH_hPWhKNo1ZQFCDC8j1JnLsFUyxDgwRjsvyTL4WY0kwpDP8mt_qtxz4k7VRkXNuc_godCyrzNyQBicAkrsNmRIWGjPL8x6FV3UGufTKrCK4qTLz90y1-8Z7PbohgsG_3v3c5y-QcClmn2MlS-Y9dbHbsafZ_t-Il9kpRcmuBJzlIeacXDA0Krhdg7A5CIiQwEG2m_26fhNTho-tcpoLRgcevVEnRLEbrgP_WmIOiSyppHqOhmlTCapKQjCjNmEkhE24ErEZJaBo4trY15xHlR82CZ8sTCbSaM3004C0334z_ArJc3p08_T8CXfybnpF-_MkXT50FnKzXMyn8n9EEiC0Gwl-MjnIOq1pZC-LiDgPSxr5Ekz1QjK7nd2p747zz9p5a_zqLAv8ydR3oWT0GyAk9ctdXV9L5s2Vn7ckCIg419PL02fkyMKPWlnYEGEOiDEo58BwLjOH61XohPmgxQ-pKGHRMPEOUWEKtbbjlKT2KzEQOw3p1fD7MWRXtmkH26NZko3UttZOgs7zKwxWNs6yFIKV-8j2qv0RHMiGSZu0Q_Hz9GSq3jetv6evm69P6-Cb_aTg=w978-h506-no)
