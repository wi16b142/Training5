using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Training5.Com
{
    public class Communication
    {
        const int port = 10100; //port for connection
        Socket serverSocket, clientSocket; //sockets for client and server connection
        Action<byte[], string> Informer; //informs the mainVM about a pending gui update with the received buffer and the name of the sender as parameter
        bool isServer; //flag to check if you are server or not. necessary to set the correct name of the sender
        byte[] buffer = new byte[512]; //buffer for sending and receiving data
        string sender; //holding the senders name

        public Communication(Action<byte[], string> informer, bool isServer)
        {
            Informer = informer; //delegate for the method informing the mainVM about gui updates
            this.isServer = isServer; //set flag if server or client

            if (isServer) //if you are a server
            {
                serverSocket = new Socket(SocketType.Stream, ProtocolType.Tcp); //create socket
                serverSocket.Bind(new IPEndPoint(IPAddress.Loopback, port)); //bind socket to nic
                serverSocket.Listen(1); //listen for one connection at most
                Task.Factory.StartNew(StartAccepting); //start to accept a client
            }
            else //if you are a client
            {
                TcpClient client = new TcpClient(); //create a new tcp client
                client.Connect(IPAddress.Loopback, port); //connect client to the server ip address
                clientSocket = client.Client; //store the tcp client in the clientsocket
                Task.Factory.StartNew(Receive); //beginn receiving
            }
        }

        private void StartAccepting()
        {
            clientSocket = serverSocket.Accept(); //accet 1 client and save the accepted socket into cliensocket
            Task.Factory.StartNew(Receive); //when the connection is established, start receiving messages from the client
        }

        private void Receive()
        {
            while (true) //infinite loop
            {
                clientSocket.Receive(buffer); //receive data from the clientsocket into the byte buffer
                if (isServer) //if you are the server
                {
                    sender = "client"; //you can only receive from the client, so the sender is the client
                }
                else sender = "server"; //otherwise you are the client and only receive from the server

                Informer(buffer,sender); //after that, inform the mainVM which updates the gui with the new state and the senders name
            }
        }

        public void Send(byte[] data)
        {
            clientSocket.Send(data); //this sends the give data to the counterpart of the connection
        }
    }
}
