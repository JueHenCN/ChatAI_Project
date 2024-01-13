using ChatAI_Connect.GlobalManage;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using MessageBox = HandyControl.Controls.MessageBox;
using Growl = HandyControl.Controls.Growl;

namespace ChatAI_WPF.Views
{
    /// <summary>
    /// LocalSetting.xaml 的交互逻辑
    /// </summary>
    public partial class ChatGlobalSettingView : UserControl
    {
        public ChatGlobalSettingView()
        {
            InitializeComponent();
        }

        public void LoadConfig()
        {
            TextB_IPAddress.Text = GlobalConfig.Config.IpAddress;
            NumericUD_Port.Value = GlobalConfig.Config.Port;
            TextB_SessionFolder.Text = GlobalConfig.Config.SessionFolderPath;
            TextB_ModelPath.Text = GlobalConfig.Config.ModelPath;
            TextB_PythonPath.Text = GlobalConfig.Config.PythonPath;
        }

        public void SaveConfig()
        {
            GlobalConfig.Config.IpAddress = TextB_IPAddress.Text;
            GlobalConfig.Config.Port = (int)NumericUD_Port.Value;
            GlobalConfig.Config.SessionFolderPath = TextB_SessionFolder.Text;
            GlobalConfig.Config.ModelPath = TextB_ModelPath.Text;
            GlobalConfig.Config.PythonPath = TextB_PythonPath.Text;
            GlobalConfig.SaveConfig();
        }

        private void Btn_ResetConfig_Click(object sender, RoutedEventArgs e)
        {
            var msgBoxResult = MessageBox.Show("是否重新载入", "恢复配置", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (msgBoxResult == MessageBoxResult.Yes)
                LoadConfig();
        }

        private void Btn_SaveConfig_Click(object sender, RoutedEventArgs e)
        {
            var msgBoxResult = MessageBox.Show("是否保存配置", "保存配置", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (msgBoxResult == MessageBoxResult.Yes)
            {
                SaveConfig();
            }
        }

        private void SelFolder_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string openType = button.Tag.ToString();

            var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            folderBrowserDialog.Description = "请选择文件夹";

            // 显示对话框并获取结果
            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                string selectedFolderPath = folderBrowserDialog.SelectedPath;
                switch (openType)
                {
                    case "SessionFolderPath":
                        TextB_SessionFolder.Text = selectedFolderPath;
                        break;
                    case "ModelPath":
                        TextB_ModelPath.Text = selectedFolderPath;
                        break;
                    case "PythonPath":
                        TextB_PythonPath.Text = selectedFolderPath;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
