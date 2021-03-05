using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net.Sockets;
using System.IO;
using System.Net;

namespace socket
{
    public partial class Form1 : Form
    {
        Button btnStartServer;
        private StreamWriter serverStreamWriter;
        private StreamReader serverStreamReader;
        private Button btnSendMessage;
        private StreamReader clientStreamReader;
        private StreamWriter clientStreamWriter;
        public Form1()
        {
            InitializeComponent();

            //create StartServer button set its properties & event handlers 
            this.btnStartServer = new Button();
            this.btnStartServer.Text = "Start Server";
            this.btnStartServer.Click +=
                new System.EventHandler(this.button1_Click);

            //add controls to form
            this.Controls.Add(this.btnStartServer);
        }

           

            //public static void Main1(string[] args)
            //{
            ////creat n display windows form
            //   Form1 tcpSockServer = new Form1();
            //    Application.Run(tcpSockServer);
            //}
            private bool StartServer()
            {
               
            System.Net.IPAddress ipaddress = System.Net.IPAddress.Parse("127.0.0.1");
            IPEndPoint ipLocalEndPoint = new IPEndPoint(ipaddress, 10443);
            TcpListener tcpClientA = new TcpListener(ipaddress,10443);
            tcpClientA.Start();        //start server
                Console.WriteLine("Server Started");
                this.btnStartServer.Enabled = false;
         
            Socket serverSocket = tcpClientA.AcceptSocket();

                try
                {
                    if (serverSocket.Connected)
                    {
                        Console.WriteLine("Client connected");
                  
                        NetworkStream serverSockStream =
                            new NetworkStream(serverSocket);
                    serverStreamWriter =
                        new StreamWriter(serverSockStream);
                    serverStreamReader =
                        new StreamReader(serverSockStream);
                }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    return false;
                }

                return true;
            }
        private bool ConnectToServer()
        {
            //connect to server at given port
            try
            {
                TcpClient tcpClient = new TcpClient("localhost", 4444);
                Console.WriteLine("Connected to Server");
                //get a network stream from server
                NetworkStream clientSockStream = tcpClient.GetStream();
                clientStreamReader = new StreamReader(clientSockStream);
                clientStreamWriter = new StreamWriter(clientSockStream);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }

            return true;
        }
        private void button1_Click(object sender, System.EventArgs e)
            {
                //start server
                if (!StartServer())
                    Console.WriteLine("Unable to start server");

                //sending n receiving msgs
                while (true)
                {
                Console.WriteLine("CLIENT: " + serverStreamReader.ReadLine());
                serverStreamWriter.WriteLine("Hi!");
                serverStreamWriter.Flush();
            }
            }
        }
    }
    

