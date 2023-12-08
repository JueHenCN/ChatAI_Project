using ChatAI_Connect.Connect.TcpConn;
using ChatAI_Connect.LogManage;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAI_Connect.Connect
{
    public class JsonMessage
    {
        public static string SerialMessage(Dictionary<string, string> msg)
        {
            string jsonMsg = JsonConvert.SerializeObject(msg);
            return jsonMsg;
        }

        public static Dictionary<string, string> DeserialMessage(string msg)
        {
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(msg);
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex.Message);
                return null;
            }
        }
    }
}
