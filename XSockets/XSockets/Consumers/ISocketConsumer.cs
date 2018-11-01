using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace XSockets.Consumers
{
    public interface ISocketConsumer
    {
        void SetSocket(Socket socket);
        void Notify(byte[] message, IPEndPoint endPoint);
    }
}
