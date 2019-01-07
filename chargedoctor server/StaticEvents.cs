using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace chargedoctor_server
{
    
    class StaticEvents
    {
        public  void SarjmatikBaglantiKesildi(string Ip, string CihazNo)
        {
           
            for (int i = 0; i < istemcilistesi.Sarjmatikv1.Count; i++)
            {
                
                if (istemcilistesi.Sarjmatikv1[i].CihazNo==CihazNo)
                {
                    istemcilistesi.Sarjmatikv1.Remove(istemcilistesi.Sarjmatikv1[i]);
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("| " + CihazNo + " | " + "Kimlik numaralı" + " | " + Ip + " | " + "ip adresli cihaz bağlantısı kesildi. İşlem zamanı: " + DateTime.Now.ToString());
            Console.ResetColor();
        }
    }
}
