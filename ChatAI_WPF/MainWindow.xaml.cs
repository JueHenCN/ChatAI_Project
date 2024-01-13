using ChatAI_Connect.GlobalManage;
using ChatAI_Connect.LogManage;
using ChatAI_WPF.Views;
using HandyControl.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Window = System.Windows.Window;

namespace ChatAI_WPF
{


    /// <summary>
    /// ChatAI_Main.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private ChatManageView chatWindow = new ChatManageView();
        private ChatGlobalSettingView localSetting = new ChatGlobalSettingView();
        private LogView logWindow = new LogView();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalConfig.LoadConfig();
        }

        private void ChatHome_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void ShowChatMessage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("显示聊天界面");
            ShowSelWindow(chatWindow);
        }

        private void ShowSessionList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("显示通讯录界面");
        }

        private void ShowGlobalSetting_MouseDown(object sender, MouseButtonEventArgs e)
        {
            localSetting.LoadConfig();
            ShowSelWindow(localSetting);
        }
        
        private void ShowLogWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowSelWindow(logWindow);
        }


        private void ShowSelWindow(UserControl selWindow)
        {
            //ShowWindow.Children.Clear();
            //ShowWindow.Children.Add(selWindow);
        }
    }
}
