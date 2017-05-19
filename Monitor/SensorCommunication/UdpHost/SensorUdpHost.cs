using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.SensorCommunication.UdpHost
{
    public class SensorUdpHost
    {
        private readonly IMessageHandler _messageHandler;
        private readonly string _ipAddress;
        private readonly int _port;

        public SensorUdpHost(IMessageHandler messageHandler, string ipAddress, int port)
        {
            _messageHandler = messageHandler;
            _ipAddress = ipAddress;
            _port = port;
        }

        public void Start()
        {
            var ip = new IPEndPoint(IPAddress.Parse(_ipAddress), _port);
            var udpClient = new UdpClient(ip);

            var sender = new IPEndPoint(IPAddress.Any, 0);

            Console.Out.WriteLine($"Listening for sensors on {_ipAddress}:{_port}");

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    var data = udpClient.Receive(ref sender);
                    var message = Encoding.UTF8.GetString(data);
                    _messageHandler.Handle(message);
                }
            });

        }
    }
}
