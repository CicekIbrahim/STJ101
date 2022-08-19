using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

UdpClient listener = new UdpClient(5000);
IPEndPoint EP = new IPEndPoint(IPAddress.Any,5000);

try
{
    Console.WriteLine("waiting...");
    byte[] receivedbytes = listener.Receive(ref EP);
    Console.WriteLine("Received Message");
    var binFormatter = new BinaryFormatter();
    var astream = new MemoryStream(receivedbytes);
    List<string> list = binFormatter.Deserialize(astream) as List<string>;
    string data = list.ElementAt(0);
    Console.WriteLine("Data Received: " + data);

}
catch (Exception e)
{

    Console.WriteLine(e.ToString());
}
listener.Close();
Console.ReadKey();