using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MessageTCPServer
{
    internal class Program
    {
        static TcpListener listener = new TcpListener(IPAddress.Any, 5000);
        static List<TcpClient> clients = new List<TcpClient>();
        static void Main(string[] args)
        {
            listener.Start();

            Task.Run(() =>
            {
                while (true)
                {
                    clients.Add(listener.AcceptTcpClient());
                    int n = clients.Count;
                    Task.Run(async () =>
                    {
                        while (true)
                        {
                            if (Program.clients.Count != 0)
                            {
                                await Task.Delay(10000);
                                var str = Program.clients[n - 1].GetStream();
                                byte[] buffer = Encoding.ASCII.GetBytes("type - " + Program.clients[n - 1].Client.AddressFamily);
                                str.Write(buffer, 0, buffer.Length);
                                str.Flush();
                            }
                        }
                    });
                    Task.Run(() =>
                    {
                        try
                        {
                            while (true)
                            {
                                NetworkStream networkStream = clients[n-1].GetStream(); 
                                StreamReader reader = new StreamReader(networkStream);

                                byte[] buffer = new byte[clients[n - 1].ReceiveBufferSize];

                                int bytesRead = networkStream.Read(buffer, 0, clients[n-1].ReceiveBufferSize);
                                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                                if (bytesRead!=0)
                                {
                                    Console.Write("\nReceived request: \n" + dataReceived);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        catch (Exception e)
                        {

                            Console.Write(e.Message);
                        }
                    });
                }
            });
            Console.ReadKey();
        }
    }
}
