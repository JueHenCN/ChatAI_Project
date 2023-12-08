﻿using ChatAI_Connect.Connect.Message;
using ChatAI_Connect.GlobalManage;
using ChatAI_WPF.Source.Message;
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
        private Dictionary<string, ChatAI_Message> sessionWindow;
        private Message_ChatEnd chatEnd;
        private Message_ChatOutput chatOutputMsg;

        public ChatAI_ChatWindow()
        {
            InitializeComponent();
        }

        private void ChatWindow_Loaded(object sender, RoutedEventArgs e)
        {
            chatEnd = new Message_ChatEnd();
            chatOutputMsg = new Message_ChatOutput();
            sessionWindow = new Dictionary<string, ChatAI_Message>();

            chatEnd.ChatEndAction += ChatEnd;
            chatOutputMsg.OutAIMessage += ChatOutputMsg;

            GlobalConnect.AddMessageProcessor(new IMessageProcessor[]
            {
                chatOutputMsg, chatEnd
            });
        }

        private void ChatOutputMsg(string sessionId, string msg)
        {
            sessionWindow[sessionId].AppendAIMessage(msg);
        }

        private void ChatEnd(string sessionId)
        {
            sessionWindow[sessionId].AIMessageChatEnd();
        }

        private void CreateSession_Click(object sender, RoutedEventArgs e)
        {
            CreateSessionButton(Guid.NewGuid().ToString());
        }

        public void LoadLocalSessions(string sessionFolderPath)
        {
            GlobalSessions.LoadLocalSessions(sessionFolderPath);

            foreach (var item in GlobalSessions.SessionDic.Values)
            {
                CreateSessionButton(item.SessionId);
            }
        }

        private void CreateSessionButton(string sessionId)
        {
            // 创建新的Button
            Button newSessionButton = new Button
            {
                Content = "新的会话",
                Width = 198,
                Height = 38,
                Tag = sessionId // 使用Tag属性存储GUID
            };

            newSessionButton.Click += NewSessionButton_Click; // 添加点击事件处理程序
            SessionList.Children.Add(newSessionButton); // 将新按钮添加到WrapPanel中

            ChatAI_Message messageWindow = new ChatAI_Message(); // 创建新的消息窗口
            sessionWindow.Add(sessionId, messageWindow); // 将新的消息窗口添加到字典中
        }

        private void NewSessionButton_Click(object sender, RoutedEventArgs e)
        {
            string sessionId = (sender as Button).Tag.ToString(); // 获取GUID
            MessageShow.Children.Clear(); // 清空消息显示区域
            ChatAI_Message messageWindow = sessionWindow[sessionId]; // 获取消息窗口
            messageWindow.LoadSession(sessionId); // 加载会话
            MessageShow.Children.Add(messageWindow); // 将消息窗口添加到消息显示区域
        }
    }
}
