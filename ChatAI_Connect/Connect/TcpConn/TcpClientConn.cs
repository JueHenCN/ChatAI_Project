using ChatAI_Connect.LogManage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchSocket.Core;
using TouchSocket.Sockets;
using LogLevel = ChatAI_Connect.LogManage.LogLevel;

namespace ChatAI_Connect.Connect.TcpConn
{
    public class TcpClientConn
    {
        private TcpClient tcpClient;
        private bool isConnect = false;
        public Action<string> ClientReceivedMessage;

        public bool IsConnected
        {
            get
            {
                return isConnect;
            }
        }

        public bool Connect(int port, string ipAddress = "127.0.0.1")
        {
            tcpClient = new TcpClient();
            tcpClient.Connecting += ClientConnection; //即将连接到服务器，此时已经创建socket，但是还未建立tcp
            tcpClient.Connected += ClientConnected;//已经连接到服务器，此时tcp已经建立，但是还未开始发送数据
            tcpClient.Disconnecting = ClientDisconnection;//即将从服务器断开连接。此处仅主动断开才有效。
            tcpClient.Disconnected = ClientDisconnected;//从服务器断开连接，当连接不成功时不会触发。
            tcpClient.Received += ClientReceived;
            //载入配置
            tcpClient.Setup(new TouchSocketConfig().SetRemoteIPHost($"{ipAddress}:{port}") );
            try
            {
                if (!isConnect)
                {
                    LogHandler.Info("正在连接服务器...");
                    tcpClient.Connect();
                    isConnect = true;
                    LogHandler.Info("服务器连接成功");
                }
                else
                {
                    LogHandler.Info("服务器已连接");
                }
                return true;
            }
            catch (Exception ex)
            {
                isConnect = false;
                LogHandler.Error($"服务器连接异常:{ex.Message}");
                return false;
            }
        }

        private Task ClientReceived(TcpClient client, ReceivedDataEventArgs e)
        {
            //从服务器收到信息。但是一般byteBlock和requestInfo会根据适配器呈现不同的值。
            var msg = Encoding.UTF8.GetString(e.ByteBlock.Buffer, 0, e.ByteBlock.Len);
            LogHandler.Debug($"客户端接收到信息：{msg}");

            ClientReceivedMessage?.Invoke(msg);

            return EasyTask.CompletedTask;
        }

        public void SendMessage(string msg)
        {
            try
            {
                tcpClient.SendAsync(msg);
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex.Message);
            }
        }

        private Task ClientConnection(ITcpClient client, ConnectingEventArgs e)
        {
            return EasyTask.CompletedTask;
        }

        private Task ClientConnected(ITcpClient client, ConnectedEventArgs e)
        {
            LogHandler.Debug("服务端连接成功");
            return EasyTask.CompletedTask;
        }

        private Task ClientDisconnection(ITcpClientBase client, DisconnectEventArgs e)
        {
            return EasyTask.CompletedTask;
        }

        private Task ClientDisconnected(ITcpClientBase client, DisconnectEventArgs e)
        {
            LogHandler.Error("服务器连接已断开");
            isConnect = false;
            return EasyTask.CompletedTask;
        }

    }
}
