using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPDataTransfer
{
    public class TcpServer
    {
        private readonly TcpListener _objServer;
        public TcpServer()
        {
            _objServer = new TcpListener(IPAddress.Any, 6868);
        }

        public void StartServer()
        {
            _objServer.Start();
            while (true)
            {
                var tc = _objServer.AcceptTcpClient();
                var objHandler = new SocketHandler(tc);
                var objThread = new System.Threading.Thread(objHandler.ProcessSocketRequest);
                objThread.Start();
            }
        }

        private class SocketHandler
        {
            private readonly NetworkStream _ns;
            public SocketHandler(TcpClient tc)
            {
                _ns = tc.GetStream();
            }

            public void ProcessSocketRequest()
            {
                FileStream fs = null;
                long currentFilePointer = 0;
                var loopBreak = false;
                while (true)
                {
                    if (_ns.ReadByte() == 2)
                    {
                        var cmdBuff = new byte[3];
                        _ns.Read(cmdBuff, 0, cmdBuff.Length);
                        var recvData = ReadStream();
                        switch (Convert.ToInt32(Encoding.UTF8.GetString(cmdBuff)))
                        {
                            case 125:
                                {
                                    fs = new FileStream(@"D:\Games\" + Encoding.UTF8.GetString(recvData), FileMode.CreateNew);
                                    var dataToSend = CreateDataPacket(Encoding.UTF8.GetBytes("126"),
                                        Encoding.UTF8.GetBytes(Convert.ToString(currentFilePointer)));
                                    _ns.Write(dataToSend, 0, dataToSend.Length);
                                    _ns.Flush();
                                }
                                break;
                            case 127:
                                {
                                    if (fs != null)
                                    {
                                        fs.Seek(currentFilePointer, SeekOrigin.Begin);
                                        fs.Write(recvData, 0, recvData.Length);
                                        currentFilePointer = fs.Position;
                                    }

                                    var dataToSend = CreateDataPacket(Encoding.UTF8.GetBytes("126"),
                                        Encoding.UTF8.GetBytes(Convert.ToString(currentFilePointer)));
                                    _ns.Write(dataToSend, 0, dataToSend.Length);
                                    _ns.Flush();
                                }
                                break;
                            case 128:
                                {
                                    fs?.Close();
                                    loopBreak = true;
                                }
                                break;

                            default:
                                break;
                        }
                    }

                    if (loopBreak != true) continue;
                    _ns.Close();
                    break;
                }
            }

            private byte[] ReadStream()
            {
                int b;
                var buffLength = "";
                while ((b = _ns.ReadByte()) != 4)
                {
                    buffLength += (char)b;
                }
                var dataLength = Convert.ToInt32(buffLength);
                var dataBuff = new byte[dataLength];
                var byteOffset = 0;
                while (byteOffset < dataLength)
                {
                    var byteRead = _ns.Read(dataBuff, byteOffset, dataLength - byteOffset);
                    byteOffset += byteRead;
                }

                return dataBuff;
            }

            private static byte[] CreateDataPacket(byte[] cmd, byte[] data)
            {
                var initialize = new byte[1];
                initialize[0] = 2;
                var separator = new byte[1];
                separator[0] = 4;
                var dataLength = Encoding.UTF8.GetBytes(Convert.ToString(data.Length));
                var ms = new MemoryStream();
                ms.Write(initialize, 0, initialize.Length);
                ms.Write(cmd, 0, cmd.Length);
                ms.Write(dataLength, 0, dataLength.Length);
                ms.Write(separator, 0, separator.Length);
                ms.Write(data, 0, data.Length);
                return ms.ToArray();
            }
        }

    }
}