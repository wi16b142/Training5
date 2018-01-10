using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Training5.Com
{
    public class Communication
    {
        const int port = 10100;
        Socket serverSocket, clientSocket;
        Action<byte[], string> Informer;
        bool isServer;
        byte[] buffer = new byte[512];
        string sender;

        public Communication(Action<byte[], string> informer, bool isServer)
        {
            Informer = informer;
            this.isServer = isServer;

            if (isServer)
            {
                serverSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(IPAddress.Loopback, port));
                serverSocket.Listen(1);
                Task.Factory.StartNew(StartAccepting);
            }
            else
            {
                TcpClient client = new TcpClient();
                client.Connect(IPAddress.Loopback, port);
                clientSocket = client.Client;
                Task.Factory.StartNew(Receive);
            }
        }

        private void StartAccepting()
        {
            clientSocket = serverSocket.Accept();
            Task.Factory.StartNew(Receive);
        }

        private void Receive()
        {
            while (true)
            {
                clientSocket.Receive(buffer);
                if (isServer)
                {
                    sender = "client";
                }
                else sender = "server";

                Informer(buffer,sender);
            }
        }

        public void Send(byte[] data)
        {
            clientSocket.Send(data);
        }
    }
}
