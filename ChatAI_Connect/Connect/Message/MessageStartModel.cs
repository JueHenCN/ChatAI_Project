using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAI_Connect.Connect.Message
{
    public class MessageStartModel : IMessageProcessor
    {
        public Action<string> ModelReadyAction;
        public Action<bool, string> ModelStartAction;
        public Action<bool, string> LoadLoaclSessionAction;

        public string MessageType => "model_start";

        public void ProcessMessage(Dictionary<string, string> messageData)
        {
            switch (messageData["tag"])
            {
                case "model_start":
                    ModelStartAction?.Invoke(bool.Parse(messageData["return_result"]), messageData["message"]);
                    break;
                case "model_ready":
                    ModelReadyAction?.Invoke(messageData["message"]);
                    break;
                case "load_local_session":
                    LoadLoaclSessionAction?.Invoke(bool.Parse(messageData["return_result"]), messageData["message"]);
                    break;
            }

        }
    }
}
