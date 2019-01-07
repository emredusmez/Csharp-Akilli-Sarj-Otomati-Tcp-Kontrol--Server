using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace chargedoctor_server
{
    //https://github.com/emredusmez
    //İletişim için ydmez6@gmail.com
    class server
    {
        private Socket _Listener;
        private bool _Listening;
       
        /// <summary>
        /// asdasd
       
        /// </summary>
        public server()
        {
            
            _Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
       public void Bind(IPEndPoint IpEndPoint)
        {
            
            _Listener.Bind(IpEndPoint);
            _Listening = true;
            _Listener.Listen(-1);

        }
        public void Start()
        {
            new Thread(new ThreadStart(() =>
            {
                while (this._Listening)
                {
                    Socket Accepted = _Listener.Accept();
                    ReceiveLoop(Accepted);
                }
                
            }))
            { IsBackground = true }.Start();
        }
        void ReceiveLoop(Socket Socket)
        {
            Dogrulama Dogrulama = new Dogrulama(Socket);
            new Thread(new ThreadStart(() =>
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

                   Dogrulama.VeriAyir(_Result);
               }
               catch (SocketException ex)
               {
                   if (ex.SocketErrorCode == SocketError.ConnectionAborted)
                   {
                       // break;

                   }
                   else if (ex.SocketErrorCode == SocketError.TimedOut)
                   {
                       // continue;
                   }
                   else if (ex.SocketErrorCode == SocketError.WouldBlock)
                   {
                       //break;
                   }
                   else
                   {
                       //  Socket.Close();
                       //break;
                   }

               }
               catch (ObjectDisposedException)
               {
                   //break;
               }

              
           }))
            { IsBackground = true }.Start();
        }
       


    }
}
