using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;



string serverIp = "127.0.0.1";
TcpClient client = new TcpClient(serverIp, 8080);
NetworkStream stream = client.GetStream();

byte[] data = Encoding.UTF8.GetBytes("jsonData");
stream.Write(data, 0, data.Length);

// Modtag svar fra serveren
byte[] buffer = new byte[1024];
int bytesRead = stream.Read(buffer, 0, buffer.Length);
string responseData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
Console.WriteLine("Server response: " + responseData);
stream.Close();
client.Close();
