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
using static System.Net.Mime.MediaTypeNames;

namespace Test_WPF
{
    /// <summary>
    /// ChatMsg.xaml 的交互逻辑
    /// </summary>
    public partial class ChatMsg : UserControl
    {
        public ChatMsg()
        {
            InitializeComponent();
            UpdateTextContent(ContentText);
        }

        public ChatRoleType DisplayMode
        {
            get { return (ChatRoleType)GetValue(DisplayModeProperty); }
            set 
            { 
                SetValue(DisplayModeProperty, value);
                UpdateDisplayMode(value);
            }
        }

        public string ContentText
        {
            get { return (string)GetValue(ContentTextProperty); }
            set 
            { 
                SetValue(ContentTextProperty, value);
                UpdateTextContent(value);
            }
        }

        public static readonly DependencyProperty DisplayModeProperty =
            DependencyProperty.Register("DisplayMode", typeof(ChatRoleType), typeof(ChatMsg),
                new PropertyMetadata(ChatRoleType.Sender, new PropertyChangedCallback(OnDisplayModeChanged)));

        public static readonly DependencyProperty ContentTextProperty =
        DependencyProperty.Register("ContentText", typeof(string), typeof(ChatMsg),
            new PropertyMetadata("", new PropertyChangedCallback(OnTextContentChanged)));

        private static void OnTextContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ChatMsg;
            if (control != null)
            {
                control.UpdateTextContent(e.NewValue.ToString());
            }
        }

        private static void OnDisplayModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ChatMsg;
            if (control != null)
            {
                control.UpdateDisplayMode((ChatRoleType)e.NewValue);
            }
        }

        public void AppendText(string text)
        {
            switch (DisplayMode)
            {
                case ChatRoleType.Sender:
                    textBox.Text += text;
                    break;
                case ChatRoleType.Receiver:
                    richTextBox.Document.Blocks.Add(new Paragraph(new Run(text)));
                    break;
            }
        }

        private void UpdateTextContent(string text)
        {
            switch (DisplayMode)
            {
                case ChatRoleType.Sender:
                    textBox.Text = text;
                    break;
                case ChatRoleType.Receiver:
                    richTextBox.Document.Blocks.Clear();
                    richTextBox.Document.Blocks.Add(new Paragraph(new Run(text)));
                    break;
            }
        }

        private void UpdateDisplayMode(ChatRoleType mode)
        {
            switch (mode)
            {
                case ChatRoleType.Sender:
                    richTextBox.Visibility = Visibility.Collapsed;
                    textBox.Visibility = Visibility.Visible;
                    textBox.Text = ContentText;
                    break;
                case ChatRoleType.Receiver:
                    textBox.Visibility = Visibility.Collapsed;
                    richTextBox.Visibility = Visibility.Visible;
                    richTextBox.Document.Blocks.Clear();
                    richTextBox.Document.Blocks.Add(new Paragraph(new Run(ContentText)));

                    break;
            }
        }

        public RichTextBox GetRichTextBox
        {
            get { return richTextBox; }
        }
    }

    public enum ChatRoleType
    {
        Sender,
        Receiver
    }
}
