using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chargedoctor_server
{
    class Admin
    {
       public static class Sarjmatikv1
        {
            public static byte BaglantiKopar(string CihazId)
            {
                for (int i = 0; i < istemcilistesi.Sarjmatikv1.Count; i++)
                {
                    if (istemcilistesi.Sarjmatikv1[i].CihazNo == CihazId)
                    {
                        return Convert.ToByte(istemcilistesi.Sarjmatikv1[i].BaglantiKopar());
                    }
                }
                return 3;
            }
            public static byte HizmetAc(string CihazId,byte HizmetTipi,string ReferansKodu,byte Sure)
            {
                for (int i = 0; i < istemcilistesi.Sarjmatikv1.Count; i++)
                {
                    if (istemcilistesi.Sarjmatikv1[i].CihazNo == CihazId)
                    {
                        if (Sure==0)
                        {
                            return Convert.ToByte(istemcilistesi.Sarjmatikv1[i].HizmetAktifEt(HizmetTipi, ReferansKodu));
                        }
                        else
                        {
                            return Convert.ToByte(istemcilistesi.Sarjmatikv1[i].HizmetAktifEt(HizmetTipi, ReferansKodu,Sure));
                        }
                        
                    }
                }
                return 3;
            }
            public static byte HizmetAc(string CihazId, byte HizmetTipi,byte HizmetSuresi, string ReferansKodu)
            {
                for (int i = 0; i < istemcilistesi.Sarjmatikv1.Count; i++)
                {
                    if (istemcilistesi.Sarjmatikv1[i].CihazNo == CihazId)
                    {
                        return Convert.ToByte(istemcilistesi.Sarjmatikv1[i].HizmetAktifEt(HizmetTipi, ReferansKodu,HizmetSuresi));
                    }
                }
                return 3;
            }
            public static byte KilitAc(string CihazId,bool Kilit1,bool Kilit2,bool Kilit3)
            {
                for (int i = 0; i < istemcilistesi.Sarjmatikv1.Count; i++)
                {
                    if (istemcilistesi.Sarjmatikv1[i].CihazNo == CihazId)
                    {
                        return Convert.ToByte(istemcilistesi.Sarjmatikv1[i].KilitAc(Kilit1,Kilit2,Kilit3));
                    }
                }
                return 3;
            }
            public static bool BilgiOku(string CihazId)
            {
                for (int i = 0; i < istemcilistesi.Sarjmatikv1.Count; i++)
                {
                    if (istemcilistesi.Sarjmatikv1[i].CihazNo == CihazId)
                    {
                        return istemcilistesi.Sarjmatikv1[i].BilgiAl();
                    }
                }
                return false;
            }
            public static byte TestModuDegistir(string CihazId,bool TetModu)
            {
                for (int i = 0; i < istemcilistesi.Sarjmatikv1.Count; i++)
                {
                    if (istemcilistesi.Sarjmatikv1[i].CihazNo == CihazId)
                    {
                        return Convert.ToByte(istemcilistesi.Sarjmatikv1[i].TestModuDegistir(TetModu));
                    }
                }
                return 3;
            }
            public static byte AktifPasifDegistir(string CihazId, bool Aktif)
            {
                for (int i = 0; i < istemcilistesi.Sarjmatikv1.Count; i++)
                {
                    if (istemcilistesi.Sarjmatikv1[i].CihazNo == CihazId)
                    {
                        return Convert.ToByte(istemcilistesi.Sarjmatikv1[i].AktifPasifDegistir(Aktif));
                    }
                }
                return 3;
            }
            public static byte MinimumAkim(string CihazId, byte Akim)
            {
                for (int i = 0; i < istemcilistesi.Sarjmatikv1.Count; i++)
                {
                    if (istemcilistesi.Sarjmatikv1[i].CihazNo == CihazId)
                    {
                        return Convert.ToByte(istemcilistesi.Sarjmatikv1[i].MinimumAkimDegistir(Akim));
                    }
                }
                return 3;
            }
            public static byte CalismaSuresiDegistir(string CihazId, byte Sure)
            {
                for (int i = 0; i < istemcilistesi.Sarjmatikv1.Count; i++)
                {
                    if (istemcilistesi.Sarjmatikv1[i].CihazNo == CihazId)
                    {
                        return Convert.ToByte(istemcilistesi.Sarjmatikv1[i].CalismaSuresiDegistir(Sure));
                    }
                }
                return 3;
            }
            public static byte MaksimumAkimDegistir(string CihazId, byte Aktif)
            {
                for (int i = 0; i < istemcilistesi.Sarjmatikv1.Count; i++)
                {
                    if (istemcilistesi.Sarjmatikv1[i].CihazNo == CihazId)
                    {
                        return Convert.ToByte(istemcilistesi.Sarjmatikv1[i].MaksimumAkimDegistir(Aktif));
                    }
                }
                return 3;
            }
            public static byte AktifPasifDegistir(string CihazId, byte ParaSayisi)
            {
                for (int i = 0; i < istemcilistesi.Sarjmatikv1.Count; i++)
                {
                    if (istemcilistesi.Sarjmatikv1[i].CihazNo == CihazId)
                    {
                        return Convert.ToByte(istemcilistesi.Sarjmatikv1[i].CalismaParaSayisiDegistir(ParaSayisi));
                    }
                }
                return 3;
            }
            public static byte AnlikIzlemeDegistir(string CihazId,int IzlemeAraligi,bool AnlikIzleme)
            {
                for (int i = 0; i < istemcilistesi.Sarjmatikv1.Count; i++)
                {
                    if (istemcilistesi.Sarjmatikv1[i].CihazNo == CihazId)
                    {
                        return Convert.ToByte(istemcilistesi.Sarjmatikv1[i].AnlikIzlemeDegistir(AnlikIzleme,IzlemeAraligi));
                    }
                }
                return 3;
            }
            public static byte CalismaParaSayisiDegistir(string CihazId,byte ParaSayisi)
            {
                for (int i = 0; i < istemcilistesi.Sarjmatikv1.Count; i++)
                {
                    if (istemcilistesi.Sarjmatikv1[i].CihazNo == CihazId)
                    {
                        return Convert.ToByte(istemcilistesi.Sarjmatikv1[i].CalismaParaSayisiDegistir(ParaSayisi));
                    }
                }
                return 3;
            }
            public static byte IslemSifresiDegistir(string CihazId, string Sifre)
            {
                for (int i = 0; i < istemcilistesi.Sarjmatikv1.Count; i++)
                {
                    if (istemcilistesi.Sarjmatikv1[i].CihazNo == CihazId)
                    {
                        return Convert.ToByte(istemcilistesi.Sarjmatikv1[i].IslemSifresiDegistir(Sifre));
                    }
                }
                return 3;
            }
            public static JObject KilitOku(string CihazId)
            {
               
                JObject kilitler = new JObject();
                for (int i = 0; i < istemcilistesi.Sarjmatikv1.Count; i++)
                {
                    if (istemcilistesi.Sarjmatikv1[i].CihazNo == CihazId)
                    {

                        
                        kilitler.Add("kilit1", istemcilistesi.Sarjmatikv1[i].Kilit1);
                        kilitler.Add("kilit2", istemcilistesi.Sarjmatikv1[i].Kilit2);
                        kilitler.Add("kilit3", istemcilistesi.Sarjmatikv1[i].Kilit3);
                        kilitler.Add("durum", "1");
                    }
                }
                kilitler.Add("durum", "0");
                return kilitler;
            }
            public static JObject SoketDurumuOku(string CihazId)
            {
                JObject soketler = new JObject();
                JArray kalansure = new JArray();
                

                for (int i = 0; i < istemcilistesi.Sarjmatikv1.Count; i++)
                {
                    if (istemcilistesi.Sarjmatikv1[i].CihazNo == CihazId)
                    {
                        for (int j = 0; j < istemcilistesi.Sarjmatikv1[i].KalanSure.Length; j++)
                        {
                            kalansure.Add(istemcilistesi.Sarjmatikv1[i].KalanSure[i]);
                        }
                        soketler.Add("kalansure", kalansure);
                    }
                }
                soketler.Add("durum", "0");
                return soketler;
            }
        }
       
    }
}
