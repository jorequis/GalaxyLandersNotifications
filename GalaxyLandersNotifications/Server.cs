using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace GalaxyLandersNotifications
{
    class Server
    {
        private MainActivity.LogDelegate logDelegate;

        public Server(MainActivity.LogDelegate logDelegate)
        {
            this.logDelegate = logDelegate;
        }

        public void Start()
        {
            byte[] ByRec;
            string logText = "";

            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint IP_PORT = new IPEndPoint(IPAddress.Any, 8999);

            try
            {

                serverSocket.Bind(IP_PORT);
                serverSocket.Listen(int.MaxValue - 1);

                while (true)
                {
                    Socket conexionSocket = serverSocket.Accept();

                    string receivedText = "";
                    ByRec = new byte[255];
                    int a = conexionSocket.Receive(ByRec, 0, ByRec.Length, 0);
                    Array.Resize(ref ByRec, a);
                    receivedText = Encoding.Default.GetString(ByRec);

                    new Thread(() => { Process(receivedText); }).Start();
                }
            }
            catch (Exception error)
            {
                logText += "\n\nError: " + error.ToString() + "\n\n";
            }
        }

        public void Process(string a)
        {

        }

        private void Log(string text)
        {
            logDelegate(text);
        }
    }
}