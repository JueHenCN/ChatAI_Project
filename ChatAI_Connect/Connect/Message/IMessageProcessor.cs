using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAI_Connect.Connect.Message
{
    public interface IMessageProcessor
    {
        string MessageType { get; }

        void ProcessMessage(Dictionary<string, string> messageData);
    }
}
