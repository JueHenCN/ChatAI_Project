using ChatAI_Connect.GlobalManage;
using ChatAI_Connect.LogManage;
using ChatAI_WPF.UserWindows;
using System.Windows;
using System.Windows.Input;
using Growl = HandyControl.Controls.Growl;

namespace ChatAI_WPF
{
    /// <summary>
    /// ChatAI_Main.xaml 的交互逻辑
    /// </summary>
    public partial class ChatAI_Main : Window
    {
        public ChatAI_Main()
        {
            InitializeComponent();
        }

        private ChatAI_ChatWindow chatWindow = new ChatAI_ChatWindow();
        private ChatAI_GlobalSetting localSetting = new ChatAI_GlobalSetting();

        private void Btn_ChatMessage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowChatMessage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("显示聊天界面");
            ShowWindow.Children.Clear();
            ShowWindow.Children.Add(chatWindow);
        }

        private void ShowSessionList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("显示通讯录界面");
        }

        private void ShowGlobalSetting_MouseDown(object sender, MouseButtonEventArgs e)
        {
            localSetting.LoadConfig();
            ShowWindow.Children.Clear();
            ShowWindow.Children.Add(localSetting);
        }

        private void GlobalInfoShow(LogInfo logInfo)
        {
            switch (logInfo.LogLevel)
            {
                case LogLevel.Info:
                    Growl.Info(logInfo.Message);
                    break;
                case LogLevel.Fatal:
                    Growl.Warning(logInfo.Message);
                    break;
                case LogLevel.Error:
                    Growl.Error(logInfo.Message);
                    break;
            }
        }

        private void ChatModelStart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (GlobalConnect.ConnectServer())
            {
                GlobalConnect.StartModel();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LogHandler.LogInfoGenerated += GlobalInfoShow;
            GlobalConfig.LoadConfig();
        }

        private void ChatHome_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
