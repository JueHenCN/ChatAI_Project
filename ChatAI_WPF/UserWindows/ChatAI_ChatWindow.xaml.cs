using ChatAI_Connect.GlobalManage;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ChatAI_WPF.UserWindows
{
    /// <summary>
    /// ChatWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChatAI_ChatWindow : UserControl
    {
        public ChatAI_ChatWindow()
        {
            InitializeComponent();
        }

        private Dictionary<string, ChatAI_Message> sessionWindow = new Dictionary<string, ChatAI_Message>();

        private void CreateSession_Click(object sender, RoutedEventArgs e)
        {
            CreateNewSession();
        }

        public void LoadLocalSessions(string sessionFolderPath)
        {
            GlobalSessions.LoadLocalSessions(sessionFolderPath);

            foreach (var item in GlobalSessions.SessionDic.Values)
            {
                Button newSessionButton = new Button
                {
                    Content = item.SessionName,
                    Width = 198,
                    Height = 38,
                    Tag = item.SessionId
                };

                newSessionButton.Click += NewSessionButton_Click;

                SessionList.Children.Add(newSessionButton);

                ChatAI_Message messageWindow = new ChatAI_Message();
                sessionWindow.Add(item.SessionId, messageWindow);
            }
        }

        private void CreateNewSession()
        {
            var session = GlobalSessions.CreateSession(); // 创建新的会话; // 创建GUID

            // 创建新的Button
            Button newSessionButton = new Button
            {
                Content = "新的会话",
                Width = 198,
                Height = 38,
                Tag = session.SessionId // 使用Tag属性存储GUID
            };

            newSessionButton.Click += NewSessionButton_Click; // 添加点击事件处理程序

            SessionList.Children.Add(newSessionButton); // 将新按钮添加到WrapPanel中


            ChatAI_Message messageWindow = new ChatAI_Message(); // 创建新的消息窗口
            sessionWindow.Add(session.SessionId, messageWindow); // 将新的消息窗口添加到字典中

        }

        private void NewSessionButton_Click(object sender, RoutedEventArgs e)
        {
            string sessionId = (sender as Button).Tag.ToString(); // 获取GUID
            MessageShow.Children.Clear(); // 清空消息显示区域
            ChatAI_Message messageWindow = sessionWindow[sessionId]; // 获取消息窗口
            messageWindow.LoadSession(sessionId); // 加载会话
            MessageShow.Children.Add(messageWindow); // 将消息窗口添加到消息显示区域
        }

        private void Btn_StartModel_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
