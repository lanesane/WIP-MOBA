using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using WIP_MOBA_Server.Data;

namespace WIP_MOBA_Server.Communication
{
    class StateObject
    {
        // Client socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const Int32 BufferSize = 2048;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received dataQue string.
        public StringBuilder sb = new StringBuilder();
    }

    public class Connection
    {

        #region Variables and Constants
        private static Int32 port = 11000; // default port number
        private static string ip = "127.0.0.1"; // default ip address (localhost)
        private static ServerClient client;
        private static DataShare dataShare;
        private static DataQue dataQue;
        private static Socket control;
        private static StateObject state = new StateObject();
        
        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);

        private static String response = String.Empty;
        #endregion


        #region Initialization and Setup
        public static void StartControlComms(ServerClient _client, Int32 _port, string _ip, DataShare _dataShare, DataQue _dataQue)
        {
            client = _client;
            dataQue = _dataQue;
            dataShare = _dataShare;
            port = _port;
            ip = _ip;

            try
            {
                IPHostEntry ipHostInfo = Dns.Resolve(ip);
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP socket.
                control = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                Console.WriteLine("[Communications] Waiting for connection to Server...");
                // Connect to the remote endpoint.
                control.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), control);
                connectDone.WaitOne();

                state.workSocket = control;

                Console.WriteLine("[Communications] Connected to Server"); 
                
                Receive(control);
                receiveDone.WaitOne();
                while (true)
                {
                    receiveDone.Reset();
                    Receive(control);

                    String dataString = dataQue.GetData();
                    if (dataString != null)
                    {
                        if (dataString == "Start Game")
                        {
                            Send(state.workSocket, "[START]<INFO><EOS>");
                            Console.WriteLine("[Communications] Game is Starting");
                        }
                        else if (dataString == "Stop Game")
                        {
                            Send(state.workSocket, "[STOP]<INFO><EOS>");
                            Console.WriteLine("[Communications] Game has Stopped");
                        }
                    }

                    Thread.Sleep(10);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static Boolean CheckConnection(Socket s)
        {
            return !((s.Poll(1000, SelectMode.SelectRead) && (s.Available == 0)) || !s.Connected);
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state Object.
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.
                client.EndConnect(ar);

                Console.WriteLine("[Communications] Connected to {0}", client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.
                connectDone.Set();
            }
            catch (Exception e) 
            { 
                Console.WriteLine(e.ToString()); 
            }
        }
        #endregion


        #region Receive
        private static void Receive(Socket control)
        {
            try
            {
                // Begin receiving the dataQue from the remote device.
                control.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                //Console.WriteLine("[ERROR] " + e.ToString());
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket control = state.workSocket;

            try
            {
                if (CheckConnection(control))
                {
                    // Read dataQue from the remote device.
                    Int32 bytesRead = control.EndReceive(ar);

                    if (bytesRead > 0)
                    {
                        // There might be more dataQue, so store the dataQue received so far.
                        state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                        if (state.sb.Length > 1)
                        {
                            if (state.sb.ToString().IndexOf("<COL>") > -1)
                            {
                                // Signal that all bytes have been received.
                                response = state.sb.ToString();
                                state.sb.Clear();

                                if (response.IndexOf("Control") > -1)
                                {
                                    Console.WriteLine("[Communications] Assigned as Control by Server");
                                    Send(control, "ConnectionC<EOS>");
                                    sendDone.WaitOne();
                                    receiveDone.Set();
                                }
                                else
                                {
                                    Console.WriteLine("[Communications] Message received : {0}", response);
                                    receiveDone.Set();
                                }
                            }
                            else if (state.sb.ToString().IndexOf("<MESS>") > -1)
                            {
                                response = state.sb.ToString();
                                state.sb.Clear();
                                Console.WriteLine("[Communications] Message received: {0}", response);
                                receiveDone.Set();
                            }
                            else if (state.sb.ToString().IndexOf("<CON>") > -1)
                            {
                                response = state.sb.ToString();
                                state.sb.Clear();
                                if (response.IndexOf("1") > -1)
                                {
                                    Console.WriteLine("[Connection] Client #1 Connected!");
                                    receiveDone.Set();
                                    client.SetControlPropertyThreadSafe(client.lClient1, "Text", "Client #1: Connected!");
                                    client.SetControlPropertyThreadSafe(client.lClient1, "ForeColor", Color.Green);
                                    dataShare.Client1Connected();
                                }
                                else if (response.IndexOf("2") > -1)
                                {
                                    Console.WriteLine("[Connection] Client #2 Connected!");
                                    receiveDone.Set();
                                    client.SetControlPropertyThreadSafe(client.lClient2, "Text", "Client #2: Connected!");
                                    client.SetControlPropertyThreadSafe(client.lClient2, "ForeColor", Color.Green);
                                    dataShare.Client2Connected();
                                }

                                if (dataShare.GetClient1Connected() && dataShare.GetClient2Connected())
                                {
                                    client.SetControlPropertyThreadSafe(client.bStart, "Enabled", true);
                                }
                            }
                            else if (state.sb.ToString().IndexOf("<DATA>") > -1)
                            {
                                response = state.sb.ToString();
                                state.sb.Clear();
                                receiveDone.Set();
                                Trace.WriteLine("[" + DateTime.Now + "]" + "[Communications] Dictionary Received:\n" + response); 

                                String team = "";
                                Int32 i = response.IndexOf('-') + 1;
                                while (response[i] != ':')
                                {
                                    team += response[i];
                                    i++;
                                }
                                Console.WriteLine("[Data] The match data received is for Team " + team +
                                    ". Pushing to sort.");
                                Trace.WriteLine("[" + DateTime.Now + "]" + "[Data] The match data received is for Team " + team +
                                    ". Pushing to sort.");
                                try
                                {
                                    // dataShare.SortPackedData(response, dataShare.GetIndexForTeam(team));
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("[ERROR] Error during the sorting:\n      " + e.Message);
                                    Trace.WriteLine("[" + DateTime.Now + "]" + " [ERROR] Error during the sorting:  " + e.Message);
                                    Trace.WriteLine(e.StackTrace);
                                }
                            }
                            else if (state.sb.ToString().IndexOf("<DISCON>") > -1)
                            {
                                response = state.sb.ToString();
                                state.sb.Clear();
                                if (response.IndexOf("1") > -1)
                                {
                                    Console.WriteLine("[Connection] Client #1 Computer Disconnected!");
                                    client.SetControlPropertyThreadSafe(client.lClient1, "Text", "Client #1: Disconnected!");
                                    client.SetControlPropertyThreadSafe(client.lClient1, "ForeColor", Color.Red);
                                    dataShare.Client1Disconnected();
                                    client.SetControlPropertyThreadSafe(client.bStart, "Enabled", false);
                                    receiveDone.Set();
                                }
                                else if (response.IndexOf("2") > -1)
                                {
                                    Console.WriteLine("[Connection] Client #2 Computer Disconnected!");
                                    client.SetControlPropertyThreadSafe(client.lClient2, "Text", "Client #2: Disconnected!");
                                    client.SetControlPropertyThreadSafe(client.lClient2, "ForeColor", Color.Red);
                                    dataShare.Client1Disconnected();
                                    client.SetControlPropertyThreadSafe(client.bStart, "Enabled", false);
                                    receiveDone.Set();
                                }
                            }
                            else
                            {
                                // Get the rest of the dataQue.
                                control.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                    new AsyncCallback(ReceiveCallback), state);
                            }
                        }
                        else
                        {
                            // Get the rest of the dataQue.
                            control.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                new AsyncCallback(ReceiveCallback), state);
                        }
                    }
                    else
                    {
                        // All the dataQue has arrived; put it in response.
                        if (state.sb.Length > 1)
                        {
                            response = state.sb.ToString();
                            state.sb.Clear();
                            Console.WriteLine("[Communications] Response received : {0}", response);
                            receiveDone.Set();
                        }
                    }
                }
                else
                {
                    //Connection Lost
                    Console.WriteLine("[Communications] Connection to Server Lost...");
                    MessageBox.Show("Communications to the server has been lost.\n You may continue"
                        + " to view statistics and data.\n Please restart the program to regain connection");

                    control.Shutdown(SocketShutdown.Both);
                    control.Close();
                }
            }
            catch (Exception e)
            {
                if (e.InnerException.Equals(SocketError.NoBufferSpaceAvailable))
                {
                    Array.Clear(state.buffer, 0, state.buffer.Length);
                    Console.WriteLine("[ERROR] Cleared Buffer Array");
                    receiveDone.Set();
                }
                else
                {
                    Console.WriteLine("[ERROR] " + e.Message);

                    control.Shutdown(SocketShutdown.Both);
                    control.Close();
                    receiveDone.Set();
                }
            }
        }
        #endregion


        #region Send
        private static void Send(Socket control, String data)
        {
            // Convert the string dataQue to byte dataQue using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the dataQue to the remote device.
            control.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), control);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state Object.
                Socket control = (Socket)ar.AsyncState;

                // Complete sending the dataQue to the remote device.
                Int32 bytesSent = control.EndSend(ar);

                // Signal that all bytes have been sent.
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR] " + e.Message);
            }
        }

        #endregion
        

        #region Close and End Tasks
        private static void shutDown(IAsyncResult ar)
        {
            try
            {
                Socket control = (Socket)ar.AsyncState;

                control.Shutdown(SocketShutdown.Both);
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR] " + e.Message);
            }
        }
        #endregion

    }
}
