using ChatAI_Connect.Connect;
using ChatAI_Connect.Connect.TcpConn;
using ChatAI_Connect.LogManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAI_Connect.GlobalManage
{
    public class GlobalConnect
    {
        public static TcpClientConn TcpClientConn = new TcpClientConn();

        public static bool ConnectServer()
        {
            if (!TcpClientConn.IsConnected)
            {
                TcpClientConn.ClientReceivedMessage += RevClientMessage;
                return TcpClientConn.Connect(GlobalConfig.Config.Port, GlobalConfig.Config.IpAddress);
            }
            return true;

        }

        public static void StartModel()
        {
            SendMessage(new Dictionary<string, string>
            {
                { "type", "model_start" },
                { "model_path", GlobalConfig.Config.ModelPath }
            });
        }

        public static void LoadSessionList()
        {
            SendMessage(new Dictionary<string, string>
            {
                { "type", "load_local_sessions" },
                { "local_folder", GlobalConfig.Config.ModelPath }
            });
        }

        public static void SendMessage(Dictionary<string, string> msgData)
        {
            if (!TcpClientConn.IsConnected)
                return;
            string jsonMsg = JsonMessage.SerialMessage(msgData);
            TcpClientConn.SendMessage(jsonMsg);
        }

        public static void SendChatMsg(string sessionId, string msg)
        {
            if (string.IsNullOrWhiteSpace(msg))
                return;

            SendMessage(new Dictionary<string, string>
            {
                { "type", "chat_input" },
                { "session_id", sessionId },
                { "message", msg }
            });
        }

        public static void RevClientMessage(string msg)
        {
            var clientMsg = JsonMessage.DeserialMessage(msg);

            ChatSession chatSession = GlobalSessions.GetSessionByID(clientMsg["session_id"]);

            switch (clientMsg["type"])
            {
                case "console":
                    if (bool.Parse(clientMsg["return_result"]))
                    {
                        LogHandler.Info(clientMsg["message"]);
                    }
                    else
                    {
                        LogHandler.Error(clientMsg["message"]);
                    }
                    break;
                case "chat":
                    chatSession.ChatReceived?.Invoke(clientMsg["message"]);
                    break;
                case "chat_end":
                    chatSession.ChatEnd?.Invoke();
                    break;
            }
        }
    }
}
