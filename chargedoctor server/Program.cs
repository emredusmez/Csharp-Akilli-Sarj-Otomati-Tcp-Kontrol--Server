using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace chargedoctor_server
{
    class Program
    {
        //https://github.com/emredusmez
        //İletişim için ydmez6@gmail.com
        static void Main(string[] args)
        {
           
            Console.Title = "CHARGEDOCTOR CİHAZ SUNUCUSU";
            server Server = new server();
            
            Server.Bind(new IPEndPoint(IPAddress.Any, 4563));
            Server.Start();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(IPAddress.Any + ":4563" + " Adresiyle server başlatıldı");
            Console.ResetColor();
            new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    Thread.Sleep(5000);
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("Bağlı client sayısı: " + istemcilistesi.Sarjmatikv1.Count);
                    Console.ResetColor();
                }
            }))
            { IsBackground = true }.Start();
            while (true)
            {
                string KonsolVerisi = Console.ReadLine();
                try
                {
                    string ayiklanmisveri = KonsolVerisi.Substring(0, KonsolVerisi.IndexOf("&"));
                    switch (ayiklanmisveri)
                    {
                        case "help":
                            StringBuilder sb = new StringBuilder();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            sb.AppendLine("Cihaz bağlantısı kesmek için: disconnect CİHAZNO");
                            sb.AppendLine("Konsolu temizlemek için     : clear");
                            sb.AppendLine("Bağlı cihaz sayısını öğrenmek için: cihazlar");
                            Console.WriteLine(sb);
                            Console.ResetColor();
                            break;
                        case "cihazlar":
                            StringBuilder sb2 = new StringBuilder();

                            for (int i = 0; i < istemcilistesi.Sarjmatikv1.Count; i++)
                            {
                                sb2.AppendLine("Cihaz kimliği: " + istemcilistesi.Sarjmatikv1[i].CihazNo + " - Bağlanma zamanı: " + istemcilistesi.Sarjmatikv1[i].BaglanmaZamani.ToString() + " - Ip adresi: " + istemcilistesi.Sarjmatikv1[i].Ip);

                            }
                            if (istemcilistesi.Sarjmatikv1.Count == 0)
                            {
                                sb2.AppendLine("Bağlı cihaz  mevcut değil");
                            }
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine(sb2);
                            Console.ResetColor();
                            break;
                        case "kick":
                            byte durum = Admin.Sarjmatikv1.BaglantiKopar(KonsolVerisi.Substring(KonsolVerisi.IndexOf("&")+1, KonsolVerisi.Length-KonsolVerisi.IndexOf("&")-1));
                            if (1 == durum)
                            {
                                Console.WriteLine(KonsolVerisi.Substring(KonsolVerisi.IndexOf("&")+1, KonsolVerisi.Length - KonsolVerisi.IndexOf("&")-1) + " Kimlik numaralı cihaz bağlantısı koparıldı.");
                            }
                            else if (3 == durum)
                            {
                                Console.WriteLine(KonsolVerisi.Substring(KonsolVerisi.IndexOf("&")+1, KonsolVerisi.Length - KonsolVerisi.IndexOf("&")-1) + " Kimlik numaralı cihaz bağlı değil !");
                            }

                            break;
                        case "kilitac":
                            Admin.Sarjmatikv1.KilitAc("FEDERAL2",true,false,true);
                            break;
                    }
            }
                catch (Exception)
            {

              
            }

        }
        }
    }
}
