using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace chargedoctor_server
{
    class Sarjmatikv1isleyici
    {
        Socket Socket;
        string CihazNo;
        string Ip;
      public  Sarjmatikv1isleyici(Socket Socket ,string CihazNo)
        {
            this.Ip = Socket.RemoteEndPoint.ToString();
            this.Socket = Socket;
            this.CihazNo = CihazNo;
        }
        /// <summary>
        /// Kimlik doğrulaması tamamlanan  istemciden gelen verilerin kabul edildiği bölüm
        /// </summary>
       public void VeriAlisverisiBaslat()
        {
            new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    int _Read;
                    string _Result;
                    byte[] _Temp = new byte[55555];
                    try
                    {
                        
                        _Read = Socket.Receive(_Temp, 0, 55555, SocketFlags.None);
                        byte[] _Received = new byte[_Read];
                        Array.Copy(_Temp, 0, _Received, 0, _Read);
                        _Result = System.Text.ASCIIEncoding.ASCII.GetString(_Received);
                        _Result = _Result.Substring(0, _Received.Length);
                        Islemyap(_Result);

                    }
                    catch (SocketException ex)
                    {
                        if (ex.SocketErrorCode == SocketError.ConnectionAborted)
                        {
                            break;
                        }
                        else if (ex.SocketErrorCode == SocketError.TimedOut)
                        {
                            continue;
                        }
                        else if (ex.SocketErrorCode == SocketError.WouldBlock)
                        {
                            break;
                        }
                        else
                        {
                          
                            break;
                        }

                    }
                    catch (ObjectDisposedException)
                    {
                        break;
                    }
                }
               
                StaticEvents StaticEvents = new StaticEvents();
                StaticEvents.SarjmatikBaglantiKesildi(Ip,CihazNo);
            }))
            { IsBackground = true }.Start();

        }
        /// <summary>
        /// İstemciden gelen verinin işlendiği bölüm
        /// </summary>
        /// <param name="GelenString">İstemciden gelen verinin string hali</param>
        void Islemyap(string GelenString)
        {
            //try
            //{
                JObject GelenJson = JObject.Parse(GelenString);
                if (GelenJson[Sabitler.Sarjmatikv1.Gelen.islem].ToString() == Sabitler.Sarjmatikv1.Gelen.hizmet)
                {
                    HizmetEkle(GelenJson);

                }
                else if (GelenJson[Sabitler.Sarjmatikv1.Gelen.islem].ToString() == Sabitler.Sarjmatikv1.Gelen.kilitbilgisi)
                {

                }
                else if (GelenJson[Sabitler.Sarjmatikv1.Gelen.islem].ToString() == Sabitler.Sarjmatikv1.Gelen.soketdurumu)
                {

                }
                
            //}
            //catch (Exception)
            //{
               
               
            //}
            
           
        }

        void KilitDurumuOku(JObject KilitDurumuJson)
        {
            try
            {
                for (int i = 0; i < istemcilistesi.Sarjmatikv1.Count; i++)
                {
                    if (istemcilistesi.Sarjmatikv1[i].CihazNo == CihazNo)
                    {
                        istemcilistesi.Sarjmatikv1[i].KilitDurumuListele(Convert.ToBoolean(KilitDurumuJson[Sabitler.Sarjmatikv1.Gelen.kilit1]), Convert.ToBoolean(KilitDurumuJson[Sabitler.Sarjmatikv1.Gelen.kilit2]), Convert.ToBoolean(KilitDurumuJson[Sabitler.Sarjmatikv1.Gelen.kilit3]));
                    }
                    }
            }
            catch (Exception)
            {

                throw;
            }
        }
        void SoketDurumuOku(JObject SoketDurumuJson)
        {
            try
            {
                for (int i = 0; i < istemcilistesi.Sarjmatikv1.Count; i++)
                {
                    if (istemcilistesi.Sarjmatikv1[i].CihazNo==CihazNo)
                    {
                        byte[] Acikmi = new byte[8];
                        byte[] NekadarCalissin = new byte[8];
                        byte[] CalismaSureleri = new byte[8];
                        for (int j = 0; j < istemcilistesi.Sarjmatikv1[i].Acikmi.Length; j++)
                        {
                         
                            Acikmi[j]= Convert.ToByte(SoketDurumuJson[Sabitler.Sarjmatikv1.Gelen.acikmi][j]);
                            NekadarCalissin[j]= Convert.ToByte(SoketDurumuJson[Sabitler.Sarjmatikv1.Gelen.nekadarcalissin][j]);
                            CalismaSureleri[j]= Convert.ToByte(SoketDurumuJson[Sabitler.Sarjmatikv1.Gelen.calismasuresi][j]);
                            

                        }
                        istemcilistesi.Sarjmatikv1[i].SoketDurumuListele(Acikmi,NekadarCalissin,CalismaSureleri);
                    }
                }
        }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Cihazdan gelen cihaz  bilgilerini (ayarlar v.s) sarjmatik nesnesine atar
        /// </summary>
        /// <param name="CihazBilgisiJson">cihaz bilgileri işlenecek json verisi</param>
        void CihazBilgisiOku(JObject CihazBilgisiJson)
        {
            // JObject GidecekYanit = new JObject();
            try
            {
                for (int i = 0; i < istemcilistesi.Sarjmatikv1.Count; i++)
                {
                    if (istemcilistesi.Sarjmatikv1[i].CihazNo==CihazNo)
                    {
                        
                        istemcilistesi.Sarjmatikv1[i].AyarListele(Convert.ToByte(CihazBilgisiJson[Sabitler.Sarjmatikv1.Gelen.minimumakim]),Convert.ToByte(CihazBilgisiJson[Sabitler.Sarjmatikv1.Gelen.maksimumakim]),Convert.ToByte(CihazBilgisiJson[Sabitler.Sarjmatikv1.Gelen.parasayisi]),Convert.ToBoolean(CihazBilgisiJson[Sabitler.Sarjmatikv1.Gelen.aktif]),Convert.ToBoolean(CihazBilgisiJson[Sabitler.Sarjmatikv1.Gelen.testmodu]),Convert.ToBoolean(CihazBilgisiJson[Sabitler.Sarjmatikv1.Gelen.anlikizleme]));

                    }
                }
            }
            catch (Exception)
            {

                
            }
            
        }
        /// <summary>
        /// Hizmet verisini işler yanıtını istemciye gönderir
        /// </summary>
        /// <param name="HizmetJson">hizmet verisi json formatı</param>
        void HizmetEkle(JObject HizmetJson)
        {
            Sql Sql = new Sql();
            JObject GidecekYanit = new JObject();
            GidecekYanit.Add(Sabitler.Sarjmatikv1.Giden.islem, Sabitler.Sarjmatikv1.Giden.hizmet);
            GidecekYanit.Add(Sabitler.Sarjmatikv1.Giden.islemkodu, HizmetJson[Sabitler.Sarjmatikv1.Gelen.islemkodu].ToString());
            try
            {

                if (Sql.HizmetEkle(CihazNo, Convert.ToByte(HizmetJson[Sabitler.Sarjmatikv1.Gelen.soketno]), Convert.ToByte(HizmetJson[Sabitler.Sarjmatikv1.Gelen.soketdurumu]), DateTime.Parse(HizmetJson[Sabitler.Sarjmatikv1.Gelen.islemzamani].ToString()), HizmetJson[Sabitler.Sarjmatikv1.Gelen.islemkodu].ToString()))
                { 
                    
                    GidecekYanit.Add(Sabitler.Sarjmatikv1.Giden.durum, Sabitler.Sarjmatikv1.hizmetislem.basarili.ToString());
                    
                }
                else
                {
                    GidecekYanit.Add(Sabitler.Sarjmatikv1.Giden.durum, Sabitler.Sarjmatikv1.hizmetislem.basarisiz.ToString());
                }
        }
            catch (Exception)
            {
               
                GidecekYanit.Add(Sabitler.Sarjmatikv1.Giden.durum, Sabitler.Sarjmatikv1.hizmetislem.hata.ToString());
            }
            finally
            {
                VeriYolla(GidecekYanit.ToString());
            }
            
        }
        /// <summary>
        /// İşlenen veriyi cliente yanıt olarak gönderir
        /// </summary>
        /// <param name="gidecekveri">string veri tipinde cliente gidecek yanıt verisi</param>
        void VeriYolla(string gidecekveri)
        {
            try
            {
                byte[] Buffer = ASCIIEncoding.ASCII.GetBytes(gidecekveri);
                Socket.Send(Buffer);
            }
            catch (SocketException)
            {
                return;
                
            }
            
            
        }

        #region Veritabanı işlemleri -------------------------------------------------------------
        class Sql
        {
         
            /// <summary>
            /// Cihazdan gelen  hizmet verisini veritabanına kaydeder
            /// </summary>
            /// <param name="CihazId">Hizmet gönderen cihazın Id numarası (CihazNo)</param>
            /// <param name="SoketNo">Cıhazın işlem yapılan şarj soketi numarası</param>
            /// <param name="SoketDurumu">İşlem yapılan soket telefon algıladı mı yoksa algılamadı mı</param>
            /// <param name="IslemZamani">Cihazdaki işlemin yapılma zamanı</param>
            /// <returns></returns>
          public  bool HizmetEkle(string CihazId,byte SoketNo,byte SoketDurumu,DateTime IslemZamani,string IslemKodu)
            {
                try
                {
                    using (SqlConnection baglanti = new SqlConnection(Ayarlar.SqlServer.CihazlarConnectionString))
                    {
                        using (SqlCommand sorgu = new SqlCommand("sp_Cihaz_Hizmet_Ekle", baglanti))
                        {
                            
                            sorgu.CommandType = CommandType.StoredProcedure;
                            sorgu.Parameters.Add("@CihazId", SqlDbType.VarChar, 20).Value = CihazId;
                            sorgu.Parameters.Add("@SoketNo", SqlDbType.TinyInt).Value = SoketNo;
                            sorgu.Parameters.Add("@SoketDurumu", SqlDbType.Bit).Value = SoketDurumu;
                            sorgu.Parameters.Add("@IslemZamani", SqlDbType.DateTime).Value = IslemZamani;
                            sorgu.Parameters.Add("@IslemKodu", SqlDbType.VarChar, 50).Value = IslemKodu;
                            sorgu.Parameters.Add("@sonuc", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                            baglanti.Open();
                            sorgu.ExecuteNonQuery();
                            if (sorgu.Parameters["@sonuc"].Value.ToString() == "1")
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        return false;
                    }
                }
                catch (Exception)
                {
                   
                    return false;
                }
               
                
            }
        }

        #endregion
    }

}
