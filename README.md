# Shredder - UDP Flooding Tool
Shredder is a simple UDP flooding tool implemented in C# that allows you to perform UDP flood attacks on a specified IP address and port. This tool can be useful for testing the resilience of network infrastructure against UDP flood attacks or for educational purposes to understand the impact of such attacks. Please use this tool responsibly and only on networks that you have permission to test.

## Getting Started
To use Shredder, follow these steps:

1. Clone the repository:

```sh
git clone https://github.com/your_username/Shredder.git
```

2. Navigate to the project directory:
```sh
cd Shredder
```

3. Build the project:

```sh
dotnet build
```

4. Run the application:

```sh
dotnet run
```

## Technical details
This class represents the main functionality of the Shredder tool. It provides methods to flood the target IP address and port with UDP packets and display information about the attack's progress.

```cs
public Shred(string ip, int port, int force, int threads)
```

- `ip`: The target IP address to flood.
- `port`: The target port to flood.
- `force`: The size of the UDP payload in each packet (default: 1250 bytes).
- `threads`: The number of threads to use for flooding (default: 100 threads).

`void Flood()`
Initiates the UDP flood attack by creating multiple threads to send UDP packets to the target.

`void Info()`
Displays information about the flood attack, such as the current flood rate in Mb/s and the total data sent in Gb.

`void Stop()`
Stops the UDP flood attack.

`void Send()`
The method called by each thread to send UDP packets to the target IP address and port.

### Other Private Helper Methods
- `string Stage(string text, char symbol = '.')`: Formats the text with a symbol and returns it.
- `EndPoint GetEndpoint()`: Returns the IPEndPoint representing the target IP address and port.
- `int GetPort()`: Returns the target port if specified; otherwise, generates a random port number.
- `static double GetTime()`: Returns the current time in seconds.

## Disclaimer 
Remember, using this tool for malicious purposes or on unauthorized networks is illegal and unethical. Always obtain proper authorization before using it for testing or educational purposes.
