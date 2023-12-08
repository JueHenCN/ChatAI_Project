using ChatAI_Connect.Connect.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAI_WPF.Source.Message
{
    public class Message_ChatOutput : IMessageProcessor
    {
        public Action<string, string> OutAIMessage;

        public string MessageType => "chat_output";

        public void ProcessMessage(Dictionary<string, string> messageData)
        {
            OutAIMessage.Invoke(messageData["tag"], messageData["message"]);
        }
    }
}
