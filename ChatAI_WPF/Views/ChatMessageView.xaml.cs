using ChatAI_Connect.Connect;
using ChatAI_Connect.GlobalManage;
using HandyControl.Controls;
using HandyControl.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace ChatAI_WPF.Views
{
    /// <summary>
    /// MessageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChatMessageView : UserControl
    {
        public ChatMessageView()
        {
            InitializeComponent();
        }

        public ChatMessageView(string sessionId)
        {
            InitializeComponent();
            LoadSession(sessionId);
        }

        private Paragraph aiChat = null;

        public void LoadSession(string sessionId)
        {
            ChatSession bindingSession = GlobalSessions.GetSessionByID(sessionId);

            foreach (var item in bindingSession.History)
            {
                if (item.Role.Equals("user"))
                {
                    AppendUserMessage(item.Content);
                }
                else
                {
                    AppendAIMessage(item.Content);
                }
            }
            //AppendAIMessage("你好我是你的人工智能助手 ChatGLM3");
        }

        private void SendMessage()
        {
            string msg = TextB_SendString.Text;
            if (string.IsNullOrWhiteSpace(msg))
                return;
            AppendUserMessage(msg);
            TextB_SendString.Clear();

        }

        private void AppendUserMessage(string msg)
        {
            ListBoxItem newItem = new ListBoxItem() { HorizontalAlignment = HorizontalAlignment.Right };
            StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            ChatBubble chatBubble = new ChatBubble()
            {
                Role = ChatRoleType.Sender,
                Content = msg
            };
            stackPanel.Children.Add(chatBubble);
            newItem.Content = stackPanel;
            ListBoxChat.Items.Add(newItem);
        }

        public void AIMessageChatEnd()
        {
            aiChat = null;
        }

        public void AppendAIMessage(string msg)
        {
            if (null != aiChat)
            {
                aiChat.Inlines.Add(new Run(msg));
            }
            else
            {
                ListBoxItem newItem = new ListBoxItem() { HorizontalAlignment = HorizontalAlignment.Left };
                StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
                RichTextBox richText = new RichTextBox()
                {
                    Width = 300,
                    IsReadOnly = true,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFE0E0E0")
                };
                aiChat = new Paragraph(new Run(msg));
                richText.Document.Blocks.Clear();
                richText.Document.Blocks.Add(aiChat);
                stackPanel.Children.Add(richText);
                newItem.Content = stackPanel;
                ListBoxChat.Items.Add(newItem);
            }
        }

        private void Btn_SendMessage_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void TextB_SendString_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                SendMessage();
            }
        }


    }
}
