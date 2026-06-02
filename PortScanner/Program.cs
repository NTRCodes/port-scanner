using System;
using System.Net.Sockets;

string target = "127.0.0.1";
int port = 80;

using var client = new TcpClient();
var result = client.BeginConnect(target, port, null, null);
bool success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));

if (success)
    Console.WriteLine($"Port {port} is OPEN");
else
    Console.WriteLine($"Port {port} is CLOSED");