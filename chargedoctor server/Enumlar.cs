using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chargedoctor_server
{
   static class Sabitler
    {
     public static  class Sarjmatikv1
        {
            public enum hizmetislem:byte
            {
                basarili=1,
                basarisiz=2,
                hata=3
            }
            public static  class Gelen
            {
                public const string minimumakim = "minimumakim";
                public const string maksimumakim = "maksimumakim";
                public const string calismaparasayisi = "calismaparasayisi";
                public const string aktif = "aktif";
                public const string testmodu = "testmodu";
                public const string anlikizleme = "anlikizleme";
                public const string parasayisi = "parasayisi";
                public const string hizmet = "hizmet";
                public const string islemkodu = "islemkodu";
                public const string soketno = "soketno";
                public const string soketdurumu = "soketdurumu";
                public const string islemzamani = "islemzamani";
                public const string durum = "durum";
                public const string islem = "islem";
                public const string kilitbilgisi = "kilitbilgisi";
                public const string acikmi = "acikmi";
                public const string nekadarcalissin = "nekadarcalissin";
                public const string calismasuresi = "calismasuresi";
                public const string kilit1 = "kilit2";
                public const string kilit2 = "kilit2";
                public const string kilit3 = "kilit3";
                public const string istemcitipi = "sarjmatikv1.1";
                public const string cihazno = "cihazno";
                public const string guvenlikkodu = "guvenlikkodu";

            }
           public static class Giden
            {
                public const string durum = "durum";
                public const string islem = "islem";
                public const string hizmet = "hizmet";
                public const string islemkodu = "islemkodu";
                public const string soketno = "soketno";
                public const string soketdurumu = "soketdurumu";
                public const string islemzamani = "islemzamani";
            }
        }
     public static class Admin
        {
            public static class Gelen
            {
                public const string islem = "islem";
                public const string soketdurumuoku = "soktdurumuoku";
                public const string bilgioku = "bilgioku";
                public const string kilitoku = "kilitoku";
                public const string kilitac = "kilitac";
                public const string anlikizlemedegistir = "anlikizlemedegistir";
                public const string testmodudegistir = "testmodudegistir";
                public const string hizmetaktifet = "hizmetaktifet";
                public const string aktifpasifdegistir = "aktifpasifdegistir";
                public const string minimumakimdegistir = "minimumakimdegistir";
                public const string maksimumakimdegistir = "maksimumakimdegistir";
                public const string parasayisidegistir = "parasayisidegistir";
                public const string islemsifresidegistir = "islemsifresidegistir";
                public const string calismasuresidegistir = "calismasuresidegistir";
                public const string cihazid = "cihazid";
                public const string kilit1 = "kilit1";
                public const string kilit2 = "kilit2";
                public const string kilit3 = "kilit3";
                public const string anlikizleme = "anlikizleme";
                public const string izlemearaligi = "izlemearaligi";
                public const string testmodu = "testmodu";
                public const string hizmettipi = "hizmettipi";
                public const string referanskodu = "referanskodu";
                public const string sarjsuresi = "sarjsuresi";
                public const string aktifpasif = "aktifpasif";
                public const string akim = "akim";
                public const string parasayisi = "parasayisi";
                public const string islemsifresi = "islemsifresi";
                public const string calismasuresi = "calismasuresi";

            }
            public static class Giden
            {
                public const string soketdurumu = "soketdurumu";
                public const string durum = "durum";
                public const string kilit1 = "kilit1";
                public const string kilit2 = "kilit2";
                public const string kilit3 = "kilit3";

            }
        }
    }
}
