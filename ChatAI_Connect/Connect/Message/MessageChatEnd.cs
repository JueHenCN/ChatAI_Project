using ChatAI_Connect.Connect.Message;
using System;
using System.Collections.Generic;

namespace ChatAI_WPF.Source.Message
{
    public class MessageChatEnd : IMessageProcessor
    {
        public Action<string> ChatEndAction;

        public string MessageType => "chat_end";

        public void ProcessMessage(Dictionary<string, string> messageData)
        {
            ChatEndAction?.Invoke(messageData["tag"]);
        }
    }
}
