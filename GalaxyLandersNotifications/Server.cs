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
using Realms;

namespace GalaxyLandersNotifications
{
    class Server
    {
        private MainActivity.LogDelegate logDelegate;

        public Server(MainActivity.LogDelegate logDelegate)
        {
            this.logDelegate = logDelegate;
            new Thread(Start).Start();
        }

        public void Start()
        {
            Log("Start");
            try
            {
                byte[] ByRec;
                Log("Starting TCP Listener...");
                TcpListener serverSocket = new TcpListener(IPAddress.Any, 8999);
                serverSocket.Start(int.MaxValue - 1);
                Log("Listening on port: " + ((IPEndPoint)serverSocket.LocalEndpoint).Port);
                Log("──────────────────────────────────────────────────"); // └ ┴ ┬ ├ ─ ┼ ┘ ┌ ┐ │ ┤
                while (true)
                {
                    Socket conexionSocket = serverSocket.AcceptSocket();

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
                Log("\n\nError: " + error.ToString() + "\n\n");
            }
        }

        private void Log(string text)
        {
            logDelegate(text);
        }

        public enum Funciones { GetToken, RegistrarToken, AppStart, MissionStart, MissionEnded, LanderUpgradeStart, LanderUpgradeEnded }

        public void Process(string a)
        {
            Realm realm = Realm.GetInstance(); 
            string[] argv = a.Split('/');

            Funciones func = (Funciones) int.Parse(argv[0]);
            string userName = argv[1];

            string logText = "";
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

            logText += "User: " + userName + "\n";
            logText += func.ToString() + ": ";
            for (int i = 2; i < argv.Length; i++)
                logText += argv[i] + " ";
            Log(logText);
            Log("──────────────────────────────────────────────────"); // └ ┴ ┬ ├ ─ ┼ ┘ ┌ ┐ │ ┤

            /*NotificacionLanderUpgrade NoResponse Args: username, landername, time
            NotificacionMissionStart NoResponse Args: username, missionname, time
            NotificacionMissionEnd NoResponse Args: username, time*/
        }
    }
}