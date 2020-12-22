using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace server_cs
{
    internal class fileuser
    {
       public delegate void LineReceive(fileuser sendUser, string message);

            public readonly int _bufferSize;
            private readonly byte[] Buffer;
            public readonly TcpClient Client;

            public fileuser(TcpClient client, int bufferSize = 10024)
            {
                Client = client;
                _bufferSize = bufferSize;
                Buffer = new byte[_bufferSize];
                Client.GetStream().BeginRead(Buffer, 0, Buffer.Length, Receive, null);
            }
            public event LineReceive LineReceived;

            public void Send(string message)
            {
                lock (Client.GetStream())
                {
                    var streamWriter = new StreamWriter(Client.GetStream());
                    streamWriter.Write(message + (char)10 + (char)13);
                    streamWriter.Flush();
                }
            }

            private void Receive(IAsyncResult iaAsyncResult)
            {
                try
                {
                    int byteRead;
                    lock (Client.GetStream())
                    {
                        byteRead = Client.GetStream().EndRead(iaAsyncResult);
                    }

                    LineReceived?.Invoke(this, Encoding.UTF8.GetString(Buffer, 0, byteRead - 1));
                    lock (Client.GetStream())
                    {
                        Client.GetStream().BeginRead(Buffer, 0, _bufferSize, Receive, null);
                    }
                }
                catch
                {
                    //ignored
                }
            }
        }
}
