using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test_WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private ChatMsg aiMsg;

        private void Btn_SendMessage_Click(object sender, RoutedEventArgs e)
        {
            //AppendAIMessage("你好");
            //AppendUserMessage("很高兴认识你");
            //aiMsg = null;

            Task.Run(async () =>
            {
                List<string> msgList = new List<string>()
                {
                    "你好", "很高兴", "认识你", "，",
                    "我", "是", "你的", "人工", "智能", "助手",
                    "ChatGLM3", "。"
                };

                foreach (var item in msgList)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        AppendAIMessage(item);
                    });
                    await Task.Delay(500);
                }
                aiMsg = null;
            });
        }

        private void AppendAIMessage(string msg)
        {
            if (null != aiMsg)
            {
                //if (aiMsg.GetRichTextBox.Document.Blocks.LastBlock is Paragraph paragraph)
                //{
                //    // 在现有段落中添加文本
                //    paragraph.Inlines.Add(new Run(msg));
                //}
                //else
                //{
                //    // 创建新的段落
                //    paragraph = new Paragraph(new Run(msg));
                //    aiMsg.GetRichTextBox.Document.Blocks.Add(paragraph);
                //}

                aiMsg.AppendText(msg);
            }
            else
            {
                ListBoxItem newItem = new ListBoxItem() { HorizontalAlignment = HorizontalAlignment.Right };
                StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
                aiMsg = new ChatMsg()
                {
                    DisplayMode = ChatRoleType.Sender,
                    ContentText = msg
                };
                stackPanel.Children.Add(aiMsg);
                newItem.Content = stackPanel;
                ListBoxChat.Items.Add(newItem);
            }
        }

        private void TextB_SendString_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
