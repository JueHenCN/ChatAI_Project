using ChatAI_Connect.Connect.TcpConn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAI_Connect.Connect
{
    public class ChatSession
    {
        public string SessionId;

        // 会话名称
        public string SessionName;

        // 输出最大长度
        public int Max_Length;

        public float Top_P;

        public float Temperature;

        public string Conn_Type;

        public bool Save_History;

        public List<ChatHistory> History;

        public Action<string> ChatReceived;

        public Action ChatEnd;

        public ChatSession() { }

        public ChatSession(string sessionId, string sessionName)
        {
            SessionId = sessionId;
            SessionName = sessionName;
            Max_Length = 8192;
            Top_P = 0.8f;
            Temperature = 0.6f;
            Conn_Type = "tcp";
            Save_History = false;
        }
    }

    public class ChatHistory
    {
        public string Role;

        public string Metadata;

        public string Content;
    }
}
