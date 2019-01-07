using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace chargedoctor_server
{
    class AdminIsleyici
    {
        Socket Socket;
        AdminIsleyici(Socket Socket)
        {
            this.Socket = Socket;
        }
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
                        IslemYap(_Result);

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
            }))
            { IsBackground = true }.Start();
        }
        void IslemYap(string GelenString)
        {
           
            JObject GelenJson = JObject.Parse(GelenString);
            JObject GidenJson=new JObject();
            string CihazId = GelenJson[Sabitler.Admin.Gelen.cihazid].ToString();
            if (GelenJson[Sabitler.Admin.Gelen.islem].ToString()==Sabitler.Admin.Gelen.bilgioku)
            {
                GidenJson.Add(Sabitler.Admin.Giden.durum, Admin.Sarjmatikv1.BilgiOku(CihazId));
                
            }
            else if (GelenJson[Sabitler.Admin.Gelen.islem].ToString()==Sabitler.Admin.Gelen.kilitoku)
            {
                GidenJson.Add(Sabitler.Admin.Giden.durum, Admin.Sarjmatikv1.KilitOku(CihazId));

            }
            else if (GelenJson[Sabitler.Admin.Gelen.islem].ToString()==Sabitler.Admin.Gelen.soketdurumuoku)
            {
                GidenJson.Add(Sabitler.Admin.Giden.durum, Admin.Sarjmatikv1.SoketDurumuOku(CihazId));
            }
            else if (GelenJson[Sabitler.Admin.Gelen.islem].ToString()==Sabitler.Admin.Gelen.kilitac)
            {
                GidenJson.Add(Sabitler.Admin.Giden.durum, Admin.Sarjmatikv1.KilitAc(CihazId,Convert.ToBoolean(GelenJson[Sabitler.Admin.Gelen.kilit1]),Convert.ToBoolean(GelenJson[Sabitler.Admin.Gelen.kilit2]), Convert.ToBoolean(GelenJson[Sabitler.Admin.Gelen.kilit3])));
            }
            else if (GelenJson[Sabitler.Admin.Gelen.islem].ToString()==Sabitler.Admin.Gelen.anlikizlemedegistir)
            {
                GidenJson.Add(Sabitler.Admin.Giden.durum, Admin.Sarjmatikv1.AnlikIzlemeDegistir(CihazId,int.Parse(GelenJson[Sabitler.Admin.Gelen.izlemearaligi].ToString()), Convert.ToBoolean(GelenJson[Sabitler.Admin.Gelen.anlikizleme])));
            }
            else if (GelenJson[Sabitler.Admin.Gelen.islem].ToString()==Sabitler.Admin.Gelen.testmodudegistir)
            {
                GidenJson.Add(Sabitler.Admin.Giden.durum, Admin.Sarjmatikv1.TestModuDegistir(CihazId,Convert.ToBoolean(GelenJson[Sabitler.Admin.Gelen.testmodu])));
            }
            else if (GelenJson[Sabitler.Admin.Gelen.islem].ToString()==Sabitler.Admin.Gelen.hizmetaktifet)
            {
                GidenJson.Add(Sabitler.Admin.Giden.durum, Admin.Sarjmatikv1.HizmetAc(CihazId, Convert.ToByte(GelenJson[Sabitler.Admin.Gelen.hizmettipi]), GelenJson[Sabitler.Admin.Gelen.referanskodu].ToString(), Convert.ToByte(GelenJson[Sabitler.Admin.Gelen.sarjsuresi])));
            }
            else if (GelenJson[Sabitler.Admin.Gelen.islem].ToString()==Sabitler.Admin.Gelen.aktifpasifdegistir)
            {
                GidenJson.Add(Sabitler.Admin.Giden.durum, Admin.Sarjmatikv1.AktifPasifDegistir(CihazId, Convert.ToBoolean(GelenJson[Sabitler.Admin.Gelen.aktifpasif])));

            }
            else if (GelenJson[Sabitler.Admin.Gelen.islem].ToString()==Sabitler.Admin.Gelen.minimumakimdegistir)
            {
                GidenJson.Add(Sabitler.Admin.Giden.durum, Admin.Sarjmatikv1.MinimumAkim(CihazId, Convert.ToByte(GelenJson[Sabitler.Admin.Gelen.akim])));

            }
            else if (GelenJson[Sabitler.Admin.Gelen.islem].ToString()==Sabitler.Admin.Gelen.maksimumakimdegistir)
            {
                GidenJson.Add(Sabitler.Admin.Giden.durum, Admin.Sarjmatikv1.MaksimumAkimDegistir(CihazId, Convert.ToByte(GelenJson[Sabitler.Admin.Gelen.akim])));

            }
            else if (GelenJson[Sabitler.Admin.Gelen.islem].ToString()==Sabitler.Admin.Gelen.parasayisidegistir)
            {
                GidenJson.Add(Sabitler.Admin.Giden.durum, Admin.Sarjmatikv1.CalismaParaSayisiDegistir(CihazId, Convert.ToByte(GelenJson[Sabitler.Admin.Gelen.parasayisi])));

            }
            else if (GelenJson[Sabitler.Admin.Gelen.islem].ToString()==Sabitler.Admin.Gelen.islemsifresidegistir)
            {
                GidenJson.Add(Sabitler.Admin.Giden.durum, Admin.Sarjmatikv1.IslemSifresiDegistir(CihazId, GelenJson[Sabitler.Admin.Gelen.islemsifresi].ToString()));

            }
            else if (GelenJson[Sabitler.Admin.Gelen.islem].ToString()==Sabitler.Admin.Gelen.calismasuresidegistir)
            {
                GidenJson.Add(Sabitler.Admin.Giden.durum, Admin.Sarjmatikv1.CalismaSuresiDegistir(CihazId, Convert.ToByte(GelenJson[Sabitler.Admin.Gelen.calismasuresi])));

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
    }
}
