using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using XSockets.Consumers;
using XSockets.Utils;

namespace XSockets.Core
{
    public class UdpDataReceiver : DataReceiver
    {
        private static UdpClient _udpClient { get; set; }
        public ISocketConsumer _consumer { get; set; }

        /// <summary>
        /// Creates de Data Receiver UDP Server
        /// </summary>
        /// <param name="port"></param>
        public UdpDataReceiver(int port, ISocketConsumer consumer)
        {
            _udpClient = new UdpClient(port);
            _consumer = consumer;
            Logger.Log(LogStatusEnum.Info, "UDP Socket created in port " + port);
        }

        /// <summary>
        /// Start listening to new packets
        /// </summary>
        public void StartListening()
        {
            Logger.Log(LogStatusEnum.Info, "Socket started listening");
            ReceiveNextPacket();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        private void OnDataArrived(IAsyncResult result)
        {
            try
            {
                IPEndPoint endPoint = null;

                var message = _udpClient
                    .EndReceive(result, ref endPoint);

                if (Watchdog.IsWatchdogPingMessage(message))
                    _udpClient.Client.SendTo(Watchdog.PONG_MSG, endPoint);
                else
                {
                    _consumer.SetSocket(_udpClient.Client);
                    _consumer.Notify(message, endPoint);
                }

                Logger.Log(
                    LogStatusEnum.Info,
                    String.Format("Received data from {0}: {1}",
                        endPoint.Address.ToString(),
                        message.ByteArrayToHexString()));

                ReceiveNextPacket();
            }
            catch (Exception e)
            {
                Logger.Log(LogStatusEnum.Error, 
                    "Error ocurred in data receiving.", e);

                if (e.Source != "System.Net.Sockets")
                    ReceiveNextPacket();
            }
        }

        /// <summary>
        /// Receives the next data packet
        /// </summary>
        private void ReceiveNextPacket()
        {
            try
            {
                _udpClient.BeginReceive(OnDataArrived, null);
            }
            catch (Exception e)
            {
                Logger.Log(LogStatusEnum.Error,
                    "Error during UDP packet receiving", e);
            }
        }

        /// <summary>
        /// Disposes the Udp client
        /// </summary>
        public void Dispose()
        {
            _udpClient.Close();
            Logger.Log(LogStatusEnum.Info, "UDP Socket disposed.");
        }
    }
}
