using System;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace ClientSocketApp

{
    class Program
    {

        static void Main(string[] args)
        {
        connection:
            try
            {
                Console.Write("Enter IP adress: ");
                string ip = Console.ReadLine();
                Console.Write("Enter Port: ");
                int port = int.Parse(Console.ReadLine());
                TcpClient client = new TcpClient(ip, port);
                Console.Write("Your text: ");
                string messageToSend = Console.ReadLine();
                int byteCount = Encoding.ASCII.GetByteCount(messageToSend + 1);
                byte[] sendData = Encoding.ASCII.GetBytes(messageToSend);

                NetworkStream stream = client.GetStream();
                stream.Write(sendData, 0, sendData.Length);
                Console.WriteLine("sending data to server...");

                StreamReader sr = new StreamReader(stream);
                string response = sr.ReadLine();
                Console.WriteLine(response);

                stream.Close();
                client.Close();
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("failed to connect...");
                goto connection;
            }
            
        }
    }
}
