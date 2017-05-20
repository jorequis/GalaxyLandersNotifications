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
                Log("Starting socket...");
                serverSocket.Bind(IP_PORT);
                serverSocket.Listen(int.MaxValue - 1);
                Log("Listening on port: " + IP_PORT.Address + " " + IP_PORT.Port);
                Log("=============================================");

                while (true)
                {
                    Socket conexionSocket = serverSocket.Accept();
                    Log("Conected");

                    string receivedText = "";
                    ByRec = new byte[255];
                    int a = conexionSocket.Receive(ByRec, 0, ByRec.Length, 0);
                    Array.Resize(ref ByRec, a);
                    receivedText = Encoding.Default.GetString(ByRec);
                    conexionSocket.Close();

                    new Thread(() => { Process(receivedText); }).Start();
                }
            }
            catch (Exception error)
            {
                logText += "\n\nError: " + error.ToString() + "\n\n";
            }
        }

        private void Log(string text)
        {
            logDelegate(text);
        }

        public enum Funciones { GetToken, RegistrarToken, AppStart, MissionStart, MissionEnded, LanderUpgradeStart, LanderUpgradeEnded }

        public void Process(string a)
        {
            string[] argv = a.Split('/');

            Funciones func = (Funciones) int.Parse(argv[0]);
            string userName = argv[1];

            switch (func)
            {
                case Funciones.GetToken:
                    break;
                case Funciones.RegistrarToken:
                    break;
                case Funciones.AppStart:
                    break;
                case Funciones.MissionStart:
                    break;
                case Funciones.MissionEnded:
                    break;
                case Funciones.LanderUpgradeStart:
                    break;
                case Funciones.LanderUpgradeEnded:
                    break;
            }

            Log("Funtion: " + func.ToString());
            Log("From: " + userName);
            for (int i = 2; i < argv.Length; i++)
                Log("Arg" + (i - 1) + ": " + argv[i]);
            Log("=============================================");

            /*NotificacionLanderUpgrade NoResponse Args: username, landername, time
            NotificacionMissionStart NoResponse Args: username, missionname, time
            NotificacionMissionEnd NoResponse Args: username, time*/
        }
    }
}