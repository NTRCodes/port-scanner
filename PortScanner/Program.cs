using System.Net.Sockets;

int startPort = 0;
int endPort = 65535;

Console.WriteLine("If you want to scan a different target, please enter the IP address or hostname (or press Enter to use localhost):");
string argument = Console.ReadLine() ?? "";

string target = argument.Trim() != "" ? argument.Trim() : "127.0.0.1";

Console.WriteLine($"Scanning {target} from port {startPort} to {endPort}...\n");

var tasks = new List<Task>();

for (int port = startPort; port <= endPort; port++)
{
    int currentPort = port;
    tasks.Add(Task.Run(async () =>
    {
        using var client = new TcpClient();
        try
        {
            var result = client.BeginConnect(target, currentPort, null, null);
            bool success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(100));
            if (success)
                Console.WriteLine($"Port {currentPort} is OPEN");
        }
        catch { }
    }));
}

await Task.WhenAll(tasks);
Console.WriteLine("\nScan complete.");