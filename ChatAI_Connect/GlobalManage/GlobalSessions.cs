using ChatAI_Connect.Connect;
using ChatAI_Connect.Connect.TcpConn;
using ChatAI_Connect.LogManage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAI_Connect.GlobalManage
{
    public class GlobalSessions
    {
        public static Dictionary<string, ChatSession> SessionDic { get; private set; }

        public static void LoadLocalSessions(string sessionFolderPath)
        {
            SessionDic = new Dictionary<string, ChatSession>();
            // 检查目录是否存在
            if (!Directory.Exists(sessionFolderPath)) return;

            // 获取文件夹中所有的 .json 文件
            string[] fileEntries = Directory.GetFiles(sessionFolderPath, "*.json");
            foreach (string fileName in fileEntries)
            {
                try
                {
                    // 读取文件内容
                    string jsonContent = File.ReadAllText(fileName);

                    // 将 JSON 字符串转换为 ChatSession 对象
                    ChatSession session = JsonConvert.DeserializeObject<ChatSession>(jsonContent);

                    // 检查 session 是否为 null 和 SessionId 是否为空
                    if (session != null && !string.IsNullOrWhiteSpace(session.SessionId))
                    {
                        // 使用 SessionId 作为键将 session 添加到字典中
                        SessionDic[session.SessionId] = session;
                    }
                }
                catch (JsonException je)
                {
                    Console.WriteLine($"JSON文件读取异常 '{fileName}': {je.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"文件读取异常 '{fileName}': {ex.Message}");
                }
            }

        }

        public static ChatSession GetSessionByID(string sessionId)
        {
            ChatSession chatSession = null;
            if (string.IsNullOrWhiteSpace(sessionId) || !SessionDic.ContainsKey(sessionId))
            {
                CreateSession();
            }

            chatSession = SessionDic[sessionId];

            return chatSession;
        }

        public static void CreateSession(string sessionName = "新的会话")
        {
            ChatSession chatSession = new ChatSession(Guid.NewGuid().ToString(), sessionName);
            SessionDic.Add(chatSession.SessionId, chatSession);

        }

    }
}
