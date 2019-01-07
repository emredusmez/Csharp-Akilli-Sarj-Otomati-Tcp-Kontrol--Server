using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace chargedoctor_server
{
    class Dogrulama
    {
        
        Socket Socket;
        IPEndPoint IstemciIp;
       public Dogrulama(Socket Socket)
        {
            this.Socket = Socket;
            this.IstemciIp = Socket.RemoteEndPoint as IPEndPoint;
        }
       public void VeriAyir(string GelenString)
        {
            sql sql = new sql();
            try
            {
             
                JObject GelenJson = JObject.Parse(GelenString);
                if (GelenJson["istemci"].ToString()==Sabitler.Sarjmatikv1.Gelen.istemcitipi)
                {

                    if (sql.SarjmatikDogrula(GelenJson[Sabitler.Sarjmatikv1.Gelen.cihazno].ToString(), GelenJson[Sabitler.Sarjmatikv1.Gelen.guvenlikkodu].ToString()))
                    {
                        bool CihazVarmi = false;
                        for (int i = 0; i < istemcilistesi.Sarjmatikv1.Count; i++)
                        {
                            if (istemcilistesi.Sarjmatikv1[i].CihazNo == GelenJson[Sabitler.Sarjmatikv1.Gelen.cihazno].ToString())
                            {
                                CihazVarmi = true;
                            }
                        }
                        if (CihazVarmi == false)
                        {
                            istemcilistesi.Sarjmatikv1.Add(new Sarjmatik(GelenJson[Sabitler.Sarjmatikv1.Gelen.cihazno].ToString(), Socket, DateTime.Now, IstemciIp.ToString()));
                            Sarjmatikv1isleyici Sarjmatikv1isleyici = new Sarjmatikv1isleyici(Socket, GelenJson[Sabitler.Sarjmatikv1.Gelen.cihazno].ToString());
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("| " + GelenJson[Sabitler.Sarjmatikv1.Gelen.cihazno].ToString() + " | " + " Kimlik numaralı | " + IstemciIp.ToString() + " | ip adresli cihaz bağlandı ve kimliğini doğruladı. İşlem zamanı: " + DateTime.Now.ToString());
                            Console.ResetColor();
                            Socket.Send(new byte[55]);
                            Sarjmatikv1isleyici.VeriAlisverisiBaslat();
                        }
                    }
                    else
                    {
                        Socket.Close();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("| " + GelenJson[Sabitler.Sarjmatikv1.Gelen.cihazno].ToString() + " | " + " Kimlik numarası | " + IstemciIp.ToString() + " | ip adresi  ile bağlanmak isteyen şarjmatikv1.1  kendini doğrulayamadı  " + DateTime.Now.ToString());
                        Console.ResetColor();
                    }
                   
                }
                else if (GelenJson["istemci"].ToString()=="admin" )
                {
                    string Ip = IstemciIp.ToString();
                    Ip= Ip.Substring(0, Ip.IndexOf(":"));
                    if (Ip=="127.0.0.1")
                    {
                        // admin ip adresi onaylandı
                    }
                }
            }
            catch (Exception )
            {

                throw;

            }
        }

        class sql
        {
         public   bool SarjmatikDogrula(string CihazNo,string GuvenlikKodu)
            {
                Console.WriteLine(CihazNo + " " + GuvenlikKodu);
                try
                {
                    using (SqlConnection baglanti = new SqlConnection(Ayarlar.SqlServer.CihazlarConnectionString))
                    {
                        using (SqlCommand sorgu = new SqlCommand("sp_Server_sarjmatikv1_dogrulama", baglanti))
                        {
                        
                            sorgu.CommandType = CommandType.StoredProcedure;
                            sorgu.Parameters.Add("@CihazId", SqlDbType.VarChar, 20).Value = CihazNo;
                            sorgu.Parameters.Add("@GuvenlikKodu", SqlDbType.VarChar,20).Value = GuvenlikKodu;
                            
                            sorgu.Parameters.Add("@sonuc", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                        baglanti.Open();
                        sorgu.ExecuteNonQuery();
                            if (sorgu.Parameters["@sonuc"].Value.ToString() != "0")
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

                    Console.WriteLine("hata");
                    return false;
                }


            }
        
        }

    }
}
