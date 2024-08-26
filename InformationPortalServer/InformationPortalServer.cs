using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;


int port;
int.TryParse(args[0], out port);
string returnInfo = args[1];
TcpListener server = new TcpListener(IPAddress.Any, port);
server.Start();
Console.WriteLine($"started listening on port {port} returning {returnInfo} when asked...");
while (true)
{
    TcpClient client = server.AcceptTcpClient();
    NetworkStream stream = client.GetStream();
    var message = new { ReturnInfo = returnInfo };
    string jsonData = JsonConvert.SerializeObject(message);
    byte[] data = Encoding.UTF8.GetBytes(jsonData);
    stream.Write(data, 0, data.Length);

    stream.Close();
    client.Close();
}
