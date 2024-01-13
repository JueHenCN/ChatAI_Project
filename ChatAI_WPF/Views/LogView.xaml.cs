using ChatAI_Connect.LogManage;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatAI_WPF.Views
{
    /// <summary>
    /// ChatAI_LogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LogView : UserControl
    {
        public LogView()
        {
            InitializeComponent();
            RichTB_LogShow.Document.Blocks.Clear();
            LogHandler.LogInfoGenerated += LogAppend;
        }

        private void LogAppend(LogInfo logInfo)
        {
            switch (logInfo.LogLevel)
            {
                case LogLevel.Info:
                    AppendLog(logInfo, Brushes.Black);
                    break;
                case LogLevel.Fatal:
                    AppendLog(logInfo, Brushes.Orange);
                    break;
                case LogLevel.Error:
                    AppendLog(logInfo, Brushes.Red);
                    break;
                case LogLevel.Debug:
                    AppendLog(logInfo, Brushes.Blue);
                    break;
            }
            
        }

        private void AppendLog(LogInfo logInfo, SolidColorBrush color)
        {
            Dispatcher.Invoke(() =>
            {
                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run($"[{logInfo.Time}] [{logInfo.LogLevel}] {logInfo.Source}: {logInfo.Message}")
                {
                    Foreground = color
                });

                RichTB_LogShow.Document.Blocks.Add(paragraph);
                RichTB_LogShow.ScrollToEnd();
            });
        }
    }
}
