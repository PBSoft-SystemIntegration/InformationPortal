using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;



TcpListener server = new TcpListener(IPAddress.Any, 8080);
server.Start();
List<int> ports = new List<int>() { 8081,8082};

while (true)
{
    TcpClient client = server.AcceptTcpClient();
    NetworkStream stream = client.GetStream();
 

    List<object> data = new List<object>();
    foreach (int port in ports)
    {
        TcpClient innerTcpClient = new TcpClient("localhost", port);

        Console.WriteLine($"contacting server on port: {port}");
        NetworkStream innerConnectionStream = innerTcpClient.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead = innerConnectionStream.Read(buffer, 0, buffer.Length);
        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        var message = JsonConvert.DeserializeObject<dynamic>(receivedData);

        Console.WriteLine($"received from server: {message}");
        data.Add(message);
        innerConnectionStream.Close();
        innerTcpClient.Close();
    }

    var ReturnMessage = new { ReturnInfo = data };
    string jsonData = JsonConvert.SerializeObject(ReturnMessage);
    byte[] byteData = Encoding.UTF8.GetBytes(jsonData);
    stream.Write(byteData, 0, byteData.Length);

    stream.Close();
    client.Close();
}
