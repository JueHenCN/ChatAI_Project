using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAI_Connect.Connect.Message
{
    public class MessageGetSessionList : IMessageProcessor
    {
        public string MessageType => "get_session_list";

        public void ProcessMessage(Dictionary<string, string> messageData)
        {
            
        }
    }
}
