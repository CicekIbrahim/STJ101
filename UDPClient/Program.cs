using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
IPAddress sendto = IPAddress.Parse("127.0.0.1");
IPEndPoint endpoint = new IPEndPoint(sendto, 5000);
List<string> str = new List<string>();
str.Add("abc");
str.Add("def");
str.Add("ghi");
var binFormatter = new BinaryFormatter();
var astream = new MemoryStream();
binFormatter.Serialize(astream, str);
socket.SendTo(astream.ToArray(),endpoint);
Console.ReadLine();