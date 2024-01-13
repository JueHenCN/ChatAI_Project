using ChatAI_Connect.Connect.TcpConn;
using ChatAI_Connect.LogManage;
using Newtonsoft.Json;
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

        public ChatSession() { }

        public ChatSession(Dictionary<string, string> sessionData)
        {
            try
            {
                SessionId = sessionData["session_id"];
                SessionName = sessionData["session_name"];
                Max_Length = int.Parse(sessionData["max_length"]);
                Top_P = float.Parse(sessionData["top_p"]);
                Temperature = float.Parse(sessionData["temperature"]);
                Conn_Type = sessionData["conn_type"];
                Save_History = bool.Parse(sessionData["save_history"]);
                History = JsonConvert.DeserializeObject<List<ChatHistory>>(sessionData["history"]);
            }
            catch (Exception err)
            {
                LogHandler.Error($"ChatSession初始化异常{err.Message}");
            }
        }

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
